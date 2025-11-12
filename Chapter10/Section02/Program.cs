using System.Diagnostics.Tracing;

namespace Section02 {
    internal class Program {
        static void Main(string[] args) {
            #region　コード10.10　１行ずつ文字列出力
            var filePath = "./Example/いろは歌.txt";
            using (var writer = new StreamWriter(filePath)) {
                writer.WriteLine("色はにほへど　散りぬるを");
                writer.WriteLine("我が世たれぞ　常ならむ");
                writer.WriteLine("有為の奥山　今日越えて");
                writer.WriteLine("浅き夢見じ　酔ひもせず");
                #endregion

                #region　コード10.11　末尾に行を追加
                //var lines = new[] { "====", "京の夢", "大阪の夢", };
                //var filePath = "./Example/いろは歌.txt";
                //using (var writer = new StreamWriter(filePath, append: true)) {
                //    foreach (var line in lines) {
                //        writer.WriteLine(line);
                //    }
                //}
                #endregion

                #region　コード10.12　文字列配列を一気にファイルに出力
                var lines = new[] { "Tokyo", "New Delhi", "Bangkok", "London", "Paris" };
                //var filePath = "./Example/Cities.txt";
                File.WriteAllLines(filePath, lines);
                #endregion

                #region　コード10.13　結果をファイルに出力
                var names = new List<string> {
                "Tokyo","New Delhi","Bangkok","London","Paris","Berlin","Canberra","Hong Kong",
            };
                //var filePath = "./Example/Cities.txt";
                File.WriteAllLines(filePath, names.Where(s => s.Length > 5));
                #endregion

                #region　コード10.14　ファイルの先頭に行を挿入
                //var filePath = "./Example/いろは歌.txt";

                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                using var reader = new StreamReader(stream);

                string texts = reader.ReadToEnd();
                stream.Position = 0;
                writer.WriteLine("挿入する新しい行1");
                writer.WriteLine("挿入する新しい行2");
                writer.Write(texts);
                #endregion
            }
        }
    }
}
