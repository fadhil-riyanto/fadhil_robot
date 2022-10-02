// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;
using TL;

namespace fadhil_robot.Commands.Group.Executor {
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
                        Contacts_ResolvedPeer peerdata = await this._inputTelegram.main_thread_ctx.ClientMT.Contacts_ResolveUsername(this._inputTelegram.value);
                        string text = "user info\n\n" +
                                        "name: " + peerdata.User.first_name + " " + peerdata.User.last_name + "\n"+
                                        "id: " + peerdata.User.id;
                        await this._botClient.SendTextMessageAsync(
                                chatId: this._message.Chat.Id, 
                                text: text, 
                                replyToMessageId: this._message.MessageId, 
                                parseMode: ParseMode.Html
                        );
                }
        }
}