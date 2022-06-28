Namespace KTB.DNet.Domain

    Public Class SPPOOrderType

        Public Const Emergency As String = "E"
        Public Const Regular As String = "R"
        Public Const Khusus As String = "K"
        Public Const Indent As String = "I"
        Public Const OtherReguler As String = "Z"
        Public Const OtherEmergency As String = "Y"
        Public Const EmergencyPQR As String = "P"
        Public Const EmergencyNonBO As String = "X"

        Public Enum EnumOrderType
            Emergency
            Regular
            Khusus
            Indent
            OtherReguler
            OtherEmergency
            EmergencyPQR
            EmergencyNonBO
        End Enum

        Public Shared Function OrderType(ByVal OrdType As String) As String
            Dim ht As Hashtable = New Hashtable
            ht.Add("E", "Emergency")
            ht.Add("R", "Regular")
            ht.Add("K", "Khusus")
            ht.Add("I", "Indent")
            ht.Add("Z", "Other Reguler")
            ht.Add("Y", "Other Emergency")
            ht.Add("P", "Emergency PQR")
            ht.Add("X", "Emergency Non Back Order")
            Return ht(OrdType)
        End Function

        Public Function RetrieveOrderType() As ArrayList
            Dim al As New ArrayList
            Dim sts As EnumOrderTypeProp
            sts = New EnumOrderTypeProp("E", "Emergency")
            al.Add(sts)
            sts = New EnumOrderTypeProp("R", "Regular")
            al.Add(sts)
            sts = New EnumOrderTypeProp("K", "Khusus")
            al.Add(sts)
            sts = New EnumOrderTypeProp("I", "Indent")
            al.Add(sts)
            sts = New EnumOrderTypeProp("Z", "Other Reguler")
            al.Add(sts)
            sts = New EnumOrderTypeProp("Y", "Other Emergency")
            al.Add(sts)

            sts = New EnumOrderTypeProp("P", "Emergency PQR")
            al.Add(sts)
            sts = New EnumOrderTypeProp("X", "Emergency Non Back Order")
            al.Add(sts)
            Return al
        End Function

    End Class

    Public Class EnumOrderTypeProp
        Private _val As String
        Private _Name As String

        Public Sub New(ByVal val As String, ByVal name As String)
            _val = val
            _Name = name
        End Sub
        Public Property ValStatus() As String
            Get
                Return _val
            End Get
            Set(ByVal Value As String)
                _val = Value
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

    'Public Class SPPOProcessCode
    '    Public Const S_SENT As String = "S"
    '    Public Const P_PROCEED As String = "P"
    '    Public Const C_CANCEL As String = "C"
    '    Public Const X_CANCEL_KTB As String = "X"
    '    Public Const O_CANCEL_ORDER As String = "O"
    '    Public Const T_REJECT As String = "T"
    '    Public Const F_FINISH As String = "F"
    '    Public Const A_ALOCATION As String = "A"
    '    Public Const R_RELEASE As String = "R"
    '    Public Const V_ORDER_TO_VENDOR As String = "V"
    '    Public Const N_NEW As String = "N"

    '    Public Const S_SENT_DESC As String = "KIRIM"
    '    Public Const P_PROCEED_DESC As String = "PROSES"
    '    Public Const C_CANCEL_DESC As String = "BATAL"
    '    Public Const X_CANCEL_KTB_DESC As String = "BATAL KTB"
    '    Public Const O_CANCEL_ORDER_DESC As String = "BATAL ORDER DEALER"
    '    Public Const T_REJECT_DESC As String = "TOLAK"
    '    Public Const F_FINISH_DESC As String = "SELESAI"
    '    Public Const A_ALOCATION_DESC As String = "ALOKASI SEBAGIAN"
    '    Public Const R_RELEASE_DESC As String = "RILIS"
    '    Public Const V_ORDER_TO_VENDOR_DESC As String = "ORDER TO VENDOR"
    '    Public Const N_NEW_DESC As String = "BARU"

    '    Public Const LOOKUP_N_NEW As Integer = 0
    '    Public Const LOOKUP_S_SENT As Integer = 1
    '    Public Const LOOKUP_P_PROCEED As Integer = 2
    '    Public Const LOOKUP_C_CANCEL As Integer = 3
    '    Public Const LOOKUP_X_CANCEL_KTB As Integer = 4
    '    Public Const LOOKUP_O_CANCEL_ORDER As Integer = 5
    '    Public Const LOOKUP_T_REJECT As Integer = 6
    '    Public Const LOOKUP_F_FINISH As Integer = 7
    '    Public Const LOOKUP_A_ALOCATION As Integer = 8
    '    Public Const LOOKUP_R_RELEASE As Integer = 9
    '    Public Const LOOKUP_V_ORDER_TO_VENDOR As Integer = 10

    '    Public Enum EnumOrderType
    '        Emergency
    '        Regular
    '        Khusus
    '        Indent
    '    End Enum

    '    'Public Shared Sub RetrieveProcessCodeForDealer(ByVal listbox As System.Web.UI.WebControls.ListBox)
    '    '    Dim arl As ArrayList = New ArrayList
    '    '    arl.Add(New EnumOrderTypeProp(N_NEW, "Baru"))
    '    '    arl.Add(New EnumOrderTypeProp(S_SENT, "Kirim"))
    '    '    arl.Add(New EnumOrderTypeProp(P_PROCEED, "Process"))
    '    '    arl.Add(New EnumOrderTypeProp(F_FINISH, "Selesai"))
    '    '    arl.Add(New EnumOrderTypeProp(C_CANCEL, "Batal"))
    '    '    arl.Add(New EnumOrderTypeProp(O_CANCEL_ORDER, "Batal Order"))
    '    '    listbox.DataTextField = "NameStatus"
    '    '    listbox.DataValueField = "ValStatus"
    '    '    listbox.DataSource = arl
    '    '    listbox.DataBind()
    '    'End Sub

    '    'Public Shared Sub RetrieveProcessCodeUpdateForDealer(ByVal ddl As System.Web.UI.WebControls.DropDownList)
    '    '    Dim arl As ArrayList = New ArrayList
    '    '    arl.Add(New EnumOrderTypeProp("0", "Silahkan Pilih"))
    '    '    arl.Add(New EnumOrderTypeProp(S_SENT, "Kirim"))
    '    '    arl.Add(New EnumOrderTypeProp(C_CANCEL, "Batal"))
    '    '    arl.Add(New EnumOrderTypeProp(O_CANCEL_ORDER, "Batal Order"))
    '    '    ddl.DataTextField = "NameStatus"
    '    '    ddl.DataValueField = "ValStatus"
    '    '    ddl.DataSource = arl
    '    '    ddl.DataBind()
    '    'End Sub

    '    'Public Shared Sub RetrieveProcessCodeForKTB(ByVal listbox As System.Web.UI.WebControls.ListBox)
    '    '    Dim arl As ArrayList = New ArrayList
    '    '    arl.Add(New EnumOrderTypeProp(S_SENT, "Baru"))
    '    '    arl.Add(New EnumOrderTypeProp(R_RELEASE, "Rilis"))
    '    '    arl.Add(New EnumOrderTypeProp(F_FINISH, "Selesai"))
    '    '    arl.Add(New EnumOrderTypeProp(C_CANCEL, "Batal"))
    '    '    arl.Add(New EnumOrderTypeProp(O_CANCEL_ORDER, "Batal Order"))
    '    '    arl.Add(New EnumOrderTypeProp(X_CANCEL_KTB, "Batal KTB"))
    '    '    arl.Add(New EnumOrderTypeProp(V_ORDER_TO_VENDOR, "Order To Vendor"))
    '    '    arl.Add(New EnumOrderTypeProp(T_REJECT, "Tolak"))
    '    '    listbox.DataTextField = "NameStatus"
    '    '    listbox.DataValueField = "ValStatus"
    '    '    listbox.DataSource = arl
    '    '    listbox.DataBind()
    '    'End Sub

    '    'Public Shared Sub RetrieveProcessCodeUpdateForKTB(ByVal ddl As System.Web.UI.WebControls.DropDownList)
    '    '    Dim arl As ArrayList = New ArrayList
    '    '    arl.Add(New EnumOrderTypeProp("0", "Silahkan Pilih"))
    '    '    arl.Add(New EnumOrderTypeProp(R_RELEASE, "Rilis"))
    '    '    arl.Add(New EnumOrderTypeProp(V_ORDER_TO_VENDOR, "Order To Vendor"))
    '    '    arl.Add(New EnumOrderTypeProp(T_REJECT, "Tolak"))
    '    '    arl.Add(New EnumOrderTypeProp(X_CANCEL_KTB, "Batal Selesai"))
    '    '    arl.Add(New EnumOrderTypeProp(F_FINISH, "Selesai"))
    '    '    ddl.DataTextField = "NameStatus"
    '    '    ddl.DataValueField = "ValStatus"
    '    '    ddl.DataSource = arl
    '    '    ddl.DataBind()
    '    'End Sub

    'End Class

End Namespace

