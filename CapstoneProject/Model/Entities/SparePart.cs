using System;
using System.Collections.Generic;

#nullable disable

namespace CapstoneProject
{
    public class SparePart
    {
        public SparePart()
        {
            SparePartApplications = new HashSet<SparePartApplication>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Manufacturer { get; set; }
        public int Amount { get; set; }
        public int DefectiveAmount { get; set; }
        public decimal Price { get; set; }
        public int TypeId { get; set; }

        public virtual Type Type { get; set; }
        public virtual ICollection<SparePartApplication> SparePartApplications { get; set; }
    }
}
