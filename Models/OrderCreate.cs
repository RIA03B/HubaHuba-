using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBHB.Models
{
    public class OrderCreate
    {
        public string UID { get; set; }
        public int PaymentID { get; set; }
        public string PaymentAmount { get; set; }
        public int CustomerID { get; set; }
        public string OrderType { get; set; }
        public string NumberOfStyles { get; set; }
        public string MatAvoid { get; set; }
        public string Budget { get; set; }
        public string Topic { get; set; }
        public string Questions { get; set; }
        public string EventType { get; set; }   
    }

    public class OrderGet
    {
        public int OrderID { get; set; }
        public string UID { get; set; }
        public int CustomerID { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentAmount { get; set; }
        public string CardNumber { get; set; }
        public string CardNumberLastFour { get; set; }

    }

    public class OrderComplete
    {
        public string OrderID { get; set; }
    }

    public class OrderGetDetails
    {
        public int OrderID { get; set; }
        public string UID { get; set; }
        public int CustomerID { get; set; }
        public string OrderType { get; set; }
        public string NumberOfStyles { get; set; }
        public string MatAvoid { get; set; }
        public string Budget { get; set; }
        public string Topic { get; set; }
        public string Questions { get; set; }
        public string EventType { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentAmount { get; set; }
        public string CardNumber { get; set; }
        public string CardNumberLastFour { get; set; }

        public string FileName { get; set; }

    }
}
