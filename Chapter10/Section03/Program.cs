namespace Section03 {
    internal class Program {
        static void Main(string[] args) {
            #region　コード10.15　Fileクラスでファイルの存在確認
            //if (File.Exists("./Example/Greeting.txt")) {
            //    Console.WriteLine("すでに存在しています。");
            //}
            #endregion

            #region　コード10.16　FileInfoクラスでファイルの存在確認
            //var fi = new FileInfo("./Example/Greeting.txt");
            //if (fi.Exists) {
            //    Console.WriteLine("すでに存在しています。");
            //}
            #endregion

            #region　コード10.17　Fileクラスでファイルの削除
            //File.Delete("./Example/Greeting.txt");
            #endregion

            #region　コード10.18　FileInfoクラスでファイルの削除
            //var fi = new FileInfo("./Example/Greeting.txt");
            //fi.Delete();
            #endregion

            #region　コード10.19　Fileクラスでファイルのコピー
            //File.Copy("./Example/src/Greeting.txt", "./Example/dest/Greeting.txt");
            #endregion

            #region　コード10.20　FileInfoクラスでファイルのコピー
            //var fi = new FileInfo("./Example/src/Greeting.txt");
            //FileInfo dup = fi.CopyTo("./Example/dest/Greeting.txt", overwrite: true);
            #endregion

            #region　コード10.21　Fileクラスでファイルの移動
            //File.Move("./Example/src/Greeting.txt", "./Example/dest/Greeting.txt");
            #endregion

            #region　コード10.22　FileInfoクラスでファイルの移動
            //var fi = new FileInfo("./Example/src/Greeting.txt");
            //fi.MoveTo("./Example/dest/Greeting.txt");
            #endregion

            #region　コード10.23　Fileクラスでファイルのリネーム
            //File.Move("./Example/oldfile.txt", "./Example/newfile.txt");
            #endregion

            #region　コード10.24　FileInfoクラスでファイルのリネーム
            //var fi = new FileInfo("./Example/oldfile.txt");
            //fi.MoveTo("./Example/newfile.txt");
            #endregion
        }
    }
}
