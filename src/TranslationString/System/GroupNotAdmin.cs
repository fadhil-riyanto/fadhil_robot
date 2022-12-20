using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.System 
{
    class GroupNotAdmin : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "Sorry, you're not admin on this group";
            }
        }

        public override string translate_id_ID {
            get {
                return "Maaf, kamu bukan admin di grup ini";
            }
        }
    }
}