Namespace KTB.DNet.Domain

    Public Class EnumAssistModule
        Public Enum AssistModule
            SPartsStock = 1
            SPartsSales = 2
            DealerExpenseSpareParts = 3
            SVCIncomingReport = 4
            DealerExpenseService = 5
            'SVCIncomingBPReport = 6
        End Enum

        Public Function RetrieveModule() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumassistmodules
            'sts = New enumassistmodules("6", "Service Incoming BP")
            'al.Add(sts)
            sts = New enumassistmodules("5", "Dealer Expense Service")
            al.Add(sts)
            sts = New enumassistmodules("4", "Service Incoming")
            al.Add(sts)
            sts = New enumassistmodules("3", "Dealer Expense Spareparts")
            al.Add(sts)
            sts = New enumassistmodules("2", "Spareparts Sales")
            al.Add(sts)
            sts = New enumassistmodules("1", "Spareparts Stock")
            al.Add(sts)

            Return al
        End Function

    End Class

    Public Class enumassistmodules
        Private _Val As String
        Private _Name As String

        Public Function GetName(ByVal month As Integer) As String
            Select Case month
                Case 1
                    Return "Spareparts Stock"
                Case 2
                    Return "Spareparts Sales"
                Case 3
                    Return "Dealer Expense Spareparts"
                Case 4
                    Return "Service Incoming"
                Case 5
                    Return "Dealer Expense Service"
                    'Case 6
                    'Return "Service Incoming BP"
            End Select
        End Function

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

