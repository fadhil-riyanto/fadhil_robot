using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class LanguageNotFound : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "The {0} languange are not found";
            }
        }

        public override string translate_id_ID {
            get {
                return "Bahasa {0} tidak ditemukan";
            }
        }
    }
}