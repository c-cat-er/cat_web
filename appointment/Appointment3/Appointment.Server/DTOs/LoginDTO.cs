namespace Appointment.Server.DTOs
{
    public class LoginDTO
    {
        public string? SHOPNO { get; set; }

        public required string USERID { get; set; }

        public required string PASSWD { get; set; }
    }
}
