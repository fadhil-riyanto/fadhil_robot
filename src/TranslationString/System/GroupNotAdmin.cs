// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.System 
{
    class GroupNotAdmin : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "Sorry, you're not admin on this group";
            }
        }

        public override string translate_id_ID {
            get {
                return "Maaf, kamu bukan admin di grup ini";
            }
        }
    }
}