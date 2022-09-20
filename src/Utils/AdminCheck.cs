// SPDX-License-Identifier: GPL-2.0

/*
 *  main.c
 *  Copyright (C) Fadhil Riyanto
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
                private Message message;
                private ITelegramBotClient botClient;
                private InputTelegram inputTelegram;
                private long[] user_ids;

                public AdminCheck(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this.message = message;
                        this.botClient = botClient;
                        this.inputTelegram = inputTelegram;
                }

                private long TimeNow()
                {
                        var now = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
                        return (long)now.TotalSeconds;
                }




                // public async Task isAdmin(long user_id)
                // {


                //         await this.botClient.SendTextMessageAsync(
                //                         chatId: this.message.Chat.Id,
                //                         text: data.ToString()
                //                 );
                // }

                public async Task makeCache()
                {
                        Telegram.Bot.Types.ChatMember[] chatmember = await this.botClient.GetChatAdministratorsAsync(
                                chatId: this.message.Chat.Id,
                                cancellationToken: this.inputTelegram.cancellationToken
                        );
                        long[] user_ids = new long[chatmember.Length];
                        int i = 0;

                        foreach (Telegram.Bot.Types.ChatMember members in chatmember)
                        {
                                user_ids[i] = members.User.Id;
                                //Console.WriteLine(members.User.Id);
                                i++;
                        }

                        var clientMongoDB = new MongoClient("mongodb://localhost:27017");
                        var dbctx = clientMongoDB.GetDatabase(Config.MongoDB_DBNAME);

                        var dbcol = dbctx.GetCollection<BsonDocument>("admin_cache");

                        try
                        {
                                var filter = Builders<BsonDocument>.Filter.Eq("chat_id", this.message.Chat.Id);
                                var filtered = await dbcol.Find(filter).FirstAsync();
                        }
                        catch (InvalidOperationException)
                        {
                                var document = new BsonDocument
                                {
                                        { "chat_id", this.message.Chat.Id },
                                        { "timestamp", this.TimeNow()},
                                        { "admin", new BsonArray(user_ids) }
                                };
                                await dbcol.InsertOneAsync(document);
                        }

                        var newdata = await dbcol.Find(Builders<BsonDocument>.Filter.Eq("chat_id", this.message.Chat.Id)).FirstAsync();
                        // await this.botClient.SendTextMessageAsync(
                        //         chatId: this.message.Chat.Id,
                        //         text: newdata.ToJson()
                        // );

                        // Console.WriteLine(newdata["timestamp"]);
                        // checking timestamp
                        if (newdata["timestamp"] < this.TimeNow() - 1 * Config.ADMIN_CACHE_TIME)
                        {
                                chatmember = await this.botClient.GetChatAdministratorsAsync(
                                        chatId: this.message.Chat.Id,
                                        cancellationToken: this.inputTelegram.cancellationToken
                                );
                                user_ids = new long[chatmember.Length];
                                i = 0;

                                foreach (Telegram.Bot.Types.ChatMember members in chatmember)
                                {
                                        user_ids[i] = members.User.Id;
                                        //Console.WriteLine(members.User.Id);
                                        i++;
                                }
                                var updatefilter = Builders<BsonDocument>.Filter.Eq("chat_id", message.Chat.Id);
                                var whatupdate = Builders<BsonDocument>.Update.Set("admin", new BsonArray(user_ids));

                                await dbcol.UpdateOneAsync(updatefilter, whatupdate);

                                this.user_ids = user_ids;
                        }
                        this.user_ids = user_ids;
                }

                public async Task<bool> isAdmin(long id)
                {
                        await this.makeCache();
                        long[] rawids = this.user_ids;
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