namespace vehicle_price_backend_api.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public string VehicleType { get; set; }
        public int Mileage { get; set; }
        public DateTime PostedDate { get; set; }
        public int ManufacturedYear { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        public double Price { get; set; }
    }
}
