// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using fadhil_robot;
using TL;
using Telegram.Bot.Types;
using fadhil_robot.Utils.Exception;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace fadhil_robot.Utils
{
    class args_parse
    {
        private string _text;
        private int getter_total;
        private string[] _data;
        private string[] _data_val;
        public args_parse(string text, int getter_total)
        {
            this._text = text;
            this.getter_total = getter_total;
            this._data_val = this.split();
            this._data = this.split();
        }
        private string[] split()
        {
            return this._text.Split(" ");
        }

        public string getArg(int index)
        {
            if (index <= this.getter_total)
            {
                return this._data[index];

            }
            else
            {
                throw new OverflowException();
            }
        }

        public string? getValue()
        {
            string? tmp = null;
            for (int a = 0; a <= this.getter_total; a++)
            {
                this._data_val[a] = "";
            }
            for (int i = 0; i < this._data_val.Length; i++)
            {
                if (this._data_val[i] == "")
                {
                    // skippable
                }
                else if (i == this._data_val.Length)
                {
                    tmp = tmp + this._data_val[i];
                }
                else
                {
                    tmp = tmp + this._data_val[i] + " ";
                }
            }
            return tmp;
        }
    }
    class UtilsFunction
    {
        public static string is64(long data)
        {
            if (data < Int32.MaxValue)
            {
                return "32";
            }
            else if (data < Int64.MaxValue)
            {
                return "64";
            }
            else
            {
                return "unknown";
            }
        }

        public static string deep_linking_gen(string data)
        {
            return $"https://t.me/{Config.BotName}?start={data}";
        }

        // get the userid from reply or mentions
        public static async Task<long> user_id_getter(InputTelegram inputTelegram, Telegram.Bot.Types.Message message)
        {
            long user_id;
            if (message.ReplyToMessage != null && inputTelegram.value != null)
            {
                user_id = 0;
                throw new DoubleInputException();

            }
            else if (message.ReplyToMessage == null && inputTelegram.value == null)
            {
                user_id = 0;
                throw new ArgumentNullException();

            }
            else if (message.ReplyToMessage != null)
            {
                user_id = message.ReplyToMessage.From.Id;
            }
            else if (inputTelegram.value != null)
            {
                Contacts_ResolvedPeer peerdata = await inputTelegram.
                        main_thread_ctx.MtprotoClient.Contacts_ResolveUsername(UtilsFunction.normalizing(inputTelegram.value));
                user_id = peerdata.User.id;
            }
            else
            {
                user_id = 0;
            }
            return user_id;
        }
        public static string normalizing(string username)
        {
            if (username[0] == '@')
            {
                return username.Remove(0, 1);
            }
            else
            {
                return username;
            }
        }


        public static async Task<string> translate_libretranslate(string text, string source, string target)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
                { "q", text },
                { "source",  source },
                { "target", target }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://libretranslate.de/translate", content);

            JObject responseString = JObject.Parse(await response.Content.ReadAsStringAsync());
            return responseString["translatedText"].ToString();
        }
    }


}


