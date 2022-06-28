Namespace KTB.DNet.Parser

    Public Class Order

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal customerID As String, ByVal productID As String, ByVal quantity As Integer)
            _CustomerID = customerID
            _productID = productID
            _Quantity = quantity
        End Sub

#End Region

#Region "Private Variables"

        Private _CustomerID As String
        Private _productID As String
        Private _Quantity As Integer

#End Region

#Region "Public Properties"

        Property CustomerID() As String
            Get
                Return _CustomerID
            End Get
            Set(ByVal Value As String)
                _CustomerID = Value
            End Set
        End Property

        Property ProductID() As String
            Get
                Return _productID
            End Get
            Set(ByVal Value As String)
                _productID = Value
            End Set
        End Property

        Property Quantity() As Integer
            Get
                Return _Quantity
            End Get
            Set(ByVal Value As Integer)
                _Quantity = Value
            End Set
        End Property

#End Region

    End Class

End Namespace