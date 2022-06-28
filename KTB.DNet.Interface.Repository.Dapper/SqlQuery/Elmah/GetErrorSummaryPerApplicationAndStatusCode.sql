SELECT [Application], [StatusCode], COUNT(StatusCode) AS Total
FROM [dbo].[ELMAH_Error]
GROUP BY Application, StatusCode