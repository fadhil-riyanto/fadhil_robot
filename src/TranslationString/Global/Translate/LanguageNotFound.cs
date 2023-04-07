// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class LanguageNotFound : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "The {0} languange are not found";
            }
        }

        public override string translate_id_ID {
            get {
                return "Bahasa {0} tidak ditemukan";
            }
        }
    }
}