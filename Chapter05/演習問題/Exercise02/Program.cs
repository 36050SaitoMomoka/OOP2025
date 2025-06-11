using Exercise01;
using System.Reflection.Metadata.Ecma335;

namespace Exercise02 {
    public class Program {
        static void Main(string[] args) {
            // 5.2.1
            var ymCollection = new YearMonth[] {
                new YearMonth(1980, 1),
                new YearMonth(1990, 4),
                new YearMonth(2000, 7),
                new YearMonth(2010, 9),
                new YearMonth(2024, 12),
            };

            Console.WriteLine("5.2.2");
            Exercise2(ymCollection);

            Console.WriteLine("5.2.4");
            Exercise4(ymCollection);

            Console.WriteLine("5.2.5");
            Exercise5(ymCollection);
        }

        //5.2.2
        private static void Exercise2(YearMonth[] ymCollection) {
            foreach (var item in ymCollection) {
                Console.WriteLine(item);
            }
        }

        //5.2.3
        //ここにメソッドを作成【メソッド名：FindFirst21C】
        private static YearMonth? FindFirst21C(YearMonth[] ymCollection) {
            foreach (var item in ymCollection) {
                if (item.Is21Century) {
                    return item;
                }
            }
            return null;
        }

        //解答
        //foreach(var ym in ymCollection){
        //    if(ym.Is21Century)
        //       return ym;
        //}
        //return null;

        //5.2.4
        private static void Exercise4(YearMonth[] ymCollection) {
            if (FindFirst21C(ymCollection) is null) {
                Console.WriteLine("21世紀のデータはありません");
            } else {
                Console.WriteLine(FindFirst21C(ymCollection));
            }

            //null合体演算子、null条件演算子
            var message = FindFirst21C(ymCollection)?.ToString() ?? "21世紀のデータではありません";
            Console.WriteLine(message);
            
            //Console.WriteLine(FindFirst21C(ymCollection)?.ToString() ?? "21世紀のデータではありません");
        }

        //解答
        //var ym = FindFirst21C(ymCollection);
        //    if(ym is null) {
        //        Console.WriteLine("21世紀のデータはありません");
        //    } else {
        //        Console.WriteLine(ym);
        //    }

        //5.2.5
        private static void Exercise5(YearMonth[] ymCollection) {
            var array = ymCollection.Select(x => x.AddOneMonth()).ToArray();
            foreach (var item in array) {
                Console.WriteLine(item);
            }
        }
    }
}
