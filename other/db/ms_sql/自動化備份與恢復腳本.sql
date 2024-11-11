--自動化備份與恢復腳本步驟

--1.自動化備份腳本，可以將這個腳本添加到SQL Server Agent作業中，設置定時執行
BACKUP DATABASE [YourDatabaseName]
TO DISK = N'C:\Backup\YourDatabaseName_Full.bak'
WITH NOFORMAT, NOINIT,
NAME = N'YourDatabaseName-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, STATS = 10;
--NOFORMAT：指定 SQL Server 不要重新格式化備份文件，如果備份文件已經存在，新的備份將被附加到文件中，而不是覆蓋文件。
--FORMAT：指定 SQL Server 重新格式化備份文件，刪除所有現有的備份集。
--NOINIT：指定 SQL Server 不要初始化備份文件。如果備份文件已經存在，新的備份將被附加到文件中。
--INIT：指定 SQL Server 初始化備份文件，刪除所有現有的備份集。
--SKIP：指定 SQL Server 跳過備份集的名稱和備份日期的檢查，允許覆蓋現有備份集。
--NOREWIND：指定 SQL Server 在備份操作結束後不要倒帶磁帶。這樣可以在同一磁帶上進行多次備份而不必每次都重新定位磁帶。
--NOUNLOAD：指定 SQL Server 在備份操作結束後不要卸載磁帶。
--STATS：指定一個整數值，每當備份過程完成了此值百分比時，SQL Server 就顯示進度消息。例如，STATS = 10 表示每完成 10% 就顯示一次進度消息。

--2.自動化恢復腳本
RESTORE DATABASE [YourDatabaseName]
FROM DISK = N'C:\Backup\YourDatabaseName_Full.bak'
WITH FILE = 1,
MOVE N'YourDatabaseName_Data' TO N'C:\Data\YourDatabaseName.mdf',
MOVE N'YourDatabaseName_Log' TO N'C:\Data\YourDatabaseName.ldf',
NOUNLOAD, STATS = 5;
GO
--FILE：指定要從備份設備中恢復的備份集，因每次備份會產生一份備份集，若有多個就要指定是第幾個。

--3.使用 PowerShell 進行備份和恢復 (除了使用T-SQL腳本，還可以使用PowerShell來自動化這些操作)
/*
# PowerShell 備份腳本
Import-Module SqlServer

$serverName = "YourServerName"
$databaseName = "YourDatabaseName"
$backupFile = "C:\Backup\YourDatabaseName_Full.bak"

Backup-SqlDatabase -ServerInstance $serverName -Database $databaseName -BackupFile $backupFile
*/
GO
