using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace modpackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewebPayController : ControllerBase
    {
        [HttpPost("SendToNewebPay")]
        public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
        {
            try
            {
                var outModel = new SendToNewebPayOut();

                // 交易欄位
                var tradeInfo = new Dictionary<string, string>
                {
                    {"MerchantID", inModel.MerchantID},
                    {"RespondType", "String"},
                    {"TimeStamp", DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0)).ToUnixTimeSeconds().ToString()},
                    {"Version", "2.0"},
                    {"MerchantOrderNo", inModel.MerchantOrderNo},
                    {"Amt", inModel.Amt},
                    {"ItemDesc", inModel.ItemDesc},
                    {"ExpireDate", inModel.ExpireDate},
                    {"ReturnURL", inModel.ReturnURL},
                    {"NotifyURL", inModel.NotifyURL},
                    {"CustomerURL", inModel.CustomerURL},
                    {"ClientBackURL", inModel.ClientBackURL},
                    {"Email", inModel.Email},
                    {"EmailModify", "0"}
                };

                // 根據具體情況添加交易資料
                if (inModel.ChannelID == "CREDIT")
                {
                    tradeInfo.Add("CREDIT", "1");
                }
                else if (inModel.ChannelID == "VACC")
                {
                    tradeInfo.Add("VACC", "1");
                }
                // 將交易資料轉換為參數字串
                var tradeInfoParam = string.Join("&", tradeInfo);

                // 將交易資料進行 AES 加密
                var hashKey = "IfCNa3tkAxPHG8xfBJIjcO4GloTeGi28"; // API 串接金鑰
                var hashIV = "CHQYzNv5VWDSzsIP"; // API 串接密碼
                var tradeInfoEncrypt = EncryptAESHex(tradeInfoParam, hashKey, hashIV);

                // 交易資料 SHA256 加密
                outModel.MerchantID = inModel.MerchantID;
                outModel.Version = "2.0";
                outModel.TradeInfo = tradeInfoEncrypt;
                outModel.TradeSha = EncryptSHA256($"HashKey={hashKey}&{tradeInfoEncrypt}&HashIV={hashIV}");

                return Ok(outModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private string EncryptAESHex(string source, string cryptoKey, string cryptoIV)
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Encoding.UTF8.GetBytes(cryptoKey);
                aes.IV = Encoding.UTF8.GetBytes(cryptoIV);

                using (var encryptor = aes.CreateEncryptor())
                {
                    var encryptedBytes = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(source), 0, source.Length);
                    return BitConverter.ToString(encryptedBytes).Replace("-", string.Empty).ToLower();
                }
            }
        }

        private string EncryptSHA256(string source)
        {
            using (var algorithm = SHA256.Create())
            {
                var hashBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(source));
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToUpper();
            }
        }
    }

    public class SendToNewebPayIn
    {
        public string MerchantID { get; set; }
        public string MerchantOrderNo { get; set; }
        public string Amt { get; set; }
        public string ItemDesc { get; set; }
        public string ExpireDate { get; set; }
        public string ReturnURL { get; set; }
        public string CustomerURL { get; set; }
        public string NotifyURL { get; set; }
        public string ClientBackURL { get; set; }
        public string Email { get; set; }
        public string ChannelID { get; set; }
    }

    public class SendToNewebPayOut
    {
        public string MerchantID { get; set; }
        public string Version { get; set; }
        public string TradeInfo { get; set; }
        public string TradeSha { get; set; }
    }
}