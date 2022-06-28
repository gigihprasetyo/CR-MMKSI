Namespace KTB.DNet.Domain
    Public Class enumMonth
        Public Enum Month
            Jan = 0
            Feb = 1
            Mar = 2
            Apr = 3
            May = 4
            Jun = 5
            Jul = 6
            Agust = 7
            Sep = 8
            Oct = 9
            Nov = 10
            Dec = 11
        End Enum
    End Class


    Public Class enumMonthGet
        Private _Val As Integer
        Private _Name As String

        Public Shared Function GetName(ByVal month As Integer) As String
            Select Case month
                Case 1
                    Return "Jan"
                Case 2
                    Return "Feb"
                Case 3
                    Return "Mar"
                Case 4
                    Return "Apr"
                Case 5
                    Return "Mei"
                Case 6
                    Return "Jun"
                Case 7
                    Return "Jul"
                Case 8
                    Return "Ags"
                Case 9
                    Return "Sep"
                Case 10
                    Return "Okt"
                Case 11
                    Return "Nov"
                Case 12
                    Return "Des"
            End Select
        End Function

        Public Shared Function RetriveMonth(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emMonth As enumMonthGet

            If (isIncludeBlank) Then
                emMonth = New enumMonthGet(99, "")
                arr.Add(emMonth)
            End If

            emMonth = New enumMonthGet(1, "Jan")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(2, "Feb")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(3, "Mar")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(4, "Apr")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(5, "May")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(6, "Jun")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(7, "Jul")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(8, "Agust")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(9, "Sep")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(10, "Oct")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(11, "Nov")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(12, "Dec")
            arr.Add(emMonth)

            Return arr
        End Function

        Public Shared Function RetriveMonth() As ArrayList
            Dim arr As New ArrayList
            Dim emMonth As enumMonthGet

            emMonth = New enumMonthGet(1, "Jan")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(2, "Feb")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(3, "Mar")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(4, "Apr")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(5, "May")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(6, "Jun")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(7, "Jul")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(8, "Agust")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(9, "Sep")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(10, "Oct")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(11, "Nov")
            arr.Add(emMonth)
            emMonth = New enumMonthGet(12, "Dec")
            arr.Add(emMonth)

            Return arr
        End Function

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _Val = val
            _Name = name
        End Sub

        Public Sub New()
            'to do nothing
        End Sub

        Public Property ValStatus() As Integer
            Get
                Return _Val
            End Get
            Set(ByVal Value As Integer)
                _Val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property
    End Class
End Namespace

