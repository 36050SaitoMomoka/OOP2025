namespace LinqSample {
    internal class Program {
        static void Main(string[] args) {

            var numbers = Enumerable.Range(1, 100);

            //合計値を出力
            Console.WriteLine(numbers.Sum());

            //偶数の合計を出力
            Console.WriteLine(numbers.Where(n => n % 2 == 0).Sum());

            //8の倍数の合計を出力
            Console.WriteLine(numbers.Where(n => n % 8 == 0).Sum());

            //平均を出力
            Console.WriteLine(numbers.Average());

            //最大値を出力
            Console.WriteLine(numbers.Max());

            //最小値を出力
            Console.WriteLine(numbers.Min());
            
            //foreach (var num in numbers) {
            //    Console.WriteLine(num);
            //}
        }
    }
}
