using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Arithmetic.xaml
    /// </summary>
    public partial class Arithmetic : UserControl
    {
        ArithmeticCl globalArithmetic;

        public Arithmetic()
        {
            InitializeComponent();

            
        }

        private void ArtBtnEn_Click(object sender, RoutedEventArgs e)
        {
            EnTxtRes.Text = "";

            string txt = EnTxtEn.Text;
            byte[] bytes = Encoding.ASCII.GetBytes(txt);

            ArithmeticCl ar = new ArithmeticCl(bytes);
            List<float> result = ar.Encode();

            foreach (var item in result)
            {
                EnTxtRes.Text += item.ToString() + "\n";
            }

            
        }

        private void ArtBtnDe_Click(object sender, RoutedEventArgs e)
        {
            string txt = EnTxtEn.Text;
            byte[] bytes = Encoding.ASCII.GetBytes(txt);
            ArithmeticCl ar = new ArithmeticCl(bytes);

            double res = Convert.ToDouble(DeTxtEn.Text);

            string w = ar.Decode(res, false);

            DeTxtRes.Text = w;
        }

        private void FileBtnEn_Click(object sender, RoutedEventArgs e)
        {
            byte[] file;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"K:\PROJEKTY VS\GolombCoding\GolombCoding\bin";
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.Filter = "Text documents (.txt)|*.txt";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == true)
                file = File.ReadAllBytes(openFileDialog.FileName);
            else
                file = new byte[] { };

            ArithmeticCl ar = new ArithmeticCl(file);
            globalArithmetic = new ArithmeticCl(file);
            List<float> result = ar.Encode();

            TextWriter tw = new StreamWriter(@"..\ArtEncode.txt");
            int i = 0;
            while (result[i] != 0)
            {
                string s = result[i].ToString().Substring(2);
                tw.Write(s);
                i++;
            }
            
            tw.Close();
        }

        private void FileBtnDe_Click(object sender, RoutedEventArgs e)
        {
            TextReader tr = new StreamReader(@"..\ArtEncode.txt");
            string resultst = tr.ReadToEnd();
            double[] result = new double[resultst.Length/7];
            int n = 0;
            for (int i = 0; i < resultst.Length; i+=7)
            {
                string s = "0," + resultst.Substring(i, 7);

                result[n] = Convert.ToDouble(s);
                n++;
            }

            tr.Close();

           // byte[] bytes = File.ReadAllBytes(@"..\test1.txt");
           

            TextWriter tw = new StreamWriter(@"..\ArtDecode.txt");

            for (int i = 0; i <= result.Length-2; i++)
            {
                string txt = globalArithmetic.Decode(result[i], false);
                tw.Write(txt);
            }

            string txt1 = globalArithmetic.Decode(result[result.Length - 1], true);
            tw.Write(txt1);


            tw.Close();
        }
    }
}
