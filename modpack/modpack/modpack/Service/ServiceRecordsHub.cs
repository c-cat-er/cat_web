using modpack.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace modpack.Service
{
    public class ServiceRecordsHub : Hub
    {
        private readonly ModPackContext _context;

        public ServiceRecordsHub(ModPackContext context)
        {
            _context = context;
        }

        public class SendMessageRequest
        {
            public int MemberId { get; set; }
            public string MemberName { get; set; }
            public string? Question { get; set; }
            public DateTime QuestionTime { get; set; }
        }

        public async Task SendMessage(SendMessageRequest requestData)
        {
            try
            {
                var newQuestion = new ServiceRecord
                {
                    MemberId = requestData.MemberId,
                    Question = requestData.Question,
                    QuestionTime = requestData.QuestionTime
                };
                _context.ServiceRecords.Add(newQuestion);
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("QuestionReceivedTwo", requestData.MemberId, requestData.Question, requestData.QuestionTime);
                await Clients.All.SendAsync("QuestionReceived", requestData.MemberId, requestData.Question, requestData.QuestionTime);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating images: {ex.Message}");
                await Clients.Caller.SendAsync("ErrorSubmittingMessage", ex.Message);
            }
        }

        public class AdminSendReply
        {
            public int MemberId { get; set; }
            public int AdminId { get; set; }
            public string? Answer { get; set; }
            public DateTime AnswerTime { get; set; }
            public bool IsResolved { get; set; }
        }

        public async Task SendAdminMessage(AdminSendReply requestAdminData)
        {
            try
            {
                var record = await _context.ServiceRecords
                    .Where(sr => sr.MemberId == requestAdminData.MemberId)
                    .OrderByDescending(sr => sr.QuestionTime)
                    .FirstOrDefaultAsync();

                if (record != null)
                {
                    record.AdministratorId = requestAdminData.AdminId;
                    record.Answer = requestAdminData.Answer;
                    var shanghaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai");
                    record.AnswerTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, shanghaiTimeZone);
                    record.IsResolved = requestAdminData.IsResolved;
                    await _context.SaveChangesAsync();
                    await Clients.All.SendAsync("AdminReplyReceived", requestAdminData.AdminId, requestAdminData.Answer, record.AnswerTime);
                }
                else
                {
                    await Clients.Caller.SendAsync("Error", "該會員沒有任何紀錄或新提問");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving admin reply: {ex.Message}");
                await Clients.Caller.SendAsync("ErrorSavingAdminReply", $"Error saving admin reply: {ex.Message}");
            }
        }
    }
}
