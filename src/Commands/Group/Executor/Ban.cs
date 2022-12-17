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

namespace fadhil_robot.Commands.Group.Executor
{
    class Ban : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Ban(InputTelegram inputTelegram,
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
            // check if they is not reply their messange
            if (this._message.ReplyToMessage == null)
            {
                string text = TranslateLocale.exec(
                    this._message, "command.Group.Ban"
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._inputTelegram.chat_id,
                    text: text,
                    replyToMessageId: this._inputTelegram.messange_id
                );
            }
            else
            {
                await this._botClient.BanChatMemberAsync(
                    chatId: this._inputTelegram.chat_id,
                    userId: this._message.ReplyToMessage.From.Id
                );
                string text = TranslateLocale.exec(
                    this._message, "command.Group.Ban.Succeed", 
                    this._message.ReplyToMessage.From.FirstName + " " +
                    this._message.ReplyToMessage.From.LastName
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._inputTelegram.chat_id,
                    text: text,
                    replyToMessageId: this._inputTelegram.messange_id
                );

                
            }
        }
    }
}