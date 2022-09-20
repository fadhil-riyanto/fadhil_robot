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
using System.Threading;


namespace fadhil_robot.Commands.Private.Executor {
        class Ping
        {
                private InputTelegram inputTelegram;
                private ITelegramBotClient botClient;
                private Message message;
                public Ping(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this.inputTelegram = inputTelegram;
                        this.botClient = botClient;
                        this.message = message;
                }
                public async Task Execute()
                {
                        
                        string text = TranslateLocale.exec(
                                        message, 
                                        "command.Private.Ping", 
                                        this.inputTelegram.command
                        );
                        await this.botClient.SendTextMessageAsync(
                                chatId: this.message.Chat.Id, 
                                text: text, 
                                replyToMessageId: this.message.MessageId, 
                                parseMode: ParseMode.Html
                        );
                }
        }
}