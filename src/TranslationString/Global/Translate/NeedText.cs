using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class NeedText : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "please insert the text, use format /tr (language code) (your text)";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon masukkan teksnya, gunakan format /tr (kode bahasa) (teksnya)";
            }
        }
    }
}