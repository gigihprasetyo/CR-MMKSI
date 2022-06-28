Namespace KTB.DNet.Domain
    Public Class EnumCcReportStatus
        Public Enum ReportStatus
            InProgress
            NoData
            Done
            Downloaded
        End Enum

        Public Shared Function RetrieveCategoryPosition() As ArrayList
            Dim al As New ArrayList
            Dim sts As CcReportStatus
            sts = New CcReportStatus(0, "In Progress")
            al.Add(sts)
            sts = New CcReportStatus(1, "Tidak ada data")
            al.Add(sts)
            sts = New CcReportStatus(2, "Selesai")
            al.Add(sts)
            sts = New CcReportStatus(3, "Sudah di download")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValue(ByVal iStatus As Integer) As String
            Dim str As String = ""
            If iStatus = 0 Then str = "In Progress"
            If iStatus = 1 Then str = "Tidak ada data"
            If iStatus = 2 Then str = "Selesai"
            If iStatus = 3 Then str = "Sudah di download"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sStatus As String) As Integer
            Dim Rsl As Integer = 0
            If sStatus.ToUpper = "IN PROGRESS" Then Rsl = 0
            If sStatus.ToUpper = "TIDAK ADA DATA" Then Rsl = 1
            If sStatus.ToUpper = "SELESAI" Then Rsl = 2
            If sStatus.ToUpper = "SUDAH DI DOWNLOAD" Then Rsl = 3
            Return Rsl
        End Function
    End Class

    Public Class CcReportStatus
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
