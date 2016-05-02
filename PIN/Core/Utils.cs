using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Xml;
using Newtonsoft.Json;
using PIN.Core.Packages;
using static PIN.Core.Language;

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
            WriteinColor(ConsoleColor.Green, $"PIN {version}");
            WriteinColor(ConsoleColor.Gray, string.Format(CurrentLanguage.GithubInformation, "https://github.com/redbaty/PIN/") + "\n");
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
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string GetPowershellValue(string valuename, string source)
        {
            source = source.Substring(source.IndexOf(valuename, StringComparison.Ordinal)).Split('\n')[0];

            return source.Contains("'") ? source.Substring(source.IndexOf("'", StringComparison.Ordinal)).Replace("'", "") : source.Substring(source.IndexOf("\"", StringComparison.Ordinal)).Replace("\"", "");
        }

        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }

        public static int GetDownloadSize(string url)
        {
            WebRequest req = WebRequest.Create(url);
            req.Method = "HEAD";
            using (WebResponse resp = req.GetResponse())
            {
                int contentLength;
                if (int.TryParse(resp.Headers.Get("Content-Length"), out contentLength))
                {
                    return contentLength;
                }
            }

            return -1;
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