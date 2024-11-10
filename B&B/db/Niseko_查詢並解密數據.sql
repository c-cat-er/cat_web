--�d�ݼƾ�
SELECT * FROM [TDepartment]
SELECT * FROM [TEmployee]
SELECT * FROM [TEmployeeLoginRecord]

--�����Τ�
--EXECUTE AS USER = 'restricted_user';

--���}��ٱK�_
OPEN SYMMETRIC KEY [NisekoSymmetricKey]
DECRYPTION BY CERTIFICATE [NisekoCert];
GO

--�d�߼ƾڡA�P�����ξB�n�M�ѱK
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
    -- �ѱK�b��
    CONVERT(NVARCHAR(50), DECRYPTBYKEY([FAccount])) AS DecryptedAccount,
    -- �ѱK�K�X����
    CONVERT(NVARCHAR(64), DECRYPTBYKEY([FPasswordHash])) AS DecryptedPasswordHash,
    -- �ѱK�K�X�Q��
    CONVERT(NVARCHAR(128), DECRYPTBYKEY([FPasswordSalt])) AS DecryptedPasswordSalt,
    [FPermissions]
FROM [TEmployee];
GO

--������ٱK�_
CLOSE SYMMETRIC KEY [NisekoSymmetricKey];
GO

