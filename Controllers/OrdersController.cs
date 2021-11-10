using HBHB.Data;
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
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace HBHB.Controllers
{
    public class OrdersController : Controller
    {
		private readonly ILogger<OrdersController> _logger;
		private readonly IHostingEnvironment _appEnvironment;
		public OrdersController(ILogger<OrdersController> logger, IHostingEnvironment appEnvironment)
		{
			_logger = logger;
			_appEnvironment = appEnvironment;

		}

		[HttpPost]
        [Route("api/createOrder")]
        public List<Response> createOrder([FromBody] OrderCreate orderObject)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            SqlConnection con = new SqlConnection(con2);
            SqlCommand cmd = new SqlCommand("InsertCustomerOrder", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", orderObject.UID);
            cmd.Parameters.AddWithValue("@PaymentID", orderObject.PaymentID);
            cmd.Parameters.AddWithValue("@PaymentAmount", orderObject.PaymentAmount);
            cmd.Parameters.AddWithValue("@CustomerID", orderObject.CustomerID);
            cmd.Parameters.AddWithValue("@OrderType", orderObject.OrderType);
            cmd.Parameters.AddWithValue("@NumberOfStyles", orderObject.NumberOfStyles);
            cmd.Parameters.AddWithValue("@MatAvoid", orderObject.MatAvoid);
            cmd.Parameters.AddWithValue("@Budget", orderObject.Budget);
            cmd.Parameters.AddWithValue("@Topic", orderObject.Topic);
            cmd.Parameters.AddWithValue("@Questions", orderObject.Questions);
            cmd.Parameters.AddWithValue("@EventType", orderObject.EventType);

            con.Open();
			int i = (int)cmd.ExecuteScalar();
			con.Close();
            Response responseObj = new Response();
            responseObj.Message = i.ToString();
            responseList.Add(responseObj);


			SqlCommand cmd2 = new SqlCommand("InsertCustomerOrderPayment", con);
			cmd2.CommandType = CommandType.StoredProcedure;
			cmd2.Parameters.AddWithValue("@UID", orderObject.UID);
			cmd2.Parameters.AddWithValue("@PaymentID", orderObject.PaymentID);
			cmd2.Parameters.AddWithValue("@PaymentAmount", orderObject.PaymentAmount);
			cmd2.Parameters.AddWithValue("@CustomerID", orderObject.CustomerID);
			cmd2.Parameters.AddWithValue("@OrderID", i.ToString());
			con.Open();
			int i2 = cmd2.ExecuteNonQuery();
			con.Close();

			return responseList;
        }

        [HttpGet]
        [Route("api/getCustomerOrders/{UID}")]
        public List<OrderGet> getCustomerOrders(string UID = "")
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<OrderGet> ordersList = new List<OrderGet>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetCustomerOpenOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", UID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    OrderGet order = new OrderGet();
					order.UID = (rdr["UID"].ToString());
					order.OrderID = (Convert.ToInt32(rdr["OrderID"]));
					order.CustomerID = (Convert.ToInt32(rdr["CustomerID"]));
					order.OrderType = (rdr["OrderType"].ToString());
					order.OrderStatus = (rdr["OrderStatus"].ToString());
					order.PaymentAmount = (rdr["PaymentAmount"].ToString());
					order.CardNumberLastFour = (rdr["CardNumber"].ToString());
					order.CardNumber = order.CardNumberLastFour.Substring(order.CardNumberLastFour.Length - 4);

					ordersList.Add(order);
                }
                con.Close();
            }
            return ordersList;
        }

		[HttpGet]
		[Route("api/getCustomerPrevOrders/{UID}")]
		public List<OrderGet> getCustomerPrevOrders(string UID = "")
		{
			db dbObj = new db();
			string con2 = dbObj.getConString();
			List<OrderGet> ordersList = new List<OrderGet>();
			using (SqlConnection con = new SqlConnection(con2))
			{
				SqlCommand cmd = new SqlCommand("GetCustomerPrevOrders", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@UID", UID);
				con.Open();
				SqlDataReader rdr = cmd.ExecuteReader();
				while (rdr.Read())
				{
					OrderGet order = new OrderGet();
					order.UID = (rdr["UID"].ToString());
					order.OrderID = (Convert.ToInt32(rdr["OrderID"]));
					order.CustomerID = (Convert.ToInt32(rdr["CustomerID"]));
					order.OrderType = (rdr["OrderType"].ToString());
					order.OrderStatus = (rdr["OrderStatus"].ToString());
					order.PaymentAmount = (rdr["PaymentAmount"].ToString());
					order.CardNumberLastFour = (rdr["CardNumber"].ToString());
					order.CardNumber = order.CardNumberLastFour.Substring(order.CardNumberLastFour.Length - 4);

					ordersList.Add(order);
				}
				con.Close();
			}
			return ordersList;
		}

		[HttpPost, DisableRequestSizeLimit]
        [Route("api/uploadGarmentPhotos/{UID}/{orderID}")]
        public async Task<ActionResult> UploadPhotosAsync(string UID = "", string orderID = "")
        {
			try
			{
				string folderName = "Images";
				string AssetsFolderPath = _appEnvironment.WebRootPath.ToString() + @"\MyStaticFiles";
				string newPath = Path.Combine(AssetsFolderPath, folderName);
				string newPath2 = newPath + @"\" + UID;
				string newPath3 = newPath + @"\" + UID + @"\Orders";
				string newPath4 = newPath + @"\" + UID + @"\Orders\" + orderID;

				if (!Directory.Exists(newPath))
				{
					Directory.CreateDirectory(newPath);
				}
				if (!Directory.Exists(newPath2))
				{
					Directory.CreateDirectory(newPath2);
				}
				if (!Directory.Exists(newPath3))
				{
					Directory.CreateDirectory(newPath3);
				}
				if (!Directory.Exists(newPath4))
				{
					Directory.CreateDirectory(newPath4);
				}

				db dbObj = new db();
				string con2 = dbObj.getConString();
				string test = "";
				var formCollection = await Request.ReadFormAsync();
				var file = formCollection.Files.First();

				if (file.Length > 0)
				{
					string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
					string fullPath = Path.Combine(newPath4, fileName);
					test = fullPath;
					using (var stream = new FileStream(fullPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}
					string newval = "MyStaticFiles/Images/" + UID + "/Orders/" + orderID + "/" + fileName;
					SqlConnection con = new SqlConnection(con2);
					SqlCommand cmd = new SqlCommand("AddGarmentPhoto", con);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("@UID", UID);
					cmd.Parameters.AddWithValue("@OrderID", orderID);
					cmd.Parameters.AddWithValue("@FileName", fileName);
					con.Open();
					int i2 = cmd.ExecuteNonQuery();
					con.Close();
				}
				return Ok(test);

			}
			catch (Exception ex)
			{
				return BadRequest("Upload Failed: " + ex.Message);
			}
		}
    }
}
