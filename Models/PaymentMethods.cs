namespace HBHB.Models
{
    public class PaymentMethods
    {
        public int PaymentID { get; set; }
        public string UID { get; set; }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public string CardNumberLastFour { get; set; }
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
        public string CVS { get; set; }
        public bool PaymentType { get; set; }

    }

    public class PaymentMethod
    {
        public string UID { get; set; }
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public string CardMonth { get; set; }
        public string CardYear { get; set; }
        public string CVS { get; set; }
        public string PaymentType { get; set; }

    }
}
