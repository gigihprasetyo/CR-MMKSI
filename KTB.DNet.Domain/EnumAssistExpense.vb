Namespace KTB.DNet.Domain

    Public Class EnumAssistExpense
        Public Enum ExpenseType
            SERVICE
            SPAREPARTS
        End Enum

        Public Enum Type
            TARGET
            BIAYA
        End Enum

        Public Function RetrieveStatusType() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumExpense
            sts = New enumExpense("TARGET", "TARGET")
            al.Add(sts)
            sts = New enumExpense("BIAYA", "BIAYA")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveStatusExpenseType() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumExpense
            sts = New enumExpense("SERVICE", "SERVICE")
            al.Add(sts)
            sts = New enumExpense("SPAREPARTS", "SPAREPARTS")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class enumExpense
        Private _Val As String
        Private _Name As String

        Public Sub New(ByVal val As String, ByVal name As String)
            _Val = val
            _Name = name
        End Sub

        Public Property ValStatus() As String
            Get
                Return _Val
            End Get
            Set(ByVal Value As String)
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

