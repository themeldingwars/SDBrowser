using System;
using System.IO;
using FauFau.Formats;

namespace SDBrowser
{
    public static class SdbNames
    {
        // The names from fields.txt and test-fields.txt next to the executable, the first name for a hash wins
        public static StaticDBNames LoadDefault()
        {
            var names = new StaticDBNames();
            var baseDir = AppContext.BaseDirectory;

            foreach (var file in new[] { "fields.txt", "test-fields.txt" }) {
                var path = Path.Combine(baseDir, file);
                if (File.Exists(path)) {
                    names.AddFromFile(path);
                }
            }

            return names;
        }
    }
}
