using HBHB.Data;
using HBHB.Models;
using HBPOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HBHB.Controllers
{
    [Authorize]
    [ApiController]
    public class AdviseController : ControllerBase
    {

        [HttpGet]
        [Route("api/getCustomerInfo/{UID}")]
        public List<CustomerProfileRetrieve> getCustomerInfo(string UID = "")
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<CustomerProfileRetrieve> customer = new List<CustomerProfileRetrieve>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetCustomerProfile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", UID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    CustomerProfileRetrieve finalcustomer = new CustomerProfileRetrieve();
                    finalcustomer.UID = (rdr["UID"].ToString());
                    finalcustomer.CustomerID = (Convert.ToInt32(rdr["CustomerID"]));
                    finalcustomer.FirstName = (rdr["FirstName"].ToString());
                    finalcustomer.LastName = (rdr["LastName"].ToString());
                    finalcustomer.PhoneNumber = (rdr["PhoneNumber"].ToString());
                    finalcustomer.HairColor = (rdr["HairColor"].ToString());
                    finalcustomer.SkinColor = (rdr["SkinColor"].ToString());
                    finalcustomer.EyeColor = (rdr["EyeColor"].ToString());
                    finalcustomer.Insecurities = (rdr["Insecurities"].ToString());
                    finalcustomer.Securities = (rdr["Securities"].ToString());
                    finalcustomer.FavTV_Movies = (rdr["FavTV_Movies"].ToString());
                    finalcustomer.JobDesc = (rdr["JobDesc"].ToString());
                    finalcustomer.AcomplishedFeeling = (rdr["AcomplishedFeeling"].ToString());
                    finalcustomer.VibeWanted = (rdr["VibeWanted"].ToString());
                    finalcustomer.Photo = (rdr["Photo"].ToString());
                    finalcustomer.FaceShapeID = (rdr["FaceShapeID"].ToString());
                    finalcustomer.Height = (rdr["Height"].ToString());
                    finalcustomer.Weight = (rdr["Weight"].ToString());
                    customer.Add(finalcustomer);
                }
                con.Close();
            }
            return customer;
        }

        [HttpPost]
        [Route("api/UpdateCustomerProfile")]
        public List<Response> UpdateCustomerProfile([FromBody] CustomerProfile customerProfile)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            SqlConnection con = new SqlConnection(con2);
            SqlCommand cmd = new SqlCommand("UpdateCustomerProfile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", customerProfile.UID);
            cmd.Parameters.AddWithValue("@FirstName", customerProfile.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customerProfile.LastName);
            cmd.Parameters.AddWithValue("@PhoneNumber", customerProfile.PhoneNumber);
            cmd.Parameters.AddWithValue("@HairColor", customerProfile.HairColor);
            cmd.Parameters.AddWithValue("@SkinColor", customerProfile.SkinColor);
            cmd.Parameters.AddWithValue("@EyeColor", customerProfile.EyeColor);
            cmd.Parameters.AddWithValue("@FaceShapeID", customerProfile.FaceShapeID);
            cmd.Parameters.AddWithValue("@Height", customerProfile.Height);
            cmd.Parameters.AddWithValue("@Weight", customerProfile.Weight);
            cmd.Parameters.AddWithValue("@Insecurities", customerProfile.Insecurities);
            cmd.Parameters.AddWithValue("@Securities", customerProfile.Securities);
            cmd.Parameters.AddWithValue("@FavTVMovies", customerProfile.FavTV_Movies);
            cmd.Parameters.AddWithValue("@JobDesc", customerProfile.JobDesc);
            cmd.Parameters.AddWithValue("@AcomplishedFeeling", customerProfile.AcomplishedFeeling);
            cmd.Parameters.AddWithValue("@VibeWanted", customerProfile.VibeWanted);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            Response responseObj = new Response();
            responseObj.Message = "Succesfull";
            responseList.Add(responseObj);
            return responseList;
        }
    }
}
