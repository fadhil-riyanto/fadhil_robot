// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Groups.Kick
{
    class FailBanAdmin : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "can't kicked because its an admin";
            }
        }

        public override string translate_id_ID {
            get {
                return "tidak bisa memkick, karna dia admin";
            }
        }
    }
}