using Telegram.Bot.Types;
using Newtonsoft.Json;
using System.IO.Compression;
using System;

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
                
                public static string pack(Message message, Dictionary<string, string> val)
                {
                        var rareclass = new packtype
                        {
                                m = message.MessageId,
                                c = message.Chat.Id,
                                d = val
                        };

                        string cokkedstring = System.Text.Json.JsonSerializer.Serialize(rareclass);
                        return cokkedstring;
                }

                public static unpacktype unpack(string rarestring)
                {
                        unpacktype up = JsonConvert.DeserializeObject<unpacktype>(rarestring);
                        return up;
                }

        }
}