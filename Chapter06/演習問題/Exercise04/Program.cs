namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";
            string[] words = line.Split(';');

            foreach (var word in words) {
                string[] name = word.Split('=');
                Console.WriteLine("{0}:{1}", ToJapanese(name[0]), name[1]);
                //Console.WriteLine($"{ToJapanese(name[0])}:{name[1]}");
            }
        }

        /// <summary>
        /// 引数の単語を日本語へ変換します
        /// </summary>
        /// <param name="key">"Novelist","BestWork","Born"</param>
        /// <returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {
            switch (key) {
                case "Novelist":
                    Console.Write("作家");
                    break;
                case "BestWork":
                    Console.Write("代表作");
                    break;
                case "Born":
                    Console.Write("誕生年");
                    break;
            }
            return ""; //エラーをなくすためのダミー

            //解答
            //switch (key) {
            //    case "Novelist":
            //        return "作家";
            //    case "BestWork":
            //        return "代表作";
            //    case "Born":
            //        return "誕生年";
            //    default:
            //        return "引数keyは、正しい値ではありません";
            //}

            //新しい書き方
            //var retText = key switch {
            //    "Novelist" => "作家",
            //    "BestWork" => "代表作",
            //    "Born" => "誕生年",
            //    => "引数keyは、正しい値ではありません"
            //};

        }
    }
}