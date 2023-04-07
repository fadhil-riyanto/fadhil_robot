// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.System 
{
    class CacheExpire : fadhil_robot.Utils.TranslationStringParent {
        public override string translate_en_US {
            get {
                return "Sorry, the cache has been expired in our server, please send that command again.. ";
            }
        }

        public override string translate_id_ID {
            get {
                return "Maaf, Data cache telah kadaluarsa diserver kami, silahkan kirim command tersebut lagi.. ";
            }
        }
    }
}