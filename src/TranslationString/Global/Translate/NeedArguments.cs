// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class NeedArguments : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "please write messages from other languages for translating into the intended language";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon tulis pesan dari bahasa lain untuk menterjemahkannya kedalam bahasa yang dituju";
            }
        }
    }
}