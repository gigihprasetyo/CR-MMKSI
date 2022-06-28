Namespace KTB.DNet.Domain
    Public Class EnumSalesmanUnit
        Public Enum SalesmanUnit
            Sparepart = 0
            Unit
            Mekanik
            Kosong
            CS
        End Enum

    End Class

    Public Class EnumSalesUnit
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveSalesmanUnit() As ArrayList
            Dim arr As New ArrayList
            Dim emSalesUnit As EnumSalesUnit
            emSalesUnit = New EnumSalesUnit(0, "Sparepart")
            arr.Add(emSalesUnit)

            emSalesUnit = New EnumSalesUnit(1, "Unit")
            arr.Add(emSalesUnit)

            emSalesUnit = New EnumSalesUnit(2, "Mekanik")
            arr.Add(emSalesUnit)

            Return arr
        End Function

        Public Shared Function RetriveSalesmanUnit(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emSalesUnit As EnumSalesUnit
            If (isIncludeBlank) Then
                emSalesUnit = New EnumSalesUnit(99, "")
                arr.Add(emSalesUnit)
            End If
            emSalesUnit = New EnumSalesUnit(0, "Sparepart")
            arr.Add(emSalesUnit)

            emSalesUnit = New EnumSalesUnit(1, "Unit")
            arr.Add(emSalesUnit)

            emSalesUnit = New EnumSalesUnit(2, "Mekanik")
            arr.Add(emSalesUnit)

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