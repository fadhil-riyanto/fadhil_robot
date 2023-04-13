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

        public bool is_real_command()
        {
            return true;
        }

        public async Task Execute()
        {
            if (this._inputTelegram.value != null)
            {
                // call handle deeplinks
                new DeepLinks(this._inputTelegram, this._botClient, this._message);
            } else {
                string text = TranslateLocale.CreateTranslation(
                    this._message,
                    new fadhil_robot.TranslationString.Private.Start.Success(),
                    this._inputTelegram.command
                );

                InlineKeyboardMarkup inlineKeyboard = new(new[]
                    {
                        InlineKeyboardButton.WithUrl(
                            text: TranslateLocale.CreateTranslation(
                            this._message,new fadhil_robot.TranslationString.Private.Start.OwnerTextKeyboard(), this._inputTelegram.command
                            ),
                            url: "https://t.me/fadhil_riyanto"
                        ),
                        InlineKeyboardButton.WithCallbackData(
                            text: TranslateLocale.CreateTranslation(
                                this._message,new fadhil_robot.TranslationString.Private.Start.HelpButton(), this._inputTelegram.command
                            ),
                            callbackData: CallbackHelper.pack(
                                this._inputTelegram, "help_from_start", null
                            )
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
}