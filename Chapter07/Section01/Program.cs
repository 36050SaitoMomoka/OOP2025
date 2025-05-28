using System;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            var books = Books.GetBooks();

            //①本の平均金額を表示
            Console.WriteLine((int)books.Average(b => b.Price));


            //②本のページ合計を表示
            Console.WriteLine((int)books.Sum(b => b.Pages));


            //③金額の安い書籍名と金額を表示
            var cheapBook = books.OrderBy(b => b.Price).ToList().First();
            Console.WriteLine($"{cheapBook.Title}:{cheapBook.Price}");

            //１冊以上表示できるように修正
            var cheapBooks = books.OrderBy(b => b.Price).Take(3);
            foreach (var b in cheapBooks) {
                Console.WriteLine($"{b.Title}:{b.Price}");
            }

            //解答    小さいやつを出してそれと比較
            var book = books.Where(x => x.Price == books.Min(b => b.Price));
            foreach (var item in book) {
                Console.WriteLine(item.Title + ":" + item.Price);
            }

            //④ページが多い書籍名とページ数を表示
            var mostPages = books.OrderByDescending(b => b.Pages).ToList().First();
            Console.WriteLine($"{mostPages.Title}:{mostPages.Pages}");

            //解答
            books.Where(x => x.Pages == books
                        .Max(b => b.Pages)).ToList()
                        .ForEach(x => Console.WriteLine($"{x.Title}:{x.Pages}ページ"));


            //⑤タイトルに「物語」が含まれている書籍名をすべて表示
            var title = books.Where(b => b.Title.Contains("物語")).ToList();
            foreach (var name in title) {
                Console.WriteLine(name.Title);
            }

            //解答
            var titles = books.Where(x => x.Title.Contains("物語"));
            foreach (var item in titles) {
                Console.WriteLine(item.Title);
            }
        }
    }
}
