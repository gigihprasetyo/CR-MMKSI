Imports System.Web.UI.WebControls

Namespace KTB.DNet.Domain

    Public Class EnumStatusRevisionPayment

        Public Enum Status
            Baru
            Validasi
            Konfirmasi
            Proses
            Selesai
        End Enum

        Private Shared _arrayListStatusRevisionPayment As ArrayList

        Public Shared ReadOnly Property ArrayListStatus()
            Get
                If (_arrayListStatusRevisionPayment Is Nothing) Then
                    _arrayListStatusRevisionPayment = New ArrayList
                    Dim listItemStatus1 As New ListItem("Baru", 0)
                    Dim listItemStatus2 As New ListItem("Validasi", 1)
                    Dim listItemStatus3 As New ListItem("Konfirmasi", 2)
                    Dim listItemStatus4 As New ListItem("Proses", 3)
                    Dim listItemStatus5 As New ListItem("Selesai", 4)

                    With _arrayListStatusRevisionPayment
                        .Add(listItemStatus1)
                        .Add(listItemStatus2)
                        .Add(listItemStatus3)
                        .Add(listItemStatus4)
                        .Add(listItemStatus5)
                    End With

                End If
                Return _arrayListStatusRevisionPayment
            End Get
        End Property

        Public Shared Function RevisionPaymentStatusDesc(ByVal RevisionPaymentStatus As String) As String
            If RevisionPaymentStatus = "" Then
                Return ""
            Else
                Return CType(RevisionPaymentStatus, Status).ToString()
            End If

        End Function

    End Class

End Namespace
