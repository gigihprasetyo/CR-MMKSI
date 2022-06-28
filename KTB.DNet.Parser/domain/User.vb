Namespace KTB.DNet.Parser

    Public Class User

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal id As String, ByVal name As String)
            _id = id
            _name = name
        End Sub

#End Region

#Region "Private Variables"

        Private _id As String
        Private _name As String

#End Region

#Region "Public Properties"

        Property UserId() As String
            Get
                Return _id
            End Get
            Set(ByVal Value As String)
                _id = Value
            End Set
        End Property

        Property UserName() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

#End Region

    End Class

End Namespace