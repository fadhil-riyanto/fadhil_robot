using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.System 
{
    class CacheExpire : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "Sorry, the cache has been expired in our server, please send that command again.. ";
            }
        }

        public override string translate_id_ID {
            get {
                return "Maaf, Data cache telah kadaluarsa diserver kami, silahkan kirim command tersebut lagi.. ";
            }
        }
    }
}