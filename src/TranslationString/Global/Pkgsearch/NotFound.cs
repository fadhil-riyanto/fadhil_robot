// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Pkgsearch
{
    class NotFound : fadhil_robot.Utils.TranslationStringParent
    {
        public override string translate_en_US {
            get {
                return "your search is not found in AUR databse";
            }
        }
        public override string translate_id_ID {
            get {
                return "pencarian kamu tidak ditemukan didatabase AUR";
            }
        }
    }
}