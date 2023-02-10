using System;
class args_parse
{
    private string _text;
    private int getter_total;
    private string[] _data;
    private string[] _data_val;
    public args_parse(string text, int getter_total = 0)
    {
        this._text = text;
        this.getter_total = getter_total;
        this._data_val = this.split();
        this._data = this.split();
    }
    private string[] split()
    {
        return this._text.Split(" ");
    }

    public string getArg(int index)
    {
        if (index <= this.getter_total)
        {
            return this._data[index];

        }
        else
        {
            return null;
        }
    }
    public string getIndex(int index)
    {
        if (index <= this._data.Length)
        {

            return this._data[index];


        }
        else
        {
            return null;
        }
    }

#pragma warning disable CS8632
    public string? getValue()
    {
        string? tmp = null;
        for (int a = 0; a <= this.getter_total; a++)
        {
            this._data_val[a] = "";
        }
        for (int i = 0; i < this._data_val.Length; i++)
        {
            if (this._data_val[i] == "")
            {
                // skippable
            }
            else if (i == this._data_val.Length)
            {
                tmp = tmp + this._data_val[i];
            }
            else
            {
                tmp = tmp + this._data_val[i] + " ";
            }
        }
        return tmp;
    }
}
class mainku
{

    public static void Main()
    {
        args_parse data = new args_parse("id", 0);
        Console.WriteLine(data.getValue());
        Console.WriteLine(data.getArg(1));
        //Console.WriteLine(data.getIndex(4));

    }
}


