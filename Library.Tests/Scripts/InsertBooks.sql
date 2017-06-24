SET IDENTITY_INSERT [dbo].[Book] ON
INSERT INTO [dbo].[Book] ([Id], [Name] ,[IsAvailable] ,[CurrentReaderId]) VALUES (99991,'test_book1',1 ,null)
INSERT INTO [dbo].[Book] ([Id], [Name] ,[IsAvailable] ,[CurrentReaderId]) VALUES (99992,'test_book2',1 ,null)
INSERT INTO [dbo].[Book] ([Id], [Name] ,[IsAvailable] ,[CurrentReaderId]) VALUES (99993,'test_book3',0 ,2)
INSERT INTO [dbo].[Book] ([Id], [Name] ,[IsAvailable] ,[CurrentReaderId]) VALUES (99994,'test_book4',0 ,2)
INSERT INTO [dbo].[Book] ([Id], [Name] ,[IsAvailable] ,[CurrentReaderId]) VALUES (99995,'test_book5',1 ,null)
SET IDENTITY_INSERT [dbo].[Book] OFF