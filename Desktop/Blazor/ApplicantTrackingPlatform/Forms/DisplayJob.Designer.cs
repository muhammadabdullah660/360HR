namespace ApplicantTrackingPlatform.Forms
{
    partial class DisplayJob
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbltitle = new System.Windows.Forms.Label();
            this.txtdes = new System.Windows.Forms.Label();
            this.lblcom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbltitle
            // 
            this.lbltitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lbltitle.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltitle.Location = new System.Drawing.Point(29, 15);
            this.lbltitle.Name = "lbltitle";
            this.lbltitle.Size = new System.Drawing.Size(210, 33);
            this.lbltitle.TabIndex = 2;
            this.lbltitle.Text = "Title";
            // 
            // txtdes
            // 
            this.txtdes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtdes.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdes.Location = new System.Drawing.Point(45, 48);
            this.txtdes.Name = "txtdes";
            this.txtdes.Size = new System.Drawing.Size(451, 35);
            this.txtdes.TabIndex = 3;
            this.txtdes.Text = "Title";
            // 
            // lblcom
            // 
            this.lblcom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblcom.Font = new System.Drawing.Font("Times New Roman", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcom.Location = new System.Drawing.Point(29, 83);
            this.lblcom.Name = "lblcom";
            this.lblcom.Size = new System.Drawing.Size(210, 40);
            this.lblcom.TabIndex = 4;
            this.lblcom.Text = "Title";
            // 
            // DisplayJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblcom);
            this.Controls.Add(this.txtdes);
            this.Controls.Add(this.lbltitle);
            this.Name = "DisplayJob";
            this.Size = new System.Drawing.Size(554, 144);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbltitle;
        private System.Windows.Forms.Label txtdes;
        private System.Windows.Forms.Label lblcom;
    }
}
