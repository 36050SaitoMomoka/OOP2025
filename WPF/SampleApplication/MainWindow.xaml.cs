using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SampleApplication
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            checkBoxTextBlock.Text = "未チェック";
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("青木優奈");
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("キャンセルされました");
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {
            //MessageBox.Show(string.Format("入力された文字は{0}です", messageTextBox.Text));
        }

        private void seasonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            seasonTextBlock.Text = (string)((ComboBoxItem)(seasonComboBox.SelectedItem)).Content;
        }

        private void redRadioButton_Checked(object sender, RoutedEventArgs e) {
            colorText.Text = (string)((RadioButton)(sender)).Content;
        }

        private void yellowRadioButton_Checked(object sender, RoutedEventArgs e) {
            colorText.Text = "黄";
        }

        private void blueRadioButton_Checked(object sender, RoutedEventArgs e) {
            colorText.Text = "青";
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e) {
            checkBoxTextBlock.Text = "チェック済み";
        }

        private void checkBox_Unchecked(object sender, RoutedEventArgs e) {
            checkBoxTextBlock.Text = "未チェック";
        }
    }
}
