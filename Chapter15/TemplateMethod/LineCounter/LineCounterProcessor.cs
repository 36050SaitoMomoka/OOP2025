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
        private string _text = "";

        protected override void Initialize(string fname) {
            Console.Write("カウントしたい単語を入力：");
            _text = Console.ReadLine() ?? "";
            _count = 0;
        }

        protected override void Execute(string line) {
            var matches = Regex.Matches(line, $@"\b{Regex.Escape(_text)}\b");
            foreach (Match match in matches) {
                _count++;
            }
        }

        protected override void Terminate() => Console.WriteLine($"{_text}の個数：{_count}個");
    }
}