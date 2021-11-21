using System;
using System.Collections.Generic;

#nullable disable

namespace CapstoneProject
{
    public partial class PartRequest
    {
        public PartRequest()
        {
            SparePartApplications = new HashSet<SparePartApplication>();
        }

        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IssuingDepartment IssuingDepartment { get; set; }

        public virtual ICollection<SparePartApplication> SparePartApplications { get; set; }
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        Insurance
    }

    public enum IssuingDepartment
    {
        Auto,
        Moto,
        Shop
    }
}
