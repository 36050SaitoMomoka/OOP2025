using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    public static class YardConverter {

        //定数
        private const double ratio = 0.9144;

        //メートルからフィートを求める
        public static double FromMeter(double number) {
            return number / ratio;
        }
        //フィートからメートルを求める
        public static double ToMeter(double number) {
            return number * ratio;
        }
    }
}
