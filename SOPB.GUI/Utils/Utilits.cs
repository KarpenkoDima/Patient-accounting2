using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOPB.GUI.Utils
{
    class Utilits
    {
        public static bool ValidateText(MaskedTextBox masked)
        {
            object text = masked.ValidateText();
            if (text != null)
            {
                Debug.Write("Text Saccess");
                Debug.WriteLine(" " + Convert.ToDateTime(text).ToShortDateString());
                return true;
            }
            else
            {
                Debug.Write("Fail");
                Debug.WriteLine(" " + Convert.ToDateTime(text).ToShortDateString());

                return false;
            }
        }
    }
}
