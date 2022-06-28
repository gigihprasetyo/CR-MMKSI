Public Class MessageModel
     
    Public Sub New()
    End Sub
    Private _Status As Boolean


    Private _Message As String


    Public Property Status() As Boolean
        Get
            Return _Status
        End Get
        Set(ByVal value As Boolean)
            _Status = value
        End Set
    End Property



    Public Property Message() As String
        Get
            Return _Message
        End Get
        Set(ByVal value As String)
            _Message = value
        End Set
    End Property
End Class
