using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Prtscbot.Utils;
using Telegram.Bot.Types.ReplyMarkups;

namespace Prtscbot.Commands.Private.Executor
{
        class Start
        {
                private InputTelegram inputTelegram;
                private ITelegramBotClient botClient;
                private Message message;
                public Start(InputTelegram inputTelegram, ITelegramBotClient
                        botClient, Message message)
                {
                        this.inputTelegram = inputTelegram;
                        this.botClient = botClient;
                        this.message = message;
                }
                public async Task Execute()
                {
                        string text = TranslateLocale.exec(
                                        message,
                                        "command.Private.Start",
                                        this.inputTelegram.command
                        );

                        InlineKeyboardMarkup inlineKeyboard = new(new[]
                                {
                                        InlineKeyboardButton.WithUrl(
                                                text: TranslateLocale.exec(
                                                        message,"command.Private.Start.OwnerTextKeyboard", this.inputTelegram.command
                                                ),
                                                url: Config.OwnerLink
                                        )
                                }
                        );
                        await this.botClient.SendTextMessageAsync(
                                chatId: this.message.Chat.Id,
                                text: text,
                                replyToMessageId: this.message.MessageId,
                                parseMode: ParseMode.Html,
                                replyMarkup: inlineKeyboard
                        );
                }
        }
}