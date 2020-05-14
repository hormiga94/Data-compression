using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolombCoding
{
    class LZ78
    {
        public LZ78()
        {

        }

        public List<List<Tuple<byte, byte>>> Encode(byte[] bytes)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            List<Tuple<byte, byte>> result = new List<Tuple<byte, byte>>();

            List<List<Tuple<byte, byte>>> resultList = new List<List<Tuple<byte, byte>>>();

            byte l=255;
            string w = "";
            int i = 0, n = 0;

            for (int j = 0; j < bytes.Length; j++)
            {
                while (n <= l && i < bytes.Length)
                {
                    if (!dictionary.ContainsValue(w + bytes[i].ToString()))
                    {
                        if (w == "")
                        {
                            result.Add(new Tuple<byte, byte>(0, bytes[i]));

                            dictionary.Add(n, bytes[i].ToString());
                        }
                        else
                        {
                            int index = dictionary.Values.ToList().IndexOf(w);
                            result.Add(new Tuple<byte, byte>((byte)dictionary.Keys.ElementAt(index), bytes[i]));
                            dictionary.Add(n, w + bytes[i].ToString());
                        }

                        n ++;
                        w = "";
                    }
                    else
                    {
                        w += bytes[i].ToString();
                    }

                    i++;
                }
                resultList.Add(result);
                result = new List<Tuple<byte, byte>>();
                dictionary = new Dictionary<int, string>();
                n = 0; l = 255; w = "";
            }

            
            return resultList;
        }

        public List<string> Decode(List<Tuple<byte, byte>> list)
        {
            Dictionary<double, string> dictionary = new Dictionary<double, string>();
            double n = 1;

            List<string> result = new List<string>();

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Item1 == 0)
                {
                    dictionary.Add(n, list[i].Item2.ToString() + " ");
                    n++;
                    result.Add(list[i].Item2.ToString());
                }
                else
                {
                    byte m = Convert.ToByte(list[i].Item1+1);
                    string w = dictionary[m];
                    result.Add(w);
                    result.Add(list[i].Item2.ToString());
                    dictionary.Add(n, w + " " + list[i].Item2);
                    n++;
                }

            }

            return result;
        }
    }
}
