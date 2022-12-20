using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Adminlist
{
    class OwnerNotFound : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "owner is hidden or unavailable";
            }
        }

        public override string translate_id_ID {
            get {
                return "owner disembunyikan, atau tidak tersedia";
            }
        }
    }
}