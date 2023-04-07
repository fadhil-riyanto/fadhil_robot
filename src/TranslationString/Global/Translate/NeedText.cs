// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class NeedText : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "please insert the text, use format /tr (language code) (your text)";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon masukkan teksnya, gunakan format /tr (kode bahasa) (teksnya)";
            }
        }
    }
}