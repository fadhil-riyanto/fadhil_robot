using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Ban
{
    class FailBanAdmin : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "can't banned because its an admin";
            }
        }

        public override string translate_id_ID {
            get {
                return "tidak bisa membanned, karna dia admin";
            }
        }
    }
}