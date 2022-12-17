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
using TL;

namespace fadhil_robot.Commands.Global.Executor
{
    class Whoami : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Whoami(InputTelegram inputTelegram,
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
            string text = TranslateLocale.exec(
                this._message, "command.Group.Whoami",
                this._message.From.FirstName + " " + this._message.From.LastName,
                this._message.From.Id.ToString(), this._message.From.LanguageCode,
                this._message.From.Username, UtilsFN.is64(this._message.From.Id)
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