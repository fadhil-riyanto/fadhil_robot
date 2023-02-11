// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using fadhil_robot.Utils;
using fadhil_robot.HandleUpdate;
using StackExchange.Redis;


namespace fadhil_robot.Program
{

    class fadhil_robot
    {
        private static bool _wantExit = true;
        public static WTelegram.Client MtprotoClient;
        public static ConnectionMultiplexer redisconn { get; set; }
        public static async Task Main()
        {

            /* 
             * WTelegramBot config
            */
            static string MtprotoConfig(string what)
            {
                switch (what)
                {
                    case "api_id": return Config.API_ID;
                    case "api_hash": return Config.API_HASH;
                    case "phone_number": return Config.PHONE_NUMBER;
                    case "verification_code": Console.Write("Input your code: "); return Console.ReadLine();

                    default: return null;
                }
            }

            /* 
             * start redis
             */
            redisconn = ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { "127.0.0.1:6379" }
            });

            /* 
             * Start MTProto session
             */
            MtprotoClient = new WTelegram.Client(MtprotoConfig);
            await MtprotoClient.LoginUserIfNeeded();
            WTelegram.Helpers.Log = (lvl, str) => { new ConsoleLogTL("(" + lvl + ") " + str); };

            /* 
             * start telegram bot
             */

            TelegramBotClientOptions tgconf = new TelegramBotClientOptions(
                token: Config.Token,
                baseUrl: (Config.BASEURL_SERVER == null) ? "https://api.telegram.org" : Config.BASEURL_SERVER
            );
            TelegramBotClient Bot = new TelegramBotClient(tgconf);
            ReceiverOptions receiver_req = new()
            {
                AllowedUpdates = { }
            };
            using var stop_bot = new CancellationTokenSource();

            Bot.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                receiver_req,
                stop_bot.Token
            );

            Telegram.Bot.Types.User me = await Bot.GetMeAsync();
            new ConsoleLogSys($"bot running : @{me.Username}");

            /*
             * detect ctrl + c press
             */

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                fadhil_robot._wantExit = false;
            };

            while (fadhil_robot._wantExit)
            {
                Thread.Sleep(1000);
            }
            stop_bot.Cancel();
            new ConsoleLogSysLn($"bot exited : @{me.Username}");
        }
        public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"[{apiRequestException.ErrorCode}] : {apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);


            // if (exception is RequestException RequestException)
            // {
            //     new ConsoleLogError(RequestException.Message);
            //     Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            // }
            // else if (exception is ApiRequestException apiRequestException)
            // {
            //     new ConsoleLogError($"[{apiRequestException.ErrorCode}] : {apiRequestException.Message}");
            // } else 
            // {
            //     new ConsoleLogError($"[{apiRequestException.ErrorCode}] : {apiRequestException.Message}");
            // }
            return Task.CompletedTask;
        }


        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {

            MainHandle h = new MainHandle();
            main_thread_ctx main_ctx = new main_thread_ctx();


            main_ctx.redis = redisconn.GetDatabase();
            main_ctx.MtprotoClient = MtprotoClient;

            var handler = update.Type switch
            {
                UpdateType.Message => h.HandleMessange(botClient, update.Message!, cancellationToken, main_ctx),
                UpdateType.CallbackQuery => h.HandleCallbackQuery(botClient, update.CallbackQuery, cancellationToken, main_ctx),
                _ => UnknownUpdateHandlerAsync(botClient, update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, cancellationToken);
            }

        }

        private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update)
        {
            Console.WriteLine($"[error] unknown update type : {update.Type}");
            return Task.CompletedTask;

        }
    }
}
