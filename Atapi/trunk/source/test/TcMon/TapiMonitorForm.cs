using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JulMar.Atapi;

namespace TcMon
{
    public partial class TapiMonitorForm : Form
    {
        TapiManager tapiManager = new TapiManager("TapiCallMonitor.net");
        
        const int COLUMNS_CID = 1;
        const int COLUMNS_STATE = 2;
        const int COLUMNS_CALLER = 3;
        const int COLUMNS_CALLED = 4;

        public TapiMonitorForm()
        {
            InitializeComponent();
        }

        private void TapiMonitorForm_Load(object sender, EventArgs e)
        {
            if (tapiManager.Initialize() == false)
            {
                MessageBox.Show("No Tapi devices found.");
                this.Close();
                return;
            }

            foreach (TapiLine line in tapiManager.Lines)
            {
                try 
                {
                    line.NewCall += OnNewCall;
                    line.CallStateChanged += OnCallStateChanged;
                    line.CallInfoChanged += OnCallInfoChanged;
                    line.Monitor();
                }
                catch (TapiException ex)
                {
                    LogError(ex.Message);
                }
            }
        }

        private void TapiMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tapiManager.Shutdown();
        }

        private void OnNewCall(object sender, NewCallEventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler<NewCallEventArgs> eh = OnNewCall;
                this.BeginInvoke(eh, new object[] { sender, e });
                return;
            }

            TapiLine line = (TapiLine) sender;
            TapiCall call = e.Call;

            ListViewItem lvi = new ListViewItem(line.Name);
            lvi.Tag = call;

            lvi.SubItems.AddRange(
                new string[] {
                    string.Format("0x{0:X}", call.Id),
                    call.CallState.ToString(),
                    string.Format("{0} {1}", call.CallerId, call.CallerName),
                    string.Format("{0} {1}", call.CalledId, call.CalledName),
                });
            lvCalls.Items.Add(lvi);
        }

        private void OnCallStateChanged(object sender, CallStateEventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler<CallStateEventArgs> eh = OnCallStateChanged;
                this.BeginInvoke(eh, new object[] { sender, e });
                return;
            }

            TapiLine line = (TapiLine)sender;
            TapiCall call = e.Call;

            foreach (ListViewItem lvi in lvCalls.Items)
            {
                if (lvi.Tag == call)
                {
                    lvi.SubItems[COLUMNS_STATE].Text = call.CallState.ToString();
                    if (GetActiveCall() == call)
                        AdjustButtonState(call);

                    if (call.CallState == CallState.Idle)
                    {
                        call.Dispose();
                        lvCalls.Items.Remove(lvi);
                    }
                    break;
                }
            }
        }

        private void OnCallInfoChanged(object sender, CallInfoChangeEventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler<CallInfoChangeEventArgs> eh = OnCallInfoChanged;
                this.BeginInvoke(eh, new object[] { sender, e });
                return;
            }

            TapiLine line = (TapiLine)sender;
            TapiCall call = e.Call;

            if ((e.Change & (CallInfoChangeTypes.CalledId | CallInfoChangeTypes.CallerId)) > 0)
            {
                foreach (ListViewItem lvi in lvCalls.Items)
                {
                    if (lvi.Tag == call)
                    {
                        lvi.SubItems[COLUMNS_CALLER].Text = string.Format("{0} {1}", call.CallerId, call.CallerName);
                        lvi.SubItems[COLUMNS_CALLED].Text = string.Format("{0} {1}", call.CalledId, call.CalledName);
                        break;
                    }
                }
            }
        }

        delegate void StringCallback(string p);
        private void LogError(string p)
        {
            if (InvokeRequired)
            {
                StringCallback scb = LogError;
                BeginInvoke(scb, new object[] { p });
                return;
            }

            this.toolStripStatusLabel1.Text = p;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.toolStripStatusLabel1.Text = String.Empty;
        }

        private void lvCalls_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdjustButtonState(GetActiveCall());
        }

        private TapiCall GetActiveCall()
        {
            if (lvCalls.SelectedItems.Count > 0)
            {
                return (TapiCall)lvCalls.SelectedItems[0].Tag;
            }
            return null;
        }

        private void AdjustButtonState(TapiCall tCall)
        {
            if (tCall != null)
            {
                btnAccept.Enabled = tCall.Features.CanAccept;
                btnAnswer.Enabled = tCall.Features.CanAnswer;
                btnDrop.Enabled = tCall.Features.CanDrop;
                btnHold.Enabled = tCall.Features.CanHold;
                btnUnhold.Enabled = tCall.Features.CanUnhold;
            }
            else
            {
                btnAccept.Enabled = false;
                btnAnswer.Enabled = false;
                btnDrop.Enabled = false;
                btnHold.Enabled = false;
                btnUnhold.Enabled = false;
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            TapiCall call = GetActiveCall();
            if (call != null)
            {
                if (!AdjustCallPrivilege(call))
                    return;

                call.BeginAccept(
                    delegate(IAsyncResult ar)
                    {
                        try
                        {
                            call.EndAccept(ar);
                        }
                        catch (TapiException ex)
                        {
                            LogError(ex.Message);
                        }
                    }, null);
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            TapiCall call = GetActiveCall();
            if (call != null)
            {
                if (!AdjustCallPrivilege(call))
                    return;

                call.BeginAnswer(
                    delegate(IAsyncResult ar)
                    {
                        try
                        {
                            call.EndAnswer(ar);
                        }
                        catch (TapiException ex)
                        {
                            LogError(ex.Message);
                        }
                    }, null);
            }
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            TapiCall call = GetActiveCall();
            if (call != null)
            {
                if (!AdjustCallPrivilege(call))
                    return;

                call.BeginDrop(
                    delegate(IAsyncResult ar)
                    {
                        try
                        {
                            call.EndDrop(ar);
                        }
                        catch (TapiException ex)
                        {
                            LogError(ex.Message);
                        }
                    }, null);
            }
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            TapiCall call = GetActiveCall();
            if (call != null)
            {
                if (!AdjustCallPrivilege(call))
                    return;

                call.BeginHold(
                    delegate(IAsyncResult ar)
                    {
                        try
                        {
                            call.EndHold(ar);
                        }
                        catch (TapiException ex)
                        {
                            LogError(ex.Message);
                        }
                    }, null);
            }
        }

        private void btnUnhold_Click(object sender, EventArgs e)
        {
            TapiCall call = GetActiveCall();
            if (call != null)
            {
                if (!AdjustCallPrivilege(call))
                    return;

                call.BeginUnhold(
                    delegate(IAsyncResult ar)
                    {
                        try
                        {
                            call.EndUnhold(ar);
                        }
                        catch (TapiException ex)
                        {
                            LogError(ex.Message);
                        }
                    }, null);
            }

        }

        private bool AdjustCallPrivilege(TapiCall call)
        {
            try
            {
                call.Privilege = Privilege.Owner;
                return true;
            }
            catch (TapiException ex)
            {
                LogError(string.Format("Cannot set ownership: {0}", ex.Message));
            }
            return false;
        }
    }
}