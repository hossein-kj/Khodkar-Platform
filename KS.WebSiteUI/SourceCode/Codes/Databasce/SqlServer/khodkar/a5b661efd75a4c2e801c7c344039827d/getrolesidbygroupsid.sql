-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
alter PROCEDURE [Security].[GetRolesIdByGroupsId] 
(
@GroupsId  VARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT ON;

     select distinct grRl.GroupId ,grRl.RoleId
	  from 
	 [Security].AspNetRoles as rl
	 inner join 
	  [Security].AspNetGroupRoles as grRl
	  on rl.Id = grRl.RoleId
	  inner join
	  [Security].AspNetGroups as gr
	  on
	  grRl.GroupId in (SELECT Item = CONVERT(INT, Item) FROM
      ( SELECT Item = x.i.value('(./text())[1]', 'varchar(max)')
        FROM ( SELECT [XML] = CONVERT(XML, '<i>'
        + REPLACE(@GroupsId, ',', '</i><i>') + '</i>').query('.')
          ) AS a CROSS APPLY [XML].nodes('i') AS x(i) ) AS y
      WHERE Item IS NOT NULL)
END