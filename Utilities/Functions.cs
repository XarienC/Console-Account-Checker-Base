using System;
using System.Linq;
using System.Text;

namespace ConsoleCheckerBase.Utilities
{
    internal class Functions
    {

        public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));

        public static string Parse(string source, string left, string right) => source.Split(new string[1]
        {
            left
        }, StringSplitOptions.None)[1].Split(new string[1]
        {
            right
        }, StringSplitOptions.None)[0];

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
