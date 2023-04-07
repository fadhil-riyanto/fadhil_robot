// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Private.Help 
{
    class Success : fadhil_robot.Utils.TranslationStringParent {
        public override string translate_en_US {
            get {
                return "Hi {0}, I'm Fadhil Robot, created by @fadhil_riyanto. this is a help menu if you don't know what I'm using, all commands work with / and !";
            }
        }

        public override string translate_id_ID {
            get {
                return "Hai {0}, aku Fadhil Robot.. ini adalah menu pertolongan jika anda tidak paham kegunaanku, semua command bekerja dengan / dan !";
            }
        }
    }
}