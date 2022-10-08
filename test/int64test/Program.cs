// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */

class is64{
        public is64(object data)
        {
                string type = Type.GetTypeCode(data.GetType()) switch
                {
                        TypeCode.Int32 => "32",
                        TypeCode.Int64 => "64",
                        _ => "unknown"
                };

                Console.WriteLine(type);
        }
}


class main
{
        public static void Main()
        {
                new is64(5252300008);
        }
}