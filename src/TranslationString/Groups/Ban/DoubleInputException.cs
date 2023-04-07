// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Groups.Ban
{
    class DoubleInputException : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "please choose one of the following reply only or type the username";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon pilih salah satu antara membalas pesannya atau tulis username nya";
            }
        }
    }
}