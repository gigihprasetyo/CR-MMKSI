Imports System.Collections.Generic

Public Class CBUReturnSendSAP
    Private _sAPConn As String
    Private _username As String
    Private _password As String
    Private _chassisClaimHeaders As ArrayList = New ArrayList
    Private _sapParameters As DataTable = New DataTable
    Private _sapResponses As List(Of CBUReturnSAPResponse) = New List(Of CBUReturnSAPResponse)
    Private _currentStatusRetur As Integer
    Private _message As String
    Private _bodyReturn As New List(Of String)

    Public Sub AddBodyResponse(ByVal body As String)
        _bodyReturn.Add(body)
    End Sub

    Public ReadOnly Property GetBodyResponse As List(Of String)
        Get
            Return _bodyReturn
        End Get
    End Property

    Public Property SAPConn() As String
        Get
            Return _sAPConn
        End Get
        Set(ByVal value As String)
            _sAPConn = value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Public Property ChassisClaimHeaders() As ArrayList
        Get
            Return _chassisClaimHeaders
        End Get
        Set(ByVal value As ArrayList)
            _chassisClaimHeaders = value
        End Set
    End Property

    Public Property SAPParameters() As DataTable
        Get
            Return _sapParameters
        End Get
        Set(ByVal value As DataTable)
            _sapParameters = value
        End Set
    End Property

    Public Property SapResponses() As List(Of CBUReturnSAPResponse)
        Get
            Return _sapResponses
        End Get
        Set(ByVal value As List(Of CBUReturnSAPResponse))
            _sapResponses = value
        End Set
    End Property

    Public Property CurrentStatusRetur() As Integer
        Get
            Return _currentStatusRetur
        End Get
        Set(ByVal value As Integer)
            _currentStatusRetur = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property
End Class

Public Class CBUReturnSAPResponse
    Private _chassisLama As String
    Private _billingNumber As String
    Private _chassisPengganti As String
    Private _DONumber As String
    Private _SORetur As String
    Private _DORetur As String
    Private _billingRetur As String
    Private _billingVH As String
    Private _billingDEP As String
    Private _SOReplacement As String
    Private _DOReplacement As String
    Private _billingReplacement As String
    Private _serialNumber As String
    Private _engineNumber As String
    Private _doDate As Date
    Private _kodeStatus As String
    Private _message As String
    Private _status As String

    Public Property ChassisLama() As String
        Get
            Return _chassisLama
        End Get
        Set(ByVal value As String)
            _chassisLama = value
        End Set
    End Property

    Public Property BillingNumber() As String
        Get
            Return _billingNumber
        End Get
        Set(ByVal value As String)
            _billingNumber = value
        End Set
    End Property

    Public Property ChassisPengganti() As String
        Get
            Return _chassisPengganti
        End Get
        Set(ByVal value As String)
            _chassisPengganti = value
        End Set
    End Property

    Public Property DONumber() As String
        Get
            Return _DONumber
        End Get
        Set(ByVal value As String)
            _DONumber = value
        End Set
    End Property

    Public Property SORetur() As String
        Get
            Return _SORetur
        End Get
        Set(ByVal value As String)
            _SORetur = value
        End Set
    End Property

    Public Property DORetur() As String
        Get
            Return _DORetur
        End Get
        Set(ByVal value As String)
            _DORetur = value
        End Set
    End Property

    Public Property BillingRetur() As String
        Get
            Return _billingRetur
        End Get
        Set(ByVal value As String)
            _billingRetur = value
        End Set
    End Property

    Public Property BillingVH() As String
        Get
            Return _billingVH
        End Get
        Set(ByVal value As String)
            _billingVH = value
        End Set
    End Property

    Public Property BillingDEP() As String
        Get
            Return _billingDEP
        End Get
        Set(ByVal value As String)
            _billingDEP = value
        End Set
    End Property

    Public Property SOReplacement() As String
        Get
            Return _SOReplacement
        End Get
        Set(ByVal value As String)
            _SOReplacement = value
        End Set
    End Property

    Public Property DOReplacement() As String
        Get
            Return _DOReplacement
        End Get
        Set(ByVal value As String)
            _DOReplacement = value
        End Set
    End Property

    Public Property BillingReplacement() As String
        Get
            Return _billingReplacement
        End Get
        Set(ByVal value As String)
            _billingReplacement = value
        End Set
    End Property

    Public Property SerialNumber() As String
        Get
            Return _serialNumber
        End Get
        Set(ByVal value As String)
            _serialNumber = value
        End Set
    End Property

    Public Property EngineNumber() As String
        Get
            Return _engineNumber
        End Get
        Set(ByVal value As String)
            _engineNumber = value
        End Set
    End Property

    Public Property DoDate() As Date
        Get
            Return _doDate
        End Get
        Set(ByVal value As Date)
            _doDate = value
        End Set
    End Property

    Public Property KodeStatus() As String
        Get
            Return _kodeStatus
        End Get
        Set(ByVal value As String)
            _kodeStatus = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Public Property Status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property
End Class