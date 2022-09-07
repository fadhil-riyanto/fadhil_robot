using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Prtscbot.Utils;

namespace Prtscbot.Program
{
        class HandleUpdate
        {
                public static async Task Handle(ITelegramBotClient botClient, Message message)
                {
                        Parse parser = new Parse(message.Text);
                        string formatted = String.Format("command: {0}\nvalue: {1}", 
                                parser.getResult()["command"], 
                                parser.getResult()["value"]
                        );
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: formatted!, replyToMessageId: message.MessageId, parseMode: ParseMode.Html);
                        new ConsoleLog(message.Text!);
        
                }
        }
}