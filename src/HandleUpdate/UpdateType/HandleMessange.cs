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
using fadhil_robot;

namespace fadhil_robot.HandleUpdate.UpdateType
{
    class UpdateType_Message
    {
        public async Task HandleMessange(ITelegramBotClient botClient,
                Message message, CancellationToken cancellationToken, main_thread_ctx ctx)
        {
            bool granted = false;
            foreach (makeNegative listall in Config.getWhiteList())
            {
                if (message.Chat.Id == listall.get())
                {
                    granted = true;
                }
                else if (message.Chat.Type == ChatType.Private)
                {
                    granted = true;
                }
            }
            if (granted)
            {
                try
                {
                    if (message.Text != null)
                    {
                        Parse parser = new Parse(message.Text);

                        InputTelegram inp = new InputTelegram
                        {
                            command = parser.getResult()["command"],
                            value = parser.getResult()["value"],
                            messange_id = message.MessageId,
                            chat_id = message.Chat.Id,
                            user_id = message.From.Id,
                            languange = message.From.LanguageCode,
                            cancellationToken = cancellationToken,
                            main_thread_ctx = ctx
                        };

                        await this.executor(inp, botClient, message);
                    }
                }
                catch (Exception e)
                {
                    if (Config.DEBUG_MODE)
                    {
                        string messange = "exception name: <b>" + e.GetType().Name + "</b>\nmessange: " +
                            e.Message + "\n\ntrace: \n" + e.StackTrace;
                        new ConsoleLogError(messange);
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: messange,
                                replyToMessageId: message.MessageId, parseMode: ParseMode.Html);
                    }
                    else
                    {
                        string messange = "exception name: <b>" + e.GetType().Name + "</b>\nmessange: " +
                                                    e.Message + "\n\ntrace: \n" + e.StackTrace;
                    }
                }
            }
            else
            {
                await botClient.LeaveChatAsync(message.Chat.Id);
            }
        }

        public async Task executor(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
        {
            new ConsoleLog(message.Chat.Id + " | " + message.Text);

            if (inputTelegram.command != null)
            {
                if (message.Text[0] == '/' || message.Text[0] == '!')
                {
                    inputTelegram.command = inputTelegram.command.ToLower();
                    await this.callerCommand(inputTelegram, botClient, message);
                }
                else
                {
                    // all this is text messange
                }
            }

        }

        protected async Task callerCommand(InputTelegram inputTelegram, ITelegramBotClient botClient,
                Message message)
        {
            

            // for receive private chat
            if (message.Chat.Type == ChatType.Private)
            {
                Utils.IExecutor executor = inputTelegram.command switch
                {
                    "start" => new Commands.Private.Executor.Start(inputTelegram, botClient, message),
                    "ping" => new Commands.Private.Executor.Ping(inputTelegram, botClient, message),
                    "help" => new Commands.Private.Executor.Help(inputTelegram, botClient, message),
                    "whoami" => new Commands.Private.Executor.Whoami(inputTelegram, botClient, message),
                    _ => new UnknownCommand(inputTelegram, botClient, message)
                };
                await executor.Execute();
            }
            else if (message.Chat.Type == ChatType.Supergroup || message.Chat.Type == ChatType.Group)
            {
                Utils.IExecutor executor = inputTelegram.command switch
                {
                    "ban" => new Commands.Group.Executor.Ban(inputTelegram, botClient, message),
                    "help" => new Commands.Group.Executor.Help(inputTelegram, botClient, message),
                    "ping" => new Commands.Group.Executor.Ping(inputTelegram, botClient, message),
                    "pin" => new Commands.Group.Executor.Pin(inputTelegram, botClient, message),
                    "unpin" => new Commands.Group.Executor.Unpin(inputTelegram, botClient, message),
                    "lookup" => new Commands.Group.Executor.Lookup(inputTelegram, botClient, message),
                    "whoami" => new Commands.Group.Executor.Whoami(inputTelegram, botClient, message),
                    "adminlist" or "getadmin" => new Commands.Group.Executor.Adminlist(inputTelegram, botClient, message),
                    _ => new UnknownCommand(inputTelegram, botClient, message)
                };
                await executor.Execute();
            }
        }
    }
    class UnknownCommand : Utils.IExecutor
    {
        private InputTelegram inputTelegram;
        private ITelegramBotClient botClient;
        private Message message;
        public UnknownCommand(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
        {
            this.inputTelegram = inputTelegram;
            this.botClient = botClient;
            this.message = message;
        }
        public async Task Execute()
        {
            if (this.message.Chat.Type == ChatType.Private)
            {
                string text = TranslateLocale.exec(
                        message,
                        "UnknownCommand",
                        this.inputTelegram.command
                );
                await this.botClient.SendTextMessageAsync(
                        chatId: this.message.Chat.Id,
                        text: text,
                        replyToMessageId: this.message.MessageId,
                        parseMode: ParseMode.Html
                );
            }
            else if (message.Chat.Type == ChatType.Supergroup)
            {
                string text = TranslateLocale.exec(
                        message,
                        "UnknownCommand",
                        this.inputTelegram.command
                );
                await this.botClient.SendTextMessageAsync(
                        chatId: this.message.Chat.Id,
                        text: text,
                        replyToMessageId: this.message.MessageId,
                        parseMode: ParseMode.Html
                );
            }
        }
    }


}