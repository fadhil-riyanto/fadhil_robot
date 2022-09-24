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
                        InputTelegramCb input_telegram = new InputTelegramCb();
                        input_telegram.chat_id = rdata.c;
                        input_telegram.messange_id = rdata.m;
                        input_telegram.data = rdata.d;
                        input_telegram.callback = callback;

                        string raw = "done fadhil";
                        await botClient.SendTextMessageAsync(
                                chatId: input_telegram.chat_id, 
                                text: raw
                        );
                }
        }
}