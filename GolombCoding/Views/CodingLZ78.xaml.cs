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
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Drawing.Imaging;
using Microsoft.Win32;

namespace GolombCoding.Views
{
    /// <summary>
    /// Interaction logic for CodingLZ78.xaml
    /// </summary>
    public partial class CodingLZ78 : UserControl
    {
        public CodingLZ78()
        {
            InitializeComponent();
        }

        private void EncodeBT_Click(object sender, RoutedEventArgs e)
        {
            string encode = EncodeTB.Text;

            LZ78 lz = new LZ78();

            byte[] bytes = Encoding.ASCII.GetBytes(encode);

            List<List<Tuple<byte, byte>>> list = lz.Encode(bytes);

            EncodeLB.Text = "";

            foreach (var item in list)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    EncodeLB.Text += string.Format("({0}, {1}) ", item[i].Item1, item[i].Item2);
                }
               
            }

            
        }

        private void DecodeBT_Click(object sender, RoutedEventArgs e)
        {
            DecodeLB.Text = "";
            string[] decode = DecodeTB.Text.Split(' ');

            LZ78 lz = new LZ78();

            List<Tuple<byte, byte>> resultd = new List<Tuple<byte, byte>>();

            for (int i = 0; i < decode.Length - 1; i += 2)
            {
                resultd.Add(new Tuple<byte, byte>(Convert.ToByte(decode[i]), Convert.ToByte(decode[i + 1])));
            }

            List<string> decoderesult = lz.Decode(resultd);

            List<string> r = new List<string>();
            for (int i = 0; i < decoderesult.Count; i++)
            {
                string[] val = decoderesult[i].Split(' ');
                foreach (var item in val)
                    r.Add(item);
            }
            for (int i = 0; i < r.Count; i++)
            {
                if (r[i] != "")
                {
                    int n = Convert.ToInt32(r[i]);
                    DecodeLB.Text += (char)n;
                }

            }
        }

        private void FileBT_Click(object sender, RoutedEventArgs e)
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

            LZ78 lz = new LZ78();

            List<List<Tuple<byte, byte>>> result = lz.Encode(file);

            TextWriter tw = new StreamWriter(@"..\LZ78Encode.txt");

            foreach (var list in result)
            {
                if (list.Count != 0)
                {

                    byte[] b = new byte[list.Count * 2];

                    for (int i = 0; i < list.Count; i++)
                    {
                        tw.Write(list[i].Item1 + " ");
                        tw.Write(list[i].Item2 + " ");

                        b[2 * i] = list[i].Item1;
                        b[2 * i + 1] = list[i].Item2;
                    }

                    using (var stream = new FileStream(@"..\LZ78Encode.bin", FileMode.Append))
                    {
                        stream.Write(b, 0, b.Length);
                    }
                }
                else
                {
                    break;
                }
                
            }
            tw.Close();

        }

        private void DecodeFileBT_Click(object sender, RoutedEventArgs e)
        {

            byte[] byt = File.ReadAllBytes(@"..\LZ78Encode.bin");

            List<Tuple<byte, byte>> resultbin = new List<Tuple<byte, byte>>();

            

            for (int i = 0; i < byt.Length/2; i++)
            {
                resultbin.Add(new Tuple<byte, byte>(byt[2 * i], byt[2 * i + 1]));
            }


            for (int j = 0; j < resultbin.Count; j+=256)
            {
                List<Tuple<byte, byte>> resultdec = resultbin.Skip(j).Take(256).ToList();

                LZ78 lz = new LZ78();
                List<string> decoderesult = lz.Decode(resultdec);
                List<string> r = new List<string>();
                for (int i = 0; i < decoderesult.Count; i++)
                {
                    string[] val = decoderesult[i].Split(' ');
                    foreach (var item in val)
                        r.Add(item);
                }
                TextWriter ws = new StreamWriter(@"..\LZ78DecodeBin.txt", true);
                for (int i = 0; i < r.Count; i++)
                {
                    if (r[i] != "")
                    {
                        int n = Convert.ToInt32(r[i]);
                        ws.Write((char)n);
                    }

                }
                ws.Close();
            }

            

        }
    }
}
