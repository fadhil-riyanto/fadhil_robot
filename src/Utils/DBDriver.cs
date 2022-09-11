using MongoDB.Driver;

namespace Prtscbot.Utils
{
        class DBDriverContext
        {
                public MongoClient dbctx {get; set;}
                public MongoClient dbSelected {get; set;}
        }
        internal class DBDriver
        {
                private DBDriverContext _ctx = new DBDriverContext();
                public DBDriverContext MakeConnection(string connstring)
                {
                        _ctx.dbctx = new MongoClient(connstring);
                        return _ctx;
                }
        }
}
