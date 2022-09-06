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
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: message.Text!, replyToMessageId: message.MessageId, parseMode: ParseMode.Html);
                        new ConsoleLog(message.Text!);
                }
        }
}