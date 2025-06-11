
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);

        }

        private static void Exercise1(string text) {
            var Dict = new Dictionary<Char, int>();
            foreach (var c in text.ToUpper()) {
                if ('A' <= c && c <= 'Z') {
                    if (Dict.ContainsKey(c)) {
                        Dict[c]++;
                    }else {
                        Dict[c] = 1;
                    }
                }
            }
            var newDict = Dict.OrderBy(d => d.Key).ToList();
            foreach (var item in newDict) {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }
        }

        private static void Exercise2(string text) {
            var sortDict = new SortedDictionary<Char, int>();
            foreach (var c in text.ToUpper()) {
                if ('A' <= c && c <= 'Z') {
                    if (sortDict.ContainsKey(c)) {
                        sortDict[c]++;
                    } else {
                        sortDict[c] = 1;
                    }
                }
            }
            foreach (var item in sortDict) {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }
        }
    }
}
