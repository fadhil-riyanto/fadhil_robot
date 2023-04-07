// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Pkgsearch 
{
    class NeedRepository : fadhil_robot.Utils.TranslationStringParent {
        public override string translate_en_US {
            get {
                return "please add repository name, \nformat: /pkgsearch repository arch repositoryname\n\nie: ``` /pkgsearch core x86_64 linux```";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon tambahkan nama repositori nya, \nformat: /pkgsearch repositori repositori namapaket\n\ncontoh: ```/pkgsearch core x86_64 linux```";
            }
        }
    }
}