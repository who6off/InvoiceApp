CREATE TABLE [Companies]
(
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(100) UNIQUE NOT NULL
);


CREATE TABLE [InvoiceStatuses]
(
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(20) UNIQUE NOT NULL
);


CREATE TABLE [InvoiceActions]
(
	[Id] INT PRIMARY KEY IDENTITY,
	[Name] VARCHAR(20) UNIQUE NOT NULL
);


CREATE TABLE [Invoices]
(
	[Id] INT PRIMARY KEY IDENTITY,
	[OwnerId] INT REFERENCES [Companies]([Id]) ON DELETE SET NULL,
	[Amount] DECIMAL(11,2) NOT NULL,
	[Month] Date NOT NULL CHECK(DAY([Month])=1),
	[CreationDate] DATETIME NOT NULL,
	[CreatorId] VARCHAR(64),
	[Status] INT REFERENCES [InvoiceStatuses]([Id]) NOT NULL, 
	[LastUpdateDate] DATETIME NOT NULL,
	[LastUpdateAction] INT REFERENCES [InvoiceActions]([Id]) NOT NULL,
	[LastUpdateAuthorId] VARCHAR(64) NOT NULL
);


--CREATE VIEW [InvoiceView] AS 
--SELECT 
--	[Id], 
--	[OwnerId],
--	(SELECT [Name] FROM [Companies] WHERE [Id]=[Invoices].[OwnerId]) AS [OwnerName],
--	[Amount],
--	[Month],
--	[CreationDate],
--	[CreatorId],
--	(SELECT [Name] FROM [InvoiceStatuses] WHERE [Id]=[Invoices].[Status]) AS [Status],
--	[LastUpdateDate],
--	(SELECT [Name] FROM [InvoiceActions] WHERE [Id]=[Invoices].[LastUpdateAction]) AS [LastUpdateAction],
--	[LastUpdateAuthorId]
--FROM [Invoices];


INSERT INTO [InvoiceStatuses]([Name]) VALUES ('Submitted'), ('Approved'), ('Rejected');

INSERT INTO [InvoiceActions]([Name]) VALUES ('Creation'), ('Update'), ('Approval'), ('Rejection');