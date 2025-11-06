using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Windows;
using System.Xml.Linq;

namespace WebBrowser {
    public partial class MainWindow : Window {
        private List<ItemData> items = new List<ItemData>();

        public MainWindow() {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            WebView.GoBack();
        }

        private void FowardButton_Click(object sender, RoutedEventArgs e) {
            WebView.GoForward();
        }

        private async void GoButton_Click(object sender, RoutedEventArgs e) {
                using (var hc = new HttpClient()) {
                    string xml = await hc.GetStringAsync(getRssUrl(AddressBar.Text));
                    XDocument xdoc = XDocument.Parse(xml);

                    var items = xdoc.Root.Descendants("item")
                        .Select(x => new {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                    if (items.Count > 0 && items[0].Link != null) {
                        AddressBar.Text = items[0].Link;
                        WebView.Source = new Uri(items[0].Link);
                    } else {
                        MessageBox.Show("リンクが見つかりませんでした。");
                    }
                }
            }

        private string getRssUrl(string input) {
            return input;
        }
    }

    public class ItemData {
        public string? Title { get; set; }
        public string? Link { get; set; }
    }
}