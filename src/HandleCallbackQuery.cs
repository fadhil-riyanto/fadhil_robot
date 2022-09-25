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
                                                callback,"CacheExpire"
                                        ),
                                        showAlert: true
                                );
                        } else {
                                InputTelegramCb InputTelegramCb = new InputTelegramCb();
                                InputTelegramCb.chat_id = rdata.c;
                                InputTelegramCb.messange_id = rdata.m;
                                InputTelegramCb.data = rdata.d;
                                InputTelegramCb.user_id = rdata.u;
                                InputTelegramCb.callback = callback;

                                Utils.IExecutor executor = rdata.caller switch
                                {
                                        "help" => new Commands.Private.Callback.HelpCb(InputTelegramCb, botClient, callback),
                                        _ => null
                                };

                                await executor.Execute();
                        }
                        
                }
        }
}