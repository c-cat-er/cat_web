using System.ComponentModel;

namespace Appointment.Server.DTOs
{
    public class CalendarDTO
    {
        public int WBID { get; set; }

        [DisplayName("掛號類型")]
        public string ACUTE { get; set; }

        [DisplayName("會員號")]
        public string CNUM { get; set; }

        [DisplayName("療程代號")]
        public string CURENO { get; set; }

        public string CURENAME { get; set; }

        [DisplayName("預約日期")]
        public string RDATE { get; set; }

        [DisplayName("預約時間")]
        public string RTIME { get; set; }

        [DisplayName("掛號人員代號")]
        public string OPERUSER { get; set; }

        [DisplayName("執行人員代號1")]
        public string CDOC { get; set; }

        [DisplayName("執行人員代號2")]
        public string CDOCS { get; set; }

        [DisplayName("執行人員代號3")]
        public string CDOCP { get; set; }

        [DisplayName("預約狀態")]
        public string CSTAT { get; set; }

        [DisplayName("購次")]
        public decimal? CURECNT { get; set; }

        [DisplayName("購數")]
        public decimal? CURECNTS { get; set; }

        [DisplayName("異動日期時間")]
        public string? CUPDATE { get; set; }
    }
}
