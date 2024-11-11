using System.ComponentModel;

namespace Appointment.clientN.VMs
{
    public class CalendarVM
    {
        [DisplayName("會員編號")]
        public string? CNUM { get; set; }

        [DisplayName("日期")]
        public string? RDATE { get; set; }

        [DisplayName("類型")]
        public string ACUTE { get; set; } = string.Empty;

        [DisplayName("狀態")]
        public string CSTAT { get; set; } = string.Empty;

        [DisplayName("療程代號")]
        public string CURENO { get; set; } = string.Empty;

        [DisplayName("執行人")]
        public string USERID { get; set; } = string.Empty;

        [DisplayName("執行人姓名")]
        public string USERNAME { get; set; } = string.Empty;
    }
}
