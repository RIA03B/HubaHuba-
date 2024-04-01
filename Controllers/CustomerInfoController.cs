using HBHB.Models;
using HBPOS.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http.Headers;

namespace HBHB.Controllers
{
    [Authorize]
    [ApiController]
    public class CustomerInfoController : ControllerBase
    {


        private readonly ILogger<CustomerInfoController> _logger;
        private readonly IHostingEnvironment _appEnvironment;
        public CustomerInfoController(ILogger<CustomerInfoController> logger, IHostingEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;

        }

        [HttpGet]
        [Route("api/CheckCustomerProfile/{UID}")]
        public List<Response> CheckCustomerProfile(string UID)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("[dbo].[CheckCustomerProfile]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", UID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Response responseObj = new Response();
                    responseObj.Message = (rdr["Message"].ToString());
                    responseList.Add(responseObj);
                }
                con.Close();
            }
            return responseList;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("api/uploadPhoto/{UID}")]
        public ActionResult UploadPhoto(string UID)
        {
            try
            {
                db dbObj = new db();
                string con2 = dbObj.getConString();
                string test = "";
                var file = Request.Form.Files[0];
                string folderName = "Images";
                string AssetsFolderPath = _appEnvironment.WebRootPath.ToString() + @"\MyStaticFiles";
                string newPath = Path.Combine(AssetsFolderPath, folderName);
                string newPath2 = newPath + @"\" + UID;
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (!Directory.Exists(newPath2))
                {
                    Directory.CreateDirectory(newPath2);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = Path.Combine(newPath2, fileName);
                    test = fullPath;
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    string newval = "MyStaticFiles/Images/" + UID + "/" + fileName;

                    SqlConnection con = new SqlConnection(con2);
                    SqlCommand cmd = new SqlCommand("UpdatePhoto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UID", UID);
                    cmd.Parameters.AddWithValue("@NewPhoto", newval);
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                }

                return Ok(test);
            }
            catch (Exception ex)
            {
                return BadRequest("Upload Failed: " + ex.Message);
            }


        }



        [HttpPost]
        [Route("api/InsertCustomerProfile")]
        public List<Response> InsertCustomerProfile([FromBody] CustomerProfile customerProfile)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            SqlConnection con = new SqlConnection(con2);
            SqlCommand cmd = new SqlCommand("InsertCustomerProfile", con);
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
