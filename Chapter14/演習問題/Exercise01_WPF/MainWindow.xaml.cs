using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exercise01_WPF {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            StatusText.Text = "";
        }

        private void ButtonOpenFile_Click(object sender, RoutedEventArgs e) {
            var dialog = new OpenFileDialog {
                Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true) {
                TextBoxFilePath.Text = dialog.FileName;
            }
        }

        private async void ButtonReadFile_Click(object sender, RoutedEventArgs e) {
            string filePath = TextBoxFilePath.Text;
            var sb = new StringBuilder();
            StatusText.Text = "読み込み中...";

            try {
                using (var sr = new StreamReader(filePath)) {
                    while (!sr.EndOfStream) {
                        string? line = await sr.ReadLineAsync();
                        sb.AppendLine(line);
                        await Task.Delay(10);
                    }
                }
                TextBoxContent.Text = sb.ToString();
                StatusText.Text = "";
            }
            catch (Exception ex) {
                StatusText.Text = "ファイルの読み込みに失敗しました。";
            }
        }
    }
}