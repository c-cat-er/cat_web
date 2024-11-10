USE [Niseko]
GO

-- 打開對稱密鑰
OPEN SYMMETRIC KEY [NisekoSymmetricKey]
DECRYPTION BY CERTIFICATE [NisekoCert];
GO

INSERT INTO [TDepartment]
	([FDepartmentName])
VALUES 
	('Human Resources'), ('Finance'), ('IT'), ('Sales');
GO

INSERT INTO [TEmployee]
	([FEmployeeCode], [FFirstName], [FLastName], [FDepartmentID], [FPosition],
    [FHireDate], [FFireDate], [FBirthDate], [FPhone], [FEmail], [FAccount], [FPasswordHash],
    [FPasswordSalt], [FPermissions])
VALUES
    ('E001', N'John', N'Doe', 1, N'Manager', GETDATE(), NULL, '1980-01-01', '0900111222',
        'john.doe@example.com',
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), 'johndoe'),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), HASHBYTES('SHA2_256', 'password')),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), CAST(NEWID() AS varbinary(128))),
        1),
    ('E002', N'Jane', N'Smith', 2, N'Analyst', GETDATE(), NULL, '1985-05-15', '0900111223',
        'jane.smith@example.com',
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), 'janesmith'),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), HASHBYTES('SHA2_256', 'password')),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), CAST(NEWID() AS varbinary(128))),
        1),
    ('E003', N'Robert', N'Brown', 3, N'Developer', GETDATE(), NULL, '1990-08-22', '0900111224',
        'robert.brown@example.com',
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), 'robertbrown'),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), HASHBYTES('SHA2_256', 'password')),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), CAST(NEWID() AS varbinary(128))),
        1),
    ('E004', N'Emily', N'Davis', 4, N'Sales Rep', GETDATE(), NULL, '1992-12-03', '0900111252',
        'emily.davis@example.com',
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), 'emilydavis'),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), HASHBYTES('SHA2_256', 'password')),
        ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), CAST(NEWID() AS varbinary(128))),
        1);
GO

INSERT INTO [TEmployeeLoginRecord]
	([FEmployeeID], [FIPAddress], [FLoginDatetime], [FLogoutDatetime])
VALUES
    (1, '192.168.1.1', GETDATE(), NULL),
    (2, '192.168.1.2', GETDATE(), NULL),
    (3, '192.168.1.3', GETDATE(), NULL),
    (4, '192.168.1.4', GETDATE(), NULL);
GO

-----------------------------------------------

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

---------------

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
    (1, ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), 'account1'), ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), HASHBYTES('SHA2_256', 'password1')), ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), CAST(NEWID() AS varbinary(128)))),
    (2, ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), 'account2'), ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), HASHBYTES('SHA2_256', 'password2')), ENCRYPTBYKEY(KEY_GUID('NisekoSymmetricKey'), CAST(NEWID() AS varbinary(128))));
GO

INSERT INTO [TMemberThirdPartyAccount]
	([FMemberID], [FLoginTypeID], [FThirdPartyUniqueID])
VALUES
    (1, 2, 'UQ1......'), --使用Google登入
    (1, 3, 'UQ2......'), --使用Microsoft登入
    (2, 4, 'UQ3......'), --使用Apple登入
    (2, 5, 'UQ4......'); --使用Facebook登入
GO

INSERT INTO [TMemberLoginRecord]
	([FMemberID], [FLoginTypeID], [FLoginTimezone], [FLoginIPAddress], [FLoginDatetime], [FLogoutDatetime])
VALUES
    (1, 1, 'Asia/Tokyo', '192.168.1.1', '2024-05-13 08:30:00', '2024-05-13 14:00:00'),
    -- 使用官網登入
    (1, 2, 'America/New_York', '192.168.1.2', '2024-05-14 12:00:00', NULL); -- 使用Apple登入
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
    ('HPL4IQ', N'Pink House', N'4間洋房、一間和室', 1),
    ('HPLD12', N'Lucky House', N'一衛一廁', 2),
    ('HPLD32', N'New House', N'洋房', 2);
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

INSERT INTO [TProductHomestayPrice]
	([FHomestayRoomID], [FIsPeakSeason], [FStayDays], [FStayPrice])
VALUES
    (1, 0, 3, 140.00),
    (1, 1, 2, 160.00);
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
    ('S', 1, 'S00E', 1), --交通車
    ('A', 1, 'HPL4IQ', 2); --民宿
GO

INSERT INTO [TMemberShoppingCart]
	([FMemberID], [FProductType], [FProductID])
VALUES
    (1, 'A', 1), --住宿 ([AccommodationRoomID])
	(1, 'E', 1), --租借 RIJSQ([EquipmentCode])
	(1, 'T', 1), --寄放 RIJSQ([StorageCode])
    (1, 'C', 11), --課程 1(C3H1 [CourseCode]) + 1(112 [CourseSelectedCode])
	(1, 'S', 1); --接送 ([ShuttleCode])
GO

INSERT INTO [TMemberShoppingCartHomestay]
	([FShoppingCartID], [FPickupLocationID], [FDropoffLocationID], [FStartDatetime], [FEndDatetime], [FRemark])
VALUES
    (1, 1, 2, '2024-05-17 17:00:00', '2024-05-19 12:00:00', NULL), --住宿
    (2, 1, 2, '2024-05-19 17:00:00', '2024-05-20 12:00:00', NULL); --住宿 + 接送
GO

INSERT INTO [TMemberShoppingCartCourse]
	([FShoppingCartID], [FLocationID], [FDays], [FPeopleCount], [FRemark])
VALUES
    (2, 1, 2, 2, NULL),
    (3, 1, 2, 2, NULL);
GO

INSERT INTO [TOrderPaymentMethod]
	([FPaymentMethodName])
VALUES
    (N'現金'), (N'銀行轉帳'), (N'Paypal'), (N'ApplePay'), 
    (N'LinePay'), (N'信用卡');
GO

INSERT INTO [TOrder]
	([FOrderCode], [FMemberID], [FCreationDatetime], [FInitialAmount], [FDiscountCouponID], [FFinalAmount])
VALUES
    ('ODI9OKWE32', 1, '2024-05-14 13:00:00', 360.00, NULL, 320.00),
    ('ODI9OKWE33', 2, '2024-05-15 14:00:00', 400.00, NULL, 350.00);
GO

INSERT INTO [TOrderDetail]
	([FOrderID], [FProductType], [FProductID])
VALUES
    (1, 'A', 1, 120.00), --住宿 HPL4IQ([AccommodationCode]) + 101([RoomCode])
	(1, 'E', 1, 20.00), --租借 RIJSQ([EquipmentCode])
	(1, 'T', 1, 30.00), --寄放 RIJSQ([StorageCode])
    (1, 'C', 11, 60.00), --課程 1(C3H1 [CourseCode]) + 1(112 [CourseSelectedCode])
	(1, 'S', 1, 30.00); --接送 S00E([ShuttleCode]) + 11(Location)
GO

INSERT INTO [TOrderDetailHomestay]
	([FOrderDetailID], [FPickupLocationID], [FDropoffLocationID], [FStartDatetime], [FEndDatetime], [FRemarks], [FIsCheckedIn], [FOrderDetailAmount])
VALUES
    (1, 1, 2, '2024-05-17 17:00:00', '2024-05-19 12:00:00', N'會提早到', 0, 120.00),
    (2, 1, 2, '2024-05-19 17:00:00', '2024-05-20 12:00:00', N'會提早到', 0, 140.00);
GO

INSERT INTO [TOrderDetailCourse]
	([FOrderDetailID], [FLocationID], [FDays], [FPeopleCount], [FRemarks], [FIsCheckedIn], [FOrderDetailAmount])
VALUES
    (1, 1, 2, 2, NULL, 0, 60.00),
    (2, 1, 2, 2, NULL, 0, 80.00);
GO

INSERT INTO [TOrderPaymentStatus]
	([FPaymentStatusName])
VALUES
    (N'已完成'),
    (N'待處理'),
    (N'失敗');
GO

INSERT INTO [TOrderPaymentRecord]
	([FOrderID], [FPaymentMethodID], [FBankID], [FCardholderName], [FCardNumber], [FCardExpiryYear], [FCardExpiryMonth], [FPaymentStatusID], [FPaymentDatetime], [FPaymentAmount])
VALUES
    (1, 2, NULL, NULL, NULL, NULL, NULL, 1, '2024-05-14 13:00:00', 140.00),
    (2, 2, NULL, NULL, NULL, NULL, NULL, 1, '2024-05-14 15:30:00', 180.00);
GO

INSERT INTO [TMemberReview]
    ([FMemberID], [FProductType], [FProductID], [FReviewDatetime], [FReviewContent])
VALUES
    (1, 'A', 1, '2024-05-20 12:00:00', N'舒適'),
    (2, 'A', 2, '2024-05-21 12:00:00', N'不錯');
GO

INSERT INTO [TCoupon]
    ([FIsPublished], [FCouponCode], [FCouponName], [FStartDate], [FEndDate])
VALUES
    (1, 'POI', N'POI', '2024-06-01', '2024-06-30'),
    (1, 'P3J', N'P3J', '2024-07-01', '2024-07-31');
GO

--關閉對稱密鑰
CLOSE SYMMETRIC KEY [NisekoSymmetricKey];
GO
