using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging; // 画像表示用

namespace TenkiApp {
    public partial class MainWindow : Window {
        private readonly List<Location> allLocations = new();

        public MainWindow() {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // 地域データ読み込みと初期化
            LoadLocations(@"C:\Users\infosys\source\repos\OOP2025\WPF\TenkiApp\locations.txt");
            PopulatePrefectureComboBox();

            DateText.Text = DateTime.Now.ToString("yyyy/MM/dd(ddd) H:mm");

            // アプリ起動時に現在地の天気を表示
            LoadCurrentLocationWeather();
        }

        // ===== 地域データ読み込み =====
        private void LoadLocations(string filePath) {
            try {
                if (!File.Exists(filePath)) {
                    MessageBox.Show("locations.txt が見つかりません。パスを確認してください。");
                    return;
                }

                using var reader = new StreamReader(filePath, Encoding.UTF8);
                string? line;
                while ((line = reader.ReadLine()) != null) {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 5) continue;

                    var loc = new Location {
                        Code = parts[0],
                        Prefecture = parts[1],
                        City = parts[2],
                        PrefectureKana = parts[3],
                        CityKana = parts[4],
                        Latitude = parts.Length >= 7 ? SafeParseDouble(parts[5]) : 36.3,
                        Longitude = parts.Length >= 7 ? SafeParseDouble(parts[6]) : 139.4
                    };
                    allLocations.Add(loc);
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"読み込みエラー: {ex.Message}");
            }
        }

        private static double SafeParseDouble(string s) {
            var normalized = s.Replace(',', '.');
            if (double.TryParse(normalized, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
                return d;
            return 0.0;
        }

        // ===== 都道府県コンボボックス初期化 =====
        private void PopulatePrefectureComboBox() {
            var prefectures = allLocations.Select(l => l.Prefecture)
                                          .Where(p => !string.IsNullOrWhiteSpace(p))
                                          .Distinct()
                                          .ToList();

            PrefectureComboBox.ItemsSource = prefectures;
            PrefectureComboBox.SelectedIndex = -1;

            CityComboBox.ItemsSource = null;
            CityComboBox.SelectedIndex = -1;
            CityComboBox.Text = "市区町村を選択";
        }

        // ===== 都道府県選択イベント =====
        private void PrefectureComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            if (PrefectureComboBox.SelectedItem is string selectedPrefecture) {
                var cities = allLocations.Where(l => l.Prefecture == selectedPrefecture)
                                         .Select(l => l.City)
                                         .Where(c => !string.IsNullOrWhiteSpace(c))
                                         .Distinct()
                                         .ToList();

                CityComboBox.ItemsSource = cities;
                CityComboBox.SelectedIndex = -1;
                CityComboBox.Text = "市区町村を選択";
            }
        }

        // ===== 検索ボタンイベント =====
        private async void SearchButton_Click(object sender, RoutedEventArgs e) {
            string prefecture = PrefectureComboBox.SelectedItem as string ?? "";
            string city = CityComboBox.SelectedItem as string ?? "";

            if (string.IsNullOrWhiteSpace(prefecture))
                prefecture = PrefectureComboBox.Text?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(city))
                city = CityComboBox.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(prefecture) || string.IsNullOrWhiteSpace(city)) {
                MessageBox.Show("都道府県と市区町村を選択してください。");
                return;
            }

            SelectedRegionText.Text = $"{prefecture} {city}";

            var location = allLocations.FirstOrDefault(l => l.Prefecture == prefecture && l.City == city);
            if (location == null) {
                MessageBox.Show("座標情報が見つかりません。locations.txt を確認してください。");
                return;
            }

            string info = await GetWeatherAsync(location.Latitude, location.Longitude);

            UpdateBackgroundByWeather(WeatherConditionText.Text);
        }

        // ===== 天気データ取得 =====
        private async Task<string> GetWeatherAsync(double latitude, double longitude) {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}" +
                         $"&current=temperature_2m,wind_speed_10m,wind_direction_10m,relative_humidity_2m" +
                         $"&daily=temperature_2m_max,temperature_2m_min,weathercode&timezone=Asia%2FTokyo";

            using var http = new HttpClient();
            try {
                var weather = await http.GetFromJsonAsync<WeatherResponse>(url);

                if (weather?.current != null &&
                    weather.daily?.temperature_2m_max?.Count > 0 &&
                    weather.daily?.temperature_2m_min?.Count > 0 &&
                    weather.daily?.weathercode?.Count > 0) {

                    CurrentTempText.Text = $"{weather.current.temperature_2m:F1} ℃";
                    MaxTempText.Text = $"{weather.daily.temperature_2m_max[0]:F1} ℃";
                    MinTempText.Text = $"{weather.daily.temperature_2m_min[0]:F1} ℃";
                    WeatherConditionText.Text = ConvertWeatherCodeToText(weather.daily.weathercode[0]);

                    WindSpeedText.Text = $"風速: {weather.current.wind_speed_10m:F1} m/s";
                    HumidityText.Text = $"湿度: {weather.current.relative_humidity_2m:F1} %";

                    // 右側の表示
                    SetWeatherIcon(weather.daily.weathercode[0]);
                    LaundryAdviceText.Text = GetLaundryAdvice(weather.daily.weathercode[0], weather.current.relative_humidity_2m);

                    ShowWeeklyWeather(weather.daily);

                    return "";
                } else {
                    return "天気データが取得できませんでした。";
                }
            }
            catch (Exception ex) {
                return $"エラー：{ex.Message}";
            }
        }

        // ===== 天気コードを日本語に変換 =====
        private static string ConvertWeatherCodeToText(int code) {
            return code switch {
                0 => "快晴",
                1 => "晴れ",
                2 => "薄曇り",
                3 => "曇り",
                45 => "霧",
                48 => "霧雨",
                51 or 53 or 55 => "小雨",
                61 or 63 or 65 => "雨",
                71 or 73 or 75 => "雪",
                80 or 81 or 82 => "にわか雨",
                95 or 96 or 99 => "雷雨",
                _ => "不明"
            };
        }

        // ===== 風向き変換 =====
        private static string ConvertWindDirection(double degree) {
            string[] dirs = { "北", "北北東", "北東", "東北東", "東", "東南東", "南東", "南南東",
                              "南", "南南西", "南西", "西南西", "西", "西北西", "北西", "北北西" };
            int index = (int)Math.Round(((degree % 360) / 22.5), MidpointRounding.AwayFromZero);
            return dirs[index % 16];
        }

        // ===== 天気アイコン切り替え =====
        private void SetWeatherIcon(int code) {
            string iconPath = "Images/unknown.png";
            if (code == 0 || code == 1) iconPath = "Images/sunny.png";
            else if (code == 2 || code == 3) iconPath = "Images/cloudy.png";
            else if (code >= 51 && code <= 65) iconPath = "Images/rain.png";
            else if (code >= 71 && code <= 75) iconPath = "Images/snow.png";
            else if (code >= 95) iconPath = "Images/thunder.png";

            WeatherIcon.Source = new BitmapImage(new Uri(iconPath, UriKind.Relative));
        }

        // ===== 洗濯指数判定 =====
        private static string GetLaundryAdvice(int weatherCode, double humidity) {
            if (weatherCode == 0 || weatherCode == 1) {
                if (humidity < 60) return "今日は洗濯日和です！";
                else return "晴れですが湿度が高め、乾きにくいかも。";
            } else if (weatherCode == 2 || weatherCode == 3) {
                return "曇りなので外干しは微妙。部屋干しがおすすめ。";
            } else if (weatherCode >= 51 && weatherCode <= 65) {
                return "雨なので部屋干ししてください。";
            } else if (weatherCode >= 71 && weatherCode <= 75) {
                return "雪が降るので外干しは避けましょう。";
            } else {
                return "天気が不安定なので部屋干しがおすすめです。";
            }
        }

        // 一週間の天気を表示
        private void ShowWeeklyWeather(DailyWeather daily) {
            var list = new List<DailyWeatherDisplay>();

            for (int i = 0; i < daily.time.Count; i++) {
                var day = DateTime.Parse(daily.time[i]).ToString("MM/dd");
                var max = daily.temperature_2m_max[i];
                var min = daily.temperature_2m_min[i];
                var code = daily.weathercode[i];
                string condition = ConvertWeatherCodeToText(code);

                string iconFile = GetIconFileName(code);
                var icon = new BitmapImage(new Uri($"pack://application:,,,/Images/{iconFile}", UriKind.Absolute));

                list.Add(new DailyWeatherDisplay {
                    Date = day,
                    Icon = icon,
                    MaxTemp = $"↑{max:F1}℃",
                    MinTemp = $"↓{min:F1}℃",
                    Condition = condition
                });
            }

            WeeklyWeatherPanel.ItemsSource = list;
        }

        // アイコンファイル名を返す補助メソッド
        private static string GetIconFileName(int code) {
            if (code == 0 || code == 1) return "sunny.png";
            else if (code == 2 || code == 3) return "cloudy.png";
            else if (code >= 51 && code <= 65) return "rain.png";
            else if (code >= 71 && code <= 75) return "snow.png";
            else if (code >= 95) return "thunder.png";
            else return "unknown.png";
        }

        // IPから現在地を取得
        private async Task<(double lat, double lon, string city, string region)> GetLocationByIpAsync() {
            using var http = new HttpClient();
            var json = await http.GetFromJsonAsync<IpLocation>("http://ip-api.com/json/");
            return (json.lat, json.lon, json.city, json.regionName);
        }

        // 起動時に現在地の天気を表示
        private async void LoadCurrentLocationWeather() {
            var (lat, lon, city, region) = await GetLocationByIpAsync();
            string info = await GetWeatherAsync(lat, lon);
            WeatherInfoText.Text = info;
            SelectedRegionText.Text = $"現在地";

            UpdateBackgroundByWeather(WeatherConditionText.Text);
        }

        private void UpdateBackgroundByWeather(string condition) {
            string imagePath = "Images/bg_sunny.jpg"; // デフォルト

            if (condition.Contains("晴"))
                imagePath = "Images/bg_sunny.jpg";
            else if (condition.Contains("曇"))
                imagePath = "Images/bg_cloudy.jpg";
            else if (condition.Contains("雨"))
                imagePath = "Images/bg_rain.jpg";
            else if (condition.Contains("雪"))
                imagePath = "Images/bg_snow.jpg";
            else if (condition.Contains("霧"))
                imagePath = "Images/bg_fog.jpg";
            else if (condition.Contains("雷"))
                imagePath = "Images/bg_thunder.jpg";

            this.Background = new ImageBrush(new BitmapImage(new Uri(imagePath, UriKind.Relative))) {
                Stretch = Stretch.UniformToFill,
                Opacity = 0.8
            };
        }
    }

    // 地域情報を保持するクラス
    public class Location {
        public string Code { get; set; } = "";
        public string Prefecture { get; set; } = "";
        public string City { get; set; } = "";
        public string PrefectureKana { get; set; } = "";
        public string CityKana { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    // Open-Meteo API のレスポンス全体
    public class WeatherResponse {
        public CurrentWeather current { get; set; } = new();
        public DailyWeather daily { get; set; } = new();
    }

    // 現在の天気データ
    public class CurrentWeather {
        public string time { get; set; } = "";
        public double temperature_2m { get; set; }
        public double wind_speed_10m { get; set; }
        public double wind_direction_10m { get; set; } // ← 風向き追加
        public double relative_humidity_2m { get; set; }
    }

    // 日ごとの天気データ
    public class DailyWeather {
        public List<string> time { get; set; } = new();
        public List<double> temperature_2m_max { get; set; } = new();
        public List<double> temperature_2m_min { get; set; } = new();
        public List<int> weathercode { get; set; } = new();
    }

    public class DailyWeatherDisplay {
        public string Date { get; set; } = "";
        public BitmapImage Icon { get; set; } = new();
        public string Condition { get; set; } = "";
        public string MaxTemp { get; set; } = "";
        public string MinTemp { get; set; } = "";
    }

    // IPベースで取得した位置情報を保持するクラス
    public class IpLocation {
        public double lat { get; set; }
        public double lon { get; set; }
        public string city { get; set; }
        public string regionName { get; set; }
    }
}