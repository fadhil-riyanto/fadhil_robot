how to run

1. install dotnet (see https://wiki.archlinux.org/title/.NET)
2. clone this repository
3. edit config.csexample & rename extension
4. (Recommended), do not change server endpoint in config, build own telegram api server (more faster than remote telegram api). see https://github.com/tdlib/telegram-bot-api
    4.1 run the telegram api server
    4.1 make sure if your 8081 port is listening
5. instal redis, make sure your redis service is active (or you can change the settings on config redis connection string for remote host)

enjoy