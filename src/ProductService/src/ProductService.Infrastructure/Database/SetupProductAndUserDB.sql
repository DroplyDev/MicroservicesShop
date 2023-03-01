USE master
GO

IF DB_ID('UserManagementMicroserviceDB') IS NOT NULL
 DROP DATABASE UserManagementMicroserviceDB;
GO

CREATE DATABASE UserManagementMicroserviceDB;
GO

USE UserManagementMicroserviceDB
	
		CREATE TABLE [Roles]
		(
			[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
			[Name] [varchar](50) NOT NULL INDEX ix_name NONCLUSTERED
		)

		CREATE TABLE [Users]
		(
			[Id] [int] IDENTITY(1,1) PRIMARY KEY CLUSTERED,
			[Email] [varchar](100) NOT NULL INDEX ix_email NONCLUSTERED,
			[EmailConfirmed] bit NOT NULL,
			[PasswordHash] [varchar](50) NOT NULL,
			[TwoFactorEnabled] bit NOT NULL,
			[DeleteDate] DateTime2,
		)

		CREATE TABLE [UserToRoles]
		(
			[UserId] [int] FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
			[RoleId] [int] FOREIGN KEY REFERENCES [Roles](Id) NOT NULL,
			PRIMARY KEY([UserId], [RoleId])
		)

		CREATE TABLE [UserInfos]
		(
			[Firstname] [varchar](50),
			[LastName] [varchar](50),
			[Address] [varchar](50),
			[Phone] [varchar](20),
			[User] [int] PRIMARY KEY FOREIGN KEY REFERENCES  [Users](Id)
			ON DELETE CASCADE
			ON UPDATE CASCADE,
		)

		CREATE TABLE [UserRefreshTokens]
		(
			[User] [int] PRIMARY KEY FOREIGN KEY REFERENCES  [Users](Id)
			ON DELETE CASCADE
			ON UPDATE CASCADE,
			[Token] [varchar](50) NOT NULL,
			[CreationDate] DateTime2 NOT NULL,
			[ExpirationDate] DateTime2 NOT NULL,
			[IsUsed] bit NOT NULL,
			[IsInvalidated] bit NOT NULL,
		)


IF DB_ID('ProductMicroserviceDB') IS NOT NULL
 DROP DATABASE ProductMicroserviceDB
GO

CREATE DATABASE ProductMicroserviceDB
GO

USE ProductMicroserviceDB
	
		CREATE TABLE [Categories]
		(
			[Id] int IDENTITY(1,1) PRIMARY KEY CLUSTERED,
			[Name] varchar(50) INDEX ix_name NONCLUSTERED NOT NULL,
			[Description] varchar(500),
			[Icon] image,
		)

		CREATE TABLE [Products]
		(
			[Id] int IDENTITY(1,1) PRIMARY KEY CLUSTERED,
			[Name] varchar(50) NOT NULL INDEX ix_name NONCLUSTERED,
			[Description] varchar(500),
			[Quantity] int NOT NULL,
			[Price] decimal NOT NULL,
			[Thumbnail] image,
			[CategoryId] [int] FOREIGN KEY REFERENCES [Categories](Id) NOT NULL,
		)

		CREATE TABLE [ProductImages]
		(
			[Id] int IDENTITY(1,1) PRIMARY KEY CLUSTERED,
			[Icon] image NOT NULL,
			[ProductId] [int] FOREIGN KEY REFERENCES [Products](Id) NOT NULL,
		)

