using System.Net;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;
        public static readonly Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
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
            //解答
            using (var hc = new HttpClient()) {
                //URLチェック
                if (Uri.IsWellFormedUriString(cbURL.Text, UriKind.Absolute)) {
                    tsslMessage.Text = "有効";
                    try {
                        string xml = await hc.GetStringAsync(getRssUrl(cbURL.Text));
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
                    catch (Exception) {
                        tsslMessage.Text = "取得できませんでした";
                    }
                } else {
                    tsslMessage.Text = "無効";
                }
            }
        }

        //コンボボックスの文字列をチェックしてアクセス可能なURLを返却する
        private string getRssUrl(string str) {
            //解答
            if (rssUrlDict.ContainsKey(str)) {
                return rssUrlDict[str];
            }
            return str;

            //foreach (var item in rssUrlDict) {
            //    if (str.Contains(item.Key)) {
            //        return item.Value;
            //    }
            //}
            ////エラー用
            //return "https://news.yahoo.co.jp/rss";
        }

        //try {
        //    using (var hc = new HttpClient()) {
        //        string xml = await hc.GetStringAsync(cbURL.Text);
        //        XDocument xdoc = XDocument.Parse(xml);   //RSSの取得

        //        //using (var wc = new WebClient()) {
        //        //var url = wc.OpenRead(tbUrl.Text);
        //        //XDocument xdoc = XDocument.Load(url);

        //        //RSSを解析して必要な要素を取得
        //        items = xdoc.Root.Descendants("item")
        //            .Select(x =>
        //                new ItemData {
        //                    Title = (string?)x.Element("title"),
        //                    Link = (string?)x.Element("link"),
        //                }).ToList();
        //    }
        //}
        //catch (Exception) {
        //    using (var hc = new HttpClient()) {
        //        var value = rssUrlDict[cbURL.Text];
        //        string xml = await hc.GetStringAsync(value.ToString());
        //        XDocument xdoc = XDocument.Parse(xml);

        //        //RSSを解析して必要な要素を取得
        //        items = xdoc.Root.Descendants("item")
        //            .Select(x =>
        //                new ItemData {
        //                    Title = (string?)x.Element("title"),
        //                    Link = (string?)x.Element("link"),
        //                }).ToList();
        //    }
        //}

        ////リストボックスへタイトルを表示
        //lbTitles.Items.Clear();
        //items.ForEach(item => lbTitles.Items.Add(item.Title ?? "データなし"));  //リンクを使う

        //タイトルを選択（クリック）したときに呼ばれるイベントハンドラ

        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }

        private void Form1_Load(object sender, EventArgs e) {
            GoForwardBtEnableSet();

            //リストボックスの色設定用
            lbTitles.DrawMode = DrawMode.OwnerDrawFixed;
            lbTitles.DrawItem += lbTitles_DrawItem;

            //コンボボックスから選択（解答）
            cbURL.DataSource = rssUrlDict.Select(k => k.Key).ToList();

            //foreach (var item in rssUrlDict.Keys) {
            //    cbURL.Items.Add(item);
            //}

            //コンボボックスの初期表示　空白
            cbURL.SelectedIndex = -1;
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

        //お気に入り登録対応
        private void btAdd_Click(object sender, EventArgs e) {
            if (rssUrlDict.ContainsKey(cbURL.Text) || rssUrlDict.ContainsValue(cbURL.Text)) {
                tsslMessage.Text = "名前またはURLがすでに登録されています";
            } else {
                rssUrlDict.Add(tbAdd.Text, cbURL.Text);
                cbURL.DataSource = rssUrlDict.Select(x => x.Key).ToList();
                tsslMessage.Text = "登録しました。";

                //コンボボックスの表示　空白
                cbURL.SelectedIndex = -1;
                //テキストボックスの表示　空白
                tbAdd.Text = "";
            }
        }

        //お気に入り削除対応
        private void btRemove_Click(object sender, EventArgs e) {
            bool result = rssUrlDict.Remove(cbURL.Text);
            cbURL.DataSource = rssUrlDict.Select(x => x.Key).ToList();
            tsslMessage.Text = "削除しました。";

            //コンボボックスの表示　空白
            cbURL.SelectedIndex = -1;
            //テキストボックスの表示　空白
            tbAdd.Text = "";
        }

        //リストボックスに交互に色をつける
        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {

            //０行の時
            if (e.Index < 0) return;

            //背景色
            if (e.Index % 2 == 0) {
                //偶数行
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            } else {
                //奇数行
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
            }

            //テキスト描画
            string itemText = ((ListBox)sender).Items[e.Index].ToString() ?? "エラー";
            e.Graphics.DrawString(itemText, e.Font, Brushes.Black, e.Bounds);

            //選択枠の描画
            e.DrawFocusRectangle();
        }
    }
}

//https://news.yahoo.co.jp/rss→yahoo RSSのリンク