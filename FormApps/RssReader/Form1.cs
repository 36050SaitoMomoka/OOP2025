using System.Net;
using System.Security.Cryptography;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;
        public  Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"��v","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"����","https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            {"����", "https://news.yahoo.co.jp/rss/topics/world.xml"},
            {"�o��", "https://news.yahoo.co.jp/rss/topics/business.xml"},
            {"�G���^��", "https://news.yahoo.co.jp/rss/topics/entertainment.xml"},
            {"�X�|�[�c", "https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"�Ȋw","https://news.yahoo.co.jp/rss/topics/science.xml" },
            {"�n��", "https://news.yahoo.co.jp/rss/topics/local.xml"},
        };

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            //��
            using (var hc = new HttpClient()) {
                //URL�`�F�b�N
                string xml = getRssUrl(cbURL.Text);
                if (!rssUrlDict.ContainsKey(cbURL.Text) && !Uri.IsWellFormedUriString(xml, UriKind.Absolute)) {
                    tsslMessage.Text = "����";
                    return;
                }
                xml = await hc.GetStringAsync(getRssUrl(cbURL.Text));
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

        //�R���{�{�b�N�X�̕�������`�F�b�N���ăA�N�Z�X�\��URL��ԋp����
        private string getRssUrl(string str) {
            //��
            if (rssUrlDict.ContainsKey(str)) {
                return rssUrlDict[str];
            }
            return str;

            //foreach (var item in rssUrlDict) {
            //    if (str.Contains(item.Key)) {
            //        return item.Value;
            //    }
            //}
            ////�G���[�p
            //return "https://news.yahoo.co.jp/rss";
        }

        //try {
        //    using (var hc = new HttpClient()) {
        //        string xml = await hc.GetStringAsync(cbURL.Text);
        //        XDocument xdoc = XDocument.Parse(xml);   //RSS�̎擾

        //        //using (var wc = new WebClient()) {
        //        //var url = wc.OpenRead(tbUrl.Text);
        //        //XDocument xdoc = XDocument.Load(url);

        //        //RSS����͂��ĕK�v�ȗv�f���擾
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

        //        //RSS����͂��ĕK�v�ȗv�f���擾
        //        items = xdoc.Root.Descendants("item")
        //            .Select(x =>
        //                new ItemData {
        //                    Title = (string?)x.Element("title"),
        //                    Link = (string?)x.Element("link"),
        //                }).ToList();
        //    }
        //}

        ////���X�g�{�b�N�X�փ^�C�g����\��
        //lbTitles.Items.Clear();
        //items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�f�[�^�Ȃ�"));  //�����N���g��

        //�^�C�g����I���i�N���b�N�j�����Ƃ��ɌĂ΂��C�x���g�n���h��

        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }

        private void Form1_Load(object sender, EventArgs e) {
            GoForwardBtEnableSet();

            //�t�@�C���ǂݍ���
            rssUrlDict = Deserialize_f("favorite.json");

            //���X�g�{�b�N�X�̐F�ݒ�p
            lbTitles.DrawMode = DrawMode.OwnerDrawFixed;
            lbTitles.DrawItem += lbTitles_DrawItem;

            //�R���{�{�b�N�X����I���i�𓚁j
            cbURL.DataSource = rssUrlDict.Select(k => k.Key).ToList();

            //foreach (var item in rssUrlDict.Keys) {
            //    cbURL.Items.Add(item);
            //}

            //�R���{�{�b�N�X�̏����\���@��
            cbURL.SelectedIndex = -1;
        }

        //�߂�{�^��
        private void btGoBack_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }

        //�i�ރ{�^��
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

        //���C�ɓ���o�^�Ή�
        private void btAdd_Click(object sender, EventArgs e) {
            if (rssUrlDict.ContainsKey(cbURL.Text) || rssUrlDict.ContainsValue(cbURL.Text)) {
                tsslMessage.Text = "���O�܂���URL�����łɓo�^����Ă��܂�";
            } else {
                rssUrlDict.Add(tbAdd.Text, cbURL.Text);
                cbURL.DataSource = rssUrlDict.Select(x => x.Key).ToList();
                tsslMessage.Text = "�o�^���܂����B";

                //�R���{�{�b�N�X�̕\���@��
                cbURL.SelectedIndex = -1;
                //�e�L�X�g�{�b�N�X�̕\���@��
                tbAdd.Text = "";
            }
        }

        //���C�ɓ���폜�Ή�
        private void btRemove_Click(object sender, EventArgs e) {
            bool result = rssUrlDict.Remove(cbURL.Text);
            cbURL.DataSource = rssUrlDict.Select(x => x.Key).ToList();
            tsslMessage.Text = "�폜���܂����B";

            //�R���{�{�b�N�X�̕\���@��
            cbURL.SelectedIndex = -1;
            //�e�L�X�g�{�b�N�X�̕\���@��
            tbAdd.Text = "";
        }

        //���X�g�{�b�N�X�Ɍ��݂ɐF������
        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {

            //�O�s�̎�
            if (e.Index < 0) return;

            //�w�i�F
            if (e.Index % 2 == 0) {
                //�����s
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            } else {
                //��s
                e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
            }

            //�e�L�X�g�`��
            string itemText = ((ListBox)sender).Items[e.Index].ToString() ?? "�G���[";
            e.Graphics.DrawString(itemText, e.Font, Brushes.Black, e.Bounds);

            //�I��g�̕`��
            e.DrawFocusRectangle();
        }

        //���C�ɓ���ۑ�
        private void btSave_Click(object sender, EventArgs e) {
            Serialize("favorite.json",rssUrlDict);
            tsslMessage.Text = "�ۑ����܂����B";
        }

        //�V���A�������ăt�@�C���֏o�͂���
        static void Serialize(string filePath, Dictionary<string,string> dictionary) {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            string jsonString = JsonSerializer.Serialize(dictionary, options);
            File.WriteAllText(filePath, jsonString);
            Console.WriteLine(jsonString);
        }

        //�쐬�����t�@�C����ǂݍ��݋t�V���A����
        static Dictionary<string,string> Deserialize_f(string filePath) {
            var options = new JsonSerializerOptions {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true,
            };

            if (!File.Exists(filePath)) {
                Console.WriteLine("�t�@�C����������܂���F" + filePath);
                return new Dictionary<string, string>();
            }

            var text = File.ReadAllText(filePath);
            var empd = JsonSerializer.Deserialize<Dictionary<string,string>>(text, options);
            return empd ?? [];
        }
    }
}