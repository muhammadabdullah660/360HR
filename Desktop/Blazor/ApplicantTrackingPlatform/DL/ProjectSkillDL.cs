using ApplicantTrackingPlatform.BL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTrackingPlatform.DL
{
    class ProjectSkillDL
    {
        private string connectionString;
        public List<ProjectSkillBL> ProjectSkilllist = new List<ProjectSkillBL>();

        public ProjectSkillDL()
        {
            connectionString = @"Data Source=(local);Initial Catalog=360HR;Integrated Security=True";
            LoadProjectSkillData();
        }

        public List<ProjectSkillBL> GetAllProjectSkill()
        {
            return ProjectSkilllist;
        }

        public List<ProjectSkillBL> GetLsitBySkill(List<String> Skill, int id)
        {
            List<ProjectSkillBL> SkillList = new List<ProjectSkillBL>();
            foreach (string s in Skill)
            {
                ProjectSkillBL sj = new ProjectSkillBL(id, s);
                SkillList.Add(sj);
            }
            return SkillList;


        }
        public List<string> GetSkill(int proid)
        {
            List<string> skill = new List<string>();
            foreach (ProjectSkillBL js in ProjectSkilllist)
            {
                if (js.Projectid1 == proid)
                {
                    skill.Add(js.Name);
                }
            }
            return skill;
        }

        public void DeleteProjectSkill(List<ProjectSkillBL> ProjectSkills, out string error)
        {
            object result = "";


            foreach (ProjectSkillBL sj in ProjectSkills)
            {
                error = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Delete from ProjectSKILLS where ProjectId=@ProjectId and Name=@Name", connection);

                    // Assuming "address" is an instance of your Address class
                    command.Parameters.AddWithValue("@ProjectId", sj.Projectid1);
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
        public void InsertProjectSkill(List<ProjectSkillBL> ProjectSkills, out string error)
        {
            object result = "";


            foreach (ProjectSkillBL sj in ProjectSkills)
            {
                error = "";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("INSERT INTO ProjectSKILLS (ProjectId,Name) VALUES (@ProjectId,@Name)", connection);

                    // Assuming "address" is an instance of your Address class
                    command.Parameters.AddWithValue("@ProjectId", sj.Projectid1);
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

        private void LoadProjectSkillData()
        {
            ProjectSkilllist.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from ProjectSkills", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProjectSkillBL ProjectSkill = new ProjectSkillBL();
                    ProjectSkill.Projectid1 = Convert.ToInt32(reader["ProjectId"]);
                    ProjectSkill.Name = reader["Name"].ToString();
                    ProjectSkilllist.Add(ProjectSkill);
                }
                reader.Close();
            }
        }
    }

}
