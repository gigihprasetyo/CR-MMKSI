Imports System.Web.UI.WebControls


Namespace KTB.DNet.Domain
    Public Class EnumParkingFeePeriod
        Public Enum PeriodType
            Jan = 1
            Feb = 2
            Mar = 3
            Apr = 4
            May = 5
            Jun = 6
            Jul = 7
            Aug = 8
            Sep = 9
            Oct = 10
            Nov = 11
            Dec = 12
        End Enum

        Public Shared Function GetStringValue(ByVal PeriodType As PeriodType, ByVal PeriodYear As Integer) As String
            Dim str As String = ""
            Dim dt1 As Date = DateSerial(PeriodYear, PeriodType, 1)
            Dim dt2 As Date = dt1.AddMonths(1)

            str = dt1.ToString("MMMM yyyy") & " - " & dt2.ToString("MMMM yyyy")
            
            Return str
        End Function

        Public Shared Function GetStringValueShort(ByVal PeriodType As PeriodType, ByVal PeriodYear As Integer) As String
            Dim str As String = ""
            Dim dt1 As Date = DateSerial(PeriodYear, PeriodType, 1)
            Dim dt2 As Date = dt1.AddMonths(1)

            str = dt1.ToString("MMM yyyy") & " - " & dt2.ToString("MMM yyyy")

            Return str
        End Function

        Public Shared Function GetStringValue(ByVal PeriodType As PeriodType) As String
            Dim str As String = ""
            Dim dt1 As Date = DateSerial(Date.Now.Year, PeriodType, 1)
            Dim dt2 As Date = dt1.AddMonths(1)

            'str = dt1.ToString("MMM") & " - " & dt2.ToString("MMM")
            str = dt1.ToString("MMM")

            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sPeriodType As String) As PeriodType
            Dim Rsl As PeriodType
            'ToDo
            Return Rsl
        End Function

        Public Shared Function GetEnumValueForWSM(ByVal sPeriodType As String, ByRef PeriodYear As Integer) As PeriodType
            Dim Rsl As PeriodType = PeriodType.Jan '01022012 or 09102012
            PeriodYear = 1900

            If sPeriodType.Length = 8 Then
                Try
                    Rsl = CType(sPeriodType.Substring(0, 2), Short)
                Catch ex As Exception
                    Rsl = PeriodType.Jan
                End Try
                Try
                    PeriodYear = CType(sPeriodType.Substring(4, 4), Integer)
                Catch ex As Exception
                    PeriodYear = 1900
                End Try
            End If

            Return Rsl
        End Function

        Public Shared Function GetList(ByVal PeriodYear As String) As ArrayList
            Dim arl As ArrayList = New ArrayList
            Dim i As Integer

            For i = 1 To 12
                arl.Add(New ListItem(GetStringValue(i, 0), i))
            Next
            Return arl
        End Function

        Public Shared Function GetList() As ArrayList
            Dim arl As ArrayList = New ArrayList
            Dim i As Integer

            For i = 1 To 12
                arl.Add(New ListItem(GetStringValue(i), i))
            Next
            Return arl
        End Function
    End Class
End Namespace
