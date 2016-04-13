using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PIN.Core.jModels;

namespace PIN.Core.Managers
{
    class Arguments
    {
        public List<string> Valueless = new List<string>();
        public Dictionary<string, List<string>> Value = new Dictionary<string, List<string>>();  

        public Arguments(string[] arguments)
        {
            foreach (string argument in arguments)
            {
                if(!argument.Contains("="))
                    Valueless.Add(GetName(argument));
                else if(argument.Contains("="))
                    Value.Add(GetName(argument),GetValue(argument));
            }

            if (Value.ContainsKey("lg"))
            {
                var x = Value["lg"];
                var result = new Scanner().Find(x[0] + ".tap");
                Translation.LoadTranslation(result[0]);
            }
        }

        public void Debug()
        {
            Console.WriteLine("Arguments without value");
            for (int index = 0; index < Valueless.Count; index++)
            {
                string s = Valueless[index];
                Utils.WriteinColor(ConsoleColor.Gray,string.Format("[{0}] {1}", index ,s));
            }

            Console.WriteLine("\nArguments with value");
            foreach (var arg in Value)
            {
                Utils.WriteinColor(ConsoleColor.Gray,string.Format("[{0}] {1}", arg.Key, Utils.JoinArray(arg.Value.ToArray())));
            }
            Console.WriteLine("\n");
        }

        private string GetName(string source)
        {
            string returnString = source.Replace("-", "");
            if (returnString.Contains("="))
                returnString = returnString.Remove(returnString.IndexOf("="));
            return returnString;
        }


        private List<string> GetValue(string source)
        {
            string returnString = source;
            if (returnString.Contains("="))
                returnString = returnString.Substring(returnString.IndexOf("=")+1);
            return returnString.Split(';').ToList();
        }
    }
}
