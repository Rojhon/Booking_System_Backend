CREATE TABLE Users (
Id int identity (1,1) not null,
UserNumber varchar(50) null,
FirstName varchar(50) null,
LastName varchar(50) null,
Position varchar(50) null,
Email varchar(50) null,
Password varchar(50) null,
Role varchar(50) null,
Verified varchar(50) null,
Active varchar(50) null,
CreatedAt DateTime null,
UpdatedAt DateTime null,
FinishedAt DateTime null,
);

CREATE TABLE Positions (
Id int identity (1,1) not null,
Position varchar(50) null,
CreatedAt DateTime null,
UpdatedAt DateTime null,
);

CREATE TABLE [dbo].[Offices]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL
)

CREATE TABLE [dbo].[Services]
(
    [Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [Fee] DECIMAL(18, 2) NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL
)

CREATE TABLE [dbo].[ Requests]
(
    [Id] INT IDENTITY(1,1)  NOT NULL , 
    [TrackingId] INT NOT NULL, 
    [OfficeId] INT NOT NULL, 
    [ServiceId] INT NOT NULL, 
    [StatusId] INT NOT NULL, 
    [UserNote] VARCHAR(50) NULL, 
    [OfficeNote] VARCHAR(50) NULL, 
    [LinkToFile] VARCHAR(50) NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL, 
    [FinishedAt] DATETIME NULL, 
    PRIMARY KEY ([Id])
)

CREATE TABLE [dbo].[TrackingStatuses]
(
    [Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [RequestId] INT NULL, 
    [TrackingStatus] VARCHAR(50) NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL, 
)

CREATE TABLE [dbo].[RequestStatuses]
(
   [Id] INT IDENTITY(1,1)  NOT NULL PRIMARY KEY, 
    [RequestId] INT NULL, 
    [Type] VARCHAR(50) NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL, 
)

CREATE TABLE [dbo].[RequestHistory]
(
   [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [RequestId] INT NULL, 
    [History] VARCHAR(50) NULL, 
    [CreatedAt] DATETIME NULL, 
    [UpdatedAt] DATETIME NULL, 
)