-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [Security].[AuthorizeViewSourceCodeOfMasterDataKeyValues] 
(
@permissionTypeId int,
@TypeId int,
@ActionKey int,
@UserId int,
@codeId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	   select count(pr.ForeignKey3)  from ContentManagement.MasterDataKeyValues as code
	   LEFT join ContentManagement.MasterDataKeyValues as pr	
	   on pr.ForeignKey3 = code.Id and pr.TypeId = @permissionTypeId and pr.[Key] = @TypeId and pr.ForeignKey1=@ActionKey and code.Id=@codeId
		inner join
		 [Security].AspNetUserGroups as ug
		 on ug.UserId = @UserId
		   inner join
		    [Security].AspNetGroupRoles as gr
			on ug.GroupId = gr.GroupId
             and (code.ModifyRoleId  =gr.roleId or  pr.ForeignKey2  =gr.roleId )
		
		-- [Security].AspNetUserRoles as rl
		--on (code.ModifyRoleId =rl.roleId or pr.ForeignKey2 = rl.roleId) and rl.UserId = @userId 
		
END


--  select count(pr.ForeignKey3)  from ContentManagement.MasterDataKeyValues as code
-- 	   LEFT join ContentManagement.MasterDataKeyValues as pr	
-- 	   on pr.ForeignKey3 = code.Id and pr.TypeId = 1032 and pr.[Key] = 1001 and pr.ForeignKey1=687 and code.Id=97
-- 		inner join [Security].AspNetUserRoles as rl
-- 		on (code.ModifyRoleId =rl.roleId or pr.ForeignKey2 = rl.roleId) and rl.UserId = 1 
		


