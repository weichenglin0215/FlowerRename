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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_FlowerRename));
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "123", "aaaaaa", "123" }, -1, Color.Empty, SystemColors.MenuHighlight, null);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "4555", "4555" }, -1);
            ListViewItem listViewItem3 = new ListViewItem(new string[] { "6666", "6666" }, -1);
            ListViewItem listViewItem4 = new ListViewItem(new string[] { "8888", "8888" }, -1);
            comboBoxAddRule = new ComboBox();
            ruleContainer = new Panel();
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
            columnFileName = new ColumnHeader();
            columnNewName = new ColumnHeader();
            columnPath = new ColumnHeader();
            columnSize = new ColumnHeader();
            rightPanel = new Panel();
            ruleContainer.SuspendLayout();
            toolStrip.SuspendLayout();
            rightPanel.SuspendLayout();
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
            comboBoxAddRule.Size = new Size(596, 28);
            comboBoxAddRule.TabIndex = 1;
            comboBoxAddRule.SelectedIndexChanged += comboBoxAddRule_SelectedIndexChanged;
            // 
            // ruleContainer
            // 
            ruleContainer.AutoScroll = true;
            ruleContainer.BackColor = SystemColors.ControlLight;
            ruleContainer.Controls.Add(buttonRename);
            ruleContainer.Controls.Add(comboBoxAddRule);
            ruleContainer.Dock = DockStyle.Left;
            ruleContainer.Location = new Point(0, 0);
            ruleContainer.Name = "ruleContainer";
            ruleContainer.Padding = new Padding(5);
            ruleContainer.Size = new Size(606, 761);
            ruleContainer.TabIndex = 1;
            // 
            // buttonRename
            // 
            buttonRename.BackColor = Color.LightSeaGreen;
            buttonRename.Dock = DockStyle.Bottom;
            buttonRename.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Bold);
            buttonRename.ForeColor = Color.White;
            buttonRename.Location = new Point(5, 696);
            buttonRename.Name = "buttonRename";
            buttonRename.Size = new Size(596, 60);
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
            toolStrip.Size = new Size(998, 48);
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
            fileListView.Columns.AddRange(new ColumnHeader[] { 原有檔名, 新檔名, 大小, 日期, 目錄 });
            fileListView.Dock = DockStyle.Fill;
            fileListView.FullRowSelect = true;
            fileListView.GridLines = true;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            listViewItem3.StateImageIndex = 0;
            listViewItem4.StateImageIndex = 0;
            fileListView.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3, listViewItem4 });
            fileListView.Location = new Point(0, 48);
            fileListView.Name = "fileListView";
            fileListView.Size = new Size(998, 713);
            fileListView.TabIndex = 0;
            fileListView.UseCompatibleStateImageBehavior = false;
            fileListView.View = View.Details;
            fileListView.ColumnClick += fileListView_ColumnClick;
            // 
            // 原有檔名
            // 
            原有檔名.Text = "原有檔名";
            原有檔名.Width = 300;
            // 
            // 新檔名
            // 
            新檔名.Text = "新檔名";
            新檔名.Width = 300;
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
            // rightPanel
            // 
            rightPanel.Controls.Add(fileListView);
            rightPanel.Controls.Add(toolStrip);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Location = new Point(606, 0);
            rightPanel.Name = "rightPanel";
            rightPanel.Size = new Size(998, 761);
            rightPanel.TabIndex = 0;
            // 
            // Form_FlowerRename
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1604, 761);
            Controls.Add(rightPanel);
            Controls.Add(ruleContainer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form_FlowerRename";
            Text = "開心花更名 V0.4.0(增加拖入多個檔案功能)";
            DragDrop += Form_FlowerRename_DragDrop;
            DragEnter += Form_FlowerRename_DragEnter;
            ruleContainer.ResumeLayout(false);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            rightPanel.ResumeLayout(false);
            ResumeLayout(false);
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
    }
}