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
    public partial class FindByBirthday : Form
    {
        public string Predicate { get; protected set; }
        public DateTime BithDay { get; private set; }

        public FindByBirthday()
        {
            InitializeComponent();
            comboBoxPreicate.SelectedIndex = 0;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DateTime bithDay;
            if (maskedTextBoxBirthOfDay.Text.Length <= 0 || !DateTime.TryParse(maskedTextBoxBirthOfDay.Text, out bithDay))
            {
                this.DialogResult = MessageBox.Show(@"Вы не ввели дату рождения! \n Повторить ввод?", @"Пустая Дата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                ClearTextBox();
                if(this.DialogResult == DialogResult.No)
                {
                    Predicate =String.Empty;
                    this.Close();
                }
                
            }
            else
            {
                this.Predicate = comboBoxPreicate.Text;
                this.BithDay = DateTime.Parse(maskedTextBoxBirthOfDay.Text);
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void ClearTextBox()
        {
            comboBoxPreicate.SelectedIndex = 0;
            maskedTextBoxBirthOfDay.Clear();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if ((DialogResult == DialogResult.Yes) ) e.Cancel = true;
        }
    }
}
