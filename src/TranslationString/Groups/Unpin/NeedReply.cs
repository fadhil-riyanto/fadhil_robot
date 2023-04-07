// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/
namespace fadhil_robot.TranslationString.Groups.Unpin
{
    class NeedReply : fadhil_robot.Utils.TranslationStringParent{
        public override string translate_en_US {
            get {
                return "You must reply to the message you want to unpin!";
            }
        }

        public override string translate_id_ID {
            get {
                return "Anda harus membalas pesan yang ingin diunpin!";
            }
        }
    }
}