using System;

namespace Prtscbot.Utils
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