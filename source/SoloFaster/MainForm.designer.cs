namespace SoloFaster
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.gpsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.satPicToolbar = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpsInfoDump = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gpsComPortMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeTab = new System.Windows.Forms.TabControl();
            this.pageCollect = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.notesBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.driverCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.eventCombo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.saveSessionAndAnalyzeButton = new System.Windows.Forms.Button();
            this.saveSessionButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.autoRecordMPH = new System.Windows.Forms.NumericUpDown();
            this.clearRecordingButton = new System.Windows.Forms.Button();
            this.autoRecordBox = new System.Windows.Forms.CheckBox();
            this.recordingStatusText = new System.Windows.Forms.Label();
            this.recordButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.collectMapBox = new SoloFaster.MapRenderBox();
            this.pageAnalyze = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.availableLapsListview = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.analyzeOpenSession = new System.Windows.Forms.Button();
            this.analyzeCloseSession = new System.Windows.Forms.Button();
            this.openSessionsListbox = new System.Windows.Forms.ListBox();
            this.analyzeSplitter = new System.Windows.Forms.SplitContainer();
            this.analyzeOverviewMapBox = new SoloFaster.MapRenderBox();
            this.overviewStrip = new System.Windows.Forms.ToolStrip();
            this.analyzeSetStart = new System.Windows.Forms.ToolStripButton();
            this.analyzeSetFinish = new System.Windows.Forms.ToolStripButton();
            this.analyzeSetStartFinish = new System.Windows.Forms.ToolStripButton();
            this.analyzeAutoCalculateSF = new System.Windows.Forms.ToolStripButton();
            this.analyzeClearSF = new System.Windows.Forms.ToolStripButton();
            this.analyzeTape = new SoloFaster.AnalysisTape();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.modeTab.SuspendLayout();
            this.pageCollect.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.autoRecordMPH)).BeginInit();
            this.pageAnalyze.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.analyzeSplitter)).BeginInit();
            this.analyzeSplitter.Panel1.SuspendLayout();
            this.analyzeSplitter.Panel2.SuspendLayout();
            this.analyzeSplitter.SuspendLayout();
            this.overviewStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gpsStatus,
            this.satPicToolbar,
            this.gpsInfoDump});
            this.statusStrip.Location = new System.Drawing.Point(0, 490);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(884, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // gpsStatus
            // 
            this.gpsStatus.Name = "gpsStatus";
            this.gpsStatus.Size = new System.Drawing.Size(80, 17);
            this.gpsStatus.Text = "Finding GPS...";
            // 
            // satPicToolbar
            // 
            this.satPicToolbar.AutoSize = false;
            this.satPicToolbar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.satPicToolbar.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.satPicToolbar.Name = "satPicToolbar";
            this.satPicToolbar.Size = new System.Drawing.Size(150, 21);
            this.satPicToolbar.Text = "toolStripStatusLabel1";
            // 
            // gpsInfoDump
            // 
            this.gpsInfoDump.Name = "gpsInfoDump";
            this.gpsInfoDump.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.openToolStripMenuItem.Text = "&Open Other Log Format";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(197, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gpsComPortMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // gpsComPortMenuItem
            // 
            this.gpsComPortMenuItem.Name = "gpsComPortMenuItem";
            this.gpsComPortMenuItem.Size = new System.Drawing.Size(168, 22);
            this.gpsComPortMenuItem.Text = "Set &GPS Com Port";
            // 
            // modeTab
            // 
            this.modeTab.Controls.Add(this.pageCollect);
            this.modeTab.Controls.Add(this.pageAnalyze);
            this.modeTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modeTab.Location = new System.Drawing.Point(0, 24);
            this.modeTab.Name = "modeTab";
            this.modeTab.SelectedIndex = 0;
            this.modeTab.Size = new System.Drawing.Size(884, 466);
            this.modeTab.TabIndex = 0;
            this.modeTab.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.modeTab_Selecting);
            // 
            // pageCollect
            // 
            this.pageCollect.Controls.Add(this.splitContainer1);
            this.pageCollect.Location = new System.Drawing.Point(4, 22);
            this.pageCollect.Name = "pageCollect";
            this.pageCollect.Padding = new System.Windows.Forms.Padding(3);
            this.pageCollect.Size = new System.Drawing.Size(876, 440);
            this.pageCollect.TabIndex = 0;
            this.pageCollect.Text = "Collect";
            this.pageCollect.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.collectMapBox);
            this.splitContainer1.Size = new System.Drawing.Size(870, 434);
            this.splitContainer1.SplitterDistance = 170;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.notesBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.driverCombo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.eventCombo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 176);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Session Metadata";
            // 
            // notesBox
            // 
            this.notesBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.notesBox.Location = new System.Drawing.Point(9, 112);
            this.notesBox.Multiline = true;
            this.notesBox.Name = "notesBox";
            this.notesBox.Size = new System.Drawing.Size(149, 58);
            this.notesBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Notes";
            // 
            // driverCombo
            // 
            this.driverCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.driverCombo.FormattingEnabled = true;
            this.driverCombo.Location = new System.Drawing.Point(9, 72);
            this.driverCombo.Name = "driverCombo";
            this.driverCombo.Size = new System.Drawing.Size(149, 21);
            this.driverCombo.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Driver";
            // 
            // eventCombo
            // 
            this.eventCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventCombo.FormattingEnabled = true;
            this.eventCombo.Location = new System.Drawing.Point(9, 32);
            this.eventCombo.Name = "eventCombo";
            this.eventCombo.Size = new System.Drawing.Size(149, 21);
            this.eventCombo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Event";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.saveSessionAndAnalyzeButton);
            this.groupBox5.Controls.Add(this.saveSessionButton);
            this.groupBox5.Location = new System.Drawing.Point(3, 315);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(164, 76);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Save Control";
            // 
            // saveSessionAndAnalyzeButton
            // 
            this.saveSessionAndAnalyzeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveSessionAndAnalyzeButton.Enabled = false;
            this.saveSessionAndAnalyzeButton.Location = new System.Drawing.Point(6, 19);
            this.saveSessionAndAnalyzeButton.Name = "saveSessionAndAnalyzeButton";
            this.saveSessionAndAnalyzeButton.Size = new System.Drawing.Size(152, 22);
            this.saveSessionAndAnalyzeButton.TabIndex = 6;
            this.saveSessionAndAnalyzeButton.Text = "Save Session and Analyze";
            this.saveSessionAndAnalyzeButton.UseVisualStyleBackColor = true;
            this.saveSessionAndAnalyzeButton.Click += new System.EventHandler(this.saveSessionAndAnalyzeButton_Click);
            // 
            // saveSessionButton
            // 
            this.saveSessionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveSessionButton.Enabled = false;
            this.saveSessionButton.Location = new System.Drawing.Point(6, 47);
            this.saveSessionButton.Name = "saveSessionButton";
            this.saveSessionButton.Size = new System.Drawing.Size(152, 22);
            this.saveSessionButton.TabIndex = 1;
            this.saveSessionButton.Text = "Save Session";
            this.saveSessionButton.UseVisualStyleBackColor = true;
            this.saveSessionButton.Click += new System.EventHandler(this.saveRecordingButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.autoRecordMPH);
            this.groupBox2.Controls.Add(this.clearRecordingButton);
            this.groupBox2.Controls.Add(this.autoRecordBox);
            this.groupBox2.Controls.Add(this.recordingStatusText);
            this.groupBox2.Controls.Add(this.recordButton);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(3, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(164, 124);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record Control";
            // 
            // autoRecordMPH
            // 
            this.autoRecordMPH.Location = new System.Drawing.Point(95, 69);
            this.autoRecordMPH.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.autoRecordMPH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.autoRecordMPH.Name = "autoRecordMPH";
            this.autoRecordMPH.Size = new System.Drawing.Size(33, 20);
            this.autoRecordMPH.TabIndex = 5;
            this.autoRecordMPH.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // clearRecordingButton
            // 
            this.clearRecordingButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clearRecordingButton.Enabled = false;
            this.clearRecordingButton.Location = new System.Drawing.Point(6, 94);
            this.clearRecordingButton.Name = "clearRecordingButton";
            this.clearRecordingButton.Size = new System.Drawing.Size(152, 22);
            this.clearRecordingButton.TabIndex = 0;
            this.clearRecordingButton.Text = "Clear Recording";
            this.clearRecordingButton.UseVisualStyleBackColor = true;
            this.clearRecordingButton.Click += new System.EventHandler(this.clearRecordingButton_Click);
            // 
            // autoRecordBox
            // 
            this.autoRecordBox.AutoSize = true;
            this.autoRecordBox.Location = new System.Drawing.Point(9, 70);
            this.autoRecordBox.Name = "autoRecordBox";
            this.autoRecordBox.Size = new System.Drawing.Size(89, 17);
            this.autoRecordBox.TabIndex = 2;
            this.autoRecordBox.Text = "Auto-Record:";
            this.autoRecordBox.UseVisualStyleBackColor = true;
            // 
            // recordingStatusText
            // 
            this.recordingStatusText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recordingStatusText.Location = new System.Drawing.Point(6, 46);
            this.recordingStatusText.Name = "recordingStatusText";
            this.recordingStatusText.Size = new System.Drawing.Size(152, 20);
            this.recordingStatusText.TabIndex = 1;
            this.recordingStatusText.Text = "No Status";
            // 
            // recordButton
            // 
            this.recordButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recordButton.Location = new System.Drawing.Point(6, 19);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(152, 22);
            this.recordButton.TabIndex = 0;
            this.recordButton.Text = "Start Recording";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(127, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "MPH";
            // 
            // collectMapBox
            // 
            this.collectMapBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.collectMapBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.collectMapBox.Location = new System.Drawing.Point(0, 0);
            this.collectMapBox.Name = "collectMapBox";
            this.collectMapBox.Size = new System.Drawing.Size(696, 434);
            this.collectMapBox.TabIndex = 0;
            // 
            // pageAnalyze
            // 
            this.pageAnalyze.Controls.Add(this.splitContainer2);
            this.pageAnalyze.Location = new System.Drawing.Point(4, 22);
            this.pageAnalyze.Name = "pageAnalyze";
            this.pageAnalyze.Padding = new System.Windows.Forms.Padding(3);
            this.pageAnalyze.Size = new System.Drawing.Size(876, 440);
            this.pageAnalyze.TabIndex = 1;
            this.pageAnalyze.Text = "Analyze";
            this.pageAnalyze.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.analyzeSplitter);
            this.splitContainer2.Size = new System.Drawing.Size(870, 434);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.availableLapsListview);
            this.groupBox3.Location = new System.Drawing.Point(3, 135);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 296);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Available Laps";
            // 
            // availableLapsListview
            // 
            this.availableLapsListview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.availableLapsListview.AutoArrange = false;
            this.availableLapsListview.CheckBoxes = true;
            this.availableLapsListview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.availableLapsListview.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availableLapsListview.FullRowSelect = true;
            this.availableLapsListview.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.availableLapsListview.Location = new System.Drawing.Point(9, 19);
            this.availableLapsListview.MultiSelect = false;
            this.availableLapsListview.Name = "availableLapsListview";
            this.availableLapsListview.ShowGroups = false;
            this.availableLapsListview.ShowItemToolTips = true;
            this.availableLapsListview.Size = new System.Drawing.Size(179, 271);
            this.availableLapsListview.TabIndex = 0;
            this.availableLapsListview.UseCompatibleStateImageBehavior = false;
            this.availableLapsListview.View = System.Windows.Forms.View.Details;
            this.availableLapsListview.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.availableLapsListview_ItemChecked);
            this.availableLapsListview.SelectedIndexChanged += new System.EventHandler(this.availableLapsListview_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Lap Name";
            this.columnHeader1.Width = 125;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Lap Time";
            this.columnHeader3.Width = 50;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.analyzeOpenSession);
            this.groupBox4.Controls.Add(this.analyzeCloseSession);
            this.groupBox4.Controls.Add(this.openSessionsListbox);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(194, 126);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Open Sessions";
            // 
            // analyzeOpenSession
            // 
            this.analyzeOpenSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.analyzeOpenSession.Location = new System.Drawing.Point(9, 94);
            this.analyzeOpenSession.Name = "analyzeOpenSession";
            this.analyzeOpenSession.Size = new System.Drawing.Size(70, 22);
            this.analyzeOpenSession.TabIndex = 2;
            this.analyzeOpenSession.Text = "Open New";
            this.analyzeOpenSession.UseVisualStyleBackColor = true;
            this.analyzeOpenSession.Click += new System.EventHandler(this.analyzeOpenSession_Click);
            // 
            // analyzeCloseSession
            // 
            this.analyzeCloseSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.analyzeCloseSession.Location = new System.Drawing.Point(118, 94);
            this.analyzeCloseSession.Name = "analyzeCloseSession";
            this.analyzeCloseSession.Size = new System.Drawing.Size(70, 22);
            this.analyzeCloseSession.TabIndex = 1;
            this.analyzeCloseSession.Text = "Close";
            this.analyzeCloseSession.UseVisualStyleBackColor = true;
            this.analyzeCloseSession.Click += new System.EventHandler(this.analyzeCloseSession_Click);
            // 
            // openSessionsListbox
            // 
            this.openSessionsListbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openSessionsListbox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.openSessionsListbox.FormattingEnabled = true;
            this.openSessionsListbox.Location = new System.Drawing.Point(9, 19);
            this.openSessionsListbox.Name = "openSessionsListbox";
            this.openSessionsListbox.Size = new System.Drawing.Size(179, 69);
            this.openSessionsListbox.TabIndex = 0;
            this.openSessionsListbox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.openSessionsListbox_DrawItem);
            this.openSessionsListbox.SelectedIndexChanged += new System.EventHandler(this.openSessionsListbox_SelectedIndexChanged);
            // 
            // analyzeSplitter
            // 
            this.analyzeSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analyzeSplitter.Location = new System.Drawing.Point(0, 0);
            this.analyzeSplitter.Name = "analyzeSplitter";
            // 
            // analyzeSplitter.Panel1
            // 
            this.analyzeSplitter.Panel1.Controls.Add(this.analyzeOverviewMapBox);
            this.analyzeSplitter.Panel1.Controls.Add(this.overviewStrip);
            this.analyzeSplitter.Panel1MinSize = 181;
            // 
            // analyzeSplitter.Panel2
            // 
            this.analyzeSplitter.Panel2.Controls.Add(this.analyzeTape);
            this.analyzeSplitter.Size = new System.Drawing.Size(666, 434);
            this.analyzeSplitter.SplitterDistance = 220;
            this.analyzeSplitter.TabIndex = 1;
            // 
            // analyzeOverviewMapBox
            // 
            this.analyzeOverviewMapBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.analyzeOverviewMapBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analyzeOverviewMapBox.Location = new System.Drawing.Point(0, 36);
            this.analyzeOverviewMapBox.Name = "analyzeOverviewMapBox";
            this.analyzeOverviewMapBox.Size = new System.Drawing.Size(220, 398);
            this.analyzeOverviewMapBox.TabIndex = 4;
            // 
            // overviewStrip
            // 
            this.overviewStrip.AutoSize = false;
            this.overviewStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.overviewStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyzeSetStart,
            this.analyzeSetFinish,
            this.analyzeSetStartFinish,
            this.analyzeAutoCalculateSF,
            this.analyzeClearSF});
            this.overviewStrip.Location = new System.Drawing.Point(0, 0);
            this.overviewStrip.Name = "overviewStrip";
            this.overviewStrip.Size = new System.Drawing.Size(220, 36);
            this.overviewStrip.TabIndex = 5;
            this.overviewStrip.Text = "toolStrip1";
            // 
            // analyzeSetStart
            // 
            this.analyzeSetStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.analyzeSetStart.Image = ((System.Drawing.Image)(resources.GetObject("analyzeSetStart.Image")));
            this.analyzeSetStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.analyzeSetStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.analyzeSetStart.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeSetStart.Name = "analyzeSetStart";
            this.analyzeSetStart.Size = new System.Drawing.Size(36, 36);
            this.analyzeSetStart.Text = "Set Start Line";
            this.analyzeSetStart.Click += new System.EventHandler(this.analyzeSetStart_Click);
            // 
            // analyzeSetFinish
            // 
            this.analyzeSetFinish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.analyzeSetFinish.Image = ((System.Drawing.Image)(resources.GetObject("analyzeSetFinish.Image")));
            this.analyzeSetFinish.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.analyzeSetFinish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.analyzeSetFinish.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeSetFinish.Name = "analyzeSetFinish";
            this.analyzeSetFinish.Size = new System.Drawing.Size(36, 36);
            this.analyzeSetFinish.Text = "Set Finish Line";
            this.analyzeSetFinish.Click += new System.EventHandler(this.analyzeSetFinish_Click);
            // 
            // analyzeSetStartFinish
            // 
            this.analyzeSetStartFinish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.analyzeSetStartFinish.Image = ((System.Drawing.Image)(resources.GetObject("analyzeSetStartFinish.Image")));
            this.analyzeSetStartFinish.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.analyzeSetStartFinish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.analyzeSetStartFinish.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeSetStartFinish.Name = "analyzeSetStartFinish";
            this.analyzeSetStartFinish.Size = new System.Drawing.Size(36, 36);
            this.analyzeSetStartFinish.Text = "Set Start/Finish Line";
            this.analyzeSetStartFinish.Click += new System.EventHandler(this.analyzeSetStartFinish_Click);
            // 
            // analyzeAutoCalculateSF
            // 
            this.analyzeAutoCalculateSF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.analyzeAutoCalculateSF.Image = ((System.Drawing.Image)(resources.GetObject("analyzeAutoCalculateSF.Image")));
            this.analyzeAutoCalculateSF.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.analyzeAutoCalculateSF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.analyzeAutoCalculateSF.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeAutoCalculateSF.Name = "analyzeAutoCalculateSF";
            this.analyzeAutoCalculateSF.Size = new System.Drawing.Size(36, 36);
            this.analyzeAutoCalculateSF.Text = "Auto-Calculate Start/Finish";
            this.analyzeAutoCalculateSF.Click += new System.EventHandler(this.analyzeAutoCalculateSF_Click);
            // 
            // analyzeClearSF
            // 
            this.analyzeClearSF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.analyzeClearSF.Image = ((System.Drawing.Image)(resources.GetObject("analyzeClearSF.Image")));
            this.analyzeClearSF.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.analyzeClearSF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.analyzeClearSF.Margin = new System.Windows.Forms.Padding(0);
            this.analyzeClearSF.Name = "analyzeClearSF";
            this.analyzeClearSF.Size = new System.Drawing.Size(36, 36);
            this.analyzeClearSF.Text = "Clear Start/Finish";
            this.analyzeClearSF.Click += new System.EventHandler(this.analyzeClearSF_Click);
            // 
            // analyzeTape
            // 
            this.analyzeTape.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analyzeTape.Location = new System.Drawing.Point(0, 0);
            this.analyzeTape.Name = "analyzeTape";
            this.analyzeTape.Size = new System.Drawing.Size(442, 434);
            this.analyzeTape.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Load other data formats...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 512);
            this.Controls.Add(this.modeTab);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(300, 512);
            this.Name = "MainForm";
            this.Text = "SoloFaster";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.modeTab.ResumeLayout(false);
            this.pageCollect.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.autoRecordMPH)).EndInit();
            this.pageAnalyze.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.analyzeSplitter.Panel1.ResumeLayout(false);
            this.analyzeSplitter.Panel2.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.analyzeSplitter)).EndInit();
            this.analyzeSplitter.ResumeLayout(false);
            this.overviewStrip.ResumeLayout(false);
            this.overviewStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel gpsStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gpsComPortMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel satPicToolbar;
        private System.Windows.Forms.TabControl modeTab;
        private System.Windows.Forms.TabPage pageCollect;
        private System.Windows.Forms.TabPage pageAnalyze;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button saveSessionButton;
        private System.Windows.Forms.Button clearRecordingButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox autoRecordBox;
        private System.Windows.Forms.Label recordingStatusText;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox notesBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox driverCombo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox eventCombo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripStatusLabel gpsInfoDump;
        private System.Windows.Forms.NumericUpDown autoRecordMPH;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button saveSessionAndAnalyzeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button analyzeOpenSession;
        private System.Windows.Forms.Button analyzeCloseSession;
        private System.Windows.Forms.ListBox openSessionsListbox;
        private MapRenderBox collectMapBox;
        private System.Windows.Forms.ListView availableLapsListview;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.SplitContainer analyzeSplitter;
        private MapRenderBox analyzeOverviewMapBox;
        private System.Windows.Forms.ToolStrip overviewStrip;
        private System.Windows.Forms.ToolStripButton analyzeSetStart;
        private System.Windows.Forms.ToolStripButton analyzeSetFinish;
        private System.Windows.Forms.ToolStripButton analyzeSetStartFinish;
        private System.Windows.Forms.ToolStripButton analyzeAutoCalculateSF;
        private AnalysisTape analyzeTape;
        private System.Windows.Forms.ToolStripButton analyzeClearSF;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

