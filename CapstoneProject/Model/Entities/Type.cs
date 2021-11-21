using System;
using System.Collections.Generic;

#nullable disable

namespace CapstoneProject
{
    public partial class Type
    {
        public Type()
        {
            SpareParts = new HashSet<SparePart>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SparePart> SpareParts { get; set; }
    }
}
