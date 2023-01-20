IF OBJECT_ID('dbo.InvoiceView', 'V') IS NOT NULL
    DROP VIEW dbo.InvoiceView
GO

CREATE VIEW [InvoiceView] AS 
SELECT 
	[Id], 
	[OwnerId],
	(SELECT [Name] FROM [Companies] WHERE [Id]=[Invoices].[OwnerId]) AS [OwnerName],
	[Amount],
	[Month],
	[CreationDate],
	[CreatorId],
	(SELECT [Name] FROM [InvoiceStatuses] WHERE [Id]=[Invoices].[Status]) AS [Status],
	[LastUpdateDate],
	(SELECT [Name] FROM [InvoiceActions] WHERE [Id]=[Invoices].[LastUpdateAction]) AS [LastUpdateAction],
	[LastUpdateAuthorId]
FROM [Invoices]