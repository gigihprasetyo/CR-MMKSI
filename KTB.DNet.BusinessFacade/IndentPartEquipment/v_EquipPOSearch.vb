Namespace KTB.DNET.BusinessFacade.IndentPartEquipment
    Public Class v_EquipPOSearch

        Dim _arlDealerCode As ArrayList = New ArrayList
        Dim _arlSPPONo As ArrayList = New ArrayList
        Dim _arlEstNo As ArrayList = New ArrayList
        Dim _arlProcessCode As ArrayList = New ArrayList
        Dim _dtmFrom As Date = DateTime.Now.Date
        Dim _dtmTo As Date = DateTime.Now.AddDays(1).Date
        Dim _bytePaymentType As Byte = 0

        Public Property arlDealerCode() As ArrayList
            Get
                Return _arlDealerCode
            End Get
            Set(ByVal Value As ArrayList)
                _arlDealerCode = Value
            End Set
        End Property

        Public Property arlSPPONo() As ArrayList
            Get
                Return _arlSPPONo
            End Get
            Set(ByVal Value As ArrayList)
                _arlSPPONo = Value
            End Set
        End Property

        Public Property arlEstNo() As ArrayList
            Get
                Return _arlEstNo
            End Get
            Set(ByVal Value As ArrayList)
                _arlEstNo = Value
            End Set
        End Property

        Public Property arlProcessCode() As ArrayList
            Get
                Return _arlProcessCode
            End Get
            Set(ByVal Value As ArrayList)
                _arlProcessCode = Value
            End Set
        End Property

        Public Property dtmFrom() As Date
            Get
                Return _dtmFrom
            End Get
            Set(ByVal Value As Date)
                _dtmFrom = Value
            End Set
        End Property

        Public Property dtmTo() As Date
            Get
                Return _dtmTo
            End Get
            Set(ByVal Value As Date)
                _dtmTo = Value
            End Set
        End Property

        Public Property bytePaymentType() As Byte
            Get
                Return _bytePaymentType
            End Get
            Set(ByVal Value As Byte)
                _bytePaymentType = Value
            End Set
        End Property

    End Class
End Namespace