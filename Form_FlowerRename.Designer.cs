namespace FlowerRename
{
    partial class Form_FlowerRename
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel ruleContainer;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.ToolStripButton btnOpenFiles; 
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnOpenDir;
        private System.Windows.Forms.ToolStripButton btnAddFiles;
        private System.Windows.Forms.ToolStripButton btnAddDir;
        private System.Windows.Forms.ToolStripButton btnClearAll;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.ColumnHeader columnNewName;
        private System.Windows.Forms.ColumnHeader columnPath;
        private System.Windows.Forms.ColumnHeader columnSize;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_FlowerRename));
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "123", "aaaaaa", "123" }, -1, Color.Empty, SystemColors.MenuHighlight, null);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "4555", "4555" }, -1);
            ListViewItem listViewItem3 = new ListViewItem(new string[] { "6666", "6666" }, -1);
            ListViewItem listViewItem4 = new ListViewItem(new string[] { "8888", "8888" }, -1);
            comboBoxAddRule = new ComboBox();
            ruleContainer = new Panel();
            menuStripAddRule = new MenuStrip();
            menuItemAddRule = new ToolStripMenuItem();
            menuItemNumberingRule = new ToolStripMenuItem();
            menuItemReplaceRule = new ToolStripMenuItem();
            menuItemInsertRule = new ToolStripMenuItem();
            menuItemGroupRule = new ToolStripMenuItem();
            buttonUndo = new Button();
            buttonRename = new Button();
            toolStrip = new ToolStrip();
            btnOpenFiles = new ToolStripButton();
            btnAddFiles = new ToolStripButton();
            btnOpenDir = new ToolStripButton();
            btnAddDir = new ToolStripButton();
            btnClearSelected = new ToolStripButton();
            btnClearAll = new ToolStripButton();
            fileListView = new ListView();
            原有檔名 = new ColumnHeader();
            新檔名 = new ColumnHeader();
            大小 = new ColumnHeader();
            日期 = new ColumnHeader();
            目錄 = new ColumnHeader();
            contextMenuStrip_Files = new ContextMenuStrip(components);
            toolStripMenuItem_ClearSelected = new ToolStripMenuItem();
            toolStripMenuItem_CopyFileNameToClipboard = new ToolStripMenuItem();
            toolStripMenuItem_OpenFile = new ToolStripMenuItem();
            columnFileName = new ColumnHeader();
            columnNewName = new ColumnHeader();
            columnPath = new ColumnHeader();
            columnSize = new ColumnHeader();
            rightPanel = new Panel();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            statusStrip_Bottom = new StatusStrip();
            toolStripStatusLabel_News = new ToolStripStatusLabel();
            toolStripStatusLabel_FilesInfo = new ToolStripStatusLabel();
            ruleContainer.SuspendLayout();
            menuStripAddRule.SuspendLayout();
            toolStrip.SuspendLayout();
            contextMenuStrip_Files.SuspendLayout();
            rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            statusStrip_Bottom.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxAddRule
            // 
            comboBoxAddRule.Dock = DockStyle.Top;
            comboBoxAddRule.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAddRule.FlatStyle = FlatStyle.Popup;
            comboBoxAddRule.FormattingEnabled = true;
            comboBoxAddRule.Items.AddRange(new object[] { "請選擇新增命名規則", "重新編號", "置換指定文字(亦可刪除)", "添加文字(根據位置)", "刪除文字(根據位置)", "群組化(AI產圖方便比對)" });
            comboBoxAddRule.Location = new Point(5, 5);
            comboBoxAddRule.Name = "comboBoxAddRule";
            comboBoxAddRule.Size = new Size(610, 28);
            comboBoxAddRule.TabIndex = 1;
            comboBoxAddRule.SelectedIndexChanged += comboBoxAddRule_SelectedIndexChanged;
            // 
            // ruleContainer
            // 
            ruleContainer.AutoScroll = true;
            ruleContainer.AutoSize = true;
            ruleContainer.BackColor = SystemColors.ControlLight;
            ruleContainer.Controls.Add(comboBoxAddRule);
            ruleContainer.Controls.Add(menuStripAddRule);
            ruleContainer.Dock = DockStyle.Fill;
            ruleContainer.Location = new Point(0, 0);
            ruleContainer.Name = "ruleContainer";
            ruleContainer.Padding = new Padding(5);
            ruleContainer.Size = new Size(620, 697);
            ruleContainer.TabIndex = 1;
            // 
            // menuStripAddRule
            // 
            menuStripAddRule.GripStyle = ToolStripGripStyle.Visible;
            menuStripAddRule.Items.AddRange(new ToolStripItem[] { menuItemAddRule });
            menuStripAddRule.Location = new Point(5, 5);
            menuStripAddRule.Name = "menuStripAddRule";
            menuStripAddRule.RenderMode = ToolStripRenderMode.Professional;
            menuStripAddRule.Size = new Size(596, 28);
            menuStripAddRule.TabIndex = 4;
            menuStripAddRule.Text = "menuStrip1";
            menuStripAddRule.Visible = false;
            // 
            // menuItemAddRule
            // 
            menuItemAddRule.DisplayStyle = ToolStripItemDisplayStyle.Text;
            menuItemAddRule.DropDownItems.AddRange(new ToolStripItem[] { menuItemNumberingRule, menuItemReplaceRule, menuItemInsertRule, menuItemGroupRule });
            menuItemAddRule.Name = "menuItemAddRule";
            menuItemAddRule.Size = new Size(85, 24);
            menuItemAddRule.Text = "添加規則";
            // 
            // menuItemNumberingRule
            // 
            menuItemNumberingRule.Name = "menuItemNumberingRule";
            menuItemNumberingRule.Size = new Size(248, 24);
            menuItemNumberingRule.Text = "重新編號";
            menuItemNumberingRule.Click += menuItemNumberingRule_Click;
            // 
            // menuItemReplaceRule
            // 
            menuItemReplaceRule.Name = "menuItemReplaceRule";
            menuItemReplaceRule.Size = new Size(248, 24);
            menuItemReplaceRule.Text = "置換指定文字(亦可刪除)";
            menuItemReplaceRule.Click += menuItemReplaceRule_Click;
            // 
            // menuItemInsertRule
            // 
            menuItemInsertRule.Name = "menuItemInsertRule";
            menuItemInsertRule.Size = new Size(248, 24);
            menuItemInsertRule.Text = "添加文字(根據位置)";
            menuItemInsertRule.Click += menuItemInsertRule_Click;
            // 
            // menuItemGroupRule
            // 
            menuItemGroupRule.Name = "menuItemGroupRule";
            menuItemGroupRule.Size = new Size(248, 24);
            menuItemGroupRule.Text = "群組化(AI產圖方便比對)";
            menuItemGroupRule.Click += menuItemGroupRule_Click;
            // 
            // buttonUndo
            // 
            buttonUndo.BackColor = Color.Tomato;
            buttonUndo.Dock = DockStyle.Left;
            buttonUndo.Enabled = false;
            buttonUndo.Font = new Font("Microsoft JhengHei UI Light", 18F, FontStyle.Regular, GraphicsUnit.Point, 136);
            buttonUndo.ForeColor = Color.White;
            buttonUndo.Location = new Point(0, 0);
            buttonUndo.Name = "buttonUndo";
            buttonUndo.Size = new Size(300, 37);
            buttonUndo.TabIndex = 3;
            buttonUndo.Text = "復原 Undo";
            buttonUndo.UseVisualStyleBackColor = false;
            buttonUndo.Visible = false;
            buttonUndo.Click += buttonUndo_Click;
            // 
            // buttonRename
            // 
            buttonRename.BackColor = Color.LightSeaGreen;
            buttonRename.Dock = DockStyle.Right;
            buttonRename.Font = new Font("Microsoft JhengHei UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 136);
            buttonRename.ForeColor = Color.White;
            buttonRename.Location = new Point(320, 0);
            buttonRename.Name = "buttonRename";
            buttonRename.Size = new Size(300, 37);
            buttonRename.TabIndex = 2;
            buttonRename.Text = "更改檔名";
            buttonRename.UseVisualStyleBackColor = false;
            buttonRename.Click += buttonRename_Click;
            // 
            // toolStrip
            // 
            toolStrip.AutoSize = false;
            toolStrip.ImageScalingSize = new Size(16, 48);
            toolStrip.Items.AddRange(new ToolStripItem[] { btnOpenFiles, btnAddFiles, btnOpenDir, btnAddDir, btnClearSelected, btnClearAll });
            toolStrip.Location = new Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Padding = new Padding(0);
            toolStrip.Size = new Size(1180, 48);
            toolStrip.TabIndex = 1;
            // 
            // btnOpenFiles
            // 
            btnOpenFiles.AutoSize = false;
            btnOpenFiles.BackgroundImage = (Image)resources.GetObject("btnOpenFiles.BackgroundImage");
            btnOpenFiles.BackgroundImageLayout = ImageLayout.Stretch;
            btnOpenFiles.Margin = new Padding(10, 0, 0, 0);
            btnOpenFiles.Name = "btnOpenFiles";
            btnOpenFiles.Padding = new Padding(10, 0, 0, 0);
            btnOpenFiles.Size = new Size(87, 25);
            btnOpenFiles.Text = "開啟檔案";
            btnOpenFiles.TextImageRelation = TextImageRelation.TextAboveImage;
            btnOpenFiles.Click += btnOpenFiles_Click;
            // 
            // btnAddFiles
            // 
            btnAddFiles.AutoSize = false;
            btnAddFiles.BackgroundImage = (Image)resources.GetObject("btnAddFiles.BackgroundImage");
            btnAddFiles.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddFiles.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnAddFiles.Margin = new Padding(10, 0, 0, 0);
            btnAddFiles.Name = "btnAddFiles";
            btnAddFiles.Padding = new Padding(10, 0, 0, 0);
            btnAddFiles.Size = new Size(87, 25);
            btnAddFiles.Text = "增加檔案+";
            btnAddFiles.TextImageRelation = TextImageRelation.TextAboveImage;
            btnAddFiles.Click += btnAddFiles_Click;
            // 
            // btnOpenDir
            // 
            btnOpenDir.AutoSize = false;
            btnOpenDir.BackgroundImage = (Image)resources.GetObject("btnOpenDir.BackgroundImage");
            btnOpenDir.BackgroundImageLayout = ImageLayout.Stretch;
            btnOpenDir.Margin = new Padding(10, 0, 0, 0);
            btnOpenDir.Name = "btnOpenDir";
            btnOpenDir.Padding = new Padding(10, 0, 0, 0);
            btnOpenDir.Size = new Size(87, 25);
            btnOpenDir.Text = "開啟目錄";
            btnOpenDir.TextImageRelation = TextImageRelation.TextAboveImage;
            btnOpenDir.Click += btnOpenDir_Click;
            // 
            // btnAddDir
            // 
            btnAddDir.AutoSize = false;
            btnAddDir.BackgroundImage = (Image)resources.GetObject("btnAddDir.BackgroundImage");
            btnAddDir.BackgroundImageLayout = ImageLayout.Stretch;
            btnAddDir.Margin = new Padding(10, 0, 0, 0);
            btnAddDir.Name = "btnAddDir";
            btnAddDir.Padding = new Padding(10, 0, 0, 0);
            btnAddDir.Size = new Size(87, 25);
            btnAddDir.Text = "增加目錄+";
            btnAddDir.TextImageRelation = TextImageRelation.TextAboveImage;
            btnAddDir.Click += btnAddDir_Click;
            // 
            // btnClearSelected
            // 
            btnClearSelected.AutoSize = false;
            btnClearSelected.BackgroundImage = (Image)resources.GetObject("btnClearSelected.BackgroundImage");
            btnClearSelected.BackgroundImageLayout = ImageLayout.Stretch;
            btnClearSelected.DisplayStyle = ToolStripItemDisplayStyle.Text;
            btnClearSelected.ImageTransparentColor = Color.Magenta;
            btnClearSelected.Margin = new Padding(10, 0, 0, 0);
            btnClearSelected.Name = "btnClearSelected";
            btnClearSelected.Size = new Size(190, 25);
            btnClearSelected.Text = "清除已選擇的檔案清單 -";
            btnClearSelected.TextImageRelation = TextImageRelation.TextAboveImage;
            btnClearSelected.Click += btnClearSelected_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.AutoSize = false;
            btnClearAll.BackgroundImage = (Image)resources.GetObject("btnClearAll.BackgroundImage");
            btnClearAll.BackgroundImageLayout = ImageLayout.Stretch;
            btnClearAll.Margin = new Padding(10, 0, 0, 0);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Padding = new Padding(10, 0, 0, 0);
            btnClearAll.Size = new Size(160, 25);
            btnClearAll.Text = "清除全部檔案清單 -";
            btnClearAll.TextImageRelation = TextImageRelation.TextAboveImage;
            btnClearAll.Click += btnClearAll_Click;
            // 
            // fileListView
            // 
            fileListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            fileListView.Columns.AddRange(new ColumnHeader[] { 原有檔名, 新檔名, 大小, 日期, 目錄 });
            fileListView.ContextMenuStrip = contextMenuStrip_Files;
            fileListView.FullRowSelect = true;
            fileListView.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            fileListView.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4 });
            fileListView.Location = new Point(0, 48);
            fileListView.Name = "fileListView";
            fileListView.Size = new Size(1180, 690);
            fileListView.TabIndex = 0;
            fileListView.UseCompatibleStateImageBehavior = false;
            fileListView.View = View.Details;
            fileListView.ColumnClick += fileListView_ColumnClick;
            // 
            // 原有檔名
            // 
            原有檔名.Text = "原有檔名";
            原有檔名.Width = 400;
            // 
            // 新檔名
            // 
            新檔名.Text = "新檔名";
            新檔名.Width = 400;
            // 
            // 大小
            // 
            大小.Text = "大小";
            大小.Width = 100;
            // 
            // 日期
            // 
            日期.Text = "日期";
            日期.Width = 200;
            // 
            // 目錄
            // 
            目錄.Text = "目錄";
            目錄.Width = 400;
            // 
            // contextMenuStrip_Files
            // 
            contextMenuStrip_Files.Items.AddRange(new ToolStripItem[] { toolStripMenuItem_ClearSelected, toolStripMenuItem_CopyFileNameToClipboard, toolStripMenuItem_OpenFile });
            contextMenuStrip_Files.Name = "contextMenuStrip_Files";
            contextMenuStrip_Files.Size = new Size(207, 76);
            // 
            // toolStripMenuItem_ClearSelected
            // 
            toolStripMenuItem_ClearSelected.Name = "toolStripMenuItem_ClearSelected";
            toolStripMenuItem_ClearSelected.Size = new Size(206, 24);
            toolStripMenuItem_ClearSelected.Text = "清除已選擇項目";
            toolStripMenuItem_ClearSelected.Click += btnClearSelected_Click;
            // 
            // toolStripMenuItem_CopyFileNameToClipboard
            // 
            toolStripMenuItem_CopyFileNameToClipboard.Name = "toolStripMenuItem_CopyFileNameToClipboard";
            toolStripMenuItem_CopyFileNameToClipboard.Size = new Size(206, 24);
            toolStripMenuItem_CopyFileNameToClipboard.Text = "複製檔名至剪貼簿";
            toolStripMenuItem_CopyFileNameToClipboard.Click += toolStripMenuItem_CopyFileNameToClipboard_Click;
            // 
            // toolStripMenuItem_OpenFile
            // 
            toolStripMenuItem_OpenFile.Name = "toolStripMenuItem_OpenFile";
            toolStripMenuItem_OpenFile.Size = new Size(206, 24);
            toolStripMenuItem_OpenFile.Text = "開啟項目...";
            toolStripMenuItem_OpenFile.Click += fileListView_DoubleClick;
            // 
            // rightPanel
            // 
            rightPanel.Controls.Add(fileListView);
            rightPanel.Controls.Add(toolStrip);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(0, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(1180, 761);
            rightPanel.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1MinSize = 606;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(rightPanel);
            splitContainer1.Size = new Size(1804, 761);
            splitContainer1.SplitterDistance = 620;
            splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(ruleContainer);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(buttonRename);
            splitContainer2.Panel2.Controls.Add(buttonUndo);
            splitContainer2.Size = new Size(620, 738);
            splitContainer2.SplitterDistance = 697;
            splitContainer2.TabIndex = 0;
            // 
            // statusStrip_Bottom
            // 
            statusStrip_Bottom.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel_News, toolStripStatusLabel_FilesInfo });
            statusStrip_Bottom.Location = new Point(0, 736);
            statusStrip_Bottom.Name = "statusStrip_Bottom";
            statusStrip_Bottom.Size = new Size(1804, 25);
            statusStrip_Bottom.TabIndex = 3;
            statusStrip_Bottom.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_News
            // 
            toolStripStatusLabel_News.AutoSize = false;
            toolStripStatusLabel_News.Name = "toolStripStatusLabel_News";
            toolStripStatusLabel_News.Size = new Size(620, 20);
            toolStripStatusLabel_News.Text = "行動資訊";
            toolStripStatusLabel_News.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel_FilesInfo
            // 
            toolStripStatusLabel_FilesInfo.Name = "toolStripStatusLabel_FilesInfo";
            toolStripStatusLabel_FilesInfo.Size = new Size(73, 20);
            toolStripStatusLabel_FilesInfo.Text = "檔案資訊";
            toolStripStatusLabel_FilesInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_FlowerRename
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1804, 761);
            Controls.Add(statusStrip_Bottom);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripAddRule;
            Name = "Form_FlowerRename";
            Text = "開心花更名 V0.5.0(新增刪除功能、開啟選擇項目、狀態列、拖入多個目錄檔案功能、undo，修正清除項目錯誤)";
            DragDrop += Form_FlowerRename_DragDrop;
            DragEnter += Form_FlowerRename_DragEnter;
            ruleContainer.ResumeLayout(false);
            ruleContainer.PerformLayout();
            menuStripAddRule.ResumeLayout(false);
            menuStripAddRule.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            contextMenuStrip_Files.ResumeLayout(false);
            rightPanel.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            statusStrip_Bottom.ResumeLayout(false);
            statusStrip_Bottom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ColumnHeader 原有檔名;
        private ColumnHeader 新檔名;
        private ColumnHeader 大小;
        private ColumnHeader 日期;
        private ColumnHeader 目錄;
        private ToolStripButton btnClearSelected;
        private ComboBox comboBoxAddRule;
        private Button buttonRename;
        private Button buttonUndo;
        private MenuStrip menuStripAddRule;
        private ToolStripMenuItem menuItemAddRule;
        private ToolStripMenuItem menuItemNumberingRule;
        private ToolStripMenuItem menuItemReplaceRule;
        private ToolStripMenuItem menuItemInsertRule;
        private ToolStripMenuItem menuItemGroupRule;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private StatusStrip statusStrip_Bottom;
        private ToolStripStatusLabel toolStripStatusLabel_News;
        private ToolStripStatusLabel toolStripStatusLabel_FilesInfo;
        private ContextMenuStrip contextMenuStrip_Files;
        private ToolStripMenuItem toolStripMenuItem_ClearSelected;
        private ToolStripMenuItem toolStripMenuItem_CopyFileNameToClipboard;
        private ToolStripMenuItem toolStripMenuItem_OpenFile;
    }
}