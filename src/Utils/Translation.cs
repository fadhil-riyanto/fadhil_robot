using Telegram.Bot.Types;

namespace Prtscbot.Utils
{

        class TranslateLocale
        {
                public static string exec(Message msg, string indicator, params string[] values)
                {

                        if (msg.From.LanguageCode == "en") {
                                string rarestr = en_US(indicator);
                                return string.Format(rarestr, values);
                        } else if (msg.From.LanguageCode == "id"){
                                string rarestr = id_ID(indicator);
                                return string.Format(rarestr, values);
                        } else {
                                // other langs, default is english us
                                string rarestr = en_US(indicator);
                                return string.Format(rarestr, values);
                        }
                        
                }
                public static string en_US(string data)
                {
                        return data switch
                        {
                                "UnknownCommand" => "Sorry, the {0} command is not recognized",
                                "command.Start"  => "Hi, I'm prtscbot. nice to meet you",
                                _ => "err key " + data
                        };
                }
                public static string id_ID(string data)
                {
                        return data switch
                        {
                                "UnknownCommand" => "Maaf, command {0} tidak dikenali",
                                "command.Start"  => "hai, saya prtscbot. senang bertemu denganmu",
                                _ => "err key " + data
                        };
                }
        }
}

