Namespace KTB.DNet.Domain
    Public Class EnumTrClass

        Public Sub New()

        End Sub

        Public Enum EnumClassType
            INCLASS_TRAINING = 1
            E_LEARNING = 2
            INHOUSE_TRAINING = 3
            FLEET_TRAINING = 4
        End Enum

        Public Shared Function GetStringValueClassType(ByVal Type As Integer) As String
            Dim str As String = ""
            If Type = 1 Then str = "Inclass Training"
            If Type = 2 Then str = "E-Learning"
            If Type = 3 Then str = "Inhouse Training"
            If Type = 4 Then str = "Fleet Training"
            Return str
        End Function

        Public Enum EnumTrClassCategory
            Others = 0
            MSTEP = 1
            GENERAL_WAJIB = 2
            NEW_MODEL = 3
            GENERAL_TIDAK_WAJIB = 4
        End Enum

        Public Shared Function RetrieveEnumTrClass() As ArrayList
            Dim al As New ArrayList
            Dim sts As TrClassCategory
            sts = New TrClassCategory(4, "General Tidak Wajib")
            al.Add(sts)
            sts = New TrClassCategory(2, "General Wajib")
            al.Add(sts)
            sts = New TrClassCategory(1, "MSTEP")
            al.Add(sts)
            sts = New TrClassCategory(3, "New Model")
            al.Add(sts)
            sts = New TrClassCategory(0, "Other")
            al.Add(sts)
            Return al
        End Function

        Public Shared Function RetrieveTrClassCategory(ByVal id As Integer) As String
            Dim arrList As ArrayList = RetrieveEnumTrClass()
            Dim ObjTrClassCategory As New TrClassCategory
            Dim retValue As String = Nothing
            For i As Integer = 0 To arrList.Count - 1
                ObjTrClassCategory = CType(arrList(i), TrClassCategory)
                If ObjTrClassCategory.ID = id Then
                    retValue = ObjTrClassCategory.Name
                    Exit For
                End If
            Next i
            Return retValue
        End Function
    End Class

    Public Class TrClassCategory
        Private _id As Integer
        Private _name As String

        Public Sub New()

        End Sub


        Public Sub New(ByVal mID As Integer, ByVal mName As String)
            _id = mID
            _name = mName
        End Sub

        Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = Value
            End Set
        End Property


        Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal Value As String)
                _name = Value
            End Set
        End Property
    End Class

End Namespace

