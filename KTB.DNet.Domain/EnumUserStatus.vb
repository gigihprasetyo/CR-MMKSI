Namespace KTB.DNet.Domain

    Public Class EnumUserStatus

        Public Enum UserStatus
            Baru = 0
            Aktif = 1
            Tidak_Aktif = 2
        End Enum

        Public Shared Function UserStatDesc(ByVal iUserStat As Integer) As String
            Return CType(iUserStat, UserStatus).ToString()
        End Function

        Public Function Retrieve() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumStatusData
            sts = New EnumStatusData(0, "Baru")
            al.Add(sts)
            sts = New EnumStatusData(1, "Aktif")
            al.Add(sts)
            sts = New EnumStatusData(2, "Non-aktif")
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveNewOnly() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumStatusData
            sts = New EnumStatusData(0, "Baru")
            al.Add(sts)
            Return al
        End Function
    End Class

#Region "EnumStatus"
    Public Class EnumStatusData

        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub

        Public Property ValTitle() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTitle() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

#End Region

End Namespace