-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

alter PROCEDURE [Security].[GetListOfDllReferencingPermissionFromeListOfDllId] 
(
@permissionTypeId int,
@ActionKey int,
@DllIds  VARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT ON;

select pr.ForeignKey2,md.Code  from ContentManagement.MasterDataKeyValues as pr
	inner join ContentManagement.MasterDataKeyValues as md
	on pr.ForeignKey3 = md.Id and pr.TypeId = @permissionTypeId  and pr.ForeignKey1=@ActionKey
	and md.Id in (
SELECT Item = CONVERT(NVARCHAR, Item) FROM
      ( SELECT Item = x.i.value('(./text())[1]', 'varchar(max)')
        FROM ( SELECT [XML] = CONVERT(XML, '<i>'
        + REPLACE(@DllIds, ',', '</i><i>') + '</i>').query('.')
          ) AS a CROSS APPLY [XML].nodes('i') AS x(i) ) AS y
      WHERE Item IS NOT NULL)
END

-- select pr.ForeignKey2,md.code  from ContentManagement.MasterDataKeyValues as pr
-- 	inner join ContentManagement.MasterDataKeyValues as md
-- 	on pr.ForeignKey3 = md.Id
-- 	and md.code in (
-- SELECT Item = CONVERT(NVARCHAR, Item) FROM
--       ( SELECT Item = x.i.value('(./text())[1]', 'varchar(max)')
--         FROM ( SELECT [XML] = CONVERT(XML, '<i>'
--         + REPLACE('system.dll.system,system.dll.system.net', ',', '</i><i>') + '</i>').query('.')
--           ) AS a CROSS APPLY [XML].nodes('i') AS x(i) ) AS y
--       WHERE Item IS NOT NULL)




