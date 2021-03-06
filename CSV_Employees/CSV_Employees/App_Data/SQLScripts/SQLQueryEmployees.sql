﻿CREATE TABLE [dbo].[Employees](
[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
[Payroll_number] NVARCHAR(6) NOT NULL,
[Forenames] NVARCHAR(30) NULL,
[Surname] NVARCHAR(30) NULL,
[Date_of_Birth] DATETIME NULL,
[Telephone] NVARCHAR(20) NULL,
[Mobile] NVARCHAR(20) NULL,
[Address] NVARCHAR(100) NULL,
[Address_2] NVARCHAR(30) NULL,
[Postcode] NVARCHAR(10) NULL,
[EMail_Home] NVARCHAR(100) NULL,
[Start_Date] DATETIME NULL
)