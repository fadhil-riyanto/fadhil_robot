using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Groups.Pin
{
    class NeedReply : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "You must reply to the message that you want to pin!";
            }
        }

        public override string translate_id_ID {
            get {
                return "Anda harus membalas pesan yang ingin dipin!";
            }
        }
    }
}