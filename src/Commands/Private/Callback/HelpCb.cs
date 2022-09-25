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


namespace fadhil_robot.Commands.Private.Callback {
        class HelpCb : Utils.IExecutor
        {
                private InputTelegramCb inputTelegramCb;
                private ITelegramBotClient botClient;
                private CallbackQuery callback;
                public HelpCb(InputTelegramCb inputTelegramCb, ITelegramBotClient botClient, CallbackQuery callback)
                {
                        this.inputTelegramCb = inputTelegramCb;
                        this.botClient = botClient;
                        this.callback = callback;
                }

                public async Task Execute()
                {
                        await this.botClient.SendTextMessageAsync(
                                chatId: this.inputTelegramCb.chat_id, 
                                text: "done banh", 
                                replyToMessageId: this.inputTelegramCb.messange_id, 
                                parseMode: ParseMode.Html
                        );
                }
        }
}