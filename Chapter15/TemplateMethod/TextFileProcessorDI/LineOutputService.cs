using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFileProcessorDI {
    internal class LineOutputService : ITextFileService {
        //P362　問題15.3
        private int _count;
        private List<string> saveLines = new List<string>();

        public void Initialize(string fname) {
            _count = 0;
        }

        public void Execute(string line) {
            if (_count < 20) {
                saveLines.Add(line);
            }
            _count++;
        }

        public void Terminate() {
            foreach (var line in saveLines) {
                Console.WriteLine(line);
            }
        }
    }
}
