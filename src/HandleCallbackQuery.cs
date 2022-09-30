// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Program
{
        class HandleCallback
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

                        await this.executor(inp, botClient, callback);
                }
                private async Task executor(InputTelegram inputTelegram, ITelegramBotClient botClient, CallbackQuery callback)
                {
                        unpacktype rdata = CallbackHelper.unpack(inputTelegram, callback.Data);
                        if (rdata == null)
                        {
                                await botClient.AnswerCallbackQueryAsync(
                                        callbackQueryId: callback.Id,
                                        text: TranslateLocale.execCb(
                                                callback, "CacheExpire"
                                        ),
                                        showAlert: true
                                );
                        }
                        else
                        {
                                InputTelegram InputTelegram = new InputTelegram
                                {
                                        chat_id = rdata.c,
                                        messange_id = rdata.m,
                                        data = rdata.d,
                                        user_id = rdata.u,
                                        callback = callback
                                };

                                Utils.IExecutor executor = rdata.caller switch
                                {
                                        "help" => new Commands.Private.Callback.HelpCb(InputTelegram, botClient, callback),
                                        _ => null
                                };

                                await executor.Execute();
                        }

                }
        }
}