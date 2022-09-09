using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Prtscbot.Utils;
using Prtscbot.Commands.Private.Executor;

namespace Prtscbot.Program
{
        class HandleUpdate
        {
                public async Task HandleMessange(ITelegramBotClient botClient, Message message)
                {
                        try
                        {
                                if (message.Text != null)
                                {
                                        Parse parser = new Parse(message.Text);
                                        InputTelegram inp = new InputTelegram();

                                        inp.command = parser.getResult()["command"];
                                        inp.value = parser.getResult()["value"];

                                        await this.executor(inp, botClient, message);
                                }

                        }
                        catch (Exception e)
                        {
                                string messange = "exception name: <b>" + e.GetType().Name + "</b>\nmessange: " + e.Message + "\n\ntrace: \n" + e.StackTrace;
                                await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: messange, replyToMessageId: message.MessageId, parseMode: ParseMode.Html);
                        }



                }


                public async Task executor(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        new ConsoleLog(message.Text);

                        if (inputTelegram.command == "start") { 
                                var modPlugin = new Start(inputTelegram, botClient, message); 
                                await modPlugin.Execute(); 
                        } else if (inputTelegram.command == "test") { 
                                var modPlugin = new Test(inputTelegram, botClient, message); 
                                await modPlugin.Execute(); 
                        } else {
                                var modPlugin = new UnknownCommand(inputTelegram, botClient, message); 
                                await modPlugin.Execute(); 
                        }

                }
        }
        class UnknownCommand
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
                                // not send anything
                        }

                }
        }


}