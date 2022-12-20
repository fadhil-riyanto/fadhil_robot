using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.System 
{
    class NotEnoughPermission : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "I don't have enough rights to manage group settings in the chat, make sure i have enough permission for control this group";
            }
        }

        public override string translate_id_ID {
            get {
                return "saya tidak mempunyai cukup hak untuk mengelola pengaturan grup didalam chat, cek lagi bahwa saya punya kendali penuh atas grup ini";
            }
        }
    }
}