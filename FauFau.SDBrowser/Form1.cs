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
using static FauFau.StaticDB;

namespace FauFau.SDBrowser
{
    public partial class Form1 : Form
    {
        private StaticDB sdb;
        private int openTable = -1;

        public Dictionary<uint, string> stringDb = new Dictionary<uint, string>();
        public List<string> usedStrings = new List<string>();
        public List<string> clearedStrings = new List<string>();
        public Dictionary<uint, List<string>> dupes = new Dictionary<uint, List<string>>();

        private Tuple<string, StaticDB.DBType>[] fields;
        private int realScroll = 0;
        private int gotoRow = -1;
        private int gotoField = -1;
        private int previousRow = -1;
        private int currentInspector = -1;
        private Control[] inspectorControls;
        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

        CustomVScrollbar lvTablesVScroll;
        CustomVScrollbar dgvRowsVScroll;
        CustomHScrollbar dgvRowsHScroll;

        private int currentRow;
        private int currentColumn;



        public int CurrentRow
        {
            get { return currentRow; }
            set
            {
                if(currentRow != value)
                {
                    currentRow = value;
                    if (dgvRowsVScroll.Value != value) dgvRowsVScroll.Value = value;
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
                    if(dgvRowsHScroll.Value != value) dgvRowsHScroll.Value = value;
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
            if(row > -1 && column > -1)
            {
                if (dgvRows.Rows != null && dgvRows.Rows.Count > row)
                {
                    if (dgvRows.Rows[row].Cells != null && dgvRows.Rows[row].Cells.Count > column)
                    {
                        //Console.WriteLine(row);
                        //dgvRows.CurrentCell = dgvRows.Rows[row].Cells[column];


                        

                        dgvRows.FirstDisplayedScrollingRowIndex = (int)Map(row, 0, dgvRows.Rows.Count, 0, dgvRows.Rows.Count - ((dgvRows.Height - 80) / 22));
                        dgvRows.FirstDisplayedScrollingColumnIndex = column;
                    }
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            this.lvTables.View = View.Details;
            this.lvTables.Columns.Add("Name");
            this.lvTables.Columns[0].Width = this.lvTables.Width - 18;
            this.lvTables.HeaderStyle = ColumnHeaderStyle.None;

            dgvRows.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 60, 63, 65);
            dgvRows.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 200, 200, 200);
            dgvRows.EnableHeadersVisualStyles = false;
            dgvRows.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgvRows.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRows.VirtualMode = true;
            dgvRows.RowHeadersWidth = 80;

            // custom table list scrollbar
            lvTablesVScroll = SetupScrollbar(vScrollBar1);      
            lvTablesVScroll.BringToFront();
            lvTablesVScroll.Scroll += (s, e) =>
            {
                lvTables.TopItem = lvTables.Items[lvTablesVScroll.Value];
                //lvTables.EnsureVisible(lvTablesVScroll.Value);
            };
            lvTables.SelectedIndexChanged += (s, e) => {
                if (lvTables.SelectedIndices.Count > 0)
                {
                    OpenTable(lvTables.SelectedIndices[0]);
                }
            };
            lvTables.TopItemChanged += (s, e) => {
                lvTablesVScroll.Value = lvTables.TopItem.Index;
            };

            dgvRowsVScroll = SetupScrollbar(vScrollBar2);
            dgvRowsVScroll.Maximum = 1;
            dgvRowsVScroll.Name = "VDGV";
            dgvRowsVScroll.BringToFront();
            dgvRowsVScroll.Scroll += (s, e) =>
            {              
                CurrentRow = dgvRowsVScroll.Value;
                //dgvRows.Rows[0].Cells[0]s EnsureVisible(dgvRowsVScroll.Value);
            };

            dgvRows.Scroll += (s, e) => 
            {
                //Console.WriteLine(dgvRows.VerticalScrollingOffset);
            };

            
            

            dgvRowsHScroll = SetupHScrollbar(hScrollBar1);
            dgvRowsHScroll.Maximum = 1;
            dgvRowsHScroll.Name = "HDGV";
            dgvRowsHScroll.BringToFront();
            dgvRowsHScroll.Scroll += (s, e) =>
            {
                //dgvRows.HorizontalScrollingOffset = dgvRowsHScroll.Value;
                dgvRows.FirstDisplayedScrollingColumnIndex = dgvRowsHScroll.Value;
                //dgvRows.Rows[0].Cells[0]s EnsureVisible(dgvRowsVScroll.Value);
            };

            //dgvRowsHScroll.Hide();
            //dgvRowsVScroll.Hide();

            dgvRows.MouseWheel += (s, e) =>
             {
                 if (e.Delta > 0 && CurrentRow > 0)
                 {
                     CurrentRow--;
                 }
                 else if (e.Delta < 0 && CurrentRow < dgvRows.RowCount-1)
                 {
                     CurrentRow++;
                 }
             };


            LoadTableAndFieldNames();
            LoadDB(@"D:\backup\clientdb\1962.sd2");
        }


        private CustomVScrollbar SetupScrollbar(ScrollBar target)
        {
            CustomVScrollbar cs = new CustomVScrollbar();
            target.Parent.Controls.Add(cs);
            target.Enabled = false;
            target.Visible = false;

            cs.BorderStyle = BorderStyle.None;
            cs.Anchor = target.Anchor;
            cs.BackColor = Color.FromArgb(255, 69, 73, 74);
            cs.ChannelColor = cs.BackColor;
            cs.LargeChange = target.LargeChange;
            cs.Location = target.Location;
            cs.Maximum = target.Maximum;
            cs.Minimum = target.Minimum;
            cs.MinimumSize = target.MinimumSize;
            cs.Size = target.Size;
            cs.SmallChange = 1;
            cs.Value = 0;
            
            return cs;
        }

        private CustomHScrollbar SetupHScrollbar(ScrollBar target)
        {
            CustomHScrollbar cs = new CustomHScrollbar();
            target.Parent.Controls.Add(cs);
            target.Enabled = false;
            target.Visible = false;

            cs.BorderStyle = BorderStyle.None;
            cs.Anchor = target.Anchor;
            cs.BackColor = Color.FromArgb(255, 69, 73, 74);
            cs.ChannelColor = cs.BackColor;
            cs.LargeChange = target.LargeChange;
            cs.Location = target.Location;
            cs.Maximum = target.Maximum;
            cs.Minimum = target.Minimum;
            cs.MinimumSize = target.MinimumSize;
            cs.Size = target.Size;
            cs.SmallChange = 1;
            cs.Value = 0;

            return cs;
        }

        public static string ByteArrayToString(byte[] bytes)
        {
            if(bytes == null)
            {

                return "OUCH";
            }
            StringBuilder hex = new StringBuilder(bytes.Length*3);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2} ", b);
            }
            return hex.ToString().ToUpper();
        }

        static IEnumerable<IEnumerable<T>>GetPermutationsWithRept<T>(IEnumerable<T> list, int length)
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
                lvTables.Items.Clear();
                dgvRows.Rows.Clear();
                dgvRows.Columns.Clear();
                ClearInspector();

                // load the new db
                sdb = new StaticDB();
                sdb.Read(filePath);

                lblPatch.Text = "Patch: " + sdb.Patch;
                lblFlags.Text = "Flags: " + sdb.Flags;
                lblCreated.Text ="Created: " + sdb.Timestamp.ToString() + " UTC";
                lblLoaded.Text = filePath;


                for (int i = 0; i < sdb.Count(); i++)
                {
                    lvTables.Items.Add(i.ToString().PadRight(5) + GetTableOrFieldName(sdb[i].Id));
                }

                lvTablesVScroll.Maximum = sdb.Count()+9;
                lvTablesVScroll.Value = 0;


            }
        }

        private string GetIdAsHex(uint id)
        {
            return "0x" + id.ToString("X4").PadLeft(8, '0');
        }

        private string GetTableOrFieldName(uint id)
        {
            if(stringDb.ContainsKey(id))
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
            if(flpInspect != null)
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
        }
        private void OpenTable(int index)
        {
            currentRow = -1;
            currentColumn = -1;
            FixScrollWidth(dgvRows, dgvRowsHScroll);
            dgvRowsVScroll.Maximum = sdb[index].Rows.Count() + 10;

            

            openTable = index;
            dgvRows.Columns.Clear();
            dgvRows.Rows.Clear();

            dgvRows.SuspendLayout(); 
            dgvRows.RowHeadersVisible = false;

            foreach(Column c in sdb[index].Columns)
            {
                string fName = GetTableOrFieldName(c.Id);
                string hexId = GetIdAsHex(c.Id);
                
                dgvRows.Columns.Add(GetIdAsHex(c.Id), c.Type.ToString() + "\n" + hexId + "\n" + (!fName.Equals(hexId) ? fName : "?") + "\n");
                var col = dgvRows.Columns[dgvRows.ColumnCount - 1];
                
                if(sdb[index].NullableColumn.Contains(c))
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
            switch(type)
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
            if (lvTables.Columns.Count > 0)
            {
                this.lvTables.Columns[0].Width = this.lvTables.Width-18;
            }

        }

        public static uint SwapEndianness(uint value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
        }

        private void lvTables_SelectedIndexChanged(object sender, EventArgs e)
        {

                
            

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
            if(e.ColumnIndex < sdb[openTable].Columns.Count && e.RowIndex < sdb[openTable].Rows.Count)
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

            if(type == StaticDB.DBType.Unknown)
            {
                lbSearchResults.Items.Clear();
                lbSearchResults.Items.Add("You have to pick a datatype first");
            }
            else
            {
                switch (type)
                {
                    case StaticDB.DBType.Byte:
                        byte v1;
                        if(byte.TryParse(tbSearchInput.Text, out v1))
                        {
                            Search(v1, type, table);
                        }
                        else
                        {
                            lbSearchResults.Items.Clear();
                            lbSearchResults.Items.Add("Invalid input");
                        }                  
                        break;
                    case StaticDB.DBType.UShort:
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
                    case StaticDB.DBType.UInt:
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
                    case StaticDB.DBType.ULong:
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
                    case StaticDB.DBType.SByte:
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
                    case StaticDB.DBType.Short:
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
                    case StaticDB.DBType.Int:
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
                    case StaticDB.DBType.Long:
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
                    case StaticDB.DBType.Float:
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
                    case StaticDB.DBType.Double:
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
                    case StaticDB.DBType.String:

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
                    case StaticDB.DBType.Vector2:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Vector3:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Vector4:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Matrix4x4:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Blob:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Box3:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Vector2Array:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Vector3Array:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Vector4Array:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.AsciiChar:
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
                    case StaticDB.DBType.ByteArray:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.UShortArray:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.UIntArray:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.HalfMatrix4x3:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                    case StaticDB.DBType.Half:
                        lbSearchResults.Items.Clear();
                        lbSearchResults.Items.Add("Shtap! I dont know how to search for that yet");
                        break;
                }
            }

        }

        private void Search(object find, StaticDB.DBType type, int table = -1, bool clear = true)
        {
            /*
            if(clear)
            {
                lbSearchResults.Items.Clear();
            }

            if(table == -1)
            {
                for (int i = 0; i < sdb.body.GetTableList().Count; i++)
                {
                    Search(find, type, i, false);
                }
            }
            else
            {
                List<string> matches = new List<string>();
                int x = 0;
                bool shtap = false;
                foreach (Tuple<string, StaticDB.DBType> field in sdb.body.GetFields(table))
                {
                    if (field.Item2 == type)
                    {
                        for (int j = 0; j < sdb.body.GetRowCount(table); j++)
                        {
                            if(!shtap)
                            {
                                object current = sdb.body.GetCell(table, j, x);

                                switch (type)
                                {
                                    case StaticDB.DBType.Byte:
                                        if ((byte)current == (byte)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.UShort:
                                        if ((ushort)current == (ushort)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.UInt:
                                        if ((uint)current == (uint)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.ULong:
                                        if ((ulong)current == (ulong)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.SByte:
                                        if ((sbyte)current == (sbyte)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.Short:
                                        if ((short)current == (short)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.Int:
                                        if ((int)current == (int)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.Long:
                                        if ((long)current == (long)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.Float:
                                        if ((float)current == (float)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.Double:
                                        if ((double)current == (double)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.String:

                                        byte[] b = sdb.body.GetDataEntry((uint)current, true);
                                        uint f = uint.MaxValue;
                                        
                                        if ((uint.TryParse((string)find, out f) && (uint)current == f) || b != null && Encoding.UTF8.GetString(b).ToLower().Contains(((string)find).ToLower()))
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.Vector2:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Vector3:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Vector4:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Matrix4x4:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Blob:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Box3:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Vector2Array:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Vector3Array:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Vector4Array:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.AsciiChar:
                                        if ((byte)current == (byte)find)
                                        {
                                            matches.Add(table + ":" + j + ":" + x);
                                        }
                                        break;
                                    case StaticDB.DBType.ByteArray:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.UShortArray:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.UIntArray:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.HalfMatrix4x3:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                    case StaticDB.DBType.Half:
                                        matches.Add("SHtap! I cant search for that datatype yet!");
                                        shtap = true;
                                        break;
                                }

                            }
                            
                        }

                    }
                    x++;
                }

                if(matches.Count == 0)
                {
                    //lvSearchResults.Items.Add("No results");
                }
                foreach(string m in matches)
                {
                    lbSearchResults.Items.Add(m);
                }


            }

            */
            
        }

        private void InspectRow(int row)
        {
            /*
            if (lvTables.SelectedIndices.Count > 0)
            {
                flpInspect.SuspendLayout();

                if(lvTables.SelectedIndices[0] != currentInspector)
                {
                    currentInspector = lvTables.SelectedIndices[0];
                    Tuple<string, StaticDB.DBType>[] fields = sdb.body.GetFields(currentInspector);
                    inspectorControls = new Control[fields.Count()];

                    ClearInspector();

                    int x = 0;
                    foreach (Tuple<string, StaticDB.DBType> field in fields)
                    {
                        FlowLayoutPanel p = new FlowLayoutPanel();
                        
                        p.AutoSize = true;
                        p.MaximumSize = new Size(flpInspect.Width - 23, int.MaxValue);
                        p.BackColor = Color.DarkGray;

                        Label l = new Label();

                        string[] sp = field.Item1.Split(' ');
                        string fName = sp[0];
                        uint k = Convert.ToUInt32(fName, 16);

                        if (stringDb.ContainsKey(k))
                        {
                            fName = stringDb[k];
                        }

                        l.Text = fName;
                        l.BackColor = Color.Gray;
                        l.Width = flpInspect.Width - 23;
                        l.Height = 15;
                        p.Controls.Add(l);

                        Control value = CreateInspectorControl(field.Item2);
                        inspectorControls[x] = value;
                        p.Controls.Add(value);
                        flpInspect.Controls.Add(p);
                        x++;
                    }

                    
                }
                int y = 0;
                foreach (Tuple<string, StaticDB.DBType> field in fields)
                {
                    object current = sdb.body.GetCell(lvTables.SelectedIndices[0], row, y);
                    SetInspectorControlValue(field.Item2, inspectorControls[y], current);
                    y++;
                }

                    

                flpInspect.ResumeLayout(true);

            }
            */
        }

        private Control CreateInspectorControl(StaticDB.DBType type)
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
                    DataGridView dgv3 = CreateInspectorGrid(new string[] {"X", "Y", "Z", "W"});
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
        private void SetInspectorControlValue(StaticDB.DBType type, Control control, object value)
        {
            /*
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
                case StaticDB.DBType.AsciiChar:
                    ((TextBox)control).Text = value != null ? value.ToString() : "";
                    break;

                case StaticDB.DBType.String:
                    string ret = "";
                    if (value != null)
                    {
                        byte[] data = sdb.body.GetDataEntry((uint)value, true);
                        if (data != null && data.Length > 0)
                        {
                            ret = MakeUtf8ControlCharactersReadable(Encoding.UTF8.GetString(data));
                        }
                    }
                    ((TextBox)control).Text = ret;
                    break;

                case StaticDB.DBType.Vector2:
                    
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((float[])value)[0], ((float[])value)[1]);
                    }
                    break;

                case StaticDB.DBType.Vector3:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((float[])value)[0], ((float[])value)[1], ((float[])value)[2]);
                    }
                    break;

                case StaticDB.DBType.Vector4:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((float[])value)[0], ((float[])value)[1], ((float[])value)[2], ((float[])value)[3]);
                    }
                    break;

                case StaticDB.DBType.Matrix4x4:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        ((DataGridView)control).Rows.Add(((float[][])value)[0][0], ((float[][])value)[0][1], ((float[][])value)[0][2], ((float[][])value)[0][3]);
                        ((DataGridView)control).Rows.Add(((float[][])value)[1][0], ((float[][])value)[1][1], ((float[][])value)[1][2], ((float[][])value)[1][3]);
                        ((DataGridView)control).Rows.Add(((float[][])value)[2][0], ((float[][])value)[2][1], ((float[][])value)[2][2], ((float[][])value)[2][3]);
                        ((DataGridView)control).Rows.Add(((float[][])value)[3][0], ((float[][])value)[3][1], ((float[][])value)[3][2], ((float[][])value)[3][3]);
                    }
                    break;

                case StaticDB.DBType.ByteArray:
                case StaticDB.DBType.Blob:
                    
                    if (value != null)
                    {
                        byte[] data = sdb.body.GetDataEntry((uint)value, true);
                        if (data != null && data.Length > 0)
                        {
                            ((HexBox)control).ByteProvider = new DynamicByteProvider(data);
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

                case StaticDB.DBType.Vector2Array:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        byte[] data = sdb.body.GetDataEntry((uint)value, true);
                        if (data != null && data.Length > 0)
                        {
                            int x = data.Length / 8;

                            for (int i = 0; i < x; i++)
                            {
                                int y = i * 8;
                                ((DataGridView)control).Rows.Add( BitConverter.ToSingle(data, y), BitConverter.ToSingle(data, y + 4) );
                            }
                        }
                    }
                    break;

                case StaticDB.DBType.Vector3Array:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        byte[] data = sdb.body.GetDataEntry((uint)value, true);
                        if (data != null && data.Length > 0)
                        {
                            int x = data.Length / 12;

                            for (int i = 0; i < x; i++)
                            {
                                int y = i * 12;
                                ((DataGridView)control).Rows.Add(BitConverter.ToSingle(data, y), BitConverter.ToSingle(data, y + 4), BitConverter.ToSingle(data, y + 8));
                            }
                        }
                    }
                    break;

                case StaticDB.DBType.Vector4Array:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        byte[] data = sdb.body.GetDataEntry((uint)value, true);
                        if (data != null && data.Length > 0)
                        {
                            int x = data.Length / 16;

                            for (int i = 0; i < x; i++)
                            {
                                int y = i * 16;
                                ((DataGridView)control).Rows.Add(BitConverter.ToSingle(data, y), BitConverter.ToSingle(data, y + 4), BitConverter.ToSingle(data, y + 8), BitConverter.ToSingle(data, y + 12));
                            }
                        }
                    }
                    break;

                case StaticDB.DBType.UShortArray:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        byte[] data = sdb.body.GetDataEntry((uint)value, true);
                        if (data != null && data.Length > 0)
                        {
                            int x = data.Length / 2;

                            for (int i = 0; i < x; i++)
                            {
                                ((DataGridView)control).Rows.Add(BitConverter.ToUInt16(data, i * 2).ToString());
                            }
                        }
                    }
                    break;

                case StaticDB.DBType.UIntArray:
                    ((DataGridView)control).Rows.Clear();
                    if (value != null)
                    {
                        byte[] data = sdb.body.GetDataEntry((uint)value, true);
                        if (data != null && data.Length > 0)
                        {
                            int x = data.Length / 4;

                            for (int i = 0; i < x; i++)
                            {
                                ((DataGridView)control).Rows.Add(BitConverter.ToUInt32(data, i * 4).ToString());
                            }
                        }
                    }
                    break;

                case StaticDB.DBType.HalfMatrix4x3:
                case StaticDB.DBType.Half:
                case StaticDB.DBType.Box3:
                    break;

            }
            */
        }

        private HexBox CreateInspectorHexBox()
        {
            HexBox hb = new HexBox();
            hb.Width = 418;
            hb.BackColor = Color.DarkGray;
            hb.BytesPerLine = 16;
            hb.Height = 200;
            hb.VScrollBarVisible = true;
            return hb;
        }

        private TextBox CreateInspectorLabel(string label = "")
        {
            /*Label l = new Label();
            l.AutoSize = true;
            l.MinimumSize = new Size(50, 15);
            l.Margin = new Padding(0, 4, 0, 0);
            l.Text = label;*/

            TextBox l = new MouseTransparentTextBox();
            l.Width = flpInspect.Width - 24;
            l.WordWrap = true;
            l.Multiline = true;
            l.TextChanged += L_TextChanged;
            l.BackColor = Color.DarkGray;
            l.BorderStyle = BorderStyle.None;
                return l;
        }

        private void L_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            const int padding = 3;
            // get number of lines (first line is 0, so add 1)
            int numLines = tb.GetLineFromCharIndex(tb.TextLength) + 1;
            // get border thickness
            int border = tb.Height - tb.ClientSize.Height;
            // set height (height of one line * number of lines + spacing)
            tb.Height = tb.Font.Height * numLines + padding + border;

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
            dgv.DefaultCellStyle.BackColor = Color.DarkGray;
            dgv.GridColor = Color.Gray;
            dgv.BorderStyle = BorderStyle.None;

            if(headers != null)
            {
                dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv.EnableHeadersVisualStyles = false;
                int x = 0;
                foreach(string header in headers)
                {
                    dgv.Columns[x].HeaderText = header;
                    x++;
                }
            }
            else
            {
                dgv.ColumnHeadersVisible = false;
            }
            for(int i = 0; i < columns; i++)
            {
                dgv.Columns[i].Width = (dgv.Width-21) / columns;
            }

            return dgv;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvTables.SelectedItems.Count > 0)
            {
                DoSearch(lvTables.SelectedItems[0].Index);
            }
        }

        private void lbSearchResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] go = ((string)lbSearchResults.SelectedItem).Split(new string[] { ":" }, StringSplitOptions.None);

            if(go.Length == 3)
            {

                int table = int.Parse(go[0]);
                int row = int.Parse(go[1]);
                int field = int.Parse(go[2]);

                if(lvTables.Items.Count >= table)
                {
                    if(lvTables.SelectedIndices.Count == 0 || lvTables.SelectedIndices[0] != table)
                    {
                        gotoRow = row;
                        gotoField = field;
                        lvTables.Items[table].Selected = true;
                        lvTables.EnsureVisible(table);
                    }
                    else if (lvTables.SelectedIndices.Count != 0 || lvTables.SelectedIndices[0] == table)
                    {
                        
                        if (dgvRows.Rows.Count >= row)
                        {
                            if (dgvRows.Columns.Count >= field)
                            {
                                dgvRows.CurrentCell = dgvRows.Rows[row].Cells[field];
                            }
                        }
                    }
                }
            }
        }

        private void dgvRows_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex > -1 && e.RowIndex > -1)
            {

                    Clipboard.SetText((string)dgvRows.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue);
                           
            }        
        }

        private void dgvRows_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex > -1)
            {
                if (e.RowIndex == -1)
                {
                    string s = (string)dgvRows.Columns[e.ColumnIndex].HeaderText.Split(' ')[0];
                    Clipboard.SetText(s);
                    rtbOutput.Clear();

                    bool b = false;

                    foreach (List<string> d in dupes.Values)
                    {

                        foreach (string dd in d)
                        {
                            if (b) break;
                            if (dd.Equals(s))
                            {
                                if (d.Count > 0)
                                {
                                    rtbOutput.Text += "RESULTS:\n";
                                    foreach (string ss in d)
                                    {
                                        rtbOutput.Text += ss + "\n";
                                    }
                                }
                                b = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    
                    if(!string.IsNullOrEmpty((string)dgvRows.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue))
                    {
                        Clipboard.SetText((string)dgvRows.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue);
                    }
                    
                }
            }
        }

        private void dgvRows_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvRows.SelectedRows.Count > 0 && dgvRows.SelectedRows[0].Index != previousRow)
            {
                previousRow = dgvRows.SelectedRows[0].Index;
                InspectRow(previousRow);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            /*ScrollBar vScrollBar1 = new VScrollBar();
            vScrollBar1.Dock = DockStyle.Right;
            vScrollBar1.Scroll += (s, ev) => { pnlScrollInspector.VerticalScroll.Value = vScrollBar1.Value; };
            pnlScrollInspector.Controls.Add(vScrollBar1);*/
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {

            /*
            uint key;
            if(uint.TryParse(tbxDecrypt.Text, out key))
            {
                
                uint key2 = BitConverter.ToUInt32(BitConverter.GetBytes(key).Reverse().ToArray(), 0);
                bool match = false;

                try
                {
                    byte[] data = sdb.body.GetDataEntry(key, false);
                    byte[] data_ = sdb.body.GetDataEntry(key, true);

                    if (data != null)
                    {
                        match = true;
                        rtbOutput.AppendText("Found data entry match for key: " + key + "\n");
                        if (data.Length > 0)
                        {
                            rtbOutput.AppendText("Data length: "+data.Length+"\nUTF-8 Decoded:\n\n");
                            rtbOutput.AppendText(Encoding.UTF8.GetString(data_) + "\n\n");
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
                    byte[] data2 = sdb.body.GetDataEntry(key2, false);
                    byte[] data2_ = sdb.body.GetDataEntry(key2, true);
                    if (data2 != null)
                    {
                        match = true;
                        rtbOutput.AppendText("Swapped endianess and found data entry match for key: " + key2 + "\n");
                        if (data2.Length > 0)
                        {
                            rtbOutput.AppendText("Data length: " + data2.Length + "\nUTF-8 Decoded:\n\n");
                            rtbOutput.AppendText(Encoding.UTF8.GetString(data2_) + "\n\n");
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

            */
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
                        if(build.Length > 0 )
                        {
                            ret.Add(new Tuple<bool, string>(false, build.ToString()));
                        }                     
                        build = new StringBuilder();
                        build.Append("[ ");
                        ctrl = true;
                    }

                    build.Append(ByteArrayToString(Encoding.UTF8.GetBytes(new char[] { ch })));
                }
                else
                {
                    if(ctrl)
                    {
                        build.Append(" ]");
                        ret.Add(new Tuple<bool, string>(true, build.ToString()));
                        build = new StringBuilder();
                        ctrl = false;
                    }
                    build.Append(ch);
                }
            }

            if(ctrl)
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
            foreach(Tuple<bool, string> pair in SplitControlAndNormalUtf8Characters(input))
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
            int c = CurrentRow;
            currentRow = 0;
            CurrentRow = c;
            dgvRowsVScroll.Value = c;
            if (openTable != -1)
            {
                lvTables.TopItem = lvTables.Items[openTable];
                lvTablesVScroll.Value = lvTables.TopItem.Index;
            }
            FixScrollWidth(dgvRows, dgvRowsHScroll);

        }

        private void FixScrollWidth(DataGridView dgvRows, CustomHScrollbar dgvRowsHScroll)
        {
            int width = 0;
            int x = 0;
            foreach (DataGridViewColumn col in dgvRows.Columns)
            {
                width += col.Width + col.DividerWidth;
                if(width > dgvRows.Width)
                {
                    break;
                }
                x++;
            }

            dgvRowsHScroll.Maximum = x;
            
        }

    }



    class MouseTransparentTextBox : TextBox
    {
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x020A: // WM_MOUSEWHEEL
                case 0x020E: // WM_MOUSEHWHEEL
                    if (this.ScrollBars == ScrollBars.None && this.Parent != null)
                        m.HWnd = this.Parent.Handle; // forward this to your parent
                    base.WndProc(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }

    public class VerticalFlowPanel : FlowLayoutPanel
    {
        /*
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                //cp.Style |= 0x00200000; // WS_VSCROLL
                return cp;
            }
        }
        */
    }
}
