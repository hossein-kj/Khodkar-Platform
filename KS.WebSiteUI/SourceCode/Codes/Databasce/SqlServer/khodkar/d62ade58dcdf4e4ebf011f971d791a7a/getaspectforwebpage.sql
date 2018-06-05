/****** Version:3d733f98-07f9-4cc4-86f0-b92a8b7e723d ******/
/****** Version:d853cd91-9ae9-46f5-a18b-8a47809f9838 ******/
/****** Version:a985e575-76b3-4953-b1fc-648e5cb646a3 ******/

/****** Object:  StoredProcedure [Security].[GetAspectForUrl]    Script Date: 8/1/2017 10:03:05 PM ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [Security].[GetAspectForWebPage] 
(
@Url nvarchar(1024),
@MobileUrl nvarchar(1024),
@Type nvarchar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
        declare @HasMobileVersion BIT
        declare @TypeId INT
        SELECT @TypeId = id from contentmanagement.masterdatakeyvalues
        where typeid = 1003 and code = @Type
        
        set @HasMobileVersion = 0
        
        
        select @HasMobileVersion = ismobileversion FROM CONTENTMANAGEMENT.WebPages
	  where  UPPER(url) = UPPER(@MobileUrl) and ismobileversion=1 and typeid= @TypeId
	  
	  select viewRoleID as 'Permission',
	  EnableCache,@HasMobileVersion as 'HasMobileVersion',CacheSlidingExpirationTimeInMinutes,status FROM CONTENTMANAGEMENT.WebPages
	  where (UPPER(url) = UPPER(@MobileUrl) or UPPER(url) = UPPER(@Url)) and typeid= @TypeId

END


--       select ismobileversion FROM CONTENTMANAGEMENT.WebPages
-- 	  where  (UPPER(url) = UPPER('/mobile' + '/fa/develop/test/testpage') or UPPER(url) = UPPER('/fa/develop/test/testpage')) and ismobileversion=1 






