// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.System 
{
    class UnknownCommand : fadhil_robot.Utils.TranslationStringParent {

        public override string translate_en_US {
            get {
                return "Sorry, the {0} command is not recognized";
            }
        }

        public override string translate_id_ID {
            get {
                return "Maaf, command {0} tidak dikenali";
            }
        }
    }
}