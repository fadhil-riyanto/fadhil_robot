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
    class KickmeConfirmation : Utils.IExecutor_cb
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private CallbackQuery _callback;
        public KickmeConfirmation(InputTelegram inputTelegram, ITelegramBotClient botClient, CallbackQuery callback)
        {
            this._inputTelegram = inputTelegram;
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
            if (Convert.ToInt64(this._inputTelegram.data["user_id"]) == this._callback.From.Id)
            {
                
                await this._botClient.SendTextMessageAsync(
                    chatId: this._inputTelegram.chat_id,
                    text: "you will got banned"
                );
            } else {
                await this.unauthorized_users();
            }
        }
    }
}