----(
--		select DISTINCT FileDate,sum([ContractsTraded])
--		FROM [WorkStudy].[dbo].[DailyMTM]
--		GROUP BY FileDate,ContractsTraded 
		
--		) as dt

CREATE PROCEDURE SP_Total_Contracts_Traded_Report
@fromDate Date = '2021-01-04',
@toDate Date = '2021-01-05'
AS
SELECT   DISTINCT FileDate
				 , [Contract]
				 ,[ContractsTraded]
				 ,(([ContractsTraded]/7273)*100) as [% Of Total Contracts Traded]
FROM [WorkStudy].[dbo].[DailyMTM]  
WHERE FileDate between @fromDate AND @toDate 	
        and ContractsTraded >0
GROUP BY [ContractsTraded] ,FileDate,[Contract] 
Order By FileDate
		