using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            try {
                Console.Write("ファイルパスを入力：");
                string path = (Console.ReadLine() ?? "").Trim('"');
                TextProcessor.Run<LineCounterProcessor>(path);
            }
            catch {
                Console.WriteLine("ファイルが存在しません。");
            }
        }
    }
}