// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.System 
{
    class UnknownSwitchLogic : fadhil_robot.Utils.TranslationStringParent {

        public override string translate_en_US {
            get {
                return "unknown switch logic, please forward it to my programmer (@fadhil_riyanto)\n\nTrace: {0}";
            }
        }


        public override string translate_id_ID {
            get {
                return "logika switch tidak dikenali, mohon teruskan pesan ini ke programmer saya (@fadhil_riyanto)\n\nTrace: {0}";
            }
        }
    }
}