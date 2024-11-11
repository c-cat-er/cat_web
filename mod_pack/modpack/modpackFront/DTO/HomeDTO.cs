using modpackFront.Models;

namespace modpackFront.DTO
{
	public class HomeDTO
	{
        public List<ImageCarousel> ImageCarousel { get; set; }

		public List<Product> Product { get; set; }

		public List<Status> Statuses { get; set; }

        public List<Promotion> Promotion { get; set; }
    }
}
