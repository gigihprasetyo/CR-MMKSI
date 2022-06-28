
Namespace KTB.DNet.Domain
    Public Class EnumSparePartStatus
        Public Enum SparePartStatus
            PO = 1
            Allocation
            Picking
            Packing
            Good_Issue
            Ready_For_Delivery
            Delivery_Partial
            Delivery_Complete
            Received_Partial
            Received_Complete
        End Enum

        Public Shared Function RetrieveName(ByVal EnumIndex As Integer) As String
            Dim strReturn As String = ""
            Select Case EnumIndex
                Case 1
                    strReturn = "PO"
                Case 2
                    strReturn = "Allocation"
                Case 3
                    strReturn = "Picking"
                Case 4
                    strReturn = "Packing"
                Case 5
                    strReturn = "Good Issue"
                Case 6
                    strReturn = "Ready For Delivery"
                Case 7
                    strReturn = "Delivery - Partial"
                Case 8
                    strReturn = "Delivery - Complete"
                Case 9
                    strReturn = "Received - Partial"
                Case 10
                    strReturn = "Received - Complete"
            End Select
            Return strReturn
        End Function


        Public Shared Function RetriveSparePartStatus(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim _sparePartStatusProperty As EnumSparePartStatusProperty

            If (isIncludeBlank) Then
                _sparePartStatusProperty = New EnumSparePartStatusProperty(-1, "Silahkan Pilih")
                arr.Add(_sparePartStatusProperty)
            End If
            _sparePartStatusProperty = New EnumSparePartStatusProperty(1, "PO")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(2, "Alocation")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(3, "Picking")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(4, "Packing")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(5, "Good Issue")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(6, "Ready For Delivery")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(7, "Delivery - Partial")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(8, "Delivery - Complete")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(9, "Received - Partial")
            arr.Add(_sparePartStatusProperty)

            _sparePartStatusProperty = New EnumSparePartStatusProperty(10, "Received - Complete")
            arr.Add(_sparePartStatusProperty)

            Return arr
        End Function

    End Class

    Public Class EnumSparePartStatusProperty
        Private _Val As Integer
        Private _Name As String

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

