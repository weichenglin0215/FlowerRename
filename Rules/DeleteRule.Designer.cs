
namespace FlowerRename
{
    partial class DeleteRuleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            startLabel = new Label();
            fromstartComboBox = new ComboBox();
            startInsertNumberNumericUpDown = new NumericUpDown();
            countLabel = new Label();
            countNumericUpDown = new NumericUpDown();
            closeBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)startInsertNumberNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)countNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // startLabel
            // 
            startLabel.AutoSize = true;
            startLabel.Location = new Point(31, 55);
            startLabel.Margin = new Padding(5, 0, 5, 0);
            startLabel.Name = "startLabel";
            startLabel.Size = new Size(89, 20);
            startLabel.TabIndex = 0;
            startLabel.Text = "位置開始：";
            // 
            // fromstartComboBox
            // 
            fromstartComboBox.FormattingEnabled = true;
            fromstartComboBox.Items.AddRange(new object[] { "從頭開始找", "從後面找起" });
            fromstartComboBox.Location = new Point(10, 10);
            fromstartComboBox.Margin = new Padding(5, 5, 5, 5);
            fromstartComboBox.Name = "fromstartComboBox";
            fromstartComboBox.Size = new Size(199, 28);
            fromstartComboBox.TabIndex = 1;
            fromstartComboBox.Text = "從開頭開始";
            // 
            // startInsertNumberNumericUpDown
            // 
            startInsertNumberNumericUpDown.Location = new Point(129, 52);
            startInsertNumberNumericUpDown.Margin = new Padding(5, 5, 5, 5);
            startInsertNumberNumericUpDown.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            startInsertNumberNumericUpDown.Name = "startInsertNumberNumericUpDown";
            startInsertNumberNumericUpDown.Size = new Size(77, 28);
            startInsertNumberNumericUpDown.TabIndex = 2;
            startInsertNumberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // countLabel
            // 
            countLabel.AutoSize = true;
            countLabel.Location = new Point(266, 55);
            countLabel.Margin = new Padding(5, 0, 5, 0);
            countLabel.Name = "countLabel";
            countLabel.Size = new Size(89, 20);
            countLabel.TabIndex = 3;
            countLabel.Text = "刪除字數：";
            // 
            // countNumericUpDown
            // 
            countNumericUpDown.Location = new Point(365, 52);
            countNumericUpDown.Margin = new Padding(5, 5, 5, 5);
            countNumericUpDown.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            countNumericUpDown.Name = "countNumericUpDown";
            countNumericUpDown.Size = new Size(77, 28);
            countNumericUpDown.TabIndex = 4;
            countNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // closeBtn
            // 
            closeBtn.ForeColor = Color.Red;
            closeBtn.Location = new Point(565, 5);
            closeBtn.Margin = new Padding(5, 5, 5, 5);
            closeBtn.Name = "closeBtn";
            closeBtn.Size = new Size(25, 25);
            closeBtn.TabIndex = 10;
            closeBtn.Text = "X";
            closeBtn.UseVisualStyleBackColor = true;
            closeBtn.Click += closeBtn_Click;
            // 
            // DeleteRuleControl
            // 
            AutoScaleDimensions = new SizeF(10F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(closeBtn);
            Controls.Add(countNumericUpDown);
            Controls.Add(countLabel);
            Controls.Add(startInsertNumberNumericUpDown);
            Controls.Add(fromstartComboBox);
            Controls.Add(startLabel);
            Margin = new Padding(5, 5, 5, 5);
            Name = "DeleteRuleControl";
            Size = new Size(598, 90);
            ((System.ComponentModel.ISupportInitialize)startInsertNumberNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)countNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label startLabel;
        public System.Windows.Forms.ComboBox fromstartComboBox;
        public System.Windows.Forms.NumericUpDown startInsertNumberNumericUpDown;
        private System.Windows.Forms.Label countLabel;
        public System.Windows.Forms.NumericUpDown countNumericUpDown;
        public System.Windows.Forms.Button closeBtn;
    }
}
