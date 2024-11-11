using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Appointment.clientN.VMs
{
    public class RegistrationVM
    {
        [Key] ///主鍵
        [DisplayName("序")]
        public int WBID { get; set; }

        [DisplayName("會員編號")]
        public string? CNUM { get; set; }

        [DisplayName("類型")]
        public string? ACUTE { get; set; }

        [DisplayName("狀態")]
        public string? CSTAT { get; set; }

        [DisplayName("療程代號")]
        public string? CURENO { get; set; }

        [DisplayName("療程名稱")]
        public string? CURENAME { get; set; }

        [DisplayName("執行人")]
        public string? USERID { get; set; }

        [DisplayName("執行人姓名")]
        public string? USERNAME { get; set; }

        [DisplayName("流水號")]
        public string? SERNO { get; set; }

        [DisplayName("銷售日期")]
        public string? SDATE { get; set; }

        [DisplayName("日期")]
        public string? RDATE { get; set; }

        [DisplayName("時間")]
        public string? RTIME { get; set; }

        [DisplayName("掛號人")]
        public string? OPERUSER { get; set; }

        [DisplayName("執行人1")]
        public string? CDOC { get; set; }

        [DisplayName("執行人2")]
        public string? CDOCS { get; set; }

        [DisplayName("執行人3")]
        public string? CDOCP { get; set; }

        [DisplayName("購次")]
        public decimal? CURECNT { get; set; }

        [DisplayName("購數")]
        public decimal? CURECNTS { get; set; }

        [DisplayName("異動日期時間")]
        public string? CUPDATE { get; set; }
    }
}
