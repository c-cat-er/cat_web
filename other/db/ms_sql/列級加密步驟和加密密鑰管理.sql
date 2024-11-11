--Column-Level Encryption(列級加密)步驟

--1.創建 Master Key(主密鑰)，主密鑰是用於保護證書和非對稱密鑰的根密鑰
USE master; --master 資料庫是 SQL Server 的核心系統資料庫，用於存儲伺服器級別的配置和系統信息。
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'StrongPassword123!';

--2.創建 Certificate(證書)
CREATE CERTIFICATE MyDatabaseCertificateName
WITH SUBJECT = 'My Certificate'; --SUBJECT 用於描述證書的用途或內容

/* 另一個證書示範
CREATE CERTIFICATE SalesDataCertificate
WITH SUBJECT = 'Certificate for encrypting sales data';
*/

--3.創建 Symmetric Key(對稱密鑰)，對稱密鑰用於加密和解密數據庫列中的數據
USE [YourDatabaseName];
CREATE SYMMETRIC KEY MySymmetricKeyName
WITH ALGORITHM = AES_256 --使用 256 位密鑰長度的 AES 算法
ENCRYPTION BY CERTIFICATE MyDatabaseCertificateName;

--假設有一個 SensitiveDataTable 表
/*
CREATE TABLE SensitiveDataTable (
    ID INT IDENTITY PRIMARY KEY,
    EncryptedColumn VARBINARY(300),
    OtherColumn NVARCHAR(100)
);*/

--4.Encrypting Data (加密數據)，在插入數據時使用對稱密鑰進行加密。
--打開對稱密鑰
OPEN SYMMETRIC KEY MySymmetricKeyName
DECRYPTION BY CERTIFICATE MyDatabaseCertificateName;

--插入加密數據
INSERT INTO [YourTableName] ([EncryptedColumn], [OtherColumns])
VALUES (
	--ENCRYPTBYKEY(KEY_GUID('MySymmetricKey'), 'Sensitive Data') 的結果插入到 [EncryptedColumn]
	--Other Data 插入到 OtherColumns]
    ENCRYPTBYKEY(KEY_GUID('MySymmetricKeyName'), 'Sensitive Data'), 'Other Data'
);

--關閉對稱密鑰
CLOSE SYMMETRIC KEY MySymmetricKeyName;

--5.Decrypting Data (解密數據)，在查詢數據時使用對稱密鑰進行解密。
--打開對稱密鑰
OPEN SYMMETRIC KEY MySymmetricKeyName
DECRYPTION BY CERTIFICATE MyDatabaseCertificateName;

--查詢解密數據
SELECT 
	--解密 [EncryptedColumn]
	--DECRYPTBYKEY 函數
	--CONVERT 函數用於將數據從一種數據類型轉換為另一種數據類型，此轉為 varchar
    CONVERT(varchar, DECRYPTBYKEY([EncryptedColumn])) AS DecryptedColumn, [OtherColumns]
FROM [YourTableName];

--關閉對稱密鑰
CLOSE SYMMETRIC KEY MySymmetricKeyName;

--加密密鑰管理 (Key Management)，管理加密密鑰的過程包括創建、備份和恢復密鑰。
--備份主密鑰
BACKUP MASTER KEY TO FILE = 'C:\Path\To\Backup\MasterKeyBackup.key'
ENCRYPTION BY PASSWORD = 'StrongBackupPassword123!';

--恢復主密鑰
RESTORE MASTER KEY FROM FILE = 'C:\Path\To\Backup\MasterKeyBackup.key'
DECRYPTION BY PASSWORD = 'StrongBackupPassword123!'
ENCRYPTION BY PASSWORD = 'StrongPassword123!';

--備份證書
--.cer 文件：這是證書文件，包含公開密鑰 (public key) 以及一些證書的元數據。
--.pvk 文件：這是私有密鑰文件，包含與證書對應的私有密鑰 (private key)。
BACKUP CERTIFICATE MyDatabaseCertificateName
TO FILE = 'C:\Path\To\Backup\DatabaseCertificateBackup.cer'
WITH PRIVATE KEY (
    FILE = 'C:\Path\To\Backup\DatabaseCertificatePrivateKeyBackup.pvk',
    ENCRYPTION BY PASSWORD = 'StrongPrivateKeyPassword123!'
);

--恢復證書
CREATE CERTIFICATE MyDatabaseCertificateName
FROM FILE = 'C:\Path\To\Backup\DatabaseCertificateBackup.cer'
WITH PRIVATE KEY (
    FILE = 'C:\Path\To\Backup\DatabaseCertificatePrivateKeyBackup.pvk',
    DECRYPTION BY PASSWORD = 'StrongPrivateKeyPassword123!'
);
GO
