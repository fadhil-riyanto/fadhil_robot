using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Unpin
{
    class NeedReply : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "You must reply to the message you want to unpin!";
            }
        }

        public override string translate_id_ID {
            get {
                return "Anda harus membalas pesan yang ingin diunpin!";
            }
        }
    }
}