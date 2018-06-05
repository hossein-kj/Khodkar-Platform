-- Table Columns 
	SELECT  @@Servername AS Server ,
	        DB_NAME() AS DBName ,
	        isc.Table_Name AS TableName ,
	        isc.Table_Schema AS SchemaName ,
	        Ordinal_Position AS Ord ,
	        Column_Name ,
	        Data_Type ,
	        Numeric_Precision AS Prec ,
	        Numeric_Scale AS Scale ,
	        Character_Maximum_Length AS LEN , -- -1 means MAX like Varchar(MAX) 
	        Is_Nullable ,
	        Column_Default ,
	        Table_Type
	FROM    INFORMATION_SCHEMA.COLUMNS isc
	        INNER JOIN information_schema.tables ist
	             ON isc.table_name = ist.table_name 
	--      WHERE Table_Type = 'BASE TABLE' -- 'Base Table' or 'View' 
	ORDER BY DBName ,
	        TableName ,
	        SchemaName ,
	        Ordinal_position; 
	 
	-- Summary of Column names and usage counts 
	-- Watch for column names with different data types or different lengths 
	SELECT  @@Servername AS Server ,
	        DB_NAME() AS DBName ,
	        Column_Name ,
	        Data_Type ,
	        Numeric_Precision AS Prec ,
	        Numeric_Scale AS Scale ,
	        Character_Maximum_Length ,
	        COUNT(*) AS Count
	FROM    information_schema.columns isc
	        INNER JOIN information_schema.tables ist
	              ON isc.table_name = ist.table_name
	WHERE   Table_type = 'BASE TABLE'
	GROUP BY Column_Name ,
	        Data_Type ,
	        Numeric_Precision ,
	        Numeric_Scale ,
	        Character_Maximum_Length;
	 
	-- Summary of data types 
	SELECT  @@Servername AS ServerName ,
	        DB_NAME() AS DBName ,
	        Data_Type ,
	        Numeric_Precision AS Prec ,
	        Numeric_Scale AS Scale ,
	        Character_Maximum_Length AS [Length] ,
	        COUNT(*) AS COUNT
	FROM    information_schema.columns isc
	        INNER JOIN information_schema.tables ist
	              ON isc.table_name = ist.table_name
	WHERE   Table_type = 'BASE TABLE'
	GROUP BY Data_Type ,
	        Numeric_Precision ,
	        Numeric_Scale ,
	        Character_Maximum_Length
	ORDER BY Data_Type ,
	        Numeric_Precision ,
	        Numeric_Scale ,
	        Character_Maximum_Length 
	 
	-- Large object data types or Binary Large Objects(BLOBs) 
	-- Note if you are using Enterprise edition, these tables can't rebuild indexes "Online" 
	SELECT  @@Servername AS ServerName ,
	        DB_NAME() AS DBName ,
	        isc.Table_Name ,
	        Ordinal_Position AS Ord ,
	        Column_Name ,
	        Data_Type AS BLOB_Data_Type ,
	        Numeric_Precision AS Prec ,
	        Numeric_Scale AS Scale ,
	        Character_Maximum_Length AS [Length]
	FROM    information_schema.columns isc
	        INNER JOIN information_schema.tables ist
	              ON isc.table_name = ist.table_name
	WHERE   Table_type = 'BASE TABLE'
	        AND ( Data_Type IN ( 'text', 'ntext', 'image', 'XML' )
	             OR ( Data_Type IN ( 'varchar', 'nvarchar', 'varbinary' )
	                  AND Character_Maximum_Length = -1
	                )
	            ) -- varchar(max), nvarchar(max), 