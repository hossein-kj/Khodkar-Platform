
/****** Object:  StoredProcedure [Security].[GetPermissionOfUrlOrPath]    Script Date: 8/1/2017 10:03:05 PM ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [Security].[GetPermissionOfPath]
(
@permissionTypeId int,
@TypeId int,
@ActionKey int,
@UrlOrPath nvarchar(1024)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	    select pr.ForeignKey2  from ContentManagement.MasterDataKeyValues as pr	
	inner join ContentManagement.MasterDataKeyValues as md
	on pr.ForeignKey3 = md.Id
	and pr.TypeId = @permissionTypeId and pr.[Key] = @TypeId and pr.ForeignKey1=@ActionKey and 
	UPPER(LEFT(@UrlOrPath ,LEN(md.PathOrUrl))) = UPPER(md.PathOrUrl)
	order by LEN(md.PathOrUrl) DESC

 --   select TOP 1 ForeignKey2  from ContentManagement.MasterDataKeyValues	
	--where TypeId = @TypeId and ForeignKey1=@ActionKey and 
	--UPPER(LEFT(@UrlOrPath ,LEN(ForeignUrl))) = UPPER(ForeignUrl)
	--order by LEN(ForeignUrl) DESC

END
