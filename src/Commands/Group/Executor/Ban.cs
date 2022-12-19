// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/


using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using fadhil_robot.Utils;
using fadhil_robot.Utils.Exception;
using TL;

namespace fadhil_robot.Commands.Group.Executor
{
    class Ban : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Telegram.Bot.Types.Message _message;
        public Ban(InputTelegram inputTelegram,
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
            AdminCheck admincheck = new AdminCheck(this._inputTelegram,
            this._botClient, this._message);

            if (admincheck.IsAdmin(this._message.From.Id).Result)
            {
                // check if they is not reply their messange
                await this.its_admin();
            }
            else
            {
                string text = TranslateLocale.exec(
                    this._message, "GroupNotAdmin"
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId,
                    parseMode: ParseMode.Html
                );
            }
        }

        private async Task its_admin()
        {
            try
            {
                long userids = await UtilsFunction.user_id_getter(this._inputTelegram, this._message);
                AdminCheck admincheck = new AdminCheck(this._inputTelegram,
                    this._botClient, this._message);

                await admincheck.force_make_new_cache();
                if (admincheck.IsAdmin(userids).Result)
                {
                    string text = TranslateLocale.exec(
                        this._message, "command.Group.Ban.FailBanAdmin",
                        userids.ToString()
                    );
                    await this._botClient.SendTextMessageAsync(
                        chatId: this._inputTelegram.chat_id,
                        text: text,
                        replyToMessageId: this._inputTelegram.messange_id
                    );
                }
                else
                {
                    await this._botClient.BanChatMemberAsync(
                        chatId: this._inputTelegram.chat_id,
                        userId: userids
                    );
                    string text = TranslateLocale.exec(
                        this._message, "command.Group.Ban.Succeed",
                        userids.ToString()
                    );
                    await this._botClient.SendTextMessageAsync(
                        chatId: this._inputTelegram.chat_id,
                        text: text,
                        replyToMessageId: this._inputTelegram.messange_id
                    );
                }
            }
            catch (DoubleInputException)
            {
                string text = TranslateLocale.exec(
                    this._message, "command.Group.Ban.DoubleInputException"
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._inputTelegram.chat_id,
                    text: text,
                    replyToMessageId: this._inputTelegram.messange_id
                );
            }
            catch (ArgumentNullException)
            {
                string text = TranslateLocale.exec(
                    this._message, "command.Group.Ban.ArgumentNullException"
                );
                await this._botClient.SendTextMessageAsync(
                    chatId: this._inputTelegram.chat_id,
                    text: text,
                    replyToMessageId: this._inputTelegram.messange_id
                );
            }
        }
    }
}