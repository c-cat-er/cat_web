using System.ComponentModel;

namespace Appointment.clientN.VMs
{
    public class Brstor
    {
        [DisplayName("分店代號")]
        public required string CNUM { get; set; }

        [DisplayName("分店名稱")]
        public required string CNAME { get; set; }
    }
}
