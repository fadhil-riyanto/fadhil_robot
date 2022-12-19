// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
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
    class ConsoleLogTL
    {
        public ConsoleLogTL(string data)
        {
            DateTime datetime = DateTime.UtcNow.Date;
            Console.WriteLine("[TLMSG] {0} | {1}", DateTime.Now, data);
        }
    }
    class ConsoleLogSys
    {
        public ConsoleLogSys(string data)
        {
            DateTime datetime = DateTime.UtcNow.Date;
            Console.WriteLine("[CSMSG] {0} | {1}", DateTime.Now, data);
        }
    }
    class ConsoleLogSysLn
    {
        public ConsoleLogSysLn(string data)
        {
            DateTime datetime = DateTime.UtcNow.Date;
            Console.WriteLine("[CSMSG] {0} | {1}", DateTime.Now, data);
        }
    }
    class ConsoleLogCallback
    {
        public ConsoleLogCallback(string data)
        {
            DateTime datetime = DateTime.UtcNow.Date;
            Console.WriteLine("[TG_CB] {0} | {1}", DateTime.Now, data);
        }
    }

}