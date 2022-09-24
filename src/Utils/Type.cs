// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using MongoDB.Driver;
using Telegram.Bot;
using Telegram.Bot.Types;
using StackExchange.Redis;

namespace fadhil_robot.Utils {
        class InputTelegram
        {
                public string command;
                public string value;
                public CancellationToken cancellationToken;

        }

        class InputTelegramCb
        {
                public int messange_id { get; set; }
                public long chat_id { get; set; }
                public Dictionary<string, string> data { get; set; }
                public CallbackQuery callback { get; set; }

        }

        class main_thread_ctx
        {
                public ConnectionMultiplexer redis { get; set; }
        }
}