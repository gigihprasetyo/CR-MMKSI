USE [BSIDNET_MMKSI_DMS]
GO
SET IDENTITY_INSERT [dbo].[AspNetRoles] ON 

GO
INSERT [dbo].[AspNetRoles] ([Id], [Level], [Name]) VALUES (1, N'Super Administrator', N'Administrator')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Level], [Name]) VALUES (8, N'WebUser Application Access', N'WebUser')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Level], [Name]) VALUES (11, N'WebAPI Access', N'WebAPI')
GO
SET IDENTITY_INSERT [dbo].[AspNetRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] ON 

GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (1, N'Zainal', N'Arifin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'zainal.arifin@mitrais.com', 1, N'AN0h+bhnCDoNXdVMh3GciKYdVLBneFFpz4YWy6rrcbZQA6EMWccJz4bsHPiUXuFWrA==', N'c4270f8c-5249-4eae-8a10-e2d2947440cd', NULL, 0, 0, NULL, 0, 0, N'zainal.arifin@mitrais.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ6YWluYWwuYXJpZmluQG1pdHJhaXMuY29tIiwicm9sZSI6InVzZXIiLCJkZWFsZXJjb2RlIjoiQkFTIiwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3QvTU1LU0lBdXRoU2VydmVyIiwiYXVkIjoiYTc3NmU5YWI3NjQ5NDVjNThiNjM4ZGM4ZDA5OWU4N2MiLCJleHAiOjE1MjQxMjI5OTAsIm5iZiI6MTUyMjkxMzQwMH0.iAUqO0LgnPn-jR5vziO1ChzRe04lWjXNSEZbI7zMxKk', CAST(0x0000A8C7007B8DB2 AS DateTime), NULL, 1, 0, CAST(0x0000A8BE0090C1B3 AS DateTime), 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (2, N'prins', N'santoso', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, N'prins.santoso@mitrais.com', 1, N'ANFqR1ySNsu/sXcCYIxquPJzBZfebKBtJqoSSfdG7rpXvKB5fnk5/V3kpkV2SR4Vzw==', N'c4270f8c-5249-4eae-8a10-e2d2947440cd', NULL, 0, 0, NULL, 0, 0, N'prins.santoso@mitrais.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJwcmlucy5zYW50b3NvQG1pdHJhaXMuY29tIiwicm9sZSI6InVzZXIiLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0L0F1dGhTZXJ2ZXIiLCJhdWQiOiI0MWJkMzhjNDc5ZmI0ZGQ0OGFjZmU0MTI5ODYwZjg5YiIsImV4cCI6MTUyMjA2MTA1NywibmJmIjoxNTIwOTM4OTIxfQ.aLcU7COqEBrb_muMkNw17rFs2Qf2wDpucDMvxFD0VWw', CAST(0x0000A8AF00B0F639 AS DateTime), NULL, 2, 0, CAST(0x0000A8A200B5D503 AS DateTime), 2)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (3, N'komang', N'wiwin', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, N'ikomangwiwinwibawa@mitrais.com', 1, N'AKi1IXm2lbSe80M0x/aRnW/4SI4y7hkXmqXfVpVnYf0kJpo7gYlGCEAclpyZOChb4g==', N'7a85fefb-090b-4ddb-a894-60c5ac53ea49', NULL, 0, 0, NULL, 0, 0, N'ikomangwiwinwibawa@mitrais.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJpa29tYW5nd2l3aW53aWJhd2FAbWl0cmFpcy5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAxMDkiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdC9NTUtTSUF1dGhTZXJ2ZXIiLCJhdWQiOiI3ZWE1YTE3ZWYxOWY0NDFhODZkYjFmODI1ODVjNWRkOSIsImV4cCI6MTUyNDU1MzIyOCwibmJmIjoxNTIzMzQzODQ1fQ.-FJD6BdxJhMg774egOG0rLhi41n82z038twWW_kjDWM', CAST(0x0000A8CC00737CE6 AS DateTime), NULL, 7, 1, CAST(0x0000A8CE00E45104 AS DateTime), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (43, N'user1', N'technosoft', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, N'user1@technosoft.com', 1, N'AKtpHLo4/Ldt7U63Hr9LWWeC9AouJ/EsOx2xjnZfa+nmB8dFarM+E5gbtmxh7Iz8dQ==', N'3f655232-f337-4457-8e55-7b464ffaab71', NULL, 0, 0, NULL, 0, 0, N'user1@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1c2VyMUB0ZWNobm9zb2Z0LmNvbSIsInJvbGUiOiJ1c2VyIiwiZGVhbGVyY29kZSI6IkJBUyIsImlzcyI6Imh0dHBzOi8vcWEtaW50ZXJmYWNlLm1pdHN1YmlzaGktbW90b3JzLmNvLmlkL0F1dGhTZXJ2ZXIvIiwiYXVkIjoiYmE0YzUwMTI5ZDBhNDdlNWI5NDkxNDVjOGUyM2RkYTEiLCJleHAiOjE1MjQ0OTc3ODQsIm5iZiI6MTUyMzM0MjYyN30.6p7vx_cz98AAHc3qOQ8Dbm3ZKc8kWy59PVu0oEVzs-Q', CAST(0x0000A8CB010130A7 AS DateTime), NULL, 4, 1, CAST(0x0000A8BE006EE7E7 AS DateTime), 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (44, N'user2', N'technosoft', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, N'user2@technosoft.com', 1, N'AP9ubl9cSkHuO/iKlu4EZpRcIxjx9EDHm9ibUFVFzC3k4ww83qY+C20ql1lpjrkMcQ==', N'386c394d-900f-4a02-a6b2-b285bcbc6da1', NULL, 0, 0, NULL, 0, 0, N'user2@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1c2VyMkB0ZWNobm9zb2Z0LmNvbSIsInJvbGUiOiJ1c2VyIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdC9BdXRoU2VydmVyIiwiYXVkIjoiYTc3NmU5YWI3NjQ5NDVjNThiNjM4ZGM4ZDA5OWU4N2MiLCJleHAiOjE1MjIwNjEwNTcsIm5iZiI6MTUyMDkzOTE2MX0.RL-lCvewbU3FU0PA_Eyamc6Re0Us3ufBQ9cpnbSodGQ', CAST(0x0000A8AF00B0F639 AS DateTime), NULL, 1, 1, CAST(0x0000A8A200B6ED6C AS DateTime), 2)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (45, N'user3', N'technosoft', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, N'user3@technosoft.com', 1, N'AGvg77Y0WLN/vgaHkUSOlapvyWITxit0D511pssXU1LgFEyHXkX5o8mYLjvpOjT9zA==', N'cf41ecb8-b516-4622-9a4e-8372dc1d777f', NULL, 0, 0, NULL, 0, 0, N'user3@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1c2VyM0B0ZWNobm9zb2Z0LmNvbSIsInJvbGUiOiJ1c2VyIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdC9LVEIuRE5ldC5JbnRlcmZhY2UuV2ViTVZDIiwiYXVkIjoiYTc3NmU5YWI3NjQ5NDVjNThiNjM4ZGM4ZDA5OWU4N2MiLCJleHAiOjE1MjExNzAyMjcsIm5iZiI6MTUxOTk2MDYyN30.FxJn_RmWYH7LcmK-uZQH0EsJzWrIOjjiJm5HTQO87Ks', CAST(0x0000A8A500A97FC0 AS DateTime), NULL, 1, 1, CAST(0x0000A89B00AB6EB8 AS DateTime), 3)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (46, N'user4', N'technosoft', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, N'user4@technosoft.com', 1, N'AH7WTLLmOBtjhU3589ZPpaC7f/iurDrj7jwVtqn2M2pr3rQw1AnUcQNZstiQfCkqCg==', N'614f5ab5-ceca-4f17-8d41-963587da6949', NULL, 0, 0, NULL, 0, 0, N'user4@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1c2VyNEB0ZWNobm9zb2Z0LmNvbSIsInJvbGUiOiJ1c2VyIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdC9LVEIuRE5ldC5JbnRlcmZhY2UuV2ViTVZDIiwiYXVkIjoiYTc3NmU5YWI3NjQ5NDVjNThiNjM4ZGM4ZDA5OWU4N2MiLCJleHAiOjE1MjEwODgxOTQsIm5iZiI6MTUxOTg3ODU5NH0.ylhl15elXCpYB_zFJv-d1eaPRuVm0A7e9PvKYhC5xL4', CAST(0x0000A8A400BD7D5A AS DateTime), NULL, 1, 1, CAST(0x0000A89B00AB6EB8 AS DateTime), 4)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (47, N'arif', N'ismiadi', N'Cempaka Raya', NULL, NULL, N'Jakarta Pusat', N'DKI Jakarta', N'10001', N'Indonesia', N'Mitrais', 1, N'arif.ismiadi@mitrais.com', 0, N'ANGsCzegGHOCpLjbgdVlULHte/sgfr0SX+GYuoWmvUQymwZfoF4Q/WUSx9xy++PUGQ==', N'6f24460b-2b30-444b-8e71-990deb1216f2', N'0812345678', 0, 0, NULL, 0, 0, N'arif.ismiadi@mitrais.com', NULL, NULL, NULL, 4, 0, CAST(0x0000A89B00AB6EB8 AS DateTime), 5)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (63, N'1000109', NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Indonesia', N'Yogyakarta', 1, N'100109@technosoft.com', 0, N'AC2Pe566Cn41zf5qhaDCCBVRRvYJTX1sUxbRbkz7NqLDSFJ5aJLgn11rzA3+jCORUw==', N'a3fe3894-e325-46cf-9efa-b0ca27573bf1', NULL, 0, 0, NULL, 0, 0, N'100109@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAxMDlAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAxMDkiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTI1NzUwMTU2LCJuYmYiOjE1MjQ2MjcxNzl9.QgvN7xht0rFO2ESI2ZqxRzZeu4ysAQK6-tE0dE05PUU', CAST(0x0000A8DA00397A7B AS DateTime), NULL, 4, 1, CAST(0x0000A8CF00A7CE52 AS DateTime), 111)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (64, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100110@technosoft.com', 0, N'AB738C+tV68RkzhhDZMqjEDbCouWUUOy5BHCfo2M230EGfbPTdy7LLgz1xmsfvHPYQ==', N'b2393b51-813d-4924-b012-b3265f475523', NULL, 0, 0, NULL, 0, 0, N'100110@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAxMTBAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAxMTAiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdC9NTUtTSUF1dGhTZXJ2ZXIiLCJhdWQiOiJiYTRjNTAxMjlkMGE0N2U1Yjk0OTE0NWM4ZTIzZGRhMSIsImV4cCI6MTUyNTgzNDA0MiwibmJmIjoxNTI0NjI0NDUzfQ.MCJUcykxwAZVp2z7A4Avwro3L6_h4yAxX23gYMTgj7k', CAST(0x0000A8DB002DF908 AS DateTime), NULL, 4, 1, CAST(0x0000A8CD002E052A AS DateTime), 112)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (65, N'10111', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100111@technosoft.com', 0, N'AAVl3T0cyFNRetR3bTwzRw5zLJzZYdCXTuRqoSBAzNvrIBKMGjgbGwwKP+oNvFVTSg==', N'da7ba814-2e6e-490e-a94e-c6c6bbf1fb56', NULL, 0, 0, NULL, 0, 0, N'100111@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAxMTFAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAxMTEiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTI2MjY0NzI0LCJuYmYiOjE1MjUwNjAxNjB9.R-j91RmehUS-zCL_rcEGhqCtU_b2SemYB_AOX2b35ho', CAST(0x0000A8E00027F01B AS DateTime), NULL, 4, 1, CAST(0x0000A8D2003EFDBD AS DateTime), 113)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (66, N'100661', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100661@technosoft.com', 0, N'AOQvqtyMI4Ws0ILerX2Oyn6twTkGs2Fbxc/7kaSOX1v5KMohhWgaCVLrKF2LgfrJ+w==', N'f26c9a51-3ff7-4a65-9b76-a413fa8c6f1d', NULL, 0, 0, NULL, 0, 0, N'100661@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDA2NjFAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDA2NjEiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTIyMjcyMDA1LCJuYmYiOjE1MjEwOTEwOTR9.ijePBYJUEysLdfHN6m5OeVDD_rhGbpOtf9xyH6AP-OQ', CAST(0x0000A8B1015F968F AS DateTime), NULL, 4, 1, CAST(0x0000A8A400576817 AS DateTime), 479)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (67, N'170003', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'170003@technosoft.com', 0, N'ABQmp2e2ksAEXizmAAl4Niv2HaQWMo1a1xjhlvlHMPSDZbGDHuj2C5HmPrbzY8++LQ==', N'3e422661-1c73-4ccc-af50-28d5abd90140', NULL, 0, 0, NULL, 0, 0, N'170003@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxNzAwMDNAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxNzAwMDMiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTIyMjcyMDA1LCJuYmYiOjE1MjEwOTExNDl9._Bd6KMADrxl2uVS5PgWTWAnw7FMZLksl_yIiaDuGcT0', CAST(0x0000A8B1015F968F AS DateTime), NULL, 4, 1, CAST(0x0000A8A40057A86A AS DateTime), 529)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (68, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100170@technosoft.com', 0, N'AIpb03jGWxDpq7Hk5GF/3eex6yRG4FqoUz3kC8THAwTlbxk0A3MbhIVtFH9V6HHBqA==', N'29e25209-a85a-49a6-8ef2-448de8b85e17', NULL, 0, 0, NULL, 0, 0, N'100170@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAxNzBAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAxNzAiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTI1MTM4ODM2LCJuYmYiOjE1MjM5MzU5Njh9.cYgXO9KWY7sfgcFQkYWZE-V4bqXlyzLH0ZIkXdWGgEs', CAST(0x0000A8D3001BA28D AS DateTime), NULL, 4, 1, CAST(0x0000A8CD00EFCE5F AS DateTime), 172)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (69, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100568@technosoft.com', 0, N'AIRZMEZTBtz5iEZpW/h0VrP+MEn8aA0rJf9JioKcR7XNFiXPdVC6dGjVsULFxtJ0Mw==', N'4b474abe-5e1c-4f65-9a67-80e5d1840507', NULL, 0, 0, NULL, 0, 0, N'100568@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDA1NjhAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDA1NjgiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdC9NTUtTSUF1dGhTZXJ2ZXIiLCJhdWQiOiJiYTRjNTAxMjlkMGE0N2U1Yjk0OTE0NWM4ZTIzZGRhMSIsImV4cCI6MTUyMzg0MzIxMCwibmJmIjoxNTIyNjMzNjE5fQ.Q1PF3JX7xbIeC4L-CbSP_5fUgJcxcNEkbnWKnFIa0QU', CAST(0x0000A8C4001D57C4 AS DateTime), NULL, 4, 1, CAST(0x0000A8B6001D6331 AS DateTime), 198)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (70, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100649@technosoft.com', 0, N'AF7wixOmirnm0iGPxa3qnGvOwlys59ByueNMhPZN9CEK2uv2o2DEOqxW5Ajje5AWvQ==', N'b0388136-d9ed-40ae-9e52-ff7cefa0b282', NULL, 0, 0, NULL, 0, 0, N'100649@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDA2NDlAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDA2NDkiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTIyMjcyMDA1LCJuYmYiOjE1MjEwOTEyMTN9.AtobvcQrgSXthAIMnZuLre_ymf914UymPsxHvJy7A4I', CAST(0x0000A8B1015F968F AS DateTime), NULL, 4, 1, CAST(0x0000A8A40057F3CC AS DateTime), 467)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (71, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100708@technosoft.com', 0, N'AM/CdA4zQ7hTJ9hUwfsUCVUXNrpxMAHIMTq9p6DEeFiDwkz+9vIUyNa9LPgCSQmTrA==', N'aa92196a-d6d9-46a9-a287-277316858d44', NULL, 0, 0, NULL, 0, 0, N'100708@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDA3MDhAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDA3MDgiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTI2MjY0NzI0LCJuYmYiOjE1MjUwNTk4NzZ9.xwq1uZ2zabHircA6Q9c3VBP2LwXy_RkWsYshpZhFYpI', CAST(0x0000A8E00027F01B AS DateTime), NULL, 4, 1, CAST(0x0000A8D2003DB09E AS DateTime), 533)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (72, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100131@technosoft.com', 0, N'AIivy4F31YQKqyiF8zKU9UkPgVOrNdDUDwpmQLKtI5GAyedF5GIN+Y30lef5NKU5og==', N'63325878-b79f-48ba-bc6f-56365b97619c', NULL, 0, 0, NULL, 0, 0, N'100131@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAxMzFAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAxMzEiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdC9NTUtTSUF1dGhTZXJ2ZXIiLCJhdWQiOiJiYTRjNTAxMjlkMGE0N2U1Yjk0OTE0NWM4ZTIzZGRhMSIsImV4cCI6MTUyMzUwNDI2MSwibmJmIjoxNTIyMjk0ODQ0fQ.8r0D7e439qmal9uOswOPwRWPfv3ATD7nqSSFJE0fF1I', CAST(0x0000A8C0003BCAD8 AS DateTime), NULL, 4, 1, CAST(0x0000A8B2003CA0D0 AS DateTime), 133)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (73, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'Indonesia', NULL, 1, N'100025@technosoft.com', 0, N'ALg2OkQsJVXdr9gyYVEWzG3tC3ooBboLg3TugwFPum4AqZpFQBgKl4aBAYtYXECWPA==', N'0e1be25c-80ca-49f2-a84a-9ebe35925da8', NULL, 0, 0, NULL, 0, 0, N'100025@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAwMjVAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAwMjUiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdC9NTUtTSUF1dGhTZXJ2ZXIiLCJhdWQiOiJiYTRjNTAxMjlkMGE0N2U1Yjk0OTE0NWM4ZTIzZGRhMSIsImV4cCI6MTUyMzUwNDI2MSwibmJmIjoxNTIyMjk0ODc3fQ.Rr4W7oAzfbM0lyZWriPRGMHl-HotjZuWhnC9tHD9jAs', CAST(0x0000A8C0003BCAD8 AS DateTime), NULL, 6, 1, CAST(0x0000A8B2003CC7D6 AS DateTime), 27)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (74, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100002@technosoft.com', 0, N'ANn9Q9Ulox+wDRANoR6oJ+mHz9bHBySlm/5h/3xzCeRbieyEN7na7kdpVALkT23Rpw==', N'fc6452cc-3f72-456d-b00f-3f3d0c95c6d2', NULL, 0, 0, NULL, 0, 0, N'100002@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAwMDJAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAwMDIiLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdC9NTUtTSUF1dGhTZXJ2ZXIiLCJhdWQiOiJiYTRjNTAxMjlkMGE0N2U1Yjk0OTE0NWM4ZTIzZGRhMSIsImV4cCI6MTUyMzUwNDI2MSwibmJmIjoxNTIyMjk0OTA2fQ.nZ1oOYg2X1M7uIjEd-cDpsvTSz6Zu2cZt76bR9m3shY', CAST(0x0000A8C0003BCAD8 AS DateTime), NULL, 4, 1, CAST(0x0000A8B2003CE974 AS DateTime), 4)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (75, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100001@technosoft.com', 0, N'AD+dJweGQdFI3rAvBB3C1WfL2f8tloD5mdQZJ+Wlat9TySeayzZsConVcM9dRC9igg==', N'301ee8f4-38c5-46d7-8d17-16b6914a3b30', NULL, 0, 0, NULL, 0, 0, N'100001@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAwMDFAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAwMDEiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTI2MDA0OTM2LCJuYmYiOjE1MjQ3OTg4NDZ9.yApdnnbJZRSoSUgkvex_WN8mRTMbE-n8taKHSKXxsq0', CAST(0x0000A8DD00253F0D AS DateTime), NULL, 4, 1, CAST(0x0000A8CF011963EC AS DateTime), 3)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (76, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'100007@technosoft.com', 0, N'APFuv9Unh180rFQmWiPK9FZzv5jQ0zN4fEoepAzXp4EktVbE0KchDvBoKbazWlkixg==', N'774cc108-3c63-4eb4-bd5b-e82055873e5f', NULL, 0, 0, NULL, 0, 0, N'100007@technosoft.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMDAwMDdAdGVjaG5vc29mdC5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiIxMDAwMDciLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImJhNGM1MDEyOWQwYTQ3ZTViOTQ5MTQ1YzhlMjNkZGExIiwiZXhwIjoxNTI0NDk3Nzg0LCJuYmYiOjE1MjMzMzU3OTF9.Z_a5We75pWCXO8klCOXiFjquZxDf8Sl8vG5RMr_BL3k', CAST(0x0000A8CB010130A7 AS DateTime), NULL, 4, 1, CAST(0x0000A8BE004F9C7F AS DateTime), 9)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (78, N'Anna', N'Nurhayanto', NULL, NULL, NULL, N'Jakarta', N'Jakarta', NULL, N'Indonesia', N'MMKSI', 1, N'anna@bsi.co.id', 0, N'ABxdUcnxJhN/0rwxy3JJgmGhDwyRvS8sLK76+bfID1nA6nZDvEZl6szeWrQS/PftOA==', N'573c6d8d-0a95-479c-8b63-9003f21b6dc6', NULL, 0, 0, NULL, 0, 0, N'anna@bsi.co.id', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJuYW5hIiwicm9sZSI6InVzZXIiLCJkZWFsZXJjb2RlIjoiQkFTIiwiaXNzIjoiaHR0cHM6Ly9xYS1pbnRlcmZhY2UubWl0c3ViaXNoaS1tb3RvcnMuY28uaWQvQXV0aFNlcnZlci8iLCJhdWQiOiJhNzc2ZTlhYjc2NDk0NWM1OGI2MzhkYzhkMDk5ZTg3YyIsImV4cCI6MTUyNDQ5Nzc4NCwibmJmIjoxNTIzMzQ2Mzg1fQ.Wu6cteSIApNzJfj3RJK-hZsXbrs3rITXUesFsAh0EwI', CAST(0x0000A8CB010130A7 AS DateTime), NULL, 1, 1, CAST(0x0000A8D2009C00EC AS DateTime), NULL)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (80, N'Irfan', N'Suteja', N'Jalan Pangkalan Asem', NULL, NULL, N'Yogyakarta', N'Jakarta', N'50011', N'Indonesia', N'BSI', 1, N'irfan_s@mitrais.com', 0, N'AFVq3JNauJJFFN7lZSmcpeOeXrgp4HJo26lIpccwZFM0IBAKejSf10sNY0Wytb6Pqg==', N'0390968b-d3b5-40e9-9b8f-9f35aa4c6706', N'085642327346', 0, 0, NULL, 0, 0, N'irfan_s@mitrais.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJpcmZhbl9zQG1pdHJhaXMuY29tIiwicm9sZSI6InVzZXIiLCJkZWFsZXJjb2RlIjoiQkFTIiwiaXNzIjoiaHR0cHM6Ly9xYS1pbnRlcmZhY2UubWl0c3ViaXNoaS1tb3RvcnMuY28uaWQvQXV0aFNlcnZlci8iLCJhdWQiOiJhNzc2ZTlhYjc2NDk0NWM1OGI2MzhkYzhkMDk5ZTg3YyIsImV4cCI6MTUyNjI2NDcyNCwibmJmIjoxNTI1MDU1MjUzfQ.KWrYZ_tzH0TJ5Aewo7JzNCpDjoIQgHztIYg_coeGFy8', CAST(0x0000A8E00027F01B AS DateTime), NULL, 1, 1, CAST(0x0000A8D2002886AD AS DateTime), 1)
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Street1], [Street2], [Street3], [City], [State], [PostalCode], [Country], [Company], [Status], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Token], [TokenExpired], [IsLogin], [ClientApplicationId], [IsActive], [LastLogin], [DealerId]) VALUES (81, N'Helmy', N'Satria', N'Jl Manukwari 99', NULL, NULL, N'Denpasar', NULL, NULL, N'Kongo', N'Mitrais', 1, N'helmy.satria@mitrais.com', 0, N'AJPz9W/ly/EFwpu55Vm+RdYxAbc45AXWqz4eF0h1xGSzQ5OiJ6sQLbNAXncwwx6CNw==', N'1d1d7fa0-fe6d-4b9f-952e-e015e519d7fc', N'089898989898', 0, 0, NULL, 0, 0, N'helmys@mitrais.com', N'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJoZWxteXNAbWl0cmFpcy5jb20iLCJyb2xlIjoidXNlciIsImRlYWxlcmNvZGUiOiJCQVMiLCJpc3MiOiJodHRwczovL3FhLWludGVyZmFjZS5taXRzdWJpc2hpLW1vdG9ycy5jby5pZC9BdXRoU2VydmVyLyIsImF1ZCI6ImE3NzZlOWFiNzY0OTQ1YzU4YjYzOGRjOGQwOTllODdjIiwiZXhwIjoxNTI1MzMwOTkzLCJuYmYiOjE1MjQyMTM3MTl9.sqBkP8eOn_8TlSNvfTZ0OXw_DQeot7Q4_b5lhq5-zCI', CAST(0x0000A8D500743DB2 AS DateTime), NULL, 1, 1, CAST(0x0000A8D200A4C1FB AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (2, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (3, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (43, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (44, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (45, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (46, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (47, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (78, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (81, 1)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (63, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (64, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (65, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (66, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (67, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (68, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (69, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (70, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (71, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (72, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (73, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (74, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (75, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (76, 11)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (80, 11)
GO
SET IDENTITY_INSERT [dbo].[Permission] ON 

GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (237, N'GetArea1BasedOnFilter', N'Read Area1 Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (238, N'GetArea2BasedOnFilter', N'Read Area2 Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (239, N'GetBusinessSectorDetailBasedOnFilter', N'Read BusinessSectorDetail Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (240, N'GetBusinessSectorHeaderBasedOnFilter', N'Read BusinessSectorHeader Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (241, N'GetCampaignBasedOnFilter', N'Read Campaign Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (242, N'GetCategoryBasedOnFilter', N'Read Category Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (243, N'GetChassisMasterBasedOnFilter', N'Read ChassisMaster Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (244, N'GetCityBasedOnFilter', N'Read City Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (245, N'GetDealerBasedOnFilter', N'Read Dealer Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (246, N'GetDealerBranchBasedOnFilter', N'Read DealerBranch Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (247, N'GetDealerGroupBasedOnFilter', N'Read DealerGroup Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (248, N'GetEndCustomerBasedOnFilter', N'Read EndCustomer Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (249, N'GetFleetBasedOnFilter', N'Read Fleet Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (250, N'GetFSKindBasedOnFilter', N'Read FSKind Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (251, N'GetLaborMasterBasedOnFilter', N'Read LaborMaster Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (252, N'GetPaymentMethodBasedOnFilter', N'Read PaymentMethod Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (253, N'GetPaymentPurposeBasedOnFilter', N'Read PaymentPurpose Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (254, N'GetPositionWSCBasedOnFilter', N'Read PositionWSC Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (255, N'GetPriceBasedOnFilter', N'Read Price Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (256, N'GetProvinceBasedOnFilter', N'Read Province Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (257, N'GetSparePartBasedOnFilter', N'Read SparePart Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (258, N'GetTermOfPaymentBasedOnFilter', N'Read TermOfPayment Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (259, N'GetVehicleClassBasedOnFilter', N'Read VehicleClass Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (260, N'GetVehicleColorBasedOnFilter', N'Read VehicleColor Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (261, N'GetVehicleKindBasedOnFilter', N'Read VehicleKind Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (262, N'GetVehicleKindGroupBasedOnFilter', N'Read VehicleKindGroup Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (263, N'GetVehicleModelBasedOnFilter', N'Read VehicleModel Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (264, N'GetVehicleTypeBasedOnFilter', N'Read VehicleType Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (265, N'CreateCustomerCase', N'Create Customer Case Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (266, N'CreateFieldFix', N'Create Field Fix Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (267, N'GetFieldFixBasedOnFilter', N'Read Criteria Field Fix Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (268, N'CreateFreeService', N'Create Free Service Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (269, N'GetMSPMembershipBasedOnFilter', N'Read Criteria MSP Membership Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (270, N'CreatePDI', N'Create PDI Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (271, N'CreateServiceIncoming', N'Create Service Incoming Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (272, N'UpdateServiceIncoming', N'Update Service Incoming Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (273, N'CreateWorkOrderPM', N'Create WorkOrderPM Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (274, N'CreateAPPayment', N'Create AP Payment Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (275, N'CreateARReceipt', N'Create AR Receipt Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (276, N'CreateAssistPartSales', N'Create AssistPartSales Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (277, N'CreateAssistPartStock', N'Create AssistPartStock Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (278, N'UpdateAssistPartStock', N'Update AssistPartStock Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (279, N'GetConversionBasedOnFilter', N'Read Criteria Conversion Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (280, N'CreateDeliveryOrder', N'Create Delivery Order Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (281, N'CreateEstimationEquipHeader', N'Create Estimation Equip Header Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (282, N'CreateIndentPart', N'Create Indent Part Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (283, N'CreateInventoryTransaction', N'Create Inventory Transaction Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (284, N'CreateInventoryTransfer', N'Create Inventory Transfer Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (285, N'GetPaymentDepositBasedOnFilter', N'Read Criteria Payment Deposit Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (286, N'CreatePOOtherVendor', N'Create PO Other Vendor Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (287, N'GetPRHistorySOBasedOnFilter', N'Read Criteria PRHistorySO Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (288, N'GetPRHistoryDOBasedOnFilter', N'Read Criteria PRHistoryDO Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (289, N'GetPurchaseReceiptBasedOnFilter', N'Read Criteria Purchase Receipt Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (290, N'UpdatePurchaseReceipt', N'Update Purchase Receipt')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (291, N'CreateSparePartPO', N'Create SparePart PO')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (292, N'GetSparePartPOOtherBasedOnFilter', N'Read Criteria Sparepart PO Other Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (293, N'UpdateSparePartPO', N'Update SparePartPO')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (294, N'GetSparePartPOEstimateBasedOnFilter', N'Read Criteria Sparepart PO Estimate Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (295, N'CreateSparePartPRFromOtherVendor', N'Create Sparepart PO From other Vendor')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (296, N'GetSparePartPriceListBasedOnFilter', N'Read Criteria Sparepart Price List Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (297, N'CreateSparePartSalesOrder', N'Create Sparepart Sales Order')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (298, N'GetSparePartUoMBasedOnFilter', N'Read Criteria Sparepart UoM Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (299, N'CreateStandardCode', N'Create StandardCode Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (300, N'DeleteStandardCode', N'Delete StandardCode Data by ID')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (301, N'GetStandardCodeBasedOnFilter', N'Read StandardCode Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (302, N'UpdateStandardCode', N'Update StandardCode Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (303, N'CreateStandardCodeChar', N'Create StandardCodeChar Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (304, N'DeleteStandardCodeChar', N'Delete StandardCodeChar Data by ID')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (305, N'GetStandardCodeBasedOnFilterChar', N'Read StandardCodeChar Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (306, N'UpdateStandardCodeChar', N'Update StandardCodeChar Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (307, N'CreateCarrosserie', N'Create Carrosserie')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (308, N'GetPODealerBasedOnFilter', N'Read Criteria PO Dealer Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (309, N'GetPOReceiptDealerBasedOnFilter', N'Read Criteria PO Receipt Dealer Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (310, N'CreateVehiclePurchase', N'Create Vehicle Purchase')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (311, N'GetCampaignReportBasedOnFilter', N'Read Criteria Campaign Report Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (312, N'CreateChassisMasterPKT', N'Create Chassis Master PKT')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (313, N'UpdateChassisMasterPKT', N'Update Chassis Master PKT')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (314, N'CreateLead', N'Create Lead Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (315, N'UpdateLead', N'Update Lead Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (316, N'GetOpenFactureBasedOnFilter', N'Read Open Facture by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (317, N'GetSPKKTP', N'Read SPK KTP by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (318, N'GetSPKDocument', N'Read SPK Document by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (319, N'CreateSPK', N'Create new SPK')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (320, N'UpdateSPK', N'Update SPK')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (321, N'CreateSPKChassis', N'Create SPKChassis Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (322, N'GetCompleteOrCanceledSPKHeader', N'Read Complete Or Canceled SPK Header by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (323, N'WebUser_AccessApp', N'Acces to Web User App')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (324, N'WebUser_GetUserList', N'View User List')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (325, N'WebUser_CreateUser', N'Create User')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (326, N'WebUser_UpdateUser', N'Update User')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (327, N'WebUser_DeleteUser', N'Delete User')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (328, N'WebUser_GetRoleList', N'View Role List')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (329, N'WebUser_CreateRole', N'Create Role')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (330, N'WebUser_UpdateRole', N'Update Role')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (331, N'WebUser_DeleteRole', N'Delete Role')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (332, N'WebUser_GetPermissionList', N'View Permission List')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (333, N'WebUser_CreatePermission', N'Create Permission')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (334, N'WebUser_UpdatePermission', N'Update Permission')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (335, N'WebUser_DeletePermission', N'Delete Permission')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (336, N'WebUser_GetClientList', N'View Client List')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (337, N'WebUser_CreateClient', N'Create Client')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (338, N'WebUser_UpdateClient', N'Update Client')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (339, N'WebUser_DeleteClient', N'Delete Client')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (340, N'WebUser_GetTransactionLogList', N'View TransactionLog List')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (341, N'WebUser_CreateTransactionLog', N'Create TransactionLog')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (342, N'WebUser_UpdateTransactionLog', N'Update TransactionLog')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (343, N'WebUser_DeleteTransactionLog', N'Delete TransactionLog')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (344, N'WebUser_GetErrorLogList', N'View ErrorLog List')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (345, N'WebUser_CreateErrorLog', N'Create ErrorLog')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (346, N'WebUser_UpdateErrorLog', N'Update ErrorLog')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (347, N'WebUser_DeleteErrorLog', N'Delete ErrorLog')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (348, N'GetQuickProductBasedOnFilter', N'FilterByCriteriaQuickProduct')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (349, N'GetVehicleExteriorColorBasedOnFilter', N'FilterByCriteriaVehicleExteriorColor')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (350, N'GetFreeServiceBasedOnFilter', N'FilterByCriteriaFreeService')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (351, N'CreateEmployeeMechanic', N'Create Master Employee Mechanic')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (352, N'UpdateEmployeeMechanic', N'Update Master Employee Mechanic')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (353, N'GetPartShopBasedOnFilter', N'Read PartShop Data by Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (354, N'CreatePartShop', N'Create Part Shop Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (355, N'UpdatePartShop', N'Update Part Shop Data')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (356, N'GetEmployeeMechanicBasedOnFilter', N'Read Employee Mechanic based on Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (357, N'GetEmployeeSalesBasedOnFilter', N'Read Employee Sales Based On Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (358, N'GetEmployeePartsBasedOnFilter', N'Read Master Data Employee Parts Based On Filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (359, N'GetCustomerVehicleBasedOnFilter', N'Get Customer Vehicle based on filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (360, N'WebUser_GetFailedTransactionLogList', N'Get failed transaction history list')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (361, N'WebUser_GetFailedTransactionDetail', N'Get failed transaction details')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (362, N'WebUser_GetTransactionDetail', N'Get transaction details')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (363, N'WebUser_ResendFailedTransactionLogList', N'Get the access to resend selected failed transaction')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (364, N'WebUser_GetDNetInterfaceChart', N'Get D-Net interface charts')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (365, N'CreateEmployeeSales', N'CreateEmployeeSales')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (366, N'UpdateEmployeeSales', N'UpdateEmployeeSales')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (367, N'WebUser_GetAppConfigList', N'Get App Config List')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (368, N'WebUser_CreateAppConfig', N'Create App Config')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (369, N'WebUser_DeleteAppConfig', N'Delete App Config')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (370, N'WebUser_UpdateAppConfig', N'Update App Config')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (371, N'CreateEmployeeVehicle', N'Create Employee Vehicle')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (372, N'GetServiceTemplateBasedOnFilter', N'Get Service Template based on filter')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (373, N'UpdateEmployeeVehicle', N'Update Employee Vehicle')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (374, N'CreateEmployeeParts', N'CreateEmployeeParts')
GO
INSERT [dbo].[Permission] ([Id], [Name], [Description]) VALUES (375, N'UpdateEmployeeParts', N'UpdateEmployeeParts')
GO
SET IDENTITY_INSERT [dbo].[Permission] OFF
GO
SET IDENTITY_INSERT [dbo].[RolePermission] ON 

GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (249, 1, 237, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (250, 1, 238, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (251, 1, 239, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (252, 1, 240, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (253, 1, 241, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (254, 1, 242, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (255, 1, 243, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (256, 1, 244, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (257, 1, 245, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (258, 1, 246, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (259, 1, 247, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (260, 1, 248, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (261, 1, 249, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (262, 1, 250, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (263, 1, 251, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (264, 1, 252, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (265, 1, 253, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (266, 1, 254, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (267, 1, 255, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (268, 1, 256, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (269, 1, 257, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (270, 1, 258, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (271, 1, 259, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (272, 1, 260, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (273, 1, 261, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (274, 1, 262, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (275, 1, 263, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (276, 1, 264, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (277, 1, 265, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (278, 1, 266, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (279, 1, 267, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (280, 1, 268, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (281, 1, 269, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (282, 1, 270, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (283, 1, 271, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (284, 1, 272, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (286, 1, 274, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (287, 1, 275, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (288, 1, 276, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (289, 1, 277, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (290, 1, 278, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (291, 1, 279, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (292, 1, 280, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (293, 1, 281, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (294, 1, 282, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (295, 1, 283, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (296, 1, 284, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (297, 1, 285, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (298, 1, 286, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (299, 1, 287, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (300, 1, 288, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (301, 1, 289, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (302, 1, 290, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (303, 1, 291, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (304, 1, 292, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (305, 1, 293, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (306, 1, 294, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (307, 1, 295, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (308, 1, 296, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (309, 1, 297, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (310, 1, 298, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (311, 1, 299, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (312, 1, 300, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (313, 1, 301, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (314, 1, 302, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (315, 1, 303, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (316, 1, 304, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (317, 1, 305, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (318, 1, 306, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (319, 1, 307, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (320, 1, 308, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (321, 1, 309, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (322, 1, 310, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (323, 1, 311, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (324, 1, 312, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (325, 1, 313, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (326, 1, 314, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (327, 1, 315, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (328, 1, 316, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (329, 1, 317, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (330, 1, 318, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (331, 1, 319, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (332, 1, 320, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (333, 1, 321, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (334, 1, 322, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (335, 1, 323, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (336, 1, 324, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (337, 1, 325, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (338, 1, 326, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (339, 1, 327, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (340, 1, 328, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (341, 1, 329, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (342, 1, 330, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (343, 1, 331, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (344, 1, 332, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (345, 1, 333, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (346, 1, 334, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (347, 1, 335, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (348, 1, 336, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (349, 1, 337, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (350, 1, 338, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (351, 1, 339, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (352, 1, 340, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (353, 1, 341, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (354, 1, 342, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (355, 1, 343, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (356, 1, 344, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (357, 1, 345, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (358, 1, 346, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (359, 1, 347, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (360, 8, 323, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (361, 8, 332, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (363, 11, 238, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (364, 11, 239, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (365, 11, 240, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (366, 11, 241, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (367, 11, 242, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (368, 11, 243, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (369, 11, 244, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (370, 11, 245, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (371, 11, 246, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (372, 11, 247, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (373, 11, 248, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (374, 11, 249, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (375, 11, 250, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (376, 11, 251, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (377, 11, 252, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (378, 11, 253, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (379, 11, 254, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (380, 11, 255, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (381, 11, 256, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (382, 11, 257, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (383, 11, 258, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (384, 11, 259, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (385, 11, 260, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (386, 11, 261, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (387, 11, 262, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (388, 11, 263, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (389, 11, 264, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (390, 11, 265, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (391, 11, 266, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (392, 11, 267, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (393, 11, 268, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (394, 11, 269, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (395, 11, 270, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (396, 11, 271, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (397, 11, 272, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (398, 11, 273, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (399, 11, 274, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (400, 11, 275, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (401, 11, 276, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (402, 11, 277, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (403, 11, 278, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (404, 11, 279, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (405, 11, 280, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (406, 11, 281, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (407, 11, 282, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (408, 11, 283, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (409, 11, 284, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (410, 11, 285, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (411, 11, 286, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (412, 11, 287, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (413, 11, 288, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (414, 11, 289, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (415, 11, 290, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (416, 11, 291, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (417, 11, 292, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (418, 11, 293, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (419, 11, 294, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (420, 11, 295, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (421, 11, 296, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (422, 11, 297, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (423, 11, 298, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (424, 11, 299, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (425, 11, 300, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (426, 11, 301, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (427, 11, 302, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (428, 11, 303, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (429, 11, 304, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (430, 11, 305, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (431, 11, 306, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (432, 11, 307, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (433, 11, 308, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (434, 11, 309, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (435, 11, 310, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (436, 11, 311, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (437, 11, 312, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (438, 11, 313, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (439, 11, 314, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (440, 11, 315, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (441, 11, 316, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (442, 11, 317, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (443, 11, 318, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (444, 11, 319, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (445, 11, 320, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (446, 11, 321, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (447, 11, 322, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (448, 11, 237, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (449, 11, 348, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (451, 11, 349, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (452, 1, 348, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (453, 1, 349, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (454, 1, 350, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (455, 1, 351, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (456, 1, 352, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (459, 1, 353, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (460, 1, 354, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (461, 1, 355, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (462, 1, 273, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (463, 1, 356, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (464, 1, 357, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (465, 1, 358, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (466, 1, 359, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (467, 1, 360, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (468, 1, 361, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (469, 1, 362, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (470, 1, 363, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (471, 1, 364, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (472, 1, 365, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (473, 1, 366, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (474, 11, 365, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (475, 11, 366, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (476, 1, 367, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (477, 1, 368, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (478, 1, 369, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (479, 1, 370, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (480, 1, 371, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (481, 1, 372, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (482, 1, 373, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (483, 1, 374, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (484, 1, 375, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (485, 11, 350, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (486, 11, 351, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (487, 11, 352, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (488, 11, 353, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (489, 11, 354, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (490, 11, 355, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (491, 11, 356, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (492, 11, 357, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (493, 11, 358, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (494, 11, 359, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (495, 11, 371, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (496, 11, 372, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (497, 11, 373, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (498, 11, 374, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (499, 11, 375, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (500, 8, 368, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (501, 8, 337, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (502, 8, 345, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (503, 8, 333, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (504, 8, 329, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (505, 8, 341, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (506, 8, 325, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (507, 8, 369, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (508, 8, 339, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (509, 8, 347, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (510, 8, 335, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (511, 8, 331, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (512, 8, 343, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (513, 8, 327, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (514, 8, 367, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (515, 8, 336, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (516, 8, 364, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (517, 8, 344, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (518, 8, 361, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (519, 8, 360, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (520, 8, 328, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (521, 8, 362, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (522, 8, 340, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (523, 8, 324, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (524, 8, 363, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (525, 8, 370, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (526, 8, 338, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (527, 8, 346, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (528, 8, 334, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (529, 8, 330, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (530, 8, 342, NULL)
GO
INSERT [dbo].[RolePermission] ([Id], [RoleId], [PermissionId], [Description]) VALUES (531, 8, 326, NULL)
GO
SET IDENTITY_INSERT [dbo].[RolePermission] OFF
GO
