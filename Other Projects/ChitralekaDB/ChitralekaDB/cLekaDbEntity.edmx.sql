
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/08/2017 12:38:00
-- Generated from EDMX file: D:\Learn Social\Chitralekha\Project\ChitralekaDB\ChitralekaDB\cLekaDbEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ChitraLeka];
GO
IF SCHEMA_ID(N'CL') IS NULL EXECUTE(N'CREATE SCHEMA [CL]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[CL].[FK_SalutationPerson]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Person] DROP CONSTRAINT [FK_SalutationPerson];
GO
IF OBJECT_ID(N'[CL].[FK_AddressTypeAddress]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Address] DROP CONSTRAINT [FK_AddressTypeAddress];
GO
IF OBJECT_ID(N'[CL].[FK_PersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Address] DROP CONSTRAINT [FK_PersonAddress];
GO
IF OBJECT_ID(N'[CL].[FK_ContactContactNumberType]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Contact] DROP CONSTRAINT [FK_ContactContactNumberType];
GO
IF OBJECT_ID(N'[CL].[FK_PersonContact]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Contact] DROP CONSTRAINT [FK_PersonContact];
GO
IF OBJECT_ID(N'[CL].[FK_PersonRegistration]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Registration] DROP CONSTRAINT [FK_PersonRegistration];
GO
IF OBJECT_ID(N'[CL].[FK_EnquiryEnquiryType]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Enquiry] DROP CONSTRAINT [FK_EnquiryEnquiryType];
GO
IF OBJECT_ID(N'[CL].[FK_PersonEnquiry]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Enquiry] DROP CONSTRAINT [FK_PersonEnquiry];
GO
IF OBJECT_ID(N'[CL].[FK_PersonPersonEmail]', 'F') IS NOT NULL
    ALTER TABLE [CL].[PersonEmail] DROP CONSTRAINT [FK_PersonPersonEmail];
GO
IF OBJECT_ID(N'[CL].[FK_PersonEmailEmailAddress]', 'F') IS NOT NULL
    ALTER TABLE [CL].[PersonEmail] DROP CONSTRAINT [FK_PersonEmailEmailAddress];
GO
IF OBJECT_ID(N'[CL].[FK_PersonPersonType]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Person] DROP CONSTRAINT [FK_PersonPersonType];
GO
IF OBJECT_ID(N'[CL].[FK_RegistrationStudent]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Student] DROP CONSTRAINT [FK_RegistrationStudent];
GO
IF OBJECT_ID(N'[CL].[FK_StudentStudentGrade]', 'F') IS NOT NULL
    ALTER TABLE [CL].[StudentGrade] DROP CONSTRAINT [FK_StudentStudentGrade];
GO
IF OBJECT_ID(N'[CL].[FK_StudentGradeGrade]', 'F') IS NOT NULL
    ALTER TABLE [CL].[StudentGrade] DROP CONSTRAINT [FK_StudentGradeGrade];
GO
IF OBJECT_ID(N'[CL].[FK_StudentGradeAcademyCenter]', 'F') IS NOT NULL
    ALTER TABLE [CL].[StudentGrade] DROP CONSTRAINT [FK_StudentGradeAcademyCenter];
GO
IF OBJECT_ID(N'[CL].[FK_RegistrationGrade]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Registration] DROP CONSTRAINT [FK_RegistrationGrade];
GO
IF OBJECT_ID(N'[CL].[FK_PersonLogin]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Login] DROP CONSTRAINT [FK_PersonLogin];
GO
IF OBJECT_ID(N'[CL].[FK_PersonPersonRole]', 'F') IS NOT NULL
    ALTER TABLE [CL].[PersonRole] DROP CONSTRAINT [FK_PersonPersonRole];
GO
IF OBJECT_ID(N'[CL].[FK_PersonRoleRole]', 'F') IS NOT NULL
    ALTER TABLE [CL].[PersonRole] DROP CONSTRAINT [FK_PersonRoleRole];
GO
IF OBJECT_ID(N'[CL].[FK_InvoiceInvoiceType]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Invoice] DROP CONSTRAINT [FK_InvoiceInvoiceType];
GO
IF OBJECT_ID(N'[CL].[FK_PersonInvoice]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Invoice] DROP CONSTRAINT [FK_PersonInvoice];
GO
IF OBJECT_ID(N'[CL].[FK_StudentGradeInvoice]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Invoice] DROP CONSTRAINT [FK_StudentGradeInvoice];
GO
IF OBJECT_ID(N'[CL].[FK_InvoicePayment]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Payment] DROP CONSTRAINT [FK_InvoicePayment];
GO
IF OBJECT_ID(N'[CL].[FK_LogLogType]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Log] DROP CONSTRAINT [FK_LogLogType];
GO
IF OBJECT_ID(N'[CL].[FK_MailoutTypeMailoutTypeParameter]', 'F') IS NOT NULL
    ALTER TABLE [CL].[MailoutTypeParameter] DROP CONSTRAINT [FK_MailoutTypeMailoutTypeParameter];
GO
IF OBJECT_ID(N'[CL].[FK_MailoutParameterMailoutTypeParameter]', 'F') IS NOT NULL
    ALTER TABLE [CL].[MailoutTypeParameter] DROP CONSTRAINT [FK_MailoutParameterMailoutTypeParameter];
GO
IF OBJECT_ID(N'[CL].[FK_PersonMailoutQueue]', 'F') IS NOT NULL
    ALTER TABLE [CL].[MailoutQueue] DROP CONSTRAINT [FK_PersonMailoutQueue];
GO
IF OBJECT_ID(N'[CL].[FK_MailoutQueueMailoutType]', 'F') IS NOT NULL
    ALTER TABLE [CL].[MailoutQueue] DROP CONSTRAINT [FK_MailoutQueueMailoutType];
GO
IF OBJECT_ID(N'[CL].[FK_MailoutQueueMailoutQueueParameterValue]', 'F') IS NOT NULL
    ALTER TABLE [CL].[MailoutQueueParameterValue] DROP CONSTRAINT [FK_MailoutQueueMailoutQueueParameterValue];
GO
IF OBJECT_ID(N'[CL].[FK_MailoutQueueParameterValueMailoutTypeParameter]', 'F') IS NOT NULL
    ALTER TABLE [CL].[MailoutQueueParameterValue] DROP CONSTRAINT [FK_MailoutQueueParameterValueMailoutTypeParameter];
GO
IF OBJECT_ID(N'[CL].[FK_PersonLog]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Log] DROP CONSTRAINT [FK_PersonLog];
GO
IF OBJECT_ID(N'[CL].[FK_GradeTerm]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Calendar] DROP CONSTRAINT [FK_GradeTerm];
GO
IF OBJECT_ID(N'[CL].[FK_TermStudentGrade]', 'F') IS NOT NULL
    ALTER TABLE [CL].[StudentGrade] DROP CONSTRAINT [FK_TermStudentGrade];
GO
IF OBJECT_ID(N'[CL].[FK_StudentGradeCalendar]', 'F') IS NOT NULL
    ALTER TABLE [CL].[StudentGrade] DROP CONSTRAINT [FK_StudentGradeCalendar];
GO
IF OBJECT_ID(N'[CL].[FK_EventTypeCalendar]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Calendar] DROP CONSTRAINT [FK_EventTypeCalendar];
GO
IF OBJECT_ID(N'[CL].[FK_AcademyCenterCalendar]', 'F') IS NOT NULL
    ALTER TABLE [CL].[Calendar] DROP CONSTRAINT [FK_AcademyCenterCalendar];
GO
IF OBJECT_ID(N'[CL].[FK_CalendarCalendarDetail]', 'F') IS NOT NULL
    ALTER TABLE [CL].[CalendarDetail] DROP CONSTRAINT [FK_CalendarCalendarDetail];
GO
IF OBJECT_ID(N'[CL].[FK_MailoutQueueMailoutTracker]', 'F') IS NOT NULL
    ALTER TABLE [CL].[MailoutTracker] DROP CONSTRAINT [FK_MailoutQueueMailoutTracker];
GO
IF OBJECT_ID(N'[CL].[FK_SecurityTypeSecurityCode]', 'F') IS NOT NULL
    ALTER TABLE [CL].[SecurityCode] DROP CONSTRAINT [FK_SecurityTypeSecurityCode];
GO
IF OBJECT_ID(N'[CL].[FK_PersonSecurityCode]', 'F') IS NOT NULL
    ALTER TABLE [CL].[SecurityCode] DROP CONSTRAINT [FK_PersonSecurityCode];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[CL].[LogType]', 'U') IS NOT NULL
    DROP TABLE [CL].[LogType];
GO
IF OBJECT_ID(N'[CL].[Country]', 'U') IS NOT NULL
    DROP TABLE [CL].[Country];
GO
IF OBJECT_ID(N'[CL].[MailoutType]', 'U') IS NOT NULL
    DROP TABLE [CL].[MailoutType];
GO
IF OBJECT_ID(N'[CL].[Salutation]', 'U') IS NOT NULL
    DROP TABLE [CL].[Salutation];
GO
IF OBJECT_ID(N'[CL].[Enquiry]', 'U') IS NOT NULL
    DROP TABLE [CL].[Enquiry];
GO
IF OBJECT_ID(N'[CL].[EmailAddress]', 'U') IS NOT NULL
    DROP TABLE [CL].[EmailAddress];
GO
IF OBJECT_ID(N'[CL].[Person]', 'U') IS NOT NULL
    DROP TABLE [CL].[Person];
GO
IF OBJECT_ID(N'[CL].[Registration]', 'U') IS NOT NULL
    DROP TABLE [CL].[Registration];
GO
IF OBJECT_ID(N'[CL].[Student]', 'U') IS NOT NULL
    DROP TABLE [CL].[Student];
GO
IF OBJECT_ID(N'[CL].[AcademyCenter]', 'U') IS NOT NULL
    DROP TABLE [CL].[AcademyCenter];
GO
IF OBJECT_ID(N'[CL].[Grade]', 'U') IS NOT NULL
    DROP TABLE [CL].[Grade];
GO
IF OBJECT_ID(N'[CL].[Invoice]', 'U') IS NOT NULL
    DROP TABLE [CL].[Invoice];
GO
IF OBJECT_ID(N'[CL].[Payment]', 'U') IS NOT NULL
    DROP TABLE [CL].[Payment];
GO
IF OBJECT_ID(N'[CL].[Key]', 'U') IS NOT NULL
    DROP TABLE [CL].[Key];
GO
IF OBJECT_ID(N'[CL].[Address]', 'U') IS NOT NULL
    DROP TABLE [CL].[Address];
GO
IF OBJECT_ID(N'[CL].[AddressType]', 'U') IS NOT NULL
    DROP TABLE [CL].[AddressType];
GO
IF OBJECT_ID(N'[CL].[ContactNumberType]', 'U') IS NOT NULL
    DROP TABLE [CL].[ContactNumberType];
GO
IF OBJECT_ID(N'[CL].[Contact]', 'U') IS NOT NULL
    DROP TABLE [CL].[Contact];
GO
IF OBJECT_ID(N'[CL].[Sample]', 'U') IS NOT NULL
    DROP TABLE [CL].[Sample];
GO
IF OBJECT_ID(N'[CL].[PersonType]', 'U') IS NOT NULL
    DROP TABLE [CL].[PersonType];
GO
IF OBJECT_ID(N'[CL].[EnquiryType]', 'U') IS NOT NULL
    DROP TABLE [CL].[EnquiryType];
GO
IF OBJECT_ID(N'[CL].[PersonEmail]', 'U') IS NOT NULL
    DROP TABLE [CL].[PersonEmail];
GO
IF OBJECT_ID(N'[CL].[StudentGrade]', 'U') IS NOT NULL
    DROP TABLE [CL].[StudentGrade];
GO
IF OBJECT_ID(N'[CL].[Role]', 'U') IS NOT NULL
    DROP TABLE [CL].[Role];
GO
IF OBJECT_ID(N'[CL].[Login]', 'U') IS NOT NULL
    DROP TABLE [CL].[Login];
GO
IF OBJECT_ID(N'[CL].[PersonRole]', 'U') IS NOT NULL
    DROP TABLE [CL].[PersonRole];
GO
IF OBJECT_ID(N'[CL].[InvoiceType]', 'U') IS NOT NULL
    DROP TABLE [CL].[InvoiceType];
GO
IF OBJECT_ID(N'[CL].[Log]', 'U') IS NOT NULL
    DROP TABLE [CL].[Log];
GO
IF OBJECT_ID(N'[CL].[MailoutParameter]', 'U') IS NOT NULL
    DROP TABLE [CL].[MailoutParameter];
GO
IF OBJECT_ID(N'[CL].[MailoutTypeParameter]', 'U') IS NOT NULL
    DROP TABLE [CL].[MailoutTypeParameter];
GO
IF OBJECT_ID(N'[CL].[MailoutQueue]', 'U') IS NOT NULL
    DROP TABLE [CL].[MailoutQueue];
GO
IF OBJECT_ID(N'[CL].[MailoutQueueParameterValue]', 'U') IS NOT NULL
    DROP TABLE [CL].[MailoutQueueParameterValue];
GO
IF OBJECT_ID(N'[CL].[Calendar]', 'U') IS NOT NULL
    DROP TABLE [CL].[Calendar];
GO
IF OBJECT_ID(N'[CL].[EventType]', 'U') IS NOT NULL
    DROP TABLE [CL].[EventType];
GO
IF OBJECT_ID(N'[CL].[CalendarDetail]', 'U') IS NOT NULL
    DROP TABLE [CL].[CalendarDetail];
GO
IF OBJECT_ID(N'[CL].[MailoutTracker]', 'U') IS NOT NULL
    DROP TABLE [CL].[MailoutTracker];
GO
IF OBJECT_ID(N'[CL].[SecurityType]', 'U') IS NOT NULL
    DROP TABLE [CL].[SecurityType];
GO
IF OBJECT_ID(N'[CL].[SecurityCode]', 'U') IS NOT NULL
    DROP TABLE [CL].[SecurityCode];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LogType'
CREATE TABLE [CL].[LogType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'Country'
CREATE TABLE [CL].[Country] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(3)  NOT NULL,
    [EnglishName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'MailoutType'
CREATE TABLE [CL].[MailoutType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [Subject] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'Salutation'
CREATE TABLE [CL].[Salutation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(20)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Enquiry'
CREATE TABLE [CL].[Enquiry] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EnquiryTypeId] int  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [IsOpen] bit  NOT NULL,
    [PersonId] int  NOT NULL
);
GO

-- Creating table 'EmailAddress'
CREATE TABLE [CL].[EmailAddress] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(50)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Person'
CREATE TABLE [CL].[Person] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SalutationId] int  NOT NULL,
    [FName] nvarchar(50)  NOT NULL,
    [MName] nvarchar(50)  NULL,
    [LName] nvarchar(50)  NOT NULL,
    [Gender] nvarchar(6)  NULL,
    [DOB] datetime  NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [GroupId] bigint  NOT NULL,
    [FatherId] smallint  NULL,
    [MotherId] smallint  NULL,
    [PersonTypeId] int  NOT NULL
);
GO

-- Creating table 'Registration'
CREATE TABLE [CL].[Registration] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [IsComplete] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [Remark] nvarchar(max)  NULL,
    [GradeId] int  NOT NULL,
    [Person_Id] int  NOT NULL
);
GO

-- Creating table 'Student'
CREATE TABLE [CL].[Student] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DateOfAdmission] datetime  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [RegistrationId] int  NOT NULL
);
GO

-- Creating table 'AcademyCenter'
CREATE TABLE [CL].[AcademyCenter] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(4)  NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Grade'
CREATE TABLE [CL].[Grade] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'Invoice'
CREATE TABLE [CL].[Invoice] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InvoiceTypeId] int  NOT NULL,
    [InvoiceNumber] nvarchar(20)  NOT NULL,
    [InvoiceDate] datetime  NOT NULL,
    [DueDate] datetime  NOT NULL,
    [IsDue] bit  NOT NULL,
    [InvoiceAmount] decimal(18,4)  NOT NULL,
    [Note] nvarchar(200)  NULL,
    [PersonId] int  NOT NULL,
    [StudentGrade_Id] int  NOT NULL
);
GO

-- Creating table 'Payment'
CREATE TABLE [CL].[Payment] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [InvoiceId] int  NOT NULL,
    [DueAmount] decimal(18,4)  NOT NULL,
    [PaidAmount] decimal(18,4)  NOT NULL,
    [BalanceAmount] decimal(18,4)  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [IsSettled] bit  NOT NULL,
    [SettleDate] datetime  NULL,
    [Note] nvarchar(500)  NULL
);
GO

-- Creating table 'Key'
CREATE TABLE [CL].[Key] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'Address'
CREATE TABLE [CL].[Address] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Line1] nvarchar(200)  NOT NULL,
    [Line2] nvarchar(max)  NULL,
    [City] nvarchar(50)  NOT NULL,
    [State] nvarchar(50)  NOT NULL,
    [Landmark] nvarchar(max)  NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [AddressTypeId] int  NOT NULL,
    [PostCode] nvarchar(10)  NOT NULL,
    [PersonId] int  NOT NULL
);
GO

-- Creating table 'AddressType'
CREATE TABLE [CL].[AddressType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(10)  NOT NULL,
    [Description] nvarchar(200)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'ContactNumberType'
CREATE TABLE [CL].[ContactNumberType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(200)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL
);
GO

-- Creating table 'Contact'
CREATE TABLE [CL].[Contact] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Value] nvarchar(20)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [ContactNumberTypeId] int  NOT NULL,
    [PersonId] int  NOT NULL
);
GO

-- Creating table 'Sample'
CREATE TABLE [CL].[Sample] (
    [Venue] varchar(50)  NULL,
    [Day] varchar(50)  NULL,
    [Time] varchar(50)  NULL,
    [Total_weeks] varchar(50)  NULL,
    [Unit_Fees] varchar(50)  NULL,
    [Fees] varchar(50)  NULL,
    [Lookup_reference] varchar(50)  NULL,
    [Registration_Date] varchar(50)  NULL,
    [Grade] varchar(50)  NULL,
    [First_Name] varchar(50)  NULL,
    [Last_Name] varchar(50)  NULL,
    [Date_Of_Birth] varchar(50)  NULL,
    [Age] varchar(50)  NULL,
    [Telephone_1] varchar(50)  NULL,
    [Telephone_2] varchar(50)  NULL,
    [Parent_Telephone] varchar(50)  NULL,
    [Email_1] varchar(50)  NULL,
    [Email_2] varchar(50)  NULL,
    [Address_1] varchar(50)  NULL,
    [Address_2] varchar(50)  NULL,
    [Town] varchar(50)  NULL,
    [Postcode] varchar(50)  NULL,
    [Parent_Name] varchar(50)  NULL,
    [Column_23] varchar(50)  NULL,
    [Column_24] varchar(50)  NULL,
    [Column_25] varchar(50)  NULL,
    [Column_26] varchar(50)  NULL,
    [Column_27] varchar(50)  NULL,
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'PersonType'
CREATE TABLE [CL].[PersonType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CretaedOn] datetime  NOT NULL
);
GO

-- Creating table 'EnquiryType'
CREATE TABLE [CL].[EnquiryType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL
);
GO

-- Creating table 'PersonEmail'
CREATE TABLE [CL].[PersonEmail] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [EmailAddressId] int  NOT NULL,
    [IsPrimary] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'StudentGrade'
CREATE TABLE [CL].[StudentGrade] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StudentId] int  NOT NULL,
    [GradeId] int  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [IsCurrent] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [AcademyCenterId] int  NOT NULL,
    [CalendarId] int  NOT NULL,
    [TermStudentGrade_StudentGrade_Id] int  NOT NULL
);
GO

-- Creating table 'Role'
CREATE TABLE [CL].[Role] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL
);
GO

-- Creating table 'Login'
CREATE TABLE [CL].[Login] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(15)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [LockedOn] datetime  NULL,
    [IsActive] bit  NOT NULL,
    [IsLock] bit  NOT NULL,
    [MustChangepassword] bit  NOT NULL,
    [Person_Id] int  NOT NULL
);
GO

-- Creating table 'PersonRole'
CREATE TABLE [CL].[PersonRole] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PersonId] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [Role_Id] int  NOT NULL
);
GO

-- Creating table 'InvoiceType'
CREATE TABLE [CL].[InvoiceType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(10)  NOT NULL,
    [Description] nvarchar(200)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'Log'
CREATE TABLE [CL].[Log] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LogTypeId] int  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [PersonId] int  NULL
);
GO

-- Creating table 'MailoutParameter'
CREATE TABLE [CL].[MailoutParameter] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(200)  NOT NULL
);
GO

-- Creating table 'MailoutTypeParameter'
CREATE TABLE [CL].[MailoutTypeParameter] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsActive] bit  NOT NULL,
    [MailoutTypeId] int  NOT NULL,
    [MailoutParameterId] int  NOT NULL
);
GO

-- Creating table 'MailoutQueue'
CREATE TABLE [CL].[MailoutQueue] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] smallint  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [UpdateOn] datetime  NULL,
    [EmailAddress] nvarchar(200)  NOT NULL,
    [PersonId] int  NOT NULL,
    [HtmlBody] nvarchar(max)  NOT NULL,
    [MailoutTypeId] int  NOT NULL
);
GO

-- Creating table 'MailoutQueueParameterValue'
CREATE TABLE [CL].[MailoutQueueParameterValue] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MailoutQueueId] int  NOT NULL,
    [Value] nvarchar(100)  NOT NULL,
    [MailoutTypeParameterId] int  NULL
);
GO

-- Creating table 'Calendar'
CREATE TABLE [CL].[Calendar] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [WeeksCount] smallint  NOT NULL,
    [DaysCount] smallint  NOT NULL,
    [UnitPrice] decimal(18,4)  NOT NULL,
    [Fee] decimal(18,4)  NOT NULL,
    [Remarks] nvarchar(200)  NULL,
    [GradeId] int  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [HasLapsed] varbinary(max)  NOT NULL,
    [IsActive] varbinary(max)  NOT NULL,
    [EventTypeId] int  NOT NULL,
    [AcademyCenterId] int  NOT NULL
);
GO

-- Creating table 'EventType'
CREATE TABLE [CL].[EventType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NOT NULL,
    [Comments] nvarchar(max)  NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'CalendarDetail'
CREATE TABLE [CL].[CalendarDetail] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CalendarId] int  NOT NULL,
    [DueOn] datetime  NOT NULL,
    [HasLapsed] bit  NOT NULL,
    [IsActive] bit  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ModifiedOn] datetime  NULL,
    [Comments] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'MailoutTracker'
CREATE TABLE [CL].[MailoutTracker] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [MailoutQueueId] int  NOT NULL,
    [IPAddress] nvarchar(50)  NOT NULL,
    [OpenedOn] datetime  NOT NULL
);
GO

-- Creating table 'SecurityType'
CREATE TABLE [CL].[SecurityType] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'SecurityCode'
CREATE TABLE [CL].[SecurityCode] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SecurityTypeId] int  NOT NULL,
    [PersonId] int  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [ExpiredOn] datetime  NULL,
    [Code] nvarchar(20)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LogType'
ALTER TABLE [CL].[LogType]
ADD CONSTRAINT [PK_LogType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Country'
ALTER TABLE [CL].[Country]
ADD CONSTRAINT [PK_Country]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MailoutType'
ALTER TABLE [CL].[MailoutType]
ADD CONSTRAINT [PK_MailoutType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Salutation'
ALTER TABLE [CL].[Salutation]
ADD CONSTRAINT [PK_Salutation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Enquiry'
ALTER TABLE [CL].[Enquiry]
ADD CONSTRAINT [PK_Enquiry]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailAddress'
ALTER TABLE [CL].[EmailAddress]
ADD CONSTRAINT [PK_EmailAddress]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Person'
ALTER TABLE [CL].[Person]
ADD CONSTRAINT [PK_Person]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Registration'
ALTER TABLE [CL].[Registration]
ADD CONSTRAINT [PK_Registration]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Student'
ALTER TABLE [CL].[Student]
ADD CONSTRAINT [PK_Student]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AcademyCenter'
ALTER TABLE [CL].[AcademyCenter]
ADD CONSTRAINT [PK_AcademyCenter]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Grade'
ALTER TABLE [CL].[Grade]
ADD CONSTRAINT [PK_Grade]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Invoice'
ALTER TABLE [CL].[Invoice]
ADD CONSTRAINT [PK_Invoice]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payment'
ALTER TABLE [CL].[Payment]
ADD CONSTRAINT [PK_Payment]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Key'
ALTER TABLE [CL].[Key]
ADD CONSTRAINT [PK_Key]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Address'
ALTER TABLE [CL].[Address]
ADD CONSTRAINT [PK_Address]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AddressType'
ALTER TABLE [CL].[AddressType]
ADD CONSTRAINT [PK_AddressType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ContactNumberType'
ALTER TABLE [CL].[ContactNumberType]
ADD CONSTRAINT [PK_ContactNumberType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contact'
ALTER TABLE [CL].[Contact]
ADD CONSTRAINT [PK_Contact]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sample'
ALTER TABLE [CL].[Sample]
ADD CONSTRAINT [PK_Sample]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonType'
ALTER TABLE [CL].[PersonType]
ADD CONSTRAINT [PK_PersonType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnquiryType'
ALTER TABLE [CL].[EnquiryType]
ADD CONSTRAINT [PK_EnquiryType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonEmail'
ALTER TABLE [CL].[PersonEmail]
ADD CONSTRAINT [PK_PersonEmail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StudentGrade'
ALTER TABLE [CL].[StudentGrade]
ADD CONSTRAINT [PK_StudentGrade]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Role'
ALTER TABLE [CL].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Login'
ALTER TABLE [CL].[Login]
ADD CONSTRAINT [PK_Login]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PersonRole'
ALTER TABLE [CL].[PersonRole]
ADD CONSTRAINT [PK_PersonRole]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InvoiceType'
ALTER TABLE [CL].[InvoiceType]
ADD CONSTRAINT [PK_InvoiceType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Log'
ALTER TABLE [CL].[Log]
ADD CONSTRAINT [PK_Log]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MailoutParameter'
ALTER TABLE [CL].[MailoutParameter]
ADD CONSTRAINT [PK_MailoutParameter]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MailoutTypeParameter'
ALTER TABLE [CL].[MailoutTypeParameter]
ADD CONSTRAINT [PK_MailoutTypeParameter]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MailoutQueue'
ALTER TABLE [CL].[MailoutQueue]
ADD CONSTRAINT [PK_MailoutQueue]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MailoutQueueParameterValue'
ALTER TABLE [CL].[MailoutQueueParameterValue]
ADD CONSTRAINT [PK_MailoutQueueParameterValue]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Calendar'
ALTER TABLE [CL].[Calendar]
ADD CONSTRAINT [PK_Calendar]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventType'
ALTER TABLE [CL].[EventType]
ADD CONSTRAINT [PK_EventType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CalendarDetail'
ALTER TABLE [CL].[CalendarDetail]
ADD CONSTRAINT [PK_CalendarDetail]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MailoutTracker'
ALTER TABLE [CL].[MailoutTracker]
ADD CONSTRAINT [PK_MailoutTracker]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SecurityType'
ALTER TABLE [CL].[SecurityType]
ADD CONSTRAINT [PK_SecurityType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SecurityCode'
ALTER TABLE [CL].[SecurityCode]
ADD CONSTRAINT [PK_SecurityCode]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SalutationId] in table 'Person'
ALTER TABLE [CL].[Person]
ADD CONSTRAINT [FK_SalutationPerson]
    FOREIGN KEY ([SalutationId])
    REFERENCES [CL].[Salutation]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SalutationPerson'
CREATE INDEX [IX_FK_SalutationPerson]
ON [CL].[Person]
    ([SalutationId]);
GO

-- Creating foreign key on [AddressTypeId] in table 'Address'
ALTER TABLE [CL].[Address]
ADD CONSTRAINT [FK_AddressTypeAddress]
    FOREIGN KEY ([AddressTypeId])
    REFERENCES [CL].[AddressType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressTypeAddress'
CREATE INDEX [IX_FK_AddressTypeAddress]
ON [CL].[Address]
    ([AddressTypeId]);
GO

-- Creating foreign key on [PersonId] in table 'Address'
ALTER TABLE [CL].[Address]
ADD CONSTRAINT [FK_PersonAddress]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonAddress'
CREATE INDEX [IX_FK_PersonAddress]
ON [CL].[Address]
    ([PersonId]);
GO

-- Creating foreign key on [ContactNumberTypeId] in table 'Contact'
ALTER TABLE [CL].[Contact]
ADD CONSTRAINT [FK_ContactContactNumberType]
    FOREIGN KEY ([ContactNumberTypeId])
    REFERENCES [CL].[ContactNumberType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactContactNumberType'
CREATE INDEX [IX_FK_ContactContactNumberType]
ON [CL].[Contact]
    ([ContactNumberTypeId]);
GO

-- Creating foreign key on [PersonId] in table 'Contact'
ALTER TABLE [CL].[Contact]
ADD CONSTRAINT [FK_PersonContact]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonContact'
CREATE INDEX [IX_FK_PersonContact]
ON [CL].[Contact]
    ([PersonId]);
GO

-- Creating foreign key on [Person_Id] in table 'Registration'
ALTER TABLE [CL].[Registration]
ADD CONSTRAINT [FK_PersonRegistration]
    FOREIGN KEY ([Person_Id])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonRegistration'
CREATE INDEX [IX_FK_PersonRegistration]
ON [CL].[Registration]
    ([Person_Id]);
GO

-- Creating foreign key on [EnquiryTypeId] in table 'Enquiry'
ALTER TABLE [CL].[Enquiry]
ADD CONSTRAINT [FK_EnquiryEnquiryType]
    FOREIGN KEY ([EnquiryTypeId])
    REFERENCES [CL].[EnquiryType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EnquiryEnquiryType'
CREATE INDEX [IX_FK_EnquiryEnquiryType]
ON [CL].[Enquiry]
    ([EnquiryTypeId]);
GO

-- Creating foreign key on [PersonId] in table 'Enquiry'
ALTER TABLE [CL].[Enquiry]
ADD CONSTRAINT [FK_PersonEnquiry]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEnquiry'
CREATE INDEX [IX_FK_PersonEnquiry]
ON [CL].[Enquiry]
    ([PersonId]);
GO

-- Creating foreign key on [PersonId] in table 'PersonEmail'
ALTER TABLE [CL].[PersonEmail]
ADD CONSTRAINT [FK_PersonPersonEmail]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPersonEmail'
CREATE INDEX [IX_FK_PersonPersonEmail]
ON [CL].[PersonEmail]
    ([PersonId]);
GO

-- Creating foreign key on [EmailAddressId] in table 'PersonEmail'
ALTER TABLE [CL].[PersonEmail]
ADD CONSTRAINT [FK_PersonEmailEmailAddress]
    FOREIGN KEY ([EmailAddressId])
    REFERENCES [CL].[EmailAddress]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonEmailEmailAddress'
CREATE INDEX [IX_FK_PersonEmailEmailAddress]
ON [CL].[PersonEmail]
    ([EmailAddressId]);
GO

-- Creating foreign key on [PersonTypeId] in table 'Person'
ALTER TABLE [CL].[Person]
ADD CONSTRAINT [FK_PersonPersonType]
    FOREIGN KEY ([PersonTypeId])
    REFERENCES [CL].[PersonType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPersonType'
CREATE INDEX [IX_FK_PersonPersonType]
ON [CL].[Person]
    ([PersonTypeId]);
GO

-- Creating foreign key on [RegistrationId] in table 'Student'
ALTER TABLE [CL].[Student]
ADD CONSTRAINT [FK_RegistrationStudent]
    FOREIGN KEY ([RegistrationId])
    REFERENCES [CL].[Registration]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegistrationStudent'
CREATE INDEX [IX_FK_RegistrationStudent]
ON [CL].[Student]
    ([RegistrationId]);
GO

-- Creating foreign key on [StudentId] in table 'StudentGrade'
ALTER TABLE [CL].[StudentGrade]
ADD CONSTRAINT [FK_StudentStudentGrade]
    FOREIGN KEY ([StudentId])
    REFERENCES [CL].[Student]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentStudentGrade'
CREATE INDEX [IX_FK_StudentStudentGrade]
ON [CL].[StudentGrade]
    ([StudentId]);
GO

-- Creating foreign key on [GradeId] in table 'StudentGrade'
ALTER TABLE [CL].[StudentGrade]
ADD CONSTRAINT [FK_StudentGradeGrade]
    FOREIGN KEY ([GradeId])
    REFERENCES [CL].[Grade]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentGradeGrade'
CREATE INDEX [IX_FK_StudentGradeGrade]
ON [CL].[StudentGrade]
    ([GradeId]);
GO

-- Creating foreign key on [AcademyCenterId] in table 'StudentGrade'
ALTER TABLE [CL].[StudentGrade]
ADD CONSTRAINT [FK_StudentGradeAcademyCenter]
    FOREIGN KEY ([AcademyCenterId])
    REFERENCES [CL].[AcademyCenter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentGradeAcademyCenter'
CREATE INDEX [IX_FK_StudentGradeAcademyCenter]
ON [CL].[StudentGrade]
    ([AcademyCenterId]);
GO

-- Creating foreign key on [GradeId] in table 'Registration'
ALTER TABLE [CL].[Registration]
ADD CONSTRAINT [FK_RegistrationGrade]
    FOREIGN KEY ([GradeId])
    REFERENCES [CL].[Grade]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RegistrationGrade'
CREATE INDEX [IX_FK_RegistrationGrade]
ON [CL].[Registration]
    ([GradeId]);
GO

-- Creating foreign key on [Person_Id] in table 'Login'
ALTER TABLE [CL].[Login]
ADD CONSTRAINT [FK_PersonLogin]
    FOREIGN KEY ([Person_Id])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonLogin'
CREATE INDEX [IX_FK_PersonLogin]
ON [CL].[Login]
    ([Person_Id]);
GO

-- Creating foreign key on [PersonId] in table 'PersonRole'
ALTER TABLE [CL].[PersonRole]
ADD CONSTRAINT [FK_PersonPersonRole]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonPersonRole'
CREATE INDEX [IX_FK_PersonPersonRole]
ON [CL].[PersonRole]
    ([PersonId]);
GO

-- Creating foreign key on [Role_Id] in table 'PersonRole'
ALTER TABLE [CL].[PersonRole]
ADD CONSTRAINT [FK_PersonRoleRole]
    FOREIGN KEY ([Role_Id])
    REFERENCES [CL].[Role]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonRoleRole'
CREATE INDEX [IX_FK_PersonRoleRole]
ON [CL].[PersonRole]
    ([Role_Id]);
GO

-- Creating foreign key on [InvoiceTypeId] in table 'Invoice'
ALTER TABLE [CL].[Invoice]
ADD CONSTRAINT [FK_InvoiceInvoiceType]
    FOREIGN KEY ([InvoiceTypeId])
    REFERENCES [CL].[InvoiceType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceInvoiceType'
CREATE INDEX [IX_FK_InvoiceInvoiceType]
ON [CL].[Invoice]
    ([InvoiceTypeId]);
GO

-- Creating foreign key on [PersonId] in table 'Invoice'
ALTER TABLE [CL].[Invoice]
ADD CONSTRAINT [FK_PersonInvoice]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonInvoice'
CREATE INDEX [IX_FK_PersonInvoice]
ON [CL].[Invoice]
    ([PersonId]);
GO

-- Creating foreign key on [StudentGrade_Id] in table 'Invoice'
ALTER TABLE [CL].[Invoice]
ADD CONSTRAINT [FK_StudentGradeInvoice]
    FOREIGN KEY ([StudentGrade_Id])
    REFERENCES [CL].[StudentGrade]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentGradeInvoice'
CREATE INDEX [IX_FK_StudentGradeInvoice]
ON [CL].[Invoice]
    ([StudentGrade_Id]);
GO

-- Creating foreign key on [InvoiceId] in table 'Payment'
ALTER TABLE [CL].[Payment]
ADD CONSTRAINT [FK_InvoicePayment]
    FOREIGN KEY ([InvoiceId])
    REFERENCES [CL].[Invoice]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoicePayment'
CREATE INDEX [IX_FK_InvoicePayment]
ON [CL].[Payment]
    ([InvoiceId]);
GO

-- Creating foreign key on [LogTypeId] in table 'Log'
ALTER TABLE [CL].[Log]
ADD CONSTRAINT [FK_LogLogType]
    FOREIGN KEY ([LogTypeId])
    REFERENCES [CL].[LogType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LogLogType'
CREATE INDEX [IX_FK_LogLogType]
ON [CL].[Log]
    ([LogTypeId]);
GO

-- Creating foreign key on [MailoutTypeId] in table 'MailoutTypeParameter'
ALTER TABLE [CL].[MailoutTypeParameter]
ADD CONSTRAINT [FK_MailoutTypeMailoutTypeParameter]
    FOREIGN KEY ([MailoutTypeId])
    REFERENCES [CL].[MailoutType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MailoutTypeMailoutTypeParameter'
CREATE INDEX [IX_FK_MailoutTypeMailoutTypeParameter]
ON [CL].[MailoutTypeParameter]
    ([MailoutTypeId]);
GO

-- Creating foreign key on [MailoutParameterId] in table 'MailoutTypeParameter'
ALTER TABLE [CL].[MailoutTypeParameter]
ADD CONSTRAINT [FK_MailoutParameterMailoutTypeParameter]
    FOREIGN KEY ([MailoutParameterId])
    REFERENCES [CL].[MailoutParameter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MailoutParameterMailoutTypeParameter'
CREATE INDEX [IX_FK_MailoutParameterMailoutTypeParameter]
ON [CL].[MailoutTypeParameter]
    ([MailoutParameterId]);
GO

-- Creating foreign key on [PersonId] in table 'MailoutQueue'
ALTER TABLE [CL].[MailoutQueue]
ADD CONSTRAINT [FK_PersonMailoutQueue]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonMailoutQueue'
CREATE INDEX [IX_FK_PersonMailoutQueue]
ON [CL].[MailoutQueue]
    ([PersonId]);
GO

-- Creating foreign key on [MailoutTypeId] in table 'MailoutQueue'
ALTER TABLE [CL].[MailoutQueue]
ADD CONSTRAINT [FK_MailoutQueueMailoutType]
    FOREIGN KEY ([MailoutTypeId])
    REFERENCES [CL].[MailoutType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MailoutQueueMailoutType'
CREATE INDEX [IX_FK_MailoutQueueMailoutType]
ON [CL].[MailoutQueue]
    ([MailoutTypeId]);
GO

-- Creating foreign key on [MailoutQueueId] in table 'MailoutQueueParameterValue'
ALTER TABLE [CL].[MailoutQueueParameterValue]
ADD CONSTRAINT [FK_MailoutQueueMailoutQueueParameterValue]
    FOREIGN KEY ([MailoutQueueId])
    REFERENCES [CL].[MailoutQueue]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MailoutQueueMailoutQueueParameterValue'
CREATE INDEX [IX_FK_MailoutQueueMailoutQueueParameterValue]
ON [CL].[MailoutQueueParameterValue]
    ([MailoutQueueId]);
GO

-- Creating foreign key on [MailoutTypeParameterId] in table 'MailoutQueueParameterValue'
ALTER TABLE [CL].[MailoutQueueParameterValue]
ADD CONSTRAINT [FK_MailoutQueueParameterValueMailoutTypeParameter]
    FOREIGN KEY ([MailoutTypeParameterId])
    REFERENCES [CL].[MailoutTypeParameter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MailoutQueueParameterValueMailoutTypeParameter'
CREATE INDEX [IX_FK_MailoutQueueParameterValueMailoutTypeParameter]
ON [CL].[MailoutQueueParameterValue]
    ([MailoutTypeParameterId]);
GO

-- Creating foreign key on [PersonId] in table 'Log'
ALTER TABLE [CL].[Log]
ADD CONSTRAINT [FK_PersonLog]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonLog'
CREATE INDEX [IX_FK_PersonLog]
ON [CL].[Log]
    ([PersonId]);
GO

-- Creating foreign key on [GradeId] in table 'Calendar'
ALTER TABLE [CL].[Calendar]
ADD CONSTRAINT [FK_GradeTerm]
    FOREIGN KEY ([GradeId])
    REFERENCES [CL].[Grade]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GradeTerm'
CREATE INDEX [IX_FK_GradeTerm]
ON [CL].[Calendar]
    ([GradeId]);
GO

-- Creating foreign key on [TermStudentGrade_StudentGrade_Id] in table 'StudentGrade'
ALTER TABLE [CL].[StudentGrade]
ADD CONSTRAINT [FK_TermStudentGrade]
    FOREIGN KEY ([TermStudentGrade_StudentGrade_Id])
    REFERENCES [CL].[Calendar]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TermStudentGrade'
CREATE INDEX [IX_FK_TermStudentGrade]
ON [CL].[StudentGrade]
    ([TermStudentGrade_StudentGrade_Id]);
GO

-- Creating foreign key on [CalendarId] in table 'StudentGrade'
ALTER TABLE [CL].[StudentGrade]
ADD CONSTRAINT [FK_StudentGradeCalendar]
    FOREIGN KEY ([CalendarId])
    REFERENCES [CL].[Calendar]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StudentGradeCalendar'
CREATE INDEX [IX_FK_StudentGradeCalendar]
ON [CL].[StudentGrade]
    ([CalendarId]);
GO

-- Creating foreign key on [EventTypeId] in table 'Calendar'
ALTER TABLE [CL].[Calendar]
ADD CONSTRAINT [FK_EventTypeCalendar]
    FOREIGN KEY ([EventTypeId])
    REFERENCES [CL].[EventType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventTypeCalendar'
CREATE INDEX [IX_FK_EventTypeCalendar]
ON [CL].[Calendar]
    ([EventTypeId]);
GO

-- Creating foreign key on [AcademyCenterId] in table 'Calendar'
ALTER TABLE [CL].[Calendar]
ADD CONSTRAINT [FK_AcademyCenterCalendar]
    FOREIGN KEY ([AcademyCenterId])
    REFERENCES [CL].[AcademyCenter]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AcademyCenterCalendar'
CREATE INDEX [IX_FK_AcademyCenterCalendar]
ON [CL].[Calendar]
    ([AcademyCenterId]);
GO

-- Creating foreign key on [CalendarId] in table 'CalendarDetail'
ALTER TABLE [CL].[CalendarDetail]
ADD CONSTRAINT [FK_CalendarCalendarDetail]
    FOREIGN KEY ([CalendarId])
    REFERENCES [CL].[Calendar]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CalendarCalendarDetail'
CREATE INDEX [IX_FK_CalendarCalendarDetail]
ON [CL].[CalendarDetail]
    ([CalendarId]);
GO

-- Creating foreign key on [MailoutQueueId] in table 'MailoutTracker'
ALTER TABLE [CL].[MailoutTracker]
ADD CONSTRAINT [FK_MailoutQueueMailoutTracker]
    FOREIGN KEY ([MailoutQueueId])
    REFERENCES [CL].[MailoutQueue]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MailoutQueueMailoutTracker'
CREATE INDEX [IX_FK_MailoutQueueMailoutTracker]
ON [CL].[MailoutTracker]
    ([MailoutQueueId]);
GO

-- Creating foreign key on [SecurityTypeId] in table 'SecurityCode'
ALTER TABLE [CL].[SecurityCode]
ADD CONSTRAINT [FK_SecurityTypeSecurityCode]
    FOREIGN KEY ([SecurityTypeId])
    REFERENCES [CL].[SecurityType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SecurityTypeSecurityCode'
CREATE INDEX [IX_FK_SecurityTypeSecurityCode]
ON [CL].[SecurityCode]
    ([SecurityTypeId]);
GO

-- Creating foreign key on [PersonId] in table 'SecurityCode'
ALTER TABLE [CL].[SecurityCode]
ADD CONSTRAINT [FK_PersonSecurityCode]
    FOREIGN KEY ([PersonId])
    REFERENCES [CL].[Person]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonSecurityCode'
CREATE INDEX [IX_FK_PersonSecurityCode]
ON [CL].[SecurityCode]
    ([PersonId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------