							
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cms_posts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)							
drop table [dbo].[cms_posts]							
GO							
							
CREATE TABLE [dbo].[cms_posts] (							
	[id] [bigint] IDENTITY (1,1) NOT NULL, 						
	[type] [nvarchar] (100) NULL, 						
	[name] [nvarchar] (100) NULL, 						
	[description] [nvarchar] (1000) NULL, 						
	[content] [nvarchar] (MAX) NULL, 						
	[imagespublicid] [nvarchar] (255) NULL, 						
	[createdAt] [datetime] NOT NULL, 						
	[createdBy] [nvarchar] (255) NULL, 						
	[updateAt] [datetime] NOT NULL, 											
	[allUser] [bit] NULL, 						
	[isDefault] [bit] NULL, 						
	[hashTagCode] [nvarchar] (500) NOT NULL, 						
	[companycode] [nvarchar] (30) NOT NULl,		
	[viewNumber] [int] NOT NULL,
	[group] [nvarchar] (50) NOT NULL,
	[source] [nvarchar] (100) NULL,
	[sourceLink] [nvarchar] (100) NULL							
) ON [PRIMARY]							
GO							
							
ALTER TABLE [dbo].[cms_posts] ADD							
	CONSTRAINT [PK_cms_posts] PRIMARY KEY  CLUSTERED						
	(						
		[id]					
							
							
	)  ON [PRIMARY]						
GO							
