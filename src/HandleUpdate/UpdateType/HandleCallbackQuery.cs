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
                        new ConsoleLogCb(callback.From.Id + " | " + callback.Data);
                        Parse parser = new Parse(callback.Data);
                        InputTelegram inp = new InputTelegram();

                        inp.command = parser.getResult()["command"];
                        inp.value = parser.getResult()["value"];
                        inp.cancellationToken = cancellationToken;
                        inp.main_thread_ctx = ctx;

                        await this._executor(inp, botClient, callback);
                }
                private async Task _executor(InputTelegram inputTelegram, ITelegramBotClient botClient, 
                        CallbackQuery callback)
                {
                        unpacktype rdata = CallbackHelper.unpack(inputTelegram, callback.Data);
                        if (rdata == null) {
                                await botClient.AnswerCallbackQueryAsync(
                                        callbackQueryId: callback.Id,
                                        text: TranslateLocale.execCb(
                                                callback, "CacheExpire"
                                        ),
                                        showAlert: true
                                );
                        } else {
                                inputTelegram.chat_id = rdata.c;
                                inputTelegram.messange_id = rdata.m;
                                inputTelegram.data = rdata.d;
                                inputTelegram.user_id = rdata.u;
                                inputTelegram.callback = callback;
                                inputTelegram.languange = callback.From.LanguageCode;

                                Utils.IExecutor executor = rdata.caller switch {
                                        "help" => new Commands.Private.Callback.HelpCb(inputTelegram, botClient, callback),
                                        "help_back" => new Commands.Private.Callback.HelpBackCb(inputTelegram, 
                                                botClient, callback),
                                        _ => null
                                };

                                await executor.Execute();
                        }

                }
        }
}