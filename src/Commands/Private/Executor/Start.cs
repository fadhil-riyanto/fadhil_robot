using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Prtscbot.Utils;

namespace Prtscbot.Commands.Private.Executor {
        class Start
        {
                public async static Task Execute(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        string text = TranslateLocale.exec(
                                        message, 
                                        "command.Start", 
                                        inputTelegram.command
                        );
                        await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: text, replyToMessageId: message.MessageId, parseMode: ParseMode.Html);
                }
        }
}