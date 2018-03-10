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
            if (string.IsNullOrEmpty(textBoxLastName.Text))
            {
                this.DialogResult = MessageBox.Show(@"Вы не ввели текст для поиска! Повторить ввод?", @"Пустая Строка", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                ClearTextBox();
                if (this.DialogResult == DialogResult.No)
                {
                    LastName = String.Empty;
                    this.Close();
                }

            }
            else
            {
                 LastName = textBoxLastName.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void ClearTextBox()
        {
            textBoxLastName.Clear();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if ((DialogResult == DialogResult.Yes)) e.Cancel = true;
        }
    }
}
