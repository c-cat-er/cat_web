USE [Niseko]
GO

/*創建表*/
create table [部門] (
    [部門ID] tinyint primary key identity(1,1),
    [部門名稱] nvarchar(50) UNIQUE not null
);

create table [員工] (
    [員工ID] int primary key identity(1,1),
	[員工代碼] char(4) NOT NULL CHECK([員工代碼] LIKE 'E%'), --'E'+2
    [姓氏] nvarchar(50) not null,
    [名字] nvarchar(50) not null,
    [部門ID] tinyint not null,
    [職位] nvarchar(50) not null,
    [雇用日期] date not null DEFAULT GETDATE(),
	[解雇日期] date,
    [生日] date not null,
	[信箱] varchar(50) CHECK ([信箱] LIKE '%@%'),
    [帳號] varchar(20) unique not null check (len([帳號]) between 4 and 20),
    [密碼哈希] varbinary(64) not null, --k-p,
    [密碼鹽] varbinary(128) not null, --k-p,
	[權限] tinyint not null default 1, --0黑名單 / 1一般 / 2初級
	constraint [FK_員工_部門ID] foreign key ([部門ID]) references [部門]([部門ID])
);

create table [員工登錄記錄] (
    [登錄ID] int primary key identity(1,1),
    [員工ID] int not null,
    [IP地址] varchar(50), --開發時改為 NOT NULL
    [登錄日期時間] datetime not null DEFAULT GETDATE(),
	[登出日期時間] datetime,
	constraint [FK_員工登錄記錄_員工ID] foreign key ([員工ID]) references [員工]([員工ID])
);

create table [教練級別](
    [教練級別ID] tinyint primary key identity(1,1),
    [級別名稱] nvarchar(50) UNIQUE not null
);

create table [教練](
    [教練ID] int primary key identity(1,1),
	[已發布] bit not null DEFAULT 0,
    [教練代碼] char(3) UNIQUE not null CHECK([教練代碼] LIKE 'O%'), --'O'+2
    [教練姓名] nvarchar(50) not null,
    [教練級別ID] tinyint not null DEFAULT 1,
	[雇用日期] date not null DEFAULT GETDATE(),
	[解雇日期] date,
    constraint [FK_教練_教練級別ID] foreign key ([教練級別ID]) references [教練級別]([教練級別ID])
);

create table [教練價格](
    [教練價格ID] int primary key identity(1,1),
    [教練級別ID] tinyint not null,
    [是否六小時] bit not null,
    [教練價格] decimal(10,2) not null,
    constraint [FK_教練價格_教練級別ID] foreign key ([教練級別ID]) references [教練級別]([教練級別ID])
);

create table [優惠券](
    [優惠券ID] tinyint primary key identity(1,1),
    [已發布] bit not null DEFAULT 0,
    [優惠券代碼] char(3) unique not null CHECK([優惠券代碼] LIKE 'P%'), --'P'+2
	[優惠券名稱] nvarchar(50) UNIQUE not null,
    [開始日期] date,
    [結束日期] date
);

------------------------

create table [非會員](
    [非會員ID] int primary key identity(1,1),
    [非會員名稱] nvarchar(50) unique not null,
    [國家代碼] varchar(10) not null, --程式裡外帶
    [電話] varchar(20) unique CHECK([電話] LIKE '[0-9]%'),
	[電子郵件] varchar(50) unique CHECK ([電子郵件] LIKE '%@%'),
	[權限] tinyint not null default 1
);

create table [會員滑雪級別](
    [滑雪級別ID] tinyint primary key identity(1,1),
    [滑雪級別名稱] nvarchar(50) unique not null
);

create table [會員](
    [會員ID] int primary key identity(1,1),
    [會員代碼] char(10) unique not null CHECK([會員代碼] LIKE 'M%'), --'M' + 3英/數 + 6流水號
    [滑雪級別ID] tinyint not null default 1,
    [會員名稱] nvarchar(50) not null,
    [性別] char(1) not null default 'N', --M(男) /F(女) /O(其他) / N(未添加)
    [生日] date,
    [國家代碼] varchar(10) not null, --程式裡外帶
    [電話] varchar(20) check([電話] like '[0-9]%'),
	[電子郵件] varchar(50) CHECK ([電子郵件] LIKE '%@%'),
	[權限] tinyint not null default 1, --0黑名單 / 1一般 / 2初級
    constraint [FK_會員_滑雪級別ID] foreign key ([滑雪級別ID]) references [會員滑雪級別]([滑雪級別ID])
);

create table [會員登錄類型](
    [登錄類型ID] tinyint primary key identity(1,1),
    [登錄類型名稱] nvarchar(20) unique not null
);

create table [會員網站帳號](
    [網站帳號ID] int primary key identity(1,1),
    [會員ID] int not null,
    [帳號] varchar(50) unique not null check (len([帳號]) between 4 and 20),
    [密碼哈希] varbinary(64) not null, --k-p, 
    [密碼鹽] varbinary(128) not null, --k-p, 
    constraint [FK_會員網站帳號_會員ID] foreign key ([會員ID]) references [會員]([會員ID])
);

create table [會員第三方帳號](
    [第三方帳號ID] int primary key identity(1,1),
    [會員ID] int not null,
    [登錄類型ID] tinyint not null,
    [第三方唯一ID] varchar(300) not null,
    constraint [FK_會員第三方帳號_會員ID] foreign key ([會員ID]) references [會員]([會員ID]),
    constraint [FK_會員第三方帳號_登錄類型ID] foreign key ([登錄類型ID]) references [會員登錄類型]([登錄類型ID])
);

create table [會員登錄記錄](
    [登錄記錄ID] int primary key identity(1,1),
    [會員ID] int not null,
    [登錄類型ID] tinyint not null,
    [登錄時區] varchar(50), --k-p,   --開發時改為 NOT NULL
    [登錄IP地址] varchar(50), --k-p,  --開發時改為 NOT NULL
    [登錄日期時間] datetime not null default getdate(),
    [登出日期時間] datetime,
    constraint [FK_會員登錄記錄_會員ID] foreign key ([會員ID]) references [會員]([會員ID]),
    constraint [FK_會員登錄記錄_登錄類型ID] foreign key ([登錄類型ID]) references [會員登錄類型]([登錄類型ID])
);

create table [供應商](
    [供應商ID] tinyint primary key identity(1,1),
	[還在聯絡] bit NOT NULL DEFAULT 1,
    [供應商代碼] char(5) unique not null,
    [供應商名稱] nvarchar(50) not null,
	[聯絡人名稱] nvarchar(20) not null,
	[聯絡人電話] varchar(20) UNIQUE not null check([聯絡人電話] like '[0-9]%'), --此只考慮本地電話號
);

create table [產品活動](
    [活動ID] int primary key identity(1,1),
    [已發布] bit not null DEFAULT 0,
    [供應商ID] tinyint not null,
    [活動代碼] char(4) unique not null CHECK([活動代碼] LIKE 'V%'),
    [活動名稱] nvarchar(50) unique not null,
    [開始日期時間] datetime,
    [結束日期時間] datetime,
    [活動價格] decimal(10,2) not null,
    constraint [FK_產品活動_供應商ID] foreign key ([供應商ID]) references [供應商]([供應商ID])
);

create table [地點](
    [地點ID] tinyint primary key identity(1,1),
    [地點名稱] nvarchar(50) unique not null
);

create table [產品民宿](
    [民宿ID] int primary key identity(1,1),
    [民宿代碼] char(6) unique not null CHECK([民宿代碼] LIKE 'H%'),
    [民宿名稱] nvarchar(50) unique not null,
    [描述] nvarchar(200),
    [地址ID] tinyint not null,
    constraint [FK_產品民宿_地址ID] foreign key ([地址ID]) references [地點]([地點ID])
);

create table [產品民宿房間](
    [民宿房間ID] int primary key identity(1,1),
    [已發布] bit not null DEFAULT 0,
    [民宿ID] int not null,
    [房間代碼] char(3) not null, --樓層房號
    [浴室數量] tinyint not null,
    [廁所數量] tinyint not null,
    [大床數量] tinyint not null,
    [單人床數量] tinyint not null,
    [最大容量] varchar(5) not null, --k-p, 10-15 (人)
	[已清潔] bit not null, --TR1
    [最後清潔日期時間] datetime not null,
    constraint [FK_產品民宿房間_民宿ID] foreign key ([民宿ID]) references [產品民宿]([民宿ID])
);

create table [產品民宿價格](
    [民宿價格ID] int primary key identity(1,1),
    [民宿房間ID] int not null,
    [是否旺季] bit not null,
    [入住天數] tinyint not null,
    [入住價格] decimal(10,2) not null,
    constraint [FK_產品民宿價格_民宿房間ID] foreign key ([民宿房間ID]) references [產品民宿房間]([民宿房間ID])
);

create table [產品接駁](
    [接駁ID] tinyint primary key identity(1,1),
	[已發布] bit not null DEFAULT 0,
    [接駁代碼] char(4) unique not null CHECK([接駁代碼] LIKE 'S%'),
    [接駁名稱] nvarchar(50) unique not null,
    [最大容量] tinyint not null
);

create table [產品接駁價格](
    [接駁價格ID] int primary key identity(1,1),
    [接駁ID] tinyint not null,
	--[地點組合] char(2) not null, --k-p, 1字元(上車地點ID) + 1字元(下車地點ID)，從 [地點] 取得
	[上車地點ID] tinyint NOT NULL,
	[下車地點ID] tinyint NOT NULL,
	[接駁價格] decimal(10,2) not null,
    constraint [FK_產品接駁價格_接駁ID] foreign key ([接駁ID]) references [產品接駁]([接駁ID]),
	constraint [FK_產品接駁價格_上車地點ID] FOREIGN KEY ([上車地點ID]) REFERENCES [地點]([地點ID]),
    constraint [FK_產品接駁價格_下車地點ID] FOREIGN KEY ([下車地點ID]) REFERENCES [地點]([地點ID])
);

create table [產品課程](
    [課程ID] int primary key identity(1,1),
	[已發布] bit not null DEFAULT 0,
    [課程代碼] char(4) unique not null CHECK([課程代碼] LIKE 'C%'),
    [課程名稱] nvarchar(50) unique not null
);

create table [產品課程價格](
    [課程價格ID] int primary key identity(1,1),
    [課程ID] int not null,
	[課程組合] char(3) not null, --k-p, 1字元(single/double board) +1字元(3H/6H) +1字元(中文/英文/日文)
	[課程價格] decimal(10,2) not null, --每日
	constraint [FK_產品課程價格_課程ID] foreign key ([課程ID]) references [產品課程]([課程ID])
);

create table [產品雪具類別](
    [雪具類別ID] tinyint primary key identity(1,1),
    [雪具類別名稱] nvarchar(50) unique not null
);

create table [產品雪具](
    [雪具ID] int primary key identity(1,1),
    [已發布] bit not null DEFAULT 0,
    [供應商ID] tinyint not null,
    [雪具類別ID] tinyint not null,
    [雪具代碼] char(5) unique not null CHECK([雪具代碼] LIKE 'Q%'),
    [雪具名稱] nvarchar(50) unique not null,
    [描述] nvarchar(200),
    [數量] int not null,
    [雪具價格] decimal(10,2) not null, --每日
    [備註] nvarchar(200), --如損壞
    constraint [FK_產品雪具_供應商ID] foreign key ([供應商ID]) references [供應商]([供應商ID]),
    constraint [FK_產品雪具_雪具類別ID] foreign key ([雪具類別ID]) references [產品雪具類別]([雪具類別ID])
);

create table [產品寄放](
    [寄放ID] int primary key identity(1,1),
	[雪具類別ID] tinyint not null,
    [寄放價格] decimal(10,2) not null, --每日
	constraint [FK_產品寄放_雪具類別ID] foreign key ([雪具類別ID]) references [產品雪具類別]([雪具類別ID])
);

create table [標籤類型](
    [標籤類型ID] tinyint primary key identity(1,1),
    [類型名稱] nvarchar(50) unique not null
);

create table [標籤](
    [標籤ID] int primary key identity(1,1),
    [標籤類型ID] tinyint not null,
    [標籤名稱] nvarchar(50) unique not null,
    constraint [FK_標籤_標籤類型ID] foreign key ([標籤類型ID]) references [標籤類型]([標籤類型ID])
);

create table [產品標籤映射](--作法一 存類型和ID。作法二 存代碼。
    [產品標籤映射ID] int primary key identity(1,1),
	[產品類型] char(1) not null,
	--H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [產品ID] int not null,
	--[民宿房間ID] or [課程代碼]+[課程組合] or [接駁ID] or [雪具ID] or [寄放ID]
    [產品代碼] varchar(9) not null,
	--[民宿代碼] or [課程代碼]+[課程組合] or [接駁代碼] or [雪具代碼] or [寄放代碼]
    [標籤ID] int not null,
    constraint [FK_產品標籤映射_標籤ID] foreign key ([標籤ID]) references [標籤]([標籤ID])
);

create table [會員購物車]( --共用
    [購物車ID] int primary key identity(1,1),
    [會員ID] int not null,
	[產品類型] char(1) not null, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [產品ID] int not null, --[民宿房間ID] or [課程代碼]+[課程組合] or [接駁ID] or [雪具ID] or [寄放ID]
    constraint [FK_會員購物車_會員ID] foreign key ([會員ID]) references [會員]([會員ID])
);

create table [會員購物車民宿]( --民宿 接送用
    [購物車民宿ID] int primary key identity(1,1),
	[購物車ID] int not null,
	[接送地點ID] tinyint, --可不接送
	[送達地點ID] tinyint,
    [開始日期時間] datetime not null,
    [結束日期時間] datetime not null,
	[備註] nvarchar(50), --可自填上下車地點
    constraint [FK_會員購物車民宿_購物車民宿ID] foreign key ([購物車民宿ID]) references [會員購物車]([購物車ID]),
	constraint [FK_會員購物車民宿_接送地點ID] foreign key ([接送地點ID]) references [地點]([地點ID]),
    constraint [FK_會員購物車民宿_送達地點ID] foreign key ([送達地點ID]) references [地點]([地點ID])
);

create table [會員購物車課程]( --課程 租借 寄放用
    [購物車課程ID] int primary key identity(1,1),
	[購物車ID] int not null,
	[地點ID] tinyint not null, -- 課程地點 or 租借地點 or 寄放地點
	[天數] tinyint not null, --課程天數 or 租借天數 or 寄放天數
	[人數] tinyint not null, -- 課程人數 or 租借數量 or 寄放數量
	[備註] nvarchar(50),
    constraint [FK_會員購物車課程_購物車課程ID] foreign key ([購物車課程ID]) references [會員購物車]([購物車ID]),
    constraint [FK_會員購物車課程_地點ID] foreign key ([地點ID]) references [地點]([地點ID])
);

create table [訂單付款方式](
    [付款方式ID] tinyint primary key identity(1,1),
    [付款方式名稱] nvarchar(50) unique not null
);

create table [訂單](
    [訂單ID] int primary key identity(1,1),
    [訂單代碼] char(10) unique not null CHECK([訂單代碼] LIKE 'O%'),
    [會員ID] int not null,
    [創建日期時間] datetime not null default getdate(),
    [初始金額] decimal(10,2) not null,
	[折扣優惠券ID] tinyint,
    [最終金額] decimal(10,2) not null,
    constraint [FK_訂單_會員ID] foreign key ([會員ID]) references [會員]([會員ID]),
    constraint [FK_訂單_折扣優惠券ID] foreign key ([折扣優惠券ID]) references [優惠券]([優惠券ID])
);

create table [訂單明細]( --共用
    [訂單明細ID] int primary key identity(1,1),
    [訂單ID] int not null,
	[產品類型] char(1) not null, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [產品ID] int not null, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
    constraint [FK_訂單明細_訂單ID] foreign key ([訂單ID]) references [訂單]([訂單ID])
);

create table [訂單明細民宿]( --民宿 接送用
	[訂單明細民宿ID] int primary key identity(1,1),
	[訂單明細ID] int not null,
	[接送地點ID] tinyint, --可不接送
	[送達地點ID] tinyint,
	[開始日期時間] datetime not null,
    [結束日期時間] datetime not null,
	[備註] nvarchar(100), --可自填上下車地點
	[是否報到] bit not null DEFAULT 0, --紀錄客戶是否如期赴約
	[訂單明細金額] decimal(10,2) not null,
    constraint [FK_訂單明細民宿_訂單明細ID] foreign key ([訂單明細ID]) references [訂單明細]([訂單明細ID]),
	constraint [FK_訂單明細民宿_接送地點ID] foreign key ([接送地點ID]) references [地點]([地點ID]),
    constraint [FK_訂單明細民宿_送達地點ID] foreign key ([送達地點ID]) references [地點]([地點ID])
);

create table [訂單明細課程]( --課程 租借 寄放用
	[訂單明細課程ID] int primary key identity(1,1),
	[訂單明細ID] int not null,
	[地點ID] tinyint not null,
	[天數] tinyint not null,
	[人數] tinyint not null,
	[備註] nvarchar(100), --可自填上下車地點
	[是否報到] bit not null DEFAULT 0, --紀錄客戶是否如期赴約
	[訂單明細金額] decimal(10,2) not null,
    constraint [FK_訂單明細課程_訂單明細ID] foreign key ([訂單明細ID]) references [訂單明細]([訂單明細ID]),
    constraint [FK_訂單明細課程_地點ID] foreign key ([地點ID]) references [地點]([地點ID])
);

create table [訂單付款狀態](
    [付款狀態ID] tinyint primary key identity(1,1),
    [付款狀態名稱] nvarchar(50) unique not null
);

create table [訂單付款記錄](
    [付款記錄ID] int primary key identity(1,1),
    [訂單ID] int not null,
    [付款方式ID] tinyint not null,
    [銀行ID] smallint, --開發時改為 NOT NULL
	[持卡人名稱] nvarchar(50), --開發時改為 NOT NULL
    [卡號] varchar(20), --開發時改為 NOT NULL
    [卡有效年] char(3), --民國年 --開發時改為 NOT NULL
    [卡有效月] char(2), --開發時改為 NOT NULL
    [付款狀態ID] tinyint not null,
    [付款日期時間] datetime not null default getdate(),
    [付款金額] decimal(10,2) not null,
    constraint [FK_訂單付款記錄_訂單ID] foreign key ([訂單ID]) references [訂單]([訂單ID]),
    constraint [FK_訂單付款記錄_付款方式ID] foreign key ([付款方式ID]) references [訂單付款方式]([付款方式ID]),
    constraint [FK_訂單付款記錄_付款狀態ID] foreign key ([付款狀態ID]) references [訂單付款狀態]([付款狀態ID])
);

create table [會員評論](
    [評論ID] int primary key identity(1,1),
    [會員ID] int not null,
	[產品類型] char(1) not null, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [產品ID] int not null, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
    --[產品代碼] varchar(6) not null,
    [評論日期時間] datetime,
    [評論內容] nvarchar(200),
    constraint [FK_會員評論_會員ID] foreign key ([會員ID]) references [會員]([會員ID])
);

create table [審計日志] ( --日誌表
    [審計ID] int primary key identity(1,1),
    [表名稱] varchar(50) not null,
    [操作類型] char(1) not null, --I(INSERT) / U(UPDATE) / D(DELETE)
    [操作日期時間] datetime not null default getdate(),
    [用戶ID] int not null,
    [舊值] nvarchar(300) not null,
    [新值] nvarchar(300) not null
);
GO
