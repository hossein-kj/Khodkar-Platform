-- Functions 
	SELECT  @@Servername AS ServerName ,
	        DB_NAME() AS DB_Name ,
	        o.name AS 'Functions' ,
	        o.[Type] ,
			sch.name as 'schema',
	        o.create_date
	FROM    sys.objects o
	inner join 
	sys.schemas sch
	on o.schema_id= sch.schema_id
	WHERE   o.Type = 'FN' -- Function 
	ORDER BY o.NAME;
	--OR 
	-- Function details 
	SELECT  @@Servername AS ServerName ,
	        DB_NAME() AS DB_Name ,
	        o.name AS 'FunctionName' ,
	        o.[type] ,
	        o.create_date ,
	        sm.[DEFINITION] AS 'Function script'
	FROM    sys.objects o
	        INNER JOIN sys.sql_modules sm ON o.object_id = sm.OBJECT_ID
	WHERE   o.[Type] = 'FN' -- Function 
	ORDER BY o.NAME;