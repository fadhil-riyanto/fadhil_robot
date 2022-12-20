// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;
using TL;

namespace fadhil_robot.Commands.Global.Executor
{
    class Lookup : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Lookup(InputTelegram inputTelegram,
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
                    new fadhil_robot.TranslationString.Global.Lookup.NeedArguments()
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId
                );
            }
            else
            {
                try
                {
                    Contacts_ResolvedPeer peerdata = await this._inputTelegram.
                        main_thread_ctx.MtprotoClient.Contacts_ResolveUsername(UtilsFunction.normalizing(this._inputTelegram.value));

                    string text = TranslateLocale.CreateTranslation(
                        this._message, new fadhil_robot.TranslationString.Global.Lookup.Success(),
                        peerdata.User.first_name + " " +
                        peerdata.User.last_name,
                        peerdata.User.id.ToString(), peerdata.User.lang_code, peerdata.User.username,
                        UtilsFunction.is64(peerdata.User.id)
                    );

                    await this._botClient.SendTextMessageAsync(
                        chatId: this._message.Chat.Id,
                        text: text,
                        replyToMessageId: this._message.MessageId,
                        parseMode: ParseMode.Html
                    );
                }
                catch (RpcException e)
                {
                    string error_result = e.Message switch
                    {
                        "USERNAME_INVALID" => TranslateLocale.CreateTranslation(
                                this._message,
                                new fadhil_robot.TranslationString.Global.Lookup.UsernameInvalid()
                                ),
                        _ => TranslateLocale.CreateTranslation(
                                this._message, new fadhil_robot.TranslationString.System.Unknown(),
                                e.Message.ToString()
                            )
                    };
                    await this._botClient.SendTextMessageAsync(
                        chatId: this._message.Chat.Id,
                        text: error_result,
                        replyToMessageId: this._message.MessageId,
                        parseMode: ParseMode.Html
                    );
                }
            }

        }
    }
}