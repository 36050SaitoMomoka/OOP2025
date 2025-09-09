using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        private MyColor myColor;
        public MainWindow() {
            InitializeComponent();
            colorComboBox.ItemsSource = GetColorList();
        }

        //すべてのスライダーから呼ばれるイベントハンドラ
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            var color = Color.FromRgb(
                (byte)rSlider.Value,
                (byte)gSlider.Value,
                (byte)bSlider.Value
            );

            var myColor = new MyColor {
                Color = Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value),
                Name = "Custom Color"
            };

            colorArea.Background = new SolidColorBrush(myColor.Color);
        }

        private void stockButton_Click(object sender, RoutedEventArgs e) {
            var myColor = new MyColor {
                Color = Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value),
                Name = "Custom Color"
            };
            if (colorComboBox.SelectedItem is MyColor selectedColor) {
                Name = selectedColor.Name;
                colorStock.Items.Add(Name);
            } else {
                colorStock.Items.Add(myColor);
            }
            colorComboBox.SelectedItem = null;
        }

        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorComboBox.SelectedItem is MyColor selected) {
                colorArea.Background = new SolidColorBrush(selected.Color);

                rSlider.Value = selected.Color.R;
                gSlider.Value = selected.Color.G;
                bSlider.Value = selected.Color.B;
            }
        }
    }
}
