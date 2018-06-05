
/****** Object:  StoredProcedure [Security].[GetUserInfoById]    Script Date: 8/1/2017 7:35:21 PM ******/

-- =============================================
-- Author:		<Author,,کیائی>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [Security].[GetUserInfoById]
(
@UserId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	    select us.AliasName,aus.Email from contentmanagement.Users as us
		inner join 
		[Security].[AspNetUsers] as aus
		on us.Id = @userId and aus.Id = @userId

END


