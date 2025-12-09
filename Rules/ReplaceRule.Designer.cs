namespace FlowerRename
{
    partial class ReplaceRuleControl// : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        public TextBox baseFileNameTextBox;
        public NumericUpDown startNumberNumericUpDown;
        private Label label1;
        private Label label2;

        private void InitializeComponent()
        {
            baseFileNameTextBox = new TextBox();
            startNumberNumericUpDown = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            replaceFileNameTextBox = new TextBox();
            moveUpBtn = new Button();
            closeBtn = new Button();
            moveDownBtn = new Button();
            replaceAllStringCheckBox = new CheckBox();
            fromstartComboBox = new ComboBox();
            ordinalIgnoreCaseCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)startNumberNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // baseFileNameTextBox
            // 
            baseFileNameTextBox.Location = new Point(10, 35);
            baseFileNameTextBox.Name = "baseFileNameTextBox";
            baseFileNameTextBox.PlaceholderText = "原始字串";
            baseFileNameTextBox.Size = new Size(278, 28);
            baseFileNameTextBox.TabIndex = 0;
            // 
            // startNumberNumericUpDown
            // 
            startNumberNumericUpDown.Enabled = false;
            startNumberNumericUpDown.Location = new Point(241, 74);
            startNumberNumericUpDown.Maximum = new decimal(new int[] { 128, 0, 0, 0 });
            startNumberNumericUpDown.Name = "startNumberNumericUpDown";
            startNumberNumericUpDown.Size = new Size(80, 28);
            startNumberNumericUpDown.TabIndex = 0;
            startNumberNumericUpDown.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 12);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 2;
            label1.Text = "原始字串";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 78);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 3;
            label2.Text = "起始位置：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(311, 12);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 8;
            label3.Text = "置換字串";
            // 
            // replaceFileNameTextBox
            // 
            replaceFileNameTextBox.Location = new Point(314, 35);
            replaceFileNameTextBox.Name = "replaceFileNameTextBox";
            replaceFileNameTextBox.PlaceholderText = "置換字串";
            replaceFileNameTextBox.Size = new Size(275, 28);
            replaceFileNameTextBox.TabIndex = 7;
            // 
            // moveUpBtn
            // 
            moveUpBtn.Location = new Point(503, 5);
            moveUpBtn.Name = "moveUpBtn";
            moveUpBtn.Size = new Size(25, 25);
            moveUpBtn.TabIndex = 11;
            moveUpBtn.Text = "▲";
            // 
            // closeBtn
            // 
            closeBtn.ForeColor = Color.Red;
            closeBtn.Location = new Point(565, 5);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(25, 25);
            closeBtn.TabIndex = 10;
            closeBtn.Text = "X";
            closeBtn.Click += closeBtn_Click;
            // 
            // moveDownBtn
            // 
            moveDownBtn.Location = new Point(534, 5);
            moveDownBtn.Name = "moveDownBtn";
            moveDownBtn.Size = new Size(25, 25);
            moveDownBtn.TabIndex = 9;
            moveDownBtn.Text = "▼";
            // 
            // replaceAllStringCheckBox
            // 
            replaceAllStringCheckBox.AutoSize = true;
            replaceAllStringCheckBox.Checked = true;
            replaceAllStringCheckBox.CheckState = CheckState.Checked;
            replaceAllStringCheckBox.Location = new Point(465, 76);
            replaceAllStringCheckBox.Name = "replaceAllStringCheckBox";
            replaceAllStringCheckBox.Size = new Size(124, 24);
            replaceAllStringCheckBox.TabIndex = 12;
            replaceAllStringCheckBox.Text = "替換所有字串";
            replaceAllStringCheckBox.UseVisualStyleBackColor = true;
            replaceAllStringCheckBox.CheckedChanged += replaceAllStringCheckBox_CheckedChanged;
            // 
            // fromstartComboBox
            // 
            fromstartComboBox.Enabled = false;
            fromstartComboBox.FormattingEnabled = true;
            fromstartComboBox.Items.AddRange(new object[] { "從頭開始找", "從後面找起" });
            fromstartComboBox.Location = new Point(10, 74);
            fromstartComboBox.Name = "fromstartComboBox";
            fromstartComboBox.Size = new Size(128, 28);
            fromstartComboBox.TabIndex = 13;
            fromstartComboBox.Text = "從頭開始找";
            // 
            // ordinalIgnoreCaseCheckBox
            // 
            ordinalIgnoreCaseCheckBox.AutoSize = true;
            ordinalIgnoreCaseCheckBox.Checked = true;
            ordinalIgnoreCaseCheckBox.CheckState = CheckState.Checked;
            ordinalIgnoreCaseCheckBox.Location = new Point(336, 76);
            ordinalIgnoreCaseCheckBox.Name = "ordinalIgnoreCaseCheckBox";
            ordinalIgnoreCaseCheckBox.Size = new Size(108, 24);
            ordinalIgnoreCaseCheckBox.TabIndex = 14;
            ordinalIgnoreCaseCheckBox.Text = "忽略大小寫";
            ordinalIgnoreCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReplaceRuleControl
            // 
            BackColor = Color.AliceBlue;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(ordinalIgnoreCaseCheckBox);
            Controls.Add(fromstartComboBox);
            Controls.Add(replaceAllStringCheckBox);
            Controls.Add(moveUpBtn);
            Controls.Add(closeBtn);
            Controls.Add(moveDownBtn);
            Controls.Add(label3);
            Controls.Add(replaceFileNameTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(baseFileNameTextBox);
            Controls.Add(startNumberNumericUpDown);
            Name = "ReplaceRuleControl";
            Size = new Size(598, 110);
            ((System.ComponentModel.ISupportInitialize)startNumberNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private Label label3;
        public TextBox replaceFileNameTextBox;
        public Button moveUpBtn;
        public Button closeBtn;
        public Button moveDownBtn;
        public CheckBox replaceAllStringCheckBox;
        public ComboBox fromstartComboBox;
        public CheckBox ordinalIgnoreCaseCheckBox;
    }
}