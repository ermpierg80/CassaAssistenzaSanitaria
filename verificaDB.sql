print @@version;

--creazione DB

USE [master]
GO
/****** Object:  Database [CassaAssistenza]    Script Date: 22/03/2021 18:15:47 ******/
CREATE DATABASE [CassaAssistenza]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CassaAssistenza', FILENAME = N'F:\Data\CassaAssistenza.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CassaAssistenza_log', FILENAME = N'F:\Log\CassaAssistenza_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CassaAssistenza] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CassaAssistenza].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CassaAssistenza] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CassaAssistenza] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CassaAssistenza] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CassaAssistenza] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CassaAssistenza] SET ARITHABORT OFF 
GO
ALTER DATABASE [CassaAssistenza] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CassaAssistenza] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CassaAssistenza] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CassaAssistenza] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CassaAssistenza] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CassaAssistenza] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CassaAssistenza] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CassaAssistenza] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CassaAssistenza] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CassaAssistenza] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CassaAssistenza] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CassaAssistenza] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CassaAssistenza] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CassaAssistenza] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CassaAssistenza] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CassaAssistenza] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CassaAssistenza] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CassaAssistenza] SET RECOVERY FULL 
GO
ALTER DATABASE [CassaAssistenza] SET  MULTI_USER 
GO
ALTER DATABASE [CassaAssistenza] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CassaAssistenza] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CassaAssistenza] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CassaAssistenza] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [CassaAssistenza] SET DELAYED_DURABILITY = DISABLED 
GO
USE [CassaAssistenza]
GO
/****** Object:  User [sec]    Script Date: 22/03/2021 18:15:48 ******/
CREATE USER [sec] FOR LOGIN [casec] WITH DEFAULT_SCHEMA=[sec]
GO
/****** Object:  User [adm]    Script Date: 22/03/2021 18:15:49 ******/
CREATE USER [adm] FOR LOGIN [caadm] WITH DEFAULT_SCHEMA=[adm]
GO
ALTER ROLE [db_datareader] ADD MEMBER [sec]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [sec]
GO
ALTER ROLE [db_datareader] ADD MEMBER [adm]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [adm]
GO
/****** Object:  Schema [adm]    Script Date: 22/03/2021 18:15:49 ******/
CREATE SCHEMA [adm]
GO
/****** Object:  Schema [sec]    Script Date: 22/03/2021 18:15:49 ******/
CREATE SCHEMA [sec]
GO
/****** Object:  Table [adm].[Iscritti]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [adm].[Iscritti](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](25) NOT NULL,
	[Cognome] [varchar](25) NOT NULL,
	[CodiceFiscale] [varchar](16) NOT NULL,
	[IBAN] [varchar](27) NOT NULL,
	[DataIscrizione] [datetime2](7) NOT NULL,
	[DataCancellazione] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Iscritti] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [adm].[Prestazioni]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [adm].[Prestazioni](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descrizione] [varchar](255) NOT NULL,
	[PercentualeRimborso] [decimal](5, 2) NOT NULL,
	[DataCreazione] [datetime2](7) NOT NULL,
	[DataCancellazione] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Prestazioni] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [adm].[Richieste]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [adm].[Richieste](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImportoFattura] [decimal](7, 2) NOT NULL,
	[ImportoRimborsatoDaTerzi] [decimal](7, 2) NOT NULL,
	[ImportoACarico] [decimal](7, 2) NOT NULL,
	[ImportoDaRimborsare] [decimal](7, 2) NOT NULL,
	[NumeroFattura] [varchar](50) NOT NULL,
	[DataFattura] [datetime2](7) NOT NULL,
	[DataRichiesta] [datetime2](7) NOT NULL,
	[DataConferma] [datetime2](7) NOT NULL,
	[Note] [varchar](max) NULL,
	[RichiedenteId] [int] NOT NULL,
	[TipologiaId] [int] NOT NULL,
	[DataCancellazione] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Richieste] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[__EFMigrationsHistory]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [sec].[AspNetRoleClaims]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [sec].[AspNetRoles]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [sec].[AspNetUserClaims]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [sec].[AspNetUserLogins]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [sec].[AspNetUserRoles]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [sec].[AspNetUsers]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[CodiceFiscale] [varchar](16) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[AspNetUserTokens]    Script Date: 22/03/2021 18:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [adm].[Richieste]  WITH CHECK ADD  CONSTRAINT [FK_Richieste_Iscritti_RichiedenteId] FOREIGN KEY([RichiedenteId])
REFERENCES [adm].[Iscritti] ([Id])
GO
ALTER TABLE [adm].[Richieste] CHECK CONSTRAINT [FK_Richieste_Iscritti_RichiedenteId]
GO
ALTER TABLE [adm].[Richieste]  WITH CHECK ADD  CONSTRAINT [FK_Richieste_Prestazioni_TipologiaId] FOREIGN KEY([TipologiaId])
REFERENCES [adm].[Prestazioni] ([Id])
GO
ALTER TABLE [adm].[Richieste] CHECK CONSTRAINT [FK_Richieste_Prestazioni_TipologiaId]
GO
ALTER TABLE [sec].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [sec].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [sec].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [sec].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [sec].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [sec].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [sec].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [sec].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [sec].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [sec].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [sec].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [sec].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [CassaAssistenza] SET  READ_WRITE 
GO
