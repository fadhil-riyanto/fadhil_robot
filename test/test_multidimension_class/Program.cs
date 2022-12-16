using System;
using System.Text;


class something_class
{
    public void print_hey(string data)
    {
        return $"hey {data}";
    }
}
class main
{
    public static void Main()
    {
        something_class tsc = new(
            new[] {
                something_class
            }
        );
    }
}

// 