#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports System.Collections.Generic
Imports System.Linq
#End Region

Public Class FrmDownloadCourseRegistrationDetailCS
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim sHdownLoad As SessionHelper = New SessionHelper
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Response.ContentType = "application/x-download"

        'Response.AddHeader("Content-Disposition", _
        '    New System.Text.StringBuilder("attachment;filename=").Append("Daftar Kelas.xls").ToString)
        'dtgDwnload.DataSource() = New TrClassFacade(User).Retrieve(CType(sHdownLoad.GetSession("searchRes"), CriteriaComposite))
        'dtgDwnload.DataBind()
    End Sub
    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)

        Response.ContentType = "application/x-download"

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append("Daftar Siswa Kelas CS.xls").ToString)

        If Not sHdownLoad.GetSession("sessListCourseRegistrationDetailCS") Is Nothing Then
            dtgDwnload.DataSource() = CType(sHdownLoad.GetSession("sessListCourseRegistrationDetailCS"), ArrayList)
            dtgDwnload.DataBind()
        End If


        dtgDwnload.RenderControl(writer)

    End Sub

    Private Sub dtgDwnload_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgDwnload.ItemDataBound

        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNamaSiswa As Label = CType(e.Item.FindControl("lblNamaSiswa"), Label)
            Dim lblMulaiKerja As Label = CType(e.Item.FindControl("lblMulaiKerja"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblValidasi As Label = CType(e.Item.FindControl("lblValidasi"), Label)
            Dim lblClassCode As Label = CType(e.Item.FindControl("lblClassCode"), Label)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgDwnload.CurrentPageIndex * dtgDwnload.PageSize)
            lblNoReg.Text = data.TrTrainee.ID
            lblNamaSiswa.Text = data.TrTrainee.Name
            lblDealerCode.Text = data.Dealer.DealerCode
            lblMulaiKerja.Text = data.TrTrainee.StartWorkingDate.DateToString()
            lblClassCode.Text = data.TrClass.ClassCode

            Try
                Dim jobPosition As JobPosition = New JobPositionFacade(User).Retrieve(data.TrTrainee.JobPosition)
                lblPosisi.Text = jobPosition.Description
            Catch ex As Exception
                lblPosisi.Text = ""
            End Try

            If data.ID.Equals(0) Then
                lblStatus.Text = "Draft"
            Else

                Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = data.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = 3)

                If trTraineeSalesmanHeader.Status = EnumTrTrainee.TrTraineeStatus.Deactive Then
                    'e.Item.ForeColor = Color.White
                    ' e.Item.BackColor = Color.Gray
                    lblStatus.Text = "Resign"
                Else
                    If data.Status = EnumTrClassRegistration.DataStatusType.Invite Then
                        'e.Item.BackColor = Color.Yellow
                    ElseIf data.Status = EnumTrClassRegistration.DataStatusType.Register Then
                        'e.Item.BackColor = Color.Cyan
                        lblValidasi.Text = data.RegistrationDate
                    ElseIf data.Status = EnumTrClassRegistration.DataStatusType.Reject Then
                        'e.Item.BackColor = Color.OrangeRed
                    End If


                    Dim enumRegis As New EnumTrClassRegistration
                    lblStatus.Text = enumRegis.StatusByIndex(data.Status)

                End If
            End If
        End If

    End Sub

   
End Class
