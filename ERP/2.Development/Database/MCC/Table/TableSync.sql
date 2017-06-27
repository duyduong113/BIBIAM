
CREATE TABLE [dbo].[TableSync](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ten_bang] [nvarchar](100) NULL,
	[ten_sp] [nvarchar](100) NULL,
 CONSTRAINT [PK_TableSync] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
) ON [PRIMARY]

GO


