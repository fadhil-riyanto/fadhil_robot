// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using fadhil_robot.Utils;

namespace fadhil_robot.Commands.Group.Executor
{
    class Unpin : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Message _message;
        public Unpin(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
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
                if (!this.CheckIsReply(this._message))
                {
                    string text = TranslateLocale.CreateTranslation(
                        this._message, new fadhil_robot.TranslationString.Groups.Unpin.NeedReply()
                    );
                    await this._botClient.SendTextMessageAsync(
                        chatId: this._message.Chat.Id,
                        text: text,
                        replyToMessageId: this._message.MessageId,
                        parseMode: ParseMode.Html
                    );
                }
                else
                {
                    try
                    {
                        await this._botClient.UnpinChatMessageAsync(
                            chatId: this._message.Chat.Id,
                            messageId: this._message.ReplyToMessage.MessageId
                        );
                        string text = TranslateLocale.CreateTranslation(
                            this._message, new fadhil_robot.TranslationString.Groups.Unpin.Success()
                        );
                        await this._botClient.SendTextMessageAsync(
                            chatId: this._message.Chat.Id,
                            text: text,
                            replyToMessageId: this._message.MessageId,
                            parseMode: ParseMode.Html
                        );
                    }
                    catch (ApiRequestException)
                    {
                        string text = TranslateLocale.CreateTranslation(
                            this._message, new fadhil_robot.TranslationString.System.NotEnoughPermission()
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
            else
            {
                string text = TranslateLocale.CreateTranslation(
                    this._message, new fadhil_robot.TranslationString.System.GroupNotAdmin()
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId,
                    parseMode: ParseMode.Html
                );
            }


        }
        protected bool CheckIsReply(Message message)
        {
            if (message.ReplyToMessage == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}