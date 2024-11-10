using System.ComponentModel;

namespace Appointment.clientN.VMs
{
    public class QueryCureVM
    {
        public int SID { get; set; }

        [DisplayName("流水號")]
        public string? SERNO { get; set; }

        [DisplayName("銷售日")]
        public string? SDATE { get; set; }

        [DisplayName("療程代號")]
        public string? CURENO { get; set; }

        [DisplayName("療程名稱")]
        public string? CURENAME { get; set; }

        [DisplayName("剩數")]
        public decimal? SUBQTY { get; set; }

        [DisplayName("剩次")]
        public decimal? SUBCNT { get; set; }
    }
}
