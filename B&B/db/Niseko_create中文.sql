USE [Niseko]
GO

/*�Ыت�*/
create table [����] (
    [����ID] tinyint primary key identity(1,1),
    [�����W��] nvarchar(50) UNIQUE not null
);

create table [���u] (
    [���uID] int primary key identity(1,1),
	[���u�N�X] char(4) NOT NULL CHECK([���u�N�X] LIKE 'E%'), --'E'+2
    [�m��] nvarchar(50) not null,
    [�W�r] nvarchar(50) not null,
    [����ID] tinyint not null,
    [¾��] nvarchar(50) not null,
    [���Τ��] date not null DEFAULT GETDATE(),
	[�Ѷ����] date,
    [�ͤ�] date not null,
	[�H�c] varchar(50) CHECK ([�H�c] LIKE '%@%'),
    [�b��] varchar(20) unique not null check (len([�b��]) between 4 and 20),
    [�K�X����] varbinary(64) not null, --k-p,
    [�K�X�Q] varbinary(128) not null, --k-p,
	[�v��] tinyint not null default 1, --0�¦W�� / 1�@�� / 2���
	constraint [FK_���u_����ID] foreign key ([����ID]) references [����]([����ID])
);

create table [���u�n���O��] (
    [�n��ID] int primary key identity(1,1),
    [���uID] int not null,
    [IP�a�}] varchar(50), --�}�o�ɧאּ NOT NULL
    [�n������ɶ�] datetime not null DEFAULT GETDATE(),
	[�n�X����ɶ�] datetime,
	constraint [FK_���u�n���O��_���uID] foreign key ([���uID]) references [���u]([���uID])
);

create table [�нm�ŧO](
    [�нm�ŧOID] tinyint primary key identity(1,1),
    [�ŧO�W��] nvarchar(50) UNIQUE not null
);

create table [�нm](
    [�нmID] int primary key identity(1,1),
	[�w�o��] bit not null DEFAULT 0,
    [�нm�N�X] char(3) UNIQUE not null CHECK([�нm�N�X] LIKE 'O%'), --'O'+2
    [�нm�m�W] nvarchar(50) not null,
    [�нm�ŧOID] tinyint not null DEFAULT 1,
	[���Τ��] date not null DEFAULT GETDATE(),
	[�Ѷ����] date,
    constraint [FK_�нm_�нm�ŧOID] foreign key ([�нm�ŧOID]) references [�нm�ŧO]([�нm�ŧOID])
);

create table [�нm����](
    [�нm����ID] int primary key identity(1,1),
    [�нm�ŧOID] tinyint not null,
    [�O�_���p��] bit not null,
    [�нm����] decimal(10,2) not null,
    constraint [FK_�нm����_�нm�ŧOID] foreign key ([�нm�ŧOID]) references [�нm�ŧO]([�нm�ŧOID])
);

create table [�u�f��](
    [�u�f��ID] tinyint primary key identity(1,1),
    [�w�o��] bit not null DEFAULT 0,
    [�u�f��N�X] char(3) unique not null CHECK([�u�f��N�X] LIKE 'P%'), --'P'+2
	[�u�f��W��] nvarchar(50) UNIQUE not null,
    [�}�l���] date,
    [�������] date
);

------------------------

create table [�D�|��](
    [�D�|��ID] int primary key identity(1,1),
    [�D�|���W��] nvarchar(50) unique not null,
    [��a�N�X] varchar(10) not null, --�{���̥~�a
    [�q��] varchar(20) unique CHECK([�q��] LIKE '[0-9]%'),
	[�q�l�l��] varchar(50) unique CHECK ([�q�l�l��] LIKE '%@%'),
	[�v��] tinyint not null default 1
);

create table [�|���Ƴ��ŧO](
    [�Ƴ��ŧOID] tinyint primary key identity(1,1),
    [�Ƴ��ŧO�W��] nvarchar(50) unique not null
);

create table [�|��](
    [�|��ID] int primary key identity(1,1),
    [�|���N�X] char(10) unique not null CHECK([�|���N�X] LIKE 'M%'), --'M' + 3�^/�� + 6�y����
    [�Ƴ��ŧOID] tinyint not null default 1,
    [�|���W��] nvarchar(50) not null,
    [�ʧO] char(1) not null default 'N', --M(�k) /F(�k) /O(��L) / N(���K�[)
    [�ͤ�] date,
    [��a�N�X] varchar(10) not null, --�{���̥~�a
    [�q��] varchar(20) check([�q��] like '[0-9]%'),
	[�q�l�l��] varchar(50) CHECK ([�q�l�l��] LIKE '%@%'),
	[�v��] tinyint not null default 1, --0�¦W�� / 1�@�� / 2���
    constraint [FK_�|��_�Ƴ��ŧOID] foreign key ([�Ƴ��ŧOID]) references [�|���Ƴ��ŧO]([�Ƴ��ŧOID])
);

create table [�|���n������](
    [�n������ID] tinyint primary key identity(1,1),
    [�n�������W��] nvarchar(20) unique not null
);

create table [�|�������b��](
    [�����b��ID] int primary key identity(1,1),
    [�|��ID] int not null,
    [�b��] varchar(50) unique not null check (len([�b��]) between 4 and 20),
    [�K�X����] varbinary(64) not null, --k-p, 
    [�K�X�Q] varbinary(128) not null, --k-p, 
    constraint [FK_�|�������b��_�|��ID] foreign key ([�|��ID]) references [�|��]([�|��ID])
);

create table [�|���ĤT��b��](
    [�ĤT��b��ID] int primary key identity(1,1),
    [�|��ID] int not null,
    [�n������ID] tinyint not null,
    [�ĤT��ߤ@ID] varchar(300) not null,
    constraint [FK_�|���ĤT��b��_�|��ID] foreign key ([�|��ID]) references [�|��]([�|��ID]),
    constraint [FK_�|���ĤT��b��_�n������ID] foreign key ([�n������ID]) references [�|���n������]([�n������ID])
);

create table [�|���n���O��](
    [�n���O��ID] int primary key identity(1,1),
    [�|��ID] int not null,
    [�n������ID] tinyint not null,
    [�n���ɰ�] varchar(50), --k-p,   --�}�o�ɧאּ NOT NULL
    [�n��IP�a�}] varchar(50), --k-p,  --�}�o�ɧאּ NOT NULL
    [�n������ɶ�] datetime not null default getdate(),
    [�n�X����ɶ�] datetime,
    constraint [FK_�|���n���O��_�|��ID] foreign key ([�|��ID]) references [�|��]([�|��ID]),
    constraint [FK_�|���n���O��_�n������ID] foreign key ([�n������ID]) references [�|���n������]([�n������ID])
);

create table [������](
    [������ID] tinyint primary key identity(1,1),
	[�٦b�p��] bit NOT NULL DEFAULT 1,
    [�����ӥN�X] char(5) unique not null,
    [�����ӦW��] nvarchar(50) not null,
	[�p���H�W��] nvarchar(20) not null,
	[�p���H�q��] varchar(20) UNIQUE not null check([�p���H�q��] like '[0-9]%'), --���u�Ҽ{���a�q�ܸ�
);

create table [���~����](
    [����ID] int primary key identity(1,1),
    [�w�o��] bit not null DEFAULT 0,
    [������ID] tinyint not null,
    [���ʥN�X] char(4) unique not null CHECK([���ʥN�X] LIKE 'V%'),
    [���ʦW��] nvarchar(50) unique not null,
    [�}�l����ɶ�] datetime,
    [��������ɶ�] datetime,
    [���ʻ���] decimal(10,2) not null,
    constraint [FK_���~����_������ID] foreign key ([������ID]) references [������]([������ID])
);

create table [�a�I](
    [�a�IID] tinyint primary key identity(1,1),
    [�a�I�W��] nvarchar(50) unique not null
);

create table [���~���J](
    [���JID] int primary key identity(1,1),
    [���J�N�X] char(6) unique not null CHECK([���J�N�X] LIKE 'H%'),
    [���J�W��] nvarchar(50) unique not null,
    [�y�z] nvarchar(200),
    [�a�}ID] tinyint not null,
    constraint [FK_���~���J_�a�}ID] foreign key ([�a�}ID]) references [�a�I]([�a�IID])
);

create table [���~���J�ж�](
    [���J�ж�ID] int primary key identity(1,1),
    [�w�o��] bit not null DEFAULT 0,
    [���JID] int not null,
    [�ж��N�X] char(3) not null, --�Ӽh�и�
    [�D�Ǽƶq] tinyint not null,
    [�Z�Ҽƶq] tinyint not null,
    [�j�ɼƶq] tinyint not null,
    [��H�ɼƶq] tinyint not null,
    [�̤j�e�q] varchar(5) not null, --k-p, 10-15 (�H)
	[�w�M��] bit not null, --TR1
    [�̫�M�����ɶ�] datetime not null,
    constraint [FK_���~���J�ж�_���JID] foreign key ([���JID]) references [���~���J]([���JID])
);

create table [���~���J����](
    [���J����ID] int primary key identity(1,1),
    [���J�ж�ID] int not null,
    [�O�_���u] bit not null,
    [�J��Ѽ�] tinyint not null,
    [�J�����] decimal(10,2) not null,
    constraint [FK_���~���J����_���J�ж�ID] foreign key ([���J�ж�ID]) references [���~���J�ж�]([���J�ж�ID])
);

create table [���~����](
    [����ID] tinyint primary key identity(1,1),
	[�w�o��] bit not null DEFAULT 0,
    [����N�X] char(4) unique not null CHECK([����N�X] LIKE 'S%'),
    [����W��] nvarchar(50) unique not null,
    [�̤j�e�q] tinyint not null
);

create table [���~�������](
    [�������ID] int primary key identity(1,1),
    [����ID] tinyint not null,
	--[�a�I�զX] char(2) not null, --k-p, 1�r��(�W���a�IID) + 1�r��(�U���a�IID)�A�q [�a�I] ���o
	[�W���a�IID] tinyint NOT NULL,
	[�U���a�IID] tinyint NOT NULL,
	[�������] decimal(10,2) not null,
    constraint [FK_���~�������_����ID] foreign key ([����ID]) references [���~����]([����ID]),
	constraint [FK_���~�������_�W���a�IID] FOREIGN KEY ([�W���a�IID]) REFERENCES [�a�I]([�a�IID]),
    constraint [FK_���~�������_�U���a�IID] FOREIGN KEY ([�U���a�IID]) REFERENCES [�a�I]([�a�IID])
);

create table [���~�ҵ{](
    [�ҵ{ID] int primary key identity(1,1),
	[�w�o��] bit not null DEFAULT 0,
    [�ҵ{�N�X] char(4) unique not null CHECK([�ҵ{�N�X] LIKE 'C%'),
    [�ҵ{�W��] nvarchar(50) unique not null
);

create table [���~�ҵ{����](
    [�ҵ{����ID] int primary key identity(1,1),
    [�ҵ{ID] int not null,
	[�ҵ{�զX] char(3) not null, --k-p, 1�r��(single/double board) +1�r��(3H/6H) +1�r��(����/�^��/���)
	[�ҵ{����] decimal(10,2) not null, --�C��
	constraint [FK_���~�ҵ{����_�ҵ{ID] foreign key ([�ҵ{ID]) references [���~�ҵ{]([�ҵ{ID])
);

create table [���~�������O](
    [�������OID] tinyint primary key identity(1,1),
    [�������O�W��] nvarchar(50) unique not null
);

create table [���~����](
    [����ID] int primary key identity(1,1),
    [�w�o��] bit not null DEFAULT 0,
    [������ID] tinyint not null,
    [�������OID] tinyint not null,
    [����N�X] char(5) unique not null CHECK([����N�X] LIKE 'Q%'),
    [����W��] nvarchar(50) unique not null,
    [�y�z] nvarchar(200),
    [�ƶq] int not null,
    [�������] decimal(10,2) not null, --�C��
    [�Ƶ�] nvarchar(200), --�p�l�a
    constraint [FK_���~����_������ID] foreign key ([������ID]) references [������]([������ID]),
    constraint [FK_���~����_�������OID] foreign key ([�������OID]) references [���~�������O]([�������OID])
);

create table [���~�H��](
    [�H��ID] int primary key identity(1,1),
	[�������OID] tinyint not null,
    [�H�����] decimal(10,2) not null, --�C��
	constraint [FK_���~�H��_�������OID] foreign key ([�������OID]) references [���~�������O]([�������OID])
);

create table [��������](
    [��������ID] tinyint primary key identity(1,1),
    [�����W��] nvarchar(50) unique not null
);

create table [����](
    [����ID] int primary key identity(1,1),
    [��������ID] tinyint not null,
    [���ҦW��] nvarchar(50) unique not null,
    constraint [FK_����_��������ID] foreign key ([��������ID]) references [��������]([��������ID])
);

create table [���~���ҬM�g](--�@�k�@ �s�����MID�C�@�k�G �s�N�X�C
    [���~���ҬM�gID] int primary key identity(1,1),
	[���~����] char(1) not null,
	--H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [���~ID] int not null,
	--[���J�ж�ID] or [�ҵ{�N�X]+[�ҵ{�զX] or [����ID] or [����ID] or [�H��ID]
    [���~�N�X] varchar(9) not null,
	--[���J�N�X] or [�ҵ{�N�X]+[�ҵ{�զX] or [����N�X] or [����N�X] or [�H��N�X]
    [����ID] int not null,
    constraint [FK_���~���ҬM�g_����ID] foreign key ([����ID]) references [����]([����ID])
);

create table [�|���ʪ���]( --�@��
    [�ʪ���ID] int primary key identity(1,1),
    [�|��ID] int not null,
	[���~����] char(1) not null, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [���~ID] int not null, --[���J�ж�ID] or [�ҵ{�N�X]+[�ҵ{�զX] or [����ID] or [����ID] or [�H��ID]
    constraint [FK_�|���ʪ���_�|��ID] foreign key ([�|��ID]) references [�|��]([�|��ID])
);

create table [�|���ʪ������J]( --���J ���e��
    [�ʪ������JID] int primary key identity(1,1),
	[�ʪ���ID] int not null,
	[���e�a�IID] tinyint, --�i�����e
	[�e�F�a�IID] tinyint,
    [�}�l����ɶ�] datetime not null,
    [��������ɶ�] datetime not null,
	[�Ƶ�] nvarchar(50), --�i�۶�W�U���a�I
    constraint [FK_�|���ʪ������J_�ʪ������JID] foreign key ([�ʪ������JID]) references [�|���ʪ���]([�ʪ���ID]),
	constraint [FK_�|���ʪ������J_���e�a�IID] foreign key ([���e�a�IID]) references [�a�I]([�a�IID]),
    constraint [FK_�|���ʪ������J_�e�F�a�IID] foreign key ([�e�F�a�IID]) references [�a�I]([�a�IID])
);

create table [�|���ʪ����ҵ{]( --�ҵ{ ���� �H���
    [�ʪ����ҵ{ID] int primary key identity(1,1),
	[�ʪ���ID] int not null,
	[�a�IID] tinyint not null, -- �ҵ{�a�I or ���ɦa�I or �H��a�I
	[�Ѽ�] tinyint not null, --�ҵ{�Ѽ� or ���ɤѼ� or �H��Ѽ�
	[�H��] tinyint not null, -- �ҵ{�H�� or ���ɼƶq or �H��ƶq
	[�Ƶ�] nvarchar(50),
    constraint [FK_�|���ʪ����ҵ{_�ʪ����ҵ{ID] foreign key ([�ʪ����ҵ{ID]) references [�|���ʪ���]([�ʪ���ID]),
    constraint [FK_�|���ʪ����ҵ{_�a�IID] foreign key ([�a�IID]) references [�a�I]([�a�IID])
);

create table [�q��I�ڤ覡](
    [�I�ڤ覡ID] tinyint primary key identity(1,1),
    [�I�ڤ覡�W��] nvarchar(50) unique not null
);

create table [�q��](
    [�q��ID] int primary key identity(1,1),
    [�q��N�X] char(10) unique not null CHECK([�q��N�X] LIKE 'O%'),
    [�|��ID] int not null,
    [�Ыؤ���ɶ�] datetime not null default getdate(),
    [��l���B] decimal(10,2) not null,
	[�馩�u�f��ID] tinyint,
    [�̲ת��B] decimal(10,2) not null,
    constraint [FK_�q��_�|��ID] foreign key ([�|��ID]) references [�|��]([�|��ID]),
    constraint [FK_�q��_�馩�u�f��ID] foreign key ([�馩�u�f��ID]) references [�u�f��]([�u�f��ID])
);

create table [�q�����]( --�@��
    [�q�����ID] int primary key identity(1,1),
    [�q��ID] int not null,
	[���~����] char(1) not null, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [���~ID] int not null, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
    constraint [FK_�q�����_�q��ID] foreign key ([�q��ID]) references [�q��]([�q��ID])
);

create table [�q����ӥ��J]( --���J ���e��
	[�q����ӥ��JID] int primary key identity(1,1),
	[�q�����ID] int not null,
	[���e�a�IID] tinyint, --�i�����e
	[�e�F�a�IID] tinyint,
	[�}�l����ɶ�] datetime not null,
    [��������ɶ�] datetime not null,
	[�Ƶ�] nvarchar(100), --�i�۶�W�U���a�I
	[�O�_����] bit not null DEFAULT 0, --�����Ȥ�O�_�p���u��
	[�q����Ӫ��B] decimal(10,2) not null,
    constraint [FK_�q����ӥ��J_�q�����ID] foreign key ([�q�����ID]) references [�q�����]([�q�����ID]),
	constraint [FK_�q����ӥ��J_���e�a�IID] foreign key ([���e�a�IID]) references [�a�I]([�a�IID]),
    constraint [FK_�q����ӥ��J_�e�F�a�IID] foreign key ([�e�F�a�IID]) references [�a�I]([�a�IID])
);

create table [�q����ӽҵ{]( --�ҵ{ ���� �H���
	[�q����ӽҵ{ID] int primary key identity(1,1),
	[�q�����ID] int not null,
	[�a�IID] tinyint not null,
	[�Ѽ�] tinyint not null,
	[�H��] tinyint not null,
	[�Ƶ�] nvarchar(100), --�i�۶�W�U���a�I
	[�O�_����] bit not null DEFAULT 0, --�����Ȥ�O�_�p���u��
	[�q����Ӫ��B] decimal(10,2) not null,
    constraint [FK_�q����ӽҵ{_�q�����ID] foreign key ([�q�����ID]) references [�q�����]([�q�����ID]),
    constraint [FK_�q����ӽҵ{_�a�IID] foreign key ([�a�IID]) references [�a�I]([�a�IID])
);

create table [�q��I�ڪ��A](
    [�I�ڪ��AID] tinyint primary key identity(1,1),
    [�I�ڪ��A�W��] nvarchar(50) unique not null
);

create table [�q��I�ڰO��](
    [�I�ڰO��ID] int primary key identity(1,1),
    [�q��ID] int not null,
    [�I�ڤ覡ID] tinyint not null,
    [�Ȧ�ID] smallint, --�}�o�ɧאּ NOT NULL
	[���d�H�W��] nvarchar(50), --�}�o�ɧאּ NOT NULL
    [�d��] varchar(20), --�}�o�ɧאּ NOT NULL
    [�d���Ħ~] char(3), --����~ --�}�o�ɧאּ NOT NULL
    [�d���Ĥ�] char(2), --�}�o�ɧאּ NOT NULL
    [�I�ڪ��AID] tinyint not null,
    [�I�ڤ���ɶ�] datetime not null default getdate(),
    [�I�ڪ��B] decimal(10,2) not null,
    constraint [FK_�q��I�ڰO��_�q��ID] foreign key ([�q��ID]) references [�q��]([�q��ID]),
    constraint [FK_�q��I�ڰO��_�I�ڤ覡ID] foreign key ([�I�ڤ覡ID]) references [�q��I�ڤ覡]([�I�ڤ覡ID]),
    constraint [FK_�q��I�ڰO��_�I�ڪ��AID] foreign key ([�I�ڪ��AID]) references [�q��I�ڪ��A]([�I�ڪ��AID])
);

create table [�|������](
    [����ID] int primary key identity(1,1),
    [�|��ID] int not null,
	[���~����] char(1) not null, --H(Homestay) / C(Course) / S(Shuttle) / E(Equipment) / T(Storage)
    [���~ID] int not null, --[HomestayRoomID] or [CourseCode]+[CourseCombo] or [ShuttleID] or [EquipmentID] or [StorageID]
    --[���~�N�X] varchar(6) not null,
    [���פ���ɶ�] datetime,
    [���פ��e] nvarchar(200),
    constraint [FK_�|������_�|��ID] foreign key ([�|��ID]) references [�|��]([�|��ID])
);

create table [�f�p���] ( --��x��
    [�f�pID] int primary key identity(1,1),
    [��W��] varchar(50) not null,
    [�ާ@����] char(1) not null, --I(INSERT) / U(UPDATE) / D(DELETE)
    [�ާ@����ɶ�] datetime not null default getdate(),
    [�Τ�ID] int not null,
    [�­�] nvarchar(300) not null,
    [�s��] nvarchar(300) not null
);
GO
