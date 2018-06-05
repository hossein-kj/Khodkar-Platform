

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [Security].[GetPermissionOfEntityIdByVersion]
(
@permissionTypeId int,
@TypeId int,
@ActionKey int,
@EntityId int,
@entityVersion int
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
	md.Id = @EntityId and (pr.Value = @entityVersion or pr.Value = 0)



END

 
 
 
 
-- select pr.ForeignKey2,pr.Value  from ContentManagement.MasterDataKeyValues as pr	
-- 	inner join ContentManagement.MasterDataKeyValues as md
-- 	on pr.ForeignKey3 = md.Id
-- 	and pr.TypeId = 1032 and pr.[Key] = 1041 and pr.ForeignKey1=924 and 
-- 	md.Id = 793 and (pr.Value = 71 or pr.Value = 0)