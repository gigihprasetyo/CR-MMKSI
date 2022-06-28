Namespace KTB.DNet.Domain
    Public Class EnumCategoryPosition
        Public Enum CategoryPositionKTB
            Sales
            Service
            SparePart
        End Enum

        Public Shared Function RetrieveCategoryPosition() As ArrayList
            Dim al As New ArrayList
            Dim sts As CategoryPosition
            'sts = New CategoryPosition(0, "Silahkan Pilih")
            'al.Add(sts)
            sts = New CategoryPosition(1, "Sales")
            al.Add(sts)
            sts = New CategoryPosition(2, "Service")
            al.Add(sts)
            sts = New CategoryPosition(3, "Spare Part")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValue(ByVal Category As Integer) As String
            Dim str As String = ""
            If Category = 1 Then str = "Sales"
            If Category = 2 Then str = "Service"
            If Category = 3 Then str = "Spare Part"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sCategoryPosition As String) As Integer
            Dim Rsl As Integer = 0
            If sCategoryPosition.ToUpper = "Sales" Then Rsl = 1
            If sCategoryPosition.ToUpper = "Service" Then Rsl = 2
            If sCategoryPosition.ToUpper = "Spare Part" Then Rsl = 3
            Return Rsl
        End Function
    End Class

    Public Class CategoryPosition
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
