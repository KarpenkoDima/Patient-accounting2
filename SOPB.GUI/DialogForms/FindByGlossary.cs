using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SOPB.GUI.Properties;

namespace SOPB.GUI.DialogForms
{
    public partial class FindByGlossary : Form
    {
        private BindingSource _glossary;
        public int _ID
        {
            get;private set;
        }
        public FindByGlossary(BindingSource glossary, string name)
        {
            InitializeComponent();
            _glossary = glossary;
            comboBoxGlossary.DataSource = _glossary;

            comboBoxGlossary.ValueMember = name + "ID";
            comboBoxGlossary.DataBindings.Clear();
            comboBoxGlossary.DataBindings.Add("SelectedValue", _glossary, name +"ID");
            comboBoxGlossary.DisplayMember = "Name";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            _ID = int.Parse(comboBoxGlossary.SelectedValue.ToString());
        }
    }
}
