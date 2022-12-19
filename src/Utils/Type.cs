// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot.Types;

namespace fadhil_robot.Utils
{
    class InputTelegram
    {
        public string command;
        public string value;
        public int messange_id { get; set; }
        public long chat_id { get; set; }
        public long user_id { get; set; }
        public string languange { get; set; }
        public CancellationToken cancellationToken;
        public main_thread_ctx main_thread_ctx;
        public Dictionary<string, string> data { get; set; }
        public CallbackQuery callback { get; set; }

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
}