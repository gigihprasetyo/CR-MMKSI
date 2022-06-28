Namespace KTB.DNet.Domain
    Public Class EnumMSPMasterStatus
        Public Enum MSPMasterStatus
            Aktif = 1
            Tidak_Aktif = 0
        End Enum

        Public Function Retrieve() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMSPMasterData

            sts = New EnumMSPMasterData(1, "Aktif")
            al.Add(sts)
            sts = New EnumMSPMasterData(0, "Tidak Aktif")
            al.Add(sts)
            Return al
        End Function

        Public Function GetStatus(ByVal val As Integer) As String
            Dim str As String
            If val = 1 Then
                str = "Aktif"
            Else
                str = "Tidak Aktif"
            End If

            Return str
        End Function
    End Class

    Public Class EnumMSPMasterData

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
End Namespace

