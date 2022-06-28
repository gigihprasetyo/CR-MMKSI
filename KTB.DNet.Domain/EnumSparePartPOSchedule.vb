Namespace KTB.DNet.Domain
    Public Class EnumSparePartPOSchedule


        Public Enum Hari
            Senin = 0
            Selasa = 1
            Rabu = 2
            Kamis = 3
            Jumat = 4
            Sabtu = 5
            Minggu = 6
        End Enum
 


        ''' <summary>
        ''' Rerieve list of the day in the week
        ''' </summary>
        ''' <param name="parIsAll">True all day</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function RetrieveHari(Optional ByVal parIsAll As Boolean = False) As SortedList
            Dim ObjHari As New ArrayList
            Dim ObjHH As New SortedList
            Dim aryEnums As Array = [Enum].GetValues(GetType(Hari))

            For i As Integer = 0 To aryEnums.Length - 1

                If parIsAll = False AndAlso (i = Hari.Sabtu OrElse i = Hari.Minggu) Then

                Else
                    ObjHH.Add(i, CType(i, Hari).ToString())
                End If

            Next


            Return ObjHH
        End Function

        Public Shared Function GetIntWeek() As Integer

            Select Case Date.Now.DayOfWeek
                Case DayOfWeek.Monday
                    Return 0
                Case DayOfWeek.Tuesday
                    Return 1
                Case DayOfWeek.Wednesday
                    Return 2
                Case DayOfWeek.Thursday
                    Return 3
                Case DayOfWeek.Friday
                    Return 4
                Case DayOfWeek.Saturday
                    Return 5
                Case DayOfWeek.Sunday
                    Return 6

            End Select

            Return -1
        End Function


    End Class

End Namespace
