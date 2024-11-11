using modpack.Models;
using System.ComponentModel;

namespace modpack.ViewModels
{
    public class CServiceRecordsViewModel
    {
        public CServiceRecordsViewModel() { }

        [DisplayName("序")]
        public int aRecordID { get; set; }

        [DisplayName("會員編")]
        public int aMemberID { get; set; }

        [DisplayName("會員姓名")]
        public string aMemberName { get; set; }

        [DisplayName("提問內容")]
        public string aQuestionContent { get; set; }

        [DisplayName("提問時間")]
        public DateTime? aQuestionTime { get; set; }

        [DisplayName("回覆員編")]
        public int? aAdminCode { get; set; }

        [DisplayName("回覆員姓名")]
        public string? aAdminName { get; set; }

        [DisplayName("回覆內容")]
        public string? aAnswerContent { get; set; }

        [DisplayName("回覆時間")]
        public DateTime? aAnswerTime { get; set; }

        [DisplayName("是否已解決")]
        public bool aIsResolved { get; set; }
    }
}
