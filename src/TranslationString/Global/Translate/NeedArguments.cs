using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Translate 
{
    class NeedArguments : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "please write messages from other languages for translating into the intended language";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon tulis pesan dari bahasa lain untuk menterjemahkannya kedalam bahasa yang dituju";
            }
        }
    }
}