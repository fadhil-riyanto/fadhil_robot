using MongoDB.Driver;
using Telegram.Bot.Types;
using Telegram.Bot;
using fadhil_robot.Utils;

namespace fadhil_robot.Utils
{
        class AdminCheck
        {
                private long user_Id;
                private MongoClient client;
                private Message message;
                private ITelegramBotClient botClient;
                private InputTelegram inputTelegram;

                public AdminCheck(InputTelegram inputTelegram, ITelegramBotClient botClient, Message message)
                {
                        this.message = message;
                        this.botClient = botClient;
                        this.inputTelegram = inputTelegram;
                }

                private MongoClient makeConn(string connstring, string dbName)
                {
                        MongoClient ctx = new MongoClient("mongodb://localhost:27017");
                        ctx.GetDatabase(dbName);
                        return ctx;
                }

                // public async Task isAdmin(long user_id)
                // {


                //         await this.botClient.SendTextMessageAsync(
                //                         chatId: this.message.Chat.Id,
                //                         text: data.ToString()
                //                 );
                // }

                private async Task makeCache()
                {
                        Telegram.Bot.Types.ChatMember[] chatmember = await this.botClient.GetChatAdministratorsAsync(
                                chatId: this.message.Chat.Id,
                                cancellationToken: this.inputTelegram.cancellationToken
                        );

                        foreach (Telegram.Bot.Types.ChatMember members in chatmember)
                        {
                                Console.WriteLine(members.User.FirstName);
                        }
                }

                // public bool isAdmin()
                // {

                // }

        }
}