

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [Security].[GetPermissionOfEntityId]
(
@permissionTypeId int,
@TypeId int,
@ActionKey int,
@EntityId int
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
	md.Id = @EntityId



END

 
 
 
 
--  select TOP 1 pr.ForeignKey3  from ContentManagement.MasterDataKeyValues as pr	

-- 	where  pr.TypeId = 1032 and pr.[Key] = 1041 and pr.ForeignKey1=815