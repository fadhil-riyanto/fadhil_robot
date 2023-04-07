// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/


using Telegram.Bot.Types;

namespace fadhil_robot.Utils
{

    class TranslateLocale
    {
        

        public static string CreateTranslation(Message msg, TranslationStringParent indicator, params string[] values)
        {

            if (msg.From.LanguageCode == "en")
            {
                return string.Format(indicator.translate_en_US, values);
            }
            else if (msg.From.LanguageCode == "id")
            {
                return string.Format(indicator.translate_id_ID, values);
            }
            else
            {
                // other langs, default is english us
                return string.Format(indicator.translate_en_US, values);
            }
        }
        public static string CreateTranslation(CallbackQuery msg, TranslationStringParent indicator, params string[] values)
        {

            if (msg.From.LanguageCode == "en")
            {
                return string.Format(indicator.translate_en_US, values);
            }
            else if (msg.From.LanguageCode == "id")
            {
                return string.Format(indicator.translate_id_ID, values);
            }
            else
            {
                // other langs, default is english us
                return string.Format(indicator.translate_en_US, values);
            }
        }
        
        
    }
}

