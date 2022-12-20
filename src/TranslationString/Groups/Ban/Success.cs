using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Ban
{
    class Success : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "[{0}] banned!";
            }
        }

        public override string translate_id_ID {
            get {
                return "[{0}] dibanned!";
            }
        }
    }
}