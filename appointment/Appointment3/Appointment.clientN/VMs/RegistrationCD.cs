using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Appointment.clientN.VMs
{
    public class RegistrationCD
    {
        [Key] ///主鍵
        [DisplayName("序")]
        public int WBID { get; set; }

        [DisplayName("掛號類型")]
        public string? ACUTE { get; set; }

        public string? CNUM { get; set; }

        [DisplayName("流水號")]
        public string? SERNO { get; set; }

        [DisplayName("銷售日")]
        public string? SDATE { get; set; }

        [DisplayName("療程代號")]
        public string? CURENO { get; set; }

        [DisplayName("預約日期")]
        public string? RDATE { get; set; }

        [DisplayName("預約時間")]
        public string? RTIME { get; set; }

        [DisplayName("掛號人編號")]
        public string? OPERUSER { get; set; }

        [DisplayName("執行人編號1")]
        public string? CDOC { get; set; }

        [DisplayName("執行人編號2")]
        public string? CDOCS { get; set; }

        [DisplayName("執行人編號3")]
        public string? CDOCP { get; set; }

        [DisplayName("購次")]
        public decimal? CURECNT { get; set; }

        [DisplayName("購數")]
        public decimal? CURECNTS { get; set; }

        [DisplayName("掛號狀態")]
        public string? CSTAT { get; set; }

        [DisplayName("狀態說明")]
        public string? CMESS { get; set; }

        [DisplayName("異動日期時間")]
        public string? CUPDATE { get; set; }
    }
}
