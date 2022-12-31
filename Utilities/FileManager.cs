using System;
using System.IO;
using System.Threading;

namespace ConsoleCheckerBase.Utilities
{
    internal class FileManager
    {
        public static ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        public static void ImportCombos(string fileName)
        {
            using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamReader streamReader= new StreamReader(bufferedStream))
                    {
                        while (streamReader.ReadLine() != null)
                        {
                            Variables.Total++;
                        }
                    }
                }
            }
        }

        public static void ImportProxies(string fileName)
        {
            using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamReader streamReader = new StreamReader(bufferedStream))
                    {
                        while (streamReader.ReadLine() != null)
                        {
                            Variables.ProxyTotal++;
                        }
                    }
                }
            }
        }

        public static void Export(string folderName, string fileName, string DataToSave)
        {
            Lock.EnterWriteLock();
            string Date = DateTime.Now.ToString("yyyy-MM-dd").ToString();

            if (!Directory.Exists("Results"))
            {
                Directory.CreateDirectory("Results");
            }
            if (!Directory.Exists("Results\\" + Date))
            {
                Directory.CreateDirectory("Results\\" + Date);
            }
            if (!Directory.Exists("Results\\" + Date + "\\" + folderName))
            {
                Directory.CreateDirectory("Results\\" + Date + "\\" + folderName);
            }

            if (!Directory.Exists("Results"))
            {
                if (!Directory.Exists("Results\\" + Date))
                {
                    if (!Directory.Exists("Results\\" + Date + "\\" + folderName))
                    {
                        Directory.CreateDirectory("Results\\" + Date + "\\" + folderName);
                    }
                }
            }

            try
            {
                using (StreamWriter streamWriter = File.AppendText(string.Concat(new string[]
                {
                    "Results\\",
                    Date,
                    "\\",
                    folderName,
                    "\\",
                    fileName
                })))
                {
                    streamWriter.WriteLine(DataToSave);
                    streamWriter.Close();
                }
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }
    }
}
