namespace Appointment.clientN.DTOs
{
    public class RegistrationDTO
    {
        public required int WBID { get; set; }

        public required string ACUTE { get; set; }

        public string? SERNO { get; set; }

        public string? SDATE { get; set; }

        public string? CURENO { get; set; }

        public required string RDATE { get; set; }

        public required string RTIME { get; set; }

        public required string OPERUSER { get; set; }

        public required string CDOC { get; set; }

        public string? CDOCS { get; set; }

        public string? CDOCP { get; set; }

        public decimal? CURECNT { get; set; }

        public decimal? CURECNTS { get; set; }

        public required string CSTAT { get; set; }

        public string? CMESS { get; set; }

        public string? CUPDATE { get; set; }
    }
}
