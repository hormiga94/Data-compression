using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace GolombCoding.Views
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public Statistics()
        {
            InitializeComponent();

            FastStatistics(4, 1, 10);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            decimal x = Convert.ToDecimal(xTxt.Text);
            int y1 = Convert.ToInt32(y1Txt.Text);
            int  y2 = Convert.ToInt32(y2Txt.Text);

            FastStatistics(x, y1, y2);
        }

        public void FastStatistics(decimal x, int y1, int y2)
        {
            ObservableCollection<KeyValuePair<string, double>> fastCoding = new ObservableCollection<KeyValuePair<string, double>>();

            Golomb g = new Golomb();

            Stopwatch sw = new Stopwatch();
            

            for (int i = y1-1; i <= y2; i++)
            {
                decimal z = Convert.ToDecimal(i);

                if (i == y1-1)
                {
                    sw.Restart();
                    g.Encode2(x, 1);
                    sw.Stop();
                }
                else
                {
                    sw.Restart();
                    g.Encode2(x, z);
                    sw.Stop();

                    fastCoding.Add(new KeyValuePair<string, double>(z.ToString(), sw.ElapsedTicks));
                } 
            }

            ShowChart(fastCoding);
        }

        private void ShowChart(ObservableCollection<KeyValuePair<string, double>> fastCoding)
        {
            myChart.DataContext = fastCoding;
        }
        
    }
}
 