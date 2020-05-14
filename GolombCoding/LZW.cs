using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolombCoding
{
    class LZW
    {
        private string _encodestr;

        private List<int> _decodelist;

        
            

        public LZW()
        {
            
        }
        public LZW(string encode)
        {
            _encodestr = encode;

            
        }
        public LZW(List<int> decodelist)
        {
            _decodelist = decodelist;
        }
        public List<int> Encode()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            for (int i = 0; i < 96; i++)
                dictionary.Add(((char)(i + 32)).ToString(), i);

            List<int> result = new List<int>();
            //int i = 0;
            string c = string.Empty;

            foreach (char w in _encodestr)
            {
                string cw = c + w;

                if (dictionary.ContainsKey(cw))
                {
                    c = cw;

                    if(_encodestr.Length == 1)
                        result.Add(dictionary[c]);

                    if (_encodestr.IndexOf(w) == _encodestr.Length - 1)
                        result.Add(dictionary[cw]);
                }
                    
                else
                {
                    dictionary.Add(cw, dictionary.Count);

                    result.Add(dictionary[c]);

                    c = w.ToString();
                }   
            }


            return result;
        }
        
        public string Decode(List<int> decodelist)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 96; i++)
                dictionary.Add(i, ((char)(i + 32)).ToString());

            dictionary.Add(dictionary.Count, ((char)243).ToString());

            string w = dictionary[decodelist[0]];
            decodelist.RemoveAt(0);

            StringBuilder decompressed = new StringBuilder(w);

            /*for (int i = 0; i < decodelist.Count; i++)
            {
                string entry = null;
                if (dictionary.ContainsKey(decodelist[i]))
                    entry = dictionary[decodelist[i]];
                else if (decodelist[i] == dictionary.Count)
                    entry = w + w[0];

                decompressed.Append(entry);

                // new sequence; add it to the dictionary
                dictionary.Add(dictionary.Count, w + entry[0]);

                w = entry;
            }*/

            foreach (int k in decodelist)
            {
                string entry = null;
                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                else if (k == dictionary.Count)
                    entry = w + w[0];

                decompressed.Append(entry);

                // new sequence; add it to the dictionary
                dictionary.Add(dictionary.Count, w + entry[0]);

                w = entry;
            }

            return decompressed.ToString();
        }

        /* public List<int> Encode2()
         {
             Dictionary<string, int> dictionary = new Dictionary<string, int>();

             for (int i = 0; i < 96; i++)
                 dictionary.Add(((char)(i + 32)).ToString(), i);

             // POLSKIE ZNAKI
             #region Add polish symbols to dictionary
             dictionary.Add(((char)177).ToString(), dictionary.Count);
             dictionary.Add(((char)230).ToString(), dictionary.Count);
             dictionary.Add(((char)234).ToString(), dictionary.Count);
             dictionary.Add(((char)179).ToString(), dictionary.Count);
             dictionary.Add(((char)241).ToString(), dictionary.Count);
             dictionary.Add(((char)243).ToString(), dictionary.Count);
             dictionary.Add(((char)182).ToString(), dictionary.Count);
             dictionary.Add(((char)188).ToString(), dictionary.Count);
             dictionary.Add(((char)191).ToString(), dictionary.Count);
             dictionary.Add(((char)161).ToString(), dictionary.Count);
             dictionary.Add(((char)198).ToString(), dictionary.Count);
             dictionary.Add(((char)202).ToString(), dictionary.Count);
             dictionary.Add(((char)163).ToString(), dictionary.Count);
             dictionary.Add(((char)209).ToString(), dictionary.Count);
             dictionary.Add(((char)211).ToString(), dictionary.Count);
             dictionary.Add("\n", dictionary.Count);
             #endregion

             List<int> result = new List<int>();
             //int i = 0;
             string c = _encodestr[0].ToString();
             string series= "";

             bool first = true;

             foreach (char s in _encodestr)
             {
                 series += s.ToString();

                 if (!dictionary.ContainsKey(s.ToString()))
                    dictionary.Add(s.ToString(), dictionary.Count);

                 if (first == true)
                 {
                     result.Add(dictionary[s.ToString()]);
                     first = false;
                 }

                 if (dictionary.ContainsKey(series))
                     c += s;
                 else
                 {
                     dictionary.Add(series, dictionary.Count);
                     result.Add(dictionary[s.ToString()]);

                     c = s.ToString();
                 }
             }

             return result;
         }*/

        public List<int> Encode2()
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            for (int i = 0; i < 96; i++)
                dictionary.Add(((char)(i + 32)).ToString(), i);
            dictionary.Add(((char)13).ToString(), dictionary.Count);
            // POLSKIE ZNAKI
            #region Add polish symbols to dictionary
            /*dictionary.Add(((char)177).ToString(), dictionary.Count);
            dictionary.Add(((char)230).ToString(), dictionary.Count);
            dictionary.Add(((char)234).ToString(), dictionary.Count);
            dictionary.Add(((char)179).ToString(), dictionary.Count);
            dictionary.Add(((char)241).ToString(), dictionary.Count);
            dictionary.Add(((char)243).ToString(), dictionary.Count);
            dictionary.Add(((char)182).ToString(), dictionary.Count);
            dictionary.Add(((char)188).ToString(), dictionary.Count);
            dictionary.Add(((char)191).ToString(), dictionary.Count);
            dictionary.Add(((char)161).ToString(), dictionary.Count);
            dictionary.Add(((char)198).ToString(), dictionary.Count);
            dictionary.Add(((char)202).ToString(), dictionary.Count);
            dictionary.Add(((char)163).ToString(), dictionary.Count);
            dictionary.Add(((char)209).ToString(), dictionary.Count);
            dictionary.Add(((char)211).ToString(), dictionary.Count);
            dictionary.Add("\n", dictionary.Count);*/
            #endregion

            List<int> result = new List<int>();
            //int i = 0;
            string c = _encodestr[0].ToString();
            string series = "";

            bool first = true;

            foreach (char s in _encodestr)
            {
                series += s.ToString();

                if (!dictionary.ContainsKey(s.ToString()))
                    dictionary.Add(s.ToString(), dictionary.Count);

                if (first == true)
                {
                    result.Add(dictionary[s.ToString()]);
                    first = false;
                }

                if (dictionary.ContainsKey(series))
                    c += s;
                else
                {
                    dictionary.Add(series, dictionary.Count);
                    result.Add(dictionary[s.ToString()]);

                    c = s.ToString();
                }
            }

            return result;
        }

        public string Decode2(List<int> decodelist)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            for (int i = 0; i < 96; i++)
                dictionary.Add(i, ((char)(i + 32)).ToString());
            dictionary.Add(dictionary.Count, ((char)13).ToString());
            // POLSKIE ZNAKI
            #region Add polish symbols to dictionary
            /*dictionary.Add(dictionary.Count, ((char)177).ToString());
            dictionary.Add(dictionary.Count, ((char)230).ToString());
            dictionary.Add(dictionary.Count, ((char)234).ToString());
            dictionary.Add(dictionary.Count, ((char)179).ToString());
            dictionary.Add(dictionary.Count, ((char)241).ToString());
            dictionary.Add(dictionary.Count, ((char)243).ToString());
            dictionary.Add(dictionary.Count, ((char)182).ToString());
            dictionary.Add(dictionary.Count, ((char)188).ToString());
            dictionary.Add(dictionary.Count, ((char)191).ToString());
            dictionary.Add(dictionary.Count, ((char)161).ToString());
            dictionary.Add(dictionary.Count, ((char)198).ToString());
            dictionary.Add(dictionary.Count, ((char)202).ToString());
            dictionary.Add(dictionary.Count, ((char)163).ToString());
            dictionary.Add(dictionary.Count, ((char)209).ToString());
            dictionary.Add(dictionary.Count, ((char)211).ToString());
            dictionary.Add(dictionary.Count, "\n");*/
            #endregion

            string w = dictionary[decodelist[0]];
            decodelist.RemoveAt(0);

            StringBuilder decompressed = new StringBuilder(w);

            foreach (int k in decodelist)
            {
                string entry = null;

              //  if (!dictionary.ContainsKey(k))
                //    dictionary.Add(s.ToString(), dictionary.Count);

                if (dictionary.ContainsKey(k))
                    entry = dictionary[k];
                else if (k == dictionary.Count)
                    entry = w + w[0];

                decompressed.Append(entry);

                // new sequence; add it to the dictionary
                dictionary.Add(dictionary.Count, w + entry[0]);

                w = entry;
            }

            return decompressed.ToString();
        }
    }
}
