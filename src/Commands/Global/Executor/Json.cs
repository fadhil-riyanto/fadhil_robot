// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using System.Text.Json;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Commands.Global.Executor
{
    class Json : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Json(InputTelegram inputTelegram,
        ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
        {
            this._inputTelegram = inputTelegram;
            this._botClient = botClient;
            this._message = message;
        }

        public bool is_real_command()
        {
            return true;
        }
        public async Task Execute()
        {
            // create json
            var opt = new JsonSerializerOptions() { WriteIndented = true };
            string text = JsonSerializer.Serialize<Telegram.Bot.Types.Message>(this._message, opt);

            string path = Path.GetTempFileName();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096)) {
                byte[] jsonbyte = new System.Text.UTF8Encoding(true).GetBytes(text);
                await fs.WriteAsync(jsonbyte, 0, jsonbyte.Length);

            }
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose)) {
                await this._botClient.SendDocumentAsync(
                    chatId: this._message.Chat.Id,
                    document: new Telegram.Bot.Types.InputFiles.InputOnlineFile(fs, "debug.txt"),
                    caption: fadhil_robot.Utils.TranslateLocale.CreateTranslation(
                        this._message, new fadhil_robot.TranslationString.Global.Json.Success()
                    )
                );
            }
        }
    }
}