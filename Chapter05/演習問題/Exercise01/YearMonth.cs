﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    //5.1.1
    public class YearMonth {
        public int Year { get; init; }
        public int Month { get; init; }

        public YearMonth(int year, int month) {
            Year = Year;
            Month = Month;
        }

        //5.1.2
        //設定されている西暦が２１世紀か判定する
        //Yearが2001～2100年の間ならtrue、それ以外ならfalseを返す
        public bool Is21Century => 2001 <= Year && Year <= 2100;

        //5.1.3
        public YearMonth AddOneMonth() {
            if (Month != 12) {
                return new YearMonth(Year, Month + 1);  //Monthが12以外
            } else {
                return new YearMonth(Year + 1, 1);  //Monthが12月
            }


        }
    }
}
