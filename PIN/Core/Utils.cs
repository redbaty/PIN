using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using Newtonsoft.Json;
using PIN.Core.Packages;

namespace PIN.Core
{
    class Utils
    {

        #region Plataform Stuff

        static bool is64BitProcess = (IntPtr.Size == 8);
        static bool is64BitOperatingSystem = is64BitProcess || InternalCheckIsWow64();

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process(
            [In] IntPtr hProcess,
            [Out] out bool wow64Process
        );

        public static bool InternalCheckIsWow64()
        {
            if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
                Environment.OSVersion.Version.Major >= 6)
            {
                using (Process p = Process.GetCurrentProcess())
                {
                    bool retVal;
                    if (!IsWow64Process(p.Handle, out retVal))
                    {
                        return false;
                    }
                    return retVal;
                }
            }
            else
            {
                return false;
            }
        }
        
        #endregion

        public static void WriteVersion()
        {
            Version version = typeof (Program).Assembly.GetName().Version;
            WriteinColor(ConsoleColor.Green, string.Format("PIN {0}", version));
            WriteinColor(ConsoleColor.Gray, "Help us at github : https://github.com/redbaty/PIN/\n");
        }

        /// <summary>
        /// Writes in a certain color.
        /// </summary>
        /// <param name="Colortowrite">The color which will be used.</param>
        /// <param name="text">The text to be written</param>
        public static void WriteinColor(ConsoleColor Colortowrite, string text)
        {
            var oldConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = Colortowrite;
            Console.WriteLine(text);
            Console.ForegroundColor = oldConsoleColor;
        }

        /// <summary>
        /// Writes in the "info" format.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void WriteInfo(string text)
        {
            var oldConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("[PIN] {0}",text);
            Console.ForegroundColor = oldConsoleColor;
        }

        /// <summary>
        /// Writes in the "error" format.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void WriteError(string text)
        {
            var oldConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[PIN] {0}", text);
            Console.ForegroundColor = oldConsoleColor;
        }

        /// <summary>
        /// Joins the array into a single string.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public static string JoinArray(string[] array)
        {
            string result = "";
            for (var index = 0; index < array.Length; index++)
            {
                var s = array[index];
                if (index < 2)
                    result = result + s + " ";
                else
                    result = result + " " + s;       
            }
            return result;
        }

        /// <summary>
        /// Removes the specified remove list from the source list.
        /// </summary>
        /// <param name="SourceList">The source list.</param>
        /// <param name="RemoveList">The list to remove.</param>
        /// <returns></returns>
        public static List<IAP> Remove(List<IAP> SourceList,List<IAP> RemoveList)
        {
            return SourceList.Except(RemoveList).ToList();
        }

        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string GetPowershellValue(string valuename, string source)
        {
            source = source.Substring(source.IndexOf(valuename)).Split('\n')[0];
            if (source.Contains("'"))
                return source.Substring(source.IndexOf("'")).Replace("'", "");
            else
                return source.Substring(source.IndexOf("\"")).Replace("\"", "");
        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        static double ConvertMegabytesToGigabytes(double megabytes) // SMALLER
        {
            // 1024 megabyte in a gigabyte
            return megabytes / 1024.0;
        }

        static double ConvertMegabytesToTerabytes(double megabytes) // SMALLER
        {
            // 1024 * 1024 megabytes in a terabyte
            return megabytes / (1024.0 * 1024.0);
        }

        static double ConvertGigabytesToMegabytes(double gigabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes * 1024.0;
        }

        static double ConvertGigabytesToTerabytes(double gigabytes) // SMALLER
        {
            // 1024 gigabytes in a terabyte
            return gigabytes / 1024.0;
        }

        static double ConvertTerabytesToMegabytes(double terabytes) // BIGGER
        {
            // 1024 * 1024 megabytes in a terabyte
            return terabytes * (1024.0 * 1024.0);
        }

        static double ConvertTerabytesToGigabytes(double terabytes) // BIGGER
        {
            // 1024 gigabytes in a terabyte
            return terabytes * 1024.0;
        }

        public class Converter
        {
            /// <summary>
            /// Convert xml content to json.
            /// </summary>
            /// <param name="content">The xml content.</param>
            /// <returns></returns>
            public static string XmlToJson(string content)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                return JsonConvert.SerializeXmlNode(doc);
            }
        }
    }
}