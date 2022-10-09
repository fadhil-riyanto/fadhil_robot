// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) 2022 Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot.Types;
using Telegram.Bot;
using fadhil_robot.Utils;
using fadhil_robot.HandleUpdate.UpdateType;

namespace fadhil_robot.HandleUpdate
{
        class MainHandle
        {
                public async Task HandleMessange(ITelegramBotClient botClient,
                        Message message, CancellationToken cancellationToken, main_thread_ctx ctx)
                {
                        UpdateType_Message utm = new UpdateType_Message();
                        await utm.HandleMessange(botClient, message, cancellationToken, ctx);
                }

                public async Task HandleCallbackQuery(ITelegramBotClient botClient,
                        CallbackQuery data, CancellationToken cancellationToken, main_thread_ctx ctx)
                {
                        UpdateType_CallbackQuery utm = new UpdateType_CallbackQuery();
                        await utm.HandleCallbackQuery(botClient, data, cancellationToken, ctx);
                }
        }
}