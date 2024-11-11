namespace modpackApi.DTO
{
    public class CreditDTO
    {
        public int CreditId { get; set; }
        public int? MemberId { get; set; }
        public string HistoryName { get; set; }
        public int? IncomingAmount { get; set; }
        public int? UsageAmount { get; set; }
        public DateTime ModificationTime { get; set; }

    }
}
