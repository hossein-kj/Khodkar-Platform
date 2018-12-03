

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [ContentManagement].[GetWebPageForPublish] 
(
@Url nvarchar(1024),
@Type nvarchar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
        declare @TypeId INT
        declare @TemplateUrl nvarchar(1024)
		declare @FrameWorkUrl nvarchar(1024)
        
        SELECT @TypeId = id from contentmanagement.masterdatakeyvalues
        where typeid = 1003 and code = @Type
        
	  
	  select Title,DependentModules,Params,Html,[Guid] as 'PageId',HaveScript,HaveStyle,[Version] FROM CONTENTMANAGEMENT.WebPages
	  where (UPPER(url) = UPPER(@Url)) and typeid= @TypeId

END