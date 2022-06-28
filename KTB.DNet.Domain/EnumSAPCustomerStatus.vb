
Namespace KTB.DNet.Domain
    Public Class EnumSAPCustomerStatus
        Public Enum SAPCustomerStatus
            Hot_Prospect = 1
            Prospect
            Suspect
            Deal_SPK
            No_Prospect
            Canceled
        End Enum
    End Class

    Public Class EnumSAPCustStatus
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveSAPCustomerStatus(ByVal isIncludeBlank As Boolean, Optional ByVal isEntryData As Boolean = False) As ArrayList
            Dim arr As New ArrayList
            Dim emSAPCustStatus As EnumSAPCustStatus

            If (isIncludeBlank) Then
                emSAPCustStatus = New EnumSAPCustStatus(99, "")
                arr.Add(emSAPCustStatus)
            End If
            emSAPCustStatus = New EnumSAPCustStatus(1, "Hot Prospect")
            arr.Add(emSAPCustStatus)

            emSAPCustStatus = New EnumSAPCustStatus(2, "Prospect")
            arr.Add(emSAPCustStatus)

            emSAPCustStatus = New EnumSAPCustStatus(3, "Suspect")
            arr.Add(emSAPCustStatus)
            If Not isEntryData Then
                emSAPCustStatus = New EnumSAPCustStatus(4, "Deal/SPK")
                arr.Add(emSAPCustStatus)

                emSAPCustStatus = New EnumSAPCustStatus(5, "No Prospect")
                arr.Add(emSAPCustStatus)

                emSAPCustStatus = New EnumSAPCustStatus(6, "Canceled")
                arr.Add(emSAPCustStatus)
            End If

            Return arr
        End Function

        Public Shared Function RetriveSAPCustomerStatus(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emSAPCustStatus As EnumSAPCustStatus

            If (isIncludeBlank) Then
                emSAPCustStatus = New EnumSAPCustStatus(99, "")
                arr.Add(emSAPCustStatus)
            End If
            emSAPCustStatus = New EnumSAPCustStatus(1, "Hot Prospect")
            arr.Add(emSAPCustStatus)

            emSAPCustStatus = New EnumSAPCustStatus(2, "Prospect")
            arr.Add(emSAPCustStatus)

            emSAPCustStatus = New EnumSAPCustStatus(3, "Suspect")
            arr.Add(emSAPCustStatus)

            emSAPCustStatus = New EnumSAPCustStatus(4, "Deal/SPK")
            arr.Add(emSAPCustStatus)

            emSAPCustStatus = New EnumSAPCustStatus(5, "No Prospect")
            arr.Add(emSAPCustStatus)

            emSAPCustStatus = New EnumSAPCustStatus(6, "Canceled")
            arr.Add(emSAPCustStatus)
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

