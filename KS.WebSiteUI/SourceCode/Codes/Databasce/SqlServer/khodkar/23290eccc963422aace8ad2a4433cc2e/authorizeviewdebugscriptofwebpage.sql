
/****** Object:  StoredProcedure [Security].[AuthorizeViewDebugScriptOfWebPage]    Script Date: 8/1/2017 10:04:14 PM ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [Security].[AuthorizeViewDebugScriptOfWebPage]
(
@permissionTypeId int,
@TypeId int,
@ActionKey int,
@UserId int,
@guid nvarchar(32)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	   select count(wp.Id)  from ContentManagement.WebPages as wp
	    inner join ContentManagement.Links as ln
	    on ln.Url = wp.Url and wp.Guid = @guid
	   LEFT join ContentManagement.MasterDataKeyValues as pr	
	   on pr.ForeignKey3 = ln.Id and pr.TypeId = @permissionTypeId and pr.[Key] = @TypeId and pr.ForeignKey1=@ActionKey
		inner join 
		 [Security].AspNetUserGroups as ug
		 on ug.UserId = @UserId
		   inner join
		    [Security].AspNetGroupRoles as gr
			on ug.GroupId = gr.GroupId
             and (pr.ForeignKey2  =gr.roleId or wp.ModifyRoleId =gr.roleId )

		--[Security].AspNetUserRoles as rl
		--on (wp.ModifyRoleId =rl.roleId or pr.ForeignKey2 = rl.roleId) and rl.UserId = @userId 
		
END







--  select wp.title,wp.url,pr.ForeignKey2  from ContentManagement.WebPages as wp
-- 	    inner join ContentManagement.Links as ln
-- 	    on ln.Url = wp.Url and wp.Guid = '6776956069d04dd6a28b44f7c1d0546c'
-- 	   left join ContentManagement.MasterDataKeyValues as pr	
-- 	   on pr.ForeignKey3 = ln.Id and pr.TypeId = 1032 and pr.[Key] = 101 and pr.ForeignKey1=687
-- 		inner join [Security].AspNetUserRoles as rl
-- 		on (wp.ModifyRoleId =rl.roleId or pr.ForeignKey2 = rl.roleId) and rl.UserId = 1 






