// SPDX-License-Identifier: GPL-2.0

/*
 *  main.c
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/ctg.git
 */

using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Commands.Private.Executor
{
        class Help
        {
                private InputTelegram inputTelegram;
                private ITelegramBotClient botClient;
                private Message message;
                public Help(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this.inputTelegram = inputTelegram;
                        this.botClient = botClient;
                        this.message = message;
                }

                public async Task Execute()
                {
                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                                {
                                        new []
                                        {
                                                InlineKeyboardButton.WithCallbackData(text: "Admins", callbackData: "11"),
                                        }
                                }
                        );
                        string text = TranslateLocale.exec(
                                        message, 
                                        "command.Private.Help", 
                                        this.inputTelegram.command
                        );
                        await this.botClient.SendTextMessageAsync(
                                chatId: this.message.Chat.Id, 
                                text: text, 
                                replyMarkup: inlineKeyboard,
                                replyToMessageId: this.message.MessageId, 
                                parseMode: ParseMode.Html
                        );
                        // await this.botClient.SendTextMessageAsync(
                        //         chatId: this.message.Chat.Id, 
                        //         text: "help menu", 
                        //         replyMarkup: inlineKeyboard,
                        //         replyToMessageId: this.message.MessageId, 
                        //         parseMode: ParseMode.Html
                        // );

                }
        }
}