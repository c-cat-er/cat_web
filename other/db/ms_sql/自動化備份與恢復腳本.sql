--�۰ʤƳƥ��P��_�}���B�J

--1.�۰ʤƳƥ��}���A�i�H�N�o�Ӹ}���K�[��SQL Server Agent�@�~���A�]�m�w�ɰ���
BACKUP DATABASE [YourDatabaseName]
TO DISK = N'C:\Backup\YourDatabaseName_Full.bak'
WITH NOFORMAT, NOINIT,
NAME = N'YourDatabaseName-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;
--NOFORMAT�G���w SQL Server ���n���s�榡�Ƴƥ����A�p�G�ƥ����w�g�s�b�A�s���ƥ��N�Q���[���󤤡A�Ӥ��O�л\���C
--FORMAT�G���w SQL Server ���s�榡�Ƴƥ����A�R���Ҧ��{�����ƥ����C
--NOINIT�G���w SQL Server ���n��l�Ƴƥ����C�p�G�ƥ����w�g�s�b�A�s���ƥ��N�Q���[���󤤡C
--INIT�G���w SQL Server ��l�Ƴƥ����A�R���Ҧ��{�����ƥ����C
--SKIP�G���w SQL Server ���L�ƥ������W�٩M�ƥ�������ˬd�A���\�л\�{���ƥ����C
--NOREWIND�G���w SQL Server �b�ƥ��ާ@�����ᤣ�n�˱a�ϱa�C�o�˥i�H�b�P�@�ϱa�W�i��h���ƥ��Ӥ����C�������s�w��ϱa�C
--NOUNLOAD�G���w SQL Server �b�ƥ��ާ@�����ᤣ�n�����ϱa�C
--STATS�G���w�@�Ӿ�ƭȡA�C��ƥ��L�{�����F���Ȧʤ���ɡASQL Server �N��ܶi�׮����C�Ҧp�ASTATS = 10 ��ܨC���� 10% �N��ܤ@���i�׮����C

--2.�۰ʤƫ�_�}��
RESTORE DATABASE [YourDatabaseName]
FROM DISK = N'C:\Backup\YourDatabaseName_Full.bak'
WITH FILE = 1,
MOVE N'YourDatabaseName_Data' TO N'C:\Data\YourDatabaseName.mdf',
MOVE N'YourDatabaseName_Log' TO N'C:\Data\YourDatabaseName.ldf',
NOUNLOAD, STATS = 5;
GO
--FILE�G���w�n�q�ƥ��]�Ƥ���_���ƥ����A�]�C���ƥ��|���ͤ@���ƥ����A�Y���h�ӴN�n���w�O�ĴX�ӡC

--3.�ϥ� PowerShell �i��ƥ��M��_ (���F�ϥ�T-SQL�}���A�٥i�H�ϥ�PowerShell�Ӧ۰ʤƳo�Ǿާ@)
/*
# PowerShell �ƥ��}��
Import-Module SqlServer

$serverName = "YourServerName"
$databaseName = "YourDatabaseName"
$backupFile = "C:\Backup\YourDatabaseName_Full.bak"

Backup-SqlDatabase -ServerInstance $serverName -Database $databaseName -BackupFile $backupFile
*/
GO
