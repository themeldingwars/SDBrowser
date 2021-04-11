namespace FauFau.SDBrowser
{
    partial class Form1
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
		        this.components = new System.ComponentModel.Container();
		        System.Windows.Forms.DataGridViewCellStyle     dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		        System.Windows.Forms.DataGridViewCellStyle     dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		        System.Windows.Forms.DataGridViewCellStyle     dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		        System.Windows.Forms.DataGridViewCellStyle     dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		        System.ComponentModel.ComponentResourceManager resources              = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
		        this.splitContainer1                = new System.Windows.Forms.SplitContainer();
		        this.splitContainer2                = new System.Windows.Forms.SplitContainer();
		        this.panel3                         = new System.Windows.Forms.Panel();
		        this.lbSearchResults                = new System.Windows.Forms.ListBox();
		        this.panel9                         = new System.Windows.Forms.Panel();
		        this.panel5                         = new System.Windows.Forms.Panel();
		        this.cbSearchType                   = new System.Windows.Forms.ComboBox();
		        this.panel7                         = new System.Windows.Forms.Panel();
		        this.panel8                         = new System.Windows.Forms.Panel();
		        this.tbSearchInput                  = new System.Windows.Forms.TextBox();
		        this.btnSearchAll                   = new System.Windows.Forms.Button();
		        this.btnSearchThis                  = new System.Windows.Forms.Button();
		        this.panel1                         = new System.Windows.Forms.Panel();
		        this.DB_ImportExport                = new System.Windows.Forms.Button();
		        this.btnLoad                        = new System.Windows.Forms.Button();
		        this.pnlLblLoaded                   = new System.Windows.Forms.Panel();
		        this.pnlHide                        = new System.Windows.Forms.Panel();
		        this.lblLoaded                      = new System.Windows.Forms.Label();
		        this.lblFlags                       = new System.Windows.Forms.Label();
		        this.lblCreated                     = new System.Windows.Forms.Label();
		        this.lblPatch                       = new System.Windows.Forms.Label();
		        this.pnlLvContainer                 = new System.Windows.Forms.Panel();
		        this.panel2                         = new System.Windows.Forms.Panel();
		        this.panel6                         = new System.Windows.Forms.Panel();
		        this.tbTableFilter                  = new System.Windows.Forms.TextBox();
		        this.lbTables                       = new System.Windows.Forms.ListBox();
		        this.splitContainer3                = new System.Windows.Forms.SplitContainer();
		        this.dgvRows                        = new System.Windows.Forms.DataGridView();
		        this.textBox1                       = new System.Windows.Forms.TextBox();
		        this.pnlInspector                   = new System.Windows.Forms.Panel();
		        this.splitContainer4                = new System.Windows.Forms.SplitContainer();
		        this.flpInspect                     = new FauFau.SDBrowser.VerticalFlowPanel();
		        this.panel4                         = new System.Windows.Forms.Panel();
		        this.btnGetHash                     = new System.Windows.Forms.Button();
		        this.btnDecrypt                     = new System.Windows.Forms.Button();
		        this.panel10                        = new System.Windows.Forms.Panel();
		        this.panel11                        = new System.Windows.Forms.Panel();
		        this.tbxDecrypt                     = new System.Windows.Forms.TextBox();
		        this.rtbOutput                      = new System.Windows.Forms.RichTextBox();
		        this.odfSdb                         = new System.Windows.Forms.OpenFileDialog();
		        this.contextMenuStrip1              = new System.Windows.Forms.ContextMenuStrip(this.components);
		        this.searchForThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		        this.copyToolStripMenuItem          = new System.Windows.Forms.ToolStripMenuItem();
		        this.rawCopyToolStripMenuItem       = new System.Windows.Forms.ToolStripMenuItem();
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).BeginInit();
		        this.splitContainer1.Panel1.SuspendLayout();
		        this.splitContainer1.Panel2.SuspendLayout();
		        this.splitContainer1.SuspendLayout();
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).BeginInit();
		        this.splitContainer2.Panel1.SuspendLayout();
		        this.splitContainer2.Panel2.SuspendLayout();
		        this.splitContainer2.SuspendLayout();
		        this.panel3.SuspendLayout();
		        this.panel9.SuspendLayout();
		        this.panel5.SuspendLayout();
		        this.panel7.SuspendLayout();
		        this.panel1.SuspendLayout();
		        this.pnlLblLoaded.SuspendLayout();
		        this.pnlLvContainer.SuspendLayout();
		        this.panel2.SuspendLayout();
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer3)).BeginInit();
		        this.splitContainer3.Panel1.SuspendLayout();
		        this.splitContainer3.Panel2.SuspendLayout();
		        this.splitContainer3.SuspendLayout();
		        ((System.ComponentModel.ISupportInitialize) (this.dgvRows)).BeginInit();
		        this.pnlInspector.SuspendLayout();
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer4)).BeginInit();
		        this.splitContainer4.Panel1.SuspendLayout();
		        this.splitContainer4.Panel2.SuspendLayout();
		        this.splitContainer4.SuspendLayout();
		        this.panel4.SuspendLayout();
		        this.panel10.SuspendLayout();
		        this.contextMenuStrip1.SuspendLayout();
		        this.SuspendLayout();
		        // 
		        // splitContainer1
		        // 
		        this.splitContainer1.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (81)))), ((int) (((byte) (81)))), ((int) (((byte) (81)))));
		        this.splitContainer1.Dock        = System.Windows.Forms.DockStyle.Fill;
		        this.splitContainer1.FixedPanel  = System.Windows.Forms.FixedPanel.Panel1;
		        this.splitContainer1.Location    = new System.Drawing.Point(0, 0);
		        this.splitContainer1.MinimumSize = new System.Drawing.Size(150, 0);
		        this.splitContainer1.Name        = "splitContainer1";
		        // 
		        // splitContainer1.Panel1
		        // 
		        this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
		        this.splitContainer1.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
		        this.splitContainer1.Panel1MinSize =  150;
		        // 
		        // splitContainer1.Panel2
		        // 
		        this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
		        this.splitContainer1.Panel2MinSize    = 500;
		        this.splitContainer1.Size             = new System.Drawing.Size(1224, 641);
		        this.splitContainer1.SplitterDistance = 296;
		        this.splitContainer1.TabIndex         = 0;
		        // 
		        // splitContainer2
		        // 
		        this.splitContainer2.Anchor      = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.splitContainer2.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (81)))), ((int) (((byte) (81)))), ((int) (((byte) (81)))));
		        this.splitContainer2.FixedPanel  = System.Windows.Forms.FixedPanel.Panel1;
		        this.splitContainer2.Location    = new System.Drawing.Point(0, 0);
		        this.splitContainer2.Name        = "splitContainer2";
		        this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		        // 
		        // splitContainer2.Panel1
		        // 
		        this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (81)))), ((int) (((byte) (81)))), ((int) (((byte) (81)))));
		        this.splitContainer2.Panel1.Controls.Add(this.panel3);
		        this.splitContainer2.Panel1.Controls.Add(this.panel1);
		        this.splitContainer2.Panel1MinSize = 28;
		        // 
		        // splitContainer2.Panel2
		        // 
		        this.splitContainer2.Panel2.Controls.Add(this.pnlLvContainer);
		        this.splitContainer2.Panel2MinSize    = 28;
		        this.splitContainer2.Size             = new System.Drawing.Size(296, 641);
		        this.splitContainer2.SplitterDistance = 298;
		        this.splitContainer2.TabIndex         = 0;
		        // 
		        // panel3
		        // 
		        this.panel3.Anchor    = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel3.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.panel3.Controls.Add(this.lbSearchResults);
		        this.panel3.Controls.Add(this.panel9);
		        this.panel3.Controls.Add(this.panel7);
		        this.panel3.Controls.Add(this.btnSearchAll);
		        this.panel3.Controls.Add(this.btnSearchThis);
		        this.panel3.Location = new System.Drawing.Point(1, 95);
		        this.panel3.Name     = "panel3";
		        this.panel3.Size     = new System.Drawing.Size(295, 209);
		        this.panel3.TabIndex = 0;
		        // 
		        // lbSearchResults
		        // 
		        this.lbSearchResults.Anchor               =  ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.lbSearchResults.BackColor            =  System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.lbSearchResults.BorderStyle          =  System.Windows.Forms.BorderStyle.None;
		        this.lbSearchResults.DrawMode             =  System.Windows.Forms.DrawMode.OwnerDrawFixed;
		        this.lbSearchResults.ForeColor            =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.lbSearchResults.FormattingEnabled    =  true;
		        this.lbSearchResults.Location             =  new System.Drawing.Point(0, 60);
		        this.lbSearchResults.Name                 =  "lbSearchResults";
		        this.lbSearchResults.Size                 =  new System.Drawing.Size(295, 143);
		        this.lbSearchResults.TabIndex             =  6;
		        this.lbSearchResults.DrawItem             += new System.Windows.Forms.DrawItemEventHandler(this.lbSearchResults_DrawItem);
		        this.lbSearchResults.SelectedIndexChanged += new System.EventHandler(this.lbSearchResults_SelectedIndexChanged);
		        // 
		        // panel9
		        // 
		        this.panel9.Anchor    = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel9.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.panel9.Controls.Add(this.panel5);
		        this.panel9.Location = new System.Drawing.Point(177, 5);
		        this.panel9.Name     = "panel9";
		        this.panel9.Size     = new System.Drawing.Size(114, 23);
		        this.panel9.TabIndex = 13;
		        // 
		        // panel5
		        // 
		        this.panel5.Anchor    = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
		        this.panel5.Controls.Add(this.cbSearchType);
		        this.panel5.Location = new System.Drawing.Point(3, 1);
		        this.panel5.Name     = "panel5";
		        this.panel5.Size     = new System.Drawing.Size(110, 21);
		        this.panel5.TabIndex = 8;
		        // 
		        // cbSearchType
		        // 
		        this.cbSearchType.Anchor            = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		        this.cbSearchType.BackColor         = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.cbSearchType.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
		        this.cbSearchType.FlatStyle         = System.Windows.Forms.FlatStyle.Flat;
		        this.cbSearchType.Font              = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
		        this.cbSearchType.FormattingEnabled = true;
		        this.cbSearchType.Location          = new System.Drawing.Point(-3, -1);
		        this.cbSearchType.Margin            = new System.Windows.Forms.Padding(0);
		        this.cbSearchType.Name              = "cbSearchType";
		        this.cbSearchType.Size              = new System.Drawing.Size(113, 23);
		        this.cbSearchType.TabIndex          = 10;
		        // 
		        // panel7
		        // 
		        this.panel7.Anchor    = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel7.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.panel7.Controls.Add(this.panel8);
		        this.panel7.Controls.Add(this.tbSearchInput);
		        this.panel7.Location = new System.Drawing.Point(4, 5);
		        this.panel7.Name     = "panel7";
		        this.panel7.Size     = new System.Drawing.Size(169, 23);
		        this.panel7.TabIndex = 12;
		        // 
		        // panel8
		        // 
		        this.panel8.Anchor   = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel8.Location = new System.Drawing.Point(165, 3);
		        this.panel8.Name     = "panel8";
		        this.panel8.Size     = new System.Drawing.Size(4, 18);
		        this.panel8.TabIndex = 1;
		        // 
		        // tbSearchInput
		        // 
		        this.tbSearchInput.Anchor      = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.tbSearchInput.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.tbSearchInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
		        this.tbSearchInput.ForeColor   = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.tbSearchInput.Location    = new System.Drawing.Point(3, 5);
		        this.tbSearchInput.Name        = "tbSearchInput";
		        this.tbSearchInput.Size        = new System.Drawing.Size(163, 13);
		        this.tbSearchInput.TabIndex    = 11;
		        // 
		        // btnSearchAll
		        // 
		        this.btnSearchAll.Anchor                    =  ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		        this.btnSearchAll.BackColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.btnSearchAll.FlatAppearance.BorderSize =  0;
		        this.btnSearchAll.FlatStyle                 =  System.Windows.Forms.FlatStyle.Flat;
		        this.btnSearchAll.Font                      =  new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		        this.btnSearchAll.ForeColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.btnSearchAll.Location                  =  new System.Drawing.Point(177, 31);
		        this.btnSearchAll.Name                      =  "btnSearchAll";
		        this.btnSearchAll.Size                      =  new System.Drawing.Size(114, 23);
		        this.btnSearchAll.TabIndex                  =  9;
		        this.btnSearchAll.Text                      =  "Search all tables";
		        this.btnSearchAll.UseVisualStyleBackColor   =  false;
		        this.btnSearchAll.Click                     += new System.EventHandler(this.button2_Click);
		        // 
		        // btnSearchThis
		        // 
		        this.btnSearchThis.Anchor                    =  ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.btnSearchThis.BackColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.btnSearchThis.FlatAppearance.BorderSize =  0;
		        this.btnSearchThis.FlatStyle                 =  System.Windows.Forms.FlatStyle.Flat;
		        this.btnSearchThis.Font                      =  new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
		        this.btnSearchThis.ForeColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.btnSearchThis.Location                  =  new System.Drawing.Point(4, 31);
		        this.btnSearchThis.Name                      =  "btnSearchThis";
		        this.btnSearchThis.Size                      =  new System.Drawing.Size(169, 23);
		        this.btnSearchThis.TabIndex                  =  5;
		        this.btnSearchThis.Text                      =  "Search this table";
		        this.btnSearchThis.UseVisualStyleBackColor   =  false;
		        this.btnSearchThis.Click                     += new System.EventHandler(this.button1_Click);
		        // 
		        // panel1
		        // 
		        this.panel1.Anchor    = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.panel1.Controls.Add(this.DB_ImportExport);
		        this.panel1.Controls.Add(this.btnLoad);
		        this.panel1.Controls.Add(this.pnlLblLoaded);
		        this.panel1.Controls.Add(this.lblFlags);
		        this.panel1.Controls.Add(this.lblCreated);
		        this.panel1.Controls.Add(this.lblPatch);
		        this.panel1.Location = new System.Drawing.Point(0, 1);
		        this.panel1.Name     = "panel1";
		        this.panel1.Size     = new System.Drawing.Size(296, 91);
		        this.panel1.TabIndex = 1;
		        // 
		        // DB_ImportExport
		        // 
		        this.DB_ImportExport.Anchor                    =  ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		        this.DB_ImportExport.BackColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.DB_ImportExport.FlatAppearance.BorderSize =  0;
		        this.DB_ImportExport.FlatStyle                 =  System.Windows.Forms.FlatStyle.Flat;
		        this.DB_ImportExport.ForeColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.DB_ImportExport.Location                  =  new System.Drawing.Point(256, 64);
		        this.DB_ImportExport.Name                      =  "DB_ImportExport";
		        this.DB_ImportExport.Size                      =  new System.Drawing.Size(35, 23);
		        this.DB_ImportExport.TabIndex                  =  8;
		        this.DB_ImportExport.Text                      =  "DB";
		        this.DB_ImportExport.UseVisualStyleBackColor   =  false;
		        this.DB_ImportExport.Click                     += new System.EventHandler(this.DB_ImportExport_Click);
		        // 
		        // btnLoad
		        // 
		        this.btnLoad.Anchor                    =  ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		        this.btnLoad.BackColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.btnLoad.FlatAppearance.BorderSize =  0;
		        this.btnLoad.FlatStyle                 =  System.Windows.Forms.FlatStyle.Flat;
		        this.btnLoad.ForeColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.btnLoad.Location                  =  new System.Drawing.Point(226, 64);
		        this.btnLoad.Name                      =  "btnLoad";
		        this.btnLoad.Size                      =  new System.Drawing.Size(26, 23);
		        this.btnLoad.TabIndex                  =  3;
		        this.btnLoad.Text                      =  "...";
		        this.btnLoad.UseVisualStyleBackColor   =  false;
		        this.btnLoad.Click                     += new System.EventHandler(this.btnLoad_Click);
		        // 
		        // pnlLblLoaded
		        // 
		        this.pnlLblLoaded.Anchor    = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.pnlLblLoaded.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.pnlLblLoaded.Controls.Add(this.pnlHide);
		        this.pnlLblLoaded.Controls.Add(this.lblLoaded);
		        this.pnlLblLoaded.ForeColor = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.pnlLblLoaded.Location  = new System.Drawing.Point(3, 64);
		        this.pnlLblLoaded.Name      = "pnlLblLoaded";
		        this.pnlLblLoaded.Size      = new System.Drawing.Size(217, 23);
		        this.pnlLblLoaded.TabIndex  = 2;
		        // 
		        // pnlHide
		        // 
		        this.pnlHide.Anchor   = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		        this.pnlHide.Location = new System.Drawing.Point(213, 3);
		        this.pnlHide.Name     = "pnlHide";
		        this.pnlHide.Size     = new System.Drawing.Size(4, 18);
		        this.pnlHide.TabIndex = 1;
		        // 
		        // lblLoaded
		        // 
		        this.lblLoaded.Anchor      = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.lblLoaded.AutoSize    = true;
		        this.lblLoaded.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.lblLoaded.Location    = new System.Drawing.Point(3, 5);
		        this.lblLoaded.Name        = "lblLoaded";
		        this.lblLoaded.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		        this.lblLoaded.Size        = new System.Drawing.Size(0, 13);
		        this.lblLoaded.TabIndex    = 1;
		        this.lblLoaded.TextAlign   = System.Drawing.ContentAlignment.TopRight;
		        // 
		        // lblFlags
		        // 
		        this.lblFlags.AutoSize = true;
		        this.lblFlags.Location = new System.Drawing.Point(3, 25);
		        this.lblFlags.Name     = "lblFlags";
		        this.lblFlags.Size     = new System.Drawing.Size(0, 13);
		        this.lblFlags.TabIndex = 5;
		        // 
		        // lblCreated
		        // 
		        this.lblCreated.AutoSize = true;
		        this.lblCreated.Location = new System.Drawing.Point(3, 43);
		        this.lblCreated.Name     = "lblCreated";
		        this.lblCreated.Size     = new System.Drawing.Size(0, 13);
		        this.lblCreated.TabIndex = 6;
		        // 
		        // lblPatch
		        // 
		        this.lblPatch.AutoSize = true;
		        this.lblPatch.Location = new System.Drawing.Point(3, 8);
		        this.lblPatch.Name     = "lblPatch";
		        this.lblPatch.Size     = new System.Drawing.Size(0, 13);
		        this.lblPatch.TabIndex = 7;
		        // 
		        // pnlLvContainer
		        // 
		        this.pnlLvContainer.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.pnlLvContainer.Controls.Add(this.panel2);
		        this.pnlLvContainer.Controls.Add(this.lbTables);
		        this.pnlLvContainer.Dock     = System.Windows.Forms.DockStyle.Fill;
		        this.pnlLvContainer.Location = new System.Drawing.Point(0, 0);
		        this.pnlLvContainer.Name     = "pnlLvContainer";
		        this.pnlLvContainer.Size     = new System.Drawing.Size(296, 339);
		        this.pnlLvContainer.TabIndex = 5;
		        // 
		        // panel2
		        // 
		        this.panel2.Anchor    = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel2.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.panel2.Controls.Add(this.panel6);
		        this.panel2.Controls.Add(this.tbTableFilter);
		        this.panel2.Location = new System.Drawing.Point(0, 0);
		        this.panel2.Name     = "panel2";
		        this.panel2.Size     = new System.Drawing.Size(296, 23);
		        this.panel2.TabIndex = 13;
		        // 
		        // panel6
		        // 
		        this.panel6.Anchor   = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel6.Location = new System.Drawing.Point(292, 3);
		        this.panel6.Name     = "panel6";
		        this.panel6.Size     = new System.Drawing.Size(4, 18);
		        this.panel6.TabIndex = 1;
		        // 
		        // tbTableFilter
		        // 
		        this.tbTableFilter.Anchor      =  ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.tbTableFilter.BackColor   =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.tbTableFilter.BorderStyle =  System.Windows.Forms.BorderStyle.None;
		        this.tbTableFilter.ForeColor   =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.tbTableFilter.Location    =  new System.Drawing.Point(3, 5);
		        this.tbTableFilter.Name        =  "tbTableFilter";
		        this.tbTableFilter.Size        =  new System.Drawing.Size(296, 13);
		        this.tbTableFilter.TabIndex    =  12;
		        this.tbTableFilter.TextChanged += new System.EventHandler(this.tbTableFilter_TextChanged);
		        // 
		        // lbTables
		        // 
		        this.lbTables.Anchor            =  ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.lbTables.BackColor         =  System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.lbTables.BorderStyle       =  System.Windows.Forms.BorderStyle.None;
		        this.lbTables.DrawMode          =  System.Windows.Forms.DrawMode.OwnerDrawFixed;
		        this.lbTables.Font              =  new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
		        this.lbTables.ForeColor         =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.lbTables.FormattingEnabled =  true;
		        this.lbTables.Location          =  new System.Drawing.Point(0, 26);
		        this.lbTables.Name              =  "lbTables";
		        this.lbTables.Size              =  new System.Drawing.Size(296, 312);
		        this.lbTables.TabIndex          =  0;
		        this.lbTables.DrawItem          += new System.Windows.Forms.DrawItemEventHandler(this.lbTables_DrawItem);
		        // 
		        // splitContainer3
		        // 
		        this.splitContainer3.BackColor  = System.Drawing.Color.FromArgb(((int) (((byte) (81)))), ((int) (((byte) (81)))), ((int) (((byte) (81)))));
		        this.splitContainer3.Dock       = System.Windows.Forms.DockStyle.Fill;
		        this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		        this.splitContainer3.Location   = new System.Drawing.Point(0, 0);
		        this.splitContainer3.Name       = "splitContainer3";
		        // 
		        // splitContainer3.Panel1
		        // 
		        this.splitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.splitContainer3.Panel1.Controls.Add(this.dgvRows);
		        this.splitContainer3.Panel1.Controls.Add(this.textBox1);
		        this.splitContainer3.Panel1MinSize = 400;
		        // 
		        // splitContainer3.Panel2
		        // 
		        this.splitContainer3.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
		        this.splitContainer3.Panel2.Controls.Add(this.pnlInspector);
		        this.splitContainer3.Panel2MinSize    = 100;
		        this.splitContainer3.Size             = new System.Drawing.Size(924, 641);
		        this.splitContainer3.SplitterDistance = 476;
		        this.splitContainer3.TabIndex         = 0;
		        // 
		        // dgvRows
		        // 
		        dataGridViewCellStyle1.BackColor             =  System.Drawing.Color.FromArgb(((int) (((byte) (55)))), ((int) (((byte) (58)))), ((int) (((byte) (60)))));
		        dataGridViewCellStyle1.ForeColor             =  System.Drawing.Color.FromArgb(((int) (((byte) (224)))), ((int) (((byte) (224)))), ((int) (((byte) (224)))));
		        this.dgvRows.AlternatingRowsDefaultCellStyle =  dataGridViewCellStyle1;
		        this.dgvRows.BackgroundColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.dgvRows.BorderStyle                     =  System.Windows.Forms.BorderStyle.None;
		        this.dgvRows.ColumnHeadersBorderStyle        =  System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		        dataGridViewCellStyle2.Alignment             =  System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		        dataGridViewCellStyle2.Font                  =  new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
		        dataGridViewCellStyle2.WrapMode              =  System.Windows.Forms.DataGridViewTriState.True;
		        this.dgvRows.ColumnHeadersDefaultCellStyle   =  dataGridViewCellStyle2;
		        this.dgvRows.ColumnHeadersHeightSizeMode     =  System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		        dataGridViewCellStyle3.Alignment             =  System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		        dataGridViewCellStyle3.BackColor             =  System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        dataGridViewCellStyle3.Font                  =  new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
		        dataGridViewCellStyle3.ForeColor             =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        dataGridViewCellStyle3.SelectionBackColor    =  System.Drawing.Color.Gray;
		        dataGridViewCellStyle3.SelectionForeColor    =  System.Drawing.Color.FromArgb(((int) (((byte) (224)))), ((int) (((byte) (224)))), ((int) (((byte) (224)))));
		        dataGridViewCellStyle3.WrapMode              =  System.Windows.Forms.DataGridViewTriState.False;
		        this.dgvRows.DefaultCellStyle                =  dataGridViewCellStyle3;
		        this.dgvRows.Dock                            =  System.Windows.Forms.DockStyle.Fill;
		        this.dgvRows.EnableHeadersVisualStyles       =  false;
		        this.dgvRows.GridColor                       =  System.Drawing.Color.FromArgb(((int) (((byte) (50)))), ((int) (((byte) (50)))), ((int) (((byte) (50)))));
		        this.dgvRows.Location                        =  new System.Drawing.Point(0, 0);
		        this.dgvRows.MultiSelect                     =  false;
		        this.dgvRows.Name                            =  "dgvRows";
		        this.dgvRows.RowHeadersBorderStyle           =  System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		        dataGridViewCellStyle4.Alignment             =  System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		        dataGridViewCellStyle4.Font                  =  new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
		        dataGridViewCellStyle4.WrapMode              =  System.Windows.Forms.DataGridViewTriState.True;
		        this.dgvRows.RowHeadersDefaultCellStyle      =  dataGridViewCellStyle4;
		        this.dgvRows.SelectionMode                   =  System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		        this.dgvRows.Size                            =  new System.Drawing.Size(476, 641);
		        this.dgvRows.TabIndex                        =  8;
		        this.dgvRows.VirtualMode                     =  true;
		        this.dgvRows.CellContextMenuStripNeeded      += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgvRows_CellContextMenuStripNeeded);
		        this.dgvRows.CellDoubleClick                 += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRows_CellDoubleClick);
		        this.dgvRows.CellMouseClick                  += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvRows_CellMouseClick);
		        this.dgvRows.CellValueNeeded                 += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.dgvRows_CellValueNeeded);
		        this.dgvRows.SelectionChanged                += new System.EventHandler(this.dgvRows_SelectionChanged);
		        // 
		        // textBox1
		        // 
		        this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		        this.textBox1.Location    = new System.Drawing.Point(500, 79);
		        this.textBox1.Name        = "textBox1";
		        this.textBox1.Size        = new System.Drawing.Size(100, 13);
		        this.textBox1.TabIndex    = 7;
		        // 
		        // pnlInspector
		        // 
		        this.pnlInspector.Controls.Add(this.splitContainer4);
		        this.pnlInspector.Dock     = System.Windows.Forms.DockStyle.Fill;
		        this.pnlInspector.Location = new System.Drawing.Point(0, 0);
		        this.pnlInspector.Name     = "pnlInspector";
		        this.pnlInspector.Size     = new System.Drawing.Size(444, 641);
		        this.pnlInspector.TabIndex = 0;
		        // 
		        // splitContainer4
		        // 
		        this.splitContainer4.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (81)))), ((int) (((byte) (81)))), ((int) (((byte) (81)))));
		        this.splitContainer4.Dock        = System.Windows.Forms.DockStyle.Fill;
		        this.splitContainer4.Location    = new System.Drawing.Point(0, 0);
		        this.splitContainer4.Name        = "splitContainer4";
		        this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
		        // 
		        // splitContainer4.Panel1
		        // 
		        this.splitContainer4.Panel1.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.splitContainer4.Panel1.Controls.Add(this.flpInspect);
		        // 
		        // splitContainer4.Panel2
		        // 
		        this.splitContainer4.Panel2.Controls.Add(this.panel4);
		        this.splitContainer4.Size             = new System.Drawing.Size(444, 641);
		        this.splitContainer4.SplitterDistance = 490;
		        this.splitContainer4.TabIndex         = 0;
		        // 
		        // flpInspect
		        // 
		        this.flpInspect.AutoScroll    = true;
		        this.flpInspect.Dock          = System.Windows.Forms.DockStyle.Fill;
		        this.flpInspect.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		        this.flpInspect.Location      = new System.Drawing.Point(0, 0);
		        this.flpInspect.Name          = "flpInspect";
		        this.flpInspect.Size          = new System.Drawing.Size(444, 490);
		        this.flpInspect.TabIndex      = 0;
		        this.flpInspect.WrapContents  = false;
		        // 
		        // panel4
		        // 
		        this.panel4.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (81)))), ((int) (((byte) (81)))), ((int) (((byte) (81)))));
		        this.panel4.Controls.Add(this.btnGetHash);
		        this.panel4.Controls.Add(this.btnDecrypt);
		        this.panel4.Controls.Add(this.panel10);
		        this.panel4.Controls.Add(this.rtbOutput);
		        this.panel4.Dock     = System.Windows.Forms.DockStyle.Fill;
		        this.panel4.Location = new System.Drawing.Point(0, 0);
		        this.panel4.Name     = "panel4";
		        this.panel4.Size     = new System.Drawing.Size(444, 147);
		        this.panel4.TabIndex = 0;
		        // 
		        // btnGetHash
		        // 
		        this.btnGetHash.Anchor                    =  ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		        this.btnGetHash.BackColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.btnGetHash.FlatAppearance.BorderSize =  0;
		        this.btnGetHash.FlatStyle                 =  System.Windows.Forms.FlatStyle.Flat;
		        this.btnGetHash.ForeColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.btnGetHash.Location                  =  new System.Drawing.Point(231, 124);
		        this.btnGetHash.Name                      =  "btnGetHash";
		        this.btnGetHash.Size                      =  new System.Drawing.Size(58, 23);
		        this.btnGetHash.TabIndex                  =  15;
		        this.btnGetHash.Text                      =  "Get hash";
		        this.btnGetHash.UseVisualStyleBackColor   =  false;
		        this.btnGetHash.Click                     += new System.EventHandler(this.btnGetHash_Click);
		        // 
		        // btnDecrypt
		        // 
		        this.btnDecrypt.Anchor                    =  ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		        this.btnDecrypt.BackColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.btnDecrypt.FlatAppearance.BorderSize =  0;
		        this.btnDecrypt.FlatStyle                 =  System.Windows.Forms.FlatStyle.Flat;
		        this.btnDecrypt.ForeColor                 =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.btnDecrypt.Location                  =  new System.Drawing.Point(157, 124);
		        this.btnDecrypt.Name                      =  "btnDecrypt";
		        this.btnDecrypt.Size                      =  new System.Drawing.Size(70, 23);
		        this.btnDecrypt.TabIndex                  =  14;
		        this.btnDecrypt.Text                      =  "Try decrypt";
		        this.btnDecrypt.UseVisualStyleBackColor   =  false;
		        this.btnDecrypt.Click                     += new System.EventHandler(this.btnDecrypt_Click);
		        // 
		        // panel10
		        // 
		        this.panel10.Anchor    = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
		        this.panel10.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.panel10.Controls.Add(this.panel11);
		        this.panel10.Controls.Add(this.tbxDecrypt);
		        this.panel10.Location = new System.Drawing.Point(0, 124);
		        this.panel10.Name     = "panel10";
		        this.panel10.Size     = new System.Drawing.Size(153, 23);
		        this.panel10.TabIndex = 13;
		        // 
		        // panel11
		        // 
		        this.panel11.Anchor   = ((System.Windows.Forms.AnchorStyles) ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
		        this.panel11.Location = new System.Drawing.Point(149, 3);
		        this.panel11.Name     = "panel11";
		        this.panel11.Size     = new System.Drawing.Size(4, 18);
		        this.panel11.TabIndex = 1;
		        // 
		        // tbxDecrypt
		        // 
		        this.tbxDecrypt.Anchor      = ((System.Windows.Forms.AnchorStyles) (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.tbxDecrypt.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (69)))), ((int) (((byte) (73)))), ((int) (((byte) (74)))));
		        this.tbxDecrypt.BorderStyle = System.Windows.Forms.BorderStyle.None;
		        this.tbxDecrypt.ForeColor   = System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.tbxDecrypt.Location    = new System.Drawing.Point(3, 5);
		        this.tbxDecrypt.Name        = "tbxDecrypt";
		        this.tbxDecrypt.Size        = new System.Drawing.Size(147, 13);
		        this.tbxDecrypt.TabIndex    = 11;
		        // 
		        // rtbOutput
		        // 
		        this.rtbOutput.Anchor      = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
		        this.rtbOutput.BackColor   = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.rtbOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
		        this.rtbOutput.Location    = new System.Drawing.Point(0, 0);
		        this.rtbOutput.Name        = "rtbOutput";
		        this.rtbOutput.Size        = new System.Drawing.Size(444, 123);
		        this.rtbOutput.TabIndex    = 8;
		        this.rtbOutput.Text        = "";
		        // 
		        // odfSdb
		        // 
		        this.odfSdb.FileName        =  "clientdb.sd2";
		        this.odfSdb.Filter          =  "StaticDB|*.sd*";
		        this.odfSdb.ReadOnlyChecked =  true;
		        this.odfSdb.FileOk          += new System.ComponentModel.CancelEventHandler(this.odfSdb_FileOk);
		        // 
		        // contextMenuStrip1
		        // 
		        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {this.searchForThisToolStripMenuItem, this.copyToolStripMenuItem, this.rawCopyToolStripMenuItem});
		        this.contextMenuStrip1.Name    =  "contextMenuStrip1";
		        this.contextMenuStrip1.Size    =  new System.Drawing.Size(150, 70);
		        this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
		        // 
		        // searchForThisToolStripMenuItem
		        // 
		        this.searchForThisToolStripMenuItem.Name  =  "searchForThisToolStripMenuItem";
		        this.searchForThisToolStripMenuItem.Size  =  new System.Drawing.Size(149, 22);
		        this.searchForThisToolStripMenuItem.Text  =  "Search for this";
		        this.searchForThisToolStripMenuItem.Click += new System.EventHandler(this.searchForThisToolStripMenuItem_Click);
		        // 
		        // copyToolStripMenuItem
		        // 
		        this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
		        this.copyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
		        this.copyToolStripMenuItem.Text = "Copy";
		        // 
		        // rawCopyToolStripMenuItem
		        // 
		        this.rawCopyToolStripMenuItem.Name = "rawCopyToolStripMenuItem";
		        this.rawCopyToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
		        this.rawCopyToolStripMenuItem.Text = "Raw copy";
		        // 
		        // Form1
		        // 
		        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
		        this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
		        this.BackColor           = System.Drawing.Color.FromArgb(((int) (((byte) (60)))), ((int) (((byte) (63)))), ((int) (((byte) (65)))));
		        this.ClientSize          = new System.Drawing.Size(1224, 641);
		        this.Controls.Add(this.splitContainer1);
		        this.ForeColor         =  System.Drawing.Color.FromArgb(((int) (((byte) (200)))), ((int) (((byte) (200)))), ((int) (((byte) (200)))));
		        this.Icon              =  ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
		        this.Location          =  new System.Drawing.Point(15, 15);
		        this.Name              =  "Form1";
		        this.Load              += new System.EventHandler(this.Form1_Load);
		        this.ClientSizeChanged += new System.EventHandler(this.Form1_ClientSizeChanged);
		        this.splitContainer1.Panel1.ResumeLayout(false);
		        this.splitContainer1.Panel2.ResumeLayout(false);
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer1)).EndInit();
		        this.splitContainer1.ResumeLayout(false);
		        this.splitContainer2.Panel1.ResumeLayout(false);
		        this.splitContainer2.Panel2.ResumeLayout(false);
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer2)).EndInit();
		        this.splitContainer2.ResumeLayout(false);
		        this.panel3.ResumeLayout(false);
		        this.panel9.ResumeLayout(false);
		        this.panel5.ResumeLayout(false);
		        this.panel7.ResumeLayout(false);
		        this.panel7.PerformLayout();
		        this.panel1.ResumeLayout(false);
		        this.panel1.PerformLayout();
		        this.pnlLblLoaded.ResumeLayout(false);
		        this.pnlLblLoaded.PerformLayout();
		        this.pnlLvContainer.ResumeLayout(false);
		        this.panel2.ResumeLayout(false);
		        this.panel2.PerformLayout();
		        this.splitContainer3.Panel1.ResumeLayout(false);
		        this.splitContainer3.Panel1.PerformLayout();
		        this.splitContainer3.Panel2.ResumeLayout(false);
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer3)).EndInit();
		        this.splitContainer3.ResumeLayout(false);
		        ((System.ComponentModel.ISupportInitialize) (this.dgvRows)).EndInit();
		        this.pnlInspector.ResumeLayout(false);
		        this.splitContainer4.Panel1.ResumeLayout(false);
		        this.splitContainer4.Panel2.ResumeLayout(false);
		        ((System.ComponentModel.ISupportInitialize) (this.splitContainer4)).EndInit();
		        this.splitContainer4.ResumeLayout(false);
		        this.panel4.ResumeLayout(false);
		        this.panel10.ResumeLayout(false);
		        this.panel10.PerformLayout();
		        this.contextMenuStrip1.ResumeLayout(false);
		        this.ResumeLayout(false);
	        }

	        private System.Windows.Forms.Button DB_ImportExport;

	        private System.Windows.Forms.Panel panel2;
	        private System.Windows.Forms.Panel panel6;

	        private System.Windows.Forms.TextBox tbTableFilter;

        #endregion

        private System.Windows.Forms.SplitContainer    splitContainer1;
        private System.Windows.Forms.SplitContainer    splitContainer3;
        private System.Windows.Forms.Panel             pnlInspector;
        private System.Windows.Forms.Panel             pnlLblLoaded;
        private System.Windows.Forms.Label             lblLoaded;
        private System.Windows.Forms.Label             lblPatch;
        private System.Windows.Forms.Label             lblCreated;
        private System.Windows.Forms.Label             lblFlags;
        private System.Windows.Forms.Panel             panel1;
        private System.Windows.Forms.SplitContainer    splitContainer2;
        private System.Windows.Forms.Button            btnLoad;
        private System.Windows.Forms.Panel             pnlHide;
        private System.Windows.Forms.Panel             pnlLvContainer;
        private System.Windows.Forms.OpenFileDialog    odfSdb;
        private System.Windows.Forms.SplitContainer    splitContainer4;
        private System.Windows.Forms.Panel             panel3;
        private System.Windows.Forms.Panel             panel4;
        private System.Windows.Forms.Button            btnSearchThis;
        private System.Windows.Forms.TextBox           textBox1;
        private System.Windows.Forms.Button            btnSearchAll;
        private System.Windows.Forms.Panel             panel5;
        private System.Windows.Forms.TextBox           tbSearchInput;
        private System.Windows.Forms.ComboBox          cbSearchType;
        private System.Windows.Forms.Panel             panel7;
        private System.Windows.Forms.Panel             panel8;
        private System.Windows.Forms.Panel             panel9;
        private System.Windows.Forms.ListBox           lbSearchResults;
        private System.Windows.Forms.RichTextBox       rtbOutput;
        private System.Windows.Forms.Button            btnDecrypt;
        private System.Windows.Forms.Panel             panel10;
        private System.Windows.Forms.Panel             panel11;
        private System.Windows.Forms.TextBox           tbxDecrypt;
        private System.Windows.Forms.Button            btnGetHash;
        private System.Windows.Forms.DataGridView      dgvRows;
        private VerticalFlowPanel                      flpInspect;
        private System.Windows.Forms.ContextMenuStrip  contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem rawCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchForThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ListBox           lbTables;
    }
}

