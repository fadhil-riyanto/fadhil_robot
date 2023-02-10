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
            var opt = new JsonSerializerOptions(){ WriteIndented=true };
            string text = JsonSerializer.Serialize<Telegram.Bot.Types.Message>(this._message, opt);
            

            // string fileName = $"{this._message.MessageId}.txt";
            // int bufferSize = 4096;
            // var fileStream = System.IO.File.Create(fileName, bufferSize, System.IO.FileOptions.DeleteOnClose);
            // await fileStream.WriteAsync(text);



            // await this._botClient.SendTextMessageAsync(
            //     chatId: this._message.Chat.Id,
            //     text: text,
            //     replyToMessageId: this._message.MessageId
            // );
        }
    }
}