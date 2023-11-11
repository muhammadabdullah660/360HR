using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class JobSkillDL
    {
        private string connectionString;
        public List<JobSkillBL> JobSkilllist = new List<JobSkillBL>();

        public JobSkillDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadJobSkillData();
        }

        public List<JobSkillBL> GetAllJobSkill()
        {
            return JobSkilllist;
        }

        public List<JobSkillBL> GetLsitBySkill(List<String> Skill,int id)
        {
            List<JobSkillBL> SkillList=new List<JobSkillBL>();
            foreach(string s in Skill)
            {
                JobSkillBL sj=new JobSkillBL(id,s);
                SkillList.Add(sj);
            }
            return SkillList;


        }
        public List<string> GetSkill(int jid)
        {
            List<string> skill = new List<string>();
            foreach(JobSkillBL js in JobSkilllist)
            {
                if(js.Jobid1==jid)
                {
                    skill.Add(js.Name);
                }
            }
            return skill;
        }

        public void DeleteJobSkill(List<JobSkillBL> JobSkills, out string error)
        {
            object result = "";


            foreach (JobSkillBL sj in JobSkills)
            {
                error = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Delete from JOBSKILLS where JobID=@JobID and Name=@Name", connection);

                    // Assuming "address" is an instance of your Address class
                    command.Parameters.AddWithValue("@JobID", sj.Jobid1);
                    command.Parameters.AddWithValue("@Name", sj.Name);
                    // Execute the SQL command and retrieve the newly created address ID
                    result = command.ExecuteScalar();


                }

            }
            if (result != null)
            {
                error = "";
            }
            else
            {
                // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                error = "Insertion failed or addressId couldn't be retrieved.";

            }
        }
        public void InsertJobSkill(List<JobSkillBL> JobSkills, out string error)
        {
            object result = "";


            foreach (JobSkillBL sj in JobSkills)
            {
                error = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO JOBSKILLS (JobID,Name) VALUES (@JobID,@Name)", connection);

                    // Assuming "address" is an instance of your Address class
                    command.Parameters.AddWithValue("@JobID", sj.Jobid1);
                    command.Parameters.AddWithValue("@Name", sj.Name);
                    // Execute the SQL command and retrieve the newly created address ID
                    result = command.ExecuteScalar();

                    
                }

            }
            if (result != null)
            {
                error = "";
            }
            else
            {
                // Handle the case where the insertion failed or the addressId couldn't be retrieved.
                error = "Insertion failed or addressId couldn't be retrieved.";

            }



        }

        private void LoadJobSkillData()
        {
            JobSkilllist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from JobSkills", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    JobSkillBL JobSkill = new JobSkillBL();
                    JobSkill.Jobid1 = Convert.ToInt32(reader["JobID"]);
                    JobSkill.Name = reader["Name"].ToString();
                    JobSkilllist.Add(JobSkill);
                }
                reader.Close();
            }
        }

    }
}
