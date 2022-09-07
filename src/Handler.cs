using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Prtscbot.Utils;
using Prtscbot.Commands.Private.Executor;

namespace Prtscbot.Program
{
        class HandleUpdate
        {
                public async Task Handle(ITelegramBotClient botClient, Message message)
                {
                        Parse parser = new Parse(message.Text);
                        InputTelegram inp = new InputTelegram();

                        inp.command = parser.getResult()["command"];
                        inp.value = parser.getResult()["value"];

                        await this.executor(inp, botClient, message);

                }
                public async Task defaultHandler(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        if (message.Chat.Type == ChatType.Private) {
                                string text = TranslateLocale.exec(
                                        message, 
                                        "UnknownCommand", 
                                        inputTelegram.command
                                );
                                await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: text, replyToMessageId: message.MessageId, parseMode: ParseMode.Html);
                        } else if (message.Chat.Type == ChatType.Supergroup) {
                                // not send anything
                        } 
                       
                }

                public async Task executor(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {      
                        Console.WriteLine(message.Chat.Type);
                        var return_caller = inputTelegram.command switch
                        {
                                "start" => Start.Execute(inputTelegram, botClient, message),
                                _ => defaultHandler(inputTelegram, botClient, message)
                        };

                        await return_caller;
                        // Console.WriteLine(inputTelegram.command);
                        // Console.WriteLine(inputTelegram.value);
                }
        }
}