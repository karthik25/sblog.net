SET NOCOUNT ON
GO

/* Create the admin settings table */
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Schema')
BEGIN
	CREATE TABLE [dbo].[Schema](
		[SchemaRecordId] [int] IDENTITY(1,1) NOT NULL,
		[ScriptName] [varchar](50) NOT NULL,
		[MajorVersion] [smallint] NOT NULL,
		[MinorVersion] [smallint] NOT NULL,
		[ScriptVersion] [smallint] NOT NULL,
		[ScriptRunDateTime] [datetime] NOT NULL,
	 CONSTRAINT [PK_Schema] PRIMARY KEY CLUSTERED 
	(
		[SchemaRecordId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'sBlog_Settings')
BEGIN
	CREATE TABLE [dbo].[sBlog_Settings](
		[KeyName] [varchar](50) NOT NULL,
		[KeyValue] [varchar](max) NULL
	) ON [PRIMARY]
END

GO

SET ANSI_PADDING OFF
GO

/* Insert the entries in to the table */
IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'InstallationComplete')
BEGIN
	INSERT INTO sBlog_Settings VALUES('InstallationComplete', 'false');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogName')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogName','sBlog.Net');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogCaption')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogCaption','Just another sBlog.net blog!');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogTheme')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogTheme',NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogPostsPerPage')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogPostsPerPage','5');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'ManageItemsPerPage')
BEGIN
	INSERT INTO sBlog_Settings VALUES('ManageItemsPerPage', '5');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSocialSharing')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSocialSharing','false');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSocialSharingChoice')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSocialSharingChoice', NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSyntaxHighlighting')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSyntaxHighlighting','false');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSyntaxTheme')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSyntaxTheme',NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSyntaxScripts')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSyntaxScripts',NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSiteErrorEmailAction')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSiteErrorEmailAction','false');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogAkismetEnabled')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogAkismetEnabled','false');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogAkismetKey')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogAkismetKey',NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogAkismetUrl')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogAkismetUrl',NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogAkismetDeleteSpam')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogAkismetDeleteSpam','false');
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogAdminEmailAddress')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogAdminEmailAddress',NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSmtpAddress')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSmtpAddress',NULL);
END

IF NOT EXISTS (SELECT * FROM sBlog_Settings WHERE KeyName = 'BlogSmtpPassword')
BEGIN
	INSERT INTO sBlog_Settings VALUES('BlogSmtpPassword', NULL);
END

GO

/* Create Users Table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Users')
BEGIN
	CREATE TABLE [dbo].[Users](
		[UserID] [int] IDENTITY(1,1) NOT NULL,
		[UserName] [varchar](50) NOT NULL,
		[Password] [varchar](50) NOT NULL,
		[UserEmailAddress] [varchar](50) NOT NULL,
		[UserDisplayName] [varchar](50) NULL,
		[UserActiveStatus] [int] NULL,
		[ActivationKey] [varchar](50) NULL,
		[OneTimeToken] [varchar](50) NULL,
		[UserCode] [varchar](128) NULL,
		[UserSite] [varchar](128) NULL,
		[LastLoginDate] [datetime] NULL,
	 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[UserID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	/* Insert the default user with the default password */
	INSERT INTO Users ([UserName],[Password],[UserEmailAddress],[UserDisplayName],[UserActiveStatus],
					   [ActivationKey],[OneTimeToken],[UserCode],[UserSite],[LastLoginDate])
	VALUES ('admin','','','admin',1,NULL,NULL,'',NULL,NULL);
END

GO

/* Create Post Table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Posts')
BEGIN
	CREATE TABLE [dbo].[Posts](
		[PostID] [int] IDENTITY(1,1) NOT NULL,
		[PostTitle] [varchar](255) NOT NULL,
		[PostContent] [varchar](max) NOT NULL,
		[PostUrl] [varchar](max) NOT NULL,
		[PostAddedDate] [datetime] NOT NULL,
		[PostEditedDate] [datetime] NULL,
		[OwnerUserID] [int] NOT NULL,
		[UserCanAddComments] [bit] NOT NULL,
		[CanBeShared] [bit] NOT NULL,
		[IsPrivate] [bit] NOT NULL,
		[EntryType] [tinyint] NOT NULL,
		[Order] [int] NULL,
	 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
	(
		[PostID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	/* Insert the default post */

	INSERT INTO [dbo].[Posts]
			   ([PostTitle],[PostContent],[PostUrl],[PostAddedDate],[PostEditedDate],[OwnerUserID],
			   [UserCanAddComments],[CanBeShared],[IsPrivate],[EntryType],[Order])
		 VALUES
			   ('Hello World!',
			   'Hello World!<br /><br />Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc non sollicitudin dui. Nunc ac augue tellus, sit amet rutrum nunc. Integer malesuada sapien tincidunt ligula vulputate blandit eu eget tellus. Praesent rhoncus neque eget augue blandit viverra. Praesent mattis gravida egestas. Integer dictum, sapien sit amet pharetra tempus, elit elit porta sem, sed fermentum tortor diam quis nulla. Sed felis sem, ultrices quis sagittis vitae, convallis at dui. Curabitur rutrum, nulla vitae semper interdum, justo velit blandit augue, ac porta lorem lorem a est. Curabitur quis metus in magna scelerisque viverra. Proin id leo eros, ullamcorper pellentesque mauris. Donec metus leo, varius at faucibus id, interdum a ipsum. Donec adipiscing tortor ac nulla convallis scelerisque. Ut posuere aliquam dolor eu viverra. Maecenas ut arcu eu lacus iaculis euismod dictum pulvinar turpis. Nulla vel sem eget lacus tristique lacinia eu id diam.<br /><br />',
			   'hello-world',GETDATE(),GETDATE(),1,'true','true','false',1,NULL)

	/* Insert the default page */
	INSERT INTO [dbo].[Posts]
			   ([PostTitle],[PostContent],[PostUrl],[PostAddedDate],[PostEditedDate],[OwnerUserID],
			   [UserCanAddComments],[CanBeShared],[IsPrivate],[EntryType],[Order])
		 VALUES
			   ('About',
			   'This is just a basic &quot;About&quot; page!<br /><br />Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc non sollicitudin dui. Nunc ac augue tellus, sit amet rutrum nunc. Integer malesuada sapien tincidunt ligula vulputate blandit eu eget tellus. Praesent rhoncus neque eget augue blandit viverra. Praesent mattis gravida egestas. Integer dictum, sapien sit amet pharetra tempus, elit elit porta sem, sed fermentum tortor diam quis nulla. Sed felis sem, ultrices quis sagittis vitae, convallis at dui. Curabitur rutrum, nulla vitae semper interdum, justo velit blandit augue, ac porta lorem lorem a est. Curabitur quis metus in magna scelerisque viverra. Proin id leo eros, ullamcorper pellentesque mauris. Donec metus leo, varius at faucibus id, interdum a ipsum. Donec adipiscing tortor ac nulla convallis scelerisque. Ut posuere aliquam dolor eu viverra. Maecenas ut arcu eu lacus iaculis euismod dictum pulvinar turpis. Nulla vel sem eget lacus tristique lacinia eu id diam.<br /><br />',
			   'about',GETDATE(),GETDATE(),1,'true','true','false',2,1)
END

GO

/* Create Comment Table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Comments')
BEGIN
	CREATE TABLE [dbo].[Comments](
		[CommentID] [int] IDENTITY(1,1) NOT NULL,
		[CommentUserFullName] [varchar](50) NOT NULL,
		[CommenterEmail] [varchar](50) NULL,
		[CommenterSite] [varchar](50) NULL,
		[CommentContent] [varchar](512) NOT NULL,
		[CommentPostedDate] [datetime] NOT NULL,
		[CommentStatus] [int] NOT NULL,
		[PostID] [int] NOT NULL,
		[UserID] [int] NULL,
	 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
	(
		[CommentID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserID])
	REFERENCES [dbo].[Users] ([UserID]);

	/* Insert a default comment for the defaul page & post */
	INSERT INTO [dbo].[Comments]
			   ([CommentUserFullName],[CommenterEmail],[CommenterSite]
			   ,[CommentContent],[CommentPostedDate],[CommentStatus],[PostID],[UserID])
		 VALUES('admin','','','Welcome to the blogosphere!',
			   GETDATE(),0,1,NULL);
	INSERT INTO [dbo].[Comments]
			   ([CommentUserFullName],[CommenterEmail],[CommenterSite]
			   ,[CommentContent],[CommentPostedDate],[CommentStatus],[PostID],[UserID])
		 VALUES('admin','','','About Me!',
			   GETDATE(),0,2,NULL);
END

GO

/* Create Categories Table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Categories')
BEGIN
	CREATE TABLE [dbo].[Categories](
		[CategoryID] [int] IDENTITY(1,1) NOT NULL,
		[CategoryName] [varchar](50) NOT NULL,
		[CategorySlug] [varchar](MAX) NOT NULL,
	 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
	(
		[CategoryID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	/* Insert default category */
	INSERT INTO Categories (CategoryName, CategorySlug) VALUES ('General','general');
END

GO

/* Create Tags Table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Tags')
BEGIN
	CREATE TABLE [dbo].[Tags](
		[TagID] [int] IDENTITY(1,1) NOT NULL,
		[TagName] [varchar](50) NOT NULL,
		[TagSlug] [varchar](MAX) NOT NULL,
	 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
	(
		[TagID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	SET ANSI_PADDING OFF

	/* Insert a default tag */
	INSERT INTO [dbo].[Tags] (TagName, TagSlug) VALUES('general','general');

END
GO

/* Create CategoryMapping Table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'CategoryMapping')
BEGIN
	CREATE TABLE [dbo].[CategoryMapping](
		[PostCategoryMappingID] [int] IDENTITY(1,1) NOT NULL,
		[CategoryID] [int] NOT NULL,
		[PostID] [int] NOT NULL,
	 CONSTRAINT [PK_PostCategoryMapping] PRIMARY KEY CLUSTERED 
	(
		[PostCategoryMappingID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[CategoryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostCategoryMapping_Categories] FOREIGN KEY([CategoryID])
	REFERENCES [dbo].[Categories] ([CategoryID])

	ALTER TABLE [dbo].[CategoryMapping] CHECK CONSTRAINT [FK_PostCategoryMapping_Categories]

	ALTER TABLE [dbo].[CategoryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostCategoryMapping_Posts] FOREIGN KEY([PostID])
	REFERENCES [dbo].[Posts] ([PostID])

	ALTER TABLE [dbo].[CategoryMapping] CHECK CONSTRAINT [FK_PostCategoryMapping_Posts]

	/* Add a default mapping */
	INSERT INTO [dbo].[CategoryMapping]([CategoryID],[PostID]) VALUES (1,1)
END

GO

/* Create TagMapping Table */
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'TagMapping')
BEGIN
	CREATE TABLE [dbo].[TagMapping](
		[PostTagMappingID] [int] IDENTITY(1,1) NOT NULL,
		[TagID] [int] NOT NULL,
		[PostID] [int] NOT NULL,
	 CONSTRAINT [PK_PostTagMapping] PRIMARY KEY CLUSTERED 
	(
		[PostTagMappingID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[TagMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostTagMapping_Posts] FOREIGN KEY([PostID])
	REFERENCES [dbo].[Posts] ([PostID])

	ALTER TABLE [dbo].[TagMapping] CHECK CONSTRAINT [FK_PostTagMapping_Posts]

	ALTER TABLE [dbo].[TagMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostTagMapping_Tags] FOREIGN KEY([TagID])
	REFERENCES [dbo].[Tags] ([TagID])

	ALTER TABLE [dbo].[TagMapping] CHECK CONSTRAINT [FK_PostTagMapping_Tags]

	/* Add default tag mapping */
	INSERT INTO [dbo].[TagMapping] VALUES(1,1);
END

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Errors')
BEGIN
CREATE TABLE [dbo].[Errors](
	[ErrorID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorDateTime] [datetime] NOT NULL,
	[ErrorMessage] [varchar](500) NOT NULL,
	[ErrorDescription] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Errors] PRIMARY KEY CLUSTERED 
(
	[ErrorID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

SET ANSI_PADDING OFF

SET NOCOUNT OFF

END

GO
