-- Stored Procedures 
	SELECT  @@Servername AS ServerName ,
	        DB_NAME() AS DBName ,
	        o.name AS StoredProcedureName ,
	        o.[Type] ,
			sch.name as 'schema',
	        o.create_date
	FROM    sys.objects o
	inner join 
	sys.schemas sch
	on o.schema_id= sch.schema_id
	WHERE   o.[Type] = 'P' -- Stored Procedures 
	ORDER BY o.name
	--OR 
	-- Stored Procedure details 
	SELECT  @@Servername AS ServerName ,
	        DB_NAME() AS DB_Name ,
	        o.name AS 'ViewName' ,
	        o.[type] ,
	        o.Create_date ,
	        sm.[definition] AS 'Stored Procedure script'
	FROM    sys.objects o
	        INNER JOIN sys.sql_modules sm ON o.object_id = sm.object_id
	WHERE   o.[type] = 'P' -- Stored Procedures 
	        -- AND sm.[definition] LIKE '%insert%'
	        -- AND sm.[definition] LIKE '%update%'
	        -- AND sm.[definition] LIKE '%delete%'
	        -- AND sm.[definition] LIKE '%tablename%'
	ORDER BY o.name;



Select * From sys.procedures
Where [Type] = 'P'

Select * From Information_Schema.Routines

SELECT
    OBJECT_NAME(OBJECT_ID),
    definition
FROM
    sys.sql_modules
WHERE
    objectproperty(OBJECT_ID, 'IsProcedure') = 1
AND definition LIKE '%webpage%' 

EXEC sp_helptext '[ContentManagement].[GetMasterDataLocalKeyValue]';
