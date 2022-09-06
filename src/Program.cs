﻿using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using System.Threading;


namespace Prtscbot.Program
{
        class Prtscbot
        {
                public static async Task Main()
                {
                        TelegramBotClient Bot = new TelegramBotClient(Config.Token);
                        User me = await Bot.GetMeAsync();
                        Console.Title = "Fadhil Riyanto Bot";
                        using var stop_bot = new CancellationTokenSource();
                        ReceiverOptions receiver_req = new() { 
                                AllowedUpdates = { } 
                        };
                        Bot.StartReceiving(
                                HandleUpdateAsync,
                                HandleErrorAsync,
                                receiver_req,
                                stop_bot.Token
                        );

                        Console.WriteLine($"Bot Berjalan : @{me.Username}");
                        while (true) { 
                                Thread.Sleep(1000); 
                        }
                }
                public static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
                {
                        var ErrorMessage = exception switch
                        {
                                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                                _ => exception.ToString()
                        };

                        Console.WriteLine(ErrorMessage);
                        Thread.Sleep(2);
                        return Task.CompletedTask;
                }

                public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
                {
                        var handler = update.Type switch
                        {
                                UpdateType.Message => HandleUpdate.Handle(botClient, update.Message!),
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