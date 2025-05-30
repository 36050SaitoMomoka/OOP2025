
namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";
            #region
            Console.WriteLine("6.3.1");
            Exercise1(text);

            Console.WriteLine("6.3.2");
            Exercise2(text);

            Console.WriteLine("6.3.3");
            Exercise3(text);

            Console.WriteLine("6.3.4");
            Exercise4(text);

            Console.WriteLine("6.3.5");
            Exercise5(text);
            #endregion
        }

        private static void Exercise1(string text) {
            Console.WriteLine("空白数：" + text.Count(c => c == ' '));
        }

        private static void Exercise2(string text) {
            Console.WriteLine(text.Replace("big", "small"));
        }

        private static void Exercise3(string text) {


        }

        private static void Exercise4(string text) {
            var words = text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("単語数：" + words.Count());
        }

        private static void Exercise5(string text) {


        }
    }
}
