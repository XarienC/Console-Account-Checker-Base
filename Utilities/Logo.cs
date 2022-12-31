using System;
using System.Drawing;
using Console = Colorful.Console;

namespace ConsoleCheckerBase.Utilities
{
    internal class Logo
    {
        private static readonly Color TopColour = Color.FromArgb(246, 120, 106);
        private static readonly Color HigherTopColour = Color.FromArgb(239, 150, 120);
        private static readonly Color MiddleTopColour = Color.FromArgb(249, 153, 102);
        private static readonly Color MiddleBottomColour = Color.FromArgb(241, 183, 172);
        private static readonly Color LowerBottomColour = Color.FromArgb(251, 211, 211);
        private static readonly Color BottomColour = Color.FromArgb(252, 225, 225);

        // To fix unrecognised escape sequence, simple put a "\" before any error regarding logo
        private static readonly string LogoTop = $"   _____ _               _             ";
        private static readonly string LogoHigherTop = $"  / ____| |             | |            ";
        private static readonly string LogoMiddleTop = $" | |    | |__   ___  ___| | _____ _ __ ";
        private static readonly string LogoMiddleBottom = $" | |    | '_ \\ / _ \\/ __| |/ / _ \\ '__|";
        private static readonly string LogoLowerBottom = $" | |____| | | |  __/ (__|   <  __/ |   ";
        private static readonly string LogoBottom = $"  \\_____|_| |_|\\___|\\___|_|\\_\\___|_|   ";
        private static readonly string Gap = "";
        private static readonly string Credits = " { Console Checker Base - Xarien } ";

        public static void Load()
        {
            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - LogoTop.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLine(LogoTop, TopColour);

            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - LogoHigherTop.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLine(LogoHigherTop, HigherTopColour);

            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - LogoMiddleTop.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLine(LogoMiddleTop, MiddleTopColour);

            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - LogoMiddleBottom.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLine(LogoMiddleBottom, MiddleBottomColour);

            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - LogoLowerBottom.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLine(LogoLowerBottom, LowerBottomColour);

            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - LogoBottom.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLine(LogoBottom, BottomColour);

            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - Gap.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLine(Gap, BottomColour);

            Console.SetCursorPosition((int)Math.Round((Console.WindowWidth - Credits.Length) / 2.0),
                Console.CursorTop);
            Console.WriteLineFormatted(Credits, BottomColour);

            Console.WriteLine();
        }
    }
}
