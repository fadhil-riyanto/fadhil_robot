using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Lookup 
{
    class UsernameInvalid : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "Invalid username!, please use valid telegram username format!";
            }
        }

        public override string translate_id_ID {
            get {
                return "Username tidak valid!, mohon gunakan format username telegram yang valid!";
            }
        }
    }
}