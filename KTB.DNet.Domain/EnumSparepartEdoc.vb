Namespace KTB.DNet.Domain
    Public Class EnumSparepartEdoc
        Public Enum DocumentType
            Faktur = 0
            CreditMemoRetur = 1
            TTTDepositC2 = 2
            PenaltiPengembalianBarang = 3
            EOPackListCase = 4
            EOPackListSummary = 5
            ROPackListCase = 6
            ROPackListSummary = 7
            CreditMemoReturManual = 8
        End Enum

        Public Shared Function RetrieveAllDocumentType() As ArrayList
            Dim arr As New ArrayList
            arr.Add(New EnumSparepartEdocList(0, "Faktur"))
            'arr.Add(New EnumSparepartEdocList(1, "Credit Memo Retur"))
            'arr.Add(New EnumSparepartEdocList(2, "Tanda Terima Titipan Deposit C2"))
            arr.Add(New EnumSparepartEdocList(3, "Penalti Pengembalian Barang"))
            arr.Add(New EnumSparepartEdocList(4, "EO Packing List"))
            'arr.Add(New EnumSparepartEdocList(5, "EO Packing List Summary"))
            arr.Add(New EnumSparepartEdocList(6, "RO Packing List"))
            'arr.Add(New EnumSparepartEdocList(7, "RO Packing List Summary"))
            arr.Add(New EnumSparepartEdocList(8, "Credit Memo Retur (Manual)"))
            Return arr
        End Function
    End Class


    Public Class EnumSparepartEdocList
        Private _id As Integer
        Private _val As String

        Public Sub New(ByVal id As Integer, ByVal val As String)
            _id = id
            _val = val
        End Sub
        Public ReadOnly Property ID() As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property Value() As String
            Get
                Return _val
            End Get
        End Property

    End Class

    Public Class EnumDocumentCancel
        Public Enum DocumentType
            BillingPart = 0
            DOPart = 1
        End Enum
    End Class
End Namespace