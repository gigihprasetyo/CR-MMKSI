Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility

Public Class PopUpInputATA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim CMid As Integer = CInt(Request.QueryString("CMid"))
        'Dim POid As Integer = CInt(Request.QueryString("POid"))
        'Dim ETA As Date = CDate(Request.QueryString("ETA"))
        Dim CM As ChassisMaster = New ChassisMasterFacade(User).Retrieve(CMid)
        'Dim PO As POHeader = New POHeaderFacade(User).Retrieve(POid)

        Dim RetCMATA As ArrayList = New ChassisMasterATAFacade(User).Retrieve(CM)
        Dim CMATA As New ChassisMasterATA
        If RetCMATA.Count > 0 Then
            CMATA = RetCMATA(0)
            'CMATA.POHeader = POs
            'CMATA.ChassisMaster = CM
            'CMATA.ETA = ETA
            txtRemarks.Text = txtRemarks.Text.Trim()
            CMATA.ATA = ICATA.Value
            If txtRemarks.Text.Length > 140 Then
                CMATA.RemarkATA = txtRemarks.Text.Substring(0, 139)
            Else
                CMATA.RemarkATA = txtRemarks.Text
            End If

            Dim _return As Integer = New ChassisMasterATAFacade(User).Update(CMATA) 'Update
        Else
            MessageBox.Show("Data untuk chassis tersebut belum turun dari SAP !")
            Exit Sub
        End If

        'Dim _return As Integer = New ChassisMasterATAFacade(User).Insert(CMATA)
        'RegisterStartupScript("CloseWindow", "<script languange=javascript>window.close();</script>")
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "CallMyFunction", "CloseWindow()", True)
    End Sub
End Class