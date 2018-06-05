
/****** Object:  StoredProcedure [ContentManagement].[GetAspectForPublicUrl]    Script Date: 8/1/2017 10:03:05 PM ******/

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [Security].[GetAspectForPublicMasterDataKeyValueUrl]
(
@UrlOrPath nvarchar(1024)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	    select TOP 1 md.Name, md.[Key] as 'EnableLog'
	    ,md.EnableCache from
	     ContentManagement.MasterDataKeyValues as md

	where 
	UPPER(LEFT(@UrlOrPath ,LEN(md.PathOrUrl))) = UPPER(md.PathOrUrl)
	order by LEN(md.PathOrUrl) DESC

END
