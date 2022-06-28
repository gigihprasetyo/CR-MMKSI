Namespace KTB.DNet.Domain
    Public Class EnumMaterialType

        Public Enum MaterialType
            Parts = 1
            Tools = 2
            Accessories = 3
            Equipment = 4
        End Enum

        Public Function RetrieveType() As ArrayList
            Dim al As New ArrayList
            Dim mtrltype As EnumMaterial
            mtrltype = New EnumMaterial(1, "Parts")
            al.Add(mtrltype)
            'Remove Tools
            'mtrltype = New EnumMaterial(2, "Tools")
            'al.Add(mtrltype)
            mtrltype = New EnumMaterial(3, "Accessories")
            al.Add(mtrltype)
            Return al
        End Function
        Public Shared Function RetrieveType(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim al As New ArrayList
            Dim mtrltype As EnumMaterial

            If (isIncludeBlank) Then
                mtrltype = New EnumMaterial(-1, "-Silahkan Pilih-")
                al.Add(mtrltype)
            End If

            mtrltype = New EnumMaterial(1, "Parts")
            al.Add(mtrltype)

            'Remove Tools
            'mtrltype = New EnumMaterial(2, "Tools")
            'al.Add(mtrltype)
            mtrltype = New EnumMaterial(3, "Accessories")
            al.Add(mtrltype)
            Return al
        End Function
        '            'If (isIncludeBlank) Then
        '    EmPaymentType = New EnumIPStatus(99, "")
        '    arr.Add(EmPaymentType)
        'If (isIncludeBlank) Then
        '    EmPaymentType = New EnumIPStatus(99, "")
        '    arr.Add(EmPaymentType)
        'End If'End If
    End Class

    Public Class EnumMaterial
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