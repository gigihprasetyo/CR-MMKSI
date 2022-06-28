Namespace KTB.DNet.Domain

    Public Class EnumTrTrainee

#Region "Trainee Status"

        Private Const UnapprovedText As String = "Belum disetujui"
        Private Const ActiveText As String = "Aktif"
        Private Const DeactiveText As String = "Tidak Aktif"

        Public Enum TrTraineeStatus
            Unapproved = 0
            Active = 1
            Deactive = 2
        End Enum

        Public Function RetrieveStatus() As ArrayList
            Dim al As New ArrayList

            Dim sts As EnumTrTraineeData
            sts = New EnumTrTraineeData(TrTraineeStatus.Unapproved, UnapprovedText)
            al.Add(sts)
            sts = New EnumTrTraineeData(TrTraineeStatus.Active, ActiveText)
            al.Add(sts)
            sts = New EnumTrTraineeData(TrTraineeStatus.Deactive, DeactiveText)
            al.Add(sts)
            Return al
        End Function

        Public Function StatusByIndex(ByVal index As Integer) As String
            Select Case index
                Case TrTraineeStatus.Unapproved
                    Return UnapprovedText
                Case TrTraineeStatus.Active
                    Return ActiveText
                Case TrTraineeStatus.Deactive
                    Return DeactiveText
                Case Else
                    Return String.Empty
            End Select
        End Function
#End Region

#Region "Shirt Size"

        Public Enum ShirtSize
            S
            M
            L
            XL
            XXL
        End Enum

        Public Function RetrieveSize() As ArrayList
            Dim al As New ArrayList

            Dim sts As EnumShirtData
            sts = New EnumShirtData(0, "S")
            al.Add(sts)
            sts = New EnumShirtData(1, "M")
            al.Add(sts)
            sts = New EnumShirtData(2, "L")
            al.Add(sts)
            sts = New EnumShirtData(3, "XL")
            al.Add(sts)
            sts = New EnumShirtData(4, "XXL")
            al.Add(sts)
            Return al
        End Function

#End Region

    End Class


#Region "EnumTrTraineeData"
    Public Class EnumTrTraineeData

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

#Region "EnumShirt"
    Public Class EnumShirtData

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
