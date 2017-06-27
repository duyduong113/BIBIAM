CREATE TABLE [dbo].[FAQ](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TopicId] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Question] [nvarchar](1000) NULL,
	[Answer] [nvarchar](4000) NULL,
	[Published] [bit] NOT NULL,
	[View] [float] NOT NULL,
	[Like] [float] NOT NULL,
	[Unlike] [float] NOT NULL,
	[Groups] [nvarchar](4000) NULL,
	[Users] [nvarchar](4000) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
