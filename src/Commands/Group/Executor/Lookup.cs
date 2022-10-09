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
        class Lookup : Utils.IExecutor
        {
                private InputTelegram _inputTelegram;
                private ITelegramBotClient _botClient;
                private Telegram.Bot.Types.Message _message;
                public Lookup(InputTelegram inputTelegram,
                        ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
                {
                        this._inputTelegram = inputTelegram;
                        this._botClient = botClient;
                        this._message = message;
                }
                public async Task Execute()
                {
                        try
                        {
                                Contacts_ResolvedPeer peerdata = await this._inputTelegram.main_thread_ctx.ClientMT.Contacts_ResolveUsername(this._inputTelegram.value);
                                string text = TranslateLocale.exec(
                                        this._message, "command.Group.Lookup", peerdata.User.first_name + " " + peerdata.User.last_name,
                                        peerdata.User.id.ToString(), peerdata.User.lang_code, peerdata.User.username, UtilsFN.is64(peerdata.User.id)
                                );
                                await this._botClient.SendTextMessageAsync(
                                        chatId: this._message.Chat.Id,
                                        text: text,
                                        replyToMessageId: this._message.MessageId,
                                        parseMode: ParseMode.Html
                                );
                        }
                        catch (RpcException e)
                        {
                                string error_result = e.Message switch
                                {
                                        "USERNAME_INVALID" => TranslateLocale.exec(this._message, "command.Group.Lookup.UsernameInvalid"),
                                        _ => TranslateLocale.exec(this._message, "Unknown", e.Message.ToString())
                                };
                                await this._botClient.SendTextMessageAsync(
                                        chatId: this._message.Chat.Id,
                                        text: error_result,
                                        replyToMessageId: this._message.MessageId,
                                        parseMode: ParseMode.Html
                                );
                        }


                }
        }
}