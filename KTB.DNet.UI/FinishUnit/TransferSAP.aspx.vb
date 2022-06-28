Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.IO
Imports System.Text

Public Class TransferSAP
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Integer = 0
        Dim arl As ArrayList = New ArrayList                
        Dim success As Boolean = False

        Dim IsCheck As Boolean = False
        Dim sw As StreamWriter
        Dim sb As New StringBuilder
        Dim filename = String.Format("{0}{1}{2}", "jvdepoA", Date.Now.ToString("ddMMyyyyHHmmss"), ".txt")
        Dim DestFile As String = ConfigurationSettings.AppSettings.Item("InvoiceFileDirectory") & "\" & filename  '-- Destination file
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename
        Dim tmp As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAKuitansiPencairan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        arl = New DepositAKuitansiPencairanFacade(User).Retrieve(criterias)

        'For Each item As DataGridItem In dtgPengajuan.Items
        '    Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
        '    If (chk.Checked) Then
        '        CR = New CustomerRequest
        '        Dim CRFacade As Service.CustomerRequestFacade = New Service.CustomerRequestFacade(User)
        '        CR = CRFacade.Retrieve(Convert.ToInt32(dtgPengajuan.DataKeys().Item(i)))
        '        If CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Validasi Then
        '            IsCheck = True
        '            CR.Status = EnumStatusCustomerRequest.TipePengajuanCustomerRequest.Proses
        '            CR.ProcessUserID = objUser.ID
        '            arl.Add(CR)
        '            Dim ObjCity As New City
        '            ObjCity = New General.CityFacade(User).Retrieve(CR.CityID)
        '            Dim preRegion As String
        '            If CR.PrintRegion = "0" Then
        '                preRegion = "X"
        '            Else
        '                preRegion = ""
        '            End If

        '            'handle sementara untuk prearea
        '            If CR.PreArea.ToLower = "blank" Then
        '                CR.PreArea = ""
        '            End If

        '            'Untuk preArea dan kota dipisahkan dengan spasi dan bukan dengan Delimiter chr(13) (Enter)
        '            'Konfirmasi dari Heru
        '            'sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(13) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion & Chr(9) & Chr(10))
        '            sb.Append(CR.ID & Chr(9) & CR.Name1 & Chr(9) & CR.Name2 & Chr(9) & CR.Name3 & Chr(9) & CR.Alamat & Chr(9) & CR.Kelurahan & Chr(9) & CR.Kecamatan & Chr(9) & CR.PostalCode & Chr(9) & CR.PreArea & Chr(9) & ObjCity.CityCode & Chr(9) & ObjCity.Province.ProvinceCode & Chr(9) & preRegion)
        '            If tmp < CountChecked() - 1 Then
        '                sb.Append(vbNewLine)
        '            End If
        '            tmp += 1
        '        End If
        '    End If
        '    i = i + 1
        'Next

        For Each item As DepositAKuitansiPencairan In arl          
            sb.Append(item.Dealer.DealerCode & ";" & DateTime.Now.ToString("yyyyMMdd") & ";" & item.CreatedTime.ToString("yyyyMMdd") & ";" & item.AssignmentNumber & ";" & item.Description & ";" & "ZC05")
            sb.Append(vbNewLine)
        Next

        If (sb.Length > 0) Then
            If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder             
                MessageBox.Show("Data berhasil di upload ke SAP")
            Else
                MessageBox.Show("Download data gagal")
            End If
        End If

            'If IsCheck Then
            '    If (sb.Length > 0) Then

            '        If Transfer(DestFile, sb.ToString(), arl) Then         '>> Code utk upload data lsg ke SAP Folder
            '            'If Download(LocalDest, sb.ToString(), arl) Then         '>> Code utk download ke folder lokal
            '            MessageBox.Show("Data berhasil di upload ke SAP")
            '            'Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & filename)
            '        Else
            '            MessageBox.Show("Download data gagal")
            '        End If
            '    End If
            'Else
            '    MessageBox.Show("Daftar customer request belum dipilih atau status tidak valid")
            'End If
    End Sub

    Private Function Transfer(ByVal DestFile As String, ByVal Val As String, ByVal arl As ArrayList) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = ConfigurationSettings.AppSettings.Get("User")
        Dim _password As String = ConfigurationSettings.AppSettings.Get("Password")
        Dim _webServer As String = ConfigurationSettings.AppSettings.Get("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                sw = New StreamWriter(DestFile)
                sw.Write(Val)
                sw.Close()

            End If
        Catch ex As Exception
            sw.Close()
            Throw ex
            Return success
        End Try
        Return success
    End Function

End Class
