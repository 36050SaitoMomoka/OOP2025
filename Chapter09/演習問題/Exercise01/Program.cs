using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var dateTime = DateTime.Now;
            DisplayDatePattern1(dateTime);
            DisplayDatePattern2(dateTime);
            DisplayDatePattern3(dateTime);
        }

        private static void DisplayDatePattern1(DateTime dateTime) {
            //2024/03/09 19:03
            //string.Formatを使った例
            Console.WriteLine(string.Format("{0:yyyy/MM/dd HH:mm}", dateTime));

            //解答
            var str = string.Format($"{dateTime:yyyy/MM/dd HH:mm}");
            Console.WriteLine(str);
        }

        private static void DisplayDatePattern2(DateTime dateTime) {
            //2024年03月09日 19時03分09秒
            //DateTime.ToStringを使った例
            Console.WriteLine(dateTime.ToString("yyyy年MM月dd日 HH時mm分ss秒"));

            //解答
            var str = dateTime.ToString($"{dateTime:yyyy年MM月dd日 HH時mm分ss秒}");
            Console.WriteLine(str);
        }

        private static void DisplayDatePattern3(DateTime dateTime) {
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            var dayOfWeek = culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);
            Console.WriteLine(string.Format(culture, "{0:gg y年 MM月 dd日}（{1}）", dateTime, dayOfWeek));

            //解答
            var datestr = dateTime.ToString("ggyy", culture);
            var dayofWeek = culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

            var str = string.Format($"{datestr}年{dateTime.Month,2}月{dateTime.Day,2}日({dayofWeek})");
            Console.WriteLine(str);

            //和暦2桁表示（ゼロサプレスあり）
            var cul = dateTime.ToString("gg", culture);
            var year = int.Parse(dateTime.ToString("yy", culture));
        }
    }
}
