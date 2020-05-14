using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace GolombCoding.Views
{
    /// <summary>
    /// Interaction logic for Statistics2.xaml
    /// </summary>
    public partial class Statistics2 : UserControl
    {
        public Statistics2()
        {
            InitializeComponent();
            LengthStatistics(4, 1, 10);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal x = Convert.ToDecimal(xTxt.Text);
            int y1 = Convert.ToInt32(y1Txt.Text);
            int y2 = Convert.ToInt32(y2Txt.Text);

            LengthStatistics(x, y1, y2);
        }

        public void LengthStatistics(decimal x, int y1, int y2)
        {
            ObservableCollection<KeyValuePair<string, int>> lengthCoding = new ObservableCollection<KeyValuePair<string, int>>();

            for (int i = y1; i <= y2; i++)
            {
                Golomb g = new Golomb(x, i);
                string result = g.Encode();
                
               lengthCoding.Add(new KeyValuePair<string, int>(i.ToString(), result.Length));
            }

            ShowChart(lengthCoding);
        }

        private void ShowChart(ObservableCollection<KeyValuePair<string, int>> fastCoding)
        {
            myChart.DataContext = fastCoding;
        }
    }
}
