using System;
using System.Collections.Generic;


namespace CapstoneProject.Schema.Mutations.inputObjects
{
    public class InputPartRequest
    {
        public DateTime DueDate { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IssuingDepartment IssuingDepartment { get; set; }

        public virtual ICollection<int> SparePartsIds { get; set; }
    }
}