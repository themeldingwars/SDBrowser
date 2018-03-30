using FauFau;
using FauFau.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            BMesh32 bMesh = new BMesh32();
            //bMesh.Read(@"V:\refall\Firefall\system\assetdb\00066000\00066102.bMesh");
            //bMesh.Write(@"V:\refall\Firefall\system\assetdb\00066000\00066102.cMesh");
            


            /*
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
            //Table table = sdb.GetTableById(0x97DFAFF9);
            //int col = table.GetColumnIndexByName("english");
            int strings = 0;

            byte[] xx = File.ReadAllBytes("test.dat");
            BinaryUtil.MTXor(361878, ref xx);
            File.WriteAllBytes("testout.dat", xx);

            sw.Restart();
            foreach (Table table in sdb)
            {
                for(int c = 0; c < table.Columns.Count; c++)
                {
                    if (table.Columns[c].Type == DBType.String)
                    {
                        Parallel.For(0, table.Rows.Count, i =>
                        {
                            string str = (string)table[i][c];
                            if (str != null && str.Contains("Viktor"))
                            {
                                //Console.WriteLine(str);
                            }
                        });
                    }
                }
            }
            Console.WriteLine("data read done in " + sw.ElapsedMilliseconds + "ms!");


            Console.WriteLine(strings);
            sw.Stop();
            */

            Console.ReadKey();




        }
    }
}
