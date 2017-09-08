namespace Phone
{
    partial class ActiveCallForm
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
            this.btnAccept = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnDrop = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnUnhold = new System.Windows.Forms.Button();
            this.btnSwapHold = new System.Windows.Forms.Button();
            this.btnPark = new System.Windows.Forms.Button();
            this.btnBlindTransfer = new System.Windows.Forms.Button();
            this.btnSetupTransfer = new System.Windows.Forms.Button();
            this.btnCompleteTransfer = new System.Windows.Forms.Button();
            this.btnDial = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAccept.Location = new System.Drawing.Point(-4, 211);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyGrid1.CommandsDisabledLinkColor = System.Drawing.Color.Black;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(12, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid1.Size = new System.Drawing.Size(338, 193);
            this.propertyGrid1.TabIndex = 2;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // btnAnswer
            // 
            this.btnAnswer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAnswer.Location = new System.Drawing.Point(70, 211);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(75, 23);
            this.btnAnswer.TabIndex = 3;
            this.btnAnswer.Text = "Answer";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // btnDrop
            // 
            this.btnDrop.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDrop.Location = new System.Drawing.Point(144, 211);
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.Size = new System.Drawing.Size(75, 23);
            this.btnDrop.TabIndex = 4;
            this.btnDrop.Text = "Drop";
            this.btnDrop.UseVisualStyleBackColor = true;
            this.btnDrop.Click += new System.EventHandler(this.btnDrop_Click);
            // 
            // btnHold
            // 
            this.btnHold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnHold.Location = new System.Drawing.Point(218, 211);
            this.btnHold.Name = "btnHold";
            this.btnHold.Size = new System.Drawing.Size(75, 23);
            this.btnHold.TabIndex = 5;
            this.btnHold.Text = "Hold";
            this.btnHold.UseVisualStyleBackColor = true;
            this.btnHold.Click += new System.EventHandler(this.btnHold_Click);
            // 
            // btnUnhold
            // 
            this.btnUnhold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUnhold.Location = new System.Drawing.Point(292, 211);
            this.btnUnhold.Name = "btnUnhold";
            this.btnUnhold.Size = new System.Drawing.Size(75, 23);
            this.btnUnhold.TabIndex = 6;
            this.btnUnhold.Text = "Unhold";
            this.btnUnhold.UseVisualStyleBackColor = true;
            this.btnUnhold.Click += new System.EventHandler(this.btnUnhold_Click);
            // 
            // btnSwapHold
            // 
            this.btnSwapHold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSwapHold.Location = new System.Drawing.Point(-4, 233);
            this.btnSwapHold.Name = "btnSwapHold";
            this.btnSwapHold.Size = new System.Drawing.Size(75, 23);
            this.btnSwapHold.TabIndex = 7;
            this.btnSwapHold.Text = "Swap Hold";
            this.btnSwapHold.UseVisualStyleBackColor = true;
            this.btnSwapHold.Click += new System.EventHandler(this.btnSwapHold_Click);
            // 
            // btnPark
            // 
            this.btnPark.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPark.Location = new System.Drawing.Point(70, 233);
            this.btnPark.Name = "btnPark";
            this.btnPark.Size = new System.Drawing.Size(75, 23);
            this.btnPark.TabIndex = 8;
            this.btnPark.Text = "Park";
            this.btnPark.UseVisualStyleBackColor = true;
            this.btnPark.Click += new System.EventHandler(this.btnPark_Click);
            // 
            // btnBlindTransfer
            // 
            this.btnBlindTransfer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBlindTransfer.Location = new System.Drawing.Point(144, 233);
            this.btnBlindTransfer.Name = "btnBlindTransfer";
            this.btnBlindTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnBlindTransfer.TabIndex = 9;
            this.btnBlindTransfer.Text = "Blind Xfer";
            this.btnBlindTransfer.UseVisualStyleBackColor = true;
            this.btnBlindTransfer.Click += new System.EventHandler(this.btnBlindTransfer_Click);
            // 
            // btnSetupTransfer
            // 
            this.btnSetupTransfer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSetupTransfer.Location = new System.Drawing.Point(218, 233);
            this.btnSetupTransfer.Name = "btnSetupTransfer";
            this.btnSetupTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnSetupTransfer.TabIndex = 10;
            this.btnSetupTransfer.Text = "Setup Xfer";
            this.btnSetupTransfer.UseVisualStyleBackColor = true;
            this.btnSetupTransfer.Click += new System.EventHandler(this.btnSetupTransfer_Click);
            // 
            // btnCompleteTransfer
            // 
            this.btnCompleteTransfer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCompleteTransfer.Location = new System.Drawing.Point(292, 233);
            this.btnCompleteTransfer.Name = "btnCompleteTransfer";
            this.btnCompleteTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnCompleteTransfer.TabIndex = 11;
            this.btnCompleteTransfer.Text = "Comp Xfer";
            this.btnCompleteTransfer.UseVisualStyleBackColor = true;
            this.btnCompleteTransfer.Click += new System.EventHandler(this.btnCompleteTransfer_Click);
            // 
            // btnDial
            // 
            this.btnDial.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDial.Location = new System.Drawing.Point(-4, 255);
            this.btnDial.Name = "btnDial";
            this.btnDial.Size = new System.Drawing.Size(75, 23);
            this.btnDial.TabIndex = 12;
            this.btnDial.Text = "Dial";
            this.btnDial.UseVisualStyleBackColor = true;
            this.btnDial.Click += new System.EventHandler(this.btnDial_Click);
            // 
            // ActiveCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 317);
            this.Controls.Add(this.btnDial);
            this.Controls.Add(this.btnCompleteTransfer);
            this.Controls.Add(this.btnSetupTransfer);
            this.Controls.Add(this.btnBlindTransfer);
            this.Controls.Add(this.btnPark);
            this.Controls.Add(this.btnSwapHold);
            this.Controls.Add(this.btnUnhold);
            this.Controls.Add(this.btnHold);
            this.Controls.Add(this.btnDrop);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.btnAccept);
            this.Name = "ActiveCallForm";
            this.Text = "ActiveCallForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ActiveCallForm_FormClosed);
            this.Load += new System.EventHandler(this.ActiveCallForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.Button btnDrop;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnUnhold;
        private System.Windows.Forms.Button btnSwapHold;
        private System.Windows.Forms.Button btnPark;
        private System.Windows.Forms.Button btnBlindTransfer;
        private System.Windows.Forms.Button btnSetupTransfer;
        private System.Windows.Forms.Button btnCompleteTransfer;
        private System.Windows.Forms.Button btnDial;

    }
}