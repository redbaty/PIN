﻿using System;

namespace PIN.Core.ConsoleUtils
{
    static class ConsoleLine
    {
        public static void Write(int line, string text)
        {
            Console.SetCursorPosition(0, line);
            ClearCurrentConsoleLine();
            Console.WriteLine(text);
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }


}
