using System.Text.Json;
using System.Text.Json.Serialization;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var jsonString = File.ReadAllText("novelist.json");
            var novelist = Deserialize(jsonString);

            if (novelist is not null) {
                Console.WriteLine(novelist);
                foreach (var item in novelist.Masterpieces) {
                    Console.WriteLine(item);
                }
            }
        }

        static Novelist? Deserialize(string jsonString) {
            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                //数値の取り扱い方法を変更p283
                NumberHandling =
                    JsonNumberHandling.AllowNamedFloatingPointLiterals |
                    JsonNumberHandling.AllowReadingFromString
            };
            var novel = JsonSerializer.Deserialize<Novelist>(jsonString, options);
            return novel;
        }
    }

    public record Novelist {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        [JsonPropertyName("birth")]
        public DateTime Birthday { get; init; }
        public string[] Masterpieces { get; init; } = [];   //空の配列を作成
    }
}
