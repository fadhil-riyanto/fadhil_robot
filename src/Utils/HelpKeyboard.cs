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
                private string _language;
                private InlineKeyboardMarkup _keyboard;
                public TGKeyboardHelpMenu(InputTelegram inputTelegram)
                {
                        this._inputTelegram = inputTelegram;
                        this._language = inputTelegram.languange;
                }

                private Dictionary<string, string> _translations(string languange)
                {
                        Dictionary<string, string> id_ID = new Dictionary<string, string> {
                                {"admin" , "Admin"},

                                // for back
                                {"back" , "Kembali ◀️"}
                        };

                        Dictionary<string, string> en_US = new Dictionary<string, string> {
                                {"admin" , "Admin"},

                                // for back
                                {"back" , "Back ◀️"}
                        };

                        if (languange == "id") {
                                return id_ID;
                        } else if (languange == "us") {
                                return en_US;
                        } else {
                                return en_US;
                        }
                }

                public TGKeyboardHelpMenu detectLanguangeMainButton()
                {
                        Dictionary<string, string> data = this._translations(this._language);

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

                public TGKeyboardHelpMenu detectLanguangeBackButton()
                {
                        Dictionary<string, string> data = this._translations(this._language);

                        this._keyboard = new(new[]
                                {
                                        new []
                                        {
                                                InlineKeyboardButton.WithCallbackData(
                                                        
                                                        text: data["back"], callbackData: CallbackHelper.pack(
                                                                        this._inputTelegram, "help_back", new Dictionary<string, string> {
                                                                        { "clicked_button", "back"}
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

                public string getContent(string keys)
                {
                        Dictionary<string, string> id_ID = new Dictionary<string, string> {
                                {"admin" , 
                                        "berikut adalah perintah untuk administrasi di grup:\n\n" +
                                        "/pin: digunakan untuk menyematkan pesan" +
                                        "/unpin: digunakan untuk melepas sematan grup"
                                }
                        };

                        Dictionary<string, string> en_US = new Dictionary<string, string> {
                                {"admin" , 
                                        "this is command for manage in your group:\n\n" +
                                        "/pin: this used for pined your messange" +
                                        "/unpin: this used for unpined your messange"
                                }
                        };

                        if (this._language == "id") {
                                return id_ID[keys];
                        } else if (this._language == "us") {
                                return en_US[keys];
                        } else {
                                return en_US[keys];
                        }
                }
        }
}