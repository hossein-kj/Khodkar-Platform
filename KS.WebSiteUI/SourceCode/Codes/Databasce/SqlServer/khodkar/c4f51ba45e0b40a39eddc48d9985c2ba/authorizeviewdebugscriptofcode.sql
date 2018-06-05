
/****** Object:  StoredProcedure [Security].[AuthorizeViewDebugScriptOfCode]    Script Date: 8/1/2017 10:06:13 PM ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [Security].[AuthorizeViewDebugScriptOfCode]
(
@permissionTypeId int,
@UserId int,
@BundleTypeId int,
@CodeTypeId int,
@ActionKey int,
@Path nvarchar(1024)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


        
        	declare @codeId as int
	set @codeId= 0
		    select TOP 1 @codeId = bundle.ParentId  from ContentManagement.MasterDataKeyValues as bundle	
where
	 bundle.TypeId = @BundleTypeId and 
	UPPER(LEFT(@Path ,LEN(bundle.PathOrUrl))) = UPPER(bundle.PathOrUrl)
	order by LEN(bundle.PathOrUrl) DESC

        
        select count(cd.Id)  from ContentManagement.MasterDataKeyValues as cd
	   LEFT join ContentManagement.MasterDataKeyValues as pr	
	   on cd.Id=@codeId and pr.ForeignKey3 = cd.Id and pr.TypeId = @permissionTypeId and pr.[Key] = @CodeTypeId and pr.ForeignKey1=@ActionKey
		inner join 
		 [Security].AspNetUserGroups as ug
		 on ug.UserId = @UserId
		   inner join
		    [Security].AspNetGroupRoles as gr
			on ug.GroupId = gr.GroupId
             and (pr.ForeignKey2  =gr.roleId or cd.ModifyRoleId =gr.roleId )


-- 	declare @viewRoleId as int
-- 	set @viewRoleId= 0
-- 		    select TOP 1 @viewRoleId = code.ViewRoleId  from ContentManagement.MasterDataKeyValues as code	
-- where
-- 	 code.TypeId = @TypeId and 
-- 	UPPER(LEFT(@Path ,LEN(code.PathOrUrl))) = UPPER(code.PathOrUrl)
-- 	order by LEN(code.PathOrUrl) DESC

-- 		   select count(gr.roleId)   from 
-- 		   [Security].AspNetUserGroups as ug
-- 		   inner join
-- 		    [Security].AspNetGroupRoles as gr
-- 			on ug.GroupId = gr.GroupId
-- 			 and ug.UserId = @UserId
--              and @viewRoleId =gr.roleId
END
