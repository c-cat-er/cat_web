using System.ComponentModel;

namespace modpack.ViewModels
{
    public class StoreLocationVM
    {
        [DisplayName("編號")]
        public int StoreLocationId { get; set; }

        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("電話")]
        public string OfficeTelephone { get; set; }

        [DisplayName("地址")]
        public string Address { get; set; }

        public StoreLocationVM()
        {
            Name = string.Empty;
            OfficeTelephone = string.Empty;
            Address = string.Empty;
        }
    }
}
