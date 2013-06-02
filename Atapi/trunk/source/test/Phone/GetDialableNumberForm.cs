using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Phone
{
    public partial class GetDialableNumberForm : Form
    {
        public string Number;
        public GetDialableNumberForm(string type)
        {
            InitializeComponent();
            button1.Text = type;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Number = textBox1.Text;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}