// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */

using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace fadhil_robot.Utils
{
        class TGKeyboardHelpMenu : ITgKeyboard
        {
                private InputTelegram _inputTelegram;
                private ITelegramBotClient _botClient;
                private Message _message;
                private InlineKeyboardMarkup _keyboard;
                public TGKeyboardHelpMenu(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this._inputTelegram = inputTelegram;
                        this._botClient = botClient;
                        this._message = message;
                }

                private Dictionary<string, string> _translations(string languange)
                {
                        Dictionary<string, string> id_ID = new Dictionary<string, string> {
                                {"admin" , "Admin"}
                        };

                        Dictionary<string, string> en_US = new Dictionary<string, string> {
                                {"admin" , "Admin"}
                        };

                        if (languange == "id") {
                                return id_ID;
                        } else if (languange == "us") {
                                return en_US;
                        } else {
                                return en_US;
                        }
                }

                public TGKeyboardHelpMenu detectLanguange()
                {
                        Dictionary<string, string> data = this._translations(this._message.From.LanguageCode);

                        this._keyboard = new(new[]
                                {
                                        new []
                                        {
                                                InlineKeyboardButton.WithCallbackData(
                                                        
                                                        text: data["admin"], callbackData: CallbackHelper.pack(
                                                                        this._inputTelegram, "help", new Dictionary<string, string> {
                                                                        { "clicked_button", "admin"}
                                                                }
                                                        )
                                                ),
                                        }
                                }
                        );
                        return this;
                }

                public Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup get()
                {
                        return this._keyboard;
                }
        }
        // {
        //         // contruct receive data from main
        //         public ID_ID()

        //         public Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup get()
        //         {
        //                 InlineKeyboardMarkup inlineKeyboard = new(new[]
        //                         {
        //                                 new []
        //                                 {
        //                                         InlineKeyboardButton.WithCallbackData(
                                                        
        //                                                 text: "Admins", callbackData: CallbackHelper.pack(
        //                                                                 message, inputTelegram, "help", new Dictionary<string, string> {
        //                                                                 { "clicked_button", "admin"}
        //                                                         }
        //                                                 )
        //                                         ),
        //                                 }
        //                         }
        //                 );
        //         }
        // }
}
