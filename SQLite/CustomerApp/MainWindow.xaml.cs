using Microsoft.Win32;
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
    public MainWindow() {
        InitializeComponent();
    }

    //顧客一覧
    private void CustomerList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

    }

    //実行ボタン
    private void ExectionButton_Click(object sender, RoutedEventArgs e) {
    }
}