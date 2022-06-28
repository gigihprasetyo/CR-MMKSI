Namespace KTB.DNet.Utility

    Public Class EndPeriodDate

        Public Shared Function EndDate(ByVal PeriodMonth As Integer, ByVal PeriodYear As Integer) As DateTime
            '-- Return the last date in this period
            '-- e.g.: the last date of October 2005 is "31/10/2005"

            Dim LastDay As Integer

            Select Case PeriodMonth
                Case 1
                    LastDay = 31
                Case 2
                    If PeriodYear Mod 4 = 0 Then
                        LastDay = 29  '-- Cabisat year
                    Else
                        LastDay = 28
                    End If
                Case 3
                    LastDay = 31
                Case 4
                    LastDay = 30
                Case 5
                    LastDay = 31
                Case 6
                    LastDay = 30
                Case 7
                    LastDay = 31
                Case 8
                    LastDay = 31
                Case 9
                    LastDay = 30
                Case 10
                    LastDay = 31
                Case 11
                    LastDay = 30
                Case 12
                    LastDay = 31
            End Select

            '-- Return the last date of the period
            Return New DateTime(PeriodYear, PeriodMonth, LastDay, 23, 59, 59)

        End Function

    End Class

End Namespace
