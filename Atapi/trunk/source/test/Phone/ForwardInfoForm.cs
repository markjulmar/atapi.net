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
    public partial class ForwardInfoForm : Form
    {
        TapiLine _line;
        ForwardInfo[] _fwdInfo;
        TapiAddress _selAddress;

        public ForwardInfoForm(TapiLine line)
        {
            _line = line;
            InitializeComponent();
        }

        public ForwardInfo[] ForwardInfo
        {
            get { return _fwdInfo;  }
        }

        public TapiAddress SelectedAddress
        {
            get { return _selAddress; }
        }

        private void ForwardInfoForm_Load(object sender, EventArgs e)
        {
            lblLineName.Text = _line.Name;
            cbAddresses.DataSource = _line.Addresses;

            System.Collections.ArrayList al = new System.Collections.ArrayList();
            al.AddRange(Enum.GetValues(typeof(ForwardingMode)));
            cbForwardEntryTypes.Items.AddRange(al.ToArray());
            cbForwardEntryTypes.SelectedIndex = 0;
        }

        private void textDestination_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = (textDestination.Text.Length > 0);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ForwardInfo fi = new ForwardInfo((ForwardingMode)cbForwardEntryTypes.SelectedItem, textCaller.Text, 0, textDestination.Text);
            lbEntries.Items.Add(fi);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbEntries.SelectedIndex >= 0)
                lbEntries.Items.RemoveAt(lbEntries.SelectedIndex);
        }

        private void cbAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            TapiAddress addr = (TapiAddress)cbAddresses.SelectedItem;
            lbEntries.Items.Clear();
            foreach (ForwardInfo fi in addr.Status.ForwardingInformation)
                lbEntries.Items.Add(fi);
        }

        private void chkAllAddresses_CheckedChanged(object sender, EventArgs e)
        {
            cbAddresses.Enabled = !chkAllAddresses.Checked;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbEntries.Items.Count > 0)
            {
                _fwdInfo = new ForwardInfo[lbEntries.Items.Count];
                for (int i = 0; i < lbEntries.Items.Count; i++)
                    _fwdInfo[i] = (ForwardInfo)lbEntries.Items[i];
            }
            else
                _fwdInfo = new ForwardInfo[0];
            if (chkAllAddresses.Checked)
                _selAddress = null;
            else
                _selAddress = (TapiAddress) cbAddresses.SelectedItem;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lbEntries_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnDelete.Enabled = lbEntries.SelectedIndex >= 0;
        }

    }
}