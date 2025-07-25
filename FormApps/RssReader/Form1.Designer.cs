namespace RssReader {
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
            btRssGet = new Button();
            lbTitles = new ListBox();
            wvRssLink = new Microsoft.Web.WebView2.WinForms.WebView2();
            btGoBack = new Button();
            btGoForward = new Button();
            cbURL = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            tbAdd = new TextBox();
            btAdd = new Button();
            statusStrip1 = new StatusStrip();
            tsslMessage = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)wvRssLink).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // btRssGet
            // 
            btRssGet.Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btRssGet.Location = new Point(577, 11);
            btRssGet.Name = "btRssGet";
            btRssGet.Size = new Size(91, 33);
            btRssGet.TabIndex = 1;
            btRssGet.Text = "取得";
            btRssGet.UseVisualStyleBackColor = true;
            btRssGet.Click += btRssGet_Click;
            // 
            // lbTitles
            // 
            lbTitles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lbTitles.Font = new Font("メイリオ", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            lbTitles.FormattingEnabled = true;
            lbTitles.ItemHeight = 24;
            lbTitles.Location = new Point(12, 102);
            lbTitles.Name = "lbTitles";
            lbTitles.Size = new Size(656, 196);
            lbTitles.TabIndex = 2;
            lbTitles.Click += lbTitles_Click;
            // 
            // wvRssLink
            // 
            wvRssLink.AllowExternalDrop = true;
            wvRssLink.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            wvRssLink.CreationProperties = null;
            wvRssLink.DefaultBackgroundColor = Color.White;
            wvRssLink.Location = new Point(12, 304);
            wvRssLink.Name = "wvRssLink";
            wvRssLink.Size = new Size(656, 314);
            wvRssLink.TabIndex = 3;
            wvRssLink.ZoomFactor = 1D;
            wvRssLink.SourceChanged += wvRssLink_SourceChanged;
            // 
            // btGoBack
            // 
            btGoBack.Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoBack.Location = new Point(12, 12);
            btGoBack.Name = "btGoBack";
            btGoBack.Size = new Size(42, 33);
            btGoBack.TabIndex = 4;
            btGoBack.Text = "戻る";
            btGoBack.UseVisualStyleBackColor = true;
            btGoBack.Click += btGoBack_Click;
            // 
            // btGoForward
            // 
            btGoForward.Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btGoForward.Location = new Point(60, 11);
            btGoForward.Name = "btGoForward";
            btGoForward.Size = new Size(42, 33);
            btGoForward.TabIndex = 4;
            btGoForward.Text = "進む";
            btGoForward.UseVisualStyleBackColor = true;
            btGoForward.Click += btGoForward_Click;
            // 
            // cbURL
            // 
            cbURL.Font = new Font("メイリオ", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            cbURL.FormattingEnabled = true;
            cbURL.Location = new Point(223, 11);
            cbURL.Name = "cbURL";
            cbURL.Size = new Size(348, 32);
            cbURL.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(118, 20);
            label1.Name = "label1";
            label1.Size = new Size(101, 15);
            label1.TabIndex = 6;
            label1.Text = "お気に入りのURL：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(125, 63);
            label2.Name = "label2";
            label2.Size = new Size(94, 15);
            label2.TabIndex = 6;
            label2.Text = "お気に入り登録：";
            // 
            // tbAdd
            // 
            tbAdd.Font = new Font("メイリオ", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            tbAdd.Location = new Point(223, 55);
            tbAdd.Name = "tbAdd";
            tbAdd.Size = new Size(348, 31);
            tbAdd.TabIndex = 7;
            // 
            // btAdd
            // 
            btAdd.Font = new Font("メイリオ", 9F, FontStyle.Regular, GraphicsUnit.Point, 128);
            btAdd.Location = new Point(577, 55);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(91, 32);
            btAdd.TabIndex = 8;
            btAdd.Text = "登録";
            btAdd.UseVisualStyleBackColor = true;
            btAdd.Click += btAdd_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsslMessage });
            statusStrip1.Location = new Point(0, 621);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(680, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            tsslMessage.Name = "toolStripStatusLabel1";
            tsslMessage.Size = new Size(0, 17);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 643);
            Controls.Add(statusStrip1);
            Controls.Add(btAdd);
            Controls.Add(tbAdd);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cbURL);
            Controls.Add(btGoForward);
            Controls.Add(btGoBack);
            Controls.Add(wvRssLink);
            Controls.Add(lbTitles);
            Controls.Add(btRssGet);
            Name = "Form1";
            Text = "RSSリーダー";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)wvRssLink).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btRssGet;
        private ListBox lbTitles;
        private Microsoft.Web.WebView2.WinForms.WebView2 wvRssLink;
        private Button btGoBack;
        private Button btGoForward;
        private ComboBox cbURL;
        private Label label1;
        private Label label2;
        private TextBox tbAdd;
        private Button btAdd;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsslMessage;
    }
}
