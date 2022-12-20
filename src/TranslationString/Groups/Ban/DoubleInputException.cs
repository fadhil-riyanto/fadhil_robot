using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Ban
{
    class DoubleInputException : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "please choose one of the following reply only or type the username";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon pilih salah satu antara membalas pesannya atau tulis username nya";
            }
        }
    }
}