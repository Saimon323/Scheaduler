USE [master]
GO
/****** Object:  Database [Scheduler]    Script Date: 2013-11-29 16:42:41 ******/
CREATE DATABASE [Scheduler]
GO
ALTER DATABASE [Scheduler] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Scheduler] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Scheduler] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Scheduler] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Scheduler] SET ARITHABORT OFF 
GO
ALTER DATABASE [Scheduler] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Scheduler] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Scheduler] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Scheduler] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Scheduler] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Scheduler] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Scheduler] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Scheduler] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Scheduler] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Scheduler] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Scheduler] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Scheduler] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Scheduler] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Scheduler] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Scheduler] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Scheduler] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Scheduler] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Scheduler] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Scheduler] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Scheduler] SET  MULTI_USER 
GO
ALTER DATABASE [Scheduler] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Scheduler] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Scheduler] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Scheduler] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Scheduler]
GO
/****** Object:  Table [dbo].[Coments]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Time] [date] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Coments] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Documents]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[DocumentName] [nvarchar](50) NOT NULL,
	[DocumentContent] [nvarchar](max) NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MenagerId] [int] NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
	[CreationData] [date] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ToUserId] [int] NOT NULL,
	[Time] [date] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](50) NOT NULL,
	[FromUserId] [int] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](50) NOT NULL,
	[Budget] [float] NOT NULL,
	[StartTime] [date] NOT NULL,
	[StopTime] [date] NULL,
	[OwnerId] [int] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectsToGroupsRealizations]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectsToGroupsRealizations](
	[ProjectId] [int] NOT NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectsToGroupsRealizations] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[WorkerId] [int] NULL,
	[StartTime] [date] NOT NULL,
	[StopTime] [date] NULL,
	[TaskName] [nvarchar](50) NOT NULL,
	[Hours] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 2013-11-29 16:42:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Coments]  WITH CHECK ADD  CONSTRAINT [FK_Coments_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([id])
GO
ALTER TABLE [dbo].[Coments] CHECK CONSTRAINT [FK_Coments_Tasks]
GO
ALTER TABLE [dbo].[Coments]  WITH CHECK ADD  CONSTRAINT [FK_Coments_Users] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Coments] CHECK CONSTRAINT [FK_Coments_Users]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Projects]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD  CONSTRAINT [FK_Documents_Users] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Users]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Users] FOREIGN KEY([MenagerId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Users]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users] FOREIGN KEY([ToUserId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users1] FOREIGN KEY([FromUserId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users1]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Users] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Users]
GO
ALTER TABLE [dbo].[ProjectsToGroupsRealizations]  WITH CHECK ADD  CONSTRAINT [FK_ProjectsToGroupsRealizations_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[ProjectsToGroupsRealizations] CHECK CONSTRAINT [FK_ProjectsToGroupsRealizations_Groups]
GO
ALTER TABLE [dbo].[ProjectsToGroupsRealizations]  WITH CHECK ADD  CONSTRAINT [FK_ProjectsToGroupsRealizations_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[ProjectsToGroupsRealizations] CHECK CONSTRAINT [FK_ProjectsToGroupsRealizations_Projects]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projects]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users] FOREIGN KEY([WorkerId])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Groups]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [Scheduler] SET  READ_WRITE 
GO
