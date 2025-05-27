
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {

            int[] numbers = [5, 10, 17, 9, 3, 21, 10, 40, 21, 3, 35];

            Console.WriteLine("7.1.1");
            Exercise1(numbers);

            Console.WriteLine("7.1.2");
            Exercise2(numbers);

            Console.WriteLine("7.1.3");
            Exercise3(numbers);

            Console.WriteLine("7.1.4");
            Exercise4(numbers);

            Console.WriteLine("7.1.5");
            Exercise5(numbers);



        }

        private static void Exercise1(int[] numbers) {
            Console.WriteLine(numbers.Max());
        }

        private static void Exercise2(int[] numbers) {  //P169～
            var index = numbers.Skip(numbers.Length - 2).Take(2);
            foreach (var num in index) {
                Console.WriteLine(num);

                ////解答
                //foreach (var n in numbers.TakeLast(2)) {
                //    Console.WriteLine(n);
                }
            }

        private static void Exercise3(int[] numbers) {  //P172～
            var strings = numbers.Select(n => n.ToString("000")).ToArray();
            foreach (var str in strings) {
                Console.WriteLine(str);
            }
        }

        private static void Exercise4(int[] numbers) {
            var sortedList = numbers.OrderBy(n => n).Take(3);
            foreach (var num in sortedList) {
                Console.WriteLine(num);
            }
        }

        private static void Exercise5(int[] numbers) {
            var result = numbers.Distinct().Count(n => n > 10);
            Console.WriteLine(result);
        }
    }
}
