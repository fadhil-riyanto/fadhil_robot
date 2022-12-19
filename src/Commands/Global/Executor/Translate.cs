// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;
using TL;

namespace fadhil_robot.Commands.Global.Executor
{
    class Translate : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Translate(InputTelegram inputTelegram,
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
            if (this._inputTelegram.value == null)
            {
                // handle if that is null and can't next operate
                string text = TranslateLocale.exec(this._message, "command.Global.Translate.NeedArgs");
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId
                );
            }
            else
            {
                Utils.args_parse args_result = new Utils.args_parse(this._inputTelegram.value, 0);
                System.Console.WriteLine(this._inputTelegram.value);
                System.Console.WriteLine(args_result.getArg(0));
                System.Console.WriteLine(args_result.getValue());
                
                string text = await UtilsFunction.translate_libretranslate(args_result.getValue(), "auto", args_result.getArg(0));
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId,
                    parseMode: ParseMode.Html
                );
            }

        }
    }
}