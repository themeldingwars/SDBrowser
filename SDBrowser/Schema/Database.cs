using System.Collections.Generic;

namespace SDBrowser.Schema
{
    public class Database
    {
        public string Name { get; set; }
        public List<Table> Tables { get; set; } = new();
    }
}