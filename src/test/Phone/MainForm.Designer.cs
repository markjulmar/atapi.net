namespace Phone
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnForward = new System.Windows.Forms.Button();
            this._ckSvc = new System.Windows.Forms.CheckBox();
            this._ckConn = new System.Windows.Forms.CheckBox();
            this._ckLck = new System.Windows.Forms.CheckBox();
            this._ckMwi = new System.Windows.Forms.CheckBox();
            this._btnConfigure = new System.Windows.Forms.Button();
            this._btnStartStop = new System.Windows.Forms.Button();
            this._addresses = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._lines = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._timerReset = new System.Windows.Forms.Timer(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this._tbNumber = new System.Windows.Forms.TextBox();
            this._btnMakeCall = new System.Windows.Forms.Button();
            this._btnPickup = new System.Windows.Forms.Button();
            this._btnUnpark = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnForward);
            this.groupBox1.Controls.Add(this._ckSvc);
            this.groupBox1.Controls.Add(this._ckConn);
            this.groupBox1.Controls.Add(this._ckLck);
            this.groupBox1.Controls.Add(this._ckMwi);
            this.groupBox1.Controls.Add(this._btnConfigure);
            this.groupBox1.Controls.Add(this._btnStartStop);
            this.groupBox1.Controls.Add(this._addresses);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this._lines);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(26, 31);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(788, 192);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Session";
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(622, 137);
            this.btnForward.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(154, 44);
            this.btnForward.TabIndex = 10;
            this.btnForward.Text = "&Forward";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // _ckSvc
            // 
            this._ckSvc.AutoSize = true;
            this._ckSvc.Enabled = false;
            this._ckSvc.Location = new System.Drawing.Point(444, 140);
            this._ckSvc.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._ckSvc.Name = "_ckSvc";
            this._ckSvc.Size = new System.Drawing.Size(87, 29);
            this._ckSvc.TabIndex = 9;
            this._ckSvc.Text = "SVC";
            this._ckSvc.UseVisualStyleBackColor = true;
            // 
            // _ckConn
            // 
            this._ckConn.AutoSize = true;
            this._ckConn.Enabled = false;
            this._ckConn.Location = new System.Drawing.Point(334, 140);
            this._ckConn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._ckConn.Name = "_ckConn";
            this._ckConn.Size = new System.Drawing.Size(90, 29);
            this._ckConn.TabIndex = 8;
            this._ckConn.Text = "CON";
            this._ckConn.UseVisualStyleBackColor = true;
            // 
            // _ckLck
            // 
            this._ckLck.AutoSize = true;
            this._ckLck.Enabled = false;
            this._ckLck.Location = new System.Drawing.Point(230, 140);
            this._ckLck.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._ckLck.Name = "_ckLck";
            this._ckLck.Size = new System.Drawing.Size(85, 29);
            this._ckLck.TabIndex = 7;
            this._ckLck.Text = "LCK";
            this._ckLck.UseVisualStyleBackColor = true;
            // 
            // _ckMwi
            // 
            this._ckMwi.AutoSize = true;
            this._ckMwi.Enabled = false;
            this._ckMwi.Location = new System.Drawing.Point(120, 140);
            this._ckMwi.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._ckMwi.Name = "_ckMwi";
            this._ckMwi.Size = new System.Drawing.Size(87, 29);
            this._ckMwi.TabIndex = 6;
            this._ckMwi.Text = "MWI";
            this._ckMwi.UseVisualStyleBackColor = true;
            // 
            // _btnConfigure
            // 
            this._btnConfigure.Location = new System.Drawing.Point(622, 85);
            this._btnConfigure.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnConfigure.Name = "_btnConfigure";
            this._btnConfigure.Size = new System.Drawing.Size(154, 44);
            this._btnConfigure.TabIndex = 5;
            this._btnConfigure.Text = "&Configure";
            this._btnConfigure.UseVisualStyleBackColor = true;
            this._btnConfigure.Click += new System.EventHandler(this._btnConfigure_Click);
            // 
            // _btnStartStop
            // 
            this._btnStartStop.Location = new System.Drawing.Point(622, 33);
            this._btnStartStop.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnStartStop.Name = "_btnStartStop";
            this._btnStartStop.Size = new System.Drawing.Size(154, 44);
            this._btnStartStop.TabIndex = 4;
            this._btnStartStop.Text = "&Start Session";
            this._btnStartStop.UseVisualStyleBackColor = true;
            this._btnStartStop.Click += new System.EventHandler(this._btnStartStop_Click);
            // 
            // _addresses
            // 
            this._addresses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._addresses.FormattingEnabled = true;
            this._addresses.Location = new System.Drawing.Point(120, 88);
            this._addresses.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._addresses.Name = "_addresses";
            this._addresses.Size = new System.Drawing.Size(486, 33);
            this._addresses.TabIndex = 3;
            this._addresses.SelectedIndexChanged += new System.EventHandler(this._addresses_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address:";
            // 
            // _lines
            // 
            this._lines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._lines.FormattingEnabled = true;
            this._lines.Location = new System.Drawing.Point(120, 37);
            this._lines.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._lines.Name = "_lines";
            this._lines.Size = new System.Drawing.Size(486, 33);
            this._lines.TabIndex = 1;
            this._lines.SelectedIndexChanged += new System.EventHandler(this._lines_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Line:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 295);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip1.Size = new System.Drawing.Size(828, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _timerReset
            // 
            this._timerReset.Interval = 5000;
            this._timerReset.Tick += new System.EventHandler(this._timerReset_Tick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 246);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Phone #:";
            // 
            // _tbNumber
            // 
            this._tbNumber.Location = new System.Drawing.Point(140, 240);
            this._tbNumber.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._tbNumber.Name = "_tbNumber";
            this._tbNumber.Size = new System.Drawing.Size(246, 31);
            this._tbNumber.TabIndex = 3;
            this._tbNumber.TextChanged += new System.EventHandler(this._tbNumber_TextChanged);
            // 
            // _btnMakeCall
            // 
            this._btnMakeCall.Location = new System.Drawing.Point(408, 237);
            this._btnMakeCall.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnMakeCall.Name = "_btnMakeCall";
            this._btnMakeCall.Size = new System.Drawing.Size(134, 44);
            this._btnMakeCall.TabIndex = 4;
            this._btnMakeCall.Text = "&Make Call";
            this._btnMakeCall.UseVisualStyleBackColor = true;
            this._btnMakeCall.Click += new System.EventHandler(this._btnMakeCall_Click);
            // 
            // _btnPickup
            // 
            this._btnPickup.Location = new System.Drawing.Point(544, 237);
            this._btnPickup.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnPickup.Name = "_btnPickup";
            this._btnPickup.Size = new System.Drawing.Size(134, 44);
            this._btnPickup.TabIndex = 5;
            this._btnPickup.Text = "&Pickup";
            this._btnPickup.UseVisualStyleBackColor = true;
            this._btnPickup.Click += new System.EventHandler(this._btnPickup_Click);
            // 
            // _btnUnpark
            // 
            this._btnUnpark.Location = new System.Drawing.Point(680, 237);
            this._btnUnpark.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this._btnUnpark.Name = "_btnUnpark";
            this._btnUnpark.Size = new System.Drawing.Size(134, 44);
            this._btnUnpark.TabIndex = 6;
            this._btnUnpark.Text = "&Unpark";
            this._btnUnpark.UseVisualStyleBackColor = true;
            this._btnUnpark.Click += new System.EventHandler(this._btnUnpark_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 317);
            this.Controls.Add(this._btnUnpark);
            this.Controls.Add(this._btnPickup);
            this.Controls.Add(this._btnMakeCall);
            this.Controls.Add(this._tbNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "MainForm";
            this.Text = "TAPI Phone Dialer (C) 2006-2018 JulMar Technology, Inc.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _btnStartStop;
        private System.Windows.Forms.ComboBox _addresses;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _lines;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _btnConfigure;
        private System.Windows.Forms.CheckBox _ckSvc;
        private System.Windows.Forms.CheckBox _ckConn;
        private System.Windows.Forms.CheckBox _ckLck;
        private System.Windows.Forms.CheckBox _ckMwi;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
        private System.Windows.Forms.Timer _timerReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _tbNumber;
        private System.Windows.Forms.Button _btnMakeCall;
        private System.Windows.Forms.Button _btnPickup;
        private System.Windows.Forms.Button _btnUnpark;
        private System.Windows.Forms.Button btnForward;
    }
}

