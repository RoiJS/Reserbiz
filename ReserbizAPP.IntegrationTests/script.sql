-- (1) Create  dbReserbizSYSSQL
CREATE DATABASE dbReserbizSYSSQL;
GO

-- (2) Create table Clients
USE [dbReserbizSYSSQL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clients](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](max) NULL,
    [DBName] [nvarchar](max) NULL,
    [DBHashName] [nvarchar](max) NULL,
    [Description] [nvarchar](max) NULL,
    [ContactNumber] [nvarchar](max) NULL,
    [DateJoined] [datetime2](7) NOT NULL,
    [DateEnded] [datetime2](7) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [IsDelete] [bit] NOT NULL,
    [DateDeleted] [datetime2](7) NOT NULL,
    [DateDeactivated] [datetime2](7) NOT NULL,
    [DateCreated] [datetime2](7) NOT NULL,
    [DateUpdated] [datetime2](7) NOT NULL,
    [Type] [int] NOT NULL,
    [DBPassword] [nvarchar](max) NULL,
    [DBServer] [nvarchar](max) NULL,
    [DBusername] [nvarchar](max) NULL,
CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


ALTER TABLE [dbo].[Clients] ADD  DEFAULT ((2)) FOR [Type]
GO

-- (3) Create table GeneralInformations
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GeneralInformations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemUpdateStatus] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateDeactivated] [datetime2](7) NOT NULL,
	[DateDeleted] [datetime2](7) NOT NULL,
	[DateUpdated] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_GeneralInformations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GeneralInformations] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DateCreated]
GO

ALTER TABLE [dbo].[GeneralInformations] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DateDeactivated]
GO

ALTER TABLE [dbo].[GeneralInformations] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DateDeleted]
GO

ALTER TABLE [dbo].[GeneralInformations] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DateUpdated]
GO

ALTER TABLE [dbo].[GeneralInformations] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO

ALTER TABLE [dbo].[GeneralInformations] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDelete]
GO


-- (3) Insert test client information
USE [dbReserbizSYSSQL]
GO

INSERT INTO [dbo].[Clients]
    ([Name]
    ,[DBName]
    ,[DBHashName]
    ,[Description]
    ,[ContactNumber]
    ,[DateJoined]
    ,[DateEnded]
    ,[IsActive]
    ,[IsDelete]
    ,[DateDeleted]
    ,[DateDeactivated]
    ,[DateCreated]
    ,[DateUpdated]
    ,[Type]
    ,[DBPassword]
    ,[DBServer]
    ,[DBusername])
VALUES
(
    'developerintegrationdb'
    ,'ReserbizDeveloperIntegrationTestDB'
    ,'10b83b59f440ec8c65e5c1356fbb62a84fdd4c1b'
    ,'1900-01-01 00:00:00.0000000'
    ,'1900-01-01 00:00:00.0000000'
    ,'1900-01-01 00:00:00.0000000'
    ,'1900-01-01 00:00:00.0000000'
    ,1
    ,0
    ,'1900-01-01 00:00:00.0000000'
    ,'1900-01-01 00:00:00.0000000'
    ,'1900-01-01 00:00:00.0000000'
    ,'1900-01-01 00:00:00.0000000'
    ,1
    ,''
    ,''
    ,''
)
GO


-- (4) Insert general information 
INSERT INTO [dbo].[GeneralInformations]
           ([SystemUpdateStatus]
           ,[DateCreated]
           ,[DateDeactivated]
           ,[DateDeleted]
           ,[DateUpdated]
           ,[IsActive]
           ,[IsDelete])
     VALUES
           (0
           ,'0001-01-01 00:00:00.0000000'
           ,'0001-01-01 00:00:00.0000000'
           ,'0001-01-01 00:00:00.0000000'
           ,'0001-01-01 00:00:00.0000000'
           ,1
           ,0)
GO


-- (4) Create database for integration testing
CREATE DATABASE ReserbizDeveloperIntegrationTestDB;
GO