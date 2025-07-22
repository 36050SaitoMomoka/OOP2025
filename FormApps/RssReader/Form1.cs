using System.Net;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;
        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"主要","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"国内","https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            {"国際", "https://news.yahoo.co.jp/rss/topics/world.xml"},
            {"経済", "https://news.yahoo.co.jp/rss/topics/business.xml"},
            {"エンタメ", "https://news.yahoo.co.jp/rss/topics/entertainment.xml"},
            {"スポーツ", "https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"科学","https://news.yahoo.co.jp/rss/topics/science.xml" },
            {"地域", "https://news.yahoo.co.jp/rss/topics/local.xml"},
        };

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            try {
                using (var hc = new HttpClient()) {
                    string xml = await hc.GetStringAsync(cbURL.Text);
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
                }
            }
            catch (Exception) {
                using (var hc = new HttpClient()) {
                    var value = rssUrlDict[cbURL.Text];
                    string xml = await hc.GetStringAsync(value.ToString());
                    XDocument xdoc = XDocument.Parse(xml);

                    //RSSを解析して必要な要素を取得
                    items = xdoc.Root.Descendants("item")
                        .Select(x =>
                            new ItemData {
                                Title = (string?)x.Element("title"),
                                Link = (string?)x.Element("link"),
                            }).ToList();
                }
            }

            //リストボックスへタイトルを表示
            lbTitles.Items.Clear();
            items.ForEach(item => lbTitles.Items.Add(item.Title ?? "データなし"));  //リンクを使う
        }

        //タイトルを選択（クリック）したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }

        private void Form1_Load(object sender, EventArgs e) {
            GoForwardBtEnableSet();
            foreach (var item in rssUrlDict.Keys) {
                cbURL.Items.Add(item);
            }
        }

        //戻るボタン
        private void btGoBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }

        //進むボタン
        private void btGoForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }

        private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e) {
            GoForwardBtEnableSet();
        }

        private void GoForwardBtEnableSet() {
            btGoBack.Enabled = wvRssLink.CanGoBack;
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }
    }
}

//https://news.yahoo.co.jp/rss→yahoo RSSのリンク
