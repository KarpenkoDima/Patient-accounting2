using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOPB.GUI.DialogForms
{
    public partial class FindForm : Form
    {
        public string LastName { get; private set; }
        public FindForm(string name = "Фамилия")
        {
            InitializeComponent();
            label1.Text = name;
            this.Text = "Поиск: " + name;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            LastName = textBoxLastName.Text;
            this.Close();
        }
    }
}
