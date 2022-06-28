Namespace KTB.DNet.Domain

    Public Class EnumMarketCategory

        Public Enum MarketCategory
            Mitsubishi
            Competitor
        End Enum

        Public Function OwnBrand() As String
            Return MarketCategory.Mitsubishi
        End Function

        Public Function Competitor() As String
            Return MarketCategory.Competitor
        End Function

        Public Function RetrieveMarketCategory() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumMarketCategoryProp
            sts = New EnumMarketCategoryProp(0, "Mitsubishi")
            al.Add(sts)
            sts = New EnumMarketCategoryProp(1, "Competitor")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumMarketCategoryProp
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameStatus() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

    End Class
End Namespace

