using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BAL
{
    internal class Utilites
    {
        public static bool IsAlphabetic(string str)
        {
            Regex r = new Regex("^[А-ЯЁІЇЄа-яёіїє'\\-\\s]*$");
            return r.IsMatch(str);
        }

        public static bool ValidateTextInputForName(string name)
        {
            return IsAlphabetic(name);
        }
        public static bool ValidateTextInputNotName(string name)
        {
            return !IsAlphabetic(name);
        }
    }
}
