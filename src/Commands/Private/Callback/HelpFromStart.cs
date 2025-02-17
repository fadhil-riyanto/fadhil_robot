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

namespace fadhil_robot.Commands.Private.Callback
{
    class HelpFromStart : Utils.IExecutor_cb
    {
        private InputTelegramParent _inputTelegram;
        private ITelegramBotClient _botClient;
        private CallbackQuery _callback;
        public HelpFromStart(InputTelegramParent inputTelegram, ITelegramBotClient botClient, CallbackQuery callback)
        {
            this._inputTelegram = inputTelegram;
            this._botClient = botClient;
            this._callback = callback;
        }

        public async Task Execute()
        {
            string text = TranslateLocale.CreateTranslation(
                this._callback,
                new fadhil_robot.TranslationString.Private.Help.Success(),
                this._inputTelegram.command
            );

            ITgKeyboard keyboard = new TGKeyboardHelpMenu(this._inputTelegram);
            await this._botClient.EditMessageTextAsync(
                messageId: this._callback.Message.MessageId,
                chatId: this._inputTelegram.chat_id,
                text: text,
                replyMarkup: keyboard.detectLanguangeMainButton().get(),
                parseMode: ParseMode.Html
            );
        }
    }
}
