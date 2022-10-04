// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) 2022 Fadhil Riyanto
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
                        string text = TranslateLocale.exec(
                                        this._message, 
                                        "command.Private.Help", 
                                        this._inputTelegram.command
                        );

                        ITgKeyboard keyboard = new TGKeyboardHelpMenu(this._inputTelegram);
                        await this._botClient.SendTextMessageAsync(
                                chatId: this._message.Chat.Id, 
                                text: text, 
                                replyMarkup: keyboard.detectLanguangeMainButton().get(),
                                replyToMessageId: this._message.MessageId, 
                                parseMode: ParseMode.Html
                        );
                }
        }
}
