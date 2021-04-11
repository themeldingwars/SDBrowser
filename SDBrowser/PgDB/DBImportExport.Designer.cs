using System.ComponentModel;

namespace FauFau.SDBrowser
{
    partial class DBImportExport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

    #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1         = new System.Windows.Forms.Label();
            this.GB_Exporter    = new System.Windows.Forms.GroupBox();
            this.btnImport      = new System.Windows.Forms.Button();
            this.label3         = new System.Windows.Forms.Label();
            this.panel1         = new System.Windows.Forms.Panel();
            this.tbSchemaName   = new System.Windows.Forms.TextBox();
            this.panel3         = new System.Windows.Forms.Panel();
            this.textBox3       = new System.Windows.Forms.TextBox();
            this.cbImportData   = new System.Windows.Forms.CheckBox();
            this.cbDropExisting = new System.Windows.Forms.CheckBox();
            this.cbCreateSchema = new System.Windows.Forms.CheckBox();
            this.panel2         = new System.Windows.Forms.Panel();
            this.panel6         = new System.Windows.Forms.Panel();
            this.tbDbConnStr    = new System.Windows.Forms.TextBox();
            this.label2         = new System.Windows.Forms.Label();
            this.groupBox1      = new System.Windows.Forms.GroupBox();
            this.rtbLogOutput   = new System.Windows.Forms.RichTextBox();
            this.GB_Exporter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font      = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label1.Location  = new System.Drawing.Point(12, 9);
            this.label1.Name      = "label1";
            this.label1.Size      = new System.Drawing.Size(344, 34);
            this.label1.TabIndex  = 0;
            this.label1.Text      = "DB Importer And Exporter";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GB_Exporter
            // 
            this.GB_Exporter.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.GB_Exporter.Controls.Add(this.btnImport);
            this.GB_Exporter.Controls.Add(this.label3);
            this.GB_Exporter.Controls.Add(this.panel1);
            this.GB_Exporter.Controls.Add(this.cbImportData);
            this.GB_Exporter.Controls.Add(this.cbDropExisting);
            this.GB_Exporter.Controls.Add(this.cbCreateSchema);
            this.GB_Exporter.Controls.Add(this.panel2);
            this.GB_Exporter.Controls.Add(this.label2);
            this.GB_Exporter.ForeColor = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.GB_Exporter.Location  = new System.Drawing.Point(12, 46);
            this.GB_Exporter.Name      = "GB_Exporter";
            this.GB_Exporter.Size      = new System.Drawing.Size(344, 172);
            this.GB_Exporter.TabIndex  = 1;
            this.GB_Exporter.TabStop   = false;
            this.GB_Exporter.Text      = "Exporter";
            // 
            // btnImport
            // 
            this.btnImport.Anchor                    =  ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.BackColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
            this.btnImport.FlatAppearance.BorderSize =  0;
            this.btnImport.FlatStyle                 =  System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.ForeColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.btnImport.Location                  =  new System.Drawing.Point(6, 143);
            this.btnImport.Name                      =  "btnImport";
            this.btnImport.Size                      =  new System.Drawing.Size(332, 23);
            this.btnImport.TabIndex                  =  26;
            this.btnImport.Text                      =  "Import";
            this.btnImport.UseVisualStyleBackColor   =  false;
            this.btnImport.Click                     += new System.EventHandler(this.btnImport_Click);
            // 
            // label3
            // 
            this.label3.Location  = new System.Drawing.Point(6, 65);
            this.label3.Name      = "label3";
            this.label3.Size      = new System.Drawing.Size(103, 23);
            this.label3.TabIndex  = 25;
            this.label3.Text      = "Schema Name: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor    = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
            this.panel1.Controls.Add(this.tbSchemaName);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Location = new System.Drawing.Point(115, 65);
            this.panel1.Name     = "panel1";
            this.panel1.Size     = new System.Drawing.Size(223, 23);
            this.panel1.TabIndex = 22;
            // 
            // tbSchemaName
            // 
            this.tbSchemaName.Anchor      = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSchemaName.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
            this.tbSchemaName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSchemaName.ForeColor   = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.tbSchemaName.Location    = new System.Drawing.Point(5, 5);
            this.tbSchemaName.Margin      = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.tbSchemaName.Name        = "tbSchemaName";
            this.tbSchemaName.Size        = new System.Drawing.Size(223, 13);
            this.tbSchemaName.TabIndex    = 13;
            this.tbSchemaName.Text        = "SDB";
            // 
            // panel3
            // 
            this.panel3.Anchor   = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Location = new System.Drawing.Point(219, 3);
            this.panel3.Name     = "panel3";
            this.panel3.Size     = new System.Drawing.Size(4, 18);
            this.panel3.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Anchor      = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor   = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.textBox3.Location    = new System.Drawing.Point(3, 5);
            this.textBox3.Name        = "textBox3";
            this.textBox3.Size        = new System.Drawing.Size(223, 13);
            this.textBox3.TabIndex    = 12;
            // 
            // cbImportData
            // 
            this.cbImportData.Checked                 = true;
            this.cbImportData.CheckState              = System.Windows.Forms.CheckState.Checked;
            this.cbImportData.Location                = new System.Drawing.Point(6, 119);
            this.cbImportData.Name                    = "cbImportData";
            this.cbImportData.Size                    = new System.Drawing.Size(131, 19);
            this.cbImportData.TabIndex                = 24;
            this.cbImportData.Text                    = "Import Data";
            this.cbImportData.UseVisualStyleBackColor = true;
            // 
            // cbDropExisting
            // 
            this.cbDropExisting.Anchor                  = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDropExisting.Checked                 = true;
            this.cbDropExisting.CheckState              = System.Windows.Forms.CheckState.Checked;
            this.cbDropExisting.Location                = new System.Drawing.Point(193, 94);
            this.cbDropExisting.Name                    = "cbDropExisting";
            this.cbDropExisting.Size                    = new System.Drawing.Size(145, 19);
            this.cbDropExisting.TabIndex                = 23;
            this.cbDropExisting.Text                    = "Drop existing";
            this.cbDropExisting.UseVisualStyleBackColor = true;
            // 
            // cbCreateSchema
            // 
            this.cbCreateSchema.Checked                 = true;
            this.cbCreateSchema.CheckState              = System.Windows.Forms.CheckState.Checked;
            this.cbCreateSchema.Location                = new System.Drawing.Point(6, 94);
            this.cbCreateSchema.Name                    = "cbCreateSchema";
            this.cbCreateSchema.Size                    = new System.Drawing.Size(131, 19);
            this.cbCreateSchema.TabIndex                = 22;
            this.cbCreateSchema.Text                    = "Create Schema";
            this.cbCreateSchema.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor    = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.tbDbConnStr);
            this.panel2.Location = new System.Drawing.Point(6, 36);
            this.panel2.Name     = "panel2";
            this.panel2.Size     = new System.Drawing.Size(332, 23);
            this.panel2.TabIndex = 21;
            // 
            // panel6
            // 
            this.panel6.Anchor   = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.Location = new System.Drawing.Point(328, 3);
            this.panel6.Name     = "panel6";
            this.panel6.Size     = new System.Drawing.Size(4, 18);
            this.panel6.TabIndex = 1;
            // 
            // tbDbConnStr
            // 
            this.tbDbConnStr.Anchor      = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDbConnStr.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
            this.tbDbConnStr.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDbConnStr.ForeColor   = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.tbDbConnStr.Location    = new System.Drawing.Point(3, 5);
            this.tbDbConnStr.Name        = "tbDbConnStr";
            this.tbDbConnStr.Size        = new System.Drawing.Size(332, 13);
            this.tbDbConnStr.TabIndex    = 12;
            this.tbDbConnStr.Text        = "User ID=user;Password=pass;Host=localhost;Port=5434;Database=TMW;";
            // 
            // label2
            // 
            this.label2.Anchor        = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location      = new System.Drawing.Point(6, 16);
            this.label2.Name          = "label2";
            this.label2.Size          = new System.Drawing.Size(332, 17);
            this.label2.TabIndex      = 0;
            this.label2.Text          = "Postgres DB Connection String: ";
            this.label2.UseWaitCursor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rtbLogOutput);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.groupBox1.Location  = new System.Drawing.Point(12, 224);
            this.groupBox1.Name      = "groupBox1";
            this.groupBox1.Size      = new System.Drawing.Size(344, 388);
            this.groupBox1.TabIndex  = 27;
            this.groupBox1.TabStop   = false;
            this.groupBox1.Text      = "Log Output";
            // 
            // rtbLogOutput
            // 
            this.rtbLogOutput.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
            this.rtbLogOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLogOutput.Dock        = System.Windows.Forms.DockStyle.Fill;
            this.rtbLogOutput.ForeColor   = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.rtbLogOutput.Location    = new System.Drawing.Point(3, 16);
            this.rtbLogOutput.Name        = "rtbLogOutput";
            this.rtbLogOutput.Size        = new System.Drawing.Size(338, 369);
            this.rtbLogOutput.TabIndex    = 0;
            this.rtbLogOutput.Text        = "";
            // 
            // DBImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
            this.ClientSize          = new System.Drawing.Size(368, 624);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GB_Exporter);
            this.Controls.Add(this.label1);
            this.ForeColor         = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
            this.Name              = "DBImportExport";
            this.RightToLeftLayout = true;
            this.Text              = "DB Importer Exporter";
            this.GB_Exporter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RichTextBox rtbLogOutput;

        private System.Windows.Forms.RichTextBox richTextBox1;

        private System.Windows.Forms.TextBox tbSchemaName;

        private System.Windows.Forms.Button btnImport;

        private System.Windows.Forms.Button btnLoad;

        private System.Windows.Forms.Panel   panel1;
        private System.Windows.Forms.Panel   panel3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label   label3;

        private System.Windows.Forms.CheckBox cbDropExisting;

        private System.Windows.Forms.CheckBox cbImportData;

        private System.Windows.Forms.Panel    panel2;
        private System.Windows.Forms.Panel    panel6;
        private System.Windows.Forms.TextBox  tbDbConnStr;
        private System.Windows.Forms.CheckBox cbCreateSchema;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.GroupBox GB_Exporter;

        private System.Windows.Forms.Label    label1;
        private System.Windows.Forms.GroupBox groupBox1;

    #endregion
    }
}