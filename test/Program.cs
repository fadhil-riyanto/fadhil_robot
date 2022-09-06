using Test.Tests;
public class Program
{
	public static void Main(){
                Parse ps = new Parse("!note test ini value sir'");
                
                Console.WriteLine("command: " + ps.getResult()["command"]);
                Console.WriteLine("value: " + ps.getResult()["value"]);
        }
}



// public class Customer
// {
//     public int CustomerId { get; set; }
//     public void meth(int id)
//     {
//         CustomerId = id;
//     }
// }
// public class Program
// {
// 	public static void Main()
// 	{
// 		Customer cs = new Customer();
// 		cs.meth(8);
// 		Console.WriteLine(cs.CustomerId);
// 	}
// }