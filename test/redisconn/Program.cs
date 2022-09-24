using StackExchange.Redis;
class ngetes {
        public ConnectionMultiplexer redisctx;
        public void makeconn() {
                this.redisctx = ConnectionMultiplexer.Connect("localhost");
        }
        public void set(string key, string value) {
                IDatabase db = this.redisctx.GetDatabase();
                db.StringSet(key, value);
        }
}
class redisconn {
        public static void Main() {
                ngetes ng = new ngetes();
                ng.makeconn();
                for(int a = 0; a < 100000; a++) {
                        ng.set("test" + a, "banh");
                }
        }
}