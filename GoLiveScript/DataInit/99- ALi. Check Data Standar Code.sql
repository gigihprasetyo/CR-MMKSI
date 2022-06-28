;
WITH    ANU
          AS ( SELECT   a.Category + ';' + CONVERT(VARCHAR(100), a.ValueId) data
               FROM     dbo.StandardCode a
               WHERE    1 = 1
                        AND a.RowStatus = 0
               GROUP BY a.Category + ';' + CONVERT(VARCHAR(100), a.ValueId)
               HAVING   COUNT(*) > 1
             ),
        single
          AS ( SELECT   *
               FROM     ( SELECT    a.data
                          FROM      ANU a
                          GROUP BY  a.data
                        ) d
                        OUTER APPLY ( SELECT TOP 1
                                                *
                                      FROM      dbo.StandardCode c
                                      WHERE     1 = 1
                                                AND c.RowStatus = 0
                                                AND c.Category + ';'
                                                + CONVERT(VARCHAR(100), c.ValueId) = d.data
                                    ) c
             )
    DELETE  FROM cumi
    FROM    dbo.StandardCode cumi
    WHERE   cumi.ID IN ( SELECT s.ID
                         FROM   single s )



SELECT  *
FROM    BSIDNET_MMKSI_DMS_20180924_0100.dbo.StandardCode c
WHERE   1 = 1
        AND c.RowStatus = 0
        AND c.Category + ';' + CONVERT(VARCHAR(100), c.ValueId) NOT IN (
        SELECT  a.Category + ';' + CONVERT(VARCHAR(100), a.ValueId) data
        FROM    dbo.StandardCode a
        WHERE   1 = 1
                AND c.RowStatus = 0 )