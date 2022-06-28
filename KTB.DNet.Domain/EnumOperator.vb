Namespace KTB.DNet.Domain
    Public Class EnumOperator
        Public Enum [Operator]
            Equal
            Greater_Equal
            Greater_Than
            Less_Equal
            Less_Than
        End Enum
    End Class

    Public Class EnumOperatorVal
        Private _Val As Integer
        Private _Name As String

        Public Shared Function RetriveOperator(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim arr As New ArrayList
            Dim emOperator As EnumOperatorVal

            If (isIncludeBlank) Then
                emOperator = New EnumOperatorVal(99, "")
                arr.Add(emOperator)
            End If
            emOperator = New EnumOperatorVal(1, "Equal")
            arr.Add(emOperator)

            emOperator = New EnumOperatorVal(2, "Greater Equal")
            arr.Add(emOperator)

            emOperator = New EnumOperatorVal(3, "Greater Than")
            arr.Add(emOperator)

            emOperator = New EnumOperatorVal(4, "Less Equal")
            arr.Add(emOperator)

            emOperator = New EnumOperatorVal(5, "Less Than")
            arr.Add(emOperator)
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


