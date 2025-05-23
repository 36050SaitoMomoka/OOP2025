
using System;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1();
            Console.WriteLine("---");
            Exercise2();
            Console.WriteLine("---");
            Exercise3();
            Console.WriteLine("---");
        }

        private static void Exercise1() {
            Console.Write("数値を入力してください。：");
            if (int.TryParse(Console.ReadLine(), out var num)) {
                if (num < 0) {
                    Console.WriteLine(num);
                } else if (num < 100) {
                    Console.WriteLine(num * 2);
                } else if (num < 500) {
                    Console.WriteLine(num * 3);
                } else if (num <= 500) {
                    Console.WriteLine(num);
                }
            } else {
                Console.WriteLine("入力値に誤りがあります。");
            }
        }


        private static void Exercise2() {

        }

        private static void Exercise3() {

        }
    }
}
