// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot.Types;

namespace fadhil_robot.Utils
{
    abstract class TranslationStringParent
    {
        public abstract string translate_id_ID { get; }
        public abstract string translate_en_US { get; }
    }

    enum InputTelegramState {
        Callback, 
        Messange
    }
    class InputTelegramParent
    {
        public virtual string command {get; set;}
        public virtual string value {get; set;}
        public virtual CancellationToken cancellationToken {get; set;}
        public virtual main_thread_ctx main_thread_ctx {get; set;}
        public virtual long chat_id {get; set;}
        public virtual int messange_id {get; set;}
        public virtual string languange {get; set;}
        public virtual long user_id {get; set;}
        public virtual Dictionary<string, string> data { get; set; }
        public virtual CallbackQuery callback { get; set; }
        public virtual Telegram.Bot.Types.Message message { get; set; }
        public virtual InputTelegramState IncomingState { get; set; }

    }

 
    class InputTelegram : InputTelegramParent
    {
        public override string command { get; set; }
        public override string value { get; set; }
        public override CancellationToken cancellationToken { get; set; }
        public override main_thread_ctx main_thread_ctx { get; set; }
        public override Telegram.Bot.Types.Message message { get; set; }
        public override InputTelegramState IncomingState { get; set; }
    }

    class InputTelegramCallback : InputTelegramParent
    {
        public override string command { get; set; }
        public override string value { get; set; }
        public override long user_id { get; set; }
        public override long chat_id { get; set; }
        public override int messange_id { get; set; }
        public override string languange { get; set; }
        public override CancellationToken cancellationToken { get; set; }
        public override main_thread_ctx main_thread_ctx { get; set; }
        public override Dictionary<string, string> data { get; set; }
        public override CallbackQuery callback { get; set; }
        public override InputTelegramState IncomingState { get; set; }
    }

    class main_thread_ctx
    {
        public WTelegram.Client MtprotoClient { get; set; }
        public StackExchange.Redis.IDatabase redis { get; set; }

    }

    interface IExecutor
    {
        public Task Execute();
        public bool is_real_command();
    }

    interface IExecutor_cb
    {
        public Task Execute();
    }

    interface ITgKeyboard
    {
        TGKeyboardHelpMenu detectLanguangeMainButton();
        TGKeyboardHelpMenu detectLanguangeBackButton();
        Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup get();
        string getContent(string keys);
        const char NL = '\n';
        const string DOUBLE_NL = "\n\n";
    }

    enum command_executed_at
    {
        private_chat,
        group_chat,
        ignore
    }
    
    enum BlacklistAction
    {
        Ban,
        Kick,
        
    }
    class BlockedListsWord
    {
        public string Word {get; set;}
        public BlacklistAction WhenInvoked {get; set;}
    }

    class AdminSettingsLiteDB
    {
        public long ChatId {get; set;}      // identifier
        public bool AdminError {get; set;}  // raise when non admin invoke admin command
        public BlockedListsWord[] BlockedListsWord {get; set;}

    }
}