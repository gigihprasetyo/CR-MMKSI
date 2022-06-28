Namespace KTB.DNet.Domain
    Public Class EnumTrClassRegistration
        Private Const RegisterText As String = "Terdaftar"
        Private Const PassText As String = "Lulus"
        Private Const FailText As String = "Tidak Lulus"
        Private Const RejectText As String = "Tolak"
        Private Const CancelText As String = "Batal"
        Private Const InviteText As String = "Terundang"


        Public Enum DataStatusType
            Register = 0
            Pass = 1
            Fail = 2
            Reject = 3
            Cancel = 4
            Invite = 5
        End Enum

        Public Function Retrieve() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumClassReg
            sts = New EnumClassReg(0, RegisterText)
            al.Add(sts)
            sts = New EnumClassReg(1, PassText)
            al.Add(sts)
            sts = New EnumClassReg(2, FailText)
            al.Add(sts)
            sts = New EnumClassReg(3, RejectText)
            al.Add(sts)
            sts = New EnumClassReg(4, CancelText)
            al.Add(sts)
            sts = New EnumClassReg(5, InviteText)
            al.Add(sts)
            Return al
        End Function

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumClassReg
            sts = New EnumClassReg(0, RegisterText)
            al.Add(sts)
            sts = New EnumClassReg(3, RejectText)
            al.Add(sts)
            sts = New EnumClassReg(4, CancelText)
            al.Add(sts)
            Return al
        End Function

        Public Function StatusByIndex(ByVal index As Integer) As String
            Select Case index
                Case DataStatusType.Register
                    Return RegisterText
                Case DataStatusType.Pass
                    Return PassText
                Case DataStatusType.Fail
                    Return FailText
                Case DataStatusType.Reject
                    Return RejectText
                Case DataStatusType.Cancel
                    Return CancelText
                Case DataStatusType.Invite
                    Return InviteText
                Case Else
                    Return String.Empty
            End Select
        End Function
    End Class

    Public Class EnumClassReg
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
