using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOPB.GUI.DialogForms
{
    public partial class ExportTo : Form
    {
        private BindingSource binding;
        public ExportTo(BindingSource bindingSource)
        {
            InitializeComponent();
            binding = bindingSource;
            GetColumns();
        }

        public void GetColumns()
        {
            var view = binding.List as DataView;
            var columns = view[0].Row.Table.Columns;
            foreach (DataColumn column in columns)
            {
                checkedListBox1.Items.Add(column.Caption);
            }

            CheckedListBox addr = new CheckedListBox();
            Point pt = checkedListBox1.Location;
            addr.Location = new Point(pt.X + checkedListBox1.Width, pt.Y);
           this.Controls.Add(addr);
            columns = view[0].Row.GetChildRows("FK_Address_Customer_CustomerID")[0].Table.Columns;
            foreach (DataColumn column in columns)
            {
                addr.Items.Add(column.Caption);
            }

    
        }
    }
}
