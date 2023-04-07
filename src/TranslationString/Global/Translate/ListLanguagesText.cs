// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class ListLanguagesText : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "list of supported languages\n\n{0}";
            }
        }

        public override string translate_id_ID {
            get {
                return "daftar bahasa yang didukung\n\n{0}";
            }
        }
    }
}