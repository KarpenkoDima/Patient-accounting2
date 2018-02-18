using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAL.ORM;

namespace SOPB.GUI.DialogForms
{
    public partial class EnterForm : Form
    {
        public EnterForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (LogInApplication.LogIn(textBoxLogin.Text, textBoxPassword.Text))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!","Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearTextBox();
                textBoxLogin.BackColor = Color.Yellow;
                textBoxPassword.BackColor = Color.Yellow;
                
                this.DialogResult = DialogResult.Retry;
            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            textBoxLogin.Text = textBoxPassword.Text = string.Empty;
        }

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            textBoxLogin.BackColor = textBoxPassword.BackColor = SystemColors.Window;
        }
    }
}
