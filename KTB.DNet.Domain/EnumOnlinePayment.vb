Namespace KTB.DNet.Domain
    Public Class EnumOnlinePayment
        Private m_Action As String

        Public Enum StatusOnlinePayment
            Baru = 0
            Validasi = 1
            Proses = 2
            Selesai = 3
        End Enum

        Public Enum OLStatus
            Active
            InActive
        End Enum

        Public Enum ActionOnlinePayment
            Baru = 0
            Validasi = 1
            Proses = 2
            Selesai = 3
        End Enum

        Public Enum PaymentType
            COD = 0
            TOP = 1
        End Enum

        Public Enum SourceDocument
            SAP
            MANUAL
        End Enum

        Public ReadOnly Property StatusOnlinePaymentList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As OnlinePaymentItem
                item = New OnlinePaymentItem("0", "Baru")
                list.Add(item)
                item = New OnlinePaymentItem("1", "Validasi")
                list.Add(item)
                item = New OnlinePaymentItem("2", "Prosess")
                list.Add(item)
                item = New OnlinePaymentItem("3", "Selesai")
                list.Add(item)
                Return list
            End Get
        End Property

        Public ReadOnly Property ActionOnlinePaymentList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As OnlinePaymentItem
                item = New OnlinePaymentItem("1", "Validasi")
                list.Add(item)
                item = New OnlinePaymentItem("2", "Prosess")
                list.Add(item)
                item = New OnlinePaymentItem("3", "Selesai")
                list.Add(item)
                Return list
            End Get
        End Property

        Public ReadOnly Property PaymentTypeList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As OnlinePaymentItem
                item = New OnlinePaymentItem("0", "COD")
                list.Add(item)
                item = New OnlinePaymentItem("1", "TOP")
                list.Add(item)
                Return list
            End Get
        End Property

        Public Function PaymentObligationStatusDesc(ByVal _status As Integer) As String
            Select Case _status
                Case 0
                    Return "Baru"
                Case 1
                    Return "Validasi"
                Case 2
                    Return "Proses"
                Case 2
                    Return "Selesai"
                Case Else
                    Return String.Empty
            End Select
        End Function

        Public ReadOnly Property SourceDocumentList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As OnlinePaymentItem
                item = New OnlinePaymentItem("0", "SAP")
                list.Add(item)
                item = New OnlinePaymentItem("1", "MANUAL")
                list.Add(item)
                Return list
            End Get
        End Property

        Public ReadOnly Property OLStatusList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As OnlinePaymentItem
                item = New OnlinePaymentItem("0", "Aktif")
                list.Add(item)
                item = New OnlinePaymentItem("1", "Tidak Aktif")
                list.Add(item)
                Return list
            End Get
        End Property


        Public Sub New()

        End Sub
    End Class

    Public Class OnlinePaymentItem
        Private m_OPcode As String
        Private m_OPValue As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal value As String, ByVal code As String)
            m_OPcode = code
            m_OPValue = value
        End Sub

        Property OPCode() As String
            Get
                Return m_OPcode
            End Get
            Set(ByVal Value As String)
                m_OPcode = Value
            End Set
        End Property

        Public Property OPValue() As String
            Get
                Return m_OPValue
            End Get
            Set(ByVal Value As String)
                m_OPValue = Value
            End Set
        End Property
    End Class
End Namespace

