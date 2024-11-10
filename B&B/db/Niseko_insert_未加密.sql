USE [Niseko];
GO

INSERT INTO [TDepartment]
    ([FDepartmentName])
VALUES 
    (N'Human Resources'), (N'Finance'), (N'IT'), (N'Sales');
GO

INSERT INTO [TEmployee]
    ([FEmployeeCode], [FFirstName], [FLastName], [FDepartmentID], [FPosition],
    [FHireDate], [FFireDate], [FBirthDate], [FPhone], [FEmail], [FAccount], [FPasswordHash],
    [FPasswordSalt], [FPermissions])
VALUES
    ('E001', N'John', N'Doe', 1, N'Manager', GETDATE(), NULL, '1980-01-01', '0900111222',
        'john.doe@example.com', 'admintest1',
        HASHBYTES('SHA2_256', 'password'), CAST(NEWID() AS VARBINARY(128)), 1),
    ('E002', N'Jane', N'Smith', 2, N'Analyst', GETDATE(), NULL, '1985-05-15', '0900111223',
        'jane.smith@example.com', 'admintest2',
        HASHBYTES('SHA2_256', 'password'), CAST(NEWID() AS VARBINARY(128)), 1),
    ('E003', N'Robert', N'Brown', 3, N'Developer', GETDATE(), NULL, '1990-08-22', '0900111224',
        'robert.brown@example.com', 'admintest3',
        HASHBYTES('SHA2_256', 'password'), CAST(NEWID() AS VARBINARY(128)), 1),
    ('E004', N'Emily', N'Davis', 4, N'Sales Rep', GETDATE(), NULL, '1992-12-03', '0900111252',
        'emily.davis@example.com', 'admintest4',
        HASHBYTES('SHA2_256', 'password'), CAST(NEWID() AS VARBINARY(128)), 1);
GO

INSERT INTO [TEmployeeLoginRecord]
    ([FEmployeeID], [FIPAddress], [FLoginDatetime], [FLogoutDatetime])
VALUES
    (1, '192.168.1.1', GETDATE(), NULL),
    (2, '192.168.1.2', GETDATE(), NULL),
    (3, '192.168.1.3', GETDATE(), NULL),
    (4, '192.168.1.4', GETDATE(), NULL);
GO

INSERT INTO [TCoachLevel]
    ([FLevelName])
VALUES
    (N'第一級'), (N'第二級'), (N'第三級');
GO

INSERT INTO [TCoach]
    ([FIsPublished], [FCoachCode], [FCoachName], [FCoachLevelID], [FHireDate],
    [FFireDate], [FPhone], [FBirthDate], [FEmail])
VALUES
    (0, 'I01', N'教練一', 1, GETDATE(), NULL, '0912345678', '1980-01-01', 'coach1@example.com'),
    (1, 'I02', N'教練二', 2, GETDATE(), NULL, '0912345679', '1985-01-01', 'coach2@example.com');
GO

INSERT INTO [TCoachPrice]
    ([FCoachLevelID], [FIsSixHours], [FCoachPrice])
VALUES
    (1, 0, 3000.00),
    (1, 1, 5000.00),
    (2, 0, 6000.00),
    (2, 1, 10000.00),
    (3, 0, 10000.00),
    (3, 1, 20000.00);
GO

INSERT INTO [TMemberSkiLevel]
    ([FSkiLevelName])
VALUES
    (N'從未滑過雪'),
    (N'可以八字煞車，尚未學習轉彎'),
    (N'可以在綠線(初級雪道)連續八字轉彎'),
    (N'可以在紅線(中級雪道)連續八字轉彎'),
    (N'可以在紅線(中級雪道)連續併腿轉彎'),
    (N'可以在黑線(高級雪道)穩定併腿轉彎'),
    (N'可以單邊側滑'),
    (N'可以用兩側的板緣落葉飄'),
    (N'可以在綠線(初級雪道)連續轉彎'),
    (N'可以在紅線(中級雪道)連續轉彎'),
    (N'可以在黑線(高級雪道)連續轉彎');
GO

INSERT INTO [TMember]
    ([FMemberCode], [FSkiLevelID], [FMemberName], [FGender], [FBirthdate],
    [FCountryCode], [FPhone], [FEmail], [FPermissions])
VALUES
    ('M32R541954', 1, N'會員姓名一', 'M', '2000-01-01', 'JP', '0311223344', 'member1@example.com', 1),
    ('MS0A136874', 2, N'會員姓名二', 'F', '2018-08-01', 'JP', '0311223345', 'member2@example.com', 1);
GO

INSERT INTO [TMemberLoginType]
    ([FLoginTypeName])
VALUES
    (N'官網'), (N'Google'), (N'Microsoft'), (N'Apple'), (N'Facebook'), (N'Instagram');
GO

INSERT INTO [TMemberWebsiteAccount]
    ([FMemberID], [FAccount], [FPasswordHash], [FPasswordSalt])
VALUES
    --(1, CAST('account1' AS VARBINARY(128)), HASHBYTES('SHA2_256', 'password1'), CAST(NEWID() AS VARBINARY(128))),
    --(2, CAST('account2' AS VARBINARY(128)), HASHBYTES('SHA2_256', 'password2'), CAST(NEWID() AS VARBINARY(128)));
    (1, 'testtest1', HASHBYTES('SHA2_256', 'testtest1'), CAST(NEWID() AS VARBINARY(128))),
    (2, 'testtest2', HASHBYTES('SHA2_256', 'testtest2'), CAST(NEWID() AS VARBINARY(128)));
GO

INSERT INTO [TMemberThirdPartyAccount]
    ([FMemberID], [FLoginTypeID], [FThirdPartyUniqueID])
VALUES
    (1, 2, 'UQ1......'), -- 使用 Google 登入
    (1, 3, 'UQ2......'), -- 使用 Microsoft 登入
    (2, 4, 'UQ3......'), -- 使用 Apple 登入
    (2, 5, 'UQ4......'); -- 使用 Facebook 登入
GO

INSERT INTO [TMemberLoginRecord]
    ([FMemberID], [FLoginTypeID], [FLoginTimezone], [FLoginIPAddress], [FLoginDatetime], [FLogoutDatetime])
VALUES
    (1, 1, 'Asia/Tokyo', '192.168.1.1', '2024-05-13 08:30:00', '2024-05-13 14:00:00'),
    -- 使用官網登入
    (1, 2, 'America/New_York', '192.168.1.2', '2024-05-14 12:00:00', NULL); -- 使用 Apple 登入
GO

INSERT INTO [TVendor]
    ([FIsContact], [FVendorCode], [FVendorName], [FContactName], [FContactPhone])
VALUES
    (1, 'VPQAW', N'活動廠商', N'聯絡人甲', '0230000000'),
    (1, 'VSKLW', N'器具品廠商', N'聯絡人乙', '0930000000');
GO

INSERT INTO [TProductEvent]
    ([FIsPublished], [FVendorID], [FEventCode], [FEventName], [FStartDatetime], [FEndDatetime], [FEventPrice])
VALUES
    (0, 1, 'V01', N'雪地摩托體驗', NULL, NULL, 20.00),
    (1, 1, 'V02', N'夜間滑雪', '2024-05-22 17:00:00', '2024-05-22 20:00:00', 20.00);
GO

INSERT INTO [TLocation]
    ([FLocationName])
VALUES
    (N'其他地點'),
    (N'北海道虻田郡倶知安町北7条東7丁目1-34'),
    (N'北海道虻田郡倶知安町南4条東4丁目1-15'),
    (N'北海道虻田郡倶知安町南2条西1丁目20-1'),
    (N'機場接送地點'),
    (N'火車站接送地點');
GO

INSERT INTO [TProductShuttle]
    ([FShuttleCode], [FShuttleName], [FMaxCapacity])
VALUES
    ('S00E', N'接駁車一號(夏季)', 6),
    ('SLPL', N'接駁車二號(冬季)', 6);
GO

INSERT INTO [TProductShuttlePrice]
    ([FShuttleID], [FPickupLocationID], [FDropoffLocationID], [FShuttlePrice])
VALUES
    (1, 1, 2, 30.00),
    (2, 1, 2, 30.00);
GO

INSERT INTO [TProductHomestay]
    ([FHomestayCode], [FHomestayName], [FDescription], [FAddressID])
VALUES
    ('HPL4IQ', N'Pink House', N'4間洋房、一間和室', 2),
    ('HPLD12', N'Lucky House', N'一衛一廁', 3),
    ('HPLD32', N'Sapphire House', N'洋房', 4);
GO

INSERT INTO [TProductHomestayRoom]
    ([FIsPublished], [FHomestayID], [FRoomCode], [FBathroomCount], [FToiletCount], [FQueenBedCount], [FSingleBedCount], [FMaxCapacity], [FIsCleaned], [FLastCleanDatetime])
VALUES
    (1, 1, '101', 1, 1, 1, 0, '2-3', 1, '2024-05-01 10:00:00'),
    (0, 1, '102', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 1, '201', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 1, '202', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 1, '203', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 2, '101', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 2, '201', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 2, '202', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 2, '203', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 2, '204', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 3, '201', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00'),
    (1, 3, '202', 2, 2, 1, 2, '6-10', 1, '2024-05-01 14:00:00');
GO

INSERT INTO [TProductImage]
	([FProductType], [FProductID], [FImage])
VALUES
	('H', 1, 'S__37707904.jpg'), ('H', 2, 'S__37707913.jpg'), ('H', 3, 'S__37707914.jpg'), 
	('R', 1, 'h1-101-1.jpg'), ('R', 2, 'h1-102-1.jpg'), ('R', 3, 'h1-201-1.jpg'), ('R', 4, 'h1-202-1.jpg'), ('R', 5, 'h1-203-1.jpeg'), 
	('R', 6, 'h2-101-1.jpg'), ('R', 7, 'h2-201-1.jpg'), ('R', 8, 'h2-202-1.jpg'), ('R', 9, 'h2-203-1.jpg'), ('R', 10, 'h2-204-1.jpg'), 
	('R', 11, 'h3-201-1.jpg'), ('R', 12, 'h3-202-1.jpg');
GO

INSERT INTO [TProductHomestayPrice]
    ([FHomestayRoomID], [FIsPeakSeason], [FStayDays], [FStayPrice])
VALUES
    (1, 0, 1, 100.00), (1, 1, 1, 110.00), 
    (2, 0, 1, 120.00), (2, 1, 1, 130.00), 
    (3, 0, 1, 140.00), (3, 1, 1, 150.00), 
    (4, 0, 1, 160.00), (4, 1, 1, 170.00), 
    (5, 0, 1, 180.00), (5, 1, 1, 190.00), 
    (6, 0, 1, 200.00), (6, 1, 1, 210.00), 
    (7, 0, 1, 220.00), (7, 1, 1, 230.00), 
    (8, 0, 1, 240.00), (8, 1, 1, 250.00), 
    (9, 0, 1, 260.00), (9, 1, 1, 270.00), 
    (10, 0, 1, 280.00), (10, 1, 1, 290.00), 
    (11, 0, 1, 300.00), (11, 1, 1, 310.00), 
    (12, 0, 1, 320.00), (12, 1, 1, 330.00);
GO

INSERT INTO [TProductCourse]
    ([FCourseCode], [FCourseName])
VALUES
    ('C3H1', N'課程名一'),
    ('C6H2', N'課程名二');
GO

INSERT INTO [TProductCoursePrice]
    ([FCourseID], [FCourseCombo], [FCoursePrice])
VALUES
    (1, '112', 20.00),
    (1, '223', 40.00);
GO

INSERT INTO [TProductEquipmentCategory]
    ([FEquipmentCategoryName])
VALUES
    (N'滑雪杖'), (N'滑雪服'), (N'滑雪板'), (N'滑雪靴'),
    (N'頭盔'), (N'護目鏡'), (N'防寒手套'), (N'雪地摩托');
GO

INSERT INTO [TProductEquipment]
    ([FIsPublished], [FVendorID], [FEquipmentCategoryID], [FEquipmentCode], [FEquipmentName], [FDescription], [FQuantity], [FEquipmentPrice], [FRemarks])
VALUES
    (1, 2, 1, 'Q001', N'滑雪杖一型', N'..', 60, 30.00, NULL),
    (1, 2, 2, 'Q002', N'滑雪服一型', N'..', 40, 40.00, NULL);
GO

INSERT INTO [TProductStorage]
    ([FEquipmentCategoryID], [FStoragePrice])
VALUES
    (1, 20.00),
    (2, 25.00);
GO

INSERT INTO [TTagType]
    ([FTypeName])
VALUES
    (N'交通車服務'), (N'交通車其他'), (N'活動頻率'), (N'活動其他'),
    (N'民宿位置'), (N'民宿周邊'), (N'民宿服務'), (N'民宿房間'), (N'民宿價格'),
    (N'民宿其他');
GO

INSERT INTO [TTag]
    ([FTagTypeID], [FTagName])
VALUES
    (1, N'機場接送'),
    (1, N'火車站接送'),
    (1, N'民宿接送'),
    (1, N'滑雪場接送'),
    (1, N'指定地點接送'),
    (3, N'每日活動'),
    (3, N'每週活動'),
    (3, N'每月活動'),
    (3, N'每季活動'),
    (3, N'年度活動'),
    (3, N'特別節日活動'),
    (5, N'鄰近某著名景點'),
    (5, N'鄰近某商業區'),
    (6, N'鄰近餐廳'),
    (6, N'鄰近購物中心'),
    (6, N'鄰近超市'),
    (6, N'鄰近公共交通站點'),
    (7, N'免費無線網絡'),
    (7, N'提供早餐'),
    (7, N'免費停車位'),
    (7, N'洗衣設施'),
    (8, N'和式房間'),
    (8, N'洋式房間'),
    (8, N'家庭房'),
    (9, N'最新優惠'),
    (9, N'限時折扣');
GO

INSERT INTO [TProductTagMapping]
    ([FProductType], [FProductID], [FProductCode], [FTagID])
VALUES
    ('S', 1, 'S00E', 1), -- 交通車
    ('H', 1, 'HPL4IQ', 2); -- 民宿
GO

INSERT INTO [TMemberShoppingCart]
    ([FMemberID], [FProductType], [FProductID], [FPrice], [FRemark])
VALUES
    (1, 'H', 1, 99.00, NULL), -- 住宿 ([AccommodationRoomID])
    (1, 'S', 1, 99.00, N'火車站-民宿'), -- 接送 ([ShuttleCode])
    (1, 'E', 1, 99.00, NULL), -- 租借 RIJSQ([EquipmentCode])
    (1, 'T', 1, 99.00, NULL); -- 寄放 RIJSQ([StorageCode])
    --(1, 'C', 11, 99.00, NULL); -- 課程 1(C3H1 [CourseCode]) + 1(112 [CourseSelectedCode])
GO

INSERT INTO [TMemberShoppingCartHomestay]
    ([FShoppingCartID], [FPickupLocationID], [FDropoffLocationID], [FStartDatetime], [FEndDatetime])
VALUES
    (1, NULL, NULL, '2024-05-17 17:00:00', '2024-05-19 12:00:00'), -- 住宿(不須上下地點)
    (2, 1, 2, '2024-05-19 17:00:00', '2024-05-20 12:00:00'); -- 接送
GO

INSERT INTO [TMemberShoppingCartCourse]
    ([FShoppingCartID], [FLocationID], [FDays], [FPeopleCount])
VALUES
    (3, 1, 2, 2), --租借
    (4, 1, 2, 2); --寄放
GO

INSERT INTO [TOrderPaymentMethod]
    ([FPaymentMethodName])
VALUES
    (N'現金'), (N'銀行轉帳'), (N'Paypal'), (N'ApplePay'), 
    (N'LinePay'), (N'信用卡');
GO

INSERT INTO [TOrder]
    ([FOrderCode], [FMemberID], [FCreationDatetime], [FInitialAmount], [FDiscountCouponID], [FFinalAmount], [FIsCheckedIn])
VALUES
    ('ODI9OKWE32', 1, '2024-05-14 13:00:00', 360.00, NULL, 320.00, 1),
    ('ODI9OKWE33', 2, '2024-05-15 14:00:00', 400.00, NULL, 350.00, 0);
GO

INSERT INTO [TOrderDetail]
    ([FOrderID], [FProductType], [FProductID], [FOrderDetailAmount], [FRemarks])
VALUES
    (1, 'H', 1, 120.00, N'會提早到'), -- 住宿 HPL4IQ([AccommodationCode]) + 101([RoomCode])
    (1, 'E', 1, 140.00, N'會提早到'), -- 租借 RIJSQ([EquipmentCode])
    (1, 'T', 1, 60.00, NULL), -- 寄放 RIJSQ([StorageCode])
    --(1, 'C', 11, 80.00, NULL), -- 課程 1(C3H1 [CourseCode]) + 1(112 [CourseSelectedCode])
    (1, 'S', 1, 80.00, NULL); -- 接送 S00E([ShuttleCode]) + 11(Location)
GO

INSERT INTO [TOrderDetailHomestay]
    ([FOrderDetailID], [FPickupLocationID], [FDropoffLocationID], [FStartDatetime], [FEndDatetime])
VALUES
    (1, 1, 2, '2024-05-17 17:00:00', '2024-05-19 12:00:00'),
    (2, 1, 2, '2024-05-19 17:00:00', '2024-05-20 12:00:00');
GO

INSERT INTO [TOrderDetailCourse]
    ([FOrderDetailID], [FLocationID], [FDays], [FPeopleCount])
VALUES
    (3, 1, 2, 2),
    (4, 1, 2, 2);
GO

INSERT INTO [TOrderPaymentStatus]
    ([FPaymentStatusName])
VALUES
    (N'已完成'),
    (N'待處理'),
    (N'失敗');
GO

INSERT INTO [TOrderPaymentRecord]
    ([FOrderID], [FPaymentStatusID], [FPaymentMethodID], [FPaymentDatetime], [FPaymentAmount], [FBankID], [FCardholderName], [FCardNumber], [FCardExpiryYear], [FCardExpiryMonth])
VALUES
    (1, 1, 2, '2024-05-14 13:00:00', 140.00, NULL, NULL, NULL, NULL, NULL),
    (2, 1, 2, '2024-05-14 15:30:00', 180.00, NULL, NULL, NULL, NULL, NULL);
GO

INSERT INTO [TMemberReview]
    ([FMemberID], [FProductType], [FProductID], [FReviewDate], [FReviewContent])
VALUES
    (1, 'H', 1, '2024-05-20', N'舒適'),
    (2, 'H', 2, '2024-05-21', N'不錯');
GO

INSERT INTO [TCoupon]
    ([FIsPublished], [FCouponCode], [FCouponName], [FStartDate], [FEndDate])
VALUES
    (1, 'POI', N'POI', '2024-06-01', '2024-06-30'),
    (1, 'P3J', N'P3J', '2024-07-01', '2024-07-31');
GO
