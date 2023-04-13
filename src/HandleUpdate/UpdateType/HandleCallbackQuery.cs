// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/


using Telegram.Bot.Types;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.HandleUpdate.UpdateType
{
    class UpdateType_CallbackQuery
    {
        public async Task HandleCallbackQuery(ITelegramBotClient botClient,
        CallbackQuery callback, CancellationToken cancellationToken, main_thread_ctx ctx)
        {
            new ConsoleLogCallback(callback.From.Id + " | " + callback.Data);
            Parse parser = new Parse(callback.Data);
            InputTelegramCallback inp = new InputTelegramCallback();

            inp.command = parser.getResult()["command"];
            inp.value = parser.getResult()["value"];
            inp.cancellationToken = cancellationToken;
            inp.main_thread_ctx = ctx;

            await this._executor(inp, botClient, callback);
        }
        private async Task _executor(InputTelegramCallback inputTelegram, ITelegramBotClient botClient,
        CallbackQuery callback)
        {
            UnPackType callbackdata = CallbackHelper.unpack(inputTelegram, callback.Data);

            if (callbackdata == null)
            {
                await botClient.AnswerCallbackQueryAsync(
                    callbackQueryId: callback.Id,
                    text: TranslateLocale.CreateTranslation(
                        callback, new fadhil_robot.TranslationString.System.CacheExpire()
                    ),
                    showAlert: true
                );
            }
            else
            {
                inputTelegram.user_id = callbackdata.user_id;
                inputTelegram.chat_id = callbackdata.chat_id;
                inputTelegram.messange_id = callbackdata.messange_id;
                inputTelegram.data = callbackdata.d;
                inputTelegram.callback = callback;
                inputTelegram.languange = callback.From.LanguageCode;
                inputTelegram.IncomingState = InputTelegramState.Callback;

                Utils.IExecutor_cb executor = callbackdata.caller switch
                {
                    "help" => new Commands.Private.Callback.HelpCb(inputTelegram, botClient, callback),
                    "help_back" => new Commands.Private.Callback.HelpBackCb(inputTelegram, botClient, callback),
                    "help_from_start" => new Commands.Private.Callback.HelpFromStart(inputTelegram, botClient, callback),
                    "translate_lists_languages" => new Commands.Global.Callback.TranslateListsLanguages(inputTelegram, botClient, callback),
                    _ => null
                };

                await executor.Execute();
            }

        }
    }
}