// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Groups.Kick
{
    class ArgumentNullException : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "you must reply to a user, or give me a username";
            }
        }

        public override string translate_id_ID {
            get {
                return "kamu harus membalas pesan penggunanya, atau berikan saya usernamenya";
            }
        }
    }
}