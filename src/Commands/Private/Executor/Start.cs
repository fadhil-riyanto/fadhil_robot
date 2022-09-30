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
                private InputTelegram _inputTelegram;
                private ITelegramBotClient _botClient;
                private Message _message;
                public Start(InputTelegram inputTelegram, ITelegramBotClient
                        botClient, Message message)
                {
                        this._inputTelegram = inputTelegram;
                        this._botClient = botClient;
                        this._message = message;
                }
                public async Task Execute()
                {
                        string text = TranslateLocale.exec(
                                        this._message,
                                        "command.Private.Start",
                                        this._inputTelegram.command
                        );

                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                                {
                                        InlineKeyboardButton.WithUrl(
                                                text: TranslateLocale.exec(
                                                        this._message,"command.Private.Start.OwnerTextKeyboard", this._inputTelegram.command
                                                ),
                                                url: "https://t.me/fadhil_riyanto"
                                        )
                                }
                        );
                        await this._botClient.SendTextMessageAsync(
                                chatId: this._message.Chat.Id,
                                text: text,
                                replyToMessageId: this._message.MessageId,
                                parseMode: ParseMode.Html,
                                replyMarkup: inlineKeyboard
                        );
                }
        }
}