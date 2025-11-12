using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01_Console {
    class Program {
        static async Task Main(string[] args) {
            Console.WriteLine("ファイルパスを入力してください:");
            string filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath)) {
                Console.WriteLine("ファイルが見つかりません。");
                return;
            }

            var sb = new StringBuilder();
            Console.WriteLine("読み込み中...");

            try {
                using (var sr = new StreamReader(filePath)) {
                    while (!sr.EndOfStream) {
                        string? line = await sr.ReadLineAsync();
                        sb.AppendLine(line);
                        await Task.Delay(10);
                    }
                }
                Console.WriteLine("読み込み完了:\n");
                Console.WriteLine(sb.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine("ファイルの読み込みに失敗しました。");
            }
        }
    }
}