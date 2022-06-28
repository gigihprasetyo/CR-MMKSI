#Region "Summary"
'// ===========================================================================		
'// Author Name   : 
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 27/09/2005
'// ===========================================================================		
#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports Ktb.DNet.Utility
Imports KTB.DNet.Parser
Imports System.IO

#End Region
Public Class FrmUploadChassisMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnStore As System.Web.UI.WebControls.Button
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgColorUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgVehicleColor As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnPrint As System.Web.UI.WebControls.Button
    Protected WithEvents btnClose As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile

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
    Private Sub BindUpload()

        btnStore.Enabled = False  '-- Disable <Simpan> button
        dgColorUpload.Visible = True

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = Server.MapPath("") & "\..\DataFile\" & SrcFile  '-- Destination file
            Try
                DataFile.PostedFile.SaveAs(DestFile)  '-- Copy source file to destination file

                Dim parser As IParser = New VehicleColorParser  '-- Declare parser Vehicle color

                ''-- Parse data file and store result into list
                'Dim arList As ArrayList = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)

                ''-- Check errors if any
                'Dim bError As Boolean = False
                'For Each vicColor As VechileColor In arList
                '    If Not vicColor.ErrorMessage = String.Empty Then
                '        bError = True
                '        Exit For
                '    End If
                'Next

                'If Not bError Then
                '    btnStore.Enabled = True '-- Enable <Simpan> button
                '    Session.Add("vicColor", arList)  '-- Store arList into session
                'End If

                ''-- Bind list to datagrid
                'dgColorUpload.DataSource = arList
                'dgColorUpload.DataBind()

            Catch Exc As Exception
                MessageBox.Show("Error: " & Exc.Message)
            End Try
        Else
            MessageBox.Show("Pilih file yang akan di-upload.")
        End If

    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        BindUpload()
    End Sub
End Class
