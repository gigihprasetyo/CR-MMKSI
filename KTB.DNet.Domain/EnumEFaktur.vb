Namespace KTB.DNet.Domain
    Public Class EnumEFaktur
        Public Enum EnumEFakturTransactionType
            MSPEXTENDED = 1
        End Enum

        Public Enum EnumEFakturStatus
            VALID = 1
            NOT_VALID = 2
            NOT_EXISTS = 3
            VALID_DOCUMENT_NOT_MATCH = 4
        End Enum

        Public Shared Function GetRemarkByEnumTransactionType(ByVal EnumEFakturTransactionType As Integer) As String
            Dim result As String = String.Empty
            Select Case EnumEFakturTransactionType
                Case 1
                    result = "MSP Extended"
            End Select
            Return result
        End Function

        Public Shared Function GetRemarkByEnumStatus(ByVal EnumEFakturStatus As Integer) As String
            Dim result As String = String.Empty
            Select Case EnumEFakturStatus
                Case 1
                    result = "Valid"
                Case 2
                    result = "Debit Number tidak sesuai"
                Case 3
                    result = "Debit Number tidak terdaftar"
                Case 4
                    result = "EFaktur tidak sesuai"
            End Select

            Return result
        End Function

        Public Function RetrieveTransactionType() As ArrayList
            Dim al As New ArrayList
            Dim typ As EnumTransactionTypeProp
            typ = New EnumTransactionTypeProp(1, "MSP Extended")
            al.Add(typ)
            Return al
        End Function

    End Class

    Public Class EnumTransactionTypeProp
        Private _val As Integer
        Private _name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _name = name
        End Sub
        Public Property ValTransactionType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameTransactionType() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property

    End Class
End Namespace
