// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Commands.Group.Executor
{
    class Admincache : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Admincache(InputTelegram inputTelegram,
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
            AdminCheck admincheck = new AdminCheck(this._inputTelegram,
            this._botClient, this._message);

            if (admincheck.IsAdmin(this._message.From.Id).Result)
            {
                // check if they is not reply their messange
                await this.its_admin();
            }
            else
            {
                string text = TranslateLocale.exec(
                    this._message, "GroupNotAdmin"
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId,
                    parseMode: ParseMode.Html
                );
            }
        }

        private async Task its_admin()
        {
            AdminCheck admincheck = new AdminCheck(this._inputTelegram,
            this._botClient, this._message);
            if (await admincheck.force_make_new_cache())
            {
                string text = TranslateLocale.exec(
                    this._message, "command.Group.Admincache.Succeed"
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId,
                    parseMode: ParseMode.Html
                );
            } else {
                string text = TranslateLocale.exec(
                    this._message, "command.Group.Admincache.Fail"
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
}