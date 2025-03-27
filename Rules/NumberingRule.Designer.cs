namespace FlowerRename
{
    partial class NumberingRuleControl// : UserControl
    {
        private System.ComponentModel.IContainer components = null;
        public TextBox baseFileNameTextBox;
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
            this.baseFileNameTextBox = new System.Windows.Forms.TextBox();
            this.startNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.paddingNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.moveDownBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.incNumberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.closeBtn = new System.Windows.Forms.Button();
            this.moveUpBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.startNumberNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddingNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.incNumberNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // baseFileNameTextBox
            // 
            this.baseFileNameTextBox.Location = new System.Drawing.Point(10, 35);
            this.baseFileNameTextBox.Name = "baseFileNameTextBox";
            this.baseFileNameTextBox.PlaceholderText = "原始檔名";
            this.baseFileNameTextBox.Size = new System.Drawing.Size(580, 28);
            this.baseFileNameTextBox.TabIndex = 0;
            // 
            // startNumberNumericUpDown
            // 
            this.startNumberNumericUpDown.Location = new System.Drawing.Point(100, 73);
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
            // paddingNumericUpDown
            // 
            this.paddingNumericUpDown.Location = new System.Drawing.Point(508, 73);
            this.paddingNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.paddingNumericUpDown.Name = "paddingNumericUpDown";
            this.paddingNumericUpDown.Size = new System.Drawing.Size(80, 28);
            this.paddingNumericUpDown.TabIndex = 0;
            this.paddingNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // moveDownBtn
            // 
            this.moveDownBtn.Location = new System.Drawing.Point(534, 5);
            this.moveDownBtn.Name = "moveDownBtn";
            this.moveDownBtn.Size = new System.Drawing.Size(25, 25);
            this.moveDownBtn.TabIndex = 0;
            this.moveDownBtn.Text = "▼";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "基礎檔名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "起始數字：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(418, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "補零位數：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(215, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "間隔數字：";
            // 
            // incNumberNumericUpDown
            // 
            this.incNumberNumericUpDown.Location = new System.Drawing.Point(305, 73);
            this.incNumberNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.incNumberNumericUpDown.Name = "incNumberNumericUpDown";
            this.incNumberNumericUpDown.Size = new System.Drawing.Size(80, 28);
            this.incNumberNumericUpDown.TabIndex = 5;
            this.incNumberNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // closeBtn
            // 
            this.closeBtn.ForeColor = System.Drawing.Color.Red;
            this.closeBtn.Location = new System.Drawing.Point(565, 5);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(25, 25);
            this.closeBtn.TabIndex = 7;
            this.closeBtn.Text = "X";
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // moveUpBtn
            // 
            this.moveUpBtn.Location = new System.Drawing.Point(503, 5);
            this.moveUpBtn.Name = "moveUpBtn";
            this.moveUpBtn.Size = new System.Drawing.Size(25, 25);
            this.moveUpBtn.TabIndex = 8;
            this.moveUpBtn.Text = "▲";
            // 
            // NumberingRuleControl
            // 
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.moveUpBtn);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.incNumberNumericUpDown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.baseFileNameTextBox);
            this.Controls.Add(this.startNumberNumericUpDown);
            this.Controls.Add(this.paddingNumericUpDown);
            this.Controls.Add(this.moveDownBtn);
            this.Name = "NumberingRuleControl";
            this.Size = new System.Drawing.Size(598, 110);
            ((System.ComponentModel.ISupportInitialize)(this.startNumberNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddingNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.incNumberNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public Button closeBtn;
        public Button moveUpBtn;
    }
}