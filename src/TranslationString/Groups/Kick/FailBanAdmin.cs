using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Kick
{
    class FailBanAdmin : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "can't kicked because its an admin";
            }
        }

        public override string translate_id_ID {
            get {
                return "tidak bisa memkick, karna dia admin";
            }
        }
    }
}