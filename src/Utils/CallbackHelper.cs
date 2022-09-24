// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
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
                public Dictionary<string, string> d { get; set; }

        }

        class unpacktype
        {
                public int m { get; set; }
                public long c { get; set; }
                public Dictionary<string, string> d { get; set; }

        }
        class CallbackHelper
        {
                
                public static string pack(Message message, InputTelegram inputTelegram, Dictionary<string, string> val)
                {
                        var rareclass = new packtype
                        {
                                m = message.MessageId,
                                c = message.Chat.Id,
                                d = val
                        };

                        string cokkedstring = System.Text.Json.JsonSerializer.Serialize(rareclass);

                        generate_rand_str rn = new generate_rand_str();
                        string key = rn.gethash();

                        inputTelegram.main_thread_ctx.redis.StringSet(key, cokkedstring, TimeSpan.FromSeconds( Config.REDIS_CALLBACK_CACHE_TIME ));
                        return key;
                }

                public static unpacktype unpack(InputTelegram inputTelegram, string key)
                {
                        // Console.WriteLine(inputTelegram.main_thread_ctx.redis.StringGet(key));
                        unpacktype up = JsonConvert.DeserializeObject<unpacktype>(
                                inputTelegram.main_thread_ctx.redis.StringGet(key)
                        );
                        return up;
                }

        }
}