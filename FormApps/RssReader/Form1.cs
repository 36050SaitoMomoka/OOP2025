using System.Net;
using System.Xml.Linq;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            using (var hc = new HttpClient()) {
                string xml = await hc.GetStringAsync(tbUrl.Text);
                XDocument xdoc = XDocument.Parse(xml);   //RSSの取得

                //using (var wc = new WebClient()) {
                //var url = wc.OpenRead(tbUrl.Text);
                //XDocument xdoc = XDocument.Load(url);

                //RSSを解析して必要な要素を取得
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                //リストボックスへタイトルを表示
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "データなし"));  //リンクを使う
            }
        }

        //タイトルを選択（クリック）したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }

        //戻るボタン
        private void button1_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
            if (!wvRssLink.CanGoBack) {
                btGoBack.Enabled = false;   //マスク処理
            }
        }

        //進むボタン
        private void button2_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
            if (!wvRssLink.CanGoForward) {
                btGoForward.Enabled = false;    //マスク処理
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            btGoForward.Enabled = false;   //マスク処理
            btGoBack.Enabled = false;
        }

        private void wvRssLink_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
            btGoForward.Enabled = true;
            btGoBack.Enabled = true;
        }
    }
}

//https://news.yahoo.co.jp/rss→yahoo RSSのリンク
