namespace Niseko.Server.DTOs
{
    public class MemberOrderHomestayDTO
    {
        // 1. db相關參數

        //TOrder
        public int FMemberID { get; set; }
        public decimal FInitialAmount { get; set; }
        //public byte? FDiscountCouponID { get; set; }
        public decimal FFinalAmount { get; set; }

        //TOrderDetail
        public required string FProductType { get; set; } //H, E, C, T, S
        public int FProductID { get; set; }
        public decimal FOrderDetailAmount { get; set; }
        public required string FRemarks { get; set; }

        //TOrderDetailHomestay
        public byte? FPickupLocationID { get; set; }
        public byte? FDropoffLocationID { get; set; }
        public required string FStartDatetime { get; set; }
        public required string FEndDatetime { get; set; }

        //TOrderPaymentRecord
        public byte FPaymentMethodID { get; set; }
        public DateOnly FPaymentDatetime { get; set; }
        public decimal FPaymentAmount { get; set; }

        // 2. 僅绿界或蓝星支付参数

        public required string MerchantID { get; set; }      // 商店代号 (不存db)
        public required string ItemDesc { get; set; }        // 商品描述
        public required string TradeInfo { get; set; }       // 交易资料 (AES-256 加密) (不存db)
        public required string TradeSha { get; set; }        // 交易杂凑 (SHA256) 用于验证交易数据的完整性 (不存db)
        public required string Version { get; set; }         // API 版本号 (不存db)
        public required string RespondType { get; set; }     // 回应格式 (JSON / XML) (不存db)
        public required string TimeStamp { get; set; }       // 时间戳 (不存db)

        // 绿界支付相关参数 (可选)
        //public string? ClientBackURL { get; set; }           // Client端返回商店的按钮链接 (可选)
        //public string? ItemURL { get; set; }                 // 商品销售网址 (可选)
        //public string? Remark { get; set; }                  // 备注栏位 (可选)
        //public string? OrderResultURL { get; set; }          // Client端回传付款结果网址 (可选)
        //public string? NeedExtraPaidInfo { get; set; }       // 是否需要额外的付款信息 (可选)
        //public string? IgnorePayment { get; set; }           // 隐藏付款方式 (可选)
        //public string? PlatformID { get; set; }              // 特约合作平台商代号 (可选)
        //public string? CustomField1 { get; set; }            // 自定义名称栏位1 (可选)
        //public string? CustomField2 { get; set; }            // 自定义名称栏位2 (可选)
        //public string? CustomField3 { get; set; }            // 自定义名称栏位3 (可选)
        //public string? CustomField4 { get; set; }            // 自定义名称栏位4 (可选)
        //public string? Language { get; set; }                // 语系设置 (可选)
    }
}
