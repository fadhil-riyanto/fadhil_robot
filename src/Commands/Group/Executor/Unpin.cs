// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
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
                private InputTelegram inputTelegram;
                private ITelegramBotClient botClient;
                private Message message;
                public Unpin(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this.inputTelegram = inputTelegram;
                        this.botClient = botClient;
                        this.message = message;
                }
                public async Task Execute()
                {

                        AdminCheck admincheck = new AdminCheck(inputTelegram, botClient, message);
                        if(admincheck.isAdmin(message.From.Id).Result)
                        {
                                if (!this.checkIsReply(message))
                                {
                                        string text = TranslateLocale.exec(
                                                message, "command.Group.Unpin.NeedReply"
                                        );
                                        await botClient.SendTextMessageAsync(
                                                chatId: message.Chat.Id,
                                                text: text,
                                                replyToMessageId: message.MessageId, 
                                                parseMode: ParseMode.Html
                                        );
                                }
                                else
                                {
                                        try {
                                                await botClient.UnpinChatMessageAsync(
                                                        chatId: message.Chat.Id,
                                                        messageId: message.ReplyToMessage.MessageId
                                                );
                                                string text = TranslateLocale.exec(
                                                        message, "command.Group.Unpin.Success"
                                                );
                                                await botClient.SendTextMessageAsync(
                                                        chatId: message.Chat.Id,
                                                        text: text,
                                                        replyToMessageId: message.MessageId, 
                                                        parseMode: ParseMode.Html
                                                );
                                        } catch (ApiRequestException) {
                                                string text = TranslateLocale.exec(
                                                        message, "command.Group.Unpin.NotEnoughPermission"
                                                );
                                                await botClient.SendTextMessageAsync(
                                                        chatId: message.Chat.Id,
                                                        text: text,
                                                        replyToMessageId: message.MessageId, 
                                                        parseMode: ParseMode.Html
                                                );
                                        }
                                }
                        } else {
                                string text = TranslateLocale.exec(
                                        message, "GroupNotAdmin"
                                );
                                await botClient.SendTextMessageAsync(
                                        chatId: message.Chat.Id,
                                        text: text,
                                        replyToMessageId: message.MessageId, 
                                        parseMode: ParseMode.Html
                                );
                        }
                        

                }
                protected bool checkIsReply(Message message)
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