using System;
using System.Text;
using System.Windows.Forms;
using JulMar.Atapi;

namespace EnumTapi
{
    public partial class EnumTapiForm : Form
    {
        readonly TapiManager _mgr = new TapiManager("EnumTapi.Net");

        public EnumTapiForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_mgr.Initialize())
            {
                comboBox1.DataSource = _mgr.Lines;
                foreach (TapiProvider tsp in _mgr.Providers)
                {
                    ListViewItem lvi = listView1.Items.Add(string.Format("0x{0:X}", tsp.Id));
                    lvi.SubItems.Add(tsp.Name);
                }
            }
        }

        private void EnumTapiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mgr.Shutdown();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                TapiLine line = (TapiLine) comboBox1.SelectedItem;
                listBox1.DataSource = line.Addresses;

                // Fill in the capabilities textbox
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Device ID: {0}\n", line.Id);
                sb.AppendFormat("Negotiated Version: {0}.{1}", ((int)line.NegotiatedVersion & 0xffff0000) >> 16, (int)line.NegotiatedVersion & 0xffff);
                sb.Append("\n");
                sb.Append(line.Capabilities.ToString("f"));
                sb.Replace("\n", Environment.NewLine);
                richTextBox1.Text = sb.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                TapiLine line = (TapiLine)comboBox1.SelectedItem;
                line.Config(this.Handle, null);
            }
        }
    }
}