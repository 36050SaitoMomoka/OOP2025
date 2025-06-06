﻿using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace DistanceConverter {
    internal class Program {
        //コマンドライン引数で指定された範囲のフィートとメートルの対応表を出力する
        static void Main(string[] args) {
            int start = int.Parse(args[1]);
            int end = int.Parse(args[2]);

            if (args.Length >= 1 && args[0] == "-tom") { //args.Length >= 1  引数がある
                if (args.Length >= 1 && args[0] == "-tom") {
                    PrintFeetToMeterList(start, end);
                } else {
                    PrintMeterToFeetList(start, end);
                }

                //フィートからメートルへの対応表を出力
                static void PrintFeetToMeterList(int start, int end) {
                    //FeetConverter converter = new FeetConverter();
                    for (int feet = start; feet <= end; feet++) {
                        //double meter = converter.ToMeter(feet);
                        double meter = FeetConverter.ToMeter(feet);
                        Console.WriteLine($"{feet}ft = {meter:0.0000}m");
                    }
                }


                //メートルからフィートへの対応表を出力
                static void PrintMeterToFeetList(int start, int end) {
                    //FeetConverter converter = new FeetConverter();
                    for (int meter = start; meter <= end; meter++) {
                        //double feet = meter * 0.3048;
                        //double feet = converter.FromMeter(meter);
                        double feet = FeetConverter.FromMeter(meter);
                        Console.WriteLine($"{meter}m = {feet:0.0000}ft");
                    }
                }
            }
        }
    }
}