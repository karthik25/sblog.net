SET NOCOUNT ON
GO

PRINT 'Initializing the sBlog.Net database installation...';
PRINT '================== Initial Install ================';
PRINT '';
GO

/* Create the admin settings table */
PRINT '******************************************************************';
PRINT '*** Creating the sBlog.Net settings table                      ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[sBlog_Settings](
	[KeyName] [varchar](50) NOT NULL,
	[KeyValue] [varchar](max) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Insert the entries in to the table */
PRINT '******************************************************************';
PRINT '*** Adding the default settings                                ***';
GO

INSERT INTO sBlog_Settings VALUES('InstallationComplete', 'false');

INSERT INTO sBlog_Settings VALUES('BlogName','sBlog.Net');
INSERT INTO sBlog_Settings VALUES('BlogCaption','Just another sBlog.net blog!');
INSERT INTO sBlog_Settings VALUES('BlogTheme',NULL);
INSERT INTO sBlog_Settings VALUES('BlogPostsPerPage','5');
INSERT INTO sBlog_Settings VALUES('ManageItemsPerPage', '5');

INSERT INTO sBlog_Settings VALUES('BlogSocialSharing','false');
INSERT INTO sBlog_Settings VALUES('BlogSocialSharingChoice', NULL);

INSERT INTO sBlog_Settings VALUES('BlogSyntaxHighlighting','false');
INSERT INTO sBlog_Settings VALUES('BlogSyntaxTheme',NULL);
INSERT INTO sBlog_Settings VALUES('BlogSyntaxScripts',NULL);

INSERT INTO sBlog_Settings VALUES('BlogSiteErrorEmailAction','false');

INSERT INTO sBlog_Settings VALUES('BlogAkismetEnabled','false');
INSERT INTO sBlog_Settings VALUES('BlogAkismetKey',NULL);
INSERT INTO sBlog_Settings VALUES('BlogAkismetUrl',NULL);
INSERT INTO sBlog_Settings VALUES('BlogAkismetDeleteSpam','false');

INSERT INTO sBlog_Settings VALUES('BlogAdminEmailAddress',NULL);
INSERT INTO sBlog_Settings VALUES('BlogSmtpAddress',NULL);
INSERT INTO sBlog_Settings VALUES('BlogSmtpPassword', NULL);

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create Users Table */
PRINT '******************************************************************';
PRINT '*** Creating the users table                                   ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

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

GO

SET ANSI_PADDING OFF
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Insert the default user with the default password */
PRINT '******************************************************************';
PRINT '*** Adding the default admin user                            ***';
GO

INSERT INTO Users ([UserName],[Password],[UserEmailAddress],[UserDisplayName],[UserActiveStatus],
				   [ActivationKey],[OneTimeToken],[UserCode],[UserSite],[LastLoginDate])
VALUES ('admin','','','admin',1,NULL,NULL,'',NULL,NULL);

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create Post Table */
PRINT '******************************************************************';
PRINT '*** Creating the posts table                                   ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

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

GO

SET ANSI_PADDING OFF
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Insert the default post */
PRINT '******************************************************************';
PRINT '*** Adding a default post                                      ***';
GO

INSERT INTO [dbo].[Posts]
           ([PostTitle],[PostContent],[PostUrl],[PostAddedDate],[PostEditedDate],[OwnerUserID],
           [UserCanAddComments],[CanBeShared],[IsPrivate],[EntryType],[Order])
     VALUES
           ('Hello World!',
           'Hello World!<br /><br />Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc non sollicitudin dui. Nunc ac augue tellus, sit amet rutrum nunc. Integer malesuada sapien tincidunt ligula vulputate blandit eu eget tellus. Praesent rhoncus neque eget augue blandit viverra. Praesent mattis gravida egestas. Integer dictum, sapien sit amet pharetra tempus, elit elit porta sem, sed fermentum tortor diam quis nulla. Sed felis sem, ultrices quis sagittis vitae, convallis at dui. Curabitur rutrum, nulla vitae semper interdum, justo velit blandit augue, ac porta lorem lorem a est. Curabitur quis metus in magna scelerisque viverra. Proin id leo eros, ullamcorper pellentesque mauris. Donec metus leo, varius at faucibus id, interdum a ipsum. Donec adipiscing tortor ac nulla convallis scelerisque. Ut posuere aliquam dolor eu viverra. Maecenas ut arcu eu lacus iaculis euismod dictum pulvinar turpis. Nulla vel sem eget lacus tristique lacinia eu id diam.<br /><br />',
           'hello-world',GETDATE(),GETDATE(),1,'true','true','false',1,NULL)
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Insert the default page */
PRINT '******************************************************************';
PRINT '*** Adding a default page                                      ***';
GO

INSERT INTO [dbo].[Posts]
           ([PostTitle],[PostContent],[PostUrl],[PostAddedDate],[PostEditedDate],[OwnerUserID],
           [UserCanAddComments],[CanBeShared],[IsPrivate],[EntryType],[Order])
     VALUES
           ('About',
           'This is just a basic &quot;About&quot; page!<br /><br />Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc non sollicitudin dui. Nunc ac augue tellus, sit amet rutrum nunc. Integer malesuada sapien tincidunt ligula vulputate blandit eu eget tellus. Praesent rhoncus neque eget augue blandit viverra. Praesent mattis gravida egestas. Integer dictum, sapien sit amet pharetra tempus, elit elit porta sem, sed fermentum tortor diam quis nulla. Sed felis sem, ultrices quis sagittis vitae, convallis at dui. Curabitur rutrum, nulla vitae semper interdum, justo velit blandit augue, ac porta lorem lorem a est. Curabitur quis metus in magna scelerisque viverra. Proin id leo eros, ullamcorper pellentesque mauris. Donec metus leo, varius at faucibus id, interdum a ipsum. Donec adipiscing tortor ac nulla convallis scelerisque. Ut posuere aliquam dolor eu viverra. Maecenas ut arcu eu lacus iaculis euismod dictum pulvinar turpis. Nulla vel sem eget lacus tristique lacinia eu id diam.<br /><br />',
           'about',GETDATE(),GETDATE(),1,'true','true','false',2,1)
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create Comment Table */
PRINT '******************************************************************';
PRINT '*** Creating the comments table                                ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

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

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Insert a default comment for the defaul page & post */
PRINT '******************************************************************';
PRINT '*** Adding a default comment for the default post              ***';
GO

INSERT INTO [dbo].[Comments]
           ([CommentUserFullName],[CommenterEmail],[CommenterSite]
           ,[CommentContent],[CommentPostedDate],[CommentStatus],[PostID],[UserID])
     VALUES('admin','','','Welcome to the blogosphere!',
           GETDATE(),0,1,NULL)
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

PRINT '******************************************************************';
PRINT '*** Adding a default comment for the default page              ***';
GO

INSERT INTO [dbo].[Comments]
           ([CommentUserFullName],[CommenterEmail],[CommenterSite]
           ,[CommentContent],[CommentPostedDate],[CommentStatus],[PostID],[UserID])
     VALUES('admin','','','About Me!',
           GETDATE(),0,2,NULL)
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create Categories Table */
PRINT '******************************************************************';
PRINT '*** Creating the categories table                              ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
	[CategorySlug] [varchar](MAX) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Insert default category */
PRINT '******************************************************************';
PRINT '*** Adding a default category                                  ***';
GO

INSERT INTO Categories (CategoryName, CategorySlug) VALUES ('General','general');

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create Tags Table */
PRINT '******************************************************************';
PRINT '*** Creating the tags table                                    ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Tags](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[TagName] [varchar](50) NOT NULL,
	[TagSlug] [varchar](MAX) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Insert a default tag */
PRINT '******************************************************************';
PRINT '*** Adding a default tag                                       ***';
GO

INSERT INTO [dbo].[Tags] (TagName, TagSlug) VALUES('general','general');

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create CategoryMapping Table */
PRINT '******************************************************************';
PRINT '*** Creating the category mapping table                        ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CategoryMapping](
	[PostCategoryMappingID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[PostID] [int] NOT NULL,
 CONSTRAINT [PK_PostCategoryMapping] PRIMARY KEY CLUSTERED 
(
	[PostCategoryMappingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CategoryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostCategoryMapping_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO

ALTER TABLE [dbo].[CategoryMapping] CHECK CONSTRAINT [FK_PostCategoryMapping_Categories]
GO

ALTER TABLE [dbo].[CategoryMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostCategoryMapping_Posts] FOREIGN KEY([PostID])
REFERENCES [dbo].[Posts] ([PostID])
GO

ALTER TABLE [dbo].[CategoryMapping] CHECK CONSTRAINT [FK_PostCategoryMapping_Posts]
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Add a default mapping */
PRINT '******************************************************************';
PRINT '*** Adding the default category to the default post            ***';
GO

INSERT INTO [dbo].[CategoryMapping]([CategoryID],[PostID]) VALUES (1,1)
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create TagMapping Table */
PRINT '******************************************************************';
PRINT '*** Creating the tag mapping table                             ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TagMapping](
	[PostTagMappingID] [int] IDENTITY(1,1) NOT NULL,
	[TagID] [int] NOT NULL,
	[PostID] [int] NOT NULL,
 CONSTRAINT [PK_PostTagMapping] PRIMARY KEY CLUSTERED 
(
	[PostTagMappingID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TagMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostTagMapping_Posts] FOREIGN KEY([PostID])
REFERENCES [dbo].[Posts] ([PostID])
GO

ALTER TABLE [dbo].[TagMapping] CHECK CONSTRAINT [FK_PostTagMapping_Posts]
GO

ALTER TABLE [dbo].[TagMapping]  WITH CHECK ADD  CONSTRAINT [FK_PostTagMapping_Tags] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tags] ([TagID])
GO

ALTER TABLE [dbo].[TagMapping] CHECK CONSTRAINT [FK_PostTagMapping_Tags]
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Add default tag mapping */
PRINT '******************************************************************';
PRINT '*** Adding the default tag to the default post                 ***';
GO

INSERT INTO [dbo].[TagMapping] VALUES(1,1);

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

/* Create the Errors table */
PRINT '******************************************************************';
PRINT '*** Creating the blog''s global errors table                   ***';
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

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

GO

SET ANSI_PADDING OFF
GO

PRINT '***                           Done                             ***';
PRINT '------------------------------------------------------------------';
PRINT '';
GO

PRINT 'Completing sBlog.Net database installation...';
PRINT '================== Done ================';
PRINT '';
GO

SET NOCOUNT OFF
GO
