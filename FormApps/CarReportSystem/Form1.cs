using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //�J�[���|�[�g�Ǘ��p
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        //�ݒ�N���X�̃C���X�^���X�𐶐�
        Settings settings = new Settings();

        public Form1() {
            InitializeComponent();

            //�u���t�v�Ɏ��Ԃ�\�������Ȃ�
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "yyyy/MM/dd";

            dgvRecord.DataSource = listCarReports;

            //dgvRecord�̎��Ԃ�\�������Ȃ�
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
                tsslbMessage.Text = "�L�^�ҁA�܂��͎Ԗ��������͂ł�";
                return;
            }
            listCarReports.Add(carReport);
            setCbAuthor(cbAuthor.Text);
            setCbCarName(cbCarName.Text);
            inputItemsAllClear();   //�o�^���ڃN���A
        }

        //���͍��ڂ����ׂăN���A
        private void inputItemsAllClear() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = "";
            rbOther.Checked = true; //���W�I�{�^���͂ǂꂩ1�͂��Ă邩��
            cbCarName.Text = "";
            tbReport.Text = "";
            pbPicture.Image = null;
        }

        private CarReport.MakerGroup getRadioButtonMaker() {
            if (rbToyota.Checked) {
                return CarReport.MakerGroup.�g���^;
            } else if (rbNissan.Checked) {
                return CarReport.MakerGroup.���Y;
            } else if (rbHonda.Checked) {
                return CarReport.MakerGroup.�z���_;
            } else if (rbSubaru.Checked) {
                return CarReport.MakerGroup.�X�o��;
            } else if (rbImport.Checked) {
                return CarReport.MakerGroup.�A����;
            } else if (rbOther.Checked) {
                return CarReport.MakerGroup.���̑�;
            }
            return CarReport.MakerGroup.�Ȃ�;
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

        //�w�肵�����[�J�[�̃��W�I�{�^�����Z�b�g
        private void setRadioButtonMaker(CarReport.MakerGroup targetMaker) {
            switch (targetMaker) {
                case CarReport.MakerGroup.�g���^:
                    rbToyota.Checked = true;
                    break;
                case CarReport.MakerGroup.���Y:
                    rbNissan.Checked = true;
                    break;
                case CarReport.MakerGroup.�z���_:
                    rbHonda.Checked = true;
                    break;
                case CarReport.MakerGroup.�X�o��:
                    rbSubaru.Checked = true;
                    break;
                case CarReport.MakerGroup.�A����:
                    rbImport.Checked = true;
                    break;
                case CarReport.MakerGroup.���̑�:
                    rbOther.Checked = true;
                    break;
            }
        }

        //�L�^�҂̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbAuthor(string author) {
            //���ɓo�^�ς݂��m�F
            if (!cbAuthor.Items.Contains(author)) {     // ! = not
                //���o�^�Ȃ�o�^�y�o�^�ς݂Ȃ牽�����Ȃ��z
                cbAuthor.Items.Add(author);
            }
        }

        //�Ԗ��̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbCarName(string carName) {
            if (!cbCarName.Items.Contains(carName)) {
                cbCarName.Items.Add(carName);
            }
        }

        //�V�K�ǉ��{�^���̃C�x���g�n���h��
        private void btNewRecord_Click(object sender, EventArgs e) {
            inputItemsAllClear();
        }

        //�C���{�^���̃C�x���g�n���h��
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

        //�폜�{�^���̃C�x���g�n���h��
        private void btRecordDelete_Click(object sender, EventArgs e) {
            //�I������Ă��Ȃ��ꍇ�͏������s��Ȃ�
            if (dgvRecord.CurrentRow?.Index != null) {
                //�I������Ă���C���f�b�N�X���擾
                var index = dgvRecord.CurrentRow.Index;
                //�폜�������C���f�b�N�X���w�肵�ă��X�g����폜
                listCarReports.RemoveAt(index);
                inputItemsAllClear();
            }
            //��
            //    if((dgvRecord.CurrentRow == null)     selected��bool
            //        || (!dgvRecord.CurrentRow.Selected)) return;
        }

        private void Form1_Load(object sender, EventArgs e) {
            inputItemsAllClear();

            //���݂ɐF��ݒ�i�f�[�^�O���b�h�r���[�j
            dgvRecord.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            //�ݒ�t�@�C����ǂݍ��ݔw�i�F��ݒ肷��i�t�V���A�����j

            //using (var reader = XmlReader.Create("setting.xml")) {
            //    var serializer = new XmlSerializer(typeof(Settings));
            //    var settings = serializer.Deserialize(reader) as Settings;

            //    BackColor = Color.FromArgb(settings.MainFormBackColor);
            //}

            //��
            if (File.Exists("setting.xml")) {   //�t�@�C�������݂��Ă邩
                try {
                    using (var reader = XmlReader.Create("setting.xml")) {
                        var serializer = new XmlSerializer(typeof(Settings));
                        settings = serializer.Deserialize(reader) as Settings;
                        //var set = serializer.Deserialize(reader) as Settings;
                        //�w�i�F�ݒ�
                        //BackColor = Color.FromArgb(set.MainFormBackColor);
                        //�ݒ�N���X�̃C���X�^���X�ɂ����݂̐ݒ�F��ݒ�
                        //settings.MainFormBackColor = BackColor.ToArgb();
                        //settings = set;
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "�t�@�C���ǂݍ��݃G���[";
                    MessageBox.Show(ex.Message);    //����̓I�ȃG���[���o��
                }
            } else {
                tsslbMessage.Text = "�ݒ�t�@�C��������܂���B";
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void tsmiAbout_Click(object sender, EventArgs e) {
            fmVersion fm = new fmVersion();
            fm.ShowDialog();
        }

        private void �F�ݒ�ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cdColor.ShowDialog() == DialogResult.OK) {
                BackColor = cdColor.Color;
                //�ݒ�t�@�C���֕ۑ�
                settings.MainFormBackColor = cdColor.Color.ToArgb();    //�w�i�F��ݒ�C���X�^���X�֐ݒ�
            }
        }

        //�t�@�C���I�[�v������
        private void reportOpenFile() {
            if (ofdReportFileOpen.ShowDialog() == DialogResult.OK) {
                try {
                    //�t�V���A�����Ńo�C�i���`������荞��
#pragma warning disable SYSLIB0011//�^�܂��̓����o�[�����`���ł�
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011//�^�܂��̓����o�[�����`���ł�

                    using (FileStream fs = File.Open(
                                    ofdReportFileOpen.FileName, FileMode.Open, FileAccess.Read)) {

                        listCarReports = (BindingList<CarReport>)bf.Deserialize(fs);
                        dgvRecord.DataSource = listCarReports;
                        //�R���{�{�b�N�X�ɓo�^
                        foreach (var item in listCarReports) {
                            setCbAuthor(item.Author);
                            setCbCarName(item.CarName);
                        }
                    }
                }
                catch (Exception) {
                    tsslbMessage.Text = "�t�@�C���`�����Ⴂ�܂��B";
                }
            }
        }

        //�t�@�C���Z�[�u����
        private void reportSaveFile() {
            if (sfdReportFileSave.ShowDialog() == DialogResult.OK) {
                try {
                    //�o�C�i���`���ŃV���A����
#pragma warning disable SYSLIB0011
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011

                    using (FileStream fs = File.Open(
                                    sfdReportFileSave.FileName, FileMode.Create)) {

                        bf.Serialize(fs, listCarReports);
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "�t�@�C�������o���G���[";
                    MessageBox.Show(ex.Message);//������̓I�ȃG���[���o��
                }
            }
        }

        private void �ۑ�ToolStripMenuItem_Click(object sender, EventArgs e) {
            reportSaveFile();
        }

        private void �J��ToolStripMenuItem_Click(object sender, EventArgs e) {
            reportOpenFile();
        }

        //�t�H�[����������Ă΂��
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            //�ݒ�t�@�C���֐F����ۑ����鏈���i�V���A�����j
            //P284�ӌ����Q�l�ɂ���i�t�@�C�����Fsetting.xml�j

            //using (var writer = XmlWriter.Create("setting.xml")) {
            //    var serializer = new XmlSerializer(settings.GetType());
            //    serializer.Serialize(writer, settings);
            //}

            //��
            try {
                using (var writer = XmlWriter.Create("setting.xml")) {
                    var serializer = new XmlSerializer(settings.GetType());
                    serializer.Serialize(writer, settings);
                }
            }
            catch (Exception ex) {
                tsslbMessage.Text = "�t�@�C�������o���G���[";
                MessageBox.Show(ex.Message);//����̓I�ȃG���[���o��
            }
        }
    }
}