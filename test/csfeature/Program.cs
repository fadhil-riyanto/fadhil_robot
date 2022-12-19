using System;

class parentme {
    protected int _x, _y;
    public parentme(int x, int y)
    {
        this._x = x;
        this._y = y;
    }
    public virtual int add_y_x()
    {
        return _x + _y;
    }
}
class anaksatu : parentme
{
    public anaksatu(int x, int y) : base(x, y)
    {

    }
    public override int add_y_x()
    {
        return _x + _y;
    }
}
class anakdua : parentme
{
    public anakdua(int x, int y) : base(x + x, y + y)
    {

    }

    public override int add_y_x()
    {
        return _x + _y;
    }
}

class mainku
{
    public static void Main()
    {
        parentme data = new anakdua(8, 9);
        Console.WriteLine(data.add_y_x());
    }
    
}


