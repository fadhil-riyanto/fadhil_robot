// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Pkgsearch 
{
    class NeedPkgName : fadhil_robot.Utils.TranslationStringParent {
        public override string translate_en_US {
            get {
                return "please add package name, \nformat: /pkgsearch repository arch packagename\n\nie: ``` /pkgsearch core x86_64 linux```";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon tambahkan nama package nya, \nformat: /pkgsearch repositori arsitektur namapaket\n\ncontoh: ```/pkgsearch core x86_64 linux```";
            }
        }
    }
}