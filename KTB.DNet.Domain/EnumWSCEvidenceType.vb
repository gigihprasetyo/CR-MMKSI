Namespace KTB.DNet.Domain
    Public Class EnumWSCEvidenceType
        Private m_Action As String

        Public Enum WSCEvidenceType
            KWITANSI_WSC = 0
            SURAT_WSC = 1
            TEKNIKAL_WSC = 2

            'add changes by rudi
            REPAIR_WO = 3
            PART_BEKAS = 4
            PHOTO = 5
        End Enum

        Public ReadOnly Property WSCEvidenceTypeList() As ArrayList
            Get
                Dim list As ArrayList = New ArrayList
                Dim item As WSCEvType
                item = New WSCEvType("0", "Kwitansi WSC")
                list.Add(item)
                item = New WSCEvType("1", "Surat WSC")
                list.Add(item)
                item = New WSCEvType("2", "Teknikal WSC")
                list.Add(item)

                'add changes by rudi
                item = New WSCEvType("3", "Repair/WO")
                list.Add(item)
                item = New WSCEvType("4", "Part Bekas")
                list.Add(item)
                item = New WSCEvType("5", "Photo")
                list.Add(item)

                Return list
            End Get
        End Property

        Public Shared Function GetStringWSCEvType(ByVal WSCEvTypeId As Integer) As String
            Dim str As String = ""
            Select Case WSCEvTypeId
                Case 0
                    str = "Kwitansi WSC"
                Case 1
                    str = "Surat WSC"
                Case 2
                    str = "Teknikal WSC"
                Case 3
                    str = "Repair/WO"
                Case 4
                    str = "Part Bekas"
                Case 5
                    str = "Photo"
            End Select
            Return str
        End Function

        Public Sub New()

        End Sub
    End Class

    Public Class WSCEvType
        Private m_WSCEvidenceTypeId As String
        Private m_WSCEvidenceTypeValue As String

        Public Sub New()

        End Sub

        Public Sub New(ByVal value As String, ByVal id As String)
            m_WSCEvidenceTypeId = id
            m_WSCEvidenceTypeValue = value
        End Sub

        Property WSCEvidenceTypeId() As String
            Get
                Return m_WSCEvidenceTypeId
            End Get
            Set(ByVal Value As String)
                m_WSCEvidenceTypeId = Value
            End Set
        End Property

        Public Property WSCEvidenceTypeValue() As String
            Get
                Return m_WSCEvidenceTypeValue
            End Get
            Set(ByVal Value As String)
                m_WSCEvidenceTypeValue = Value
            End Set
        End Property
    End Class
End Namespace

