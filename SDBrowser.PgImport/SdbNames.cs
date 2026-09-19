using System;
using System.Collections.Generic;
using System.IO;

namespace SDBrowser
{
    public class SdbNames
    {
        private readonly Dictionary<uint, string> names = new Dictionary<uint, string>();

        public static SdbNames LoadDefault()
        {
            var names = new SdbNames();
            var baseDir = AppContext.BaseDirectory;

            names.AddFromFile(Path.Combine(baseDir, "fields.txt"));
            names.AddFromFile(Path.Combine(baseDir, "test-fields.txt"));

            return names;
        }

        public void AddFromFile(string path)
        {
            if (!File.Exists(path)) {
                return;
            }

            foreach (var str in File.ReadAllLines(path)) {
                // Same rule as the UI: the first string for a hash wins
                names.TryAdd(FauFau.Util.Checksum.FFnv32(str), str);
            }
        }

        public string GetTableOrFieldName(uint id) => names.TryGetValue(id, out var name) ? name : GetIdAsHex(id);

        public static string GetIdAsHex(uint id) => "0x" + id.ToString("X4").PadLeft(8, '0');
    }
}
