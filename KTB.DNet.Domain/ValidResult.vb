Public Class ValidResult


    Public Sub New()
        _message = ""
        _result = False
    End Sub


    Private _result As Boolean
    Public Property IsValid() As Boolean
        Get
            Return _result
        End Get
        Set(ByVal value As Boolean)
            _result = value
        End Set
    End Property

    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property


    Private _errorCode As Integer
    Public Property ErrorCode() As Integer
        Get
            Return _errorCode
        End Get
        Set(ByVal value As Integer)
            _errorCode = value
        End Set
    End Property




End Class
