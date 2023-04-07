// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Groups.Ban
{
    class Success : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "[{0}] banned!";
            }
        }

        public override string translate_id_ID {
            get {
                return "[{0}] dibanned!";
            }
        }
    }
}