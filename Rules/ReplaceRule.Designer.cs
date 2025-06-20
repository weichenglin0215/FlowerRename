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
            ((System.ComponentModel.ISupportInitialize)startNumberNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // baseFileNameTextBox
            // 
            baseFileNameTextBox.Location = new Point(10, 35);
            baseFileNameTextBox.Name = "baseFileNameTextBox";
            baseFileNameTextBox.PlaceholderText = "��l�r��";
            baseFileNameTextBox.Size = new Size(278, 28);
            baseFileNameTextBox.TabIndex = 0;
            // 
            // startNumberNumericUpDown
            // 
            startNumberNumericUpDown.Enabled = false;
            startNumberNumericUpDown.Location = new Point(241, 74);
            startNumberNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            startNumberNumericUpDown.Name = "startNumberNumericUpDown";
            startNumberNumericUpDown.Size = new Size(80, 28);
            startNumberNumericUpDown.TabIndex = 0;
            startNumberNumericUpDown.TextAlign = HorizontalAlignment.Right;
            startNumberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 12);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 2;
            label1.Text = "��l�r��";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 78);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 3;
            label2.Text = "�_�l��m�G";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(311, 12);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 8;
            label3.Text = "�m���r��";
            // 
            // replaceFileNameTextBox
            // 
            replaceFileNameTextBox.Location = new Point(314, 35);
            replaceFileNameTextBox.Name = "replaceFileNameTextBox";
            replaceFileNameTextBox.PlaceholderText = "�m���r��";
            replaceFileNameTextBox.Size = new Size(275, 28);
            replaceFileNameTextBox.TabIndex = 7;
            // 
            // moveUpBtn
            // 
            moveUpBtn.Location = new Point(503, 5);
            moveUpBtn.Name = "moveUpBtn";
            moveUpBtn.Size = new Size(25, 25);
            moveUpBtn.TabIndex = 11;
            moveUpBtn.Text = "��";
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
            moveDownBtn.Text = "��";
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
            replaceAllStringCheckBox.Text = "�����Ҧ��r��";
            replaceAllStringCheckBox.UseVisualStyleBackColor = true;
            replaceAllStringCheckBox.CheckedChanged += replaceAllStringCheckBox_CheckedChanged;
            // 
            // fromstartComboBox
            // 
            fromstartComboBox.Enabled = false;
            fromstartComboBox.FormattingEnabled = true;
            fromstartComboBox.Items.AddRange(new object[] { "�q�Y�}�l��", "�q�᭱��_" });
            fromstartComboBox.Location = new Point(10, 74);
            fromstartComboBox.Name = "fromstartComboBox";
            fromstartComboBox.Size = new Size(128, 28);
            fromstartComboBox.TabIndex = 13;
            fromstartComboBox.Text = "�q�Y�}�l��";
            // 
            // ReplaceRuleControl
            // 
            BackColor = Color.AliceBlue;
            BorderStyle = BorderStyle.Fixed3D;
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
    }
}