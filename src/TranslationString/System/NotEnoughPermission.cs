// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.System 
{
    class NotEnoughPermission : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "I don't have enough rights to manage group settings in the chat, make sure i have enough permission for control this group";
            }
        }

        public override string translate_id_ID {
            get {
                return "saya tidak mempunyai cukup hak untuk mengelola pengaturan grup didalam chat, cek lagi bahwa saya punya kendali penuh atas grup ini";
            }
        }
    }
}