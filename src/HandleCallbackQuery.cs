// SPDX-License-Identifier: GPL-2.0

/*
 *  main.c
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
                        Console.WriteLine(inputTelegram.command);
                        Console.WriteLine(inputTelegram.value);
                }
        }
}