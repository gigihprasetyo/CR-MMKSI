Public Class ZFUST0075

    ' Fields
    Private _ITEM As Integer
    Private _QTY As Integer

    ' Properties
    Public Property ITEM As Integer
        Get
            Return Me._ITEM
        End Get
        Set(ByVal value As Integer)
            Me._ITEM = value
        End Set
    End Property

    Public Property QTY As Integer
        Get
            Return Me._QTY
        End Get
        Set(ByVal value As Integer)
            Me._QTY = value
        End Set
    End Property

End Class
