// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using System.Threading;
using StackExchange.Redis;
using fadhil_robot.Utils;
using MongoDB.Driver;


namespace fadhil_robot.Program
{

        class fadhil_robot
        {
                private static bool wantExit = true;
                public static ConnectionMultiplexer redisconn { get; set; }
                public static MongoClient mongoClientConn { get; set; }
                public static async Task Main()
                {
                        try
                        {
                                redisconn = ConnectionMultiplexer.Connect(Config.RedisHost);
                        }
                        catch (StackExchange.Redis.RedisConnectionException)
                        {
                                new ConsoleLogError("Redis error while trying to connect");
                                System.Environment.Exit(15);
                        }
                        mongoClientConn = new MongoClient("mongodb://localhost:27017");


                        Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
                        {
                                e.Cancel = true;
                                fadhil_robot.wantExit = false;
                        };


                        TelegramBotClient Bot = new TelegramBotClient(Config.Token);
                        
                        User me = await Bot.GetMeAsync();
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

                        Console.WriteLine($"bot running : @{me.Username}");
                        while(fadhil_robot.wantExit) { 
                                Thread.Sleep(1000);
                        }
                        stop_bot.Cancel();
                        Console.WriteLine($"bot exited : @{me.Username}");
                }
                public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
                {
                        var ErrorMessage = exception switch
                        {
                                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                                _ => exception.ToString()
                        };

                        Console.WriteLine(ErrorMessage);
                        // Thread.Sleep(2);
                        return Task.CompletedTask;
                }


                public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
                {

                        HandleUpdate h = new HandleUpdate();
                        main_thread_ctx main_ctx = new main_thread_ctx();

                        StackExchange.Redis.IDatabase db = redisconn.GetDatabase();
                        main_ctx.redis = db;
                        main_ctx.mongodbCtx = mongoClientConn;

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



                private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
                {
                        Console.WriteLine($"Tipe update tidak dikenali : {update.Type}");
                        return Task.CompletedTask;

                }
        }
}