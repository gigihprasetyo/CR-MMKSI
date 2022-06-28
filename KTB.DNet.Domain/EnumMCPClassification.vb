Namespace KTB.DNet.Domain

    Public Class EnumMCPClassification
        Public Enum MCPClassification
            G1
            G2
            G3
        End Enum

        Public Shared Function RetrieveClassification() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumMCPClass

            sts = New enumMCPClass(0, "G1")
            al.Add(sts)
            sts = New enumMCPClass(1, "G2")
            al.Add(sts)
            sts = New enumMCPClass(2, "G3")
            al.Add(sts)
            Return al
        End Function
        ' add by ako 28 Sept'17 (used by fleet customer upload)
        Public Function Retrieve() As ArrayList
            Dim al As New ArrayList
            Dim sts As enumMCPClass

            sts = New enumMCPClass(0, "G1")
            al.Add(sts)
            sts = New enumMCPClass(1, "G2")
            al.Add(sts)
            sts = New enumMCPClass(2, "G3")
            al.Add(sts)
            Return al
        End Function

        ' add by ako 28 Sept'17 (used by fleet customer upload)
        Public Function GetIndex(ByVal EnumName As String) As Integer
            Dim res As Integer = -1
            Select Case EnumName
                Case "G1"
                    res = 0
                Case "G2"
                    res = 1
                Case "G3"
                    res = 2
            End Select
            Return res
        End Function

        Public Shared Function RetrieveClassification(ByVal isIncludeBlank As Boolean) As ArrayList
            Dim al As New ArrayList
            Dim sts As enumMCPClass

            If (isIncludeBlank) Then
                sts = New enumMCPClass(-1, "-Silahkan Pilih-")
                al.Add(sts)
            End If

            sts = New enumMCPClass(0, "G1")
            al.Add(sts)
            sts = New enumMCPClass(1, "G2")
            al.Add(sts)
            sts = New enumMCPClass(2, "G3")
            Return al
        End Function

    End Class

    Public Class enumMCPClass
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

