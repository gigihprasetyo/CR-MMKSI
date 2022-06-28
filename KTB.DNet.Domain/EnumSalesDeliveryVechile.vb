Namespace KTB.DNet.Domain
    Public Class EnumSalesDeliveryVechile


        Public Enum SalesDeliveryVechileStatus
            Baru
            Selesai
            Batal
        End Enum

        Public Function RetrieveSalesDeliveryVechileStatus() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumSalesDeliveryVechileStatus
            sts = New EnumSalesDeliveryVechileStatus(0, "Baru")
            al.Add(sts)
            sts = New EnumSalesDeliveryVechileStatus(1, "Selesai")
            al.Add(sts)
            Return al
        End Function
    End Class

    Public Class EnumSalesDeliveryVechileStatus
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public ReadOnly Property ValStatus() As Integer
            Get
                Return _val
            End Get
        End Property

        Public ReadOnly Property NameStatus() As String
            Get
                Return _Name
            End Get
        End Property

    End Class
End Namespace

