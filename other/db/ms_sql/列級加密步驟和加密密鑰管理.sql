--Column-Level Encryption(�C�ť[�K)�B�J

--1.�Ы� Master Key(�D�K�_)�A�D�K�_�O�Ω�O�@�ҮѩM�D��ٱK�_���ڱK�_
USE master; --master ��Ʈw�O SQL Server ���֤ߨt�θ�Ʈw�A�Ω�s�x���A���ŧO���t�m�M�t�ΫH���C
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'StrongPassword123!';

--2.�Ы� Certificate(�Ү�)
CREATE CERTIFICATE MyDatabaseCertificateName
WITH SUBJECT = 'My Certificate'; --SUBJECT �Ω�y�z�ҮѪ��γ~�Τ��e

/* �t�@���Үѥܽd
CREATE CERTIFICATE SalesDataCertificate
WITH SUBJECT = 'Certificate for encrypting sales data';
*/

--3.�Ы� Symmetric Key(��ٱK�_)�A��ٱK�_�Ω�[�K�M�ѱK�ƾڮw�C�����ƾ�
USE [YourDatabaseName];
CREATE SYMMETRIC KEY MySymmetricKeyName
WITH ALGORITHM = AES_256 --�ϥ� 256 ��K�_���ת� AES ��k
ENCRYPTION BY CERTIFICATE MyDatabaseCertificateName;

--���]���@�� SensitiveDataTable ��
/*
CREATE TABLE SensitiveDataTable (
    ID INT IDENTITY PRIMARY KEY,
    EncryptedColumn VARBINARY(300),
    OtherColumn NVARCHAR(100)
);*/

--4.Encrypting Data (�[�K�ƾ�)�A�b���J�ƾڮɨϥι�ٱK�_�i��[�K�C
--���}��ٱK�_
OPEN SYMMETRIC KEY MySymmetricKeyName
DECRYPTION BY CERTIFICATE MyDatabaseCertificateName;

--���J�[�K�ƾ�
INSERT INTO [YourTableName] ([EncryptedColumn], [OtherColumns])
VALUES (
	--ENCRYPTBYKEY(KEY_GUID('MySymmetricKey'), 'Sensitive Data') �����G���J�� [EncryptedColumn]
	--Other Data ���J�� OtherColumns]
    ENCRYPTBYKEY(KEY_GUID('MySymmetricKeyName'), 'Sensitive Data'), 'Other Data'
);

--������ٱK�_
CLOSE SYMMETRIC KEY MySymmetricKeyName;

--5.Decrypting Data (�ѱK�ƾ�)�A�b�d�߼ƾڮɨϥι�ٱK�_�i��ѱK�C
--���}��ٱK�_
OPEN SYMMETRIC KEY MySymmetricKeyName
DECRYPTION BY CERTIFICATE MyDatabaseCertificateName;

--�d�߸ѱK�ƾ�
SELECT 
	--�ѱK [EncryptedColumn]
	--DECRYPTBYKEY ���
	--CONVERT ��ƥΩ�N�ƾڱq�@�ؼƾ������ഫ���t�@�ؼƾ������A���ର varchar
    CONVERT(varchar, DECRYPTBYKEY([EncryptedColumn])) AS DecryptedColumn, [OtherColumns]
FROM [YourTableName];

--������ٱK�_
CLOSE SYMMETRIC KEY MySymmetricKeyName;

--�[�K�K�_�޲z (Key Management)�A�޲z�[�K�K�_���L�{�]�A�ЫءB�ƥ��M��_�K�_�C
--�ƥ��D�K�_
BACKUP MASTER KEY TO FILE = 'C:\Path\To\Backup\MasterKeyBackup.key'
ENCRYPTION BY PASSWORD = 'StrongBackupPassword123!';

--��_�D�K�_
RESTORE MASTER KEY FROM FILE = 'C:\Path\To\Backup\MasterKeyBackup.key'
DECRYPTION BY PASSWORD = 'StrongBackupPassword123!'
ENCRYPTION BY PASSWORD = 'StrongPassword123!';

--�ƥ��Ү�
--.cer ���G�o�O�ҮѤ��A�]�t���}�K�_ (public key) �H�Τ@���ҮѪ����ƾڡC
--.pvk ���G�o�O�p���K�_���A�]�t�P�Үѹ������p���K�_ (private key)�C
BACKUP CERTIFICATE MyDatabaseCertificateName
TO FILE = 'C:\Path\To\Backup\DatabaseCertificateBackup.cer'
WITH PRIVATE KEY (
    FILE = 'C:\Path\To\Backup\DatabaseCertificatePrivateKeyBackup.pvk',
    ENCRYPTION BY PASSWORD = 'StrongPrivateKeyPassword123!'
);

--��_�Ү�
CREATE CERTIFICATE MyDatabaseCertificateName
FROM FILE = 'C:\Path\To\Backup\DatabaseCertificateBackup.cer'
WITH PRIVATE KEY (
    FILE = 'C:\Path\To\Backup\DatabaseCertificatePrivateKeyBackup.pvk',
    DECRYPTION BY PASSWORD = 'StrongPrivateKeyPassword123!'
);
GO
