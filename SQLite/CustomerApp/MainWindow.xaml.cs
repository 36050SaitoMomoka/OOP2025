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

namespace CustomerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private List<Customer> _customer = new List<Customer>();

    public MainWindow() {
        InitializeComponent();
        ReadDatabase();
        CustomerList.ItemsSource = _customer;
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
        var selected = CustomerList.SelectedItem as Customer;

        if (selected == null) return;

        NameTextBox.Text = selected.Name;
        PhoneTextBox.Text = selected.Phone;
        AddressTextBox.Text = selected.Address;
    }

    //保存ボタン
    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        var person = new Customer() {
            Name = NameTextBox.Text,
            Phone = PhoneTextBox.Text,
            Address = AddressTextBox.Text,
        };

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Insert(person);
        }

        //データ取得
        ReadDatabase();
        CustomerList.ItemsSource = _customer;

        //画面クリア
        ClearScreen();
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

            ReadDatabase();
            CustomerList.ItemsSource = _customer;
        }
    }

    //画面クリア
    private void ClearScreen() {
        NameTextBox.Text = "";
        PhoneTextBox.Text = "";
        AddressTextBox.Text = "";
        Picture.Source = null;
    }
}