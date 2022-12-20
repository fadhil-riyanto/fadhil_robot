using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Lookup 
{
    class NeedArguments : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "please give me the username";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon berikan username";
            }
        }
    }
}