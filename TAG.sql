USE DB_A66B31_asadeek
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/29/2020 9:36:56 AM ******/
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
/****** Object:  Table [dbo].[AppointmentDetails]    Script Date: 9/29/2020 9:36:56 AM ******/
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
/****** Object:  Table [dbo].[Appointments]    Script Date: 9/29/2020 9:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Branches]    Script Date: 9/29/2020 9:36:56 AM ******/
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
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Branches] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 9/29/2020 9:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 9/29/2020 9:36:56 AM ******/
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
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 9/29/2020 9:36:56 AM ******/
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
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketDistributions]    Script Date: 9/29/2020 9:36:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketDistributions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SeatNumber] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_TicketDistributions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 9/29/2020 9:36:56 AM ******/
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
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/29/2020 9:36:56 AM ******/
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
	[CompanyId] [int] NOT NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200927204958_init1', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200927223211_asd1', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200927230007_asd12', N'3.1.2')
SET IDENTITY_INSERT [dbo].[AppointmentDetails] ON 

INSERT [dbo].[AppointmentDetails] ([AppointmentDetailId], [AppointmentId], [BranchId], [LeaveTime], [Price]) VALUES (1, 1, 1, CAST(N'10:00:00' AS Time), 120)
INSERT [dbo].[AppointmentDetails] ([AppointmentDetailId], [AppointmentId], [BranchId], [LeaveTime], [Price]) VALUES (2, 2, 2, CAST(N'09:00:00' AS Time), 120)
INSERT [dbo].[AppointmentDetails] ([AppointmentDetailId], [AppointmentId], [BranchId], [LeaveTime], [Price]) VALUES (3, 3, 1, CAST(N'09:30:00' AS Time), 120)
SET IDENTITY_INSERT [dbo].[AppointmentDetails] OFF
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([AppointmentId], [Title], [Description], [IsActive], [CompanyId]) VALUES (1, N'ميعاد ترافيل من القاهره الساعه 10', N'ميعاد ترافيل من القاهره الساعه 10', 1, 1)
INSERT [dbo].[Appointments] ([AppointmentId], [Title], [Description], [IsActive], [CompanyId]) VALUES (2, N'ميعاد ترافيل من نقاده الساعه 9', N'ميعاد ترافيل من نقاده الساعه 9', 1, 1)
INSERT [dbo].[Appointments] ([AppointmentId], [Title], [Description], [IsActive], [CompanyId]) VALUES (3, N'ميعاد كوزمو من القاهره', NULL, 1, 2)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
SET IDENTITY_INSERT [dbo].[Branches] ON 

INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone], [CompanyId]) VALUES (1, N'مكتب القاهره', N'مكتب القاهره', 1, N'127 ش رمسيس-عماره الفتح', N'01030565720', 1)
INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone], [CompanyId]) VALUES (2, N'مكتب نقاده', N'مكتب نقاده', 1, N'مكتب الفاروق-امام الموقف', N'01021788050', 1)
INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone], [CompanyId]) VALUES (3, N'مكتب القاهره', N'34434', 1, N'343', N'343', 2)
INSERT [dbo].[Branches] ([BranchId], [Title], [Description], [IsActive], [Address], [Phone], [CompanyId]) VALUES (4, N'دشنا', N'1', 1, N'2', N'1', 2)
SET IDENTITY_INSERT [dbo].[Branches] OFF
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [IsActive]) VALUES (1, N'ترافيل للنقل البري', N'ترافيل للنقل البري', 1)
INSERT [dbo].[Company] ([CompanyId], [Name], [Description], [IsActive]) VALUES (2, N'كوزمو ترافيل', N'كوزمو ترافيل', 1)
SET IDENTITY_INSERT [dbo].[Company] OFF
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerId], [Code], [Job], [FullName], [Adreess1], [Adreess2], [Phone1], [Phone2], [Phone3], [IsActive], [CompanyId]) VALUES (1, N'1', N'فني لحام', N'رائد محمد ابراهيم', N'نقاده ', NULL, N'01025478545', NULL, NULL, 1, 1)
INSERT [dbo].[Customers] ([CustomerId], [Code], [Job], [FullName], [Adreess1], [Adreess2], [Phone1], [Phone2], [Phone3], [IsActive], [CompanyId]) VALUES (2, N'1', NULL, N'عبده الدشناوي', NULL, N'123', N'01254451', NULL, NULL, 1, 2)
SET IDENTITY_INSERT [dbo].[Customers] OFF
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive], [CompanyId]) VALUES (1, 20, N'الاسواني', NULL, NULL, NULL, NULL, NULL, NULL, 1, 2)
INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive], [CompanyId]) VALUES (2, 20, N'جمال', NULL, NULL, NULL, NULL, NULL, NULL, 1, 1)
INSERT [dbo].[Suppliers] ([SupplierId], [Commision], [FullName], [Code], [Job], [Adreess1], [Phone1], [Phone2], [Phone3], [IsActive], [CompanyId]) VALUES (3, 18, N'نقاده', NULL, NULL, NULL, NULL, NULL, NULL, 1, 2)
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (1, 1, 2, 1, 1, CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 120, 1, 2, 0, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (3, 1, 2, 1, 1, CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 120, 1, 2, 2, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (4, 1, 2, 1, 1, CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 150, 2, 3, 6, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (5, 2, 1, 4, 3, CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 150, 1, 4, 1, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (6, 2, 3, 4, 3, CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 1, 1, 1, 2, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (7, 2, 3, 4, 3, CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 1, 1, 1, 5, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (8, 2, 3, 4, 3, CAST(N'2020-09-27T00:00:00.0000000' AS DateTime2), 1, 1, 1, 6, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (9, 2, 3, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 1, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (10, 2, 3, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 2, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (11, 2, 3, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 17, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (12, 2, 3, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 10, 1, 1, 9, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (13, 2, 3, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 5, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (14, 2, 1, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 13, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (15, 2, 1, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 14, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (16, 2, 1, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 0, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (17, 2, 1, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 6, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (18, 2, 1, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 18, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (19, 2, 1, 4, 3, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 10, 1, 1, 10, 2)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (20, 1, 2, 1, 1, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 120, 1, 1, 1, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (21, 1, 2, 1, 1, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 120, 1, 1, 9, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (22, 1, 2, 1, 1, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 2, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (23, 1, 2, 1, 1, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 5, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (24, 1, 2, 1, 1, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 17, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (25, 1, 2, 1, 1, CAST(N'2020-09-28T00:00:00.0000000' AS DateTime2), 0, 1, 1, 13, 1)
INSERT [dbo].[Tickets] ([TicketId], [CustomerId], [SupplierId], [UserId], [AppointmentId], [TicketDate], [Price], [FromBranchId], [ToBranchId], [SeatId], [CompanyId]) VALUES (26, 1, 2, 1, 1, CAST(N'2020-09-29T00:00:00.0000000' AS DateTime2), 120, 1, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Tickets] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Firstname], [BranchId], [CompanyId], [IsAdmin]) VALUES (1, N'1', N'1', N'ابو عمر', 2, 1, 1)
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Firstname], [BranchId], [CompanyId], [IsAdmin]) VALUES (2, N'2', N'2', N'حجاج', 1, 1, 0)
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Firstname], [BranchId], [CompanyId], [IsAdmin]) VALUES (3, N'3', N'3', N'عفيفي', 1, 2, 0)
INSERT [dbo].[Users] ([UserId], [UserName], [Password], [Firstname], [BranchId], [CompanyId], [IsAdmin]) VALUES (4, N'4', N'4', N'حماده', 4, 2, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[AppointmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentDetails_Appointments] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointments] ([AppointmentId])
GO
ALTER TABLE [dbo].[AppointmentDetails] CHECK CONSTRAINT [FK_AppointmentDetails_Appointments]
GO
ALTER TABLE [dbo].[AppointmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentDetails_Branches] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([BranchId])
GO
ALTER TABLE [dbo].[AppointmentDetails] CHECK CONSTRAINT [FK_AppointmentDetails_Branches]
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD  CONSTRAINT [FK_Appointments_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Appointments] CHECK CONSTRAINT [FK_Appointments_Company]
GO
ALTER TABLE [dbo].[Branches]  WITH CHECK ADD  CONSTRAINT [FK_Branches_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Branches] CHECK CONSTRAINT [FK_Branches_Company]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Company]
GO
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_Suppliers_Company]
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
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Branches1]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Company]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Customers_CustomerId]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Suppliers_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([SupplierId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Suppliers_SupplierId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Branches] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branches] ([BranchId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Branches]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Company]
GO
