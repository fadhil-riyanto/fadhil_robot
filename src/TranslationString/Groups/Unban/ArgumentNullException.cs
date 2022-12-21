using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Unban
{
    class ArgumentNullException : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "you must reply to a user, or give me a username";
            }
        }

        public override string translate_id_ID {
            get {
                return "kamu harus membalas pesan penggunanya, atau berikan saya usernamenya";
            }
        }
    }
}