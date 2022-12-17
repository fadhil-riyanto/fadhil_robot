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

namespace fadhil_robot.Commands.Group.Executor
{
    class Adminlist : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private ChatMember[] _adminlist;
        private Message _message;
        public Adminlist(InputTelegram inputTelegram, ITelegramBotClient
        botClient, Message message)
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
                await this.getData();

                List<string> adm_parse = new List<string>();
                string[] admin = this.getadmin();
                for (int i = 0; i < admin.Length; i++)
                {
                    if (i == admin.Length - 1)
                    {
                        adm_parse.Add($"\u200E└ {admin[i]}");
                    }
                    else
                    {
                        adm_parse.Add($"\u200E├ {admin[i]}");
                    }
                }

                string owner = this.getowner();
                string text = $"Owner:\n{owner}\n\nAdmin:\n{String.Join("\n", adm_parse)}";
                await this._botClient.SendTextMessageAsync(
                    chatId: this._message.Chat.Id,
                    text: text,
                    replyToMessageId: this._message.MessageId,
                    parseMode: ParseMode.Html
                );
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
        private async Task getData()
        {
            this._adminlist = await this._botClient.GetChatAdministratorsAsync(chatId: this._message.Chat.Id);
        }
        private string[] getadmin()
        {

            List<string> adminName = new List<string>();
            foreach (ChatMember listadmins in this._adminlist)
            {
                if (listadmins.GetType() == typeof(Telegram.Bot.Types.ChatMemberAdministrator))
                {
                    adminName.Add($"<a href=\"tg://user?id={listadmins.User.Id}\">\u200E{listadmins.User.FirstName + " " + listadmins.User.LastName}</a>");
                }
            }
            return adminName.ToArray<string>();
        }
        private string getowner()
        {
            string adminName = TranslateLocale.exec(
                this._message, "command.Group.Adminlist.OwnerNotFound"
            );
            foreach (var listadmins in this._adminlist)
            {
                if (listadmins.GetType() == typeof(Telegram.Bot.Types.ChatMemberOwner))
                {
                    adminName = $"<a href=\"tg://user?id={listadmins.User.Id}\">\u200E{listadmins.User.FirstName + " " + listadmins.User.LastName}</a>";
                }
            }
            return adminName;
        }
    }
}