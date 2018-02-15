using FauFau;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static FauFau.StaticDB;
using static FauFau.Util.BinaryStream;

namespace FauFau.Test
{
    class Program
    {
        private static byte one = 1;
        private static byte zero = 0;

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            StaticDB sdb = new StaticDB();
            //sdb.Read(@"V:\refall\Firefall\system\db\clientdb.sd2");



            sdb.Read(@"D:\backup\clientdb\1962.sd2");
            GC.Collect();
            Console.WriteLine("read done in " + sw.ElapsedMilliseconds + "ms!");
            sw.Restart();
            sdb.Write(new Util.BinaryStream());
            Console.WriteLine("write done in " + sw.ElapsedMilliseconds + "ms!");
            
            sw.Restart();
            Table table = sdb.GetTableById(0x97DFAFF9);
            int col = table.GetColumnIndexByName("english");

            Parallel.ForEach(table, (row) =>
            {
                for (int i = 0; i < 6; i++)
                {
                    string str = (string)row[i];
                    if (str.Contains("melding"))
                    {

                    }
                }

            });

            sw.Stop();
            Console.WriteLine("data read done in " + sw.ElapsedMilliseconds + "ms!");
            Console.ReadKey();




        }
    }
}
