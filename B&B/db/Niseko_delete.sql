USE [Niseko]
GO

IF OBJECT_ID('MemberNon', 'U') IS NOT NULL DELETE FROM [MemberNon];
IF OBJECT_ID('MemberSkiLevel', 'U') IS NOT NULL DELETE FROM [MemberSkiLevel];
IF OBJECT_ID('Member', 'U') IS NOT NULL DELETE FROM [Member];
IF OBJECT_ID('MemberLoginType', 'U') IS NOT NULL DELETE FROM [MemberLoginType];
IF OBJECT_ID('MemberWebsiteAccount', 'U') IS NOT NULL DELETE FROM [MemberWebsiteAccount];
IF OBJECT_ID('MemberThirdPartyAccount', 'U') IS NOT NULL DELETE FROM [MemberThirdPartyAccount];
IF OBJECT_ID('MemberLoginRecord', 'U') IS NOT NULL DELETE FROM [MemberLoginRecord];
IF OBJECT_ID('Vendor', 'U') IS NOT NULL DELETE FROM [Vendor];
IF OBJECT_ID('ProductEvent', 'U') IS NOT NULL DELETE FROM [ProductEvent];
IF OBJECT_ID('Location', 'U') IS NOT NULL DELETE FROM [Location];
IF OBJECT_ID('ProductShuttle', 'U') IS NOT NULL DELETE FROM [ProductShuttle];
IF OBJECT_ID('ProductShuttlePrice', 'U') IS NOT NULL DELETE FROM [ProductShuttlePrice];
IF OBJECT_ID('ProductAccommodation', 'U') IS NOT NULL DELETE FROM [ProductAccommodation];
IF OBJECT_ID('ProductAccommodationRoom', 'U') IS NOT NULL DELETE FROM [ProductAccommodationRoom];
IF OBJECT_ID('ProductAccommodationPrice', 'U') IS NOT NULL DELETE FROM [ProductAccommodationPrice];
IF OBJECT_ID('ProductCourse', 'U') IS NOT NULL DELETE FROM [ProductCourse];
IF OBJECT_ID('ProductEquipmentCategory', 'U') IS NOT NULL DELETE FROM [ProductEquipmentCategory];
IF OBJECT_ID('ProductEquipment', 'U') IS NOT NULL DELETE FROM [ProductEquipment];
IF OBJECT_ID('ProductStorage', 'U') IS NOT NULL DELETE FROM [ProductStorage];
IF OBJECT_ID('TagType', 'U') IS NOT NULL DELETE FROM [TagType];
IF OBJECT_ID('Tag', 'U') IS NOT NULL DELETE FROM [Tag];
IF OBJECT_ID('ProductTagMapping', 'U') IS NOT NULL DELETE FROM [ProductTagMapping];
IF OBJECT_ID('MemberFavorite', 'U') IS NOT NULL DELETE FROM [MemberFavorite];
IF OBJECT_ID('MemberShoppingCart', 'U') IS NOT NULL DELETE FROM [MemberShoppingCart];
IF OBJECT_ID('MemberShoppingCartAccommodation', 'U') IS NOT NULL DELETE FROM [MemberShoppingCartAccommodation];
IF OBJECT_ID('MemberShoppingCartCourse', 'U') IS NOT NULL DELETE FROM [MemberShoppingCartCourse];
IF OBJECT_ID('OrderPaymentMethod', 'U') IS NOT NULL DELETE FROM [OrderPaymentMethod];
IF OBJECT_ID('Order', 'U') IS NOT NULL DELETE FROM [Order];
IF OBJECT_ID('OrderDetail', 'U') IS NOT NULL DELETE FROM [OrderDetail];
IF OBJECT_ID('OrderDetailAccommodation', 'U') IS NOT NULL DELETE FROM [OrderDetailAccommodation];
IF OBJECT_ID('OrderDetailCourse', 'U') IS NOT NULL DELETE FROM [OrderDetailCourse];
IF OBJECT_ID('OrderPaymentStatus', 'U') IS NOT NULL DELETE FROM [OrderPaymentStatus];
IF OBJECT_ID('OrderPaymentRecord', 'U') IS NOT NULL DELETE FROM [OrderPaymentRecord];
IF OBJECT_ID('MemberReview', 'U') IS NOT NULL DELETE FROM [MemberReview];
IF OBJECT_ID('Coupon', 'U') IS NOT NULL DELETE FROM [Coupon];
