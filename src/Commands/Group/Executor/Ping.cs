// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) 2022 Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Commands.Group.Executor {
        class Ping : Utils.IExecutor
        {
                private InputTelegram _inputTelegram;
                private ITelegramBotClient _botClient;
                private Message _message;
                public Ping(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this._inputTelegram = inputTelegram;
                        this._botClient = botClient;
                        this._message = message;
                }
                public async Task Execute()
                {
                        string text = TranslateLocale.exec(
                                this._message, 
                                "command.Group.Ping", 
                                this._inputTelegram.command
                        );
                        await this._botClient.SendTextMessageAsync(
                                chatId: this._message.Chat.Id, 
                                text: text, 
                                replyToMessageId: this._message.MessageId, 
                                parseMode: ParseMode.Html
                        );
                }
        }
}