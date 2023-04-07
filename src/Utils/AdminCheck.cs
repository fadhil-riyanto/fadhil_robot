// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/


using Telegram.Bot.Types;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Utils
{
    class admin_list_type
    {
        public long time { get; set; }
        public long[] admin { get; set; }

    }
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

        private async Task<long[]> get_adminlists()
        {
            Telegram.Bot.Types.ChatMember[] chatmember = await this._botClient.GetChatAdministratorsAsync(
                chatId: this._message.Chat.Id,
                cancellationToken: this._inputTelegram.cancellationToken
            );
            long[] user_ids = new long[chatmember.Length];
            int i = 0;

            foreach (Telegram.Bot.Types.ChatMember members in chatmember)
            {
                user_ids[i] = members.User.Id;
                i++;
            }
            return user_ids;
        }

        private async Task _makeCache()
        {
            long[] user_ids;
            string data = this._inputTelegram.main_thread_ctx.redis.StringGet($"admin_cache_{this._message.Chat.Id}");

            if (data == null)
            {
                user_ids = await this.get_adminlists();

                admin_list_type data_admin = new admin_list_type {
                    time = this._timeNow(),
                    admin = user_ids
                };

                var ress = Newtonsoft.Json.JsonConvert.SerializeObject(data_admin);
                this._inputTelegram.main_thread_ctx.redis.StringSet($"admin_cache_{this._message.Chat.Id}", ress);
            }
            else
            {
                admin_list_type unpacked_data = Newtonsoft.Json.JsonConvert.DeserializeObject<admin_list_type>(data);
                if (unpacked_data.time < this._timeNow() - 1 * Config.ADMIN_CACHE_TIME)
                {
                    user_ids = await this.get_adminlists();

                    admin_list_type data_admin = new admin_list_type {
                        time = this._timeNow(),
                        admin = user_ids
                    };

                    var ress = Newtonsoft.Json.JsonConvert.SerializeObject(data_admin);

                    new ConsoleLogSys($"admin_cache_{this._message.Chat.Id} refreshed");
                    this._inputTelegram.main_thread_ctx.redis.StringSet($"admin_cache_{this._message.Chat.Id}", ress);
                } else {
                    user_ids = unpacked_data.admin;
                }
            }
            this._user_ids = user_ids;
        }

        public async Task<bool> force_make_new_cache()
        {
            
            long[] user_ids = await this.get_adminlists();

            admin_list_type data_admin = new admin_list_type {
                time = this._timeNow(),
                admin = user_ids
            };

            var ress = Newtonsoft.Json.JsonConvert.SerializeObject(data_admin);
            return this._inputTelegram.main_thread_ctx.redis.StringSet($"admin_cache_{this._message.Chat.Id}", ress);
        }


        public async Task<bool> IsAdmin(long id)
        {
            await this._makeCache();
            long[] rawids = this._user_ids;
            foreach (long ids in rawids)
            {
                if (ids == id)
                {
                    return true;
                }
            }
            return false;

        }

    }
}