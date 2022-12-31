using System;
using ConsoleCheckerBase.Utilities;
using System.Collections.Generic;
using System.Drawing;
using Console = Colorful.Console;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckerBase.Checker
{
    internal class Setup
    {
        public static void Start()
        {
            Console.Clear();
            Console.WriteLine();

            Logo.Load();
            Console.Title = "[Console Checker Base] - Xarien";
            Console.WriteLine();

            Console.Write("    > [", Color.GhostWhite);
            Console.Write("+", Color.FromArgb(246, 120, 106));
            Console.Write("] How many ", Color.GhostWhite);
            Console.Write("threads ", Color.FromArgb(246, 120, 106));
            Console.Write("would you like to use? ", Color.GhostWhite);
            try
            {
                Variables.Threads = int.Parse(Console.ReadLine());
            }
            catch
            {
                Variables.Threads = 100;
            }

            for (; ; )
            {
                Console.Write("\n    > [", Color.GhostWhite);
                Console.Write("+", Color.FromArgb(246, 120, 106));
                Console.Write("] Select Proxy Type: ", Color.GhostWhite);
                Console.Write("[HTTP, SOCKS4, SOCKS5]: ", Color.FromArgb(246, 120, 106));
                Variables.ProxyType = Console.ReadLine();
                Variables.ProxyType = Variables.ProxyType.ToUpper();
                if (Variables.ProxyType == "HTTP" || Variables.ProxyType == "SOCKS4" || Variables.ProxyType == "SOCKS5")
                {
                    break;
                }
                Console.Write("    > [", Color.GhostWhite);
                Console.Write("+", Color.FromArgb(255, 0, 0));
                Console.Write("] Please choose a valid proxy type. \n ", Color.GhostWhite);
                Thread.Sleep(2000);
            }

            Console.WriteLine();
            string fileName;
            OpenFileDialog openfile = new OpenFileDialog();

            do
            {
                Console.Write("    > [", Color.GhostWhite);
                Console.Write("+", Color.FromArgb(246, 120, 106));
                Console.Write("] Load your Combo List: ", Color.GhostWhite);
                openfile.Title = "Load Combolist";
                openfile.Filter = "Text Files |*.txt";
                openfile.RestoreDirectory = true;
                openfile.ShowDialog();
                fileName = openfile.FileName;
                Variables.GetComboName = fileName;
                Console.Write(fileName, Color.GhostWhite);
            }
            while (!File.Exists(fileName));

            Variables.Combos = new List<string>(File.ReadAllLines(fileName));
            FileManager.ImportCombos(fileName);

            Console.Write(" > ", Color.FromArgb(246, 120, 106));
            Console.Write(Variables.Total, Color.GhostWhite);
            Console.WriteLine(" Combos Added\n", Color.FromArgb(246, 120, 106));

            do
            {
                Console.Write("    > [", Color.GhostWhite);
                Console.Write("+", Color.FromArgb(246, 120, 106));
                Console.Write("] Load your Proxy List: ", Color.GhostWhite);
                openfile.Title = "Load Proxies";
                openfile.Filter = "Text Files |*.txt";
                openfile.RestoreDirectory = true;
                openfile.ShowDialog();
                fileName = openfile.FileName;
                Console.Write(fileName, Color.GhostWhite);
            }
            while (!File.Exists(fileName));

            Variables.Proxies = new List<string>(File.ReadAllLines(fileName));
            FileManager.ImportProxies(fileName);

            Console.Write(" > ", Color.FromArgb(246, 120, 106));
            Console.Write(Variables.ProxyTotal, Color.White);
            Console.WriteLine(" Proxies Added\n", Color.FromArgb(246, 120, 106));

            Console.Write("    > [", Color.GhostWhite);
            Console.Write("+", Color.FromArgb(246, 120, 106));
            Console.Write("] Starting Checker...\n\n", Color.GhostWhite);
            Thread.Sleep(50);

            Task.Factory.StartNew(delegate ()
            {
                Brute.UpdateInterface();
            });

            for (int i = 1; i <= Variables.Threads; i++)
            {
                new Thread(new ThreadStart(Brute.Checker)).Start();
            }

            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
