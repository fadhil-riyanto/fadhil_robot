using fadhil_robot.Utils;

namespace fadhil_robot.TranslationString.Global.Lookup 
{
    class Success : Utils.translate_string_parent{
        public override string translate_en_US {
            get {
                return "User info\n\n" +
                    "👦🏻 name: {0}\n" +
                    "🆔 id: {1}\n" +
                    "⁣🌐 languange: {2}\n" +
                    "👤 username: {3}\n" +
                    "🆔 id type: {4}\n";
            }
        }

        public override string translate_id_ID {
            get {
                return "Info pengguna\n\n" +
                    "👦🏻 nama: {0}\n" +
                    "🆔 id: {1}\n" +
                    "⁣🌐 bahasa: {2}\n" +
                    "👤 nama pengguna: {3}\n" +
                    "🆔 tipe id: {4}\n";
            }
        }
    }
}