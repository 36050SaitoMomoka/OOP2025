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

namespace ConverterApp {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            MetricUnit.Items.Add("mm");
            MetricUnit.Items.Add("cm");
            MetricUnit.Items.Add("m");
            MetricUnit.Items.Add("km");

            ImperialUnit.Items.Add("in");
            ImperialUnit.Items.Add("ft");
            ImperialUnit.Items.Add("yd");
            ImperialUnit.Items.Add("mi");
        }

        Dictionary<string, double> metric = new Dictionary<string, double> {
            {"mm",1 },
            {"cm",10 },
            {"m",100 },
            {"km",1000 },
        };

        Dictionary<string, double> imperial = new Dictionary<string, double> {
            {"in",25.4 },
            {"ft",304.8 },
            {"yd",914.4 },
            {"mi",1609344 },
        };

        private void MetricToImperialUnit_Click(object sender, RoutedEventArgs e) {
            int selectedMetricUnit = MetricUnit.SelectedIndex;
            int selectedImperialUnit = ImperialUnit.SelectedIndex;


        }
    }
}
