Namespace KTB.DNet.Domain
    Public Class EnumDRReportStatus
        Public Enum ReportStatus
            Open
            Closed
        End Enum

        Public Shared Function RetrieveCategoryPosition() As ArrayList
            Dim al As New ArrayList
            Dim sts As DRReportStatus
            sts = New DRReportStatus(0, "Open")
            al.Add(sts)
            sts = New DRReportStatus(1, "Closed")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValue(ByVal iStatus As Integer) As String
            Dim str As String = ""
            If iStatus = 0 Then str = "Open"
            If iStatus = 1 Then str = "Closed"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sStatus.ToUpper = "OPEN" Then Rsl = 0
            If sStatus.ToUpper = "CLOSED" Then Rsl = 1
            Return Rsl
        End Function
    End Class

    Public Class DRReportStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
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
