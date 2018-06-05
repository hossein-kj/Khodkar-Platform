-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [Security].[AuthorizeViewSourceCodeOfService] 
(
@permissionTypeId int,
@TypeId int,
@ActionKey int,
@UserId int,
@serviceId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	   select count(pr.ForeignKey3)  from ContentManagement.MasterDataKeyValues as service
	   LEFT join ContentManagement.MasterDataKeyValues as pr	
	   on pr.ForeignKey3 = service.Id and pr.TypeId = @permissionTypeId and pr.[Key] = @TypeId and pr.ForeignKey1=@ActionKey and service.Id=@serviceId
		inner join
		 [Security].AspNetUserGroups as ug
		 on ug.UserId = @UserId
		   inner join
		    [Security].AspNetGroupRoles as gr
			on ug.GroupId = gr.GroupId
             and (service.ModifyRoleId  =gr.roleId or  pr.ForeignKey2  =gr.roleId )
		
		-- [Security].AspNetUserRoles as rl
		--on (service.ModifyRoleId =rl.roleId or pr.ForeignKey2 = rl.roleId) and rl.UserId = @userId 
		
END


--  select count(pr.ForeignKey3)  from ContentManagement.MasterDataKeyValues as service
-- 	   LEFT join ContentManagement.MasterDataKeyValues as pr	
-- 	   on pr.ForeignKey3 = service.Id and pr.TypeId = 1032 and pr.[Key] = 1001 and pr.ForeignKey1=687 and service.Id=97
-- 		inner join [Security].AspNetUserRoles as rl
-- 		on (service.ModifyRoleId =rl.roleId or pr.ForeignKey2 = rl.roleId) and rl.UserId = 1 
		


