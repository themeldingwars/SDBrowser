using SharpCompress.Archives.SevenZip;
using System;

namespace FauFau.Repo
{
    class Program
    {
        static void Main(string[] args)
        {
            SevenZipArchive archive = SevenZipArchive.Open(@"V:\refall\beta-1432.0.7z");

            foreach(SevenZipArchiveEntry entry in archive.Entries)
            {

            }

            
            Console.ReadKey();

        }
    }
}
