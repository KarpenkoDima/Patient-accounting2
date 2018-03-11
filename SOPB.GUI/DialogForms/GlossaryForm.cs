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
using NLog;

namespace SOPB.GUI.DialogForms
{
    public partial class GlossaryForm : Form
    {
        private BindingSource _bindingGlossary;
        public GlossaryForm(string nameGlossary, BindingSource bindingGlossary)
        {
            InitializeComponent();
            _bindingGlossary = new BindingSource(bindingGlossary.DataSource, bindingGlossary.DataMember);
            this.Text += @" " + nameGlossary;
            glossasryDataGridView.DataSource = _bindingGlossary;
            glossasryDataGridView.Columns[0].Visible = false;
            bindingNavigator1.BindingSource = _bindingGlossary;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            _bindingGlossary.EndEdit();
            try
            {
                CustomerService service = new CustomerService();
                service.SaveGlossary("Land");
                this.Close();
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Abort;
                this.Close();
            }

        }
       
        private void sveToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                _bindingGlossary.EndEdit();
                CustomerService service = new CustomerService();
                service.SaveGlossary("Land");
            }
            catch (Exception exception)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Warn(exception.Message);
                MessageBox.Show("Произошла ошибка. Приложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Abort;
                this.Close();
            }
        }
    }
}
