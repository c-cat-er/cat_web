--查看數據
SELECT * FROM [TDepartment]
SELECT * FROM [TEmployee]
SELECT * FROM [TEmployeeLoginRecord]

--切換用戶
--EXECUTE AS USER = 'restricted_user';

--打開對稱密鑰
OPEN SYMMETRIC KEY [NisekoSymmetricKey]
DECRYPTION BY CERTIFICATE [NisekoCert];
GO

--查詢數據，同時應用遮罩和解密
SELECT 
    [FEmployeeID],
    [FEmployeeCode],
    [FFirstName],
    [FLastName],
    [FDepartmentID],
    [FPosition],
    [FHireDate],
    [FFireDate],
    [FBirthDate],
    [FEmail],
    -- 解密帳號
    CONVERT(NVARCHAR(50), DECRYPTBYKEY([FAccount])) AS DecryptedAccount,
    -- 解密密碼哈希
    CONVERT(NVARCHAR(64), DECRYPTBYKEY([FPasswordHash])) AS DecryptedPasswordHash,
    -- 解密密碼鹽值
    CONVERT(NVARCHAR(128), DECRYPTBYKEY([FPasswordSalt])) AS DecryptedPasswordSalt,
    [FPermissions]
FROM [TEmployee];
GO

--關閉對稱密鑰
CLOSE SYMMETRIC KEY [NisekoSymmetricKey];
GO

