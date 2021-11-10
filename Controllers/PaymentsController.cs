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
    public class PaymentsController : ControllerBase
    {

        [HttpGet]
        [Route("api/getCustomerPayments/{UID}")]
        public List<PaymentMethods> getCustomerPayments(string UID = "")
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<PaymentMethods> payments = new List<PaymentMethods>();
            using (SqlConnection con = new SqlConnection(con2))
            {
                SqlCommand cmd = new SqlCommand("GetCustomerPayments", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UID", UID);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PaymentMethods payment = new PaymentMethods();
                    payment.PaymentID = (Convert.ToInt32(rdr["PaymentID"]));
                    payment.UID = (rdr["UID"].ToString());
                    payment.CustomerID = (Convert.ToInt32(rdr["CustomerID"]));
                    payment.FirstName = (rdr["FirstName"].ToString());
                    payment.LastName = (rdr["LastName"].ToString());
                    payment.CardNumber = (rdr["CardNumber"].ToString());
                    payment.CardNumberLastFour = payment.CardNumber.Substring(payment.CardNumber.Length - 4);
                    payment.CardMonth = (rdr["CardMonth"].ToString());
                    payment.CardYear = (rdr["CardYear"].ToString());
                    payment.CVS = (rdr["CVS"].ToString());
                    string paymentType = rdr["PaymentType"].ToString();
                    if (paymentType == "Primary")
                    {
                        payment.PaymentType = true;
                    }
                    else if (paymentType == "Secondary")
                    {
                        payment.PaymentType = false;
                    }
                    else {
                        payment.PaymentType = false;
                    }
                    payments.Add(payment);
                }
                con.Close();
            }
            return payments;
        }

        [HttpPost]
        [Route("api/addPaymentMethod")]
        public List<Response> addPaymentMethod([FromBody] PaymentMethod paymentMethod)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            SqlConnection con = new SqlConnection(con2);
            SqlCommand cmd = new SqlCommand("InsertCustomerPayment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", paymentMethod.UID);
            cmd.Parameters.AddWithValue("@CustomerID", paymentMethod.CustomerID);
            cmd.Parameters.AddWithValue("@FirstName", paymentMethod.FirstName);
            cmd.Parameters.AddWithValue("@LastName", paymentMethod.LastName);
            cmd.Parameters.AddWithValue("@CardNumber", paymentMethod.CardNumber);
            cmd.Parameters.AddWithValue("@CardMonth", paymentMethod.CardMonth);
            cmd.Parameters.AddWithValue("@CardYear", paymentMethod.CardYear);
            cmd.Parameters.AddWithValue("@CVS", paymentMethod.CVS);
            cmd.Parameters.AddWithValue("@PaymentType", paymentMethod.PaymentType);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            Response responseObj = new Response();
            responseObj.Message = "Succesfull";
            responseList.Add(responseObj);
            return responseList;
        }

        [HttpPost]
        [Route("api/updatePaymentMethod/{paymentID}")]
        public List<Response> updatePaymentMethod([FromBody] PaymentMethod paymentMethod , string paymentID)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            SqlConnection con = new SqlConnection(con2);
            SqlCommand cmd = new SqlCommand("UpdateCustomerPayment", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", paymentMethod.UID);
            cmd.Parameters.AddWithValue("@PaymentID", paymentID);
            cmd.Parameters.AddWithValue("@CustomerID", paymentMethod.CustomerID);
            cmd.Parameters.AddWithValue("@FirstName", paymentMethod.FirstName);
            cmd.Parameters.AddWithValue("@LastName", paymentMethod.LastName);
            cmd.Parameters.AddWithValue("@CardNumber", paymentMethod.CardNumber);
            cmd.Parameters.AddWithValue("@CardMonth", paymentMethod.CardMonth);
            cmd.Parameters.AddWithValue("@CardYear", paymentMethod.CardYear);
            cmd.Parameters.AddWithValue("@CVS", paymentMethod.CVS);
            cmd.Parameters.AddWithValue("@PaymentType", paymentMethod.PaymentType);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            Response responseObj = new Response();
            responseObj.Message = "Succesfull";
            responseList.Add(responseObj);
            return responseList;
        }

        [HttpGet]
        [Route("api/deletePayment/{paymentID}")]
        public List<Response> deletePayment(string paymentID)
        {
            db dbObj = new db();
            string con2 = dbObj.getConString();
            List<Response> responseList = new List<Response>();
            SqlConnection con = new SqlConnection(con2);
            SqlCommand cmd = new SqlCommand("DeletePaymentMethod", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PaymentID", paymentID);
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
