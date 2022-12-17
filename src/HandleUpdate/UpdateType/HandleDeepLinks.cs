// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/


using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;
using Telegram.Bot.Types.ReplyMarkups;
using fadhil_robot.Commands;

class deeplinks
{
    private InputTelegram _inputTelegram;
    private ITelegramBotClient _botClient;
    private Message _message;
    public deeplinks(InputTelegram inputTelegram, ITelegramBotClient
        botClient, Message message)
    {
        this._inputTelegram = inputTelegram;
        this._botClient = botClient;
        this._message = message;

        this.deeplinks_handler();
    }

    private void deeplinks_handler()
    {
        IExecutor deephandler = this._inputTelegram.value switch
        {
            "help_menu" => new fadhil_robot.Commands.Private.Executor.Help(this._inputTelegram, this._botClient, this._message),
            _ => new UnknownDeeplinks(this._inputTelegram, this._botClient, this._message)
        };

        deephandler.Execute();
    }

    
}

class UnknownDeeplinks : IExecutor
    {
        private InputTelegram inputTelegram;
        private ITelegramBotClient botClient;
        private Message message;
        public UnknownDeeplinks(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
        {
            this.inputTelegram = inputTelegram;
            this.botClient = botClient;
            this.message = message;
        }

        public command_executed_at at()
        {
            return command_executed_at.ignore;
        }

        public bool is_real_command()
        {
            return false;
        }
        public async Task Execute()
        {
           
            await this.botClient.SendTextMessageAsync(
                    chatId: this.message.Chat.Id,
                    text: "mwrong deeplink",
                    replyToMessageId: this.message.MessageId,
                    parseMode: ParseMode.Html
            );
            
        }
    }