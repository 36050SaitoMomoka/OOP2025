using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Windows;
using System.Xml.Linq;

namespace WebBrowser {
    public partial class MainWindow : Window {
        #region　自分のコード
        //private List<ItemData> items = new List<ItemData>();
        #endregion

        public MainWindow() {
            InitializeComponent();
            InitializeAsync();
        }

        private async void InitializeAsync() {
            await WebView.EnsureCoreWebView2Async();    //非同期にしてブラウザの初期化処理を行う

            WebView.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
            WebView.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;

        }

        //読み込み開始したらプログレスバーを表示
        private void CoreWebView2_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e) {
            LoadingBar.Visibility = Visibility.Visible;
            LoadingBar.IsIndeterminate = true;
        }

        //読み込み完了したらプログレスバーを非表示
        private void CoreWebView2_NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e) {
            LoadingBar.Visibility = Visibility.Collapsed;
            LoadingBar.IsIndeterminate = false;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e) {
            if (WebView.CanGoBack) {    //修正後
                WebView.GoBack();
            }
        }

        private void FowardButton_Click(object sender, RoutedEventArgs e) {
            if (WebView.CanGoForward) {
                WebView.GoForward();
            }
        }

        private void GoButton_Click(object sender, RoutedEventArgs e) {
            var url = AddressBar.Text.Trim();

            if (string.IsNullOrWhiteSpace(url)) return;

            WebView.Source = new Uri(url);

            #region　自分のコード
            //using (var hc = new HttpClient()) {
            //    string xml = await hc.GetStringAsync(getRssUrl(AddressBar.Text));
            //    XDocument xdoc = XDocument.Parse(xml);

            //    var items = xdoc.Root.Descendants("item")
            //        .Select(x => new {
            //            Title = (string?)x.Element("title"),
            //            Link = (string?)x.Element("link"),
            //        }).ToList();

            //    if (items.Count > 0 && items[0].Link != null) {
            //        AddressBar.Text = items[0].Link;
            //        WebView.Source = new Uri(items[0].Link);
            //    } else {
            //        MessageBox.Show("リンクが見つかりませんでした。");
            //    }
            //}

            //    }

            //    private string getRssUrl(string input) {
            //        return input;
            //    }
            //}

            //public class ItemData {
            //    public string? Title { get; set; }
            //    public string? Link { get; set; }
            //}
            #endregion
        }
    }
}