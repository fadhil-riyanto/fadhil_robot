// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */

using System.Security.Cryptography;
using System;
using System.Text;
class generate_rand_str
{
        public string csprng(int bytes)
        {
                #pragma warning disable SYSLIB0023
                using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
                {
                        byte[] tokenData = new byte[bytes];
                        rng.GetBytes(tokenData);

                        string token = Convert.ToBase64String(tokenData);
                        return token;
                }
        }
        public string csprng2(int bytes)
        {
                using (var generator = RandomNumberGenerator.Create())
                {
                        var salt = new byte[bytes];
                        generator.GetBytes(salt);
                        return Convert.ToBase64String(salt);
                }
        }

        public string unix()
        {
                var now = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
                return now.TotalSeconds.ToString();
        }

        public string rand_int()
        {
                Random rn = new Random();
                return rn.NextInt64(999999999999999999).ToString();
        }
        public string rand_int_range(int range)
        {
                Random rn = new Random();
                return rn.NextInt64(range).ToString();
        }

        public string sha256(string randomString)
        {
                #pragma warning disable SYSLIB0021
                var crypt = new SHA256Managed();
                string hash = String.Empty;
                byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
                foreach (byte theByte in crypto)
                {
                        hash += theByte.ToString("x2");
                }
                return hash;
        }

        public string md5(string input)
        {
                using var provider = System.Security.Cryptography.MD5.Create();
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                        builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();

        }

        public string gethash()
        {
                string data1 = this.sha256(this.csprng(4));
                string data2 = this.sha256(this.csprng(8));
                string data3 = this.sha256(this.unix());
                string data4 = this.sha256(this.rand_int());
                string data5 = this.sha256(this.rand_int_range(100));
                string data6 = this.sha256(this.rand_int_range(300));

                return this.md5(data1 + data2 + data3 + data4 + data5 + data6);
        }


}