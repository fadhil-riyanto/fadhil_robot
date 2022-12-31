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
using Archlinux.Api;
using Archlinux.Api.Exception;
using Archlinux.Api.Methods;
using Archlinux.Api.Types.Result;
using Archlinux.Api.Types;
using Newtonsoft.Json;


namespace fadhil_robot.Commands.Global.Executor
{
    class PkgsearchUtils
    {
        private ArchlinuxApi archlinuxctx = new ArchlinuxApi();
        private string pkgname;
        private Archlinux.Api.Types.ArchRepository repo;
        private InputTelegram _inputTelegram;
        public PkgsearchUtils(string pkgname, Archlinux.Api.Types.ArchRepository repo, InputTelegram inputTelegram)
        {
            this.pkgname = pkgname;
            this.repo = repo;
            this._inputTelegram = inputTelegram;
        }

        private async Task<PackageSearchResult> getdata(int page = 1)
        {
            PackageSearch pkgsearch = new PackageSearch(archlinuxctx);
            return await pkgsearch.Query(this.pkgname).Repository(this.repo).get(page);
        }

        public async Task<(PackageSearchResult, string)> GetResultIndexOf(int page)
        {
            PackageSearchResult res = await this.getdata(page);
            string key = this.genCache(JsonConvert.SerializeObject(res), page);
            return (res, key);
        }

        private string genCache(string serealized, int page = 1)
        {
            return CallbackHelper.pack(this._inputTelegram, "pkgsearch_cb", new Dictionary<string, string>{
                { "page", page.ToString() },
                { "data", serealized }
            });
        }
    }
    
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
                // handle if that is null and can't next operate
                string text = TranslateLocale.CreateTranslation(
                    this._message,
                    new fadhil_robot.TranslationString.Global.Pkgsearch.NeedArguments()
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
                this._args_result = args_result;
                if (args_result.getValue() == null)
                {
                    await this.doWhileIsInvalid();
                } else {
                    await this.doWhileIsValid();
                }

                
            }

        }

        public async Task doWhileIsInvalid()
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

        public async Task doWhileIsValid()
        {
            PkgsearchUtils search = new PkgsearchUtils(
                this._args_result.getValue(), 
                Archlinux.Api.Types.ArchRepository.FromString(this._args_result.getArg(0)),
                this._inputTelegram);


            (PackageSearchResult aaa, string key) = await search.GetResultIndexOf(1);

            string debug = $"key: {key}\n\ndata: {aaa.results[0].pkgdesc}";
            await this._botClient.SendTextMessageAsync(
                chatId: this._message.Chat.Id,
                text: debug,
                replyToMessageId: this._message.MessageId
            );
        }
    }
}