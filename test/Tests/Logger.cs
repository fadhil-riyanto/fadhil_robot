using System;
namespace Test.Tests {
        class Logger {
                public static void run()
                {
                        DateTime datetime = DateTime.UtcNow.Date;


                        string date = datetime.ToString("dd/mm/yyyy");
                        string time = datetime.ToString("hh:mm:ss");
                        Console.WriteLine("{0} - {1} | ", date, time);
                }
        }
}