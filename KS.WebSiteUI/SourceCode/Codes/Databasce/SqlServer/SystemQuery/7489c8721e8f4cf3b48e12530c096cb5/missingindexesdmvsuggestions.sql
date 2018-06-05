-- Missing Indexes DMV Suggestions 
	SELECT  @@ServerName AS ServerName ,
	        DB_NAME() AS DBName ,
	        t.name AS 'Affected_table' ,
	        ( LEN(ISNULL(ddmid.equality_columns, N'')
	              + CASE WHEN ddmid.equality_columns IS NOT NULL
	                          AND ddmid.inequality_columns IS NOT NULL THEN ','
	                     ELSE ''
	                END) - LEN(REPLACE(ISNULL(ddmid.equality_columns, N'')
	                                   + CASE WHEN ddmid.equality_columns
	                                                             IS NOT NULL
	                                               AND ddmid.inequality_columns
	                                                             IS NOT NULL
	                                          THEN ','
	                                          ELSE ''
	                                     END, ',', '')) ) + 1 AS K ,
	        COALESCE(ddmid.equality_columns, '')
	        + CASE WHEN ddmid.equality_columns IS NOT NULL
	                    AND ddmid.inequality_columns IS NOT NULL THEN ','
	               ELSE ''
	          END + COALESCE(ddmid.inequality_columns, '') AS Keys ,
	        COALESCE(ddmid.included_columns, '') AS [include] ,
	        'Create NonClustered Index IX_' + t.name + '_missing_'
	        + CAST(ddmid.index_handle AS VARCHAR(20)) 
	        + ' On ' + ddmid.[statement] COLLATE database_default
	        + ' (' + ISNULL(ddmid.equality_columns, '')
	        + CASE WHEN ddmid.equality_columns IS NOT NULL
	                    AND ddmid.inequality_columns IS NOT NULL THEN ','
	               ELSE ''
	          END + ISNULL(ddmid.inequality_columns, '') + ')'
	        + ISNULL(' Include (' + ddmid.included_columns + ');', ';')
	                                                  AS sql_statement ,
	        ddmigs.user_seeks ,
	        ddmigs.user_scans ,
	        CAST(( ddmigs.user_seeks + ddmigs.user_scans )
	        * ddmigs.avg_user_impact AS BIGINT) AS 'est_impact' ,
	        avg_user_impact ,
	        ddmigs.last_user_seek ,
	        ( SELECT    DATEDIFF(Second, create_date, GETDATE()) Seconds
	          FROM      sys.databases
	          WHERE     name = 'tempdb'
	        ) SecondsUptime 
	-- Select * 
	FROM    sys.dm_db_missing_index_groups ddmig
	        INNER JOIN sys.dm_db_missing_index_group_stats ddmigs
	               ON ddmigs.group_handle = ddmig.index_group_handle
	        INNER JOIN sys.dm_db_missing_index_details ddmid
	               ON ddmig.index_handle = ddmid.index_handle
	        INNER JOIN sys.tables t ON ddmid.OBJECT_ID = t.OBJECT_ID
	WHERE   ddmid.database_id = DB_ID()
	ORDER BY est_impact DESC;