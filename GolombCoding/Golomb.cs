using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolombCoding
{
    class Golomb
    {
        private decimal _number;
        private decimal _range;

        public Golomb()
        {

        }
        public Golomb(decimal number, decimal range)
        {
            _number = number;
            _range = range;
        }
        public Golomb(decimal range)
        {
            _range = range;
        }
        public string Encode()
        {
            string result = "";
            string tmpResult = "";

            var q = Math.Floor(_number / _range);     // Quotient
            decimal r = _number % _range;     // Reminder

            // PREFIKS
            for (int i = 0; i < q; i++)
                result += "1";
            result += "0";

            // SUFIKS
            int intRange = Convert.ToInt32(_range);
            int intReminder = Convert.ToInt32(r);
            
            if (intRange != 0 && ((intRange & (intRange - 1)) == 0)) // jeśli M jest potęgą 2
            {
                int p = 1, p2 = 2;
                while (p2 < intRange)
                {
                    p2 = p2 * 2;
                    p++;
                }

                tmpResult = Convert.ToString(intReminder, 2);

                int x = p - tmpResult.Length;
                if(x != 0)
                    tmpResult = tmpResult.PadLeft(p, '0');
            }
            else
            {
                double doubleReminder = Convert.ToDouble(r);
                double doubleRange = Convert.ToDouble(_range);
                double b = Math.Ceiling(Math.Log(doubleRange, 2));

                if (doubleReminder < (Math.Pow(2, b) - doubleRange))
                {
                    tmpResult = Convert.ToString(intReminder, 2);
                    double x = b - 1 - tmpResult.Length;
                    int w = 0;
                    if (b != 0)
                        w = Convert.ToInt32(x) + tmpResult.Length;

                    tmpResult = tmpResult.PadLeft(Convert.ToInt32(w), '0');
                }
                    
                else
                {
                    int pow = Convert.ToInt32(Math.Pow(2, b));
                    int newResult = intReminder + pow - intRange;

                    tmpResult = Convert.ToString(newResult, 2);

                    double x = b - tmpResult.Length;
                    int w = 0;
                    if (b != 0)
                        w = Convert.ToInt32(x) + tmpResult.Length;

                    tmpResult = tmpResult.PadLeft(w, '0');
                }
            }
            result += tmpResult;
            return result;
        }

        public void Encode2(decimal number, decimal range)
        {
            string result = "";
            string tmpResult = "";
            var q = Math.Floor(number / range);     // Quotient
            decimal r = number % range;     // Reminder

            // PREFIKS

            for (int i = 0; i < q; i++)
                result += "1";
            result += "0";

            // SUFIKS
            int intRange = Convert.ToInt32(_range);
            int intReminder = Convert.ToInt32(r);

            if (intRange != 0 && ((intRange & (intRange - 1)) == 0)) // jeśli M jest potęgą 2
            {

                tmpResult = Convert.ToString(intReminder, 2);
            }
            else
            {
                double doubleReminder = Convert.ToDouble(r);
                double doubleRange = Convert.ToDouble(_range);
                double b = Math.Ceiling(Math.Log(doubleRange, 2));

                if (doubleReminder < (Math.Pow(2, b) - doubleRange))
                {
                    tmpResult = Convert.ToString(intReminder, 2);
                }

                else
                {
                    int pow = Convert.ToInt32(Math.Pow(2, b));
                    int newResult = intReminder + pow - intRange;

                    tmpResult = Convert.ToString(newResult, 2);
                }
            }
            result += tmpResult;

            // return result; 
        }

        public string Encode3(decimal number)
        {
            _number = number;
            string result = "";
            string tmpResult = "";

            var q = Math.Floor(_number / _range);     // Quotient
            decimal r = _number % _range;     // Reminder

            // PREFIKS
            for (int i = 0; i < q; i++)
                result += "1";
            result += "0";

            // SUFIKS
            int intRange = Convert.ToInt32(_range);
            int intReminder = Convert.ToInt32(r);

            if (intRange != 0 && ((intRange & (intRange - 1)) == 0)) // jeśli M jest potęgą 2
            {
                int p = 1, p2 = 2;
                while (p2 < intRange)
                {
                    p2 = p2 * 2;
                    p++;
                }

                tmpResult = Convert.ToString(intReminder, 2);

                int x = p - tmpResult.Length;
                if (x != 0)
                    tmpResult = tmpResult.PadLeft(p, '0');
            }
            else
            {
                double doubleReminder = Convert.ToDouble(r);
                double doubleRange = Convert.ToDouble(_range);
                double b = Math.Ceiling(Math.Log(doubleRange, 2));

                if (doubleReminder < (Math.Pow(2, b) - doubleRange))
                {
                    tmpResult = Convert.ToString(intReminder, 2);
                    double x = b - 1 - tmpResult.Length;
                    int w = 0;
                    if (b != 0)
                        w = Convert.ToInt32(x) + tmpResult.Length;

                    tmpResult = tmpResult.PadLeft(Convert.ToInt32(w), '0');
                }

                else
                {
                    int pow = Convert.ToInt32(Math.Pow(2, b));
                    int newResult = intReminder + pow - intRange;

                    tmpResult = Convert.ToString(newResult, 2);

                    double x = b - tmpResult.Length;
                    int w = 0;
                    if (b != 0)
                        w = Convert.ToInt32(x) + tmpResult.Length;

                    tmpResult = tmpResult.PadLeft(w, '0');
                }
            }
            result += tmpResult;
            return result;
        }

        public decimal Decode(string code, decimal range)
        {
            _number = 0;

            // PREFIKS
            char[] codeTab = code.ToCharArray();
            int i = 0;           
            while (codeTab[i] != '0')
            {
                _number += range;
                i++;
            }

            // SUFIKS
            code = code.Remove(0,i+1);

            int intRange = Convert.ToInt32(range);
            decimal tmpResult = 0;
            if (intRange != 0 && ((intRange & (intRange - 1)) == 0)) // jeśli M jest potęgą 2
            {
                tmpResult = Convert.ToInt32(code, 2);
            }
            else
            {
                double doubleRange = Convert.ToDouble(range);
                double b = Math.Ceiling(Math.Log(doubleRange, 2));

                if (code.Length == b)
                {
                    tmpResult = Convert.ToInt32(code, 2);
                    tmpResult = tmpResult - Convert.ToDecimal(Math.Pow(2, b)) + range;
                }
                if (code.Length == b - 1)
                {
                    tmpResult = Convert.ToInt32(code, 2);
                }
                
            }
            
            _number = _number + tmpResult;

            return _number;
        }
    }
}
