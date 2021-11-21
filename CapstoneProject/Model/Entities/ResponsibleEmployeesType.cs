using System;
using System.Collections.Generic;

#nullable disable

namespace CapstoneProject
{
    public partial class ResponsibleEmployeesType
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int TypeId { get; set; }

        public virtual ResponsibleEmployee Employee { get; set; }
        public virtual Type Type { get; set; }
    }
}
