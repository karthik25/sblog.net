/* Roles table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Roles')
BEGIN
	CREATE TABLE [dbo].[Roles](
		[RoleId] [smallint] NOT NULL,
		[RoleName] [varchar](50) NOT NULL,
		[RoleDescription] [varchar](255) NOT NULL,
	 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
	(
		[RoleId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	/* Roles table entries */
	INSERT INTO dbo.Roles VALUES(0,'SuperAdmin','Super Admin');
	INSERT INTO dbo.Roles VALUES(1, 'Admin', 'Administrator');
	INSERT INTO dbo.Roles VALUES(2, 'Author', 'Blog Author');
END
GO

/* UserRoles table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'UserRoles')
BEGIN
	CREATE TABLE [dbo].[UserRoles](
		[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
		[UserId] [int] NOT NULL,
		[RoleId] [smallint] NOT NULL,
	 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
	(
		[UserRoleId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
	REFERENCES [dbo].[Roles] ([RoleId])

	ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]

	ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
	REFERENCES [dbo].[Users] ([UserID])

	ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]

	/* Entry for 1 user */
	INSERT INTO dbo.UserRoles VALUES(1, 0);
END

/* Update the db version - to be used in the future by the soon to be written app! */
UPDATE sBlog_Settings SET KeyValue = '02_02' WHERE KeyName = 'BlogDbVersion';
