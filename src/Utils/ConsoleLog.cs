// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using System;

namespace fadhil_robot.Utils
{
        class ConsoleLog
        {
                public ConsoleLog(string data)
                {
                        DateTime datetime = DateTime.UtcNow.Date;

                        string date = datetime.ToString("dd/mm/yyyy");
                        string time = datetime.ToString("hh:mm:ss");
                        Console.WriteLine("[INPUT] {0} | {1}", DateTime.Now, data);
                }
        }
        class ConsoleLogError
        {
                public ConsoleLogError(string data)
                {
                        DateTime datetime = DateTime.UtcNow.Date;
                        Console.WriteLine("[ERROR] {0} | {1}", DateTime.Now, data);
                }
        }
        class ConsoleLogCb
        {
                public ConsoleLogCb(string data)
                {
                        DateTime datetime = DateTime.UtcNow.Date;
                        Console.WriteLine("[TG_CB] {0} | {1}", DateTime.Now, data);
                }
        }
}