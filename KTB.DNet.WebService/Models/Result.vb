Public Class ResultModel

    Public Sub New()
    End Sub
    Private _Status As Integer = 0


    Private _Message As String = ""


    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
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


Public Class FileModel

    Public Sub New()
    End Sub

    Private _DocType As String
    Public Property DocType() As String
        Get
            Return _DocType
        End Get
        Set(ByVal value As String)
            _DocType = value
        End Set
    End Property



    Private _randomName As String
    Public Property RandomName() As String
        Get
            Return _randomName
        End Get
        Set(ByVal value As String)
            _randomName = value
        End Set
    End Property


    Private _oriName As String
    Public Property OriName() As String
        Get
            Return _oriName
        End Get
        Set(ByVal value As String)
            _oriName = value
        End Set
    End Property



    Private _ext As String
    Public Property Ext() As String
        Get
            Return _ext
        End Get
        Set(ByVal value As String)
            _ext = value
        End Set
    End Property


    Private _Msg As String = ""
    Public Property Msg() As String
        Get
            Return _Msg
        End Get
        Set(ByVal value As String)
            _Msg = value
        End Set
    End Property

End Class
