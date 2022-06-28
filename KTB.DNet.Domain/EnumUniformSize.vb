Namespace KTB.DNet.Domain
    Public Class EnumUniformSize
        Public Enum UniformSize
            'Ukuran Seragam
            Silahkan_Pilih
            S
            M
            L
            XL
            XXL
        End Enum

        Public Function GetName(ByVal unifId As Integer) As String

            Select Case unifId
                Case 0
                    Return "Silahkan Pilih"
                Case 1
                    Return "S"
                Case 2
                    Return "M"
                Case 3
                    Return "L"
                Case 4
                    Return "XL"
                Case 5
                    Return "XXL"
                Case Else
                    Return ""

            End Select

        End Function


        Public Function RetrieveUniformSize() As ArrayList
            Dim al As New ArrayList
            Dim Ufs As EnumUnifSize
            Ufs = New EnumUnifSize(0, "Silahkan Pilih")
            al.Add(Ufs)
            Ufs = New EnumUnifSize(1, "S")
            al.Add(Ufs)
            Ufs = New EnumUnifSize(2, "M")
            al.Add(Ufs)
            Ufs = New EnumUnifSize(3, "L")
            al.Add(Ufs)
            Ufs = New EnumUnifSize(4, "XL")
            al.Add(Ufs)
            Ufs = New EnumUnifSize(5, "XXL")
            al.Add(Ufs)
            Return al
        End Function

    End Class

    Public Class EnumUnifSize
        Private _val As Integer
        Private _Name As String

        Public Sub New(ByVal val As Integer, ByVal name As String)
            _val = val
            _Name = name
        End Sub

        Public Property ValType() As Integer
            Get
                Return _val
            End Get
            Set(ByVal Value As Integer)
                _val = Value
            End Set
        End Property

        Property NameType() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property
    End Class

End Namespace
