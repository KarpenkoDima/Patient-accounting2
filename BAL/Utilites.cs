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

        internal static class QueryCriteria
        {
            public  const string LastName = "LastName";
            public  const string FirstName = "FirstName";
            public  const string Bithday = "Birthday";
            public  const string Address = "Address";
            public  const string GetAll = "GetAll";
            public const string Glossary = "Glossary";
            public const string GetGlossaries = "GetGlossaries";
            public const string ID = "ID";

            // Or you could initialize in static constructor
            static QueryCriteria()
            {
                //row = string.Format("String{0}", 4);
            }
        }
    }
}
