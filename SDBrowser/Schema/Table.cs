using System.Collections.Generic;

namespace SDBrowser.Schema
{
    public class Table
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; } = new();
    }
}