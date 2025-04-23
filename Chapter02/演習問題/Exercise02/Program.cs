
using Exercise02;
using Microsoft.VisualBasic;
using System.Security.Cryptography;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            //Console.WriteLine("１：ヤードからメートル");
            //Console.WriteLine("２：メートルからヤード");
            //Console.Write(">");
            //string conversion = Console.ReadLine();      //１or２入力

            Console.Write("変換する数値:");
            int number = int.Parse(Console.ReadLine());

            //if (conversion == "1") {
            //    PrintYardToMeterList(number);
            //} else if (conversion == "2") {
            //    PrintMeterToYardList(number);
            //}

            static void PrintYardToMeterList(int number) {
                    double meter = YardConverter.ToMeter(number);
                    Console.WriteLine($"{number}yard = {meter:0.0000}m");
            }

            static void PrintMeterToYardList(int number) {
                    double inch = YardConverter.FromMeter(number);
                    Console.WriteLine($"{number}m = {inch:0.0000}inch");
            }
        }
    }
}
