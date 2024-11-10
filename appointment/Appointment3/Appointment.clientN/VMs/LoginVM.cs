using System.ComponentModel;

namespace Appointment.clientN.VMs
{
    public class LoginVM
    {
        [DisplayName("分店")]
        public required string SHOPNO { get; set; }

        [DisplayName("帳號")]
        public required string USERID { get; set; }

        [DisplayName("密碼")]
        public required string PASSWD { get; set; }

        //k-p, 欲帶入的分店表
        public required Brstor Brstor { get; set; }

        //k-p, 帶入分店表轉為 list
        public IEnumerable<Brstor> StoreList { get; set; } = [];
    }
}
