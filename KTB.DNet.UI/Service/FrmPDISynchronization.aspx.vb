#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmPDISynchronization
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgPDI As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnUploadSave As System.Web.UI.WebControls.Button
    Protected WithEvents dfPDI As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Dim arlist As ArrayList
    Dim sHPDI As SessionHelper = New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub getPDIData()
        Dim bError As Boolean = False
        For Each arPDI As PDI In arlist
            If Not arPDI.ErrorMessage = String.Empty Then
                bError = True
                Exit For
            End If
        Next
        If Not bError Then
            sHPDI.SetSession("sesPDI", arlist)
        End If
        dtgPDI.DataSource = arlist
        dtgPDI.DataBind()
    End Sub

    Private Sub InsertPDI(ByVal sesPDI As PDI)
        Dim objPDI As PDIFacade = New PDIFacade(User)
        objPDI.Insert(sesPDI)
    End Sub

    Private Sub UpdatePDI(ByVal sesPDI As PDI)
        Dim criterias As CriteriaComposite
        Dim objPDI2 As PDI
        Dim objPDIRet As ArrayList
        Dim objPDIFacade As PDIFacade = New PDIFacade(User)
        criterias = New CriteriaComposite(New Criteria(GetType(PDI), "ChassisMaster.ID", MatchType.Exact, sesPDI.ChassisMaster.ID))
        objPDIRet = New PDIFacade(User).Retrieve(criterias)
        If objPDIRet.Count > 0 Then
            objPDI2 = objPDIRet(0)
        End If
        sesPDI.ID = objPDI2.ID
        objPDIFacade.Update(sesPDI)
    End Sub

    Private Sub savePDIData()
        Dim arList As ArrayList = CType(sHPDI.GetSession("sesPDI"), ArrayList)

        'For Each sesPDI As PDI In arList
        '    If Not IsExistCode2(sesPDI.ChassisMaster.ID) Then
        '        InsertPDI(sesPDI)  '-- Insert 
        '    Else
        '        UpdatePDI(sesPDI)  '-- Update 
        '    End If
        'Next

        Try
            For Each objPDI As PDI In arList
                If checkChassis(objPDI) = True Then
                    If checkPDI(objPDI) = True Then
                        If checkDealer(objPDI) = True Then
                            UpdatePDI(objPDI)
                        Else

                        End If
                    Else
                        InsertPDI(objPDI)
                    End If
                Else

                End If

            Next
        Catch ex As Exception
            Throw New Exception("Process Upload Gagal")
        End Try

    End Sub

    Private Function checkChassis(ByVal objPDI As PDI) As Boolean

        Dim objChassisMasterAl As ArrayList
        Dim objCriteria As CriteriaComposite

        objCriteria = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objPDI.ChassisMaster.ChassisNumber.ToString.Trim()))
        objChassisMasterAl = New ChassisMasterFacade(User).Retrieve(objCriteria)


        If objChassisMasterAl.Count <> 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function checkPDI(ByVal objPDI As PDI) As Boolean
        Dim objPDIAl As ArrayList
        Dim objCriteria As CriteriaComposite

        objCriteria = New CriteriaComposite(New Criteria(GetType(PDI), "ChassisMaster.ChassisNumber", MatchType.Exact, objPDI.ChassisMaster.ChassisNumber.ToString.Trim()))

        objPDIAl = New PDIFacade(User).Retrieve(objCriteria)

        If objPDIAl.Count <> 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function checkDealer(ByVal objPDI As PDI) As Boolean
        Dim objPDIAl As ArrayList
        Dim objCriteria As CriteriaComposite

        objCriteria = New CriteriaComposite(New Criteria(GetType(PDI), "Dealer.DealerCode", MatchType.Exact, objPDI.Dealer.DealerCode.ToString.Trim()))

        objPDIAl = New PDIFacade(User).Retrieve(objCriteria)

        If objPDIAl.Count <> 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub bindUpload()

        If (Not dfPDI.PostedFile Is Nothing) And (dfPDI.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(dfPDI.PostedFile.FileName)  '-- Source file name
            Dim DestFile As String = Path.GetTempPath & SrcFile '-- Destination file
            Try
                '-- Copy source file to destination file
                Dim objUpload As New UploadToWebServer
                objUpload.Upload(dfPDI.PostedFile.InputStream, DestFile)
                Dim parser As IParser = New PDISynChronizationParser '-- Declare parser PDI
                arlist = CType(parser.ParseNoTransaction(DestFile, "User"), ArrayList)
                getPDIData()
                savePDIData()
            Catch Exc As Exception
                MessageBox.Show("Error: " & Exc.Message)
            End Try
        Else
            MessageBox.Show("File yang anda pilih kosong")
        End If

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnUploadSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUploadSave.Click
        bindUpload()
    End Sub

    Private Sub dtgPDI_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPDI.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgPDI.CurrentPageIndex * dtgPDI.PageSize)
        End If
    End Sub

#End Region

    
End Class