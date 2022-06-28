#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
#End Region

Public Class FrmFSSynchronization
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents DataFile As System.Web.UI.HtmlControls.HtmlInputFile
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

#Region "Custom Method"

    Private Sub upload()
        Dim chassisMasterAL As New ArrayList

        chassisMasterAL = getDataByUploading()

        If IsNothing(chassisMasterAL) Then
            Throw New Exception("fomat isi file salah")
        Else
            processFreeService(chassisMasterAL)
        End If


    End Sub


    Private Sub process()
        Dim objParser As IParser = New FreeServiceSynChronizationParser
        Dim arList As ArrayList = CType(objParser.ParseWithTransaction("C:\fs_status234449.txt", "User"), ArrayList)
    End Sub

    Private Function getDataByUploading() As ArrayList

        If (Not DataFile.PostedFile Is Nothing) And (DataFile.PostedFile.ContentLength > 0) Then
            Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)
            Dim DestFile As String = Path.GetTempPath & SrcFile

            Dim objUpload As New UploadToWebServer
            objUpload.Upload(DataFile.PostedFile.InputStream, DestFile)

            Dim objParser As IParser = New FreeServiceSynChronizationParser

            Dim arList As ArrayList = CType(objParser.ParseNoTransaction(DestFile, "User"), ArrayList)

            Dim bError As Boolean = False
            For Each objFreeService As FreeService In arList
                If Not objFreeService.ErrorMessage = "".Trim() Then
                    Return Nothing
                End If
            Next

            Return arList

        Else
            Throw New Exception("Pilih file yang akan di-upload.")
        End If
    End Function

    Private Sub processFreeService(ByVal objAL As ArrayList)

        Try
            For Each objFreeService As FreeService In objAL

                If checkChassisAndFsKind(objFreeService) = True Then
                    If checkFreeService(objFreeService) = True Then
                        If checkDealer(objFreeService) = True Then
                            updateFreeService(objFreeService)
                        Else

                        End If
                    Else
                        insertFreeService(objFreeService)
                    End If
                Else

                End If

            Next
        Catch ex As Exception
            Throw New Exception("Process Upload Gagal")
        End Try

    End Sub

    Private Function checkChassisAndFsKind(ByVal objFreeService As FreeService) As Boolean

        Dim objChassisMasterAL As ArrayList
        Dim objFsKindAL As ArrayList
        Dim objCriteria As CriteriaComposite

        objCriteria = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, objFreeService.ChassisMaster.ChassisNumber.ToString.Trim()))
        objChassisMasterAL = New ChassisMasterFacade(User).Retrieve(objCriteria)


        If objChassisMasterAL.Count <> 0 Then
            objCriteria = New CriteriaComposite(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, objFreeService.FSKind.KindCode.ToString.Trim()))
            objFsKindAL = New FSKindFacade(User).Retrieve(objCriteria)

            If objFsKindAL.Count <> 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Private Function checkFreeService(ByVal objFreeService As FreeService) As Boolean
        Dim objFreeServiceAL As ArrayList
        Dim objCriteria As CriteriaComposite

        objCriteria = New CriteriaComposite(New Criteria(GetType(FreeService), "FSKind.KindCode", MatchType.Exact, objFreeService.FSKind.KindCode.ToString.Trim()))
        objCriteria.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ChassisNumber", MatchType.GreaterOrEqual, objFreeService.ChassisMaster.ChassisNumber.ToString.Trim()))

        objFreeServiceAL = New FreeServiceFacade(User).Retrieve(objCriteria)

        If objFreeServiceAL.Count <> 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function checkDealer(ByVal objFreeService As FreeService) As Boolean
        Dim objFreeServiceAl As ArrayList
        Dim objCriteria As CriteriaComposite

        objCriteria = New CriteriaComposite(New Criteria(GetType(FreeService), "Dealer.DealerCode", MatchType.Exact, objFreeService.Dealer.DealerCode.ToString.Trim()))

        objFreeServiceAl = New FreeServiceFacade(User).Retrieve(objCriteria)

        If objFreeServiceAl.Count <> 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub insertFreeService(ByVal objFreeService As FreeService)

        Dim objFreeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
        objFreeServiceFacade.Insert(objFreeService)

    End Sub

    Private Sub updateFreeService(ByVal objFreeService As FreeService)

        Dim objFreeServiceFacade As New FreeServiceFacade(User)
        Dim objFreeService2 As FreeService
        Dim objCriterias As New CriteriaComposite(New Criteria(GetType(FreeService), "Dealer.ID", MatchType.Exact, objFreeService.Dealer.ID))
        objCriterias.opAnd(New Criteria(GetType(FreeService), "FSKind.ID", MatchType.Exact, objFreeService.FSKind.ID))
        objCriterias.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ID", MatchType.Exact, objFreeService.ChassisMaster.ID))

        objFreeService2 = objFreeServiceFacade.Retrieve(objCriterias).Item(0)

        objFreeService2.Dealer = objFreeService.Dealer

        objFreeService2.Dealer = objFreeService.Dealer
        objFreeService2.ChassisMaster = objFreeService.ChassisMaster
        objFreeService2.FSKind = objFreeService.FSKind
        objFreeService2.ServiceDate = objFreeService.ServiceDate
        objFreeService2.SoldDate = objFreeService.SoldDate
        objFreeService2.MileAge = objFreeService.MileAge
        objFreeService2.NotificationNumber = objFreeService.NotificationNumber
        objFreeService2.NotificationType = objFreeService.NotificationType
        objFreeService2.Reject = objFreeService.Reject
        objFreeService2.Reason = objFreeService.Reason
        objFreeService2.LabourAmount = objFreeService.LabourAmount
        objFreeService2.PartAmount = objFreeService.PartAmount
        objFreeService2.PPNAmount = objFreeService.PPNAmount
        objFreeService2.PPHAmount = objFreeService.PPHAmount

        objFreeServiceFacade.Update(objFreeService2)

    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not Page.IsPostBack Then

        End If

    End Sub

    Private Sub btnUpload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpload.Click

        Try
            upload()
            MessageBox.Show("Process Upload Berhasil")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

#End Region

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        process()
    End Sub
End Class