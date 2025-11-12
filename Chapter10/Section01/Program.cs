using System.IO;
using System.Text;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            #region　コード10.1　1行ずつ読み込み
            //var filePath = "./Greeting.txt";
            //if (File.Exists(filePath)) {
            //    using var reader = new StreamReader(filePath, Encoding.UTF8);
            //    while (!reader.EndOfStream) {
            //        var line = reader.ReadLine();
            //        Console.WriteLine(line);
            //    }
            //}
            #endregion

            #region　コード10.2　一気に読み込み
            //var filePath = "./Greeting.txt";
            //var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}
            #endregion

            #region　コード10.3　IEnumerable
            //var filePath = "./Greeting.txt";
            //var lines = File.ReadLines(filePath);
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}
            #endregion

            #region　コード10.4　先頭のn行を取り出す
            //var filePath = "./Greeting.txt";
            //var lines = File.ReadLines(filePath)
            //    .Take(10)
            //    .ToArray();
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}
            #endregion

            #region　コード10.5　条件に一致した行数
            //var filePath = "./Greeting.txt";
            //var count = File.ReadLines(filePath)
            //    .Count(s => s.Contains("猫"));
            //Console.WriteLine(count);
            #endregion

            #region　コード10.6　条件に一致した行だけ
            //var filePath = "./Greeting.txt";
            //var lines = File.ReadLines(filePath)
            //    .Where(s => !String.IsNullOrWhiteSpace(s))
            //    .ToArray();
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}
            #endregion

            #region　コード10.7　行が存在しているか
            //var filePath = "./Greeting.txt";
            //var exists = File.ReadLines(filePath)
            //    .Where(s => !String.IsNullOrWhiteSpace(s))
            //    .Any(s => s.All(c => Char.IsAsciiDigit(c)));
            //    Console.WriteLine(exists);
            #endregion

            #region　コード10.8　重複排除並べ替え
            //var filePath = "./Greeting.txt";
            //var lines = File.ReadLines(filePath)
            //    .Distinct()
            //    .OrderBy(s => s.Length)
            //    .ToArray();
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}
            #endregion

            #region　コード10.9　行ごとに変換処理
            var filePath = "./Greeting.txt";
            var lines = File.ReadAllLines(filePath)
                .Select((s, ix) => $"{ix + 1,4}:{s}");
            foreach (var line in lines) {
                Console.WriteLine(line);
            }
            #endregion
        }
    }
}