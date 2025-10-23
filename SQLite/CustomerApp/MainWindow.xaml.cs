using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace CustomerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private List<Customer> _customer = new List<Customer>();
    private Customer _selected;
    private byte[] SelectedPictureBytes;

    public MainWindow() {
        InitializeComponent();
        ReadDatabase();
        CustomerList.ItemsSource = _customer;
        //imageBox.Source
    }

    //データベースから全レコード取得
    private void ReadDatabase() {
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            _customer = connection.Table<Customer>().ToList();
        }
    }

    //顧客一覧
    private void CustomerList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        _selected = CustomerList.SelectedItem as Customer;

        if (_selected == null) return;

        NameTextBox.Text = _selected.Name;
        PhoneTextBox.Text = _selected.Phone;
        AddressTextBox.Text = _selected.Address;
        SelectedPictureBytes = _selected.Picture;
    }

    //保存ボタン
    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        var newName = NameTextBox.Text;
        var newPhone = PhoneTextBox.Text;
        var newAddress = AddressTextBox.Text;

        if (string.IsNullOrWhiteSpace(newName)) return;

        bool isModified = _selected != null &&
            (_selected.Name != newName ||
             _selected.Phone != newPhone ||
             _selected.Address != newAddress);

        if (_selected != null && isModified) {
            var result = MessageBox.Show(
                "この顧客情報は変更されています。\n更新しますか？「いいえ」を選ぶと新規保存します。",
                "変更確認",
                MessageBoxButton.YesNoCancel
            );

            if (result == MessageBoxResult.Yes) {
                _selected.Name = newName;
                _selected.Phone = newPhone;
                _selected.Address = newAddress;

                using (var connection = new SQLiteConnection(App.databasePath)) {
                    connection.CreateTable<Customer>();
                    connection.Update(_selected);
                }
            } else if (result == MessageBoxResult.No) {
                var newCustomer = new Customer() {
                    Name = newName,
                    Phone = newPhone,
                    Address = newAddress
                };

                using (var connection = new SQLiteConnection(App.databasePath)) {
                    connection.CreateTable<Customer>();
                    connection.Insert(newCustomer);
                }
            } else {
                return;
            }
        } else {
            var person = new Customer() {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                Picture = SelectedPictureBytes,
            };

            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>();
                connection.Insert(person);
            }
        }

        //データ取得
        ReadDatabase();
        CustomerList.ItemsSource = _customer;

        //画面クリア
        ClearScreen();
        _selected = null;
    }

    //フィルタリング
    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {
        var filterList = _customer.Where(p => p.Name.Contains(SearchTextBox.Text));

        CustomerList.ItemsSource = filterList;
    }

    //右クリック削除
    private void DeleteCustomer_Click(object sender, RoutedEventArgs e) {
        var item = CustomerList.SelectedItem as Customer;
        if (item == null) {
            return;
        }

        var result = MessageBox.Show($"{item.Name} を削除しますか？", "確認", MessageBoxButton.YesNo);
        if (result != MessageBoxResult.Yes) return;

        //データベース接続
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Delete(item);    //データベースから選択されているレコードの削除
            ReadDatabase();
            CustomerList.ItemsSource = _customer;
        }
        ClearScreen();
    }

    //画面クリア
    private void ClearScreen() {
        NameTextBox.Text = "";
        PhoneTextBox.Text = "";
        AddressTextBox.Text = "";
        Picture.Source = null;
    }

    //画像ファイルを開く
    private void OpenFile_Click(object sender, RoutedEventArgs e) {
        var dialog = new OpenFileDialog();
        dialog.Filter = "画像ファイル (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

        if (dialog.ShowDialog() == true) {
            // 画像を表示
            Picture.Source = new BitmapImage(new Uri(dialog.FileName));

            // 画像を byte[] に変換して保存用に保持
            SelectedPictureBytes = File.ReadAllBytes(dialog.FileName);
        }
    }

    //画像を削除
    private void DeleteImage_Click(object sender, RoutedEventArgs e) {
        Picture.Source = null;
    }
}