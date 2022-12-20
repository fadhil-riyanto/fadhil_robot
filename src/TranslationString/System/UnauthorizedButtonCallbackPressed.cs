using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.System 
{
    class UnauthorizedButtonCallbackPressed : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "Sorry, this button not for you";
            }
        }

        public override string translate_id_ID {
            get {
                return "Maaf, tombol ini bukan untuk kamu";
            }
        }
    }
}