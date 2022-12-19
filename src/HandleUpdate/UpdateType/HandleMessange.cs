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
    class command_lists
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Message _message;
        private Utils.command_executed_at _executed_at;
        public command_lists(InputTelegram inputTelegram, ITelegramBotClient
        botClient, Message message)
        {
            this._inputTelegram = inputTelegram;
            this._botClient = botClient;
            this._message = message;
        }
        public IExecutor private_chat()
        {
            Utils.IExecutor executor = this._inputTelegram.command switch
            {
                "start" => new Commands.Private.Executor.Start(this._inputTelegram, this._botClient, this._message),
                "ping" => new Commands.Private.Executor.Ping(this._inputTelegram, this._botClient, this._message),
                "help" => new Commands.Private.Executor.Help(this._inputTelegram, this._botClient, this._message),
                // "whoami" => new Commands.Private.Executor.Whoami(this._inputTelegram, this._botClient, this._message),
                _ => null
                // new UnknownCommand(this._inputTelegram, this._botClient, this._message)
            };

            if (executor == null)
            {
                executor = this.global_chat();
            }

            return executor;
        }
        public IExecutor group_chat()
        {
            Utils.IExecutor executor = this._inputTelegram.command switch
            {
                "ban" => new Commands.Group.Executor.Ban(this._inputTelegram, this._botClient, this._message),
                "help" => new Commands.Group.Executor.Help(this._inputTelegram, this._botClient, this._message),
                "ping" => new Commands.Group.Executor.Ping(this._inputTelegram, this._botClient, this._message),
                "admincache" => new Commands.Group.Executor.Admincache(this._inputTelegram, this._botClient, this._message),
                "pin" => new Commands.Group.Executor.Pin(this._inputTelegram, this._botClient, this._message),
                "unpin" => new Commands.Group.Executor.Unpin(this._inputTelegram, this._botClient, this._message),
                
                // "whoami" => new Commands.Group.Executor.Whoami(this._inputTelegram, this._botClient, this._message),
                "adminlist" or "getadmin" => new Commands.Group.Executor.Adminlist(this._inputTelegram, this._botClient, this._message),
                _ => null
            };

            if (executor == null)
            {
                executor = this.global_chat();
            }

            return executor;
        }

        public IExecutor global_chat()
        {
            
            Utils.IExecutor executor = this._inputTelegram.command switch
            {
                "lookup" => new Commands.Global.Executor.Lookup(this._inputTelegram, this._botClient, this._message),
                "whoami" => new Commands.Global.Executor.Whoami(this._inputTelegram, this._botClient, this._message),
                _ => new UnknownCommand(this._inputTelegram, this._botClient, this._message)
            };
            return executor;
        }
    }
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
            command_lists commands = new command_lists(inputTelegram, botClient, message);
            // if (message.Chat.Type == ChatType.Supergroup || message.Chat.Type == ChatType.Group || message.Chat.Type == ChatType.Private)
            // {
            //     await commands.global_chat().Execute();
            // }
            // else 
            if (message.Chat.Type == ChatType.Private)
            {

                if (commands.group_chat().is_real_command())
                {
                    if (commands.private_chat().is_real_command())
                    {

                        await commands.private_chat().Execute();
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "this command just available in group",
                            replyToMessageId: message.MessageId,
                            parseMode: ParseMode.Html
                        );
                    }
                }
                else
                {
                    await commands.private_chat().Execute();
                }
            }
            else if (message.Chat.Type == ChatType.Supergroup || message.Chat.Type == ChatType.Group)
            {
                // await commands.group_chat().Execute();
                if (commands.private_chat().is_real_command())
                {
                    if (commands.group_chat().is_real_command())
                    {

                        await commands.group_chat().Execute();
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "this command just available in private",
                            replyToMessageId: message.MessageId,
                            parseMode: ParseMode.Html
                        );
                    }
                }
                else
                {
                    await commands.group_chat().Execute();
                }
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