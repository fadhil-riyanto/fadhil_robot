using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Pkgsearch 
{
    class NeedPkgName : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "please add package name, ie: ``` /pkgsearch core linux```";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon tambahkan nama package nya, contoh: ```/pkgsearch core linux```";
            }
        }
    }
}