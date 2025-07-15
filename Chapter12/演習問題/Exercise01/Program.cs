using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using System.Text;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var emp = new Employee {
                Id = 123,
                Name = "山田太郎",
                HireDate = new DateTime(2018, 10, 1),
            };
            var jsonString = Serialize(emp);
            Console.WriteLine(jsonString);
            var obj = Deserialize(jsonString);
            Console.WriteLine(obj);

            //問題12.1.2
            Employee[] employees = [
                new () {
                    Id = 123,
                    Name = "山田太郎",
                    HireDate = new DateTime(2018, 10, 1),
                },
                new () {
                    Id = 198,
                    Name = "田中華子",
                    HireDate = new DateTime(2020, 4, 1),
                },
            ];
            Serialize("employees.json", employees);

            //問題12.1.3
            var empdata = Deserialize_f("employees.json");
            foreach (var empd in empdata)
                Console.WriteLine(empd);
        }

        //問題12.1.1
        static string Serialize(Employee emp) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string jsonString = JsonSerializer.Serialize(emp, options);
            return jsonString;
        }

        //問題12.1.1
        static Employee? Deserialize(string jsonString) {   //(string text)のままでOK
            var employee = JsonSerializer.Deserialize<Employee>(jsonString);    //(jsonString)→(text)
            return employee;
        }

        //問題12.1.2
        //シリアル化してファイルへ出力する
        static void Serialize(string filePath, IEnumerable<Employee> employees) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            //byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(employees, options);
            //File.WriteAllBytes(filePath, utf8Bytes);
            string jsonString = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine(jsonString);
        }

        //問題12.1.3
        //12.1.2で作成したファイルを読み込み逆シリアル化
        static Employee[] Deserialize_f(string filePath) {
            //追加
            var options = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };
            var text = File.ReadAllText(filePath);
            var empd = JsonSerializer.Deserialize<Employee[]>(text, options);
            return empd ?? [];  //修正
        }

        public record Employee {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime HireDate { get; set; }
        }
    }
}
