using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.System 
{
    class UnknownCommand : Utils.translate_string_parent{

        public override string translate_en_US {
            get {
                return "Sorry, the {0} command is not recognized";
            }
        }

        public override string translate_id_ID {
            get {
                return "Maaf, command {0} tidak dikenali";
            }
        }
    }
}