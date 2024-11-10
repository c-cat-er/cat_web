using System.ComponentModel;

namespace Appointment.clientN.VMs
{
    public class QueryVM
    {
        [DisplayName("會員編號 *")]
        public required string CNUM { get; set; }

        [DisplayName("姓名 *")]
        public required string CNAME { get; set; }

        [DisplayName("生日 *")]
        public string? CBIRTH { get; set; }

        [DisplayName("手機 *")]
        public string? CMOBILE { get; set; }

        //k-p, 帶入查詢結果 VM
        public IEnumerable<QueryCureVM> Results { get; set; }


        [DisplayName("預約類型 *")]
        public required string ACUTE { get; set; }

        [DisplayName("流水號")]
        public string? SERNO { get; set; }

        [DisplayName("銷售日期")]
        public string? SDATE { get; set; }

        [DisplayName("療程選項")]
        public string? CURENO { get; set; }

        [DisplayName("預約日期 *")]
        public required string RDATE { get; set; }

        [DisplayName("預約時間 *")]
        public required string RTIME { get; set; }

        [DisplayName("掛號人代號")]
        public required string OPERUSER { get; set; }

        [DisplayName("執行人1 *")]
        public required string CDOC { get; set; }

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

        [DisplayName("備註")]
        public string? CMESS { get; set; }
    }
}
