using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Pkgsearch 
{
    class Success : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "{0} ({1})\n\nDesc: {2}\nLast update: {3})";
            }
        }

        public override string translate_id_ID {
            get {
                return "{0} ({1})\n\nDeskripsi: {2}\nTerakhir update: {3}) ";
            }
        }
    }
}