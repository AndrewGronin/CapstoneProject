using System;

#nullable disable
namespace CapstoneProject.Schema.Queries
{
    public class SparePartsIssue
    {
        public SparePart SparePart { get; set; }
        
        public DateTime IssuingDateTime { get; set; }
        
        public string IssuedTo { get; set; }
        
        public ResponsibleEmployee IssuedBy { get; set; }
    }
}