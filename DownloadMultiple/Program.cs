using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DownloadMultiple
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = new FileIo();

            f.DownloadFiles();

        }
    }

    public class FileIo
    {
        public string FileLocation = ConfigurationManager.AppSettings["FileLocation"];

        /// <summary>
        /// Opens the and read file.
        /// </summary>
        /// <returns></returns>
        public List<string> OpenAndReadFile()
        {

            var lines = File.ReadAllLines(FileLocation);

            var list = lines.ToList();
            list.RemoveAt(0);

            return list;

        }

        /// <summary>
        /// Downloads the files.
        /// </summary>
        public void DownloadFiles()
        {
            var urls = OpenAndReadFile();
            var count = 0;

            Parallel.ForEach(urls, s =>
            {
                if (count.Equals(urls.Count)) return;
                count += 1;
                var fileName = @"C:\Users\jdave\Desktop\DownloadMultiple\Downloaded_Data\" + count + "test.pdf";
                using (var client = new WebClient())
                {
                    Console.WriteLine("Starting to download {0}", s);
                    client.DownloadFile(s, fileName);
                    Console.WriteLine("Finished downloading {0}", s);
                }
            });

        }
    }
}
