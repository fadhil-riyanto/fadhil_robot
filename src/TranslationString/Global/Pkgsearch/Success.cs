// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Pkgsearch 
{
    class Success : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "{0} ({1})\n\nDesc: {2}\nLast update: {3})";
            }
        }

        public override string translate_id_ID {
            get {
                return "{0} ({1})\n\nDeskripsi: {2}\nTerakhir update: {3}) ";
            }
        }
    }
}