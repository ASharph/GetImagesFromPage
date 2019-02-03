using System;
using System.IO;
using System.Collections.Generic;
using System.Net;

namespace GetImage
{
    class HtmlManagment
    {
        public static void CreateHtmlFile(string url, string path)
        {
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                client.DownloadFile(url, path);
            }
        }

        public static List<string> GetLines(string path, string tag)
        {
            List<string> lines = new List<string>();
            string line;
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(tag))
                    {
                        lines.Add(line);
                    }
                }
            }

            return lines;
        }

        public static void CreateImages(List<string> links, string directory)
        {
            using (WebClient wb = new WebClient())
            {
                for (int i = 0; i < links.Count; i++)
                {
                    if (links[i].IndexOf(".png") != -1)
                    {
                        wb.DownloadFile(links[i], directory + $"\\{i.ToString()}.png");
                    }

                    if (links[i].IndexOf(".jpg") != -1)
                    {
                        wb.DownloadFile(links[i], directory + $"\\{i.ToString()}.jpg");
                    }
                }
            }
        }

        public static void GetImgLink(ref List<string> links)
        {
            string quote = '\u0022'.ToString();

            for (int i = 0; i < links.Count; i++)
            {
                links[i] = GetStringBetween(links[i], "src=" + quote, quote);

                if (links[i].IndexOf("http:") == -1 && links[i].IndexOf("https:") == -1)
                {
                    links[i] = "http:" + links[i];
                }
            }
        }

        private static string GetStringBetween(string s, string from, string until)
        {
            int startIndex = s.IndexOf(from) + from.Length;
            int entIndex = s.IndexOf(until, startIndex);

            return s.Substring(startIndex, entIndex - startIndex);
        }
    }
}
