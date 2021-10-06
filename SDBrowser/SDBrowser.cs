using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;
using FauFau.Formats;
using FauFau.Util.CommmonDataTypes;
using static FauFau.Formats.StaticDB;
using System.Text.RegularExpressions;
using System.Drawing.Text;


namespace FauFau.SDBrowser {
	public partial class Form1 : Form
    {


        private StaticDB sdb;
        private int openTable = -1;

        public static Dictionary<uint, string> stringDb = new Dictionary<uint, string>();
        public List<string> usedStrings = new List<string>();
        public List<string> clearedStrings = new List<string>();
        public Dictionary<uint, List<string>> dupes = new Dictionary<uint, List<string>>();
        private List<string> TableNames = new List<string>();

        private Tuple<string, StaticDB.DBType>[] fields;
        private int realScroll = 0;
        private int gotoRow = -1;
        private int gotoField = -1;
        private int previousRow = -1;
        private int currentInspector = -1;
        private Control[] inspectorControls;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));


        private List<DBType> rawCopyableTypes = new List<DBType>
        {
            DBType.AsciiChar,
            DBType.String,
        };

        private List<DBType> searchableTypes = new List<DBType>
        {
            DBType.AsciiChar,
            DBType.Double,
            DBType.Float,
            DBType.Half,
            DBType.Int,
            DBType.Long,
            DBType.SByte,
            DBType.Short,
            DBType.String,
            DBType.UInt,
            DBType.ULong,
            DBType.UShort,
            DBType.Byte
        };


        private int contextRow = -1;
        private int contextColumn = -1;

        private int currentRow;
        private int currentColumn;

        public int CurrentRow
        {
            get { return currentRow; }
            set
            {
                if (currentRow != value)
                {
                    currentRow = value;
                    //if (dsbVerticalGrid.Value != value) dsbVerticalGrid.Value = value;
                    GotoCell(currentRow, currentColumn);
                }
            }
        }

        public int CurrentColumn
        {
            get { return currentColumn; }
            set
            {
                if (currentColumn != value)
                {
                    currentColumn = value;
                    //if(dsbHorizontalGrid.Value != value) dsbHorizontalGrid.Value = value;
                    GotoCell(currentRow, currentColumn);
                }
            }
        }

        private decimal Map(decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
        {
            return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        private void GotoCell(int row, int column)
        {
            if (row > -1 && column > -1)
            {
                if (dgvRows.Rows != null && dgvRows.Rows.Count > row)
                {
                    if (dgvRows.Rows[row].Cells != null && dgvRows.Rows[row].Cells.Count > column)
                    {

                        dgvRows.FirstDisplayedScrollingRowIndex = row;
                        dgvRows.FirstDisplayedScrollingColumnIndex = column;
                    }
                }
            }
        }


        public int GetSelectedTableIdx()
        {
            var name = lbTables.Items[lbTables.SelectedIndices[0]] as string;
            var idx  = TableNames.IndexOf(name);
            return idx;
        }
        

        public Form1()
        {
            InitializeComponent();


            foreach (DBType type in Enum.GetValues(typeof(DBType)))
            {
                if (searchableTypes.Contains(type))
                {
                    cbSearchType.Items.Add(type);
                }
            }


            lbTables.SelectedIndexChanged += (s, e) =>
            {
                if (lbTables.SelectedIndices.Count > 0) {
                    OpenTable(GetSelectedTableIdx());
                }
            };

            LoadTableAndFieldNames();
            //LoadDB(@"V:\refall\Firefall\system\db\clientdb.sd2");

            /*
            
            int count = 0;
            int total = 0;
            foreach (var table in sdb)
            {
                total++;
                if(stringDb.ContainsKey(table.Id))
                {
                    count++;
                }
                foreach(var column in table.Columns)
                {
                    total++;
                    if (stringDb.ContainsKey(column.Id))
                    {
                        count++;
                    }
                }
            }

            rtbOutput.Text = count + " of " + total + "fieldnames known";

            */

        }


        public static string ByteArrayToString(byte[] bytes)
        {
            if (bytes == null)
            {

                return "OUCH";
            }
            StringBuilder hex = new StringBuilder(bytes.Length * 3);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2} ", b);
            }
            return hex.ToString().ToUpper();
        }

        static IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutationsWithRept(list, length - 1)
                .SelectMany(t => list,
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private void LoadDB(string filePath)
        {
            if (File.Exists(filePath))
            {
                // clear
                lbTables.Items.Clear();
                dgvRows.Rows.Clear();
                dgvRows.Columns.Clear();
                TableNames.Clear();
                ClearInspector();

                // load the new db
                sdb = new StaticDB();
                sdb.Read(filePath);

                lblPatch.Text = "Patch: " + sdb.Patch;
                lblFlags.Text = "Flags: " + sdb.Flags;
                lblCreated.Text = "Created: " + sdb.Timestamp.ToString() + " UTC";
                lblLoaded.Text = filePath;

                int x = 0;
                int y = 0;
                for (int i = 0; i < sdb.Count(); i++)
                {
                    string hex = GetIdAsHex(sdb[i].Id);
                    if(!hex.Equals(GetTableOrFieldName(sdb[i].Id)))
                    {
                        x++;
                    }
                    y++;

                    var name = i.ToString().PadRight(8) + GetTableOrFieldName(sdb[i].Id);
                    TableNames.Add(name);
                    lbTables.Items.Add(name);
                }

                Console.WriteLine(x + " / " + y);

            }
        }


        public static string GetIdAsHex(uint id)
        {
            return "0x" + id.ToString("X4").PadLeft(8, '0');
        }

        public static string GetTableOrFieldName(uint id)
        {
            if (stringDb.ContainsKey(id))
            {
                return stringDb[id];
            }
            else
            {
                return GetIdAsHex(id);
            }
        }
        private void ClearInspector()
        {
            if (flpInspect != null)
            {
                List<Control> clear = new List<Control>();
                foreach (Control c in flpInspect.Controls)
                {
                    clear.Add(c);
                }
                foreach (Control c in clear)
                {
                    flpInspect.Controls.Remove(c);
                    c.Dispose();
                }
            }
        }
        private void LoadTableAndFieldNames()
        {
            if (File.Exists("fields.txt"))
            {
                usedStrings = File.ReadAllLines("fields.txt").ToList();
                clearedStrings = File.ReadAllLines("fields.txt").ToList();

                foreach (string str in usedStrings)
                {
                    uint key = FauFau.Util.Checksum.FFnv32(str);

                    if (!stringDb.ContainsKey(key))
                    {
                        stringDb.Add(key, str);
                        dupes.Add(key, new List<string>());
                        dupes[key].Add(str);
                    }
                    else
                    {
                        if (!str.Equals(stringDb[key]))
                        {
                            Console.WriteLine("collision: " + key + " : " + stringDb[key] + ", " + str);
                            dupes[key].Add(str);
                        }
                    }

                }
            }

            /*
            if (File.Exists("console.log"))
            {
                string log = File.ReadAllText("console.log");
                usedStrings = log.Split(new char[] {' ', '\n' }).ToList();

                foreach (string stra in usedStrings)
                {
                    string str = stra.Trim().Replace("'", "");
                    uint key = FauFau.Util.Checksum.FFnv32(str);

                    if (!stringDb.ContainsKey(key))
                    {
                        stringDb.Add(key, str);
                        dupes.Add(key, new List<string>());
                        dupes[key].Add(str);
                        Console.WriteLine(str + "\n");
                    }
                    else
                    {
                        if (!str.Equals(stringDb[key]))
                        {
                            Console.WriteLine("collision: " + key + " : " + stringDb[key] + ", " + str);
                            dupes[key].Add(str);
                        }
                    }

                }
            }
            */

        }
        private void OpenTable(int index)
        {
            currentRow = -1;
            currentColumn = -1;

            openTable = index;
            dgvRows.Columns.Clear();
            dgvRows.Rows.Clear();

            dgvRows.SuspendLayout();
            dgvRows.RowHeadersVisible = false;

            foreach (Column c in sdb[index].Columns)
            {
                string fName = GetTableOrFieldName(c.Id);
                string hexId = GetIdAsHex(c.Id);

                dgvRows.Columns.Add(GetIdAsHex(c.Id), c.Type.ToString() + "\n" + hexId + "\n" + (!fName.Equals(hexId) ? fName : "?") + "\n");
                var col = dgvRows.Columns[dgvRows.ColumnCount - 1];

                if (sdb[index].NullableColumn.Contains(c))
                {
                    col.HeaderCell.Style.BackColor = Color.FromArgb(255, 65, 45, 45);
                }
                // color fields
                bool b = false;
                foreach (List<string> d in dupes.Values)
                {

                    if (b) break;
                    foreach (string dd in d)
                    {
                        if (dd.Equals(fName))
                        {
                            if (!clearedStrings.Contains(fName))
                            {
                                dgvRows.Columns[dgvRows.ColumnCount - 1].HeaderCell.Style.BackColor = Color.FromArgb(80, 150, 80);
                            }
                            else if (d.Count > 1)
                            {
                                dgvRows.Columns[dgvRows.ColumnCount - 1].HeaderCell.Style.BackColor = Color.FromArgb(150, 80, 80);
                            }
                            b = true;
                            break;
                        }
                    }
                }
            }

            dgvRows.RowCount = sdb[openTable].Count() + 1;
            dgvRows.RowHeadersVisible = true;
            dgvRows.ResumeLayout();
            dgvRows.RowHeadersWidth = 80;

            if (gotoField != -1 && gotoRow != -1)
            {
                CurrentRow = gotoRow;
                CurrentColumn = gotoField;

                gotoRow = -1;
                gotoField = -1;
            }
            else
            {
                if (dgvRows.Rows.Count > 0)
                {
                    CurrentColumn = 0;
                    CurrentRow = 0;
                    InspectRow(0);
                }
            }

        }
        private object CellPreview(object data, DBType type)
        {
            if (data == null) return null;
            switch (type)
            {
                case DBType.Byte:
                case DBType.UShort:
                case DBType.UInt:
                case DBType.ULong:
                case DBType.SByte:
                case DBType.Short:
                case DBType.Int:
                case DBType.Long:
                case DBType.Half:
                case DBType.Float:
                case DBType.Double:
                case DBType.String:
                case DBType.AsciiChar:
                    return data;

                case DBType.Blob:
                case DBType.ByteArray:
                    return BrowserUtil.CreatePreview((List<byte>)data);

                case DBType.UShortArray:
                    return BrowserUtil.CreatePreview((List<ushort>)data);
                case DBType.UIntArray:
                    return BrowserUtil.CreatePreview((List<uint>)data);

                case DBType.Vector2:
                    return BrowserUtil.CreatePreview((Vector2)data);
                case DBType.Vector3:
                    return BrowserUtil.CreatePreview((Vector3)data);
                case DBType.Vector4:
                    return BrowserUtil.CreatePreview((Vector4)data);

                case DBType.Vector2Array:
                    return BrowserUtil.CreatePreview((List<Vector2>)data);
                case DBType.Vector3Array:
                    return BrowserUtil.CreatePreview((List<Vector3>)data);
                case DBType.Vector4Array:
                    return BrowserUtil.CreatePreview((List<Vector4>)data);

                case DBType.Matrix4x4:
                    return BrowserUtil.CreatePreview((Matrix4x4)data);
                case DBType.HalfMatrix4x3:
                    return BrowserUtil.CreatePreview((HalfMatrix4x3)data);
                case DBType.Box3:
                    return BrowserUtil.CreatePreview((Box3)data);

            }
            return data;
        }

        // fix table width
        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            /*
            if (lbTables.Columns.Count > 0)
            {
                this.lbTables.Columns[0].Width = this.lbTables.Width - 18;
            }
            */
        }

        public static uint SwapEndianness(uint value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            odfSdb.ShowDialog();
        }

        private void odfSdb_FileOk(object sender, CancelEventArgs e)
        {
            if (File.Exists(odfSdb.FileName))
            {
                LoadDB(odfSdb.FileName);
            }
        }

        private void dgvRows_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.ColumnIndex < sdb[openTable].Columns.Count && e.RowIndex < sdb[openTable].Rows.Count)
            {
                if (dgvRows.Rows[e.RowIndex].HeaderCell.Value == null)
                {
                    dgvRows.Rows[e.RowIndex].HeaderCell.Value = e.RowIndex.ToString();
                }

                e.Value = CellPreview(sdb[openTable][e.RowIndex][e.ColumnIndex], sdb[openTable].Columns[e.ColumnIndex].Type);

            }
        }


        private void DoSearch(int table = -1)
        {
            StaticDB.DBType type = (StaticDB.DBType)cbSearchType.SelectedIndex + 1;

            if (type == DBType.Unknown)
            {
                lbSearchResults.Items.Clear();
                lbSearchResults.Items.Add("You have to pick a datatype first");
            }
            else
            {

                switch (type)
                {
                    case DBType.Byte:
                        byte v1;
                        if (byte.TryParse(tbSearchInput.Text, out v1))
                        {
                            Search(v1, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.UShort:
                        ushort v2;
                        if (ushort.TryParse(tbSearchInput.Text, out v2))
                        {
                            Search(v2, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.UInt:
                        uint v3;
                        if (uint.TryParse(tbSearchInput.Text, out v3))
                        {
                            Search(v3, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.ULong:
                        ulong v4;
                        if (ulong.TryParse(tbSearchInput.Text, out v4))
                        {
                            Search(v4, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.SByte:
                        sbyte v5;
                        if (sbyte.TryParse(tbSearchInput.Text, out v5))
                        {
                            Search(v5, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.Short:
                        short v6;
                        if (short.TryParse(tbSearchInput.Text, out v6))
                        {
                            Search(v6, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.Int:
                        int v7;
                        if (int.TryParse(tbSearchInput.Text, out v7))
                        {
                            Search(v7, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.Long:
                        long v8;
                        if (long.TryParse(tbSearchInput.Text, out v8))
                        {
                            Search(v8, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.Float:
                    case DBType.Half:
                        float v9;
                        if (float.TryParse(tbSearchInput.Text, out v9))
                        {
                            Search(v9, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.Double:
                        double v10;
                        if (double.TryParse(tbSearchInput.Text, out v10))
                        {
                            Search(v10, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.AsciiChar:
                        if (tbSearchInput.Text.Length == 1)
                        {
                            Search(Encoding.ASCII.GetBytes(tbSearchInput.Text)[0], type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.String:
                        if (tbSearchInput.Text.Length > 1)
                        {
                            Search(tbSearchInput.Text, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Please input at least 2 chars");
                        }
                        break;

                    // array contains sequence
                    case DBType.Blob:
                    case DBType.ByteArray:
                        List<byte> bytes = new List<byte>();
                        string[] split = tbSearchInput.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in split)
                        {
                            byte v11;
                            if (byte.TryParse(s.Trim(), out v11))
                            {
                                bytes.Add(v11);
                            }
                        }
                        if (bytes.Count > 0)
                        {
                            Search(bytes, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.UShortArray:
                        List<ushort> ushorts = new List<ushort>();
                        string[] split2 = tbSearchInput.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in split2)
                        {
                            ushort v12;
                            if (ushort.TryParse(s.Trim(), out v12))
                            {
                                ushorts.Add(v12);
                            }
                        }
                        if (ushorts.Count > 0)
                        {
                            Search(ushorts, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;
                    case DBType.UIntArray:
                        List<uint> uints = new List<uint>();
                        string[] split3 = tbSearchInput.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in split3)
                        {
                            uint v13;
                            if (uint.TryParse(s.Trim(), out v13))
                            {
                                uints.Add(v13);
                            }
                        }
                        if (uints.Count > 0)
                        {
                            Search(uints, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }
                        break;

                    // not implemented
                    case DBType.Box3:
                    case DBType.Vector2Array:
                    case DBType.Vector3Array:
                    case DBType.Vector4Array:
                    case DBType.Vector2:
                    case DBType.Vector3:
                    case DBType.Vector4:
                    case DBType.Matrix4x4:
                    case DBType.HalfMatrix4x3:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;

                }
            }

        }



        private void Search(object find, DBType type, int table = -1, bool clear = true)
        {

            if (clear)
            {
                lbSearchResults.Items.Clear();
            }

            if (table == -1)
            {
                for (int i = 0; i < sdb.Tables.Count(); i++)
                {
                    Search(find, type, i, false);
                }
            }
            else
            {
                List<string> matches = new List<string>();
                for (int column = 0; column < sdb[table].Columns.Count(); column++)
                {
                    if (sdb[table].Columns[column].Type == type)
                    {

                        switch (type)
                        {
                            //simple equals
                            case DBType.Byte:
                            case DBType.UShort:
                            case DBType.UInt:
                            case DBType.ULong:
                            case DBType.SByte:
                            case DBType.Short:
                            case DBType.Int:
                            case DBType.Long:
                            case DBType.Float:
                            case DBType.Double:
                            case DBType.AsciiChar:
                            case DBType.Half:
                                for (int row = 0; row < sdb[table].Rows.Count(); row++)
                                {
                                    if (sdb[table][row][column] != null)
                                    {
                                        if (sdb[table][row][column].Equals(find))
                                        {
                                            matches.Add(table + ":" + row + ":" + column);
                                        }
                                    }
                                }
                                break;

                            // string contains sequence
                            case DBType.String:
                                string findString = ((string)find).ToLower();
                                for (int row = 0; row < sdb[table].Rows.Count(); row++)
                                {
                                    if (sdb[table][row][column] != null)
                                    {
                                        string currentString = ((string)sdb[table][row][column]).ToLower();
                                        if (currentString.Contains(findString))
                                        {
                                            matches.Add(table + ":" + row + ":" + column);
                                        }
                                    }
                                }
                                break;

                            // array contains sequence
                            case DBType.Blob:
                            case DBType.ByteArray:
                                List<byte> subset = (List<byte>)find;
                                for (int row = 0; row < sdb[table].Rows.Count(); row++)
                                {
                                    if (!subset.Except((List<byte>)sdb[table][row][column]).Any())
                                    {
                                        matches.Add(table + ":" + row + ":" + column);
                                    }
                                }
                                break;
                            case DBType.UShortArray:
                                List<ushort> subset2 = (List<ushort>)find;
                                for (int row = 0; row < sdb[table].Rows.Count(); row++)
                                {
                                    if (!subset2.Except((List<ushort>)sdb[table][row][column]).Any())
                                    {
                                        matches.Add(table + ":" + row + ":" + column);
                                    }
                                };
                                break;
                            case DBType.UIntArray:
                                List<uint> subset3 = (List<uint>)find;
                                for (int row = 0; row < sdb[table].Rows.Count(); row++)
                                {
                                    if (!subset3.Except((List<uint>)sdb[table][row][column]).Any())
                                    {
                                        matches.Add(table + ":" + row + ":" + column);
                                    }
                                }
                                break;

                            // not implemented
                            case DBType.Box3:
                            case DBType.Vector2Array:
                            case DBType.Vector3Array:
                            case DBType.Vector4Array:
                            case DBType.Vector2:
                            case DBType.Vector3:
                            case DBType.Vector4:
                            case DBType.Matrix4x4:
                            case DBType.HalfMatrix4x3:
                                lbSearchResults.Items.Clear();
                                lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                                break;
                        }
                    }
                }

                if (matches.Count == 0)
                {
                    //lvSearchResults.Items.Add("No results");
                }
                foreach (string m in matches)
                {
                    lbSearchResults.Items.Add(m);
                }


            }



        }

        private void InspectRow(int row)
        {

            if (lbTables.SelectedIndices.Count > 0)
            {
                flpInspect.SuspendLayout();


                if (GetSelectedTableIdx() != currentInspector)
                {
                    currentInspector = GetSelectedTableIdx();
                    //Tuple<string, StaticDB.DBType>[] fields = sdb.body.GetFields(currentInspector);
                    inspectorControls = new Control[sdb[currentInspector].Columns.Count()];

                    ClearInspector();

                    int x = 0;
                    foreach (Column column in sdb[currentInspector].Columns)
                    {
                        FlowLayoutPanel p = new FlowLayoutPanel();

                        p.AutoSize = true;
                        p.MaximumSize = new Size(flpInspect.Width - 23, int.MaxValue);
                        p.BackColor = Color.FromArgb(60, 63, 65);

                        Label l = new Label();

                        l.Text = GetTableOrFieldName(column.Id);
                        l.BackColor = Color.FromArgb(50, 50, 50);
                        l.Width = flpInspect.Width - 23;
                        l.Height = 15;
                        p.Controls.Add(l);

                        Control value = CreateInspectorControl(column.Type);
                        inspectorControls[x] = value;
                        p.Controls.Add(value);
                        flpInspect.Controls.Add(p);
                        x++;
                    }


                }

                if (sdb[currentInspector].Count() > row)
                {
                    int y = 0;
                    foreach (Column column in sdb[currentInspector].Columns)
                    {

                        object current = sdb[currentInspector][row][y];
                        SetInspectorControlValue(column.Type, inspectorControls[y], current);
                        y++;
                    }

                }
                else
                {
                    int y = 0;
                    foreach (Column column in sdb[currentInspector].Columns)
                    {
                        SetInspectorControlValue(column.Type, inspectorControls[y], null);
                        y++;
                    }
                }



                flpInspect.ResumeLayout(true);

            }

        }

        private Control CreateInspectorControl(DBType type)
        {

            switch (type)
            {
                case StaticDB.DBType.Byte:
                case StaticDB.DBType.UShort:
                case StaticDB.DBType.UInt:
                case StaticDB.DBType.ULong:
                case StaticDB.DBType.SByte:
                case StaticDB.DBType.Short:
                case StaticDB.DBType.Int:
                case StaticDB.DBType.Long:
                case StaticDB.DBType.Float:
                case StaticDB.DBType.Double:
                case StaticDB.DBType.String:
                case StaticDB.DBType.AsciiChar:
                    return CreateInspectorLabel();

                case StaticDB.DBType.Vector2:
                    DataGridView dgv0 = CreateInspectorGrid(new string[] { "X", "Y" });
                    dgv0.Height = 45;
                    return dgv0;

                case StaticDB.DBType.Vector3:
                    DataGridView dgv1 = CreateInspectorGrid(new string[] { "X", "Y", "Z" });
                    dgv1.Height = 45;
                    return dgv1;

                case StaticDB.DBType.Vector4:

                    DataGridView dgv2 = CreateInspectorGrid(new string[] { "X", "Y", "Z", "W" });
                    dgv2.Height = 45;
                    return dgv2;

                case StaticDB.DBType.Matrix4x4:
                    DataGridView dgv3 = CreateInspectorGrid(new string[] { "X", "Y", "Z", "W" });
                    dgv3.Height = 115;
                    return dgv3;

                case StaticDB.DBType.ByteArray:
                case StaticDB.DBType.Blob:
                    return CreateInspectorHexBox();

                case StaticDB.DBType.Vector2Array:
                    DataGridView dgv4 = CreateInspectorGrid(new string[] { "X", "Y" });
                    dgv4.Height = 200;
                    return dgv4;

                case StaticDB.DBType.Vector3Array:
                    DataGridView dgv5 = CreateInspectorGrid(new string[] { "X", "Y", "Z" });
                    dgv5.Height = 200;
                    return dgv5;

                case StaticDB.DBType.Vector4Array:
                    DataGridView dgv6 = CreateInspectorGrid(new string[] { "X", "Y", "Z", "W" });
                    dgv6.Height = 200;
                    return dgv6;

                case StaticDB.DBType.UShortArray:
                    DataGridView dgv7 = CreateInspectorGrid(1);
                    dgv7.Height = 200;
                    return dgv7;

                case StaticDB.DBType.UIntArray:
                    DataGridView dgv8 = CreateInspectorGrid(1);
                    dgv8.Height = 200;
                    return dgv8;

                case StaticDB.DBType.HalfMatrix4x3:
                case StaticDB.DBType.Half:
                case StaticDB.DBType.Box3:
                    return CreateInspectorLabel("No inspector for this yet");

            }

            return null;
        }
        private void SetInspectorControlValue(DBType type, Control control, object value)
        {

            switch (type)
            {
                case DBType.Byte:
                case DBType.UShort:
                case DBType.UInt:
                case DBType.ULong:
                case DBType.SByte:
                case DBType.Short:
                case DBType.Int:
                case DBType.Long:
                case DBType.Float:
                case DBType.Double:
                case DBType.AsciiChar:
                    ((RichTextBox)control).Text = value != null ? value.ToString() : "";
                    break;

                case DBType.String:
                    string ret = "";
                    if (value != null)
                    {
                        if (value != null && ((string)value).Length > 0)
                        {
                            ret = MakeUtf8ControlCharactersReadable((string)value);
                        }
                    }
                    ((RichTextBox)control).Text = ret;
                    break;

                case DBType.Vector2:

                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((Vector2)value).x, ((Vector2)value).y);
                    }
                    break;

                case DBType.Vector3:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((Vector3)value).x, ((Vector3)value).y, ((Vector3)value).z);
                    }
                    break;

                case StaticDB.DBType.Vector4:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z, ((Vector4)value).w);
                    }
                    break;

                case StaticDB.DBType.Matrix4x4:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((Matrix4x4)value).x.x, ((Matrix4x4)value).x.y, ((Matrix4x4)value).x.z, ((Matrix4x4)value).x.w);
                        ((DataGridView)control).Rows.Add(((Matrix4x4)value).y.x, ((Matrix4x4)value).y.y, ((Matrix4x4)value).y.z, ((Matrix4x4)value).y.w);
                        ((DataGridView)control).Rows.Add(((Matrix4x4)value).z.x, ((Matrix4x4)value).z.y, ((Matrix4x4)value).z.z, ((Matrix4x4)value).z.w);
                        ((DataGridView)control).Rows.Add(((Matrix4x4)value).w.x, ((Matrix4x4)value).w.y, ((Matrix4x4)value).w.z, ((Matrix4x4)value).w.w);
                    }
                    break;

                case StaticDB.DBType.ByteArray:
                case StaticDB.DBType.Blob:

                    if (value != null)
                    {

                        if (value != null && ((List<byte>)value).Count > 0)
                        {
                            ((HexBox)control).ByteProvider = new DynamicByteProvider((List<byte>)value);
                        }
                        else
                        {
                            ((HexBox)control).ByteProvider = new DynamicByteProvider(new byte[0]);
                        }
                    }
                    else
                    {
                        ((HexBox)control).ByteProvider = new DynamicByteProvider(new byte[0]);
                    }
                    break;

                case DBType.Vector2Array:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        foreach (Vector2 entry in (List<Vector2>)value)
                        {
                            ((DataGridView)control).Rows.Add(entry.x, entry.y);
                        }
                    }
                    break;

                case DBType.Vector3Array:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        foreach (Vector3 entry in (List<Vector3>)value)
                        {
                            ((DataGridView)control).Rows.Add(entry.x, entry.y, entry.z);
                        }
                    }
                    break;

                case DBType.Vector4Array:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        foreach (Vector4 entry in (List<Vector4>)value)
                        {
                            ((DataGridView)control).Rows.Add(entry.x, entry.y, entry.z, entry.w);
                        }
                    }
                    break;

                case DBType.UShortArray:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add((List<ushort>)value);
                    }
                    break;

                case StaticDB.DBType.UIntArray:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add((List<uint>)value);
                    }
                    break;

                case StaticDB.DBType.HalfMatrix4x3:
                case StaticDB.DBType.Half:
                case StaticDB.DBType.Box3:
                    break;

            }

        }

        private HexBox CreateInspectorHexBox()
        {
            HexBox hb = new HexBox();
            hb.Width = 418;
            hb.BackColor = Color.FromArgb(55, 58, 60);
            hb.BytesPerLine = 16;
            hb.Height = 200;
            hb.VScrollBarVisible = true;
            return hb;
        }

        private RichTextBox CreateInspectorLabel(string label = "")
        {
            /*Label l = new Label();
            l.AutoSize = true;
            l.MinimumSize = new Size(50, 15);
            l.Margin = new Padding(0, 4, 0, 0);
            l.Text = label;*/

            RichTextBox l = new MouseTransparentTextBox();
            l.Width = flpInspect.Width - 24;
            l.WordWrap = true;
            l.Multiline = true;
            l.TextChanged += L_TextChanged;
            l.BackColor = Color.FromArgb(55, 58, 60);
            l.ForeColor = Color.FromArgb(200, 200, 200);
            l.BorderStyle = BorderStyle.None;
            return l;
        }

        private void L_TextChanged(object sender, EventArgs e)
        {
            RichTextBox tb = sender as RichTextBox;

            const int padding = 3;
            // get number of lines (first line is 0, so add 1)
            int numLines = tb.GetLineFromCharIndex(tb.TextLength) + 1;
            // get border thickness
            int border = tb.Height - tb.ClientSize.Height;
            // set height (height of one line * number of lines + spacing)
            tb.Height = tb.Font.Height * numLines + padding + border;

            SimpleHighlight(tb);


        }

        private void SimpleHighlight(RichTextBox rtb)
        {
            rtb.SelectionStart = 0;
            rtb.SelectionLength = rtb.Text.Length;
            rtb.SelectionColor = Color.FromArgb(200, 200, 200);

            string regex = @"(UTF8)(\[)((([0-9A-Fa-f]{2}(?=\s|\]))|\s)+)(\])";
            MatchCollection matches = Regex.Matches(rtb.Text, regex);

            foreach (Match m in matches)
            {
                rtb.SelectionStart = m.Groups[1].Index;
                rtb.SelectionLength = m.Groups[1].Length;
                rtb.SelectionColor = Color.DarkRed;

                rtb.SelectionStart = m.Groups[2].Index;
                rtb.SelectionLength = m.Groups[2].Length;
                rtb.SelectionColor = Color.DarkRed;

                rtb.SelectionStart = m.Groups[3].Index;
                rtb.SelectionLength = m.Groups[3].Length;
                rtb.SelectionColor = Color.White;

                rtb.SelectionStart = m.Groups[6].Index;
                rtb.SelectionLength = m.Groups[6].Length;
                rtb.SelectionColor = Color.DarkRed;

            }
        }


        private DataGridView CreateInspectorGrid(string[] headers)
        {
            return CreateInspectorGrid(headers.Length, headers);
        }
        private DataGridView CreateInspectorGrid(int columns, string[] headers = null)
        {
            DataGridView dgv = new DataGridView();
            dgv.Width = flpInspect.Width - 26;
            dgv.ColumnCount = columns;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(60)))));
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgv.ColumnHeadersDefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgv.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgv.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            dgv.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Gray;
            dgv.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dgv.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgv.EnableHeadersVisualStyles = false;
            dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            dgv.Location = new System.Drawing.Point(0, 0);
            dgv.MultiSelect = false;
            dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;

            //dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;


            if (headers != null)
            {
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(55, 58, 60);
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(224, 224, 224);

                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv.EnableHeadersVisualStyles = false;
                int x = 0;
                foreach (string header in headers)
                {
                    dgv.Columns[x].HeaderText = header;
                    x++;
                }
            }
            else
            {
                dgv.ColumnHeadersVisible = false;
            }
            for (int i = 0; i < columns; i++)
            {
                dgv.Columns[i].Width = (dgv.Width - 21) / columns;
            }

            return dgv;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbTables.SelectedItems.Count > 0)
            {
                DoSearch(lbTables.SelectedIndex);
            }
        }

        private void lbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbSearchResults.SelectedItem == null)
            {
                return;
            }
            string[] go = ((string)lbSearchResults.SelectedItem).Split(new string[] { ":" }, StringSplitOptions.None);

            if (go.Length == 3)
            {

                int table = int.Parse(go[0]);
                int row = int.Parse(go[1]);
                int field = int.Parse(go[2]);

                if (lbTables.Items.Count >= table)
                {
                    if (lbTables.SelectedIndices.Count == 0 || GetSelectedTableIdx() != table)
                    {
                        lbTables.SelectedIndex = table;
                        CurrentRow = row;
                        CurrentColumn = field;
                        dgvRows.Rows[row].Selected = true;
                    }
                    else if (lbTables.SelectedIndices.Count != 0 || GetSelectedTableIdx() == table)
                    {

                        if (dgvRows.Rows.Count >= row)
                        {
                            if (dgvRows.Columns.Count >= field)
                            {
                                CurrentRow = row;
                                CurrentColumn = field;
                                dgvRows.Rows[row].Selected = true;
                            }
                        }
                    }
                }
            }
        }

        private void dgvRows_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                Clipboard.SetText((string)dgvRows.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue);

            }
        }

        private void dgvRows_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRows.SelectedRows.Count > 0 && dgvRows.SelectedRows[0].Index != previousRow)
            {
                previousRow = dgvRows.SelectedRows[0].Index;
                InspectRow(previousRow);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            cbSearchType.SelectedIndex = cbSearchType.Items.IndexOf(DBType.String);


            /*
            StringBuilder sbSecret = new StringBuilder();
            StringBuilder sbNotSecret = new StringBuilder();
            StringBuilder sbAll = new StringBuilder();
            StringBuilder sbInternal = new StringBuilder();

            foreach (Row row in sdb[27])
            {
                if (((byte)row[9]) == 1)
                {
                    string name = "";
                    foreach (Row strings in sdb[31])
                    {
                        if (((uint)strings[6]) == (uint)row[0])
                        {
                            name = (string)strings[4];
                            break;
                        }
                    }

                    string desc = "";
                    foreach (Row strings in sdb[31])
                    {
                        if (((uint)strings[6]) == (uint)row[6])
                        {
                            desc = (string)strings[4];
                            break;
                        }
                    }

                    uint id = (uint)row[7];

                    List<string[]> steps = new List<string[]>();
                    foreach (Row step in sdb[48])
                    {
                        if (((uint)step[1]) == id)
                        {
                            steps.Add(new string[] { (string)step[2], (string)step[3] });
                        }
                    }

                    bool secret = (byte)row[10] == 1;

                    StringBuilder sb = new StringBuilder();

                    sb.Append("ID: ");
                    sb.Append(id);
                    sb.Append("\nSECRET: ");
                    sb.Append(secret);
                    sb.Append("\nNAME: ");
                    sb.AppendLine(name.Replace("\0", string.Empty));
                    sb.Append("DESCRIPTION: ");
                    sb.AppendLine(desc.Replace("\0", string.Empty));

                    if (steps.Count() > 0)
                    {
                        sb.AppendLine("INFO: ");

                        int i = 0;
                        foreach (string[] step in steps)
                        {
                            sb.AppendLine(i + ": " + step[1].Replace("\0", string.Empty) + "   -   " + step[0].Replace("\0", string.Empty));
                            i++;
                        }
                    }

                    sb.AppendLine("\n==========================\n");
                    sbInternal.Append(sb);
                }
            }

            foreach (Row row in sdb[7])
            {
                if (((byte)row[9]) == 1)
                {
                    string name = "";
                    foreach (Row strings in sdb[31])
                    {
                        if (((uint)strings[6]) == (uint)row[0])
                        {
                            name = (string)strings[4];
                            break;
                        }
                    }

                    string desc = "";
                    foreach (Row strings in sdb[31])
                    {
                        if (((uint)strings[6]) == (uint)row[6])
                        {
                            desc = (string)strings[4];
                            break;
                        }
                    }

                    uint id = (uint)row[7];

                    List<string[]> steps = new List<string[]>();
                    foreach (Row step in sdb[48])
                    {
                        if (((uint)step[1]) == id)
                        {
                            steps.Add(new string[] { (string)step[2], (string)step[3] });
                        }
                    }

                    bool secret = (byte)row[10] == 1;

                    StringBuilder sb = new StringBuilder();

                    sb.Append("ID: ");
                    sb.Append(id);
                    sb.Append("\nSECRET: ");
                    sb.Append(secret);
                    sb.Append("\nNAME: ");
                    sb.AppendLine(name.Replace("\0", string.Empty));
                    sb.Append("DESCRIPTION: ");
                    sb.AppendLine(desc.Replace("\0", string.Empty));

                    if (steps.Count() > 0)
                    {
                        sb.AppendLine("INFO: ");

                        int i = 0;
                        foreach (string[] step in steps)
                        {
                            sb.AppendLine(i + ": " + step[1].Replace("\0", string.Empty) + "   -   " + step[0].Replace("\0", string.Empty));
                            i++;
                        }
                    }

                    sb.AppendLine("\n==========================\n");

                    if (secret)
                    {
                        sbSecret.Append(sb);
                    }
                    else
                    {
                        sbNotSecret.Append(sb);
                    }
                    sbAll.Append(sb);

                }
            }

            File.WriteAllText("achievements.txt", sbAll.ToString());
            File.WriteAllText("achievements.secret.txt", sbSecret.ToString());
            File.WriteAllText("achievements.notsecret.txt", sbNotSecret.ToString());
            File.WriteAllText("achievements.internal.txt", sbInternal.ToString());
            */
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {

            uint key;
            if(uint.TryParse(tbxDecrypt.Text, out key))
            {
                
                uint key2 = BitConverter.ToUInt32(BitConverter.GetBytes(key).Reverse().ToArray(), 0);
                bool match = false;

                try
                {
                    byte[] data = sdb.GetDataEntry(key);

                    if (data != null)
                    {
                        match = true;
                        rtbOutput.AppendText("Found data entry match for key: " + key + "\n");
                        if (data.Length > 0)
                        {
                            rtbOutput.AppendText("Data length: "+data.Length+"\nUTF-8 Decoded:\n\n");
                            rtbOutput.AppendText(MakeUtf8ControlCharactersReadable(Encoding.UTF8.GetString(data)) + "\n\n");
                            rtbOutput.AppendText("Raw Bytes:\n\n");
                            rtbOutput.AppendText(ByteArrayToString(data) + "\n\n");

                        }
                        else
                        {
                            rtbOutput.AppendText("but it's empty..\n");
                        }
                    }
                }
                catch(Exception ex)
                {

                }

                try
                {
                    byte[] data2 = sdb.GetDataEntry(key);
                    if (data2 != null)
                    {
                        match = true;
                        rtbOutput.AppendText("Swapped endianess and found data entry match for key: " + key2 + "\n");
                        if (data2.Length > 0)
                        {
                            rtbOutput.AppendText("Data length: " + data2.Length + "\nUTF-8 Decoded:\n\n");
                            rtbOutput.AppendText(MakeUtf8ControlCharactersReadable(Encoding.UTF8.GetString(data2)) + "\n\n");
                            rtbOutput.AppendText("Raw Bytes:\n\n");
                            rtbOutput.AppendText(ByteArrayToString(data2) + "\n\n");
                        }
                        else
                        {
                            rtbOutput.AppendText("but it's empty..\n");
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                if (!match)
                {
                    rtbOutput.AppendText("No data entry matches for key: " + key + "\n");
                }
            }
            else
            {
                tbxDecrypt.Text = "Invalid input";
            }

            
        }


        private List<Tuple<bool, string>> SplitControlAndNormalUtf8Characters(string input)
        {
            List<Tuple<bool, string>> ret = new List<Tuple<bool, string>>();
            bool ctrl = false;

            if (input == null) return null;
            StringBuilder build = new StringBuilder();
            char ch;
            for (int i = 0; i < input.Length; i++)
            {
                ch = input[i];

                if (char.IsControl(ch))
                {
                    if (!ctrl)
                    {
                        if (build.Length > 0)
                        {
                            ret.Add(new Tuple<bool, string>(false, build.ToString()));
                        }
                        build = new StringBuilder();
                        build.Append("UTF8[ ");
                        ctrl = true;
                    }

                    build.Append(ByteArrayToString(Encoding.UTF8.GetBytes(new char[] { ch })));
                }
                else
                {
                    if (ctrl)
                    {
                        build.Append(" ]");
                        ret.Add(new Tuple<bool, string>(true, build.ToString()));
                        build = new StringBuilder();
                        ctrl = false;
                    }
                    build.Append(ch);
                }
            }

            if (ctrl)
            {
                build.Append(" ]");
            }

            if (build.Length > 0)
            {
                ret.Add(new Tuple<bool, string>(false, build.ToString()));
            }

            return ret;
        }
        private string MakeUtf8ControlCharactersReadable(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Tuple<bool, string> pair in SplitControlAndNormalUtf8Characters(input))
            {
                sb.Append(pair.Item2);
            }
            return sb.ToString();
        }

        private void dgvRows_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Console.WriteLine(e.ColumnIndex);
        }

        private void btnGetHash_Click(object sender, EventArgs e)
        {
            rtbOutput.Clear();
            rtbOutput.Text = "Hashing string : " + tbxDecrypt.Text + "\n";
            //rtbOutput.Text += "Result         : 0x" + StaticDB.FFnv32(tbxDecrypt.Text).ToString("X4");
        }

        private void lvTables_VisibleChanged(object sender, EventArgs e)
        {

        }


        private void Form1_ClientSizeChanged(object sender, EventArgs e)
        {


        }

        private void FixScrollWidth(DataGridView dgvRows, CustomHScrollbar dgvRowsHScroll)
        {
            int width = 0;
            int x = 0;
            foreach (DataGridViewColumn col in dgvRows.Columns)
            {
                width += col.Width + col.DividerWidth;
                if (width > dgvRows.Width)
                {
                    break;
                }
                x++;
            }

            dgvRowsHScroll.Maximum = x;

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void dgvRows_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < sdb[openTable].Count() && e.ColumnIndex >= 0 && e.ColumnIndex < sdb[openTable].Columns.Count())
            {
                contextColumn = e.ColumnIndex;
                contextRow = e.RowIndex;

                // clear menu
                for (int i = 0; i < contextMenuStrip1.Items.Count; i++)
                {
                    contextMenuStrip1.Items[i].Available = false;
                }

                // !empty cells
                if (sdb[openTable][e.RowIndex][e.ColumnIndex] != null)
                {
                    if (searchableTypes.Contains(sdb[openTable].Columns[e.ColumnIndex].Type))
                    {
                        contextMenuStrip1.Items[0].Available = true;
                    }

                    contextMenuStrip1.Items[1].Available = true;

                    if (rawCopyableTypes.Contains(sdb[openTable].Columns[e.ColumnIndex].Type))
                    {
                        contextMenuStrip1.Items[2].Available = true;
                    }
                }



                e.ContextMenuStrip = contextMenuStrip1;
            }

        }

        private void dgvRows_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void searchForThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbSearchInput.Text = sdb[openTable][contextRow][contextColumn].ToString();

            cbSearchType.SelectedIndex = cbSearchType.Items.IndexOf(sdb[openTable].Columns[contextColumn].Type);
            Search(sdb[openTable][contextRow][contextColumn], sdb[openTable].Columns[contextColumn].Type);
        }

        private SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.White);
        private SolidBrush reportsForegroundBrush = new SolidBrush(Color.FromArgb(200,200,200));
        private SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.Gray));
        private SolidBrush reportsBackgroundBrush2 = new SolidBrush(Color.FromArgb(60, 63, 65));
        private SolidBrush reportsBackgroundBrush1 = new SolidBrush(Color.FromArgb(55, 58, 60));

        private void lbSearchResults_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < lbSearchResults.Items.Count)
            {
                string text = lbSearchResults.Items[index].ToString();
                Graphics g = e.Graphics;

                //background:
                SolidBrush backgroundBrush;
                if (selected)
                    backgroundBrush = reportsBackgroundBrushSelected;
                else if ((index % 2) == 0)
                    backgroundBrush = reportsBackgroundBrush1;
                else
                    backgroundBrush = reportsBackgroundBrush2;
                g.FillRectangle(backgroundBrush, e.Bounds);

                //text:
                SolidBrush foregroundBrush = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush;
                g.DrawString(text, e.Font, foregroundBrush, lbSearchResults.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();

        }

        private void lbTables_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < lbTables.Items.Count)
            {
                string text = lbTables.Items[index].ToString();
                Graphics g = e.Graphics;

                //background:
                SolidBrush backgroundBrush;
                if (selected)
                    backgroundBrush = reportsBackgroundBrushSelected;
                else if ((index % 2) == 0)
                    backgroundBrush = reportsBackgroundBrush1;
                else
                    backgroundBrush = reportsBackgroundBrush2;
                g.FillRectangle(backgroundBrush, e.Bounds);

                //text:
                SolidBrush foregroundBrush = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush;
                g.DrawString(text, e.Font, foregroundBrush, lbTables.GetItemRectangle(index).Location);
            }

            e.DrawFocusRectangle();
        }

        private void tbTableFilter_TextChanged(object sender, EventArgs e)
        {
            var filter = tbTableFilter.Text.ToUpper();
            lbTables.Items.Clear();
            
            foreach (var tableName in TableNames) {
                if (tableName.ToUpper().Contains(filter)) {
                    lbTables.Items.Add(tableName);
                }
            }
        }

        private void DB_ImportExport_Click(object sender, EventArgs e)
        {
            var dbWindow = new DBImportExport();
            dbWindow.Db = sdb;
            dbWindow.Show();
        }
    }
}
