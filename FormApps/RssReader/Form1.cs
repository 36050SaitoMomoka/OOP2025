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
                XDocument xdoc = XDocument.Parse(xml);   //RSS�̎擾

                //using (var wc = new WebClient()) {
                //var url = wc.OpenRead(tbUrl.Text);
                //XDocument xdoc = XDocument.Load(url);

                //RSS����͂��ĕK�v�ȗv�f���擾
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();

                //���X�g�{�b�N�X�փ^�C�g����\��
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�f�[�^�Ȃ�"));  //�����N���g��
            }
        }

        //�^�C�g����I���i�N���b�N�j�����Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }

        //�߂�{�^��
        private void button1_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
            if (!wvRssLink.CanGoBack) {
                btGoBack.Enabled = false;   //�}�X�N����
            }
        }

        //�i�ރ{�^��
        private void button2_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
            if (!wvRssLink.CanGoForward) {
                btGoForward.Enabled = false;    //�}�X�N����
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            btGoForward.Enabled = false;   //�}�X�N����
            btGoBack.Enabled = false;
        }

        private void wvRssLink_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
            btGoForward.Enabled = true;
            btGoBack.Enabled = true;
        }
    }
}

//https://news.yahoo.co.jp/rss��yahoo RSS�̃����N
