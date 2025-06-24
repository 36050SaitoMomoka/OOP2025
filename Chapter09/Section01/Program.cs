using System.Dynamic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            //var today = new DateTime(2025,7,12);    //日付
            //var now = DateTime.Now;     //日付と時刻

            //Console.WriteLine($"Today:{today.Month}");
            //Console.WriteLine($"Now:{now}");

            //①自分の生年月日は何曜日かをプログラムを書いて調べる
            //var day = new DateTime(2005, 6, 14);
            //Console.WriteLine($"{day.DayOfWeek}");

            //日付を入力
            Console.WriteLine("生年月日を入力してください。");

            //西暦：
            Console.Write("西暦：");
            var seireki = int.Parse(Console.ReadLine()!);

            //月：
            Console.Write("月：");
            var month = int.Parse(Console.ReadLine()!);

            //日：
            Console.Write("日：");
            var day = int.Parse(Console.ReadLine()!);

            //時間：
            Console.Write("時：");
            var hour = int.Parse(Console.ReadLine()!);

            //分：
            Console.Write("分：");
            var minute = int.Parse(Console.ReadLine()!);

            var datetime = new DateTime(seireki, month, day);

            //平成○○年〇月〇日は〇曜日です　←曜日は漢字で表示（P202）
            var cultureJp = new CultureInfo("ja-jp", false);
            cultureJp.DateTimeFormat.Calendar = new JapaneseCalendar();

            var week = datetime.ToString("dddd", cultureJp);

            var wareki = datetime.ToString("ggy年M月d日", cultureJp);

            Console.WriteLine($"{wareki}は{week}です。");

            Console.WriteLine();

            //③生まれてから○○○○日目です。
            var dt1 = new DateTime(seireki, month, day,hour,minute,0);
            var dt2 = DateTime.Today;
            //Console.WriteLine($"生まれてから{diff.Days}日目");

            TimeSpan diff;
            while (true) {
                diff = DateTime.Now - dt1.Date;
                Console.Write($"\r{diff.TotalSeconds}秒");   //生まれてからの経過秒数
            }

            Console.WriteLine();

            //④あなたは○○歳です！
            var age = dt2.Year - dt1.Year;
            Console.WriteLine($"あなたは{age}歳です！");

            Console.WriteLine();

            //⑤1月1日から何日目か？
            var dt3 = new DateTime(seireki, 1, 1);
            var days = (dt1.Date - dt3.Date).Days;
            Console.WriteLine($"1月1日から{days}日目です。");

            Console.WriteLine();

            //②うるう年の判定プログラムを作成する
            //西暦を入力
            //→○○○○はうるう年です
            //→○○○○は平年です
            //Console.Write("西暦を入力：");
            //var LeapYear = int.Parse(Console.ReadLine()!);

            var LeapYear = seireki;
            var isLeapYear = DateTime.IsLeapYear(LeapYear);
            if (isLeapYear) {
                Console.WriteLine("うるう年です。");
            } else {
                Console.WriteLine("うるう年ではありません。");
            }

        }
    }
}
