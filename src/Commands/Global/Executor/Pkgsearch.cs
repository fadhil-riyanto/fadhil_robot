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
using Archlinux.Api;
using Archlinux.Api.Exception;
using Archlinux.Api.Methods;
using Archlinux.Api.Types.Result;
using Archlinux.Api.Types;
using Newtonsoft.Json;


namespace fadhil_robot.Commands.Global.Executor
{


    class Pkgsearch : Utils.IExecutor
    {
        private InputTelegram _inputTelegram;
        private ITelegramBotClient _botClient;
        private Utils.args_parse _args_result;
        private Telegram.Bot.Types.Message _message;
        public Pkgsearch(InputTelegram inputTelegram,
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
                await this.NeedRepository();
            }
            else
            {
                Utils.args_parse args_result = new Utils.args_parse(this._inputTelegram.value, 0);
                if (args_result.getIndex(1) == null)
                {
                    await this.NeedArch();
                }
                else if (args_result.getIndex(2) == null)
                {
                    await this.NeedPkgName();
                }
                else
                {
                    this._args_result = args_result;
                    await this.doWhileIsValid();
                    
                }
            }
        }

        public async Task NeedPkgName()
        {
            string text = TranslateLocale.CreateTranslation(
                this._message,
                new fadhil_robot.TranslationString.Global.Pkgsearch.NeedPkgName()
            );
            await this._botClient.SendTextMessageAsync(
                chatId: this._message.Chat.Id,
                text: text,
                replyToMessageId: this._message.MessageId,
                parseMode: ParseMode.Markdown
            );
        }

        public async Task NeedRepository()
        {
            string text = TranslateLocale.CreateTranslation(
                this._message,
                new fadhil_robot.TranslationString.Global.Pkgsearch.NeedRepository()
            );
            await this._botClient.SendTextMessageAsync(
                chatId: this._message.Chat.Id,
                text: text,
                replyToMessageId: this._message.MessageId,
                parseMode: ParseMode.Markdown
            );
        }

        public async Task NeedArch()
        {
            string text = TranslateLocale.CreateTranslation(
                this._message,
                new fadhil_robot.TranslationString.Global.Pkgsearch.NeedArch()
            );
            await this._botClient.SendTextMessageAsync(
                chatId: this._message.Chat.Id,
                text: text,
                replyToMessageId: this._message.MessageId,
                parseMode: ParseMode.Markdown
            );
        }

        public async Task doWhileIsValid()
        {
            Console.WriteLine(this._args_result.getIndex(0));
            ArchlinuxApi archlinuxctx = new ArchlinuxApi();
            PackageDetails pkgdetails = new PackageDetails(archlinuxctx, usefiles: false);
            PackageDetailAll res = await pkgdetails.Repository(ArchRepository.FromString(this._args_result.getIndex(0)))
                .Architecture(Arch.FromString(this._args_result.getIndex(1)))
                .Name(this._args_result.getIndex(2))
                .get();

            //PackageDetailAll res = await pkgdetails.Name("0ad").Repository(ArchRepository.FromString("community")).Architecture(Arch.FromString("x86_64")).get();

            // string text = TranslateLocale.CreateTranslation(
            //     this._message,
            //     new fadhil_robot.TranslationString.Global.Pkgsearch.NeedPkgName()
            // );

            string text = TranslateLocale.CreateTranslation(
                this._message,
                new fadhil_robot.TranslationString.Global.Pkgsearch.Success(),
                res.pkgname, res.pkgver, res.pkgdesc
            );
            await this._botClient.SendTextMessageAsync(
                chatId: this._message.Chat.Id,
                text: text,
                replyToMessageId: this._message.MessageId
            );
        }
    }
}