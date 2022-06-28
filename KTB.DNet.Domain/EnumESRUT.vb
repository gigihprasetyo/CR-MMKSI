Namespace KTB.DNet.Domain

    Public Class EnumESRUT

        Public Enum ESRUTItemStatusDealer
            READY_TO_PRINT = 1
            NOT_AVAILABLE = 2
        End Enum

        Public Enum ESRUTItemStatus
            VALID = 1
            NOT_VALID = 2
            NOT_EXISTS = 3
            VALID_DOCUMENT_NOT_MATCH = 4
            NOT_VALID_DOCUMENT_NOT_MATCH = 5
            NOT_EXIST_DOCUMENT_NOT_MATCH = 6
        End Enum

        Public Shared Function GetRemarkByEnum(ByVal ESRUTItemStatus As Integer) As String
            Dim result As String = String.Empty
            Select Case ESRUTItemStatus
                Case 1
                    result = "Valid"
                Case 2
                    result = "Nomor Mesin tidak sesuai"
                Case 3
                    result = "Nomor chassis tidak terdaftar"
                Case 4
                    result = "ESRUT tidak sesuai"
                Case 5
                    result = "ESRUT dan nomor mesin tidak sesuai"
                Case 6
                    result = "ESRUT dan Nomor chassis tidak terdaftar"
            End Select

            Return result
        End Function

        Public Shared Function GetRemarkByEnumDealer(ByVal ESRUTItemStatusDealer As Integer) As String
            Dim result As String = String.Empty
            Select Case ESRUTItemStatusDealer
                Case 1
                    result = "Siap dicetak"
                Case 2
                    result = "Nomor ESRUT tidak tersedia"
            End Select

            Return result
        End Function

     

    End Class
End Namespace
