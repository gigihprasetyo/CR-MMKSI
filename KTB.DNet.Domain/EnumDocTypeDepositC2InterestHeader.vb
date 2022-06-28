Namespace KTB.DNet.Domain
    Public Class EnumDocTypeDepositC2InterestHeader
        Public Enum DepositC2InterestHeaderDocumentTypeEnum
            FilePathKwitansi
            FilePathLetter
        End Enum

        Public Shared Function RetrieveDocumentTypeString(ByVal i As Integer) As String
            Select Case i
                Case 0
                    Return "Kwitansi"
                Case 1
                    Return "Letter"
                Case Else
                    Return ""
            End Select
        End Function
    End Class

    Public Class DepositC2InterestHeaderListItem

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
