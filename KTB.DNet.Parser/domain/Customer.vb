Namespace KTB.DNet.Parser

    Public Class Customerxxxx

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As String, ByVal name As String)
            _id = id
            _Name = name
        End Sub

#End Region

#Region "Private Variables"
        Private _id As String
        Private _Name As String
        Private _Orders As New ArrayList
#End Region

#Region "Public Properties"

        Property Id() As String
            Get
                Return _id
            End Get
            Set(ByVal Value As String)
                _id = Value
            End Set
        End Property

        Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

        Property Orders() As ArrayList
            Get
                Return _Orders
            End Get
            ' TODO: Change 'Orders' to be read-only by removing the property setter.
            ' Properties that return collections should be read-only so that users cannot entirely replace the backing store. Users can still modify the contents of the collection by calling relevant methods on the collection.
            Set(ByVal Value As ArrayList)
                _Orders = Value
            End Set
        End Property

#End Region

    End Class

End Namespace