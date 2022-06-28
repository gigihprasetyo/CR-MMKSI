 

Namespace KTB.DNet.Domain

    Public Class SPLEnum
        Public Enum CreditCeiling
            Regular = 0
            Temporary = 1
        End Enum

        Public Enum Interest
            Tidak = 0
            Ya = 1
        End Enum

        Public Shared Function RetrieveEnumInterest() As ArrayList
            Dim al As New ArrayList
            Dim CR As SPLInterest
            CR = New SPLInterest(0, "Tidak")
            al.Add(CR)
            CR = New SPLInterest(1, "Ya")
            al.Add(CR)
            Return al
        End Function
        
        Public Shared Function GetStringValue(ByVal InterestType As Interest) As String
            Dim str As String = ""
            If InterestType = Interest.Tidak Then str = Interest.Tidak.ToString
            If InterestType = Interest.Ya Then str = Interest.Ya.ToString

            Return str
        End Function

        Public Shared Function GetEnumValue(ByVal sInterest As String) As Integer
            Dim Rsl As Integer = 0
            If sInterest.ToUpper = Interest.Tidak.ToString Then Rsl = Interest.Tidak
            If sInterest.ToUpper = Interest.Ya.ToString Then Rsl = Interest.Ya

            Return Rsl
        End Function

        Public Shared Function RetrieveEnumCreditCeiling() As ArrayList
            Dim al As New ArrayList
            Dim CR As SPLCreditCeiling
            CR = New SPLCreditCeiling(0, "Regular")
            al.Add(CR)
            CR = New SPLCreditCeiling(1, "Temporary")
            al.Add(CR)
            Return al
        End Function
    End Class

    Public Class SPLInterest
        Private _code As Integer
        Private _desc As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal code As Integer, ByVal desc As String)
            _code = code
            _desc = desc
        End Sub

        Public Property Code() As Integer
            Get
                Return _code
            End Get
            Set(ByVal Value As Integer)
                _code = Value
            End Set
        End Property

        Public Property Desc() As String
            Get
                Return _desc
            End Get
            Set(ByVal Value As String)
                _desc = Value
            End Set
        End Property
    End Class

    Public Class SPLCreditCeiling
        Private _code As Integer
        Private _description As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal code As Integer, ByVal desc As String)
            _code = code
            _description = desc
        End Sub

        Public Property Code() As Integer
            Get
                Return _code
            End Get
            Set(ByVal Value As Integer)
                _code = Value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal Value As String)
                _description = Value
            End Set
        End Property
    End Class
End Namespace
