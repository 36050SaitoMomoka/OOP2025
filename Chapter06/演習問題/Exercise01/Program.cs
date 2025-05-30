using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {

            Console.Write("一文字目：");
            var str1 = Console.ReadLine();
            Console.Write("二文字目：");
            var str2 = Console.ReadLine();

            var cultureinfo = new CultureInfo("ja-JP");
            if (String.Compare(str1, str2, ignoreCase: true) == 0)
                Console.WriteLine("等しい。");
            else
                Console.WriteLine("等しくない。");
        }
    }
}
