// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) 2022 Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using MongoDB.Driver;
using MongoDB.Bson;
using Telegram.Bot.Types;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Utils
{
        class AdminCheck
        {
                private Message _message;
                private ITelegramBotClient _botClient;
                private InputTelegram _inputTelegram;
                private long[] _user_ids;

                public AdminCheck(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this._message = message;
                        this._botClient = botClient;
                        this._inputTelegram = inputTelegram;
                }

                private long _timeNow()
                {
                        var now = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
                        return (long)now.TotalSeconds;
                }

                private async Task _makeCache()
                {
                        Telegram.Bot.Types.ChatMember[] chatmember = await this._botClient.GetChatAdministratorsAsync(
                                chatId: this._message.Chat.Id,
                                cancellationToken: this._inputTelegram.cancellationToken
                        );
                        long[] user_ids = new long[chatmember.Length];
                        int i = 0;

                        foreach (Telegram.Bot.Types.ChatMember members in chatmember) {
                                user_ids[i] = members.User.Id;
                                //Console.WriteLine(members.User.Id);
                                i++;
                        }


                        var dbctx = this._inputTelegram.main_thread_ctx.mongodbCtx.GetDatabase(Config.MongoDB_DBNAME);

                        var dbcol = dbctx.GetCollection<BsonDocument>("admin_cache");

                        try {
                                var filter = Builders<BsonDocument>.Filter.Eq("chat_id", this._message.Chat.Id);
                                var filtered = await dbcol.Find(filter).FirstAsync();
                        } catch (InvalidOperationException) {
                                var document = new BsonDocument
                                {
                                        { "chat_id", this._message.Chat.Id },
                                        { "timestamp", this._timeNow()},
                                        { "admin", new BsonArray(user_ids) }
                                };
                                await dbcol.InsertOneAsync(document);
                        }

                        var newdata = await dbcol.Find(Builders<BsonDocument>.Filter.Eq("chat_id", this._message.Chat.Id)).FirstAsync();

                        if (newdata["timestamp"] < this._timeNow() - 1 * Config.ADMIN_CACHE_TIME) {
                                chatmember = await this._botClient.GetChatAdministratorsAsync(
                                        chatId: this._message.Chat.Id,
                                        cancellationToken: this._inputTelegram.cancellationToken
                                );
                                user_ids = new long[chatmember.Length];
                                i = 0;

                                foreach (Telegram.Bot.Types.ChatMember members in chatmember)
                                {
                                        user_ids[i] = members.User.Id;
                                        //Console.WriteLine(members.User.Id);
                                        i++;
                                }
                                var updatefilter = Builders<BsonDocument>.Filter.Eq("chat_id", this._message.Chat.Id);
                                var whatupdate = Builders<BsonDocument>.Update.Set("admin", new BsonArray(user_ids));

                                await dbcol.UpdateOneAsync(updatefilter, whatupdate);

                                this._user_ids = user_ids;
                        }
                        this._user_ids = user_ids;
                }

                public async Task<bool> IsAdmin(long id)
                {
                        await this._makeCache();
                        long[] rawids = this._user_ids;
                        foreach(long ids in rawids)
                        {
                                if(ids == id)
                                {
                                        return true;
                                }
                        }
                        return false;

                }

        }
}