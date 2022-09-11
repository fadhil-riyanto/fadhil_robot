using MongoDB.Driver;
using Telegram.Bot;

namespace fadhil_robot.Utils {
        class InputTelegram
        {
                public string command;
                public string value;
                public CancellationToken cancellationToken;

        }
}