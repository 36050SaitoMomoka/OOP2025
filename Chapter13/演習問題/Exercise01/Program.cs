
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
                .Where(b => b.PublishedYear == 2022)
                .Join(Library.Categories,
                    b => b.CategoryId,
                    c => c.Id,
                    (b, c) => c.Name)
                .Distinct();

            #region 自分のコード
            //.Join(Library.Categories,
            //    book => book.CategoryId,
            //    category => category.Id,
            //    (book, category) => new {
            //        book.Title,
            //        Category = category.Name,
            //        book.PublishedYear,
            //    }
            //)
            //.Where(x => x.PublishedYear == 2022)
            //.Select(x => x.Category)
            //.Distinct();
            #endregion

            foreach (var item in category) {
                Console.WriteLine(item);
            }
        }

        private static void Exercise1_6() {
            var groups = Library.Books
                .Join(Library.Categories,
                    b => b.CategoryId,
                    c => c.Id,
                    (b, c) => new {
                        CategoryName = c.Name,
                        b.Title
                    }
                )
                .GroupBy(x => x.CategoryName)
                .OrderBy(x => x.Key);

            foreach (var group in groups) {
                Console.WriteLine($"# {group.Key}");
                foreach (var book in group) {
                    Console.WriteLine($"　　{book.Title}");
                }
            }

            #region 自分のコード
            //var category = Library.Books
            //    .Join(Library.Categories,
            //        book => book.CategoryId,
            //        category => category.Id,
            //        (book, category) => new {
            //            book.Title,
            //            Category = category.Name,
            //        }
            //    )
            //    .GroupBy(b => b.Category)
            //    .OrderBy(x => x.Key);

            //foreach (var group in category) {
            //    Console.WriteLine($"# {group.Key}");
            //    foreach (var book in group) {
            //        Console.WriteLine($"　　{book.Title}");
            //    }
            //}
            #endregion
        }

        private static void Exercise1_7() {
            var groups = Library.Categories
                .Where(c => c.Name.Equals("Development"))
                .Join(Library.Books,
                    c => c.Id,
                    b => b.CategoryId,
                    (c, b) => new {
                        b.Title,
                        b.PublishedYear
                    })
                .GroupBy(x => x.PublishedYear)
                .OrderBy(x => x.Key);

            foreach (var group in groups) {
                Console.WriteLine($"# {group.Key}");
                foreach (var book in group) {
                    Console.WriteLine($"　　{book.Title}");
                }
            }

            #region 自分のコード

            //var category = Library.Books
            //    .Join(Library.Categories,
            //        book => book.CategoryId,
            //        category => category.Id,
            //        (book, category) => new {
            //            book.Title,
            //            Category = category.Name,
            //            book.PublishedYear,
            //        }
            //     )
            //    .Where(b => b.Category == "Development")
            //    .GroupBy(b => b.PublishedYear)
            //    .OrderBy(x => x.Key);


            //foreach (var group in category) {
            //    Console.WriteLine($"# {group.Key}");
            //    foreach (var book in group) {
            //        Console.WriteLine($"　　{book.Title}");
            //    }
            //}
            #endregion
        }

        private static void Exercise1_8() {
            var categoryNames = Library.Categories
                .GroupJoin(Library.Books,
                       c => c.Id,
                       b => b.CategoryId,
                       (c, books) => new {
                           CategoryName = c.Name,
                           Count = books.Count(),
                       })
                .Where(x => x.Count >= 4)
                .Select(x => x.CategoryName);

            foreach (var name in categoryNames) {
                Console.WriteLine(name);
            }

            #region 自分のコード
            //var groups = Library.Categories
            //   .GroupJoin(Library.Books
            //        , c => c.Id
            //        , b => b.CategoryId,
            //        (c, books) => new {
            //            Category = c.Name,
            //            Count = books.Count(),
            //        })
            //   .Where(b => b.Count >= 4)
            //   .Distinct();

            //foreach (var group in groups) {
            //    Console.WriteLine(group.Category);
            //}
            #endregion
        }
    }
}
