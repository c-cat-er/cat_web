USE [Niseko];
GO

-- 先刪除外鍵約束
ALTER TABLE [TEmployeeLoginRecord] DROP CONSTRAINT [FK_TEmployeeLoginRecord_FEmployeeID];
GO
ALTER TABLE [TEmployee] DROP CONSTRAINT [FK_TEmployee_FDepartmentID];
GO
ALTER TABLE [TCoach] DROP CONSTRAINT [FK_TCoach_FCoachLevelID];
GO
ALTER TABLE [TCoachPrice] DROP CONSTRAINT [FK_TCoachPrice_FCoachLevelID];
GO
ALTER TABLE [TMember] DROP CONSTRAINT [FK_TMember_FSkiLevelID];
GO
ALTER TABLE [TMemberWebsiteAccount] DROP CONSTRAINT [FK_TMemberWebsiteAccount_FMemberID];
GO
ALTER TABLE [TMemberThirdPartyAccount] DROP CONSTRAINT [FK_TMemberThirdPartyAccount_FMemberID];
GO
ALTER TABLE [TMemberThirdPartyAccount] DROP CONSTRAINT [FK_TMemberThirdPartyAccount_FLoginTypeID];
GO
ALTER TABLE [TMemberLoginRecord] DROP CONSTRAINT [FK_TMemberLoginRecord_FMemberID];
GO
ALTER TABLE [TMemberLoginRecord] DROP CONSTRAINT [FK_TMemberLoginRecord_FLoginTypeID];
GO
ALTER TABLE [TProductEvent] DROP CONSTRAINT [FK_TProductEvent_FVendorID];
GO
ALTER TABLE [TProductShuttlePrice] DROP CONSTRAINT [FK_TProductShuttlePrice_FShuttleID];
GO
ALTER TABLE [TProductShuttlePrice] DROP CONSTRAINT [FK_TProductShuttlePrice_FPickupLocationID];
GO
ALTER TABLE [TProductShuttlePrice] DROP CONSTRAINT [FK_TProductShuttlePrice_FDropoffLocationID];
GO
ALTER TABLE [TProductHomestay] DROP CONSTRAINT [FK_TProductHomestay_FAddressID];
GO
ALTER TABLE [TProductHomestayRoom] DROP CONSTRAINT [FK_TProductHomestayRoom_FHomestayID];
GO
--ALTER TABLE [TProductHomestayImage] DROP CONSTRAINT [FK_TProductHomestayImage_FHomestayRoomID];
--GO
ALTER TABLE [TProductHomestayPrice] DROP CONSTRAINT [FK_TProductHomestayPrice_FHomestayRoomID];
GO
ALTER TABLE [TProductCoursePrice] DROP CONSTRAINT [FK_TProductCoursePrice_FCourseID];
GO
ALTER TABLE [TProductEquipment] DROP CONSTRAINT [FK_TProductEquipment_FVendorID];
GO
ALTER TABLE [TProductEquipment] DROP CONSTRAINT [FK_TProductEquipment_FEquipmentCategoryID];
GO
ALTER TABLE [TProductStorage] DROP CONSTRAINT [FK_TProductStorage_FEquipmentCategoryID];
GO
ALTER TABLE [TTag] DROP CONSTRAINT [FK_TTag_FTagTypeID];
GO
ALTER TABLE [TProductTagMapping] DROP CONSTRAINT [FK_TProductTagMapping_FTagID];
GO
ALTER TABLE [TMemberShoppingCartHomestay] DROP CONSTRAINT [FK_TMemberShoppingCartHomestay_FShoppingCartHomestayID];
GO
ALTER TABLE [TMemberShoppingCartHomestay] DROP CONSTRAINT [FK_TMemberShoppingCartHomestay_FPickupLocationID];
GO
ALTER TABLE [TMemberShoppingCartHomestay] DROP CONSTRAINT [FK_TMemberShoppingCartHomestay_FDropoffLocationID];
GO
ALTER TABLE [TMemberShoppingCartCourse] DROP CONSTRAINT [FK_TMemberShoppingCartCourse_FShoppingCartCourseID];
GO
ALTER TABLE [TMemberShoppingCartCourse] DROP CONSTRAINT [FK_TMemberShoppingCartCourse_FLocationID];
GO
ALTER TABLE [TMemberShoppingCart] DROP CONSTRAINT [FK_TMemberShoppingCart_FMemberID];
GO
ALTER TABLE [TOrder] DROP CONSTRAINT [FK_TOrder_FMemberID];
GO
ALTER TABLE [TOrder] DROP CONSTRAINT [FK_TOrder_FDiscountCouponID];
GO
ALTER TABLE [TOrderDetail] DROP CONSTRAINT [FK_TOrderDetail_FOrderID];
GO
ALTER TABLE [TOrderDetailHomestay] DROP CONSTRAINT [FK_TOrderDetailHomestay_FOrderDetailID];
GO
ALTER TABLE [TOrderDetailHomestay] DROP CONSTRAINT [FK_TOrderDetailHomestay_FPickupLocationID];
GO
ALTER TABLE [TOrderDetailHomestay] DROP CONSTRAINT [FK_TOrderDetailHomestay_FDropoffLocationID];
GO
ALTER TABLE [TOrderDetailCourse] DROP CONSTRAINT [FK_TOrderDetailCourse_FOrderDetailID];
GO
ALTER TABLE [TOrderDetailCourse] DROP CONSTRAINT [FK_TOrderDetailCourse_FLocationID];
GO
ALTER TABLE [TOrderPaymentRecord] DROP CONSTRAINT [FK_TOrderPaymentRecord_FOrderID];
GO
ALTER TABLE [TOrderPaymentRecord] DROP CONSTRAINT [FK_TOrderPaymentRecord_FPaymentMethodID];
GO
ALTER TABLE [TOrderPaymentRecord] DROP CONSTRAINT [FK_TOrderPaymentRecord_FPaymentStatusID];
GO
ALTER TABLE [TMemberReview] DROP CONSTRAINT [FK_TMemberReview_FMemberID];
GO

-- 現在刪除表格
DROP TABLE IF EXISTS [TAuditLog];
DROP TABLE IF EXISTS [TEmployeeLoginRecord];
DROP TABLE IF EXISTS [TEmployee];
DROP TABLE IF EXISTS [TDepartment];
DROP TABLE IF EXISTS [TCoachLevel];
DROP TABLE IF EXISTS [TCoach];
DROP TABLE IF EXISTS [TCoachPrice];
DROP TABLE IF EXISTS [TCoupon];
DROP TABLE IF EXISTS [TMemberNon];
DROP TABLE IF EXISTS [TLocation];
DROP TABLE IF EXISTS [TMember];
DROP TABLE IF EXISTS [TMemberSkiLevel];
DROP TABLE IF EXISTS [TMemberLoginType];
DROP TABLE IF EXISTS [TMemberWebsiteAccount];
DROP TABLE IF EXISTS [TMemberThirdPartyAccount];
DROP TABLE IF EXISTS [TMemberLoginRecord];
DROP TABLE IF EXISTS [TVendor];
DROP TABLE IF EXISTS [TProductImage];
DROP TABLE IF EXISTS [TProductEvent];
DROP TABLE IF EXISTS [TProductShuttle];
DROP TABLE IF EXISTS [TProductShuttlePrice];
DROP TABLE IF EXISTS [TProductHomestay];
DROP TABLE IF EXISTS [TProductHomestayRoom];
--DROP TABLE IF EXISTS [TProductHomestayImage];
DROP TABLE IF EXISTS [TProductHomestayPrice];
DROP TABLE IF EXISTS [TProductCourse];
DROP TABLE IF EXISTS [TProductCoursePrice];
DROP TABLE IF EXISTS [TProductEquipmentCategory];
DROP TABLE IF EXISTS [TProductEquipment];
DROP TABLE IF EXISTS [TProductStorage];
DROP TABLE IF EXISTS [TTagType];
DROP TABLE IF EXISTS [TTag];
DROP TABLE IF EXISTS [TProductTagMapping];
DROP TABLE IF EXISTS [TMemberShoppingCart];
DROP TABLE IF EXISTS [TMemberShoppingCartHomestay];
DROP TABLE IF EXISTS [TMemberShoppingCartCourse];
DROP TABLE IF EXISTS [TOrderPaymentMethod];
DROP TABLE IF EXISTS [TOrder];
DROP TABLE IF EXISTS [TOrderDetail];
DROP TABLE IF EXISTS [TOrderDetailHomestay];
DROP TABLE IF EXISTS [TOrderDetailCourse];
DROP TABLE IF EXISTS [TOrderPaymentStatus];
DROP TABLE IF EXISTS [TOrderPaymentRecord];
DROP TABLE IF EXISTS [TMemberReview];
GO
