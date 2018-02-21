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
    public partial class MainForm : Form
    {
        TapiManager _mgr = new TapiManager("Phone.Net");
       
        public MainForm()
        {
            InitializeComponent();
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        }

        void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception is TapiException)
                MessageBox.Show(e.Exception.Message);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_mgr.Initialize())
            {
                _lines.DataSource = _mgr.Lines;
                foreach (TapiLine line in _mgr.Lines)
                {
                    line.Changed += this.OnLineStateChanged;
                    line.NewCall += this.OnNewCall;
                    line.Ringing += this.OnRinging;
                }
            }
        }

        TapiLine CurrentLine
        {
            get 
            {
                return (TapiLine)_lines.SelectedItem;
            }    
        }

        TapiAddress CurrentAddress
        {
            get
            {
                return (TapiAddress)_addresses.SelectedItem;
            }
        }

        private void OnLineStateChanged(object sender, LineInfoChangeEventArgs e)
        {
            if (InvokeRequired == true)
            {
                this.BeginInvoke(new EventHandler<LineInfoChangeEventArgs>(this.OnLineStateChanged), new object[] { sender, e });
                return;
            }

            if (sender == CurrentLine)
                ChangeButtonStates();
        }

        private void OnNewCall(object sender, NewCallEventArgs e)
        {
            if (InvokeRequired == true)
            {
                this.BeginInvoke(new EventHandler<NewCallEventArgs>(this.OnNewCall), new object[] { sender, e });
                return;
            }

            ActiveCallForm acf = new ActiveCallForm(e.Call);
            acf.Show();
        }

        private void OnRinging(object sender, EventArgs e)
        {
            if (InvokeRequired == true)
            {
                this.BeginInvoke(new EventHandler<RingEventArgs>(this.OnRinging), new object[] { sender, e });
                return;
            }

            if (sender == CurrentLine)
            {
                _statusLabel.Text = "Ringing...";
                _timerReset.Enabled = true;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _mgr.Shutdown();
        }

        private void _btnStartStop_Click(object sender, EventArgs e)
        {
            TapiLine line = CurrentLine;
            if (line.IsOpen)
            {
                line.Close();
                ChangeButtonStates();
            }
            else
            {
                try
                {
                    line.Open(line.Capabilities.MediaModes);
                }
                catch (TapiException)
                {
                    line.Open(MediaModes.DataModem);
                }
                ChangeButtonStates();
            }
        }

        private void ChangeButtonStates()
        {
            TapiLine line = CurrentLine;
            if (line.IsOpen)
            {
                _btnStartStop.Text = Properties.Resources.String_Stop;
            }
            else
            {
                _btnStartStop.Text = Properties.Resources.String_Start;
            }

            _tbNumber.Enabled = line.Status.CanMakeCall;
            _ckConn.Checked = line.Status.Connected;
            _ckLck.Checked = line.Status.Locked;
            _ckMwi.Checked = line.Status.MessageWaitingLampState;
            _ckSvc.Checked = line.Status.InService;

            _btnMakeCall.Enabled = (CurrentAddress != null && CurrentAddress.Status.CanMakeCall && _tbNumber.Text.Length > 0);
            _btnPickup.Enabled = (CurrentAddress != null && CurrentAddress.Status.CanPickupCall && _tbNumber.Text.Length > 0);
            _btnUnpark.Enabled = (CurrentAddress != null && CurrentAddress.Status.CanUnparkCall && _tbNumber.Text.Length > 0);
            btnForward.Enabled = (CurrentLine.Capabilities.SupportsForwarding && CurrentLine.IsOpen);
        }

        private void _timerReset_Tick(object sender, EventArgs e)
        {
            _statusLabel.Text = string.Empty;
        }

        private void _btnConfigure_Click(object sender, EventArgs e)
        {
            CurrentLine.Config(this.Handle, null);
        }

        private void _tbNumber_TextChanged(object sender, EventArgs e)
        {
            ChangeButtonStates();
        }

        private void _btnMakeCall_Click(object sender, EventArgs e)
        {
            ITapiCall tc = CurrentAddress.MakeCall(_tbNumber.Text);
            ActiveCallForm acf = new ActiveCallForm(tc);
            acf.Show();
        }

        private void _btnPickup_Click(object sender, EventArgs e)
        {
            ITapiCall tc = CurrentAddress.Pickup(_tbNumber.Text, null);
            ActiveCallForm acf = new ActiveCallForm(tc);
            acf.Show();
        }

        private void _btnUnpark_Click(object sender, EventArgs e)
        {
            ITapiCall tc = CurrentAddress.Unpark(_tbNumber.Text);
            ActiveCallForm acf = new ActiveCallForm(tc);
            acf.Show();
        }

        private void _lines_SelectedIndexChanged(object sender, EventArgs e)
        {
            _addresses.DataSource = CurrentLine.Addresses;
            ChangeButtonStates();
        }

        private void _addresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeButtonStates();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            ForwardInfoForm fif = new ForwardInfoForm(CurrentLine);
            if (fif.ShowDialog() == DialogResult.OK)
            {
                if (fif.ForwardInfo.Length > 0)
                {
                    if (fif.SelectedAddress != null)
                        fif.SelectedAddress.Forward(fif.ForwardInfo, 3, null);
                    else
                        CurrentLine.Forward(fif.ForwardInfo, 3, null);
                }
                else
                {
                    if (fif.SelectedAddress != null)
                        fif.SelectedAddress.CancelForward();
                    else
                        CurrentLine.CancelForward();
                }
            }
        }
    }
}