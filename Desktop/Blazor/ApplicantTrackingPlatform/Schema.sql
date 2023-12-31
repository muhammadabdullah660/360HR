USE [360HR]
GO
/****** Object:  Table [dbo].[Achievement]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievement](
	[ProfileID] [int] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [UQ_Achievement] UNIQUE NONCLUSTERED 
(
	[ProfileID] ASC,
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[StreetNo] [nvarchar](50) NOT NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddressChanges]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressChanges](
	[ChangeID] [int] IDENTITY(1,1) NOT NULL,
	[AddressID] [int] NOT NULL,
	[OldStreet] [nvarchar](100) NULL,
	[OldState] [nvarchar](100) NULL,
	[OldCountry] [nvarchar](100) NULL,
	[NewStreet] [nvarchar](100) NULL,
	[NewState] [nvarchar](100) NULL,
	[NewCountry] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[AddressID] [int] NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Contact] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompanyChanges]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyChanges](
	[ChangeID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[OldTitle] [nvarchar](100) NULL,
	[OldDescription] [nvarchar](max) NULL,
	[OldAddressID] [int] NULL,
	[OldContact] [nvarchar](50) NULL,
	[NewTitle] [nvarchar](100) NULL,
	[NewDescription] [nvarchar](max) NULL,
	[NewAddressID] [int] NULL,
	[NewContact] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](100) NULL,
	[Name] [nvarchar](100) NOT NULL,
	[StartDate] [datetime] NULL,
	[ProfileID] [int] NOT NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Education]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Education](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InstituteName] [nvarchar](100) NOT NULL,
	[Degree] [nvarchar](100) NOT NULL,
	[StartYear] [datetime] NULL,
	[EndYear] [datetime] NULL,
	[ProfileID] [int] NOT NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Grade] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExceptionTable]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExceptionTable](
	[ExceptionID] [int] IDENTITY(1,1) NOT NULL,
	[FunctionName] [nvarchar](255) NOT NULL,
	[ExceptionMessage] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExceptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FriendshipRequest]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FriendshipRequest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProfileIDSender] [int] NOT NULL,
	[ProfileIDReceiver] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[DateSent] [datetime] NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InterviewFeedback]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterviewFeedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[InterviewID] [int] NOT NULL,
	[FeedbackText] [nvarchar](max) NOT NULL,
	[FeedbackDate] [datetime] NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interviewslot]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interviewslot](
	[interviewid] [int] NULL,
	[slotdatetime] [nvarchar](25) NULL,
 CONSTRAINT [UC_interviewSLot] UNIQUE NONCLUSTERED 
(
	[interviewid] ASC,
	[slotdatetime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Managerid] [int] NOT NULL,
	[RecruiterId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobApplication]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobApplication](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NOT NULL,
	[ProfileID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[DateOfApply] [datetime] NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_JobIDProfileID] UNIQUE NONCLUSTERED 
(
	[JobID] ASC,
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobChanges]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobChanges](
	[ChangeID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NOT NULL,
	[OldTitle] [nvarchar](100) NULL,
	[OldDescription] [nvarchar](max) NULL,
	[OldcompabyID] [int] NULL,
	[OldManagerID] [int] NULL,
	[NewTitle] [nvarchar](100) NULL,
	[NewDescription] [nvarchar](max) NULL,
	[NewcompabyID] [int] NULL,
	[NewManagerID] [int] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobSkills]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobSkills](
	[JobID] [int] NULL,
	[Name] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[Managerid] [int] IDENTITY(1,1) NOT NULL,
	[Personid] [int] NOT NULL,
	[Companyid] [int] NOT NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[isManager] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Managerid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Personid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Person_PersonID] UNIQUE NONCLUSTERED 
(
	[Personid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Gender] [char](1) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](11) NULL,
	[Password] [nvarchar](8) NOT NULL,
	[RoleID] [int] NULL,
	[AddressID] [int] NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PhoneNumber] UNIQUE NONCLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Person__A9D1053422E183B4] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonChanges]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonChanges](
	[ChangeID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[OldFirstName] [nvarchar](50) NULL,
	[OldLastName] [nvarchar](50) NULL,
	[OldGender] [nvarchar](50) NULL,
	[OldEmail] [nvarchar](255) NULL,
	[OldPhoneNumber] [nvarchar](11) NULL,
	[OldPassword] [nvarchar](8) NULL,
	[OldRoleID] [int] NULL,
	[OldAddressID] [int] NULL,
	[NewFirstName] [nvarchar](50) NULL,
	[NewLastName] [nvarchar](50) NULL,
	[NewGender] [nvarchar](50) NULL,
	[NewEmail] [nvarchar](255) NULL,
	[NewPhoneNumber] [nvarchar](11) NULL,
	[NewPassword] [nvarchar](8) NULL,
	[NewRoleID] [int] NULL,
	[NewAddressID] [int] NULL,
	[CreatedAT] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[ProfilePicture] [image] NULL,
	[LinkedInLink] [nvarchar](255) NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[isFreelancer] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_PersonID] UNIQUE NONCLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ManagerID] [int] NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[RecruiterId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectApplicants]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectApplicants](
	[ProjectID] [int] NOT NULL,
	[ProfileID] [int] NOT NULL,
	[StatusID] [int] NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[updatedAt] [datetime] NOT NULL,
	[active] [bit] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rate] [real] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectChanges]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectChanges](
	[ChangeID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[OldTitle] [nvarchar](100) NULL,
	[OldDescription] [nvarchar](max) NULL,
	[OldStartDate] [datetime] NULL,
	[OldEndDate] [datetime] NULL,
	[OldManagerID] [int] NULL,
	[NewTitle] [nvarchar](100) NULL,
	[NewDescription] [nvarchar](max) NULL,
	[NewStartDate] [datetime] NULL,
	[NewEndDate] [datetime] NULL,
	[NewManagerID] [int] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChangeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectSkills]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectSkills](
	[ProjectId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[ProfileID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [UC_Skills] UNIQUE NONCLUSTERED 
(
	[ProfileID] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkExperience]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkExperience](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[Duration] [float] NOT NULL,
	[StartDate] [datetime] NULL,
	[Role] [nvarchar](100) NOT NULL,
	[ProfileID] [int] NOT NULL,
	[createdAT] [datetime] NOT NULL,
	[updatedAT] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_CompanyIDProfileID] UNIQUE NONCLUSTERED 
(
	[CompanyID] ASC,
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Achievement] ADD  CONSTRAINT [DF_CreatedATAchivement]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Achievement] ADD  CONSTRAINT [DF_UpdatedATAchivement]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Achievement] ADD  CONSTRAINT [DF_ActiveAchivement]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_createdATAddress]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_updatedATAddress]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [DF_ActiveAddress]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[AddressChanges] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_createdATCompany]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_updatedATCompany]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_ActiveCompany]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[CompanyChanges] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_CreatedATCourse]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_UpdatedATCourse]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Courses] ADD  CONSTRAINT [DF_ActiveCourse]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_CreatedATEducation]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_UpdatedATEducation]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Education] ADD  CONSTRAINT [DF_ActiveEducation]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ExceptionTable] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[FriendshipRequest] ADD  CONSTRAINT [DF_CreatedATFrnd]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[FriendshipRequest] ADD  CONSTRAINT [DF_UpdatedATFrnd]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[FriendshipRequest] ADD  CONSTRAINT [DF_ActiveFrnd]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[InterviewFeedback] ADD  CONSTRAINT [DF_CreatedATFeedback]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[InterviewFeedback] ADD  CONSTRAINT [DF_UpdatedATFeedback]  DEFAULT (getdate()) FOR [updatedAt]
GO
ALTER TABLE [dbo].[InterviewFeedback] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_CreatedATJob]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_UpdatedATJob]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Job] ADD  CONSTRAINT [DF_ActiveJob]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[JobApplication] ADD  CONSTRAINT [DF_DateofApplyjobApplicant]  DEFAULT (getdate()) FOR [DateOfApply]
GO
ALTER TABLE [dbo].[JobApplication] ADD  CONSTRAINT [DF_CreatedATjobApplicant]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[JobApplication] ADD  CONSTRAINT [DF_UpdatedATjobApplicant]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[JobApplication] ADD  CONSTRAINT [DF_ActivejobApplicant]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[JobChanges] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Manager] ADD  CONSTRAINT [DF_createdATManager]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Manager] ADD  CONSTRAINT [DF_updatedATManager]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Manager] ADD  CONSTRAINT [DF_ActiveManager]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_createdAT]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_updatedAT]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[PersonChanges] ADD  DEFAULT (getdate()) FOR [CreatedAT]
GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF_createdATProfile]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF_updatedATProfile]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF_ActiveProfile]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_CreatedATProject]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Project] ADD  CONSTRAINT [DF_UpdatedATProject]  DEFAULT (getdate()) FOR [updatedAt]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[ProjectApplicants] ADD  CONSTRAINT [DF_CreatedAtProjectApplicant]  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[ProjectApplicants] ADD  CONSTRAINT [DF_UpdatedAtProjectApplicant]  DEFAULT (getdate()) FOR [updatedAt]
GO
ALTER TABLE [dbo].[ProjectApplicants] ADD  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[ProjectChanges] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Skills] ADD  CONSTRAINT [Df_CreatedATSkills]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[Skills] ADD  CONSTRAINT [Df_UpdatedATSkills]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[Skills] ADD  CONSTRAINT [Df_ActiveSkills]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[WorkExperience] ADD  CONSTRAINT [DF_CreatedATWork]  DEFAULT (getdate()) FOR [createdAT]
GO
ALTER TABLE [dbo].[WorkExperience] ADD  CONSTRAINT [DF_UpdatedATWork]  DEFAULT (getdate()) FOR [updatedAT]
GO
ALTER TABLE [dbo].[WorkExperience] ADD  CONSTRAINT [DF_ActiveWork]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Achievement]  WITH CHECK ADD FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[AddressChanges]  WITH CHECK ADD  CONSTRAINT [FK_AddressChanges_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[AddressChanges] CHECK CONSTRAINT [FK_AddressChanges_AddressID]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[CompanyChanges]  WITH CHECK ADD  CONSTRAINT [FK_CompanyChanges_CompanyID] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([ID])
GO
ALTER TABLE [dbo].[CompanyChanges] CHECK CONSTRAINT [FK_CompanyChanges_CompanyID]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[Education]  WITH CHECK ADD FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[FriendshipRequest]  WITH CHECK ADD  CONSTRAINT [FK_Receiver] FOREIGN KEY([ProfileIDReceiver])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[FriendshipRequest] CHECK CONSTRAINT [FK_Receiver]
GO
ALTER TABLE [dbo].[FriendshipRequest]  WITH CHECK ADD  CONSTRAINT [FK_Sender] FOREIGN KEY([ProfileIDSender])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[FriendshipRequest] CHECK CONSTRAINT [FK_Sender]
GO
ALTER TABLE [dbo].[FriendshipRequest]  WITH CHECK ADD  CONSTRAINT [FK_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([ID])
GO
ALTER TABLE [dbo].[FriendshipRequest] CHECK CONSTRAINT [FK_Status]
GO
ALTER TABLE [dbo].[InterviewFeedback]  WITH CHECK ADD  CONSTRAINT [FK_InterviewFeedback_Interview] FOREIGN KEY([InterviewID])
REFERENCES [dbo].[JobApplication] ([ID])
GO
ALTER TABLE [dbo].[InterviewFeedback] CHECK CONSTRAINT [FK_InterviewFeedback_Interview]
GO
ALTER TABLE [dbo].[Interviewslot]  WITH CHECK ADD  CONSTRAINT [FK_interviewslot_interviewid] FOREIGN KEY([interviewid])
REFERENCES [dbo].[JobApplication] ([ID])
GO
ALTER TABLE [dbo].[Interviewslot] CHECK CONSTRAINT [FK_interviewslot_interviewid]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([ID])
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_Manager] FOREIGN KEY([Managerid])
REFERENCES [dbo].[Manager] ([Managerid])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_Manager]
GO
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Recruiter] FOREIGN KEY([RecruiterId])
REFERENCES [dbo].[Manager] ([Managerid])
GO
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Recruiter]
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([ID])
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[JobApplication]  WITH CHECK ADD FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([ID])
GO
ALTER TABLE [dbo].[JobChanges]  WITH CHECK ADD  CONSTRAINT [FK_JobChanges_JobID] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([ID])
GO
ALTER TABLE [dbo].[JobChanges] CHECK CONSTRAINT [FK_JobChanges_JobID]
GO
ALTER TABLE [dbo].[JobSkills]  WITH CHECK ADD FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([ID])
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK_Manager_Company] FOREIGN KEY([Companyid])
REFERENCES [dbo].[Company] ([ID])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK_Manager_Company]
GO
ALTER TABLE [dbo].[Manager]  WITH CHECK ADD  CONSTRAINT [FK_Manager_Person] FOREIGN KEY([Personid])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Manager] CHECK CONSTRAINT [FK_Manager_Person]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[PersonChanges]  WITH CHECK ADD  CONSTRAINT [FK_PersonChanges_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[PersonChanges] CHECK CONSTRAINT [FK_PersonChanges_PersonID]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Manager] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Manager] ([Managerid])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Manager]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_RecruiterProject] FOREIGN KEY([RecruiterId])
REFERENCES [dbo].[Manager] ([Managerid])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_RecruiterProject]
GO
ALTER TABLE [dbo].[ProjectApplicants]  WITH CHECK ADD  CONSTRAINT [FK_ProjectApplicants_Profile] FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[ProjectApplicants] CHECK CONSTRAINT [FK_ProjectApplicants_Profile]
GO
ALTER TABLE [dbo].[ProjectApplicants]  WITH CHECK ADD  CONSTRAINT [FK_ProjectApplicants_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[ProjectApplicants] CHECK CONSTRAINT [FK_ProjectApplicants_Project]
GO
ALTER TABLE [dbo].[ProjectApplicants]  WITH CHECK ADD  CONSTRAINT [FK_ProjectApplicants_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([ID])
GO
ALTER TABLE [dbo].[ProjectApplicants] CHECK CONSTRAINT [FK_ProjectApplicants_Status]
GO
ALTER TABLE [dbo].[ProjectChanges]  WITH CHECK ADD  CONSTRAINT [FK_ProjectChanges_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[ProjectChanges] CHECK CONSTRAINT [FK_ProjectChanges_ProjectID]
GO
ALTER TABLE [dbo].[ProjectSkills]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([ID])
GO
ALTER TABLE [dbo].[Skills]  WITH CHECK ADD FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[WorkExperience]  WITH CHECK ADD FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([ID])
GO
ALTER TABLE [dbo].[WorkExperience]  WITH CHECK ADD FOREIGN KEY([ProfileID])
REFERENCES [dbo].[Profile] ([ID])
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD CHECK  (([Gender]='M' OR [Gender]='F'))
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD CHECK  (([Password] like '%[0-9]%' AND [Password] like '%[A-Za-z]%' AND [Password] like '%[^A-Za-z0-9]%'))
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD CHECK  ((len([PhoneNumber])=(11)))
GO
/****** Object:  StoredProcedure [dbo].[InsertPersonWithOutput]    Script Date: 11/28/2023 11:31:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertPersonWithOutput]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Gender CHAR(1),
    @Email NVARCHAR(255),
    @PhoneNumber NVARCHAR(11),
    @Password NVARCHAR(8),
    @RoleId INT,
    @AddressId INT,
    @PersonID INT OUTPUT
AS
BEGIN
    INSERT INTO Person (FirstName, LastName, Gender, Email, PhoneNumber, Password, RoleId, AddressId)
    VALUES (@FirstName, @LastName, @Gender, @Email, @PhoneNumber, @Password, @RoleId, @AddressId);

    SET @PersonID = SCOPE_IDENTITY(); -- Set the output parameter to the newly inserted PersonID
END;
GO
