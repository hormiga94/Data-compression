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
using System.Drawing;
using System.IO;

namespace GolombCoding.Views
{
    /// <summary>
    /// Interaction logic for Coding.xaml
    /// </summary>
    public partial class Coding : UserControl
    {
        public Coding()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            encodeResultTxt.Text = "Kod: ";

            Golomb g = new Golomb(Convert.ToDecimal(numberTxt.Text), Convert.ToDecimal(rangeTxt.Text));
            string result = g.Encode();
            encodeResultTxt.Text += result;

        }

        private void ButtonDec_Click(object sender, RoutedEventArgs e)
        {
            decodeResultTxt.Text = "Liczba: ";
            Golomb g = new Golomb();
            decimal result = g.Decode(numberDecTxt.Text, Convert.ToDecimal(rangeDecTxt.Text));
            decodeResultTxt.Text += result.ToString();

        }
    }
}
