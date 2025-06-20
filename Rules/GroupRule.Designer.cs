namespace FlowerRename
{
    partial class GroupRuleControl// : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        public TextBox GroupFileNameTextBox;
        public NumericUpDown startNumberNumericUpDown;
        public NumericUpDown incNumberNumericUpDown;
        public NumericUpDown paddingNumericUpDown;
        public Button moveDownBtn;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

        private void InitializeComponent()
        {
            GroupFileNameTextBox = new TextBox();
            startNumberNumericUpDown = new NumericUpDown();
            paddingNumericUpDown = new NumericUpDown();
            moveDownBtn = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            incNumberNumericUpDown = new NumericUpDown();
            closeBtn = new Button();
            moveUpBtn = new Button();
            label5 = new Label();
            groupAmountUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)startNumberNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)paddingNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)incNumberNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAmountUpDown).BeginInit();
            SuspendLayout();
            // 
            // GroupFileNameTextBox
            // 
            GroupFileNameTextBox.Location = new Point(10, 35);
            GroupFileNameTextBox.Name = "GroupFileNameTextBox";
            GroupFileNameTextBox.PlaceholderText = "原始檔名";
            GroupFileNameTextBox.Size = new Size(580, 28);
            GroupFileNameTextBox.TabIndex = 0;
            // 
            // startNumberNumericUpDown
            // 
            startNumberNumericUpDown.Location = new Point(100, 73);
            startNumberNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            startNumberNumericUpDown.Name = "startNumberNumericUpDown";
            startNumberNumericUpDown.Size = new Size(80, 28);
            startNumberNumericUpDown.TabIndex = 0;
            startNumberNumericUpDown.TextAlign = HorizontalAlignment.Right;
            startNumberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // paddingNumericUpDown
            // 
            paddingNumericUpDown.Location = new Point(508, 73);
            paddingNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            paddingNumericUpDown.Name = "paddingNumericUpDown";
            paddingNumericUpDown.Size = new Size(80, 28);
            paddingNumericUpDown.TabIndex = 0;
            paddingNumericUpDown.TextAlign = HorizontalAlignment.Right;
            paddingNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // moveDownBtn
            // 
            moveDownBtn.Location = new Point(534, 5);
            moveDownBtn.Name = "moveDownBtn";
            moveDownBtn.Size = new Size(25, 25);
            moveDownBtn.TabIndex = 0;
            moveDownBtn.Text = "▼";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 10);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 2;
            label1.Text = "基礎檔名";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 77);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 3;
            label2.Text = "起始數字：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(418, 77);
            label3.Name = "label3";
            label3.Size = new Size(89, 20);
            label3.TabIndex = 4;
            label3.Text = "補零位數：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(215, 77);
            label4.Name = "label4";
            label4.Size = new Size(89, 20);
            label4.TabIndex = 6;
            label4.Text = "間隔數字：";
            // 
            // incNumberNumericUpDown
            // 
            incNumberNumericUpDown.Location = new Point(305, 73);
            incNumberNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            incNumberNumericUpDown.Name = "incNumberNumericUpDown";
            incNumberNumericUpDown.Size = new Size(80, 28);
            incNumberNumericUpDown.TabIndex = 5;
            incNumberNumericUpDown.TextAlign = HorizontalAlignment.Right;
            incNumberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // closeBtn
            // 
            closeBtn.ForeColor = Color.Red;
            closeBtn.Location = new Point(565, 5);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(25, 25);
            closeBtn.TabIndex = 7;
            closeBtn.Text = "X";
            closeBtn.Click += closeBtn_Click;
            // 
            // moveUpBtn
            // 
            moveUpBtn.Location = new Point(503, 5);
            moveUpBtn.Name = "moveUpBtn";
            moveUpBtn.Size = new Size(25, 25);
            moveUpBtn.TabIndex = 8;
            moveUpBtn.Text = "▲";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(10, 117);
            label5.Name = "label5";
            label5.Size = new Size(264, 20);
            label5.TabIndex = 10;
            label5.Text = "群組數量(2以上，預設A、B、C...)：";
            // 
            // groupAmountUpDown
            // 
            groupAmountUpDown.Location = new Point(280, 115);
            groupAmountUpDown.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            groupAmountUpDown.Name = "groupAmountUpDown";
            groupAmountUpDown.Size = new Size(80, 28);
            groupAmountUpDown.TabIndex = 9;
            groupAmountUpDown.TextAlign = HorizontalAlignment.Right;
            groupAmountUpDown.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // GroupRuleControl
            // 
            BackColor = Color.AliceBlue;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(label5);
            Controls.Add(groupAmountUpDown);
            Controls.Add(moveUpBtn);
            Controls.Add(closeBtn);
            Controls.Add(label4);
            Controls.Add(incNumberNumericUpDown);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(GroupFileNameTextBox);
            Controls.Add(startNumberNumericUpDown);
            Controls.Add(paddingNumericUpDown);
            Controls.Add(moveDownBtn);
            Name = "GroupRuleControl";
            Size = new Size(598, 153);
            ((System.ComponentModel.ISupportInitialize)startNumberNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)paddingNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)incNumberNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAmountUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        public Button closeBtn;
        public Button moveUpBtn;
        private Label label5;
        public NumericUpDown groupAmountUpDown;
    }
}