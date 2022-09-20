// SPDX-License-Identifier: GPL-2.0

/*
 *  main.c
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


namespace fadhil_robot.Utils
{
        public class Parse
        {
                private static char[] identifier = new char[2]{
                        '/', '!'
                };
                private bool isvalid_command;
                private string textraw, delete1char;

                public string command, value;

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

                                string[] at_split = this.delete1char.Split("@");


                                if (splitted.Length == 1)
                                {
                                        if (at_split.Length == 2)
                                        {
                                                if (at_split[1].ToLower() == Config.BotName.ToLower())
                                                {
                                                        this.command = at_split[0];
                                                } else {
                                                        this.command = null;
                                                }
                                                this.value = null;
                                        }
                                        else
                                        {
                                                this.command = this.delete1char;
                                                this.value = null;
                                        }
                                        // result.Add("command", this.delete1char);
                                        // result.Add("value", null);
                                }
                                else
                                {
                                        int i = this.delete1char.IndexOf(" ") + 1;
                                        string str = this.delete1char.Substring(i);

                                        if (at_split.Length == 2)
                                        {
                                                this.command = at_split[0];
                                                this.value = str;
                                        }
                                        else
                                        {
                                                this.command = splitted[0];
                                                this.value = str;
                                        }

                                        // result.Add("command", splitted[0]);
                                        // result.Add("value", str);
                                }
                                result.Add("command", this.command);
                                result.Add("value", this.value);

                                //Console.WriteLine(this.delete1char);

                        }
                        else
                        {
                                result.Add("command", null);
                                result.Add("value", null);
                        }
                        // result.Add("command", null);
                        // result.Add("value", null);

                        return result;
                }


        }
}