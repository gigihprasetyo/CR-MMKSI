Public Class enumInvoice
    Public Enum InvoiceKind
        VH = 0
        DP = 1
        LC = 2
        AC = 3
    End Enum

    Public Class TipeEnumGeneral
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub

        Public Property ValTipe() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTipe() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class


    Public Shared Function GetListOfInvoiceKind(Optional ByVal UseDefault As Boolean = False) As ArrayList
        'Dim items As Array
        Dim al As New ArrayList
        'items = System.Enum.GetValues(GetType(InvoiceKind))
        'Dim item As String
        'For Each item In items
        '    Dim sts As TipeEnumGeneral
        '    sts = New TipeEnumGeneral(0, item)
        '    al.Add(sts)
        'Next

        Dim sts As TipeEnumGeneral
        sts = New TipeEnumGeneral(0, "VH")
        al.Add(sts)
        sts = New TipeEnumGeneral(1, "DP")
        al.Add(sts)
        sts = New TipeEnumGeneral(2, "LC")
        al.Add(sts)
        sts = New TipeEnumGeneral(3, "AC")
        al.Add(sts)
        Return al


    End Function

End Class
