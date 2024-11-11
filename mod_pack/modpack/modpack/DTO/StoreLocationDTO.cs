namespace modpackApi.DTO
{
    public class StoreLocationDTO
    {
        public int StoreLocationId { get; set; }

        public string Name { get; set; }

        public string OfficeTelephone { get; set; }

        public string Address { get; set; }

        public StoreLocationDTO()
        {
            Name = string.Empty;
            OfficeTelephone = string.Empty;
            Address = string.Empty;
        }
    }
}
