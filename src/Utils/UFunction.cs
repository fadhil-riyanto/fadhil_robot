// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using fadhil_robot;
using TL;
using Telegram.Bot.Types;
using fadhil_robot.Utils.Exception;

namespace fadhil_robot.Utils
{
    class UtilsFunction
    {
        public static string is64(long data)
        {
            if (data < Int32.MaxValue)
            {
                return "32";
            }
            else if (data < Int64.MaxValue)
            {
                return "64";
            }
            else
            {
                return "unknown";
            }
        }

        public static string deep_linking_gen(string data)
        {
            return $"https://t.me/{Config.BotName}?start={data}";
        }

        // get the userid from reply or mentions
        public static async Task<long> user_id_getter(InputTelegram inputTelegram, Telegram.Bot.Types.Message message)
        {
            long user_id;
            if (message.ReplyToMessage != null && inputTelegram.value != null)
            {
                user_id = 0;
                throw new DoubleInputException();
                
            }
            else if (message.ReplyToMessage != null)
            {
                user_id = message.ReplyToMessage.From.Id;
            }
            else if (inputTelegram.value != null)
            {
                Contacts_ResolvedPeer peerdata = await inputTelegram.
                        main_thread_ctx.MtprotoClient.Contacts_ResolveUsername(UtilsFunction.normalizing(inputTelegram.value));
                user_id = peerdata.User.id;
            }
            else {
                user_id = 0;
            }
            return user_id;
        }
        public static string normalizing(string username)
        {
            if (username[0] == '@')
            {
                return username.Remove(0, 1);
            }
            else
            {
                return username;
            }
        }
    }


}


