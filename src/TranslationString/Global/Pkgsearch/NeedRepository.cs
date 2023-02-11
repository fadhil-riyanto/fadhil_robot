using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Pkgsearch 
{
    class NeedRepository : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "please add repository name, \nformat: /pkgsearch repository arch repositoryname\n\nie: ``` /pkgsearch core x86_64 linux```";
            }
        }

        public override string translate_id_ID {
            get {
                return "mohon tambahkan nama repositori nya, \nformat: /pkgsearch repositori repositori namapaket\n\ncontoh: ```/pkgsearch core x86_64 linux```";
            }
        }
    }
}