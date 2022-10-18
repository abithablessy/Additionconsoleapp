USE [master]
GO
/****** Object:  Database [db_Savedatacalc]    Script Date: 17-10-2022 20:08:29 ******/
CREATE DATABASE [db_Savedatacalc]
GO
USE [db_Savedatacalc]
GO
/****** Object:  Table [dbo].[tb_addcalc]    Script Date: 17-10-2022 20:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_addcalc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Arg1] [float] NOT NULL,
	[Arg2] [float] NOT NULL,
	[Result] [float] NOT NULL,
	[Createddate] [datetime] NULL,
 CONSTRAINT [PK_tb_addcalc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_errors]    Script Date: 17-10-2022 20:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_errors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Errornumber] [int] NULL,
	[Errorstate] [int] NULL,
	[Errorseverity] [int] NULL,
	[Errorline] [int] NULL,
	[Errorprocedure] [varchar](max) NULL,
	[Errormessage] [varchar](max) NULL,
	[Datetime] [datetime] NULL,
 CONSTRAINT [PK_tb_errors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Checkdataexist]    Script Date: 17-10-2022 20:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Checkdataexist](@arg1 float,@arg2 float)
As
Begin
Begin try
Select count(*) from dbo.tb_addcalc where Arg1=@arg1 and Arg2 =@arg2
End try
Begin catch
Insert into dbo.tb_errors
values
(ERROR_NUMBER(),
   ERROR_STATE(),
   ERROR_SEVERITY(),
   ERROR_LINE(),
   ERROR_PROCEDURE(),
   ERROR_MESSAGE(),
   GETDATE());
End catch
End
GO
/****** Object:  StoredProcedure [dbo].[Getaddcalclist]    Script Date: 17-10-2022 20:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Getaddcalclist]
As
BEGIN
BEGIN TRY
SELECT Id,Arg1,Arg2,Result,Createddate from dbo.tb_addcalc
END TRY
BEGIN CATCH
Insert into dbo.tb_errors
values
(ERROR_NUMBER(),
   ERROR_STATE(),
   ERROR_SEVERITY(),
   ERROR_LINE(),
   ERROR_PROCEDURE(),
   ERROR_MESSAGE(),
   GETDATE());
END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Insertaddcalc]    Script Date: 17-10-2022 20:08:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Insertaddcalc](@arg1 float,@arg2 float,@result float,@date datetime)
As
Begin
Begin try
Insert into dbo.tb_addcalc(Arg1,Arg2,Result,Createddate)
values(@arg1,@arg2,@result,@date)
End try
Begin catch
Insert into dbo.tb_errors(Errornumber,Errorstate,Errorseverity,Errorline,Errorprocedure,Errormessage,Datetime)
values
(ERROR_NUMBER(),
   ERROR_STATE(),
   ERROR_SEVERITY(),
   ERROR_LINE(),
   ERROR_PROCEDURE(),
   ERROR_MESSAGE(),
   GETDATE());
End catch

End
GO
USE [master]
GO
ALTER DATABASE [db_Savedatacalc] SET  READ_WRITE 
GO
