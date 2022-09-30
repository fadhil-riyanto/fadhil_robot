// SPDX-License-Identifier: GPL-2.0

/*
 *  Copyright (C) Fadhil Riyanto
 *
 *  https://github.com/fadhil-riyanto/fadhil_robot.git
 */


namespace fadhil_robot.Utils
{
        public class Parse
        {
                private char[] _identifier = {
                        '/', '!'
                };
                private bool _isvalid_command;
                private string _textraw, _delete1char;

                public string _command, _value;

                public Parse(string text)
                {
                        this._textraw = text;
                }

                private void CheckIsCommand()
                {
                        foreach (char identifier_l in this._identifier)
                        {
                                if (this._textraw[0] == identifier_l)
                                {
                                        this._isvalid_command = true;
                                }
                        }
                }

                public Dictionary<string, string> getResult()
                {
                        Dictionary<string, string> result = new Dictionary<string, string>();

                        this.CheckIsCommand();
                        if (this._isvalid_command)
                        {
                                this._delete1char = this._textraw.Remove(0, 1);
                                string[] splitted = this._delete1char.Split(" ");

                                string[] at_split = this._delete1char.Split("@");


                                if (splitted.Length == 1)
                                {
                                        if (at_split.Length == 2)
                                        {
                                                if (at_split[1].ToLower() == Config.BotName.ToLower())
                                                {
                                                        this._command = at_split[0];
                                                } else {
                                                        this._command = null;
                                                }
                                                this._value = null;
                                        }
                                        else
                                        {
                                                this._command = this._delete1char;
                                                this._value = null;
                                        }
                                        // result.Add("command", this.delete1char);
                                        // result.Add("value", null);
                                }
                                else
                                {
                                        int i = this._delete1char.IndexOf(" ") + 1;
                                        string str = this._delete1char.Substring(i);

                                        if (at_split.Length == 2)
                                        {
                                                string[] newstr = at_split[1].Split(" ");
                                                if (newstr[0].ToLower() == Config.BotName.ToLower())
                                                {
                                                        this._command = at_split[0];
                                                } else {
                                                        this._command = null;
                                                }
                                                this._value = str;
                                                // this._command = at_split[0];
                                                // this._value = str;
                                        }
                                        else
                                        {
                                                this._command = splitted[0];
                                                this._value = str;
                                        }

                                        // result.Add("command", splitted[0]);
                                        // result.Add("value", str);
                                }
                                result.Add("command", this._command);
                                result.Add("value", this._value);

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