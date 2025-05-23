
using System.Xml.Linq;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
               "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
                "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise1(List<string> langs) {
            //foreach文  ※Exercise2 LINQを使う
            langs.Where(s => s.Contains("S")).ToList().ForEach(Console.WriteLine);
            //for文
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains("S")) {
                    Console.WriteLine(langs[i]);
                }
            }
            //while文
            int k = 0;
            while (k < langs.Count) {
                if (langs[k].Contains("S")) {
                    Console.WriteLine(langs[k]);
                }
                k++;
            }
        }

        private static void Exercise2(List<string> langs) {
            //foreach文  ※Exercise1 LINQを使わない
            foreach (var lang in langs) {
                if (lang.Contains("S")) {
                    Console.WriteLine(lang);
                }
            }
            //for文
            langs = langs.Where(s => s.Contains("S")).ToList();
            for (int i = 0; i < langs.Count; i++) {
                Console.WriteLine(langs[i]);
            }
            //while文
            langs = langs.Where(s => s.Contains("S")).ToList();
            int j = 0;
            while (j < langs.Count) {
                Console.WriteLine(langs[j]);
                j++;
            }
        }

        private static void Exercise3(List<string> langs) {
            //２行で完結させる
            var lang = langs.Find(s => s.Length == 10) ?? "unknown";
            Console.WriteLine(lang);
        }
    }
}
