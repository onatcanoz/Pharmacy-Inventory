namespace Pharmacy_Inventory.Models.Field
{
    public class Medicine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Stock { get; set; }
        public long Salary { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
