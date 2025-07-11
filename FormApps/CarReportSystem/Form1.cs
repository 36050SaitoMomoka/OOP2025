using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //カーレポート管理用
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        //設定クラスのインスタンスを生成
        Settings settings = new Settings();

        public Form1() {
            InitializeComponent();

            //「日付」に時間を表示させない
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "yyyy/MM/dd";

            dgvRecord.DataSource = listCarReports;

            //dgvRecordの時間を表示させない
            dgvRecord.Columns["Date"].DefaultCellStyle.Format = "yyyy/MM/dd";
        }

        private void btPicOpen_Click(object sender, EventArgs e) {
            if (ofdPicFileOpen.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        private void btPicDelete_Click(object sender, EventArgs e) {
            pbPicture.Image = null;
        }

        private void btRecordAdd_Click(object sender, EventArgs e) {

            tsslbMessage.Text = String.Empty;

            var carReport = new CarReport {
                Date = dtpDate.Value,
                Author = cbAuthor.Text,
                Maker = getRadioButtonMaker(),
                CarName = cbCarName.Text,
                Report = tbReport.Text,
                Picture = pbPicture.Image
            };

            if (cbAuthor.Text == String.Empty || cbCarName.Text == String.Empty) {
                tsslbMessage.Text = "記録者、または車名が未入力です";
                return;
            }
            listCarReports.Add(carReport);
            setCbAuthor(cbAuthor.Text);
            setCbCarName(cbCarName.Text);
            inputItemsAllClear();   //登録項目クリア
        }

        //入力項目をすべてクリア
        private void inputItemsAllClear() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = "";
            rbOther.Checked = true; //ラジオボタンはどれか1つはついてるから
            cbCarName.Text = "";
            tbReport.Text = "";
            pbPicture.Image = null;
        }

        private CarReport.MakerGroup getRadioButtonMaker() {
            if (rbToyota.Checked) {
                return CarReport.MakerGroup.トヨタ;
            } else if (rbNissan.Checked) {
                return CarReport.MakerGroup.日産;
            } else if (rbHonda.Checked) {
                return CarReport.MakerGroup.ホンダ;
            } else if (rbSubaru.Checked) {
                return CarReport.MakerGroup.スバル;
            } else if (rbImport.Checked) {
                return CarReport.MakerGroup.輸入車;
            } else if (rbOther.Checked) {
                return CarReport.MakerGroup.その他;
            }
            return CarReport.MakerGroup.なし;
        }

        private void dgvRecord_Click(object sender, EventArgs e) {
            //if (dgvRecord.CurrentRow == null) return;

            dtpDate.Value = ((DateTime)dgvRecord.CurrentRow.Cells["Date"].Value).Date;
            cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
            setRadioButtonMaker((CarReport.MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
            cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
            tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
            pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
        }

        //指定したメーカーのラジオボタンをセット
        private void setRadioButtonMaker(CarReport.MakerGroup targetMaker) {
            switch (targetMaker) {
                case CarReport.MakerGroup.トヨタ:
                    rbToyota.Checked = true;
                    break;
                case CarReport.MakerGroup.日産:
                    rbNissan.Checked = true;
                    break;
                case CarReport.MakerGroup.ホンダ:
                    rbHonda.Checked = true;
                    break;
                case CarReport.MakerGroup.スバル:
                    rbSubaru.Checked = true;
                    break;
                case CarReport.MakerGroup.輸入車:
                    rbImport.Checked = true;
                    break;
                case CarReport.MakerGroup.その他:
                    rbOther.Checked = true;
                    break;
            }
        }

        //記録者の履歴をコンボボックスへ登録（重複なし）
        private void setCbAuthor(string author) {
            //既に登録済みか確認
            if (!cbAuthor.Items.Contains(author)) {     // ! = not
                //未登録なら登録【登録済みなら何もしない】
                cbAuthor.Items.Add(author);
            }
        }

        //車名の履歴をコンボボックスへ登録（重複なし）
        private void setCbCarName(string carName) {
            if (!cbCarName.Items.Contains(carName)) {
                cbCarName.Items.Add(carName);
            }
        }

        //新規追加ボタンのイベントハンドラ
        private void btNewRecord_Click(object sender, EventArgs e) {
            inputItemsAllClear();
        }

        //修正ボタンのイベントハンドラ
        private void btRecordModify_Click(object sender, EventArgs e) {
            var index = dgvRecord.CurrentRow.Index;

            if (dgvRecord.Rows.Count == 0) ;

            listCarReports[index].Date = dtpDate.Value;
            listCarReports[index].Author = cbAuthor.Text;
            listCarReports[index].Maker = getRadioButtonMaker();
            listCarReports[index].CarName = cbCarName.Text;
            listCarReports[index].Report = tbReport.Text;
            listCarReports[index].Picture = pbPicture.Image;

            dgvRecord.Refresh();
        }

        //削除ボタンのイベントハンドラ
        private void btRecordDelete_Click(object sender, EventArgs e) {
            //選択されていない場合は処理を行わない
            if (dgvRecord.CurrentRow?.Index != null) {
                //選択されているインデックスを取得
                var index = dgvRecord.CurrentRow.Index;
                //削除したいインデックスを指定してリストから削除
                listCarReports.RemoveAt(index);
                inputItemsAllClear();
            }
            //解答
            //    if((dgvRecord.CurrentRow == null)     selectedはbool
            //        || (!dgvRecord.CurrentRow.Selected)) return;
        }

        private void Form1_Load(object sender, EventArgs e) {
            inputItemsAllClear();

            //交互に色を設定（データグリッドビュー）
            dgvRecord.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            //設定ファイルを読み込み背景色を設定する（逆シリアル化）

            //using (var reader = XmlReader.Create("setting.xml")) {
            //    var serializer = new XmlSerializer(typeof(Settings));
            //    var settings = serializer.Deserialize(reader) as Settings;

            //    BackColor = Color.FromArgb(settings.MainFormBackColor);
            //}

            //解答
            if (File.Exists("setting.xml")) {   //ファイルが存在してるか
                try {
                    using (var reader = XmlReader.Create("setting.xml")) {
                        var serializer = new XmlSerializer(typeof(Settings));
                        settings = serializer.Deserialize(reader) as Settings;
                        //var set = serializer.Deserialize(reader) as Settings;
                        //背景色設定
                        //BackColor = Color.FromArgb(set.MainFormBackColor);
                        //設定クラスのインスタンスにも現在の設定色を設定
                        //settings.MainFormBackColor = BackColor.ToArgb();
                        //settings = set;
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "ファイル読み込みエラー";
                    MessageBox.Show(ex.Message);    //より具体的なエラーを出力
                }
            } else {
                tsslbMessage.Text = "設定ファイルがありません。";
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void tsmiAbout_Click(object sender, EventArgs e) {
            fmVersion fm = new fmVersion();
            fm.ShowDialog();
        }

        private void 色設定ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cdColor.ShowDialog() == DialogResult.OK) {
                BackColor = cdColor.Color;
                //設定ファイルへ保存
                settings.MainFormBackColor = cdColor.Color.ToArgb();    //背景色を設定インスタンスへ設定
            }
        }

        //ファイルオープン処理
        private void reportOpenFile() {
            if (ofdReportFileOpen.ShowDialog() == DialogResult.OK) {
                try {
                    //逆シリアル化でバイナリ形式を取り込む
#pragma warning disable SYSLIB0011//型またはメンバーが旧形式です
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011//型またはメンバーが旧形式です

                    using (FileStream fs = File.Open(
                                    ofdReportFileOpen.FileName, FileMode.Open, FileAccess.Read)) {

                        listCarReports = (BindingList<CarReport>)bf.Deserialize(fs);
                        dgvRecord.DataSource = listCarReports;
                        //コンボボックスに登録
                        foreach (var item in listCarReports) {
                            setCbAuthor(item.Author);
                            setCbCarName(item.CarName);
                        }
                    }
                }
                catch (Exception) {
                    tsslbMessage.Text = "ファイル形式が違います。";
                }
            }
        }

        //ファイルセーブ処理
        private void reportSaveFile() {
            if (sfdReportFileSave.ShowDialog() == DialogResult.OK) {
                try {
                    //バイナリ形式でシリアル化
#pragma warning disable SYSLIB0011
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011

                    using (FileStream fs = File.Open(
                                    sfdReportFileSave.FileName, FileMode.Create)) {

                        bf.Serialize(fs, listCarReports);
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "ファイル書き出しエラー";
                    MessageBox.Show(ex.Message);//←より具体的なエラーを出力
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e) {
            reportSaveFile();
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e) {
            reportOpenFile();
        }

        //フォームが閉じたら呼ばれる
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            //設定ファイルへ色情報を保存する処理（シリアル化）
            //P284意向を参考にする（ファイル名：setting.xml）

            //using (var writer = XmlWriter.Create("setting.xml")) {
            //    var serializer = new XmlSerializer(settings.GetType());
            //    serializer.Serialize(writer, settings);
            //}

            //解答
            try {
                using (var writer = XmlWriter.Create("setting.xml")) {
                    var serializer = new XmlSerializer(settings.GetType());
                    serializer.Serialize(writer, settings);
                }
            }
            catch (Exception ex) {
                tsslbMessage.Text = "ファイル書き出しエラー";
                MessageBox.Show(ex.Message);//より具体的なエラーを出力
            }
        }
    }
}