// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;
using Telegram.Bot.Types.ReplyMarkups;

namespace fadhil_robot.Commands.Private.Executor
{
        class Start : Utils.IExecutor
        {
                private InputTelegram inputTelegram;
                private ITelegramBotClient botClient;
                private Message message;
                public Start(InputTelegram inputTelegram, ITelegramBotClient
                        botClient, Message message)
                {
                        this.inputTelegram = inputTelegram;
                        this.botClient = botClient;
                        this.message = message;
                }
                public async Task Execute()
                {
                        string text = TranslateLocale.exec(
                                        message,
                                        "command.Private.Start",
                                        this.inputTelegram.command
                        );

                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                                {
                                        InlineKeyboardButton.WithUrl(
                                                text: TranslateLocale.exec(
                                                        message,"command.Private.Start.OwnerTextKeyboard", this.inputTelegram.command
                                                ),
                                                url: "https://t.me/fadhil_riyanto"
                                        )
                                }
                        );
                        await this.botClient.SendTextMessageAsync(
                                chatId: this.message.Chat.Id,
                                text: text,
                                replyToMessageId: this.message.MessageId,
                                parseMode: ParseMode.Html,
                                replyMarkup: inlineKeyboard
                        );
                }
        }
}