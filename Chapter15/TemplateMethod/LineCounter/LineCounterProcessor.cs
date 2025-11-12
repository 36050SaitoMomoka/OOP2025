using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextFileProcessor;
using static System.Net.Mime.MediaTypeNames;

namespace LineCounter {
    internal class LineCounterProcessor : TextProcessor {
        private int _count = 0;

        protected override void Initialize(string fname) => _count = 0;

        protected override void Execute(string line) {
            var matches = Regex.Matches(line, @"\bprivate\b");
            foreach (Match match in matches) {
                _count++;
            }
        }

        protected override void Terminate() => Console.WriteLine("privateの個数：{0}個", _count);
    }
}
