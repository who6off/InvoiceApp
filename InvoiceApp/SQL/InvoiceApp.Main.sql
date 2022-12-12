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
	[Owner] INT REFERENCES [Companies]([Id]) ON DELETE SET NULL,
	[Amount] DECIMAL(11,2) NOT NULL,
	[Month] Date NOT NULL CHECK(DAY([Month])=1),
	[CrationDate] DATE NOT NULL,
	[CreatorId] VARCHAR(30),
	[Status] INT REFERENCES [InvoiceStatuses]([Id]) NOT NULL, 
	[LastUdpdateTime] DATETIME NOT NULL,
	[LastUpdateAction] INT REFERENCES [InvoiceActions]([Id]) NOT NULL,
	[LastUpdateAuthor] VARCHAR(30) NOT NULL
);


INSERT INTO [InvoiceStatuses]([Name]) VALUES ('Submitted'), ('Approved'), ('Rejected');

INSERT INTO [InvoiceActions]([Name]) VALUES ('Creation'), ('Update'), ('Approval'), ('Rejection');