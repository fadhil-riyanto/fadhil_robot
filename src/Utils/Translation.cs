// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;

namespace fadhil_robot.Utils
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
                                "GroupNotAdmin" => "Sorry, you're not admin on this group",

                                // private
                                "command.Private.Start"  => "Hi, I'm fadhil robot, nice to meet you. I can manage your telegram group with many useful features!",
                                "command.Private.Start.OwnerTextKeyboard"  => "My Programmer",
                                "command.Private.Ping"  => "Ping in private chat ok!",
                                "command.Private.Help"  => "Hi {0}, I'm Fadhil Robot, created by @fadhil_riyanto. this is a help menu if you don't know what I'm using, all commands work with / and !",

                                // group
                                "command.Group.Ping"  => "Ping in group ok!",
                                "command.Group.Pin.NeedReply"  => "You must reply to the message you want to pin!",
                                "command.Group.Pin.Success"  => "Message was successfully pinned",
                                "command.Group.Pin.NotEnoughPermission"  => "I don't have enough rights to manage pinned messages in the chat",
                                "command.Group.Unpin.NeedReply"  => "You must reply to the message you want to unpin!",
                                "command.Group.Unpin.Success"  => "Message was successfully unpinned",
                                "command.Group.Unpin.NotEnoughPermission"  => "I don't have enough rights to manage pinned messages in the chat",

                                _ => "err key \"" + data + "\""
                        };
                }
                public static string id_ID(string data)
                {
                        return data switch
                        {
                                // system
                                "UnknownCommand" => "Maaf, command {0} tidak dikenali",
                                "GroupNotAdmin" => "Maaf, kamu bukan admin di grup ini",

                                // Private
                                "command.Private.Start"  => "hai, saya fadhil_robot, senang bertemu denganmu. saya bisa mengatur grup telegram kamu dengan banyak fitur yang berguna!",
                                "command.Private.Start.OwnerTextKeyboard"  => "Programmer saya",
                                "command.Private.Ping"  => "Ping di chat pribadi oke!",
                                "command.Private.Help"  => "Hai {0}, aku Fadhil Robot.. ini adalah menu pertolongan jika anda tidak paham kegunaanku, semua command bekerja dengan / dan !",

                                // group
                                "command.Group.Ping"  => "Ping di grup oke!",
                                "command.Group.Pin.NeedReply"  => "Anda harus membalas pesan yang ingin dipin!",
                                "command.Group.Pin.Success"  => "Pesan telah berhasil dipin",
                                "command.Group.Pin.NotEnoughPermission"  => "saya tidak mempunyai cukup hak untuk mengelola pesan yang disematkan dalam grup",
                                "command.Group.Unpin.NeedReply"  => "Anda harus membalas pesan yang ingin diunpin!",
                                "command.Group.Unpin.Success"  => "Pesan telah berhasil diunpin",
                                "command.Group.Unpin.NotEnoughPermission"  => "saya tidak mempunyai cukup hak untuk mengelola pesan yang disematkan dalam grup",

                                _ => "err key \"" + data + "\""
                        };
                }
        }
}

