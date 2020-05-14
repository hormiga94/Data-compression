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
using GolombCoding.Codes;

namespace GolombCoding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CodingViewModel();
        }

        private void CodingClicked(object sender, RoutedEventArgs e)
        {
            DataContext = new CodingViewModel();
        }

        private void StatisticsClicked(object sender, RoutedEventArgs e)
        {
            DataContext = new StatisticsViewModel();
        }

        private void Statistics2Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new Statistics2ViewModel();
        }

        private void LZClicked(object sender, RoutedEventArgs e)
        {
            DataContext = new CodingLZ78Model();

        }

        private void AritmethicClicked(object sender, RoutedEventArgs e)
        {
            DataContext = new CodingArithmeticModel();

        }

    }
}
