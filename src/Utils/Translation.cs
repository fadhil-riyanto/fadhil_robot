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
                                // system
                                "UnknownCommand" => "Sorry, the {0} command is not recognized",

                                // private
                                "command.Private.Start"  => "Hi, I'm prtscbot, nice to meet you. I can manage your telegram group with many useful features!",
                                "command.Private.Start.OwnerTextKeyboard"  => "My Programmer",
                                "command.Private.Ping"  => "Ping in private chat ok!",

                                // group
                                "command.Group.Ping"  => "Ping in group ok!",
                                "command.Group.Pin.NeedReply"  => "You must reply to the message you want to pin!",
                                "command.Group.Pin.Success"  => "Message was successfully pinned",
                                "command.Group.Pin.NotEnoughPermission"  => "I don't have enough rights to manage pinned messages in the chat",

                                _ => "err key \"" + data + "\""
                        };
                }
                public static string id_ID(string data)
                {
                        return data switch
                        {
                                // system
                                "UnknownCommand" => "Maaf, command {0} tidak dikenali",

                                // Private
                                "command.Private.Start"  => "hai, saya prtscbot, senang bertemu denganmu. saya bisa mengatur grup telegram kamu dengan banyak fitur yang berguna!",
                                "command.Private.Start.OwnerTextKeyboard"  => "Programmer saya",
                                "command.Private.Ping"  => "Ping di chat pribadi oke!",

                                // group
                                "command.Group.Ping"  => "Ping di grup oke!",
                                "command.Group.Pin.NeedReply"  => "Anda harus membalas pesan yang ingin dipin!",
                                "command.Group.Pin.Success"  => "Pesan telah berhasil dipin",
                                "command.Group.Pin.NotEnoughPermission"  => "saya tidak mempunyai cukup hak untuk mengelola pesan yang disematkan dalam grup",

                                _ => "err key \"" + data + "\""
                        };
                }
        }
}

