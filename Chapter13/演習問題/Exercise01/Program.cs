
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("(2)");
            Exercise1_2();
            Console.WriteLine();
            Console.WriteLine("(3)");
            Exercise1_3();
            Console.WriteLine();
            Console.WriteLine("(4)");
            Exercise1_4();
            Console.WriteLine();
            Console.WriteLine("(5)");
            Exercise1_5();
            Console.WriteLine();
            Console.WriteLine("(6)");
            Exercise1_6();
            Console.WriteLine();
            Console.WriteLine("(7)");
            Exercise1_7();
            Console.WriteLine();
            Console.WriteLine("(8)");
            Exercise1_8();

            Console.ReadLine();
        }

        private static void Exercise1_2() {
            var book = Library.Books.MaxBy(b => b.Price);
            Console.WriteLine(book);
        }

        private static void Exercise1_3() {
            var groups = Library.Books
                .GroupBy(b => b.PublishedYear)
                .Select(x => new {
                    PublisheYear = x.Key,
                    Count = x.Count(),
                })
                .OrderBy(b => b.PublisheYear);

            foreach (var item in groups) {
                Console.WriteLine($"{item.PublisheYear}：{item.Count}");
            }
        }

        private static void Exercise1_4() {
            var orderby = Library.Books
                .OrderByDescending(b => b.PublishedYear)
                .ThenByDescending(b => b.Price);

            foreach (var item in orderby) {
                Console.WriteLine($"{item.PublishedYear}年 {item.Price}円 {item.Title}");
            }
        }

        private static void Exercise1_5() {
            var category = Library.Books
                .Join(Library.Categories,
                    book => book.CategoryId,
                    category => category.Id,
                    (book, category) => new {
                        book.Title,
                        Category = category.Name,
                        book.PublishedYear,
                    }
                )
                .Where(x => x.PublishedYear == 2022)
                .Select(x => x.Category)
                .Distinct();

            foreach (var item in category) {
                Console.WriteLine(item);
            }
        }

        private static void Exercise1_6() {

        }

        private static void Exercise1_7() {

        }

        private static void Exercise1_8() {

        }
    }
}
