using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBHB.Models
{
    public class CustomerProfile
    {
        public string UID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string FaceShapeID { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Insecurities { get; set; }
        public string Securities { get; set; }
        public string FavTV_Movies { get; set; }
        public string JobDesc { get; set; }
        public string AcomplishedFeeling { get; set; }
        public string VibeWanted { get; set; }
    }

    public class CustomerProfileRetrieve
    {
        public string UID { get; set; }
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string HairColor { get; set; }
        public string SkinColor { get; set; }
        public string EyeColor { get; set; }
        public string FaceShapeID { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Insecurities { get; set; }
        public string Securities { get; set; }
        public string FavTV_Movies { get; set; }
        public string JobDesc { get; set; }
        public string AcomplishedFeeling { get; set; }
        public string VibeWanted { get; set; }
        public string Photo { get; set; }
    }
}
