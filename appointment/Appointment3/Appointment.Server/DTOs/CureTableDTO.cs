namespace Appointment.Server.DTOs
{
    public class CureTableDTO
    {
        public int SID { get; set; }
        public required string SERNO { get; set; }

        public required string SDATE { get; set; }

        public required string CURENO { get; set; }

        public required string CURENAME { get; set; }

        public decimal? SUBCNT { get; set; }

        public decimal? SUBQTY { get; set; }
    }
}
