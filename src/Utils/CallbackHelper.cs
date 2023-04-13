// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/


using Telegram.Bot.Types;
using Newtonsoft.Json;
using System.IO.Compression;
using System;
using fadhil_robot.Utils;

namespace fadhil_robot.Utils
{
    class PackType
    {
        public int messange_id { get; set; }
        public long chat_id { get; set; }
        public long user_id { get; set; }
        public string caller { get; set; }
        public Dictionary<string, string> d { get; set; }

    }

    class UnPackType
    {
        public int messange_id { get; set; }
        public long chat_id { get; set; }
        public long user_id { get; set; }
        public string caller { get; set; }
        public Dictionary<string, string> d { get; set; }

    }
    class CallbackHelper
    {

        public static string pack(InputTelegramParent inputTelegram, string caller, Dictionary<string, string> val)
        {
            var rareclass = new PackType
            {
                messange_id = (inputTelegram.IncomingState == InputTelegramState.Messange) ? inputTelegram.message.MessageId : inputTelegram.messange_id,
                chat_id = (inputTelegram.IncomingState == InputTelegramState.Messange) ? inputTelegram.message.Chat.Id : inputTelegram.chat_id,
                user_id =  (inputTelegram.IncomingState == InputTelegramState.Messange) ? inputTelegram.message.From.Id : inputTelegram.user_id,
                caller = caller,
                d = val
            };

            string cokkedstring = System.Text.Json.JsonSerializer.Serialize(rareclass);

            generate_rand_str rn = new generate_rand_str();
            string key = rn.gethash();
            inputTelegram.main_thread_ctx.redis.StringSet(
                $"callback_data_{key}",
                cokkedstring,
                TimeSpan.FromSeconds(Config.CALLBACK_CACHE_TIME)
            );
            return key;
        }

        public static UnPackType unpack(InputTelegramCallback inputTelegram, string key)
        {
            UnPackType up;

            try
            {
                up = JsonConvert.DeserializeObject<UnPackType>(
                        inputTelegram.main_thread_ctx.redis.StringGet($"callback_data_{key}")
                );
            }
            catch (System.ArgumentNullException)
            {
                up = null;
            }
            return up;
        }

    }
}