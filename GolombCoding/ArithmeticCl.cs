using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolombCoding
{
    class ArithmeticCl
    {
        private List<Tuple<byte, double, double>> _list;
        private int _len;
        private byte[] bytes;
        public ArithmeticCl(byte[] bytes)
        {
            this.bytes = bytes;
            double len = bytes.Length;
            Dictionary<byte, double> dict = bytes.GroupBy(c => c).OrderBy(c => c.Key).ToDictionary(grp => grp.Key, grp => Convert.ToDouble(grp.Count()) / len);
            var probabilities = dict.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            _list = new List<Tuple<byte, double, double>>();

            double dolny = 0, gorny = probabilities.Values.ElementAt(0);
            for (int i = 0; i < probabilities.Count; i++)
            {

                _list.Add(new Tuple<byte, double, double>(probabilities.Keys.ElementAt(i), dolny, gorny));

                dolny = gorny;

                if (i == probabilities.Count - 1)
                    gorny = 1;
                else
                    gorny += probabilities.Values.ElementAt(i + 1);
            }

            _len = bytes.Length;
        }

          public List<float> Encode()
          {
              List<float> wyniki = new List<float>();

              float D = 0, G = 1, DX = 0, GX = 0, result = 0, dtmp = 0, gtmp = 1;
              float range;

              int l = 0;

              for (int k = 0; k < bytes.Length-8; k++)
              { 
                  var tmparray = bytes.Skip(l).Take(8).ToArray();

                  for (int i = 0; i < tmparray.Length; i++)
                  {
                      byte b = tmparray[i];

                      for (int j = 0; j < _list.Count; j++)
                      {
                          if (_list[j].Item1 == b)
                          {
                              DX = (float)_list[j].Item2;
                              GX = (float)_list[j].Item3;
                              break;
                          }
                      }
                      range = gtmp - dtmp;

                      D = dtmp + range * DX;
                      G = dtmp + range * GX;

                      result = D + (G - D) / 2;

                      dtmp = D;
                      gtmp = G;
                  }
                  wyniki.Add(result);
                  D = 0; G = 1; DX = 0; GX = 0; result = 0; dtmp = 0; gtmp = 1; range = 0;
                  l += 8;
              }

              return wyniki;
          }

        

        public string Decode(double result, bool last)
        {
            string wynik = "";
            char tmp;
            double K = result, D0, G0;
            int ln = _len % 8;

            if (last == false)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < _list.Count; j++)
                    {
                        if (K <= _list[j].Item3)
                        {
                            tmp = (char)_list[j].Item1;

                            D0 = _list[j].Item2;
                            G0 = _list[j].Item3;

                            K = (K - D0) / (G0 - D0);

                            wynik += tmp;
                            break;
                        }
                    }
                }
            }
            else
            {
                if(ln == 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < _list.Count; j++)
                        {
                            if (K <= _list[j].Item3)
                            {
                                tmp = (char)_list[j].Item1;

                                D0 = _list[j].Item2;
                                G0 = _list[j].Item3;

                                K = (K - D0) / (G0 - D0);

                                wynik += tmp;
                                break;
                            }
                        }
                    }
                }
                else
                {

                    for (int i = 0; i < ln; i++)
                    {
                        for (int j = 0; j < _list.Count; j++)
                        {
                            if (K <= _list[j].Item3)
                            {
                                tmp = (char)_list[j].Item1;

                                D0 = _list[j].Item2;
                                G0 = _list[j].Item3;

                                K = (K - D0) / (G0 - D0);

                                wynik += tmp;
                                break;
                            }
                        }
                    }
                }
                
            }
            

            return wynik;
        }
    }
}
