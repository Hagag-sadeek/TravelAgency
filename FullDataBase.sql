Create database [TravelAgency]
GO

USE [TravelAgency]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppointmentDetails]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppointmentDetails](
	[AppointmentDetailId] [int] IDENTITY(1,1) NOT NULL,
	[AppointmentId] [int] NULL,
	[BranchId] [int] NOT NULL,
	[LeaveTime] [time](7) NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_AppointmentDetails] PRIMARY KEY CLUSTERED 
(
	[AppointmentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[BranchId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
 CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Job] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Adreess1] [nvarchar](max) NULL,
	[Adreess2] [nvarchar](max) NULL,
	[Phone1] [nvarchar](max) NULL,
	[Phone2] [nvarchar](max) NULL,
	[Phone3] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[Commision] [int] NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Code] [nvarchar](max) NULL,
	[Job] [nvarchar](max) NULL,
	[Adreess1] [nvarchar](max) NULL,
	[Phone1] [nvarchar](max) NULL,
	[Phone2] [nvarchar](max) NULL,
	[Phone3] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[SupplierId] [int] NULL,
	[UserId] [int] NOT NULL,
	[AppointmentId] [int] NOT NULL,
	[TicketDate] [datetime2](7) NOT NULL,
	[Price] [int] NOT NULL,
	[FromBranchId] [int] NULL,
	[ToBranchId] [int] NULL,
	[SeatId] [int] NOT NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/31/2020 10:09:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Firstname] [nvarchar](max) NOT NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200823164722_AddUsersinit', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200823172902_users', N'3.1.2')
SET IDENTITY_INSERT [dbo].[AppointmentDetails] ON 

INSERT [dbo].[AppointmentDetails] ([AppointmentDetailId], [AppointmentId], [BranchId], [LeaveTime], [Price]) VALUES (1, 1, 1, CAST(N'09:00:00' AS Time), 120)
INSERT [dbo].[AppointmentDetails] ([AppointmentDetailId], [AppointmentId], [BranchId], [LeaveTime], [Price]) VALUES (3, 1, 4, CAST(N'10:00:00' AS Time), 120)
INSERT [dbo].[AppointmentDetails] ([AppointmentDetailId], [AppointmentId], [BranchId], [LeaveTime], [Price]) VALUES (4, 1, 3, CAST(N'10:30:00' AS Time), 120)
INSERT [dbo].[AppointmentDetails] ([AppointmentDetailId], [AppointmentId], [BranchId], [LeaveTime], [Price]) VALUES (5, 2, 2, CAST(N'10:00:00' AS Time), 120)
SET IDENTITY_INSERT [dbo].[AppointmentDetails] OFF
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([AppointmentId], [Title], [Description], [IsActive]) VALUES (1, N'ميعاد سيتي ترافيل الساعه 9 من الصعيد', N'ميعاد سيتي ترافيل الساعه 9 من الوقف قنا القاهره', 1)
INSERT [dbo].[Appointments] ([AppointmentId], [Title], [Description], [IsActive]) VALUES (2, N'ميعاد سيتي ترافيل من القاهره الساعه 10', N'ميعاد سيتي ترافيل من القاهره الساعه 10قنا الوقف', 1)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone]) VALUES (1, N'مكتب الوقف', N'مكتب الوقف', 1, N'مكتب الوقف', N'01025032878')
INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone]) VALUES (2, N'مكتب القاهره', N'مكتب القاهره', 1, N'مكتب القاهره', N'0123654785')
INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone]) VALUES (3, N'مكتب قنا', N'مكتب قنا', 1, N'قنا - اعلى مكتب الاتحاد العربي', N'010')
INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone]) VALUES (4, N'مكتب الترامسه', N'مكتب الترامسه', 1, N'اخر كوبري دندره', N'01025032878')
SET IDENTITY_INSERT [dbo].[Branches] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerId], [Code], [Job], [FullName], [Adreess1], [Adreess2], [Phone1], [Phone2], [Phone3], [IsActive]) VALUES (1, N'01', N'عميل مكتب', N'عميل مكتب', N'012012472487', N'02225487', N'012012472487', NULL, NULL, 1)
INSERT [dbo].[Customers] ([CustomerId], [Code], [Job], [FullName], [Adreess1], [Adreess2], [Phone1], [Phone2], [Phone3], [IsActive]) VALUES (2, N'005', NULL, N'م علاء', N'ش', NULL, N'0120', NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive]) VALUES (1, 20, N'مكتب الوقف', N'1', N'مكتب الوقف', N'مكتب الوقف', N'01025032878', NULL, NULL, 1)
INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive]) VALUES (2, 20, N'مكتب قنا', N'2', N'مكتب قنا', N'مكتب قنا', N'01025032878', NULL, NULL, 1)
INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive]) VALUES (3, 20, N'مكتب الترامسه', N'3', N'مكتب الترامسه', N'قنا-اخر كوبري دندره', N'01025032878', NULL, NULL, 1)
INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive]) VALUES (4, 0, N'مكتب القاهره', N'4', N'مكتب القاهره', N'القاهره-بجوار مسجد الفتح', N'01025032878', NULL, NULL, 1)
INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive]) VALUES (5, 10, N'جرار من المحطه', N'5', N'جرار من المحطه', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId]) VALUES (6, 2, 2, 1, 1, CAST(N'2020-08-31T00:00:00.0000000' AS DateTime2), 120, 3, 2, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId]) VALUES (7, 1, 2, 1, 1, CAST(N'2020-08-31T00:00:00.0000000' AS DateTime2), 0, 3, 2, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId]) VALUES (8, 1, 2, 1, 1, CAST(N'2020-08-31T00:00:00.0000000' AS DateTime2), 207, 3, 2, 39)
SET IDENTITY_INSERT [dbo].[Tickets] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Firstname], [BranchId]) VALUES (1, N'1', N'1', N'مكتب الوقف', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[AppointmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentDetails_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([AppointmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AppointmentDetails] CHECK CONSTRAINT [FK_AppointmentDetails_Appointments]
GO
ALTER TABLE [dbo].[AppointmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentDetails_Branches_BranchId] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([BranchId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AppointmentDetails] CHECK CONSTRAINT [FK_AppointmentDetails_Branches_BranchId]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([AppointmentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Appointments]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Branches] FOREIGN KEY([FromBranchId])
REFERENCES [dbo].[Branches] ([BranchId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Branches]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Branches1] FOREIGN KEY([ToBranchId])
REFERENCES [dbo].[Branches] ([BranchId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Branches1]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Suppliers_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([SupplierId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Suppliers_SupplierId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Branches] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([BranchId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Branches]
GO
