using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Pkgsearch 
{
    class NeedPkgName : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "please add package name, \nformat: /pkgsearch repository arch packagename\n\nie: ``` /pkgsearch core x86_64 linux```";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon tambahkan nama package nya, \nformat: /pkgsearch repositori arsitektur namapaket\n\ncontoh: ```/pkgsearch core x86_64 linux```";
            }
        }
    }
}