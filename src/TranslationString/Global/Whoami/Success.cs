// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

namespace fadhil_robot.TranslationString.Global.Whoami
{
    class Success : fadhil_robot.Utils.TranslationStringParent {
        public override string translate_en_US {
            get {
                return "Your info\n\n" +
                    "👦🏻 name: {0}\n" +
                    "🆔 id: {1}\n" +
                    "⁣🌐 languange: {2}\n" +
                    "👤 username: {3}\n" +
                    "🆔 id type: {4}\n";
            }
        }

        public override string translate_id_ID {
            get {
                return "Info Kamu\n\n" +
                    "👦🏻 nama: {0}\n" +
                    "🆔 id: {1}\n" +
                    "⁣🌐 bahasa: {2}\n" +
                    "👤 nama pengguna: {3}\n" +
                    "🆔 tipe id: {4}\n";
            }
        }
    }
}