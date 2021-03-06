/****** Object:  Table [Security].[AspNetGroupRoles]    Script Date: 2/21/2018 6:51:09 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [Security].[AspNetGroupRoles](
	[RoleId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_Security.AspNetGroupRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


INSERT [Security].[AspNetGroupRoles] ([RoleId], [GroupId]) VALUES (5, 1)
INSERT [Security].[AspNetGroupRoles] ([RoleId], [GroupId]) VALUES (6, 1)
INSERT [Security].[AspNetGroupRoles] ([RoleId], [GroupId]) VALUES (7, 1)
INSERT [Security].[AspNetGroupRoles] ([RoleId], [GroupId]) VALUES (7, 2)
INSERT [Security].[AspNetGroupRoles] ([RoleId], [GroupId]) VALUES (6, 2)
INSERT [Security].[AspNetGroupRoles] ([RoleId], [GroupId]) VALUES (15, 3)
/****** Object:  Index [IX_GroupId]    Script Date: 2/21/2018 6:51:14 PM ******/
CREATE NONCLUSTERED INDEX [IX_GroupId] ON [Security].[AspNetGroupRoles]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

/****** Object:  Index [IX_RoleId]    Script Date: 2/21/2018 6:51:14 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [Security].[AspNetGroupRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE [Security].[AspNetGroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_Security.AspNetGroupRoles_Security.AspNetGroups_GroupId] FOREIGN KEY([GroupId])
REFERENCES [Security].[AspNetGroups] ([Id])
ON DELETE CASCADE

ALTER TABLE [Security].[AspNetGroupRoles] CHECK CONSTRAINT [FK_Security.AspNetGroupRoles_Security.AspNetGroups_GroupId]

ALTER TABLE [Security].[AspNetGroupRoles]  WITH CHECK ADD  CONSTRAINT [FK_Security.AspNetGroupRoles_Security.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [Security].[AspNetRoles] ([Id])
ON DELETE CASCADE

ALTER TABLE [Security].[AspNetGroupRoles] CHECK CONSTRAINT [FK_Security.AspNetGroupRoles_Security.AspNetRoles_RoleId]

