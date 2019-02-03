using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace GetImage
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = Console.ReadLine();
            string path = @"D:\1\localfile.html";

            Console.WriteLine();

            if (url.IndexOf("http") == -1 && url.IndexOf("https") == -1)
            {
                url = @"http://"+url;
            }

            try
            {
                HtmlManagment.CreateHtmlFile(url, path);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            List<string> tagLines = HtmlManagment.GetLines(path, "<img");
            HtmlManagment.GetImgLink(ref tagLines);
            
            foreach (string s in tagLines)
            {
                Console.WriteLine(s);
            }

            tagLines.RemoveRange(5, tagLines.Count - 5 );

            try
            {
                HtmlManagment.CreateImages(tagLines, @"D:\1");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }        
    }
}
