using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FauFau.Formats;
using SDBrowser;

namespace FauFau.SDBrowser
{
    public partial class DBImportExport : Form
    {
        public StaticDB Db;
        
        public DBImportExport()
        {
            InitializeComponent();
        }

        private void tb_DBConnstr_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = false;
            
            var importTask = Task.Factory.StartNew(() =>
            {
                var sw = Stopwatch.StartNew();
                LogMsg(" ======================= Importing ====================== ");

                var connStr          = $"{tbDbConnStr.Text}; Search Path={tbSchemaName.Text};";
                var importerExporter = new PgImportExport(connStr, tbSchemaName.Text, Db, LogMsg);

                if (cbDropExisting.Checked) {
                    importerExporter.DropExisting();
                }

                if (cbCreateSchema.Checked) {
                    importerExporter.CreateSchema();
                }

                if (cbImportData.Checked) {
                    importerExporter.ImportData();
                    GC.Collect();
                }

                sw.Stop();
                LogMsg($" Importing done in {sw.Elapsed}");
                
                Invoke(new MethodInvoker(() => { btnImport.Enabled = true; }));
            });
        }

        private void LogMsg(string msg, bool error = false)
        {
            rtbLogOutput.Invoke(new MethodInvoker(() =>
            {
                var line = $"{msg}\n";

                if (!error) {
                    rtbLogOutput.AppendText(line);
                    return;
                }

                LogTextColor(line, Color.Firebrick);
            }));
        }
        
        public void LogTextColor(string text, Color? color = null)
        {
            rtbLogOutput.SelectionStart  = rtbLogOutput.TextLength;
            rtbLogOutput.SelectionLength = 0;
    
            rtbLogOutput.SelectionColor = color ?? Color.FromArgb(255, 200, 200, 200);
            rtbLogOutput.AppendText(text);
            rtbLogOutput.SelectionColor = rtbLogOutput.ForeColor;
        }
    }
}