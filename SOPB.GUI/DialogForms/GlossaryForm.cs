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
            CustomerService service = new CustomerService();
            service.SaveGlossary("Land");
            this.Close();

        }
       
        private void sveToolStripButton_Click(object sender, EventArgs e)
        {
            _bindingGlossary.EndEdit();
            CustomerService service = new CustomerService();
            service.SaveGlossary("Land");
        }
    }
}
