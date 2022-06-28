Namespace KTB.DNet.Domain
    Public Class EnumCustomerCaseResponse
        Public Enum CustomerCaseResponse
            NewStatus
            Re_Open
            Inprogres
            Escalated
            Closed
            Re_Schedule
            Cancellation
        End Enum

        Public Shared Function GetStringCustomerResponse(ByVal source As Integer) As String
            Dim str As String = ""
            Select Case source
                Case 0
                    str = "New"
                Case 1
                    str = "Re-Open"
                Case 2
                    str = "In Progress"
                Case 3
                    str = "Escalated"
                Case 4
                    str = "Closed"
                Case 5
                    str = "Re-Schedule"
                Case 6
                    str = "Cancellation"
            End Select
            Return str
        End Function

        Public Shared Function RetriveCustomerPurpose(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EnumCustomerCaseResponseOp As EnumCustomerCaseResponseOp

            If (isIncludeBlank) Then
                EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(-1, "Silahkan pilih")
                arr.Add(EnumCustomerCaseResponseOp)
            End If

            EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(0, "New")
            arr.Add(EnumCustomerCaseResponseOp)

            EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(1, "Re-Open")
            arr.Add(EnumCustomerCaseResponseOp)

            EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(2, "In Progress")
            arr.Add(EnumCustomerCaseResponseOp)

            EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(3, "Escalated")
            arr.Add(EnumCustomerCaseResponseOp)

            EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(4, "Closed")
            arr.Add(EnumCustomerCaseResponseOp)

            EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(5, "Re-Schedule")
            arr.Add(EnumCustomerCaseResponseOp)

            EnumCustomerCaseResponseOp = New EnumCustomerCaseResponseOp(6, "Cancellation")
            arr.Add(EnumCustomerCaseResponseOp)
            Return arr
        End Function
    End Class

    Public Class EnumCustomerCaseResponseOp
        Private _Val As Integer
        Private _Name As String

        

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _Val = val
            _Name = name
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

