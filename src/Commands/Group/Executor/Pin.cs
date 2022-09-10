using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Prtscbot.Utils;

namespace Prtscbot.Commands.Group.Executor {
        class Pin
        {
                private InputTelegram inputTelegram;
                private ITelegramBotClient botClient;
                private Message message;
                public Pin(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this.inputTelegram = inputTelegram;
                        this.botClient = botClient;
                        this.message = message;
                }
                public async Task Execute()
                {
                        if (!this.checkIsReply(message))
                        {

                                string text = TranslateLocale.exec(
                                        message, "command.Group.Pin.NeedReply"
                                );
                                await botClient.SendTextMessageAsync(
                                        chatId: message.Chat.Id,
                                        text: text,
                                        parseMode: ParseMode.Html
                                );
                        }
                        
                }
                protected bool checkIsReply(Message message)
                {
                        if (message.ReplyToMessage == null)
                        {
                                return false;
                        } else {
                                return true;
                        }
                }
        }
}