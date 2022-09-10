using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Prtscbot.Utils;

namespace Prtscbot.Commands.Group.Executor
{
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
                        AdminCheck admincheck = new AdminCheck(message.From.Id);
                        
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
                        else
                        {
                                try {
                                        await botClient.PinChatMessageAsync(
                                                chatId: message.Chat.Id,
                                                messageId: message.ReplyToMessage.MessageId
                                        );
                                        string text = TranslateLocale.exec(
                                                message, "command.Group.Pin.Success"
                                        );
                                        await botClient.SendTextMessageAsync(
                                                chatId: message.Chat.Id,
                                                text: text,
                                                parseMode: ParseMode.Html
                                        );
                                } catch (ApiRequestException) {
                                        string text = TranslateLocale.exec(
                                                message, "command.Group.Pin.NotEnoughPermission"
                                        );
                                        await botClient.SendTextMessageAsync(
                                                chatId: message.Chat.Id,
                                                text: text,
                                                parseMode: ParseMode.Html
                                        );
                                }
                                // string text = TranslateLocale.exec(
                                //         message, "command.Group.Pin.Success"
                                // );
                                // await botClient.SendTextMessageAsync(
                                //         chatId: message.Chat.Id,
                                //         text: text,
                                //         parseMode: ParseMode.Html
                                // );
                        }

                }
                protected bool checkIsReply(Message message)
                {
                        if (message.ReplyToMessage == null)
                        {
                                return false;
                        }
                        else
                        {
                                return true;
                        }
                }
        }
}