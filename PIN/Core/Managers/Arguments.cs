using System;
using System.Collections.Generic;
using System.Linq;
using PIN.Core.Packages;

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
                Language.FindTranslation(Value["lg"][0]);
            }

            if (Value.ContainsKey("install"))
            {
                foreach (string packageName in Value["install"])
                {
                    Chocolatey c = new Chocolatey(packageName);
                    c.StartDownload();
                }
            }

            if (Value.ContainsKey("update"))
            {
                foreach (var package in Value["update"].SelectMany(packagetoupdate => new Scanner().Scan().Where(package => package.Packagename == packagetoupdate)))
                {
                    package.Update(Valueless.Contains("forceupdate"));
                }
            }
        }

        public void Debug()
        {
            Console.WriteLine("Arguments without value");
            for (int index = 0; index < Valueless.Count; index++)
            {
                string s = Valueless[index];
                Utils.WriteinColor(ConsoleColor.Gray, $"[{index}] {s}");
            }

            Console.WriteLine("\nArguments with value");
            foreach (var arg in Value)
            {
                Utils.WriteinColor(ConsoleColor.Gray, $"[{arg.Key}] {Utils.JoinArray(arg.Value.ToArray())}");
            }
            Console.WriteLine("\n");
        }

        private string GetName(string source)
        {
            string returnString = source.Replace("-", "");
            if (returnString.Contains("="))
                returnString = returnString.Remove(returnString.IndexOf("=", StringComparison.Ordinal));
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
