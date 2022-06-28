#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.VehicleData
Imports KTB.DNet.Parser
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Configuration
#End Region

Public Class FrmDownloadListVDH
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgData As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Private Variables"

    Private ssHelper As SessionHelper = New SessionHelper

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.EnableViewState = False
        SetDownload()
        If Not IsPostBack Then
            BindDataGrid()
        End If
    End Sub

#End Region

#Region "Custom Method"

    Private Function SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Histori Kendaraan-DaftarHistoriKendaraan.xls").Append("""").ToString
        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)
    End Function

    Private Sub BindDataGrid()
        Dim arl As ArrayList = ssHelper.GetSession("DownloadableVDH")
        dtgData.DataSource = arl
        dtgData.DataBind()
    End Sub
    Private Sub BindDataGridX()
        'Dim arl As ArrayList = ssHelper.GetSession("DownloadableVDH")
        Dim list As ArrayList
        Dim criterias As CriteriaComposite = ssHelper.GetSession("ListVDH")
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) And (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) Then
            sortColl.Add(New Sort(GetType(VDH), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        Else
            sortColl = Nothing
        End If

        If Not criterias Is Nothing Then
            list = New VDHFacade(User).Retrieve(criterias, sortColl)
        Else
            list = New ArrayList
        End If
        dtgData.DataSource = list
        dtgData.DataBind()
    End Sub


#End Region
    

  
    Private Sub dtgData_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgData.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim _serial As Label = CType(e.Item.FindControl("lblGridserialNo"), Label)
            Dim _chassisNo As Label = CType(e.Item.FindControl("lblGridChassisNo"), Label)
            Dim _MMCLot As Label = CType(e.Item.FindControl("lblMMCLot"), Label)


            Dim objVdh As VDH = e.Item.DataItem

            Select Case objVdh.Serial.Trim.Length
                Case 0
                    _serial.Text = "'000000'"

                Case 1
                    _serial.Text = "'00000" & objVdh.Serial.Trim.ToString & "'"

                Case 2
                    _serial.Text = "'0000" & objVdh.Serial.Trim.ToString & "'"

                Case 3
                    _serial.Text = "'000" & objVdh.Serial.Trim.ToString & "'"
                Case 4
                    _serial.Text = "'00" & objVdh.Serial.Trim.ToString & "'"
                Case 5
                    _serial.Text = "'0" & objVdh.Serial.Trim.ToString & "'"
                Case 6
                    _serial.Text = "'" & objVdh.Serial.Trim.ToString & "'"
            End Select

            Select Case objVdh.MMCLotNo.Trim.Length
                Case 0
                    _MMCLot.Text = "'0000000'"

                Case 1
                    _MMCLot.Text = "'000000" & objVdh.MMCLotNo.Trim.ToString & "'"

                Case 2
                    _MMCLot.Text = "'00000" & objVdh.MMCLotNo.Trim.ToString & "'"

                Case 3
                    _MMCLot.Text = "'0000" & objVdh.MMCLotNo.Trim.ToString & "'"
                Case 4
                    _MMCLot.Text = "'000" & objVdh.MMCLotNo.Trim.ToString & "'"
                Case 5
                    _MMCLot.Text = "'00" & objVdh.MMCLotNo.Trim.ToString & "'"
                Case 6
                    _MMCLot.Text = "'0" & objVdh.MMCLotNo.Trim.ToString & "'"

                Case 7
                    _MMCLot.Text = "'" & objVdh.MMCLotNo.Trim.ToString & "'"
            End Select

            Select Case objVdh.ChassisNo.Trim.Length
                Case 0
                    _chassisNo.Text = "'000000'"

                Case 1
                    _chassisNo.Text = "'00000" & objVdh.ChassisNo.Trim.ToString & "'"

                Case 2
                    _chassisNo.Text = "'0000" & objVdh.ChassisNo.Trim.ToString & "'"

                Case 3
                    _chassisNo.Text = "'000" & objVdh.ChassisNo.Trim.ToString & "'"
                Case 4
                    _chassisNo.Text = "'00" & objVdh.ChassisNo.Trim.ToString & "'"
                Case 5
                    _chassisNo.Text = "'0" & objVdh.ChassisNo.Trim.ToString & "'"
                Case 6
                    _chassisNo.Text = "'" & objVdh.ChassisNo.Trim.ToString & "'"
            End Select




        End If
    End Sub
End Class
