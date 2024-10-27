namespace FauFau.SDBrowser
{
    partial class SDBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SDBrowser));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            panel3 = new System.Windows.Forms.Panel();
            lbSearchResults = new System.Windows.Forms.ListBox();
            panel9 = new System.Windows.Forms.Panel();
            panel5 = new System.Windows.Forms.Panel();
            cbSearchType = new System.Windows.Forms.ComboBox();
            panel7 = new System.Windows.Forms.Panel();
            panel8 = new System.Windows.Forms.Panel();
            tbSearchInput = new System.Windows.Forms.TextBox();
            btnSearchAll = new System.Windows.Forms.Button();
            btnSearchThis = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            btnJsonExport = new System.Windows.Forms.Button();
            DB_ImportExport = new System.Windows.Forms.Button();
            btnLoad = new System.Windows.Forms.Button();
            pnlLblLoaded = new System.Windows.Forms.Panel();
            pnlHide = new System.Windows.Forms.Panel();
            lblLoaded = new System.Windows.Forms.Label();
            lblFlags = new System.Windows.Forms.Label();
            lblCreated = new System.Windows.Forms.Label();
            lblPatch = new System.Windows.Forms.Label();
            pnlLvContainer = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            panel6 = new System.Windows.Forms.Panel();
            tbTableFilter = new System.Windows.Forms.TextBox();
            lbTables = new System.Windows.Forms.ListBox();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            dgvRows = new System.Windows.Forms.DataGridView();
            textBox1 = new System.Windows.Forms.TextBox();
            pnlInspector = new System.Windows.Forms.Panel();
            splitContainer4 = new System.Windows.Forms.SplitContainer();
            flpInspect = new VerticalFlowPanel();
            panel4 = new System.Windows.Forms.Panel();
            btnGetHash = new System.Windows.Forms.Button();
            btnDecrypt = new System.Windows.Forms.Button();
            panel10 = new System.Windows.Forms.Panel();
            panel11 = new System.Windows.Forms.Panel();
            tbxDecrypt = new System.Windows.Forms.TextBox();
            rtbOutput = new System.Windows.Forms.RichTextBox();
            odfSdb = new System.Windows.Forms.OpenFileDialog();
            contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
            searchForThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            rawCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            panel3.SuspendLayout();
            panel9.SuspendLayout();
            panel5.SuspendLayout();
            panel7.SuspendLayout();
            panel1.SuspendLayout();
            pnlLblLoaded.SuspendLayout();
            pnlLvContainer.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRows).BeginInit();
            pnlInspector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            panel4.SuspendLayout();
            panel10.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = System.Drawing.Color.FromArgb(81, 81, 81);
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer1.MinimumSize = new System.Drawing.Size(175, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1.Resize += splitContainer1_Panel1_Resize;
            splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer3);
            splitContainer1.Panel2MinSize = 500;
            splitContainer1.Size = new System.Drawing.Size(1428, 740);
            splitContainer1.SplitterDistance = 296;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer2.BackColor = System.Drawing.Color.FromArgb(81, 81, 81);
            splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(81, 81, 81);
            splitContainer2.Panel1.Controls.Add(panel3);
            splitContainer2.Panel1.Controls.Add(panel1);
            splitContainer2.Panel1MinSize = 28;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(pnlLvContainer);
            splitContainer2.Panel2MinSize = 28;
            splitContainer2.Size = new System.Drawing.Size(296, 740);
            splitContainer2.SplitterDistance = 298;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel3.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            panel3.Controls.Add(lbSearchResults);
            panel3.Controls.Add(panel9);
            panel3.Controls.Add(panel7);
            panel3.Controls.Add(btnSearchAll);
            panel3.Controls.Add(btnSearchThis);
            panel3.Location = new System.Drawing.Point(1, 110);
            panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(295, 195);
            panel3.TabIndex = 0;
            // 
            // lbSearchResults
            // 
            lbSearchResults.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lbSearchResults.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            lbSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbSearchResults.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            lbSearchResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            lbSearchResults.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            lbSearchResults.FormattingEnabled = true;
            lbSearchResults.Location = new System.Drawing.Point(0, 69);
            lbSearchResults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lbSearchResults.Name = "lbSearchResults";
            lbSearchResults.Size = new System.Drawing.Size(295, 112);
            lbSearchResults.TabIndex = 6;
            lbSearchResults.DrawItem += lbSearchResults_DrawItem;
            lbSearchResults.SelectedIndexChanged += lbSearchResults_SelectedIndexChanged;
            // 
            // panel9
            // 
            panel9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            panel9.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            panel9.Controls.Add(panel5);
            panel9.Location = new System.Drawing.Point(157, 6);
            panel9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(133, 24);
            panel9.TabIndex = 13;
            // 
            // panel5
            // 
            panel5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            panel5.Controls.Add(cbSearchType);
            panel5.Location = new System.Drawing.Point(4, -2);
            panel5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(128, 24);
            panel5.TabIndex = 8;
            // 
            // cbSearchType
            // 
            cbSearchType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            cbSearchType.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            cbSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbSearchType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            cbSearchType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            cbSearchType.ForeColor = System.Drawing.SystemColors.ScrollBar;
            cbSearchType.FormattingEnabled = true;
            cbSearchType.ItemHeight = 17;
            cbSearchType.Location = new System.Drawing.Point(-4, 0);
            cbSearchType.Margin = new System.Windows.Forms.Padding(0);
            cbSearchType.Name = "cbSearchType";
            cbSearchType.Size = new System.Drawing.Size(131, 25);
            cbSearchType.TabIndex = 10;
            // 
            // panel7
            // 
            panel7.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel7.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(tbSearchInput);
            panel7.Location = new System.Drawing.Point(5, 6);
            panel7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(148, 24);
            panel7.TabIndex = 12;
            // 
            // panel8
            // 
            panel8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            panel8.Location = new System.Drawing.Point(143, 3);
            panel8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(5, 21);
            panel8.TabIndex = 1;
            // 
            // tbSearchInput
            // 
            tbSearchInput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbSearchInput.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tbSearchInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbSearchInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            tbSearchInput.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            tbSearchInput.Location = new System.Drawing.Point(4, 6);
            tbSearchInput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbSearchInput.Name = "tbSearchInput";
            tbSearchInput.Size = new System.Drawing.Size(141, 13);
            tbSearchInput.TabIndex = 11;
            // 
            // btnSearchAll
            // 
            btnSearchAll.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnSearchAll.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            btnSearchAll.FlatAppearance.BorderSize = 0;
            btnSearchAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSearchAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btnSearchAll.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            btnSearchAll.Location = new System.Drawing.Point(157, 36);
            btnSearchAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSearchAll.Name = "btnSearchAll";
            btnSearchAll.Size = new System.Drawing.Size(133, 27);
            btnSearchAll.TabIndex = 9;
            btnSearchAll.Text = "Search all tables";
            btnSearchAll.UseVisualStyleBackColor = false;
            btnSearchAll.Click += button2_Click;
            // 
            // btnSearchThis
            // 
            btnSearchThis.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            btnSearchThis.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            btnSearchThis.FlatAppearance.BorderSize = 0;
            btnSearchThis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSearchThis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btnSearchThis.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            btnSearchThis.Location = new System.Drawing.Point(5, 36);
            btnSearchThis.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSearchThis.Name = "btnSearchThis";
            btnSearchThis.Size = new System.Drawing.Size(148, 27);
            btnSearchThis.TabIndex = 5;
            btnSearchThis.Text = "Search this table";
            btnSearchThis.UseVisualStyleBackColor = false;
            btnSearchThis.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            panel1.Controls.Add(btnJsonExport);
            panel1.Controls.Add(DB_ImportExport);
            panel1.Controls.Add(btnLoad);
            panel1.Controls.Add(pnlLblLoaded);
            panel1.Controls.Add(lblFlags);
            panel1.Controls.Add(lblCreated);
            panel1.Controls.Add(lblPatch);
            panel1.Location = new System.Drawing.Point(0, 1);
            panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(296, 105);
            panel1.TabIndex = 1;
            // 
            // btnJsonExport
            // 
            btnJsonExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnJsonExport.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            btnJsonExport.FlatAppearance.BorderSize = 0;
            btnJsonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnJsonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btnJsonExport.Location = new System.Drawing.Point(247, 74);
            btnJsonExport.Name = "btnJsonExport";
            btnJsonExport.Size = new System.Drawing.Size(44, 27);
            btnJsonExport.TabIndex = 9;
            btnJsonExport.Text = "JSON";
            btnJsonExport.UseVisualStyleBackColor = false;
            btnJsonExport.Click += btnJsonExport_Click;
            // 
            // DB_ImportExport
            // 
            DB_ImportExport.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            DB_ImportExport.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            DB_ImportExport.FlatAppearance.BorderSize = 0;
            DB_ImportExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            DB_ImportExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            DB_ImportExport.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            DB_ImportExport.Location = new System.Drawing.Point(211, 74);
            DB_ImportExport.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            DB_ImportExport.Name = "DB_ImportExport";
            DB_ImportExport.Size = new System.Drawing.Size(33, 27);
            DB_ImportExport.TabIndex = 8;
            DB_ImportExport.Text = "DB";
            DB_ImportExport.UseVisualStyleBackColor = false;
            DB_ImportExport.Click += DB_ImportExport_Click;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnLoad.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btnLoad.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            btnLoad.Location = new System.Drawing.Point(178, 74);
            btnLoad.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new System.Drawing.Size(30, 27);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "...";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;
            // 
            // pnlLblLoaded
            // 
            pnlLblLoaded.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pnlLblLoaded.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            pnlLblLoaded.Controls.Add(pnlHide);
            pnlLblLoaded.Controls.Add(lblLoaded);
            pnlLblLoaded.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            pnlLblLoaded.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            pnlLblLoaded.Location = new System.Drawing.Point(4, 74);
            pnlLblLoaded.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pnlLblLoaded.Name = "pnlLblLoaded";
            pnlLblLoaded.Size = new System.Drawing.Size(170, 27);
            pnlLblLoaded.TabIndex = 2;
            // 
            // pnlHide
            // 
            pnlHide.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            pnlHide.Location = new System.Drawing.Point(165, 3);
            pnlHide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pnlHide.Name = "pnlHide";
            pnlHide.Size = new System.Drawing.Size(5, 21);
            pnlHide.TabIndex = 1;
            // 
            // lblLoaded
            // 
            lblLoaded.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblLoaded.AutoSize = true;
            lblLoaded.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            lblLoaded.Location = new System.Drawing.Point(4, 6);
            lblLoaded.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblLoaded.Name = "lblLoaded";
            lblLoaded.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lblLoaded.Size = new System.Drawing.Size(0, 13);
            lblLoaded.TabIndex = 1;
            lblLoaded.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFlags
            // 
            lblFlags.AutoSize = true;
            lblFlags.Location = new System.Drawing.Point(4, 29);
            lblFlags.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblFlags.Name = "lblFlags";
            lblFlags.Size = new System.Drawing.Size(0, 15);
            lblFlags.TabIndex = 5;
            // 
            // lblCreated
            // 
            lblCreated.AutoSize = true;
            lblCreated.Location = new System.Drawing.Point(4, 50);
            lblCreated.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCreated.Name = "lblCreated";
            lblCreated.Size = new System.Drawing.Size(0, 15);
            lblCreated.TabIndex = 6;
            // 
            // lblPatch
            // 
            lblPatch.AutoSize = true;
            lblPatch.Location = new System.Drawing.Point(4, 9);
            lblPatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblPatch.Name = "lblPatch";
            lblPatch.Size = new System.Drawing.Size(0, 15);
            lblPatch.TabIndex = 7;
            // 
            // pnlLvContainer
            // 
            pnlLvContainer.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            pnlLvContainer.Controls.Add(panel2);
            pnlLvContainer.Controls.Add(lbTables);
            pnlLvContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlLvContainer.Location = new System.Drawing.Point(0, 0);
            pnlLvContainer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pnlLvContainer.Name = "pnlLvContainer";
            pnlLvContainer.Size = new System.Drawing.Size(296, 437);
            pnlLvContainer.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(tbTableFilter);
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(296, 27);
            panel2.TabIndex = 13;
            // 
            // panel6
            // 
            panel6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            panel6.Location = new System.Drawing.Point(292, 3);
            panel6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(5, 21);
            panel6.TabIndex = 1;
            // 
            // tbTableFilter
            // 
            tbTableFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbTableFilter.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tbTableFilter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbTableFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            tbTableFilter.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            tbTableFilter.Location = new System.Drawing.Point(4, 6);
            tbTableFilter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbTableFilter.Name = "tbTableFilter";
            tbTableFilter.Size = new System.Drawing.Size(296, 13);
            tbTableFilter.TabIndex = 12;
            tbTableFilter.TextChanged += tbTableFilter_TextChanged;
            // 
            // lbTables
            // 
            lbTables.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lbTables.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            lbTables.BorderStyle = System.Windows.Forms.BorderStyle.None;
            lbTables.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            lbTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            lbTables.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            lbTables.FormattingEnabled = true;
            lbTables.Location = new System.Drawing.Point(0, 30);
            lbTables.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            lbTables.Name = "lbTables";
            lbTables.Size = new System.Drawing.Size(296, 400);
            lbTables.TabIndex = 0;
            lbTables.DrawItem += lbTables_DrawItem;
            // 
            // splitContainer3
            // 
            splitContainer3.BackColor = System.Drawing.Color.FromArgb(81, 81, 81);
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer3.Location = new System.Drawing.Point(0, 0);
            splitContainer3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            splitContainer3.Panel1.Controls.Add(dgvRows);
            splitContainer3.Panel1.Controls.Add(textBox1);
            splitContainer3.Panel1MinSize = 400;
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            splitContainer3.Panel2.Controls.Add(pnlInspector);
            splitContainer3.Panel2MinSize = 100;
            splitContainer3.Size = new System.Drawing.Size(1127, 740);
            splitContainer3.SplitterDistance = 670;
            splitContainer3.SplitterWidth = 5;
            splitContainer3.TabIndex = 0;
            // 
            // dgvRows
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(55, 58, 60);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
            dgvRows.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvRows.BackgroundColor = System.Drawing.Color.FromArgb(60, 63, 65);
            dgvRows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dgvRows.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvRows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dgvRows.DefaultCellStyle = dataGridViewCellStyle3;
            dgvRows.Dock = System.Windows.Forms.DockStyle.Fill;
            dgvRows.EnableHeadersVisualStyles = false;
            dgvRows.GridColor = System.Drawing.Color.FromArgb(50, 50, 50);
            dgvRows.Location = new System.Drawing.Point(0, 0);
            dgvRows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            dgvRows.MultiSelect = false;
            dgvRows.Name = "dgvRows";
            dgvRows.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dgvRows.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvRows.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvRows.ShowCellToolTips = false;
            dgvRows.Size = new System.Drawing.Size(670, 740);
            dgvRows.TabIndex = 8;
            dgvRows.VirtualMode = true;
            dgvRows.CellContextMenuStripNeeded += dgvRows_CellContextMenuStripNeeded;
            dgvRows.CellDoubleClick += dgvRows_CellDoubleClick;
            dgvRows.CellMouseClick += dgvRows_CellMouseClick;
            dgvRows.CellValueNeeded += dgvRows_CellValueNeeded;
            dgvRows.SelectionChanged += dgvRows_SelectionChanged;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Location = new System.Drawing.Point(583, 91);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(117, 16);
            textBox1.TabIndex = 7;
            // 
            // pnlInspector
            // 
            pnlInspector.Controls.Add(splitContainer4);
            pnlInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlInspector.Location = new System.Drawing.Point(0, 0);
            pnlInspector.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pnlInspector.Name = "pnlInspector";
            pnlInspector.Size = new System.Drawing.Size(452, 740);
            pnlInspector.TabIndex = 0;
            // 
            // splitContainer4
            // 
            splitContainer4.BackColor = System.Drawing.Color.FromArgb(81, 81, 81);
            splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer4.Location = new System.Drawing.Point(0, 0);
            splitContainer4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            splitContainer4.Panel1.Controls.Add(flpInspect);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(panel4);
            splitContainer4.Size = new System.Drawing.Size(452, 740);
            splitContainer4.SplitterDistance = 565;
            splitContainer4.SplitterWidth = 5;
            splitContainer4.TabIndex = 0;
            // 
            // flpInspect
            // 
            flpInspect.AutoScroll = true;
            flpInspect.Dock = System.Windows.Forms.DockStyle.Fill;
            flpInspect.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flpInspect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            flpInspect.Location = new System.Drawing.Point(0, 0);
            flpInspect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            flpInspect.Name = "flpInspect";
            flpInspect.Size = new System.Drawing.Size(452, 565);
            flpInspect.TabIndex = 0;
            flpInspect.WrapContents = false;
            // 
            // panel4
            // 
            panel4.BackColor = System.Drawing.Color.FromArgb(81, 81, 81);
            panel4.Controls.Add(btnGetHash);
            panel4.Controls.Add(btnDecrypt);
            panel4.Controls.Add(panel10);
            panel4.Controls.Add(rtbOutput);
            panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            panel4.Location = new System.Drawing.Point(0, 0);
            panel4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(452, 170);
            panel4.TabIndex = 0;
            // 
            // btnGetHash
            // 
            btnGetHash.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnGetHash.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            btnGetHash.FlatAppearance.BorderSize = 0;
            btnGetHash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnGetHash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btnGetHash.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            btnGetHash.Location = new System.Drawing.Point(270, 143);
            btnGetHash.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnGetHash.Name = "btnGetHash";
            btnGetHash.Size = new System.Drawing.Size(68, 27);
            btnGetHash.TabIndex = 15;
            btnGetHash.Text = "Get hash";
            btnGetHash.UseVisualStyleBackColor = false;
            btnGetHash.Click += btnGetHash_Click;
            // 
            // btnDecrypt
            // 
            btnDecrypt.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnDecrypt.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            btnDecrypt.FlatAppearance.BorderSize = 0;
            btnDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDecrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            btnDecrypt.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            btnDecrypt.Location = new System.Drawing.Point(183, 143);
            btnDecrypt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new System.Drawing.Size(82, 27);
            btnDecrypt.TabIndex = 14;
            btnDecrypt.Text = "Try decrypt";
            btnDecrypt.UseVisualStyleBackColor = false;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // panel10
            // 
            panel10.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            panel10.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            panel10.Controls.Add(panel11);
            panel10.Controls.Add(tbxDecrypt);
            panel10.Location = new System.Drawing.Point(0, 143);
            panel10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(178, 27);
            panel10.TabIndex = 13;
            // 
            // panel11
            // 
            panel11.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            panel11.Location = new System.Drawing.Point(174, 3);
            panel11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel11.Name = "panel11";
            panel11.Size = new System.Drawing.Size(5, 21);
            panel11.TabIndex = 1;
            // 
            // tbxDecrypt
            // 
            tbxDecrypt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tbxDecrypt.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tbxDecrypt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbxDecrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            tbxDecrypt.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            tbxDecrypt.Location = new System.Drawing.Point(4, 6);
            tbxDecrypt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tbxDecrypt.Name = "tbxDecrypt";
            tbxDecrypt.Size = new System.Drawing.Size(172, 13);
            tbxDecrypt.TabIndex = 11;
            // 
            // rtbOutput
            // 
            rtbOutput.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            rtbOutput.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            rtbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            rtbOutput.ForeColor = System.Drawing.SystemColors.ScrollBar;
            rtbOutput.Location = new System.Drawing.Point(0, 0);
            rtbOutput.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.Size = new System.Drawing.Size(452, 142);
            rtbOutput.TabIndex = 8;
            rtbOutput.Text = "";
            // 
            // odfSdb
            // 
            odfSdb.FileName = "clientdb.sd2";
            odfSdb.Filter = "StaticDB|*.sd*";
            odfSdb.ReadOnlyChecked = true;
            odfSdb.FileOk += odfSdb_FileOk;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { searchForThisToolStripMenuItem, copyToolStripMenuItem, rawCopyToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(150, 70);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // searchForThisToolStripMenuItem
            // 
            searchForThisToolStripMenuItem.Name = "searchForThisToolStripMenuItem";
            searchForThisToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            searchForThisToolStripMenuItem.Text = "Search for this";
            searchForThisToolStripMenuItem.Click += searchForThisToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            copyToolStripMenuItem.Text = "Copy";
            // 
            // rawCopyToolStripMenuItem
            // 
            rawCopyToolStripMenuItem.Name = "rawCopyToolStripMenuItem";
            rawCopyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            rawCopyToolStripMenuItem.Text = "Raw copy";
            // 
            // SDBrowser
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            ClientSize = new System.Drawing.Size(1428, 740);
            Controls.Add(splitContainer1);
            ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Location = new System.Drawing.Point(15, 15);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SDBrowser";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "SDBrowser";
            Load += Form1_Load;
            ClientSizeChanged += Form1_ClientSizeChanged;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel9.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlLblLoaded.ResumeLayout(false);
            pnlLblLoaded.PerformLayout();
            pnlLvContainer.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRows).EndInit();
            pnlInspector.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Button DB_ImportExport;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel6;

        private System.Windows.Forms.TextBox tbTableFilter;

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel pnlInspector;
        private System.Windows.Forms.Panel pnlLblLoaded;
        private System.Windows.Forms.Label lblLoaded;
        private System.Windows.Forms.Label lblPatch;
        private System.Windows.Forms.Label lblCreated;
        private System.Windows.Forms.Label lblFlags;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Panel pnlHide;
        private System.Windows.Forms.Panel pnlLvContainer;
        private System.Windows.Forms.OpenFileDialog odfSdb;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSearchThis;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSearchAll;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbSearchInput;
        private System.Windows.Forms.ComboBox cbSearchType;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ListBox lbSearchResults;
        private System.Windows.Forms.RichTextBox rtbOutput;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox tbxDecrypt;
        private System.Windows.Forms.Button btnGetHash;
        private System.Windows.Forms.DataGridView dgvRows;
        private VerticalFlowPanel flpInspect;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rawCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchForThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ListBox lbTables;
        private System.Windows.Forms.Button btnJsonExport;
    }
}
