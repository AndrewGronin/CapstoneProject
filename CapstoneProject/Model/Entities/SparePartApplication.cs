using System;
using System.Collections.Generic;

#nullable disable

namespace CapstoneProject
{
    public partial class SparePartApplication
    {
        public int Id { get; set; }
        public int SparePartId { get; set; }
        public DateTime? IssueDate { get; set; }
        public int CreationDate { get; set; }
        public string IssuedTo { get; set; }
        public int? RequestId { get; set; }
        public bool? Issued { get; set; }

        public virtual PartRequest Request { get; set; }
        public virtual SparePart SparePart { get; set; }
    }
}
