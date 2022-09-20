// SPDX-License-Identifier: GPL-2.0

/*
 *  main.c
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using MongoDB.Driver;
using Telegram.Bot;

namespace fadhil_robot.Utils {
        class InputTelegram
        {
                public string command;
                public string value;
                public CancellationToken cancellationToken;

        }
}