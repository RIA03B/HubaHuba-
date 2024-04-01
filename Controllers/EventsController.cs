using HBHB.Models;
using HBPOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
namespace HBHB.Controllers
{
    [Authorize]
    [ApiController]
    public class EventsController : ControllerBase
    {


        [HttpGet]
        [Route("api/getEvent/{UID}")]
        public List<CustomerProfileRetrieve> getEvent(string UID = "")
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
    }
}
