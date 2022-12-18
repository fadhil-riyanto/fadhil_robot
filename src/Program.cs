// SPDX-License-Identifier: GPL-2.0

/*
*  Copyright (C) 2022 Fadhil Riyanto
*
*  https://github.com/fadhil-riyanto/fadhil_robot.git
*/


using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using System.Threading;
using fadhil_robot.Utils;
using fadhil_robot.HandleUpdate;
using MongoDB.Driver;

using TL;
using StackExchange.Redis;


namespace fadhil_robot.Program
{

    class fadhil_robot
    {
        private static bool wantExit = true;
        public static WTelegram.Client ClientMT;
        public static MongoClient mongoClientConn { get; set; }
        public static ConnectionMultiplexer redisconn { get; set; }
        public static async Task Main()
        {

            redisconn = ConnectionMultiplexer.Connect(new ConfigurationOptions{
                EndPoints = {"127.0.0.1:6379"}                
            });

            mongoClientConn = new MongoClient("mongodb://localhost:27017");

            static string MTConfig(string what)
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

            ClientMT = new WTelegram.Client(MTConfig);

            WTelegram.Helpers.Log = (lvl, str) => { new ConsoleLogTL("(" + lvl + ") " + str); };
            await ClientMT.LoginUserIfNeeded();

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                fadhil_robot.wantExit = false;
            };


            TelegramBotClientOptions tgconf = new TelegramBotClientOptions(
                token: Config.Token,
                baseUrl: (Config.BASEURL_SERVER == null) ? "https://api.telegram.org" : Config.BASEURL_SERVER
            );
            TelegramBotClient Bot = new TelegramBotClient(tgconf);

            Telegram.Bot.Types.User me = await Bot.GetMeAsync();
            Console.Title = "fadhil_robot";
            using var stop_bot = new CancellationTokenSource();
            ReceiverOptions receiver_req = new()
            {
                AllowedUpdates = { }
            };

            Bot.StartReceiving(
            new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync
            ),
            receiver_req,
            stop_bot.Token
            );

            new ConsoleLogSys($"bot running : @{me.Username}");

            while (fadhil_robot.wantExit)
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
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };


            if (exception is RequestException)
            {
                Console.WriteLine("network error");
                Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }


        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {

            MainHandle h = new MainHandle();
            main_thread_ctx main_ctx = new main_thread_ctx();

            
            main_ctx.redis =  redisconn.GetDatabase();
            main_ctx.mongodbCtx = mongoClientConn;
            main_ctx.ClientMT = ClientMT;

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
