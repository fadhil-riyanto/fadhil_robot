using System;

abstract class parent
{
    public abstract string translate {get; }
}


class CacheExpire : parent
{
    public override string translate {
        get {
            return "Sorry, the cache has been expired in our server, please send that command again.. ";
        }
    }

}
class TranslateLocale
{
    public static void prints(parent str)
    {
        Console.WriteLine(str.translate);
    }
}

class mainku
{

    public static void Main()
    {
        TranslateLocale.prints(new CacheExpire());

    }
}



