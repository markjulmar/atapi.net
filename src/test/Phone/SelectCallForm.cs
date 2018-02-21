using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JulMar.Atapi;

namespace Phone
{
    public partial class SelectCallForm : Form
    {
        ITapiLine _line;
        TapiCall _call;

        public SelectCallForm(ITapiLine line)
        {
            _line = line;
            InitializeComponent();
        }

        public TapiCall SelectedCall
        {
            get { return _call;  }
        }

        private void SelectCallForm_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = _line.GetCalls();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _call = listBox1.SelectedItem as TapiCall;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}