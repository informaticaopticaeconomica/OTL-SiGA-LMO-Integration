CREATE TYPE [dbo].[udtUpdateProductsInventoryInLMO] AS TABLE(
	[numberorder] [bigint] NOT NULL,
	[numberinvoice] [int] NOT NULL,
	[invoicedate] [datetime] NOT NULL,
	[affiliationid] [int] NOT NULL,
	[opcside] [char](1) NOT NULL,
	[opc] [varchar](100) NOT NULL,
	[quantity] [decimal](18, 2) NOT NULL
)
GO


