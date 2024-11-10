using modpack.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace modpack.ViewModels
{
    public class CImageCarouselViewModel
    {
        public CImageCarouselViewModel() { }

        [DisplayName("序")]
        public int aImageCarouselID { get; set; }

        [DisplayName("照片")]
        [Required(ErrorMessage = "照片不可為空")]

        public string aImageFile { get; set; }

        public IFormFile photo { get; set; }

        [DisplayName("相關文字")]
        public string aImageDescription { get; set; }
    }
}
