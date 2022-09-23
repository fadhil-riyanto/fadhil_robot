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
                public async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callback, CancellationToken cancellationToken)
                {
                        new ConsoleLog("[callback] " + callback.From.Id + " | " + callback.Data);
                        Parse parser = new Parse(callback.Data);
                        InputTelegram inp = new InputTelegram();

                        inp.command = parser.getResult()["command"];
                        inp.value = parser.getResult()["value"];
                        inp.cancellationToken = cancellationToken;

                        await this.executor(inp, botClient, callback);
                }
                private async Task executor(InputTelegram inputTelegram, ITelegramBotClient botClient, CallbackQuery callback)
                {
                        unpacktype rdata = CallbackHelper.unpack(callback.Data);
                        InputTelegramCb input_telegram = new InputTelegramCb();
                        input_telegram.c = rdata.c;
                        input_telegram.m = rdata.m;
                        input_telegram.d = rdata.d;

                        string raw = "chat id: " + input_telegram.c + "\n messange id: " + input_telegram.m + "\n data: " +
                                rdata.d.ToString();
                        await botClient.SendTextMessageAsync(
                                chatId: rdata.c, 
                                text: raw
                        );


                        // check 
                        Console.WriteLine(input_telegram.d["type"]);
                }
        }
}