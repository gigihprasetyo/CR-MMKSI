

Namespace KTB.DNet.Domain
    Public Class EnumGIStatusMatPromotion

        Public Enum GIStatus
            Belum_Good_Issue
            Partial_Good_Issue
            Good_Issue
        End Enum

        Public Function RetrieveStatus() As ArrayList


            Dim al As New ArrayList
            Dim sts As EnumClaimStatusProp
            sts = New EnumClaimStatusProp(GIStatus.Belum_Good_Issue, GIStatus.Belum_Good_Issue.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusProp(GIStatus.Partial_Good_Issue, GIStatus.Partial_Good_Issue.ToString)
            al.Add(sts)
            sts = New EnumClaimStatusProp(GIStatus.Good_Issue, GIStatus.Good_Issue.ToString)
            al.Add(sts)

            Return al
        End Function


    End Class

    Public Class EnumGIOp
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
