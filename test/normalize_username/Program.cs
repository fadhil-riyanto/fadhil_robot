using System;
class normalize
{
    public static string normalizing(string username)
    {
        if (username[0] == '@')
        {
            return username.Remove(0, 1);
        } else {
            return username;
        }
    }
    
}

class mainku
{
    public static void Main()
    {
        Console.WriteLine(normalize.normalizing("nameku"));
    }
    
}


