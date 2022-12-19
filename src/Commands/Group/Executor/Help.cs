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
using TL;

namespace fadhil_robot.Commands.Group.Executor
{
    class Help : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Help(InputTelegram inputTelegram,
        ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
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
            string text_response = TranslateLocale.exec(
                this._message,
                "command.Group.Help.MainText"
            );
            string text_button = TranslateLocale.exec(
                this._message,
                "command.Group.Help.Button"
            );
            await this._botClient.SendTextMessageAsync(
                replyToMessageId: this._inputTelegram.messange_id,
                chatId: this._inputTelegram.chat_id,
                text: text_response,
                replyMarkup: new InlineKeyboardMarkup(
                    new InlineKeyboardButton[][] {
                        new InlineKeyboardButton[] {
                            InlineKeyboardButton.WithUrl(text_button, UtilsFunction.deep_linking_gen("help_menu"))
                        }
                    }
                )
            );
        }
    }
}