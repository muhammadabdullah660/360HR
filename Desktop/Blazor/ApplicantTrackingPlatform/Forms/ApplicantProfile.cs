using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class ApplicantProfile : Form
    {
        private int pid;
        private int aid;
        private Form activeForm = null;
        public ApplicantProfile(int pid, int aid)
        {
            InitializeComponent();
            this.pid = pid;
            this.aid = aid;
        }
        private void OpenChildForm(Form childfrom)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childfrom;
            childfrom.TopLevel = false;
            childfrom.FormBorderStyle = FormBorderStyle.None;
            childfrom.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childfrom);
            panelChildForm.Tag = childfrom;
            childfrom.BringToFront();
            childfrom.Show();
        }
        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }
        private byte[] GetImageBytesFromPictureBox(Image image)
        {
            // Convert image from picture box to byte array
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
        private void ApplicantProfile_Load(object sender, EventArgs e)
        {

            PersonDL p = new PersonDL();
            PersonBL per = p.GetPersonById(pid);
            label1.Text = per.Email;
            label2.Text = per.Firstname + " " + per.Lastname;
            label3.Text = per.MobileNumber;
            textBox7.Text = per.Firstname;
            textBox6.Text = per.Lastname;
            textBox11.Text = per.MobileNumber;
            AddressDL a = new AddressDL();
            List<AddressBL> al = a.LoadAddresses();
            foreach (AddressBL ad in al)
            {
                if (ad.Id == per.AddressId)
                {
                    textBox8.Text = ad.Country;
                    textBox9.Text = ad.State;
                    textBox10.Text = ad.StreetNo;
                }
            }

            if (aid == -1)
            {
               // textBox10.Enabled = false;
                //textBox9.Enabled = false;
                //textBox8.Enabled = false;
                //textBox7.Enabled = false;
                //textBox6.Enabled = false;
                //textBox11.Enabled = false;
                //richTextBox1.Text = this.companyDescription;
                //textBox1.Text = this.companyName;
                //textBox5.Text = this.comapnyContact;
                //textBox4.Text = this.country;
                //textBox2.Text = this.state;
                //textBox3.Text = this.street;
                button1.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled=false;
                button7.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
                ApplicantDL m = new ApplicantDL();
                ApplicantBL ma = m.GetApplicantbyId(pid);
                textBox1.Text = ma.Linkutl;
                if(ma.Isfreelancer)
                {
                    checkBox1.Checked=true;
                }
               
                try
                {
                    byte[] imageData = ma.Image;

                    // Check if the byte array is not empty
                    if (imageData != null && imageData.Length > 0)
                    {
                        // Convert the byte array to an Image
                        using (MemoryStream ms = new MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(ms);
                            // Set the Image property of the PictureBox
                           // pictureBox1.Image = image;
                            this.Invoke((MethodInvoker)delegate
                            {
                                pictureBox1.Image = new Bitmap(image);
                            });
                        }
                    }
                    else
                    {
                        // Handle case where the byte array is empty or null
                        MessageBox.Show("Image data is empty or null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during image loading
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               

            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the image file into a temporary variable
                    using (Image tempImage = Image.FromFile(openFileDialog.FileName))
                    {
                        // Dispose of the current image in the PictureBox, if any
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image.Dispose();
                        }

                        // Set the new image to the PictureBox
                        pictureBox1.Image = new Bitmap(tempImage);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string err;
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            if (pe.Firstname != textBox7.Text || pe.Lastname != textBox6.Text || pe.MobileNumber != textBox11.Text)
            {
                if (p.UpdatePerson(pid, textBox7.Text, textBox6.Text, textBox11.Text, out err))
                {

                }
            }

            AddressDL a = new AddressDL();
            List<AddressBL> adl = a.LoadAddresses();
            foreach (AddressBL ad in adl)
            {
                if (ad.Id == pe.AddressId)
                {
                    if (ad.StreetNo != textBox10.Text || ad.State != textBox9.Text || ad.Country != textBox8.Text)
                    {
                        if (a.UpdateAddress(pe.AddressId, textBox8.Text, textBox9.Text, textBox10.Text, out String error))
                        {
                        }

                    }
                }
            }
            bool isfree=false;
            if(checkBox1.Checked)
            {
                isfree = true;
            }
            byte[] image = GetImageBytesFromPictureBox(pictureBox1.Image);
            ApplicantDL man = new ApplicantDL();
            ApplicantBL m = new ApplicantBL(pid,textBox1.Text,image,isfree);
            int mi = -1;
            string erro;
            if (man.InsertApplicant(m, out mi, out erro) != -1)
            {
                MessageBox.Show("User Inserted!!");

            }
            else
            {
                MessageBox.Show("Error Occur" + erro);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err;
            PersonDL p = new PersonDL();
            PersonBL pe = p.GetPersonById(pid);
            if (pe.Firstname != textBox7.Text || pe.Lastname != textBox6.Text || pe.MobileNumber != textBox11.Text)
            {
                if (p.UpdatePerson(pid, textBox7.Text, textBox6.Text, textBox11.Text, out err))
                {
                    MessageBox.Show("Update Profile Succesfully!!!");
                }
                else
                {
                    MessageBox.Show("Error in Updating");
                }
            }

            AddressDL a = new AddressDL();
            List<AddressBL> adl = a.LoadAddresses();
            foreach (AddressBL ad in adl)
            {
                if (ad.Id == pe.AddressId)
                {
                    if (ad.StreetNo != textBox10.Text || ad.State != textBox9.Text || ad.Country != textBox8.Text)
                    {
                        if (a.UpdateAddress(pe.AddressId, textBox8.Text, textBox9.Text, textBox10.Text, out String error))
                        {
                            MessageBox.Show("Update Address Succesfully!!!");
                        }
                        else
                        {
                            MessageBox.Show("Error in Updating");
                        }

                    }
                }
            }
            bool isfree=false;
            if(checkBox1.Checked)
            {
                isfree = true;
            }
            byte[] image = GetImageBytesFromPictureBox(pictureBox1.Image);
            ApplicantDL pr = new ApplicantDL();
           if(pr.UpdateApplicant(aid, textBox1.Text, isfree, image, out String errr))
                {
                    MessageBox.Show("Updated Succesfully!!!");
                }
                else
                {
                    MessageBox.Show("Error in Updating");
                }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddEducation(pid, aid));
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddWorkExperience(pid, aid));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddCourses(pid, aid));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddAchivement(pid, aid));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new AddSkills(pid, aid));
        }
    }
}
