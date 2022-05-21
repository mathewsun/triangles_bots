USE [Exchange]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrianglesData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Pairs] [nvarchar](max) NOT NULL,
	[1PairPrice] [decimal](38, 20) NOT NULL,
	[2PairPrice] [decimal](38, 20) NOT NULL,
	[3PairPrice] [decimal](38, 20) NOT NULL,
	[Profit] [decimal](38, 20) NOT NULL,
	[ProfitPercent] [decimal](38, 20) NOT NULL,
 CONSTRAINT [PK_TrianglesData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[TrianglesData] ADD  CONSTRAINT [DF_TrianglesData_Profit]  DEFAULT ((0)) FOR [Profit]
GO
ALTER TABLE [dbo].[TrianglesData] ADD  CONSTRAINT [DF_TrianglesData_ProfitPercent]  DEFAULT ((0)) FOR [ProfitPercent]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[AddTriangleData]
@pairs nvarchar(max),
@pair1price decimal(38,20),
@pair2price decimal(38,20),
@pair3price decimal(38,20),
@date datetime
AS
BEGIN

INSERT INTO [Exchange].[dbo].[TrianglesData] ([Date], Pairs, [1PairPrice], [2PairPrice], [3PairPrice], [Profit], [ProfitPercent])
VALUES (@date, @pairs, @pair1price, @pair2price, @pair3price, (((1 / @pair3price) * @pair2price/ @pair1price) - 1), ((((1 / @pair3price) * @pair2price/ @pair1price) - 1) * 100))

END
GO
