namespace ApplicantTrackingPlatform.Forms
{
    partial class SignUp
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtstreet = new System.Windows.Forms.TextBox();
            this.txtphone = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtlname = new System.Windows.Forms.TextBox();
            this.txtfname = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbrole = new System.Windows.Forms.ComboBox();
            this.cmbgender = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtstate = new System.Windows.Forms.TextBox();
            this.txtcountry = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(372, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 24);
            this.label3.TabIndex = 9;
            this.label3.Text = "Last Name";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(129, 222);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(202, 20);
            this.txtpassword.TabIndex = 8;
            this.txtpassword.Validating += new System.ComponentModel.CancelEventHandler(this.txtpassword_Validating);
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(125, 158);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(202, 20);
            this.txtemail.TabIndex = 7;
            this.txtemail.Validating += new System.ComponentModel.CancelEventHandler(this.txtemail_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "Role";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(388, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "Gender";
            // 
            // txtstreet
            // 
            this.txtstreet.Location = new System.Drawing.Point(127, 286);
            this.txtstreet.Name = "txtstreet";
            this.txtstreet.Size = new System.Drawing.Size(166, 20);
            this.txtstreet.TabIndex = 12;
            this.txtstreet.Text = "Street NO";
            this.txtstreet.TextChanged += new System.EventHandler(this.txtstreet_TextChanged);
            this.txtstreet.Validating += new System.ComponentModel.CancelEventHandler(this.txtstreet_Validating);
            // 
            // txtphone
            // 
            this.txtphone.Location = new System.Drawing.Point(508, 159);
            this.txtphone.Name = "txtphone";
            this.txtphone.Size = new System.Drawing.Size(202, 20);
            this.txtphone.TabIndex = 11;
            this.txtphone.TextChanged += new System.EventHandler(this.txtphone_TextChanged);
            this.txtphone.Validating += new System.ComponentModel.CancelEventHandler(this.txtphone_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 24);
            this.label5.TabIndex = 18;
            this.label5.Text = "First Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(23, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 24);
            this.label6.TabIndex = 17;
            this.label6.Text = "Email";
            // 
            // txtlname
            // 
            this.txtlname.Location = new System.Drawing.Point(508, 92);
            this.txtlname.Name = "txtlname";
            this.txtlname.Size = new System.Drawing.Size(202, 20);
            this.txtlname.TabIndex = 16;
            this.txtlname.Validating += new System.ComponentModel.CancelEventHandler(this.txtlname_Validating);
            // 
            // txtfname
            // 
            this.txtfname.Location = new System.Drawing.Point(129, 92);
            this.txtfname.Name = "txtfname";
            this.txtfname.Size = new System.Drawing.Size(202, 20);
            this.txtfname.TabIndex = 15;
            this.txtfname.Validating += new System.ComponentModel.CancelEventHandler(this.txtfname_Validating);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(741, 66);
            this.panel1.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Monotype Corsiva", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(296, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 33);
            this.label7.TabIndex = 1;
            this.label7.Text = "WELCOME";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(367, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 24);
            this.label8.TabIndex = 20;
            this.label8.Text = "Contact No";
            // 
            // cmbrole
            // 
            this.cmbrole.FormattingEnabled = true;
            this.cmbrole.Items.AddRange(new object[] {
            "Applicant",
            "Manager",
            "Recruiter"});
            this.cmbrole.Location = new System.Drawing.Point(126, 357);
            this.cmbrole.Name = "cmbrole";
            this.cmbrole.Size = new System.Drawing.Size(202, 21);
            this.cmbrole.TabIndex = 21;
            this.cmbrole.Validating += new System.ComponentModel.CancelEventHandler(this.cmbrole_Validating);
            // 
            // cmbgender
            // 
            this.cmbgender.FormattingEnabled = true;
            this.cmbgender.Items.AddRange(new object[] {
            "F",
            "M"});
            this.cmbgender.Location = new System.Drawing.Point(505, 212);
            this.cmbgender.Name = "cmbgender";
            this.cmbgender.Size = new System.Drawing.Size(202, 21);
            this.cmbgender.TabIndex = 22;
            this.cmbgender.Validating += new System.ComponentModel.CancelEventHandler(this.cmbgender_Validating);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(23, 286);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 24);
            this.label9.TabIndex = 23;
            this.label9.Text = "Address";
            // 
            // txtstate
            // 
            this.txtstate.Location = new System.Drawing.Point(316, 286);
            this.txtstate.Name = "txtstate";
            this.txtstate.Size = new System.Drawing.Size(184, 20);
            this.txtstate.TabIndex = 24;
            this.txtstate.Text = "City";
            this.txtstate.Validating += new System.ComponentModel.CancelEventHandler(this.txtstate_Validating);
            // 
            // txtcountry
            // 
            this.txtcountry.Location = new System.Drawing.Point(518, 286);
            this.txtcountry.Name = "txtcountry";
            this.txtcountry.Size = new System.Drawing.Size(202, 20);
            this.txtcountry.TabIndex = 25;
            this.txtcountry.Text = "Country";
            this.txtcountry.Validating += new System.ComponentModel.CancelEventHandler(this.txtcountry_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(298, 281);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 24);
            this.label10.TabIndex = 26;
            this.label10.Text = "-";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(500, 283);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 24);
            this.label11.TabIndex = 27;
            this.label11.Text = "-";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(124, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(207, 26);
            this.label12.TabIndex = 28;
            this.label12.Text = "Password must contain letter, number and \r\nspecial character. Length is 8 charact" +
    "ers. ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(505, 182);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(162, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Phone Number is without dashes";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(570, 392);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 34);
            this.button1.TabIndex = 30;
            this.button1.Text = "Registered";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // SignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(741, 461);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtcountry);
            this.Controls.Add(this.txtstate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbgender);
            this.Controls.Add(this.cmbrole);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtlname);
            this.Controls.Add(this.txtfname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtstreet);
            this.Controls.Add(this.txtphone);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtemail);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(757, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(757, 500);
            this.Name = "SignUp";
            this.Text = "SignUp";
            this.Load += new System.EventHandler(this.SignUp_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtstreet;
        private System.Windows.Forms.TextBox txtphone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtlname;
        private System.Windows.Forms.TextBox txtfname;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbrole;
        private System.Windows.Forms.ComboBox cmbgender;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtstate;
        private System.Windows.Forms.TextBox txtcountry;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}