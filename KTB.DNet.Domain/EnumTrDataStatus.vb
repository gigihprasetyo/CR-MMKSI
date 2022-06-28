Namespace KTB.DNet.Domain
    Public Class EnumTrDataStatus
        Private Const ActiveText As String = "Aktif"
        Private Const DeactiveText As String = "Tidak Aktif"

        Public Enum DataStatusType
            Deactive '0
            Active '1
        End Enum

        Public Function Retrieve() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumDataStatus
            sts = New EnumDataStatus(0, DeactiveText)
            al.Add(sts)
            sts = New EnumDataStatus(1, ActiveText)
            al.Add(sts)
            Return al
        End Function

        Public Function StatusByIndex(ByVal index As Integer) As String
            Select Case index
                Case DataStatusType.Active
                    Return ActiveText
                Case DataStatusType.Deactive
                    Return DeactiveText
                Case Else
                    Return String.Empty
            End Select
        End Function
    End Class

    Public Class EnumDataStatus
        Private _val As Integer
        Private _Name As String


        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValueType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class

End Namespace
