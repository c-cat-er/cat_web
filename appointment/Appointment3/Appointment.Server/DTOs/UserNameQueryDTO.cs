namespace Appointment.Server.DTOs
{
    public class UserNameQueryDTO
    {
        public required string OPERUSERName { get; set; }

        public required string CDOCName { get; set; }

        public string? CDOCSName { get; set; }

        public string? CDOCPName { get; set; }
    }
}
