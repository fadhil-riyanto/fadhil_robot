using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Private.Help 
{
    class Success : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "Hi {0}, I'm Fadhil Robot, created by @fadhil_riyanto. this is a help menu if you don't know what I'm using, all commands work with / and !";
            }
        }

        public override string translate_id_ID {
            get {
                return "Hai {0}, aku Fadhil Robot.. ini adalah menu pertolongan jika anda tidak paham kegunaanku, semua command bekerja dengan / dan !";
            }
        }
    }
}