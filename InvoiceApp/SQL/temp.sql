INSERT INTO [Invoices] VALUES
(
	3,
	1000,
	'2022-10-01',
	'2022-12-08',
	'1', 
	(SELECT [Id] FROM [InvoiceStatuses] WHERE [Name]='Submitted'),
	'2022-12-08',
	(SELECT [Id] FROM [InvoiceActions] WHERE [Name]='Creation'),
	'1'
);

--INSERT INTO [Companies]([Name]) VALUES ('BMW');
--SELECT * FROM [Companies] WHERE [Id]= SCOPE_IDENTITY();


--SELECT 
--	[Month],
--	[OwnerName],
--	SUM([Amount]) AS [Sum]
--FROM [InvoicesView]
--WHERE [OwnerId]=1
--GROUP BY [Month], [OwnerName]


SELECT 
	[Month],
	SUM([Amount]) AS [Sum]
FROM [InvoicesView]
WHERE YEAR([Month])=2022
GROUP BY [Month]