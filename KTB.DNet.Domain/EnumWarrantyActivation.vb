Namespace KTB.DNet.Domain
    Public Class EnumWarrantyActivation
        Public Enum WAStatus
            Tidak_Aktif
            Proses
            Aktif
        End Enum

        Public Shared Function GetStringInformationType(ByVal Type As Integer) As String
            Dim str As String = ""
            Select Case Type
                Case 0
                    str = "Tidak Aktif"
                Case 1
                    str = "Proses"
                Case 2
                    str = "Aktif"
            End Select
            Return str
        End Function


    End Class
    Public Class EnumWarrantyActivationOp
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveInformationType(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim EnumInformationTypeOp As EnumInformationTypeOp

            If (isIncludeBlank) Then
                EnumInformationTypeOp = New EnumInformationTypeOp(0, "Tidak Aktif")
                arr.Add(EnumInformationTypeOp)
            End If
            
            EnumInformationTypeOp = New EnumInformationTypeOp(1, "Proses")
            arr.Add(EnumInformationTypeOp)

            EnumInformationTypeOp = New EnumInformationTypeOp(2, "Aktif")
            arr.Add(EnumInformationTypeOp)

            Return arr
        End Function

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _Val = val
            _Name = name
        End Sub

        Public Property ValStatus() As Integer
            Get
                Return _Val
            End Get
            Set(ByVal Value As Integer)
                _Val = Value
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