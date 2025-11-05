namespace Exercise01_WinForm {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            OpenFileButton = new Button();
            textBox1 = new TextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            ReadButton = new Button();
            textBox2 = new TextBox();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // OpenFileButton
            // 
            OpenFileButton.Location = new Point(440, 12);
            OpenFileButton.Name = "OpenFileButton";
            OpenFileButton.Size = new Size(112, 30);
            OpenFileButton.TabIndex = 0;
            OpenFileButton.Text = "開く...";
            OpenFileButton.UseVisualStyleBackColor = true;
            OpenFileButton.Click += OpenFileButton_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 57);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(658, 354);
            textBox1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(685, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // ReadButton
            // 
            ReadButton.Location = new Point(558, 12);
            ReadButton.Name = "ReadButton";
            ReadButton.Size = new Size(112, 30);
            ReadButton.TabIndex = 3;
            ReadButton.Text = "読み込む";
            ReadButton.UseVisualStyleBackColor = true;
            ReadButton.Click += ReadButton_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 17);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(422, 23);
            textBox2.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(685, 450);
            Controls.Add(textBox2);
            Controls.Add(ReadButton);
            Controls.Add(statusStrip1);
            Controls.Add(textBox1);
            Controls.Add(OpenFileButton);
            Name = "Form1";
            Text = "Form1";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OpenFileButton;
        private TextBox textBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Button ReadButton;
        private TextBox textBox2;
    }
}
