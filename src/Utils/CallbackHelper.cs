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
    class packtype
    {
        public int m { get; set; }
        public long c { get; set; }
        public long u { get; set; }
        public string caller { get; set; }
        public Dictionary<string, string> d { get; set; }

    }

    class unpacktype
    {
        public int m { get; set; }
        public long c { get; set; }
        public long u { get; set; }
        public string caller { get; set; }
        public Dictionary<string, string> d { get; set; }

    }
    class CallbackHelper
    {

        public static string pack(InputTelegram inputTelegram, string caller, Dictionary<string, string> val)
        {
            var rareclass = new packtype
            {
                m = inputTelegram.messange_id,
                c = inputTelegram.chat_id,
                u = inputTelegram.user_id,
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

        public static unpacktype unpack(InputTelegram inputTelegram, string key)
        {
            unpacktype up;

            try
            {
                up = JsonConvert.DeserializeObject<unpacktype>(
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