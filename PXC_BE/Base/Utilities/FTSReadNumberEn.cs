#region

using System;
using System.Globalization;

#endregion

namespace FTS.Base.Utilities {
    public static class FTSReadNumberEn {
        public static string changeCurrencyToWords(this double value, string currencyid) {
            string decimals = "";
            string input = Functions.ConvertToString(Math.Round(value, 2),".",",",2).Replace(",",string.Empty);

            if (input.Contains(".")) {
                decimals = input.Substring(input.IndexOf(".") + 1).PadRight(2,'0');
                // remove decimal part from input
                input = input.Remove(input.IndexOf("."));
            }

            // Convert input into words. save it into strWords
            string strWords = GetWords(input);
            if (currencyid == "USD") {
                strWords += " dollars";
            } else {
                if (currencyid == "VND") {
                    strWords += " dong";
                } else {
                    strWords += " " + currencyid;
                }
            }
            

            if (decimals.Length > 0) {
                // if there is any decimal part convert it to words and add it to strWords.
                strWords += " and " + GetWords(decimals) + " Cents";
            }

            if (strWords.Length > 0) {
                strWords = strWords.Substring(0, 1).ToUpper() + strWords.Substring(1, strWords.Length - 1);
            }
            return strWords;
        }

        private static string GetWords(string input) {
            // these are seperators for each 3 digit in numbers. you can add more if you want convert beigger numbers.
            string[] seperators = {"", " thousand ", " million ", " billion "};

            // Counter is indexer for seperators. each 3 digit converted this will count.
            int i = 0;

            string strWords = "";

            while (input.Length > 0) {
                // get the 3 last numbers from input and store it. if there is not 3 numbers just use take it.
                string _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
                // remove the 3 last digits from input. if there is not 3 numbers just remove it.
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

                int no = int.Parse(_3digits);
                // Convert 3 digit number into words.
                _3digits = GetWord(no);

                // apply the seperator.
                _3digits += seperators[i];
                // since we are getting numbers from right to left then we must append resault to strWords like this.
                strWords = _3digits + strWords;

                // 3 digits converted. count and go for next 3 digits
                i++;
            }

            return strWords;
        }

        // your method just to convert 3digit number into words.
        private static string GetWord(int no) {
            string[] Ones = {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven",
                "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "minteen"
            };

            string[] Tens = {"ten", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninty"};

            string word = "";

            if (no > 99 && no < 1000) {
                int i = no / 100;
                word = word + Ones[i - 1] + " hundred ";
                no = no % 100;
            }

            if (no > 19 && no < 100) {
                int i = no / 10;
                word = word + Tens[i - 1] + " ";
                no = no % 10;
            }

            if (no > 0 && no < 20) {
                word = word + Ones[no - 1];
            }

            return word;
        }
    }
}