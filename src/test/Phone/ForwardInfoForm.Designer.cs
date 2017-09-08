namespace Phone
{
    partial class ForwardInfoForm
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
            this.lblLineName = new System.Windows.Forms.Label();
            this.cbAddresses = new System.Windows.Forms.ComboBox();
            this.chkAllAddresses = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textDestination = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textCaller = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbForwardEntryTypes = new System.Windows.Forms.ComboBox();
            this.lbEntries = new System.Windows.Forms.ListBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLineName
            // 
            this.lblLineName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLineName.AutoSize = true;
            this.lblLineName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineName.ForeColor = System.Drawing.Color.Blue;
            this.lblLineName.Location = new System.Drawing.Point(12, 9);
            this.lblLineName.Name = "lblLineName";
            this.lblLineName.Size = new System.Drawing.Size(46, 17);
            this.lblLineName.TabIndex = 0;
            this.lblLineName.Text = "label1";
            // 
            // cbAddresses
            // 
            this.cbAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAddresses.FormattingEnabled = true;
            this.cbAddresses.Location = new System.Drawing.Point(12, 36);
            this.cbAddresses.Name = "cbAddresses";
            this.cbAddresses.Size = new System.Drawing.Size(272, 21);
            this.cbAddresses.TabIndex = 1;
            this.cbAddresses.SelectedIndexChanged += new System.EventHandler(this.cbAddresses_SelectedIndexChanged);
            // 
            // chkAllAddresses
            // 
            this.chkAllAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAllAddresses.AutoSize = true;
            this.chkAllAddresses.Location = new System.Drawing.Point(290, 41);
            this.chkAllAddresses.Name = "chkAllAddresses";
            this.chkAllAddresses.Size = new System.Drawing.Size(89, 17);
            this.chkAllAddresses.TabIndex = 2;
            this.chkAllAddresses.Text = "All Addresses";
            this.chkAllAddresses.UseVisualStyleBackColor = true;
            this.chkAllAddresses.CheckedChanged += new System.EventHandler(this.chkAllAddresses_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDelete);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.textDestination);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textCaller);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbForwardEntryTypes);
            this.groupBox1.Location = new System.Drawing.Point(12, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 136);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Forwarding Entry";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(199, 103);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(96, 103);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textDestination
            // 
            this.textDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textDestination.Location = new System.Drawing.Point(73, 77);
            this.textDestination.Name = "textDestination";
            this.textDestination.Size = new System.Drawing.Size(272, 20);
            this.textDestination.TabIndex = 4;
            this.textDestination.TextChanged += new System.EventHandler(this.textDestination_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination";
            // 
            // textCaller
            // 
            this.textCaller.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textCaller.Location = new System.Drawing.Point(73, 48);
            this.textCaller.Name = "textCaller";
            this.textCaller.Size = new System.Drawing.Size(272, 20);
            this.textCaller.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Caller";
            // 
            // cbForwardEntryTypes
            // 
            this.cbForwardEntryTypes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbForwardEntryTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbForwardEntryTypes.FormattingEnabled = true;
            this.cbForwardEntryTypes.Location = new System.Drawing.Point(7, 20);
            this.cbForwardEntryTypes.Name = "cbForwardEntryTypes";
            this.cbForwardEntryTypes.Size = new System.Drawing.Size(338, 21);
            this.cbForwardEntryTypes.TabIndex = 0;
            // 
            // lbEntries
            // 
            this.lbEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbEntries.FormattingEnabled = true;
            this.lbEntries.Location = new System.Drawing.Point(12, 206);
            this.lbEntries.Name = "lbEntries";
            this.lbEntries.Size = new System.Drawing.Size(357, 134);
            this.lbEntries.TabIndex = 5;
            this.lbEntries.SelectedIndexChanged += new System.EventHandler(this.lbEntries_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(294, 9);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "Forward";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ForwardInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 353);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lbEntries);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkAllAddresses);
            this.Controls.Add(this.cbAddresses);
            this.Controls.Add(this.lblLineName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ForwardInfoForm";
            this.Text = "Forward Information";
            this.Load += new System.EventHandler(this.ForwardInfoForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLineName;
        private System.Windows.Forms.ComboBox cbAddresses;
        private System.Windows.Forms.CheckBox chkAllAddresses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textDestination;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textCaller;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbForwardEntryTypes;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lbEntries;
        private System.Windows.Forms.Button btnOK;
    }
}