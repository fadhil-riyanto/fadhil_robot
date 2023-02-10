using System;

class compute
{
    public void predict()
    {
        double squareroot = (5 * Math.Sqrt(2)) / 2;
        Console.WriteLine(squareroot);
    }
}

class main
{
    public static void Main()
    {
        compute cm = new compute();
        cm.predict();
    }
}

