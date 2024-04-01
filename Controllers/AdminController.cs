using HBHB.Models;
using HBPOS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace HBHB.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {


        [HttpGet]
        [Route("api/checkUserAdmin/{UID}")]
        public async Task<ActionResult<string>> checkUserAdmin(string UID = "")
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            int i;
            List<CustomerProfileRetrieve> customer = new List<CustomerProfileRetrieve>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetUserAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", UID);
                con.Open();
                i = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return new JsonResult(new { isAdmin = i.ToString() });
        }

        [HttpGet]
        [Route("api/getOpenOrders")]
        public List<OrderGet> getOpenOrders()
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<OrderGet> ordersList = new List<OrderGet>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetOpenOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;
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
        [Route("api/getCompletedOrders")]
        public List<OrderGet> getCompletedOrders()
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<OrderGet> ordersList = new List<OrderGet>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetCompletedOrders", con);
                cmd.CommandType = CommandType.StoredProcedure;
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

        [HttpPost]
        [Route("api/completeOrder")]
        public List<Response> completeOrder([FromBody] OrderComplete orderObject)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            SqlConnection con = new SqlConnection(con2);
            SqlCommand cmd = new SqlCommand("MarkOrderComplete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderID", orderObject.OrderID);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            Response responseObj = new Response();
            responseObj.Message = "Succesfull";
            responseList.Add(responseObj);
            return responseList;
        }

        [HttpGet]
        [Route("api/getOrderDetails/{orderID}")]
        public List<OrderGetDetails> getOrderDetails(string orderID = "")
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<OrderGetDetails> ordersList = new List<OrderGetDetails>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetOrderDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    OrderGetDetails order = new OrderGetDetails();
                    order.UID = (rdr["UID"].ToString());
                    order.OrderID = (Convert.ToInt32(rdr["OrderID"]));
                    order.CustomerID = (Convert.ToInt32(rdr["CustomerID"]));
                    order.OrderType = (rdr["OrderType"].ToString());
                    order.NumberOfStyles = (rdr["NumberOfStyles"].ToString());
                    order.MatAvoid = (rdr["MatAvoid"].ToString());
                    order.Budget = (rdr["Budget"].ToString());
                    order.Topic = (rdr["Topic"].ToString());
                    order.Questions = (rdr["Questions"].ToString());
                    order.EventType = (rdr["EventType"].ToString());
                    order.OrderStatus = (rdr["OrderStatus"].ToString());
                    order.PaymentAmount = (rdr["PaymentAmount"].ToString());
                    order.CardNumber = (rdr["CardNumber"].ToString());
                    order.CardNumberLastFour = order.CardNumber.Substring(order.CardNumber.Length - 4);
                    order.FileName = (rdr["FileName"].ToString());

                    ordersList.Add(order);
                }
                con.Close();
            }
            return ordersList;
        }

    }
}
