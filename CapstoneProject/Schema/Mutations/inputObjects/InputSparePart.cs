namespace CapstoneProject.Schema.Mutations.inputObjects
{
    public class InputSparePart
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Manufacturer { get; set; }
        public int? Amount { get; set; }
        public int? DefectiveAmount { get; set; }
        public decimal? Price { get; set; }
        public int? TypeId { get; set; }
    }
}