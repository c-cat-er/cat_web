--1.在 master 資料庫中創建主密鑰和憑證
USE master;
GO

CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S3cur3P@ssw0rd!2024#';
GO

CREATE CERTIFICATE [NisekoCert]
WITH SUBJECT = 'My Database Field Encryption Certificate';
GO

--2.備份憑證並在需要的資料庫中創建
--備份憑證
BACKUP CERTIFICATE [NisekoCert]
TO FILE = 'C:\SecureBackup\NisekoCert.cer'
WITH PRIVATE KEY (
    FILE = 'C:\SecureBackup\NisekoCert.pvk',
    ENCRYPTION BY PASSWORD = 'S3cur3P@ssw0rd!2024#'
);
GO

--切換到 Niseko 資料庫
USE [Niseko];
GO

--創建資料庫主密鑰
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'NisekoStr0ngP@ssw0rd!2024$';
GO

--從備份中創建憑證
CREATE CERTIFICATE [NisekoCert]
FROM FILE = 'C:\SecureBackup\NisekoCert.cer'
WITH PRIVATE KEY (
    FILE = 'C:\SecureBackup\NisekoCert.pvk',
    DECRYPTION BY PASSWORD = 'S3cur3P@ssw0rd!2024#'
);
GO

--3.在 Niseko 資料庫中創建對稱密鑰並插入加密數據
--創建對稱密鑰
CREATE SYMMETRIC KEY [NisekoSymmetricKey]
WITH ALGORITHM = AES_256
ENCRYPTION BY CERTIFICATE [NisekoCert];
GO

--從此開始
/*C*/
USE [Niseko];
GO

CREATE TABLE [TDepartment] (
    [FDepartmentID] tinyint PRIMARY KEY IDENTITY(1,1),
    [FDepartmentName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TEmployee] (
    [FEmployeeID] int PRIMARY KEY IDENTITY(1,1),
    [FEmployeeCode] char(4) NOT NULL CHECK([FEmployeeCode] LIKE 'E%'), --'E'+2
    [FFirstName] nvarchar(50) NOT NULL,
    [FLastName] nvarchar(50) NOT NULL,
    [FDepartmentID] tinyint NOT NULL,
    [FPosition] nvarchar(50) NOT NULL,
    [FHireDate] date NOT NULL DEFAULT GETDATE(),
    [FFireDate] date MASKED WITH (FUNCTION = 'default()'),
    [FBirthDate] date NOT NULL,
	[FPhone] varchar(15) NOT NULL CHECK([FPhone] LIKE '[0-9]%[0-9]'), --不可為 null，故可設為 [0-9]*。設為 [0-9]* 無法插入整數值??
    [FEmail] varchar(50) NULL CHECK([FEmail] IS NULL OR [FEmail] LIKE '%@%'),
    [FAccount] varchar(20) UNIQUE NOT NULL,
    [FPasswordHash] varbinary(128) NOT NULL, --k-p, AES
    [FPasswordSalt] varbinary(128) NOT NULL, --k-p, AES
    [FPermissions] tinyint NOT NULL DEFAULT 1, --0黑名單 / 1一般 / 2初級
    CONSTRAINT [FK_TEmployee_FDepartmentID] FOREIGN KEY ([FDepartmentID]) REFERENCES [TDepartment]([FDepartmentID])
);

CREATE TABLE [TEmployeeLoginRecord] (
    [FLoginID] int PRIMARY KEY IDENTITY(1,1),
    [FEmployeeID] int NOT NULL,
    [FIPAddress] varchar(50), --開發時改為 NOT NULL
    [FLoginDatetime] Datetime NOT NULL DEFAULT GETDATE(),
    [FLogoutDatetime] Datetime,
    CONSTRAINT [FK_TEmployeeLoginRecord_FEmployeeID] FOREIGN KEY ([FEmployeeID]) REFERENCES [TEmployee]([FEmployeeID])
);

CREATE TABLE [TCoachLevel](
    [FCoachLevelID] tinyint PRIMARY KEY identity(1,1),
    [FLevelName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TCoach](
    [FCoachID] int PRIMARY KEY identity(1,1),
    [FIsPublished] bit NOT NULL DEFAULT 0,
    [FCoachCode] char(3) UNIQUE NOT NULL CHECK([FCoachCode] LIKE 'I%'), --'I'+2
    [FCoachName] nvarchar(50) NOT NULL,
    [FCoachLevelID] tinyint NOT NULL DEFAULT 1,
    [FHireDate] date NOT NULL DEFAULT GETDATE(),
    [FFireDate] date,
	[FPhone] varchar(15) NOT NULL,
    [FBirthDate] date NOT NULL,
    [FEmail] varchar(50) NULL CHECK([FEmail] IS NULL OR [FEmail] LIKE '%@%'),
    CONSTRAINT [FK_TCoach_FCoachLevelID] FOREIGN KEY ([FCoachLevelID]) REFERENCES [TCoachLevel]([FCoachLevelID])
);

CREATE TABLE [TCoachPrice](
    [FCoachPriceID] int PRIMARY KEY identity(1,1),
    [FCoachLevelID] tinyint NOT NULL,
    [FIsSixHours] bit NOT NULL,
    [FCoachPrice] decimal(10,2) NOT NULL,
    CONSTRAINT [FK_TCoachPrice_FCoachLevelID] FOREIGN KEY ([FCoachLevelID]) REFERENCES [TCoachLevel]([FCoachLevelID])
);

CREATE TABLE [TCoupon](
    [FCouponID] tinyint PRIMARY KEY identity(1,1),
    [FIsPublished] bit NOT NULL DEFAULT 0,
    [FCouponCode] char(3) UNIQUE NOT NULL CHECK([FCouponCode] LIKE 'P%'), --'P'+2
    [FCouponName] nvarchar(50) UNIQUE NOT NULL,
    [FStartDate] date,
    [FEndDate] date
);

CREATE TABLE [TMemberNon](
    [FMemberNonID] int PRIMARY KEY identity(1,1),
    [FMemberNonName] nvarchar(50) UNIQUE NOT NULL,
    [FCountryCode] varchar(10) NOT NULL, --程式裡外帶
    [FPhone] varchar(20) UNIQUE NOT NULL CHECK([FPhone] LIKE '[0-9]%[0-9]'),
    [FEmail] varchar(50) UNIQUE NULL CHECK([FEmail] IS NULL OR [FEmail] LIKE '%@%'),
    [FPermissions] tinyint NOT NULL DEFAULT 1
);

CREATE TABLE [TMemberSkiLevel](
    [FSkiLevelID] tinyint PRIMARY KEY identity(1,1),
    [FSkiLevelName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TMember](
    [FMemberID] int PRIMARY KEY identity(1,1),
    [FMemberCode] char(10) UNIQUE NOT NULL CHECK([FMemberCode] LIKE 'M%'), --'M' + 3英/數 + 6流水號
    [FSkiLevelID] tinyint NOT NULL DEFAULT 1,
    [FMemberName] nvarchar(50) NOT NULL,
    [FGender] char(1) NOT NULL DEFAULT 'N', --M(男) /F(女) /O(其他) / N(未添加)
    [FBirthdate] date,
    [FCountryCode] varchar(10), --程式裡外帶
    [FPhone] varchar(20) NULL CHECK([FPhone] IS NULL OR [FPhone] LIKE '[0-9]%[0-9]'), --k-p, 可為 null 或受約束的值，若設為 [0-9]* 則會變成不可插入 null 值
    [FEmail] varchar(50) NULL CHECK([FEmail] IS NULL OR [FEmail] LIKE '%@%'),
    [FPermissions] tinyint NOT NULL DEFAULT 1, --0黑名單 / 1一般 / 2初級
    CONSTRAINT [FK_TMember_FSkiLevelID] FOREIGN KEY ([FSkiLevelID]) REFERENCES [TMemberSkiLevel]([FSkiLevelID])
);

CREATE TABLE [TMemberLoginType](
    [FLoginTypeID] tinyint PRIMARY KEY identity(1,1),
    [FLoginTypeName] nvarchar(20) UNIQUE NOT NULL
);

CREATE TABLE [TMemberWebsiteAccount](
    [FWebsiteAccountID] int PRIMARY KEY identity(1,1),
    [FMemberID] int NOT NULL,
    [FAccount] varchar(20) UNIQUE NOT NULL, --AES
    [FPasswordHash] varbinary(128) NOT NULL, --AES
    [FPasswordSalt] varbinary(128) NOT NULL, --AES
    CONSTRAINT [FK_TMemberWebsiteAccount_FMemberID] FOREIGN KEY ([FMemberID]) REFERENCES [TMember]([FMemberID])
);

CREATE TABLE [TMemberThirdPartyAccount](
    [FThirdPartyAccountID] int PRIMARY KEY identity(1,1),
    [FMemberID] int NOT NULL,
    [FLoginTypeID] tinyint NOT NULL,
    [FThirdPartyUniqueID] varchar(300) NOT NULL,
    CONSTRAINT [FK_TMemberThirdPartyAccount_FMemberID] FOREIGN KEY ([FMemberID]) REFERENCES [TMember]([FMemberID]),
    CONSTRAINT [FK_TMemberThirdPartyAccount_FLoginTypeID] FOREIGN KEY ([FLoginTypeID]) REFERENCES [TMemberLoginType]([FLoginTypeID])
);

CREATE TABLE [TMemberLoginRecord](
    [FLoginRecordID] int PRIMARY KEY identity(1,1),
    [FMemberID] int NOT NULL,
    [FLoginTypeID] tinyint NOT NULL,
    [FLoginTimezone] varchar(50), --k-p,   --開發時改為 NOT NULL
    [FLoginIPAddress] varchar(50), --k-p,  --開發時改為 NOT NULL
    [FLoginDatetime] Datetime NOT NULL DEFAULT GETDATE(),
    [FLogoutDatetime] Datetime,
    CONSTRAINT [FK_TMemberLoginRecord_FMemberID] FOREIGN KEY ([FMemberID]) REFERENCES [TMember]([FMemberID]),
    CONSTRAINT [FK_TMemberLoginRecord_FLoginTypeID] FOREIGN KEY ([FLoginTypeID]) REFERENCES [TMemberLoginType]([FLoginTypeID])
);

CREATE TABLE [TVendor](
    [FVendorID] tinyint PRIMARY KEY identity(1,1),
    [FIsContact] bit NOT NULL DEFAULT 1,
    [FVendorCode] char(5) UNIQUE NOT NULL,
    [FVendorName] nvarchar(50) NOT NULL,
    [FContactName] nvarchar(20) NOT NULL,
    [FContactPhone] varchar(20) UNIQUE NOT NULL CHECK([FContactPhone] LIKE '[0-9]%[0-9]'), --此只考慮本地電話號
);

CREATE TABLE [TProductEvent](
    [FEventID] int PRIMARY KEY identity(1,1),
    [FIsPublished] bit NOT NULL DEFAULT 0,
    [FVendorID] tinyint NOT NULL,
    [FEventCode] char(4) UNIQUE NOT NULL CHECK([FEventCode] LIKE 'V%'),
    [FEventName] nvarchar(50) UNIQUE NOT NULL,
    [FStartDatetime] Datetime,
    [FEndDatetime] Datetime,
    [FEventPrice] decimal(10,2) NOT NULL,
    CONSTRAINT [FK_TProductEvent_FVendorID] FOREIGN KEY ([FVendorID]) REFERENCES [TVendor]([FVendorID])
);

CREATE TABLE [TLocation](
    [FLocationID] tinyint PRIMARY KEY identity(1,1),
    [FLocationName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TProductHomestay](
    [FHomestayID] int PRIMARY KEY identity(1,1),
    [FHomestayCode] char(6) UNIQUE NOT NULL CHECK([FHomestayCode] LIKE 'H%'),
    [FHomestayName] nvarchar(50) UNIQUE NOT NULL,
    [FDescription] nvarchar(200),
    [FAddressID] tinyint NOT NULL,
    CONSTRAINT [FK_TProductHomestay_FAddressID] FOREIGN KEY ([FAddressID]) REFERENCES [TLocation]([FLocationID])
);

CREATE TABLE [TProductHomestayRoom](
    [FHomestayRoomID] int PRIMARY KEY identity(1,1),
    [FIsPublished] bit NOT NULL DEFAULT 0,
    [FHomestayID] int NOT NULL,
    [FRoomCode] char(3) NOT NULL, --樓層房號
    [FBathroomCount] tinyint NOT NULL,
    [FToiletCount] tinyint NOT NULL,
    [FQueenBedCount] tinyint NOT NULL,
    [FSingleBedCount] tinyint NOT NULL,
    [FMaxCapacity] varchar(5) NOT NULL, --k-p, 10-15 (人)
    [FIsCleaned] bit NOT NULL, --TR1
    [FLastCleanDatetime] Datetime NOT NULL,
    CONSTRAINT [FK_TProductHomestayRoom_FHomestayID] FOREIGN KEY ([FHomestayID]) REFERENCES [TProductHomestay]([FHomestayID])
);

/*
CREATE TABLE [TProductHomestayImage](
	[FHomestayImageID] int PRIMARY KEY identity(1,1),
    [FHomestayRoomID] int NOT NULL,
	[FImage] varchar(20) NOT NULL,
	CONSTRAINT [FK_TProductHomestayImage_FHomestayRoomID] FOREIGN KEY ([FHomestayRoomID]) REFERENCES [TProductHomestayRoom]([FHomestayRoomID])
);*/

CREATE TABLE [TProductImage](
	[FProductImageID] int PRIMARY KEY identity(1,1),
    [FProductType] char(1) NOT NULL, --H:民宿 R:民宿房間 C:課程 U:教練 S:接送車 E:雪具設備 T:寄放
    [FProductID] int NOT NULL, --所有產品ID
	[FImage] varchar(20) NOT NULL,
);

CREATE TABLE [TProductHomestayPrice](
    [FHomestayPriceID] int PRIMARY KEY identity(1,1),
    [FHomestayRoomID] int NOT NULL,
    [FIsPeakSeason] bit NOT NULL,
    [FStayDays] tinyint NOT NULL,
    [FStayPrice] decimal(10,2) NOT NULL,
    CONSTRAINT [FK_TProductHomestayPrice_FHomestayRoomID] FOREIGN KEY ([FHomestayRoomID]) REFERENCES [TProductHomestayRoom]([FHomestayRoomID])
);

CREATE TABLE [TProductShuttle](
    [FShuttleID] tinyint PRIMARY KEY identity(1,1),
    [FIsPublished] bit NOT NULL DEFAULT 0,
    [FShuttleCode] char(4) UNIQUE NOT NULL CHECK([FShuttleCode] LIKE 'S%'),
    [FShuttleName] nvarchar(50) UNIQUE NOT NULL,
    [FMaxCapacity] tinyint NOT NULL
);

CREATE TABLE [TProductShuttlePrice](
    [FShuttlePriceID] int PRIMARY KEY identity(1,1),
    [FShuttleID] tinyint NOT NULL,
    [FPickupLocationID] tinyint NOT NULL,
    [FDropoffLocationID] tinyint NOT NULL,
    [FShuttlePrice] decimal(10,2) NOT NULL,
    CONSTRAINT [FK_TProductShuttlePrice_FShuttleID] FOREIGN KEY ([FShuttleID]) REFERENCES [TProductShuttle]([FShuttleID]),
    CONSTRAINT [FK_TProductShuttlePrice_FPickupLocationID] FOREIGN KEY ([FPickupLocationID]) REFERENCES [TLocation]([FLocationID]),
    CONSTRAINT [FK_TProductShuttlePrice_FDropoffLocationID] FOREIGN KEY ([FDropoffLocationID]) REFERENCES [TLocation]([FLocationID])
);

CREATE TABLE [TProductCourse](
    [FCourseID] int PRIMARY KEY identity(1,1),
    [FIsPublished] bit NOT NULL DEFAULT 0,
    [FCourseCode] char(4) UNIQUE NOT NULL CHECK([FCourseCode] LIKE 'C%'), -- C+3
    [FCourseName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TProductCoursePrice](
    [FCoursePriceID] int PRIMARY KEY identity(1,1),
    [FCourseID] int NOT NULL,
    [FCourseCombo] char(3) NOT NULL, --k-p, 1字元(single/double board) +1字元(3H/6H) +1字元(中文/英文/日文)
    [FCoursePrice] decimal(10,2) NOT NULL, --Daily
    CONSTRAINT [FK_TProductCoursePrice_FCourseID] FOREIGN KEY ([FCourseID]) REFERENCES [TProductCourse]([FCourseID])
);

CREATE TABLE [TProductEquipmentCategory](
    [FEquipmentCategoryID] tinyint PRIMARY KEY identity(1,1),
    [FEquipmentCategoryName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TProductEquipment](
    [FEquipmentID] int PRIMARY KEY identity(1,1),
    [FIsPublished] bit NOT NULL DEFAULT 0,
    [FVendorID] tinyint NOT NULL,
    [FEquipmentCategoryID] tinyint NOT NULL,
    [FEquipmentCode] char(5) UNIQUE NOT NULL CHECK([FEquipmentCode] LIKE 'Q%'),
    [FEquipmentName] nvarchar(50) UNIQUE NOT NULL,
    [FDescription] nvarchar(200),
    [FQuantity] int NOT NULL,
    [FEquipmentPrice] decimal(10,2) NOT NULL, --Daily
    [FRemarks] nvarchar(200),
    CONSTRAINT [FK_TProductEquipment_FVendorID] FOREIGN KEY ([FVendorID]) REFERENCES [TVendor]([FVendorID]),
    CONSTRAINT [FK_TProductEquipment_FEquipmentCategoryID] FOREIGN KEY ([FEquipmentCategoryID]) REFERENCES [TProductEquipmentCategory]([FEquipmentCategoryID])
);

CREATE TABLE [TProductStorage](
    [FStorageID] int PRIMARY KEY identity(1,1),
    [FEquipmentCategoryID] tinyint NOT NULL,
    [FStoragePrice] decimal(10,2) NOT NULL, --Daily
    CONSTRAINT [FK_TProductStorage_FEquipmentCategoryID] FOREIGN KEY ([FEquipmentCategoryID]) REFERENCES [TProductEquipmentCategory]([FEquipmentCategoryID])
);

CREATE TABLE [TTagType](
    [FTagTypeID] tinyint PRIMARY KEY identity(1,1),
    [FTypeName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TTag](
    [FTagID] int PRIMARY KEY identity(1,1),
    [FTagTypeID] tinyint NOT NULL,
    [FTagName] nvarchar(50) UNIQUE NOT NULL,
    CONSTRAINT [FK_TTag_FTagTypeID] FOREIGN KEY ([FTagTypeID]) REFERENCES [TTagType]([FTagTypeID])
);

CREATE TABLE [TProductTagMapping](--作法一 存類型和ID。作法二 存代碼。
    [FProductTagMappingID] int PRIMARY KEY identity(1,1),
    [FProductType] char(1) NOT NULL, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [FProductID] int NOT NULL, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
    [FProductCode] varchar(9) NOT NULL, --[民宿代碼] or [課程代碼]+[課程組合] or [接駁代碼] or [設備代碼] or [存儲代碼]
    [FTagID] int NOT NULL,
    CONSTRAINT [FK_TProductTagMapping_FTagID] FOREIGN KEY ([FTagID]) REFERENCES [TTag]([FTagID])
);

CREATE TABLE [TMemberShoppingCart]( --共用
    [FShoppingCartID] int PRIMARY KEY identity(1,1),
    [FMemberID] int NOT NULL,
    [FProductType] CHAR(1) NOT NULL, -- H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [FProductID] int NOT NULL, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
	[FPrice] decimal(8,2) NOT NULL,
    [FRemark] nvarchar(50), --可填上下車地點
    CONSTRAINT [FK_TMemberShoppingCart_FMemberID] FOREIGN KEY ([FMemberID]) REFERENCES [TMember]([FMemberID])
);

CREATE TABLE [TMemberShoppingCartHomestay]( --for H, S use this
    [FShoppingCartHomestayID] int PRIMARY KEY identity(1,1),
    [FShoppingCartID] int NOT NULL,
    [FPickupLocationID] tinyint, --可不接送
    [FDropoffLocationID] tinyint,
    [FStartDatetime] Datetime NOT NULL, --開發後改為 yyyymmddHHmm
    [FEndDatetime] Datetime NOT NULL,
    CONSTRAINT [FK_TMemberShoppingCartHomestay_FShoppingCartHomestayID] FOREIGN KEY ([FShoppingCartID]) REFERENCES [TMemberShoppingCart]([FShoppingCartID]),
    CONSTRAINT [FK_TMemberShoppingCartHomestay_FPickupLocationID] FOREIGN KEY ([FPickupLocationID]) REFERENCES [TLocation]([FLocationID]),
    CONSTRAINT [FK_TMemberShoppingCartHomestay_FDropoffLocationID] FOREIGN KEY ([FDropoffLocationID]) REFERENCES [TLocation]([FLocationID])
);

CREATE TABLE [TMemberShoppingCartCourse]( --for C, E, T use this
    [FShoppingCartCourseID] int PRIMARY KEY identity(1,1),
    [FShoppingCartID] int NOT NULL,
    [FLocationID] tinyint NOT NULL, -- 課程地點 or 租借地點 or 寄放地點
    [FDays] tinyint NOT NULL, --課程天數 or 租借天數 or 寄放天數
    [FPeopleCount] tinyint NOT NULL, -- 課程人數 or 租借數量 or 寄放數量
    CONSTRAINT [FK_TMemberShoppingCartCourse_FShoppingCartCourseID] FOREIGN KEY ([FShoppingCartID]) REFERENCES [TMemberShoppingCart]([FShoppingCartID]),
    CONSTRAINT [FK_TMemberShoppingCartCourse_FLocationID] FOREIGN KEY ([FLocationID]) REFERENCES [TLocation]([FLocationID])
);

CREATE TABLE [TOrderPaymentMethod](
    [FPaymentMethodID] tinyint PRIMARY KEY identity(1,1),
    [FPaymentMethodName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TOrder](
    [FOrderID] int PRIMARY KEY identity(1,1),
    [FOrderCode] char(10) UNIQUE NOT NULL CHECK([FOrderCode] LIKE 'O%'),
    [FMemberID] int NOT NULL,
    [FCreationDatetime] Datetime NOT NULL DEFAULT GETDATE(),
    [FInitialAmount] decimal(10,2) NOT NULL,
    [FDiscountCouponID] tinyint,
    [FFinalAmount] decimal(10,2) NOT NULL,
    [FIsCheckedIn] bit NOT NULL DEFAULT 0, --紀錄客戶是否如期赴約
    CONSTRAINT [FK_TOrder_FMemberID] FOREIGN KEY ([FMemberID]) REFERENCES [TMember]([FMemberID]),
    CONSTRAINT [FK_TOrder_FDiscountCouponID] FOREIGN KEY ([FDiscountCouponID]) REFERENCES [TCoupon]([FCouponID])
);

CREATE TABLE [TOrderDetail]( --共用
    [FOrderDetailID] int PRIMARY KEY identity(1,1),
    [FOrderID] int NOT NULL,
    [FProductType] char(1) NOT NULL, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [FProductID] int NOT NULL, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
    [FOrderDetailAmount] decimal(10,2) NOT NULL,
    [FRemarks] nvarchar(100), --可自填上下車地點
    CONSTRAINT [FK_TOrderDetail_FOrderID] FOREIGN KEY ([FOrderID]) REFERENCES [TOrder]([FOrderID])
);

CREATE TABLE [TOrderDetailHomestay]( --民宿 接送用此表
    [FOrderDetailHomestayID] int PRIMARY KEY identity(1,1),
    [FOrderDetailID] int NOT NULL,
    [FPickupLocationID] tinyint, --可不接送
    [FDropoffLocationID] tinyint,
    [FStartDatetime] Datetime NOT NULL, --開發後改為 yyyymmddHHmm
    [FEndDatetime] Datetime NOT NULL,
    CONSTRAINT [FK_TOrderDetailHomestay_FOrderDetailID] FOREIGN KEY ([FOrderDetailID]) REFERENCES [TOrderDetail]([FOrderDetailID]),
    CONSTRAINT [FK_TOrderDetailHomestay_FPickupLocationID] FOREIGN KEY ([FPickupLocationID]) REFERENCES [TLocation]([FLocationID]),
    CONSTRAINT [FK_TOrderDetailHomestay_FDropoffLocationID] FOREIGN KEY ([FDropoffLocationID]) REFERENCES [TLocation]([FLocationID])
);

CREATE TABLE [TOrderDetailCourse]( --課程 租借 寄放用此表
    [FOrderDetailCourseID] int PRIMARY KEY identity(1,1),
    [FOrderDetailID] int NOT NULL,
    [FLocationID] tinyint NOT NULL,
    [FDays] tinyint NOT NULL,
    [FPeopleCount] tinyint NOT NULL,
    CONSTRAINT [FK_TOrderDetailCourse_FOrderDetailID] FOREIGN KEY ([FOrderDetailID]) REFERENCES [TOrderDetail]([FOrderDetailID]),
    CONSTRAINT [FK_TOrderDetailCourse_FLocationID] FOREIGN KEY ([FLocationID]) REFERENCES [TLocation]([FLocationID])
);

CREATE TABLE [TOrderPaymentStatus](
    [FPaymentStatusID] tinyint PRIMARY KEY identity(1,1),
    [FPaymentStatusName] nvarchar(50) UNIQUE NOT NULL
);

CREATE TABLE [TOrderPaymentRecord](
    [FPaymentRecordID] int PRIMARY KEY identity(1,1),
    [FOrderID] int NOT NULL,
    [FPaymentStatusID] tinyint NOT NULL,
    [FPaymentMethodID] tinyint NOT NULL,
	[FPaymentDatetime] Datetime NOT NULL DEFAULT GETDATE(),
    [FPaymentAmount] decimal(10,2) NOT NULL,
    [FBankID] smallint, --開發時改為 NOT NULL
    [FCardholderName] nvarchar(50), --開發時改為 NOT NULL
    [FCardNumber] varchar(20), --開發時改為 NOT NULL
    [FCardExpiryYear] char(3), --民國年  --開發時改為 NOT NULL
    [FCardExpiryMonth] char(2), --開發時改為 NOT NULL
    CONSTRAINT [FK_TOrderPaymentRecord_FOrderID] FOREIGN KEY ([FOrderID]) REFERENCES [TOrder]([FOrderID]),
    CONSTRAINT [FK_TOrderPaymentRecord_FPaymentMethodID] FOREIGN KEY ([FPaymentMethodID]) REFERENCES [TOrderPaymentMethod]([FPaymentMethodID]),
    CONSTRAINT [FK_TOrderPaymentRecord_FPaymentStatusID] FOREIGN KEY ([FPaymentStatusID]) REFERENCES [TOrderPaymentStatus]([FPaymentStatusID])
);

CREATE TABLE [TMemberReview](
    [FReviewID] int PRIMARY KEY identity(1,1),
    [FMemberID] int NOT NULL,
	[FProductType] CHAR(1) NOT NULL, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [FProductID] int NOT NULL, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
    --[FProductCode] varchar(6) NOT NULL,
    [FReviewDate] Date,
    [FReviewContent] nvarchar(200),
    CONSTRAINT [FK_TMemberReview_FMemberID] FOREIGN KEY ([FMemberID]) REFERENCES [TMember]([FMemberID])
);

CREATE TABLE [TAuditLog] (
    [FAuditID] int PRIMARY KEY IDENTITY(1,1),
    [FTableName] varchar(50) NOT NULL,
    [FActionType] char(1) NOT NULL, --I(INSERT) / U(UPDATE) / D(DELETE)
    [FActionDatetime] Datetime NOT NULL DEFAULT GETDATE(),
    [FUserID] int NOT NULL,
    [FOldValue] nvarchar(300) NOT NULL,
    [FNewValue] nvarchar(300) NOT NULL
);
GO

/*Indexes*/------------------------------------------------------------
--1.查詢所有未離職員工資料
CREATE INDEX idx_ActiveEmployees_FFireDate ON [TEmployee]([FFireDate]);

--2.查詢所有已離職員工資料
CREATE INDEX idx_InactiveEmployees_FFireDate ON [TEmployee]([FFireDate]);

--3.查詢某員工ID的所有資料
CREATE INDEX idx_EmployeeByID_FEmployeeID ON [TEmployee]([FEmployeeID]);
GO



/*視圖和 SP*/------------------------------------------------------------
--1.顯示所有員工資料，並顯示關聯的部門名稱
CREATE VIEW [vwAllEmployees] AS
SELECT e.*, d.[FDepartmentName]
FROM [TEmployee] e
JOIN [TDepartment] d ON e.[FDepartmentID] = d.[FDepartmentID];
GO

--2.顯示所有未離職員工資料，並顯示關聯的部門名稱
SELECT *
FROM [vwAllEmployees]
WHERE [FFireDate] IS NULL;
GO

--3.顯示所有已離職員工資料，並顯示關聯的部門名稱
SELECT *
FROM [vwAllEmployees]
WHERE [FFireDate] IS NOT NULL;
GO

--4.顯示某員工ID的所有資料，並顯示關聯的部門名稱和登入紀錄
CREATE PROCEDURE [spGetEmployeeByID]
    @EmployeeID INT
AS
BEGIN
    SELECT *
	FROM [vwAllEmployees]
    WHERE [FEmployeeID] = @EmployeeID;
END
GO

--查詢所有員工的帳號密碼
--顯示所有員工的登入紀錄，並顯示關聯的會員資料和部門名稱
--5.顯示當前登入中員工，並顯示關聯的部門名稱
--查詢某員工ID的所有登入紀錄
--查詢所有未離職教練資料
--查詢所有已離職教練資料
--查詢某教練ID的所有資料

--查詢所有合作中廠商資料
--查詢某廠商ID的所有資料

--查詢所有民宿(房間)資料
--查詢所有已清潔的民宿(房間)資料
--查詢所有未清潔的民宿(房間)資料

--查詢所有接送服務資料
--查詢所有已開放的接送服務資料
--查詢所有未開放的接送服務資料

--查詢所有滑雪課程資料
--查詢所有已開放的滑雪課程資料
--查詢所有未開放的滑雪課程資料

--查詢所有雪具資料
--查詢所有已開放租借的雪具資料
--查詢所有未開放租借的雪具資料

--查詢所有寄放的資料

--查詢所有權限不為0的會員資料
--查詢所有會員的帳號密碼
--查詢當前登入中會員
--查詢某會員ID的所有個人資料
--查詢某會員ID的購物車資料
--查詢某會員ID的訂單資料
--查詢某會員ID的付款紀錄資料
--查詢某會員ID支付的卡號資料
--查詢某會員ID當月支付總金額
--查詢某會員ID當年支付總金額

--查詢所有訂單資料
--查詢當日所有訂單資料
--查詢當週所有訂單資料
--查詢當月所有訂單資料
--查詢當年所有訂單資料
--查詢所有未赴約訂單資料
--查詢當日所有訂單總金額
--查詢當週所有訂單總金額
--查詢當月所有訂單總金額
--查詢當年所有訂單總金額

--查詢所有評論資料
--查詢某會員ID評論資料

--級聯刪除或更新
--產品相關
--刪除 Product 記錄時，刪除相關的 ProductTagMapping 記錄。

--會員相關
--刪除 Member 記錄時，刪除相關的 MemberLoginRecord、MemberWebsiteAccount、MemberThirdPartyAccount 和 MemberReview 記錄：
ALTER TABLE [MemberLoginRecord]
ADD CONSTRAINT [FK_MemberLoginRecord_MemberID]
FOREIGN KEY ([MemberID]) REFERENCES [Member]([MemberID])
ON DELETE CASCADE;

ALTER TABLE [MemberWebsiteAccount]
ADD CONSTRAINT [FK_MemberWebsiteAccount_MemberID]
FOREIGN KEY ([MemberID]) REFERENCES [Member]([MemberID])
ON DELETE CASCADE;

ALTER TABLE [MemberThirdPartyAccount]
ADD CONSTRAINT [FK_MemberThirdPartyAccount_MemberID]
FOREIGN KEY ([MemberID]) REFERENCES [Member]([MemberID])
ON DELETE CASCADE;

ALTER TABLE [MemberReview]
ADD CONSTRAINT [FK_MemberReview_MemberID]
FOREIGN KEY ([MemberID]) REFERENCES [Member]([MemberID])
ON DELETE CASCADE;

--訂單相關
--刪除 Order 記錄時，刪除相關的 OrderDetail、OrderDetailHomestay、OrderDetailCourse 和 OrderPaymentRecord 記錄：
ALTER TABLE [OrderDetail]
ADD CONSTRAINT [FK_OrderDetail_OrderID]
FOREIGN KEY ([OrderID]) REFERENCES [Order]([OrderID])
ON DELETE CASCADE;

ALTER TABLE [OrderDetailHomestay]
ADD CONSTRAINT [FK_OrderDetailHomestay_OrderDetailID]
FOREIGN KEY ([OrderDetailID]) REFERENCES [OrderDetail]([OrderDetailID])
ON DELETE CASCADE;

ALTER TABLE [OrderDetailCourse]
ADD CONSTRAINT [FK_OrderDetailCourse_OrderDetailID]
FOREIGN KEY ([OrderDetailID]) REFERENCES [OrderDetail]([OrderDetailID])
ON DELETE CASCADE;

ALTER TABLE [OrderPaymentRecord]
ADD CONSTRAINT [FK_OrderPaymentRecord_OrderID]
FOREIGN KEY ([OrderID]) REFERENCES [Order]([OrderID])
ON DELETE CASCADE;
GO

/*SP*/------------------------------------------------------------
--會員登入驗證
CREATE PROCEDURE [usp_ValidateMemberLogin]
    @Account VARCHAR(50),
    @Password VARCHAR(50)
AS
BEGIN
    DECLARE @PasswordHash VARBINARY(64);
    DECLARE @PasswordSalt VARBINARY(128);
    DECLARE @ComputedHash VARBINARY(64);

    -- 從資料庫中獲取帳戶的密碼散列和加鹽
    SELECT @PasswordHash = [FPasswordHash], @PasswordSalt = [FPasswordSalt]
    FROM [TMemberWebsiteAccount]
    WHERE [FAccount] = @Account;

    -- 如果帳戶不存在，返回錯誤
    IF @PasswordHash IS NULL
    BEGIN
        RETURN -1; -- 帳戶不存在
    END

    -- 計算輸入密碼的散列
    SET @ComputedHash = HASHBYTES('SHA2_256', @Password + CONVERT(VARCHAR, @PasswordSalt, 1));

    -- 比較計算出的散列與存儲的散列
    IF @ComputedHash = @PasswordHash
    BEGIN
        RETURN 1; -- 驗證成功
    END
    ELSE
    BEGIN
        RETURN 0; -- 驗證失敗
    END
END;
GO

--三個月後自動刪除[MemberReview]表[ReviewTime]欄為空的資料
DELETE FROM [MemberReview]
WHERE [ReviewDatetime] IS NULL AND [ReviewID] < DATEADD(MONTH, -3, GETDATE());
GO

/*Trigger*/------------------------------------------------------------
--UpdateCleaningStatus 觸發器在處理多行插入/更新時可能會有問題。考慮使用 MERGE 語句或確保觸發器能正確處理多行的情況。
--若[OrderDetail]表[DetailType]欄為'A'時，就更新[ProductHomestayRoom]表該[ProductCode]的[IsCleaned]為0，
CREATE TRIGGER UpdateCleaningStatus
ON [OrderDetail]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON; --提高性能，減少客户端與服務器之間的消息交換
    IF EXISTS (SELECT 1 FROM inserted WHERE [DetailType] = 'H')
    BEGIN
        UPDATE [ProductHomestayRoom]
        SET [IsCleaned] = 0
        FROM [ProductHomestayRoom] INNER JOIN [OrderDetail]
        ON [ProductHomestayRoom].[RoomCode] = [OrderDetail].[ProductCode]
        WHERE [OrderDetail].[DetailType] = 'H';
    END
END;
GO


--虽然规范化能够减少数据冗余，但在某些查询频繁的情况下，反规范化可以提高查询性能。考虑在特定场景下进行适当的反规范化，以提高性能。


