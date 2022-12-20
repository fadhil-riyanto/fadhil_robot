using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class ListLanguagesText : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "list of supported languages\n\n{0}";
            }
        }

        public override string translate_id_ID {
            get {
                return "daftar bahasa yang didukung\n\n{0}";
            }
        }
    }
}