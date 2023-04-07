// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;
using Fluent.LibreTranslate;


namespace fadhil_robot.Commands.Global.Callback
{
    class TranslateListsLanguages : Utils.IExecutor_cb
    {
        private InputTelegramCallback _InputTelegramCallback;
        private ITelegramBotClient _botClient;
        private CallbackQuery _callback;
        public TranslateListsLanguages(InputTelegramCallback InputTelegramCallback, ITelegramBotClient botClient, CallbackQuery callback)
        {
            this._InputTelegramCallback = InputTelegramCallback;
            this._botClient = botClient;
            this._callback = callback;
        }

        private async Task unauthorized_users()
        {
            await this._botClient.AnswerCallbackQueryAsync(
                callbackQueryId: this._callback.Id,
                text: TranslateLocale.CreateTranslation(
                    this._callback, new fadhil_robot.TranslationString.System.UnauthorizedButtonCallbackPressed()
                ),
                showAlert: true
            );
        }

        public async Task Execute()
        {
            if (Convert.ToInt64(this._InputTelegramCallback.data["user_id"]) == this._callback.From.Id)
            {
                libretranslateExtern libre = new libretranslateExtern(LibreTranslateServer.Libretranslate_de);
                langLists[] data = await libre.GetListsLanguage();

                string raws = string.Empty;
                foreach (langLists data_literate in data)
                {
                    raws += $"{data_literate.code} = {data_literate.name}\n";
                }

                string text = TranslateLocale.CreateTranslation(
                    this._callback, 
                    new fadhil_robot.TranslationString.Global.Translate.ListLanguagesText(),
                    raws
                );

                await this._botClient.EditMessageTextAsync(
                    messageId: this._callback.Message.MessageId,
                    chatId: this._InputTelegramCallback.chat_id,
                    text: text,
                    parseMode: ParseMode.Html
                );
            }
            else
            {
                await this.unauthorized_users();
            }
            // ITgKeyboard keyboard = new TGKeyboardHelpMenu(this._inputTelegram);

            // await this._botClient.EditMessageTextAsync(
            //     messageId: this._callback.Message.MessageId,
            //     chatId: this._inputTelegram.chat_id,
            //     text: keyboard.getContent(CallbackHelper.unpack(
            //         this._inputTelegram, this._callback.Data).d["clicked_button"]
            //     ),
            //     replyMarkup: keyboard.detectLanguangeBackButton().get(),
            //     parseMode: ParseMode.Html
            // );
        }
    }

    class langLists
    {
        public string code { get; set; }
        public string name { get; set; }
        public string[] targets { get; set; }
    }

    class libretranslateExtern
    {
        private HttpClient _httpClient;
        public libretranslateExtern(LibreTranslateServer ServerSelect)
        {
            this._httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ServerSelect.ToString())
            };
        }

        public async Task<langLists[]> GetListsLanguage()
        {
            return JsonConvert.DeserializeObject<langLists[]>(await this._httpClient.GetStringAsync("/languages"));
        }
    }
}