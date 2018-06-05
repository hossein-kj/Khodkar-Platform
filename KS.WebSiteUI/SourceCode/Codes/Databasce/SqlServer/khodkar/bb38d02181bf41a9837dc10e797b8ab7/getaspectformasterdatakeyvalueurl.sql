
/****** Object:  StoredProcedure [Security].[GetAspectForUrl]    Script Date: 8/1/2017 10:03:05 PM ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [Security].[GetAspectForMasterDataKeyValueUrl]
(
@TypeId int,
@ActionKey int,
@UrlOrPath nvarchar(1024)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	    select TOP 1 pr.ForeignKey2 as 'Permission',md.Name,md.[Key] as 'EnableLog'
	    ,md.EnableCache from ContentManagement.MasterDataKeyValues as pr	
	inner join ContentManagement.MasterDataKeyValues as md
	on pr.ForeignKey3 = md.Id
	and pr.TypeId = @TypeId and pr.ForeignKey1=@ActionKey and 
	UPPER(LEFT(@UrlOrPath ,LEN(md.PathOrUrl))) = UPPER(md.PathOrUrl)
	order by LEN(md.PathOrUrl) DESC

END
