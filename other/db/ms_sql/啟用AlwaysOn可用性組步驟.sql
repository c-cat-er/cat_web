--�ҥ� AlwaysOn �i�ΩʲըB�J

--1.�]�m�ƾڮw�����_�Ҧ��A�~�వ����ƥ��B�t���ƥ��M�ưȤ�x�ƥ�
ALTER DATABASE [YourDatabaseName] SET RECOVERY FULL;
--�ƥ�
BACKUP DATABASE [YourDatabaseName] TO DISK = 'C:\Backup\YourDatabaseName.bak';

--2.YourAGName(�i�Ωʲ�)���G���ಾ
ALTER AVAILABILITY GROUP [YourAGName] FAILOVER;

--3.���ư���B�J1.

--4.�Ыإi�Ωʲ�
CREATE AVAILABILITY GROUP [YourAGName]
FOR DATABASE [YourDatabaseName] --�n�]�t��Db
REPLICA ON --�t�m�i�Ωʲդ����ƥ�(�ƻs���A��)
	--�t�m�D�ƥ�
  N'Server1' WITH (
    ENDPOINT_URL = N'TCP://Server1:5022', --�Ω�ƾڦP�B�����I URL
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, --���w�P�B����Ҧ��A�T�O�ƾڦb�D�ƥ��M���n�ƥ������P�B�C
    FAILOVER_MODE = AUTOMATIC, --�]�m�۰ʬG���ಾ�Ҧ��A��D�ƥ��G�ٮɡA���n�ƥ��|�۰ʱ��ޡC
    SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY) --���\���n�ƥ���Ū���X�ݡC
  ),
  	--�t�m���ƥ�
  N'Server2' WITH (
    ENDPOINT_URL = N'TCP://Server2:5022',
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    FAILOVER_MODE = AUTOMATIC,
    SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY)
  );

--5.�K�[���n�ƥ�����w YourAGName(�i�Ωʲ�)
ALTER AVAILABILITY GROUP [YourAGName] JOIN;
GO
