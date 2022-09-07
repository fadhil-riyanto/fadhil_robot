using System;
namespace Test.Tests
{
        public class Parse
        {
                private static char[] identifier = new char[2]{
                        '/', '!'
                };
                private bool isvalid_command;
                private string textraw, delete1char;

                public Parse(string text)
                {
                        this.textraw = text;
                }

                private void CheckIsCommand()
                {
                        foreach (char identifier_l in identifier)
                        {
                                if (this.textraw[0] == identifier_l)
                                {
                                        this.isvalid_command = true;
                                }
                        }
                }


                public Dictionary<string, string> getResult()
                {
                        Dictionary<string, string> result = new Dictionary<string, string>();

                        this.CheckIsCommand();
                        if (this.isvalid_command)
                        {
                                this.delete1char = this.textraw.Remove(0, 1);
                                string[] splitted = this.delete1char.Split(" ");

                                if (splitted.Length == 1)
                                {
                                        result.Add("command", this.delete1char);
                                        result.Add("value", null);
                                }
                                else
                                {
                                        int i = this.delete1char.IndexOf(" ") + 1;
                                        string str = this.delete1char.Substring(i);

                                        result.Add("command", splitted[0]);
                                        result.Add("value", str);
                                }

                        }
                        return result;
                }


        }
}