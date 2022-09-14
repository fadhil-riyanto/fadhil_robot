class makeNegative{
        private long data;
        public makeNegative(long data)
        {
                this.data = data;
        }

        public long get()
        {
                return this.data;
        }
}


class main
{
        public static void Main()
        {
                // makeNegative[] list = new makeNegative[];
                // list[0] = new makeNegative(-1001410961692);

                // foreach(makeNegative lists in list)
                // {
                //         Console.WriteLine(lists.get());
                // }

                List<makeNegative> lists = new List<makeNegative>();
                lists.Add(new makeNegative(-1001410961692));

                foreach(makeNegative listall in lists)
                {
                        Console.WriteLine(listall.get());
                }

        }
}