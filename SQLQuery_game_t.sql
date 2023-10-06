USE [computer_game_db]
GO

/****** Object:  Table [dbo].[game_t]    Script Date: 07.10.2023 5:06:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[game_t](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name_f] [nvarchar](50) NOT NULL,
	[released_in_f] [int] NOT NULL,
	[price_f] [int] NOT NULL
) ON [PRIMARY]
GO


