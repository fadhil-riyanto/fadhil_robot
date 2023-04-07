// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using fadhil_robot.Utils;
using Fluent.LibreTranslate;

namespace fadhil_robot.Commands.Global.Executor
{
    class Translate : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Translate(InputTelegram inputTelegram,
        ITelegramBotClient botClient, Telegram.Bot.Types.Message message)
        {
            this._inputTelegram = inputTelegram;
            this._botClient = botClient;
            this._message = message;
        }

        public bool is_real_command()
        {
            return true;
        }
        public async Task Execute()
        {
            if (this._inputTelegram.value == null)
            {
                // handle if that is null and can't next operate
                string text = TranslateLocale.CreateTranslation(
                    this._message,
                    new fadhil_robot.TranslationString.Global.Translate.NeedArguments()
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId
                );
            }
            else
            {
                Utils.args_parse args_result = new Utils.args_parse(this._inputTelegram.value, 0);

                if (args_result.getValue() == null)
                {
                    string text = TranslateLocale.CreateTranslation(this._message, new fadhil_robot.TranslationString.Global.Translate.NeedText());
                    
                    await this._botClient.SendTextMessageAsync(
                        chatId: this._message.Chat.Id,
                        text: text,
                        replyToMessageId: this._message.MessageId
                    );
                }
                else
                {
                    GlobalLibreTranslateSettings.Server = LibreTranslateServer.Libretranslate_de;
                    GlobalLibreTranslateSettings.ApiKey = null;
                    GlobalLibreTranslateSettings.UseRateLimitControl = true; //to avoid "429 Too Many Requests" exception
                    GlobalLibreTranslateSettings.RateLimitTimeSpan = TimeSpan.FromMicroseconds(0);


                    try
                    {
                        string translated = await args_result.getValue().TranslateAsync(LanguageCode.FromString(args_result.getArg(0)));
                        await this._botClient.SendTextMessageAsync(
                            chatId: this._message.Chat.Id,
                            text: translated,
                            replyToMessageId: this._message.MessageId,
                            parseMode: ParseMode.Html
                        );
                    }
                    catch (System.ArgumentException)
                    {
                        InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(
                            new InlineKeyboardButton[] {
                                InlineKeyboardButton.WithCallbackData(
                                    text: TranslateLocale.CreateTranslation(
                                        this._message, 
                                        new fadhil_robot.TranslationString.Global.Translate.ListLanguagesKeyboard(), 
                                        this._inputTelegram.command
                                    ),
                                    callbackData: CallbackHelper.pack(
                                        this._inputTelegram, "translate_lists_languages", new Dictionary<string, string> {
                                            { "user_id", this._message.From.Id.ToString() }
                                        }
                                    )
                                )
                            }
                        );
                        string text = TranslateLocale.CreateTranslation(
                            this._message, 
                            new fadhil_robot.TranslationString.Global.Translate.LanguageNotFound(),
                            args_result.getArg(0)
                        );
                        await this._botClient.SendTextMessageAsync(
                            chatId: this._message.Chat.Id,
                            text: text,
                            replyMarkup: keyboard,
                            replyToMessageId: this._message.MessageId
                        );
                    }
                }
            }

        }
    }
}