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
            this.baseFileNameTextBox = new System.Windows.Forms.TextBox();
            this.startNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.replaceFileNameTextBox = new System.Windows.Forms.TextBox();
            this.moveUpBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.moveDownBtn = new System.Windows.Forms.Button();
            this.replaceAllStringCheckBox = new System.Windows.Forms.CheckBox();
            this.fromstartComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.startNumberNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // baseFileNameTextBox
            // 
            this.baseFileNameTextBox.Location = new System.Drawing.Point(10, 35);
            this.baseFileNameTextBox.Name = "baseFileNameTextBox";
            this.baseFileNameTextBox.PlaceholderText = "原始字串";
            this.baseFileNameTextBox.Size = new System.Drawing.Size(278, 28);
            this.baseFileNameTextBox.TabIndex = 0;
            // 
            // startNumberNumericUpDown
            // 
            this.startNumberNumericUpDown.Enabled = false;
            this.startNumberNumericUpDown.Location = new System.Drawing.Point(241, 74);
            this.startNumberNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startNumberNumericUpDown.Name = "startNumberNumericUpDown";
            this.startNumberNumericUpDown.Size = new System.Drawing.Size(80, 28);
            this.startNumberNumericUpDown.TabIndex = 0;
            this.startNumberNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "原始字串";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(151, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "起始位置：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "置換字串";
            // 
            // replaceFileNameTextBox
            // 
            this.replaceFileNameTextBox.Location = new System.Drawing.Point(314, 35);
            this.replaceFileNameTextBox.Name = "replaceFileNameTextBox";
            this.replaceFileNameTextBox.PlaceholderText = "置換字串";
            this.replaceFileNameTextBox.Size = new System.Drawing.Size(275, 28);
            this.replaceFileNameTextBox.TabIndex = 7;
            // 
            // moveUpBtn
            // 
            this.moveUpBtn.Location = new System.Drawing.Point(503, 5);
            this.moveUpBtn.Name = "moveUpBtn";
            this.moveUpBtn.Size = new System.Drawing.Size(25, 25);
            this.moveUpBtn.TabIndex = 11;
            this.moveUpBtn.Text = "▲";
            // 
            // closeBtn
            // 
            this.closeBtn.ForeColor = System.Drawing.Color.Red;
            this.closeBtn.Location = new System.Drawing.Point(565, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(25, 25);
            this.closeBtn.TabIndex = 10;
            this.closeBtn.Text = "X";
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // moveDownBtn
            // 
            this.moveDownBtn.Location = new System.Drawing.Point(534, 5);
            this.moveDownBtn.Name = "moveDownBtn";
            this.moveDownBtn.Size = new System.Drawing.Size(25, 25);
            this.moveDownBtn.TabIndex = 9;
            this.moveDownBtn.Text = "▼";
            // 
            // replaceAllStringCheckBox
            // 
            this.replaceAllStringCheckBox.AutoSize = true;
            this.replaceAllStringCheckBox.Checked = true;
            this.replaceAllStringCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.replaceAllStringCheckBox.Location = new System.Drawing.Point(465, 76);
            this.replaceAllStringCheckBox.Name = "replaceAllStringCheckBox";
            this.replaceAllStringCheckBox.Size = new System.Drawing.Size(124, 24);
            this.replaceAllStringCheckBox.TabIndex = 12;
            this.replaceAllStringCheckBox.Text = "替換所有字串";
            this.replaceAllStringCheckBox.UseVisualStyleBackColor = true;
            this.replaceAllStringCheckBox.CheckedChanged += new System.EventHandler(this.replaceAllStringCheckBox_CheckedChanged);
            // 
            // fromstartComboBox
            // 
            this.fromstartComboBox.Enabled = false;
            this.fromstartComboBox.FormattingEnabled = true;
            this.fromstartComboBox.Items.AddRange(new object[] {
            "從頭開始找",
            "從後面找起"});
            this.fromstartComboBox.Location = new System.Drawing.Point(10, 74);
            this.fromstartComboBox.Name = "fromstartComboBox";
            this.fromstartComboBox.Size = new System.Drawing.Size(128, 28);
            this.fromstartComboBox.TabIndex = 13;
            this.fromstartComboBox.Text = "從頭開始找";
            // 
            // ReplaceRuleControl
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.fromstartComboBox);
            this.Controls.Add(this.replaceAllStringCheckBox);
            this.Controls.Add(this.moveUpBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.moveDownBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.replaceFileNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.baseFileNameTextBox);
            this.Controls.Add(this.startNumberNumericUpDown);
            this.Name = "ReplaceRuleControl";
            this.Size = new System.Drawing.Size(598, 110);
            ((System.ComponentModel.ISupportInitialize)(this.startNumberNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label label3;
        public TextBox replaceFileNameTextBox;
        public Button moveUpBtn;
        public Button closeBtn;
        public Button moveDownBtn;
        public CheckBox replaceAllStringCheckBox;
        public ComboBox fromstartComboBox;
    }
}