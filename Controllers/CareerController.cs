using HBHB.Models;
using HBPOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace HBHB.Controllers
{
    [Authorize]
    [ApiController]
    public class CareerController : ControllerBase
    {
        [HttpGet]
        [Route("api/getCareerInfo/{UID}")]
        public List<CareerProfileRetrieve> GetCareerInfo(string UID = "")
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<CareerProfileRetrieve> careerList = new List<CareerProfileRetrieve>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetCareerProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", UID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CareerProfileRetrieve careerProfile = new CareerProfileRetrieve();
                    careerProfile.UID = rdr["UID"].ToString();
                    careerProfile.Affiliation = rdr["Affiliation"].ToString();
                    careerProfile.Experience = rdr["Experience"].ToString();
                    careerProfile.DesignSpecialization = rdr["DesignSpecialization"].ToString();
                    careerProfile.Portfolio = rdr["Portfolio"].ToString();
                    careerProfile.FashionEvents = rdr["FashionEvents"].ToString();
                    careerProfile.DesignInspirations = rdr["DesignInspirations"].ToString();
                    careerProfile.SpecialRequirements = rdr["SpecialRequirements"].ToString();
                    careerProfile.HeardAbout = rdr["HeardAbout"].ToString();
                    careerProfile.DesignAesthetic = rdr["DesignAesthetic"].ToString();
                    careerProfile.TargetAudience = rdr["TargetAudience"].ToString();
                    careerProfile.DesignTechniques = rdr["DesignTechniques"].ToString();
                    careerProfile.AwardsRecognition = rdr["AwardsRecognition"].ToString();
                    careerProfile.ChallengesObstacles = rdr["ChallengesObstacles"].ToString();
                    careerProfile.InspirationMotivation = rdr["InspirationMotivation"].ToString();
                    careerProfile.GoalsMilestones = rdr["GoalsMilestones"].ToString();
                    careerList.Add(careerProfile);
                }
                con.Close();
            }
            return careerList;
        }

        [HttpPost]
        [Route("api/UpdateCareerProfile")]
        public List<Response> UpdateCareerProfile([FromBody] CareerProfile careerProfile)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("UpdateCareerProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", careerProfile.UID);
                cmd.Parameters.AddWithValue("@Affiliation", careerProfile.Affiliation);
                cmd.Parameters.AddWithValue("@Experience", careerProfile.Experience);
                cmd.Parameters.AddWithValue("@DesignSpecialization", careerProfile.DesignSpecialization);
                cmd.Parameters.AddWithValue("@Portfolio", careerProfile.Portfolio);
                cmd.Parameters.AddWithValue("@FashionEvents", careerProfile.FashionEvents);
                cmd.Parameters.AddWithValue("@DesignInspirations", careerProfile.DesignInspirations);
                cmd.Parameters.AddWithValue("@SpecialRequirements", careerProfile.SpecialRequirements);
                cmd.Parameters.AddWithValue("@HeardAbout", careerProfile.HeardAbout);
                cmd.Parameters.AddWithValue("@DesignAesthetic", careerProfile.DesignAesthetic);
                cmd.Parameters.AddWithValue("@TargetAudience", careerProfile.TargetAudience);
                cmd.Parameters.AddWithValue("@DesignTechniques", careerProfile.DesignTechniques);
                cmd.Parameters.AddWithValue("@AwardsRecognition", careerProfile.AwardsRecognition);
                cmd.Parameters.AddWithValue("@ChallengesObstacles", careerProfile.ChallengesObstacles);
                cmd.Parameters.AddWithValue("@InspirationMotivation", careerProfile.InspirationMotivation);
                cmd.Parameters.AddWithValue("@GoalsMilestones", careerProfile.GoalsMilestones);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            Response responseObj = new Response();
            responseObj.Message = "Successful";
            responseList.Add(responseObj);
            return responseList;
        }
    }
}
