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
            //スライダーの変更にコンボボックスを合わせる
            var colorList = colorComboBox.ItemsSource as IEnumerable<MyColor>;
            if (colorList != null) {
                colorComboBox.SelectedIndex = colorList.ToList().FindIndex(c => c.Color.Equals(myColor.Color));
            }

            //colorComboBox.SelectedIndex = ((MyColor[])DataContext).ToList().FindIndex(c => c.Color.Equals(myColor.Color));

            colorArea.Background = new SolidColorBrush(myColor.Color);
        }

        //ストック（リスト）に追加
        private void stockButton_Click(object sender, RoutedEventArgs e) {
            var myColor = new MyColor {
                Color = Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value),
                Name = "Custom Color"
            };

            //登録済みか確認
            if (!colorStock.Items.Contains(myColor)) {
                if (colorComboBox.SelectedItem is MyColor selectedColor) {
                    Name = selectedColor.Name;
                    colorStock.Items.Add(Name);
                } else {
                    colorStock.Items.Add(myColor);
                }
                colorComboBox.SelectedItem = null;
            } else {
                MessageBox.Show("既に登録済みです！", "ColorChecker", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //コンボボックスの色一覧
        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }

        //コンボボックスから選択
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorComboBox.SelectedItem is MyColor selected) {
                colorArea.Background = new SolidColorBrush(selected.Color);

                rSlider.Value = selected.Color.R;
                gSlider.Value = selected.Color.G;
                bSlider.Value = selected.Color.B;
            }
        }

        //リスト右クリックで削除
        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            if (colorStock.SelectedItem != null) {
                colorStock.Items.Remove(colorStock.SelectedItem);
            }
        }

        //リストボックスから選択
        private void colorStock_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (colorStock.SelectedItem is MyColor selectedColor) {
                rSlider.Value = selectedColor.Color.R;
                gSlider.Value = selectedColor.Color.G;
                bSlider.Value = selectedColor.Color.B;

                Color resetColor = selectedColor.Color;
                resetColor.A = 255;

                colorArea.Background = new SolidColorBrush(selectedColor.Color);
            } else if (colorStock.SelectedItem is string selectedColorName) {
                foreach (var item in GetColorList()) {
                    if (item.Name == selectedColorName) {
                        rSlider.Value = item.Color.R;
                        gSlider.Value = item.Color.G;
                        bSlider.Value = item.Color.B;

                        Color resetColor = item.Color;
                        resetColor.A = 255;

                        colorArea.Background = new SolidColorBrush(item.Color);
                    }
                }
            }
        }
    }
}
