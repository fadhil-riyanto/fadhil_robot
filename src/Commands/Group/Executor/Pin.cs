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
    class Pin : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Message _message;
        public Pin(InputTelegram inputTelegram, ITelegramBotClient
        botClient, Message message)
        {
            this._inputTelegram = inputTelegram;
            this._botClient = botClient;
            this._message = message;
        }
        public async Task Execute()
        {

            AdminCheck admincheck = new AdminCheck(this._inputTelegram,
            this._botClient, this._message);

            if (admincheck.IsAdmin(this._message.From.Id).Result)
            {
                if (!this.CheckIsReply(this._message))
                {
                    string text = TranslateLocale.exec(
                        this._message, "command.Group.Pin.NeedReply"
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
                        await this._botClient.PinChatMessageAsync(
                            chatId: this._message.Chat.Id,
                            messageId: this._message.ReplyToMessage.MessageId
                        );
                        string text = TranslateLocale.exec(
                            this._message, "command.Group.Pin.Success"
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
                        string text = TranslateLocale.exec(
                            this._message, "command.Group.Pin.NotEnoughPermission"
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