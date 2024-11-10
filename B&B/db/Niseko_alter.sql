USE [Niseko]
GO

--添加動態數據遮罩
ALTER TABLE [TEmployee]
ALTER COLUMN [FEmployeeCode] ADD MASKED WITH (FUNCTION = 'partial(1,"XX",1)');

ALTER TABLE [TEmployee]
ALTER COLUMN [FDepartmentID] ADD MASKED WITH (FUNCTION = 'default()');

ALTER TABLE [TEmployee]
ALTER COLUMN [FPosition] ADD MASKED WITH (FUNCTION = 'default()');

ALTER TABLE [TEmployee]
ALTER COLUMN [FHireDate] ADD MASKED WITH (FUNCTION = 'default()');

ALTER TABLE [TEmployee]
ALTER COLUMN [FFireDate] ADD MASKED WITH (FUNCTION = 'default()');

ALTER TABLE [TEmployee]
ALTER COLUMN [FEmail] ADD MASKED WITH (FUNCTION = 'email()');

ALTER TABLE [TEmployee]
ALTER COLUMN [FPermissions] ADD MASKED WITH (FUNCTION = 'default()');
GO

ALTER TABLE [TEmployeeLoginRecord]
ALTER COLUMN [FIPAddress] ADD MASKED WITH (FUNCTION = 'default()');
GO
