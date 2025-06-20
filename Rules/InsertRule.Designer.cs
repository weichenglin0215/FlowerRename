namespace FlowerRename
{
    partial class InsertRuleControl// : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        public TextBox baseFileNameTextBox;
        public NumericUpDown startInsertNumberNumericUpDown;
        private Label label1;
        private Label label2;

        private void InitializeComponent()
        {
            baseFileNameTextBox = new TextBox();
            startInsertNumberNumericUpDown = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            InsertFileNameTextBox = new TextBox();
            moveUpBtn = new Button();
            closeBtn = new Button();
            moveDownBtn = new Button();
            InsertNumberCheckBox = new CheckBox();
            fromstartComboBox = new ComboBox();
            label4 = new Label();
            incNumberNumericUpDown = new NumericUpDown();
            label5 = new Label();
            label6 = new Label();
            startNumberNumericUpDown = new NumericUpDown();
            paddingNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)startInsertNumberNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)incNumberNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startNumberNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)paddingNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // baseFileNameTextBox
            // 
            baseFileNameTextBox.Location = new Point(10, 35);
            baseFileNameTextBox.Name = "baseFileNameTextBox";
            baseFileNameTextBox.PlaceholderText = "插入字串";
            baseFileNameTextBox.Size = new Size(278, 28);
            baseFileNameTextBox.TabIndex = 0;
            // 
            // startInsertNumberNumericUpDown
            // 
            startInsertNumberNumericUpDown.Location = new Point(278, 74);
            startInsertNumberNumericUpDown.Name = "startInsertNumberNumericUpDown";
            startInsertNumberNumericUpDown.Size = new Size(80, 28);
            startInsertNumberNumericUpDown.TabIndex = 0;
            startInsertNumberNumericUpDown.TextAlign = HorizontalAlignment.Right;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 12);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 2;
            label1.Text = "插入字串";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 78);
            label2.Name = "label2";
            label2.Size = new Size(121, 20);
            label2.TabIndex = 3;
            label2.Text = "起始插入位置：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(311, 12);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 8;
            label3.Text = "字串(無功能)";
            // 
            // InsertFileNameTextBox
            // 
            InsertFileNameTextBox.Location = new Point(314, 35);
            InsertFileNameTextBox.Name = "InsertFileNameTextBox";
            InsertFileNameTextBox.PlaceholderText = "字串(無功能)";
            InsertFileNameTextBox.Size = new Size(275, 28);
            InsertFileNameTextBox.TabIndex = 7;
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
            // InsertNumberCheckBox
            // 
            InsertNumberCheckBox.AutoSize = true;
            InsertNumberCheckBox.Location = new Point(14, 133);
            InsertNumberCheckBox.Name = "InsertNumberCheckBox";
            InsertNumberCheckBox.Size = new Size(124, 24);
            InsertNumberCheckBox.TabIndex = 12;
            InsertNumberCheckBox.Text = "插入連續數字";
            InsertNumberCheckBox.UseVisualStyleBackColor = true;
            InsertNumberCheckBox.CheckedChanged += InsertNumberCheckBox_CheckedChanged;
            // 
            // fromstartComboBox
            // 
            fromstartComboBox.FormattingEnabled = true;
            fromstartComboBox.Items.AddRange(new object[] { "從頭開始找", "從後面找起" });
            fromstartComboBox.Location = new Point(10, 74);
            fromstartComboBox.Name = "fromstartComboBox";
            fromstartComboBox.Size = new Size(128, 28);
            fromstartComboBox.TabIndex = 13;
            fromstartComboBox.Text = "從頭開始找";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(215, 167);
            label4.Name = "label4";
            label4.Size = new Size(89, 20);
            label4.TabIndex = 19;
            label4.Text = "間隔數字：";
            // 
            // incNumberNumericUpDown
            // 
            incNumberNumericUpDown.Enabled = false;
            incNumberNumericUpDown.Location = new Point(305, 163);
            incNumberNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            incNumberNumericUpDown.Name = "incNumberNumericUpDown";
            incNumberNumericUpDown.Size = new Size(80, 28);
            incNumberNumericUpDown.TabIndex = 18;
            incNumberNumericUpDown.TextAlign = HorizontalAlignment.Right;
            incNumberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(418, 167);
            label5.Name = "label5";
            label5.Size = new Size(89, 20);
            label5.TabIndex = 17;
            label5.Text = "補零位數：";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 167);
            label6.Name = "label6";
            label6.Size = new Size(89, 20);
            label6.TabIndex = 16;
            label6.Text = "起始數字：";
            // 
            // startNumberNumericUpDown
            // 
            startNumberNumericUpDown.Enabled = false;
            startNumberNumericUpDown.Location = new Point(100, 163);
            startNumberNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            startNumberNumericUpDown.Name = "startNumberNumericUpDown";
            startNumberNumericUpDown.Size = new Size(80, 28);
            startNumberNumericUpDown.TabIndex = 14;
            startNumberNumericUpDown.TextAlign = HorizontalAlignment.Right;
            startNumberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // paddingNumericUpDown
            // 
            paddingNumericUpDown.Enabled = false;
            paddingNumericUpDown.Location = new Point(508, 163);
            paddingNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            paddingNumericUpDown.Name = "paddingNumericUpDown";
            paddingNumericUpDown.Size = new Size(80, 28);
            paddingNumericUpDown.TabIndex = 15;
            paddingNumericUpDown.TextAlign = HorizontalAlignment.Right;
            paddingNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // InsertRuleControl
            // 
            BackColor = Color.AliceBlue;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(label4);
            Controls.Add(incNumberNumericUpDown);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(startNumberNumericUpDown);
            Controls.Add(paddingNumericUpDown);
            Controls.Add(fromstartComboBox);
            Controls.Add(InsertNumberCheckBox);
            Controls.Add(moveUpBtn);
            Controls.Add(closeBtn);
            Controls.Add(moveDownBtn);
            Controls.Add(label3);
            Controls.Add(InsertFileNameTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(baseFileNameTextBox);
            Controls.Add(startInsertNumberNumericUpDown);
            Name = "InsertRuleControl";
            Size = new Size(598, 200);
            ((System.ComponentModel.ISupportInitialize)startInsertNumberNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)incNumberNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)startNumberNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)paddingNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label3;
        public TextBox InsertFileNameTextBox;
        public Button moveUpBtn;
        public Button closeBtn;
        public Button moveDownBtn;
        public CheckBox InsertNumberCheckBox;
        public ComboBox fromstartComboBox;
        private Label label4;
        public NumericUpDown incNumberNumericUpDown;
        private Label label5;
        private Label label6;
        public NumericUpDown startNumberNumericUpDown;
        public NumericUpDown paddingNumericUpDown;
    }
}