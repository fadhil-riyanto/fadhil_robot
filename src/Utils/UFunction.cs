// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) 2022 Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


namespace fadhil_robot.Utils
{
        class UtilsFN
        {
                public static string is64(long data)
                {
                        if (data < Int32.MaxValue) {
                                return "32";
                        } else if (data < Int64.MaxValue) {
                                return "64";
                        } else {
                                return "unknown";
                        }
                }
        }
}


