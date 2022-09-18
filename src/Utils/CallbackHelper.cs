using Telegram.Bot.Types;
using Newtonsoft.Json;

namespace fadhil_robot.Utils
{
        class packtype {
                public int messange_id {get; set;}
                public long chat_id {get; set;}
                public Dictionary<string, object> data {get; set;}

        }

        class unpacktype {
                public int messange_id {get; set;}
                public long chat_id {get; set;}
                public Dictionary<string, object> data {get; set;}

        }
        class CallbackHelper
        {

                public static string pack(Message message, Dictionary<string, object> val)
                {
                        var rareclass = new packtype
                        {
                                messange_id = message.MessageId,
                                chat_id = message.Chat.Id,
                                data = val
                        };

                        string cokkedstring = System.Text.Json.JsonSerializer.Serialize(rareclass);
                        return cokkedstring;
                }

                public static unpacktype unpack(string rarestring)
                {
                        unpacktype up = JsonConvert.DeserializeObject<unpacktype>(rarestring);

                        Console.WriteLine(up.data["note"]);
                        return up;
                }

        }
}