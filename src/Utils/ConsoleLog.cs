// SPDX-License-Identifier: GPL-2.0

/*
 *  main.c
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
                        Console.WriteLine("{0} - {1} | {2}", date, time, data);
                }
        }
}