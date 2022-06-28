Imports System.Web.UI.WebControls

Namespace KTB.DNet.Domain

    Public Class enumStatusWSC

        Public Enum Status
            Baru
            Proses
            Selesai
        End Enum

        Private Shared _arrayListStatusWSCEvidence As ArrayList

        Public Shared ReadOnly Property ArrayListStatus()
            Get
                If (_arrayListStatusWSCEvidence Is Nothing) Then
                    _arrayListStatusWSCEvidence = New ArrayList
                    Dim listItemStatus1 As New ListItem("Baru", 0)
                    Dim listItemStatus2 As New ListItem("Proses", 1)
                    Dim listItemStatus3 As New ListItem("Selesai", 2)

                    With _arrayListStatusWSCEvidence
                        .Add(listItemStatus1)
                        .Add(listItemStatus2)
                        .Add(listItemStatus3)
                    End With

                End If
                Return _arrayListStatusWSCEvidence
            End Get
        End Property

        Public Shared Function WSCStatusDesc(ByVal WSCStatus As String) As String
            If WSCStatus = "" Then
                Return ""
            Else
                Return CType(WSCStatus, Status).ToString()
            End If

        End Function

    End Class

End Namespace
