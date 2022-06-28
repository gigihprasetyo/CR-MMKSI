
#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Configuration
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade
#End Region

Namespace KTB.DNet.Parser

    Public Class MontlyDocumentParser
        Inherits AbstractParser

#Region "Private variable"
        Dim DEPOSIT_B_REPORT_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("DEPOSIT_B_REPORT_FILE_NAME")
        Dim KWITANSI_WARANTY_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("KWITANSI_WARANTY_FILE_NAME")
        Dim KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME")
        Dim PDI_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("PDI_LETTER_FILE_NAME")
        Dim FREE_SERVICE_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("FREE_SERVICE_LETTER_FILE_NAME")
        Dim FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME")
        Dim WARANTY_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("WARANTY_LETTER_FILE_NAME")
        Dim WARANTY_STATUS_LIST_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("WARANTY_STATUS_LIST_FILE_NAME")
        Dim PERIODICAL_MAINTENANCE_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("PERIODICAL_MAINTENANCE_LETTER_FILE_NAME")
        Dim KUDEPB_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("KUDEPB_FILE_NAME")
        Dim INDEPB_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("INDEPB_FILE_NAME")
        Dim PDI_LETTER2_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("PDI_LETTER2_FILE_NAME")

        Dim FL_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("FL_LETTER_FILE_NAME")
        Dim KFL_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("KFL_LETTER_FILE_NAME")

        Dim FREE_MAINTENANCE_LETTER_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("FREE_MAINTENANCE_LETTER_FILE_NAME")
        Dim KWITANSI_FREE_MAINTENANCE_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("KWITANSI_FREE_MAINTENANCE_FILE_NAME")
        Dim Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME")
        Dim Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME")
        Dim Free_Maintenance_and_campaign_letter_FILE_NAME As String = KTB.DNet.Lib.WebConfig.GetValue("Free_Maintenance_and_campaign_letter_FILE_NAME")

        Dim _montlyDocument As MonthlyDocument
        Dim _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()

        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                _montlyDocument = parseMontlyDocument(fileName, user)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MontlyDocumentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.MontlyDocumentParser, BlockName)
                Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try

        End Function

        Protected Overrides Function DoTransaction() As Integer
            If Not _montlyDocument Is Nothing Then
                Dim _montlyDocumentFacade As MonthlyDocumentFacade = New MonthlyDocumentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Try
                    Dim oldMontlyDocument As MonthlyDocument = isMontlyDocumentExist(_montlyDocument)
                    If oldMontlyDocument.id > 0 Then
                        'oldMontlyDocument.FileSize = _montlyDocument.FileSize
                        '_montlyDocumentFacade.Update(oldMontlyDocument)
                        Return 0
                    Else
                        _montlyDocumentFacade.Insert(_montlyDocument)
                        Return 1
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MontlyDocumentParse.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.MontlyDocumentParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & _fileName & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            End If
        End Function

        Private Function isMontlyDocumentExist(ByVal objMontlyDocument As MonthlyDocument) As MonthlyDocument
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Kind", MatchType.Exact, objMontlyDocument.Kind))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeMonth", MatchType.Exact, objMontlyDocument.PeriodeMonth))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "PeriodeYear", MatchType.Exact, objMontlyDocument.PeriodeYear))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "Dealer.ID", MatchType.Exact, objMontlyDocument.Dealer.ID))
            If Not IsNothing(objMontlyDocument.ProductCategory) AndAlso objMontlyDocument.ProductCategory.ID > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.MonthlyDocument), "ProductCategory.ID", MatchType.Exact, objMontlyDocument.ProductCategory.ID))
            End If

            Dim MDList As ArrayList = New MonthlyDocumentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If MDList.Count > 0 Then
                Return CType(MDList(0), MonthlyDocument)
            Else
                Return New MonthlyDocument
            End If
        End Function

        Private Function RetrieveDealer(ByVal code As String) As Dealer
            Dim _dealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Return _dealerFacade.Retrieve(code)
        End Function

        Private Function parseMontlyDocument(ByVal fileName As String, ByVal user As String) As MonthlyDocument
            Dim finfo As FileInfo = New FileInfo(fileName)
            Dim objMontDoc As MonthlyDocument = New MonthlyDocument
            If finfo.Exists Then
                Dim fName As String = finfo.Name.Substring(0, finfo.Name.Length - 4)
                Dim str() As String = fName.Split("_")
                Dim _type = str(0).Substring(0, 6).ToUpper
                Dim _periode As String = str(1)
                Dim _dealer As String = str(2)
                Dim oPC As ProductCategory
                Dim oPCFac As New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

                'Dim path As String = KTB.DNet.Lib.WebConfig.GetValue("WEBMONTLYDOCUMENT_FOLDER") & "\" & _type & "\" & finfo.Name
                Dim path As String = "MONTLYDOCUMENT" & "\" & _type & "\" & finfo.Name
                objMontDoc.FileName = path
                objMontDoc.FileSize = finfo.Length
                If fName.Trim.ToLower.EndsWith("_mmc") Then
                    oPC = oPCFac.Retrieve("MMC")
                ElseIf fName.Trim.ToLower.EndsWith("_mftbc") Then
                    oPC = oPCFac.Retrieve("MFTBC")
                Else
                    oPC = oPCFac.Retrieve("ALL")
                End If
                objMontDoc.ProductCategory = oPC
                Try
                    objMontDoc.PeriodeMonth = _periode.Substring(4, 2)
                    objMontDoc.PeriodeYear = _periode.Substring(0, 4)
                Catch ex As Exception
                    Throw ex
                End Try
                Dim objDealer As Dealer = RetrieveDealer(_dealer)
                If objDealer.ID > 0 Then
                    objMontDoc.Dealer = objDealer
                Else
                    Throw New Exception("Dealer Tidak Ketemu")
                End If
                Select Case _type
                    Case Is = DEPOSIT_B_REPORT_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Report
                    Case Is = FREE_SERVICE_CAMPAIGN_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Campaign_Letter
                    Case Is = KWITANSI_WARANTY_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Warranty
                    Case Is = KWITANSI_FREE_SERVICE_CAMPAIGN_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Service_Campaign
                    Case Is = PDI_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.PDI_Letter
                    Case Is = PDI_LETTER2_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kend_Belum_PDI_List
                    Case Is = FREE_SERVICE_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Service_Regular_Letter
                    Case Is = WARANTY_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Letter
                    Case Is = WARANTY_STATUS_LIST_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Warranty_ESP_Status_List
                    Case Is = PERIODICAL_MAINTENANCE_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Periodical_Maintenance_Letter
                    Case Is = KUDEPB_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Kwitansi
                    Case Is = INDEPB_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Deposit_B_Interest

                    Case Is = FL_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_ESP_Labour_Letter

                    Case Is = KFL_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_ESP_Free_Labour

                    Case Is = FREE_MAINTENANCE_LETTER_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_Letter

                    Case Is = KWITANSI_FREE_MAINTENANCE_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance

                    Case Is = Kwitansi_Warranty_Spare_Part_Accessories_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Warranty_Spare_Part_Accessories

                    Case Is = Kwitansi_Free_Maintenance_and_Campaign_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Kwitansi_Free_Maintenance_and_Campaign

                    Case Is = Free_Maintenance_and_campaign_letter_FILE_NAME
                        objMontDoc.Kind = MonthlyDocumentType.MonthlyDocumentTypeEnum.Free_Maintenance_and_campaign_letter
                End Select
            Else
                Throw New Exception("File Tidak Ditemukan")
            End If
            Return objMontDoc
        End Function



        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
#End Region

#Region "Private Methods"


#End Region


    End Class

End Namespace