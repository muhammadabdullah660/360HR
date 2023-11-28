using ApplicantTrackingPlatform.BL;
using ApplicantTrackingPlatform.DL;
using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ApplicantTrackingPlatform.Forms
{
    public partial class ReportAccept : Form
    {
        int mid;
        public ReportAccept(int mid)
        {
            InitializeComponent();
            this.mid = mid;
        }

        private void ReportAccept_Load(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();

            PersonDL personDL = new PersonDL();
            ApplicantDL applicantDL = new ApplicantDL();
            InterviewFeedbackDL interviewFeedbackDL = new InterviewFeedbackDL();
            JobDL jobDL = new JobDL();
            JobApplicantDL jobApplicantDL = new JobApplicantDL();

            foreach (JobBL job in jobDL.GetAllJob())
            {
                foreach (JobApplicantBL jobApplicant in jobApplicantDL.GetAllJobApplicant())
                {
                    if (jobApplicant.Jobid == job.Id && job.Recid == mid)
                    {
                        foreach (InterviewFeedbackBL feedback in interviewFeedbackDL.GetAllInterviewFeedback())
                        {
                            if (feedback.Jid == jobApplicant.Id)
                            {
                                    foreach (ApplicantBL applicant in applicantDL.GetAllApplicant())
                                    {
                                        if (applicant.Id == jobApplicant.Profileid)
                                        {
                                            PersonBL person = personDL.GetPersonById(applicant.Personid);
                                            DisplayFeedback displayFeedback = new DisplayFeedback();
                                            displayFeedback.Name = $"{person.Firstname} {person.Lastname} accepted the job '{job.Title}' with feedback: {feedback.Message}";
                                            flowLayoutPanel1.Controls.Add(displayFeedback);
                                        }
                                    }
                                
                            }
                        }
                    }
                }
            }

        }
        private void AddControlContentToPdf(Document document, Control customControl)
        {
            if (customControl != null)
            {
                // Add the text content of the custom control as a list item (bullet)
                if (customControl is Label labelControl)
                {
                    string labelText = labelControl.Text ?? "Default Text"; // Replace "Default Text" with a default value
                    document.Add(new ListItem(labelText));
                }
                // You can add more content as needed, depending on the type of your custom control
                // For example, if it's a TextBox, you might want to add its text content:
                // if (customControl is TextBox textBox)
                // {
                //     document.Add(new ListItem(textBox.Text));
                // }

                // If it's a custom user control, you might need to access its internal elements or properties.
                // Adjust the logic based on the actual structure and content of your custom control.
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the path selected by the user
                    string filePath = saveFileDialog.FileName;

                    // Create a PdfWriter and Document
                    using (PdfWriter pdfWriter = new PdfWriter(filePath))
                    {
                        using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
                        {
                            Document document = new Document(pdfDocument);

                            foreach (Control control in flowLayoutPanel1.Controls)
                            {
                                if (control is DisplayFeedback displayFeedback)
                                {
                                    AddControlContentToPdf(document, displayFeedback);
                                }
                            }

                            // Close the Document
                            // Check if the document has pages before closing it
                            if (pdfDocument.GetNumberOfPages() > 0)
                            {
                                // Close the Document
                                document.Close();
                                MessageBox.Show("PDF saved successfully!");
                            }
                            else
                            {
                                // Provide a message or handle the case where no pages were added
                                MessageBox.Show("No content added to the PDF document.");
                            }
                        }
                    }

                    MessageBox.Show("PDF saved successfully!");
                }
            }
        }
    }
}
