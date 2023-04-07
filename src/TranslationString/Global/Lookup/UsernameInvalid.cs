// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Lookup 
{
    class UsernameInvalid : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "Invalid username!, please use valid telegram username format!";
            }
        }

        public override string translate_id_ID {
            get {
                return "Username tidak valid!, mohon gunakan format username telegram yang valid!";
            }
        }
    }
}