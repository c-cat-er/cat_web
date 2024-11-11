--啟用 AlwaysOn 可用性組步驟

--1.設置數據庫完整恢復模式，才能做完整備份、差異備份和事務日誌備份
ALTER DATABASE [YourDatabaseName] SET RECOVERY FULL;
--備份
BACKUP DATABASE [YourDatabaseName] TO DISK = 'C:\Backup\YourDatabaseName.bak';

--2.YourAGName(可用性組)的故障轉移
ALTER AVAILABILITY GROUP [YourAGName] FAILOVER;

--3.重複執行步驟1.

--4.創建可用性組
CREATE AVAILABILITY GROUP [YourAGName]
FOR DATABASE [YourDatabaseName] --要包含的Db
REPLICA ON --配置可用性組中的副本(複製伺服器)
	--配置主副本
  N'Server1' WITH (
    ENDPOINT_URL = N'TCP://Server1:5022', --用於數據同步的端點 URL
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, --指定同步提交模式，確保數據在主副本和次要副本之間同步。
    FAILOVER_MODE = AUTOMATIC, --設置自動故障轉移模式，當主副本故障時，次要副本會自動接管。
    SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY) --允許次要副本的讀取訪問。
  ),
  	--配置次副本
  N'Server2' WITH (
    ENDPOINT_URL = N'TCP://Server2:5022',
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    FAILOVER_MODE = AUTOMATIC,
    SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY)
  );

--5.添加次要副本到指定 YourAGName(可用性組)
ALTER AVAILABILITY GROUP [YourAGName] JOIN;
GO
