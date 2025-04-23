
using DistanceConverter;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("１：インチからメートル");
            Console.WriteLine("２：メートルからインチ");
            Console.Write(">");
            string conversion = Console.ReadLine();      //１or２入力

            Console.Write("はじめ:");  //コンソール入力
            int start = int.Parse(Console.ReadLine());   //文字列で取り込んで整数へ変換
            Console.Write("おわり:");  //コンソール入力
            int end = int.Parse(Console.ReadLine());   //文字列で取り込んで整数へ変換

            if (conversion == "1") {
                PrintInchToMeterList(start, end);
            } else if (conversion == "2") {
                PrintMeterToInchList(start, end);
            }

            static void PrintInchToMeterList(int start, int end) {
                for (int inch = start; inch <= end; inch++) {
                    double meter = InchConverter.ToMeter(inch);
                    Console.WriteLine($"{inch}inch = {meter:0.0000}m");
                }
            }

            static void PrintMeterToInchList(int start, int end) {
                for (int meter = start; meter <= end; meter++) {
                    double inch = InchConverter.ToMeter(meter);
                    Console.WriteLine($"{meter}m = {inch:0.0000}inch");
                }
            }
        }
    }
}
