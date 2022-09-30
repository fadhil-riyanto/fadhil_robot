// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Commands.Private.Executor
{
        class Help : Utils.IExecutor
        {
                private InputTelegram _inputTelegram;
                private ITelegramBotClient _botClient;
                private Message _message;
                public Help(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this._inputTelegram = inputTelegram;
                        this._botClient = botClient;
                        this._message = message;
                }

                public async Task Execute()
                {       
                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                                {
                                        new []
                                        {
                                                InlineKeyboardButton.WithCallbackData(
                                                        
                                                        text: "Admins", callbackData: CallbackHelper.pack(
                                                                        this._inputTelegram, "help", new Dictionary<string, string> {
                                                                        { "clicked_button", "admin"}
                                                                }
                                                        )
                                                ),
                                        }
                                }
                        );
                        string text = TranslateLocale.exec(
                                        this._message, 
                                        "command.Private.Help", 
                                        this._inputTelegram.command
                        );
                        await this._botClient.SendTextMessageAsync(
                                chatId: this._message.Chat.Id, 
                                text: text, 
                                replyMarkup: inlineKeyboard,
                                replyToMessageId: this._message.MessageId, 
                                parseMode: ParseMode.Html
                        );
                }
        }
}