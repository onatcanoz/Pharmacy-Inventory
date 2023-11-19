namespace Pharmacy_Inventory.Models
{
    public class UpdateMedicineViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Stock { get; set; }
        public long Salary { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
