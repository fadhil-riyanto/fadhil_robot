using Telegram.Bot.Types;
using System.Text.Json;

namespace fadhil_robot.Utils
{
        class helpertype {
                public int messange_id {get; set;}
                public long chat_id {get; set;}
                public Dictionary<string, object> data {get; set;}

        }
        class CallbackHelper
        {

                public static string pack(Message message, Dictionary<string, object> val)
                {
                        var rareclass = new helpertype
                        {
                                messange_id = message.MessageId,
                                chat_id = message.Chat.Id,
                                data = val
                        };

                        string cokkedstring = JsonSerializer.Serialize(rareclass);

                        Console.WriteLine(cokkedstring);
                }
                
        }
}