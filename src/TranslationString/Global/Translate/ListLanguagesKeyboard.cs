using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class ListLanguagesKeyboard : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "supported languages";
            }
        }

        public override string translate_id_ID {
            get {
                return "bahasa yang didukung";
            }
        }
    }
}