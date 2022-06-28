

Namespace KTB.DNet.Domain

    Public Class EnumPQRType

        Public Enum PQRType
            PQR_WSC
            PQR_Only
            SparePart
            Accessories
            PQR_ESP
        End Enum

        Public Function RetrievePQRType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumPQRTypeValue

            sts = New EnumPQRTypeValue(0, "PQR WSC")
            al.Add(sts)
            sts = New EnumPQRTypeValue(1, "PQR Only")
            al.Add(sts)
            sts = New EnumPQRTypeValue(2, "PQR Spare Part")
            al.Add(sts)
            sts = New EnumPQRTypeValue(3, "PQR Accessories")
            al.Add(sts)
            sts = New EnumPQRTypeValue(4, "PQR Extended Waranty")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function GetStringValue(ByVal PQRType As String) As String
            Dim str As String = ""
            If PQRType = "0" Then str = "PQR WSC"
            If PQRType = "1" Then str = "PQR Only"
            If PQRType = "2" Then str = "PQR Spare Part"
            If PQRType = "3" Then str = "PQR Accessories"
            If PQRType = "4" Then str = "PQR Extended Waranty"
            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sPQRType As String) As Integer
            Dim Rsl As Integer = 0
            If sPQRType.ToUpper = "PQR WSC".ToUpper Then Rsl = PQRType.PQR_WSC
            If sPQRType.ToUpper = "PQR Only".ToUpper Then Rsl = PQRType.PQR_Only
            If sPQRType.ToUpper = "PQR Spare Part".ToUpper Then Rsl = PQRType.SparePart
            If sPQRType.ToUpper = "PQR Accessories".ToUpper Then Rsl = PQRType.Accessories
            If sPQRType.ToUpper = "PQR Extended Waranty".ToUpper Then Rsl = PQRType.PQR_ESP
            Return Rsl
        End Function
    End Class

    Public Class EnumPQRTypeValue
        Private _code As Integer
        Private _desc As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal code As Integer, ByVal desc As String)
            _code = code
            _desc = desc
        End Sub

        Public Property Code() As Integer
            Get
                Return _code
            End Get
            Set(ByVal Value As Integer)
                _code = Value
            End Set
        End Property

        Public Property Desc() As String
            Get
                Return _desc
            End Get
            Set(ByVal Value As String)
                _desc = Value
            End Set
        End Property

    End Class

End Namespace
