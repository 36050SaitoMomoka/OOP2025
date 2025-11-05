
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace Exercise01_WinForm {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            toolStripStatusLabel1.Text = "";
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.WordWrap = true;
        }

        private void OpenFileButton_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            dialog.Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
            if (dialog.ShowDialog() == DialogResult.OK) {
                textBox2.Text = dialog.FileName;
            }
        }

        private async void ReadButton_Click(object sender, EventArgs e) {
            string filePath = textBox2.Text;
            var sb = new StringBuilder();

            toolStripStatusLabel1.Text = "読み込み中...";

            try {
                //usingを使って自動的にファイルを閉じる
                using (var sr = new StreamReader(filePath)) {
                    while (!sr.EndOfStream) {
                        string? line = await sr.ReadLineAsync();//一行読み取り
                        sb.AppendLine(line);
                        await Task.Delay(10);
                    }
                    textBox1.Text = sb.ToString();
                    toolStripStatusLabel1.Text = "";
                }
            }catch (Exception ex) {
                toolStripStatusLabel1.Text = "ファイルの読み込みに失敗しました。";
            }
        }
    }
}
