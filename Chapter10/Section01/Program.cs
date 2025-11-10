using System.IO;
using System.Text;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            #region　コード10.1　1行ずつ読み込み
            //var filePath = "./吾輩は猫である.txt";
            //if (File.Exists(filePath)) {
            //    using var reader = new StreamReader(filePath, Encoding.UTF8);
            //    while (!reader.EndOfStream) {
            //        var line = reader.ReadLine();
            //        Console.WriteLine(line);
            //    }
            //}
            #endregion

            #region　コード10.2　一気に読み込み
            //var filePath = "./吾輩は猫である.txt";
            //var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}
            #endregion

            #region　コード10.3　IEnumerable
            var filePath = "./吾輩は猫である.txt";
            //var lines = File.ReadLines(filePath);
            #region　コード10.4　先頭のn行を取り出す
            //var lines = File.ReadLines(filePath)
            //    .Take(10)
            //    .ToArray();
            #endregion
            #region　コード10.5　条件に一致した行数
            //var count = File.ReadLines(filePath)
            //    .Count(s => s.Contains("猫"));
            #endregion
            #region　条件に一致した行だけ
            var lines = File.ReadLines(filePath)
                .Where(s => !String.IsNullOrWhiteSpace(s))
                .ToArray();
            #endregion
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}
            #endregion
        }
    }
}
