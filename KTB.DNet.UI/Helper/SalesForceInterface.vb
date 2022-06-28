Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.WebApi.Models
Imports KTB.DNet.Domain.Search
Imports System.Text.RegularExpressions


Public Class SalesForceInterface
    Inherits System.Web.UI.Page

    Private _valTransferToSF As String
    Private _accountID As String
    Private _strJson As String

    Public Function InsertOportunity(ByVal _spkHeader As SPKHeader, ByVal IsSms As Boolean) As Boolean
        Dim vReturn As Boolean = True
        Dim msg As String = String.Empty
        GetConfig()

        If _valTransferToSF = "1" Then
            Try
                GetAccount()
                'Insert oportunity
                Dim objParam As KTB.DNet.WebApi.Models.paramWalkinOpportunity = New KTB.DNet.WebApi.Models.paramWalkinOpportunity()
                'objParam.Dealer__c = String.Empty
                objParam.Dealer_Code__c = _spkHeader.Dealer.DealerCode
                objParam.Dealer_Name__c = _spkHeader.Dealer.DealerName
                objParam.Consumen_Type__c = EnumTipePelangganCustomerRequest.RetrieveTipePelangganCustomerRequest(_spkHeader.SPKCustomer.TipeCustomer)
                objParam.Name = _spkHeader.SPKCustomer.Name1
                objParam.Address__c = _spkHeader.SPKCustomer.Alamat
                If EmailAddressCheck(_spkHeader.SPKCustomer.Email) Then
                    objParam.Email__c = _spkHeader.SPKCustomer.Email
                Else
                    objParam.Email__c = String.Empty
                End If

                objParam.Gender__c = String.Empty
                objParam.Mobile_Phone__c = String.Empty
                objParam.ID_Type__c = String.Empty
                objParam.ID_Number__c = String.Empty
                objParam.MMKSI_WEB_ID__c = String.Empty

                'start add by anh 20171030 yg dikirim adalah no HP di SAPCustomer
                If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer) Then
                    objParam.Mobile_Phone__c = _spkHeader.SPKCustomer.HpNo
                End If
                'end

                If _spkHeader.SPKCustomer.SPKCustomerProfiles.Count > 0 Then
                    For Each _profiles As SPKCustomerProfile In _spkHeader.SPKCustomer.SPKCustomerProfiles
                        If _profiles.ProfileHeader.ID = 27 Then 'gender
                            objParam.Gender__c = IIf(_profiles.ProfileValue = "LK", "Male", "Female")
                        End If
                        If _profiles.ProfileHeader.ID = 29 Then 'ktp/tdp
                            Dim tipeCustomer As EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest = _spkHeader.SPKCustomer.TipeCustomer
                            Select Case tipeCustomer
                                Case EnumTipePelangganCustomerRequest.TipePelangganCustomerRequest.Perorangan
                                    objParam.ID_Type__c = "KTP"
                                Case Else
                                    objParam.ID_Type__c = "TDP"
                            End Select
                            objParam.ID_Number__c = _profiles.ProfileValue
                        End If
                        If _profiles.ProfileHeader.ID = 30 Then 'No telp
                            If objParam.Mobile_Phone__c.Trim = "" Then
                                objParam.Mobile_Phone__c = _profiles.ProfileValue
                            End If
                        End If
                    Next
                    'remarks by anh 20171030 pindah ke atas jadi priority #1
                    'Else
                    'If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer) Then
                    '    objParam.Gender__c = IIf(_spkHeader.SPKCustomer.SAPCustomer.Sex = 1, "Male", "Female")
                    '    objParam.Mobile_Phone__c = _spkHeader.SPKCustomer.HpNo
                    'End If
                End If

                If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer) Then
                    objParam.Information_Type__c = EnumInformationType.GetStringInformationType(_spkHeader.SPKCustomer.SAPCustomer.InformationType)
                    objParam.Customer_Purposes__c = EnumCustomerPurpose.GetStringCustomerPurpose(_spkHeader.SPKCustomer.SAPCustomer.CustomerPurpose)
                    objParam.LeadSource = EnumInformationSource.GetStringInformationSource(_spkHeader.SPKCustomer.SAPCustomer.InformationSource)
                    objParam.Prospect_Date__c = _spkHeader.SPKCustomer.SAPCustomer.ProspectDate.ToString("yyyy-MM-dd")
                    objParam.MMKSI_WEB_ID__c = _spkHeader.SPKCustomer.SAPCustomer.WebID
                Else
                    objParam.Information_Type__c = String.Empty
                    objParam.Customer_Purposes__c = String.Empty
                    objParam.LeadSource = String.Empty
                    objParam.Prospect_Date__c = _spkHeader.SPKCustomer.CreatedTime.ToString("yyyy-MM-dd")
                End If

                objParam.SPK_No__c = _spkHeader.SPKNumber
                objParam.Dealer_SPK_No__c = _spkHeader.DealerSPKNumber
                objParam.SPK_Status__c = "Closed Won"
                objParam.Validation_Key__c = _spkHeader.ValidationKey

                objParam.StageName = EnumSAPCustomerResponse.GetStringValue(CInt(EnumSAPCustomerResponse.SAPCustomerResponse.SPK))
                objParam.CloseDate = Date.Now.ToString("yyyy-MM-dd")
                objParam.AccountID = _accountID
                objParam.Is_Valid_To_Send_SMS__c = IIf(IsSms = True, "true", "false")


                If _spkHeader.SPKDetails.Count > 0 Then
                    Dim _spkDetail As SPKDetail = CType(_spkHeader.SPKDetails(0), SPKDetail)
                    If Not IsNothing(_spkDetail.VechileColor.VechileType) Then
                        'objParam.Car__c = String.Empty
                        objParam.Car_Code__c = _spkDetail.VechileColor.VechileType.VechileTypeCode
                        objParam.Car_Type__c = _spkDetail.VechileColor.VechileType.Description
                        'objParam.Color__c = _spkDetail.VechileColor.ColorEngName
                    Else
                        'objParam.Car__c = String.Empty
                        objParam.Car_Code__c = _spkDetail.VehicleTypeCode
                        Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(_spkDetail.VehicleTypeCode)
                        If Not IsNothing(objVT) Then
                            objParam.Car_Type__c = objVT.Description
                        Else
                            objParam.Car_Type__c = String.Empty
                        End If
                        'objParam.Color__c = String.Empty
                    End If
                    objParam.Quantity__c = _spkDetail.Quantity
                Else
                    If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer) Then
                        If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer.VechileType) Then
                            'objParam.Car__c = String.Empty
                            objParam.Car_Code__c = _spkHeader.SPKCustomer.SAPCustomer.VechileType.VechileTypeCode
                            objParam.Car_Type__c = _spkHeader.SPKCustomer.SAPCustomer.VechileType.Description
                        Else
                            'objParam.Car__c = String.Empty
                            objParam.Car_Code__c = _spkHeader.SPKCustomer.SAPCustomer.VehicleTypeCode
                            Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(_spkHeader.SPKCustomer.SAPCustomer.VehicleTypeCode)
                            If Not IsNothing(objVT) Then
                                objParam.Car_Type__c = objVT.Description
                            Else
                                objParam.Car_Type__c = String.Empty
                            End If
                        End If
                        objParam.Quantity__c = _spkHeader.SPKCustomer.SAPCustomer.Qty
                    Else
                        'objParam.Car__c = String.Empty
                        objParam.Car_Code__c = String.Empty
                        objParam.Car_Type__c = String.Empty
                        objParam.Quantity__c = String.Empty
                    End If
                End If

                System.Threading.Tasks.Task.Run(Sub() KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", KTB.DNet.WebApi.Models.paramWalkinOpportunity.SObjectTypeName), objParam, True).Wait())

                If KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess = False Then
                    vReturn = False
                    msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message

                    _strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam)
                    CreateWsLog(vReturn, msg, "K:SALESFORCEINSERTOPPORTUNITY\n" & _strJson)

                End If
            Catch ex As Exception
                vReturn = False
                msg = "Gagal insert opportunity di salesforce"
                _strJson = "SPKNumber : " & _spkHeader.SPKNumber & " Error : " & ex.Message
                CreateWsLog(vReturn, msg, "K:SALESFORCEINSERTOPPORTUNITY\n" & _strJson)
            End Try
        End If
        Return vReturn
    End Function

    Public Function UpdateOportunity(ByVal _spkHeader As SPKHeader, ByVal IsSms As Boolean) As Boolean
        Dim vReturn As Boolean = True
        Dim msg As String = String.Empty
        GetConfig()

        If _valTransferToSF = "1" Then
            Try
                GetAccount()
                'Insert oportunity
                Dim objParam As KTB.DNet.WebApi.Models.paramUpdateOpportunity = New KTB.DNet.WebApi.Models.paramUpdateOpportunity()
                objParam.id = _spkHeader.SPKCustomer.SAPCustomer.SalesforceID
                objParam.Dealer_Code__c = _spkHeader.Dealer.DealerCode
                objParam.Dealer_Name__c = _spkHeader.Dealer.DealerName
                objParam.SPK_Status__c = "Closed Won"
                objParam.SPK_No__c = _spkHeader.SPKNumber
                objParam.Dealer_SPK_No__c = _spkHeader.DealerSPKNumber
                objParam.Validation_Key__c = _spkHeader.ValidationKey
                objParam.StageName = EnumSAPCustomerResponse.GetStringValue(CInt(EnumSAPCustomerResponse.SAPCustomerResponse.SPK))
                objParam.Is_Valid_To_Send_SMS__c = IIf(IsSms = True, "true", "false")

                If _spkHeader.SPKDetails.Count > 0 Then
                    Dim _spkDetail As SPKDetail = CType(_spkHeader.SPKDetails(0), SPKDetail)
                    If Not IsNothing(_spkDetail.VechileColor.VechileType) Then
                        objParam.Car_Code__c = _spkDetail.VechileColor.VechileType.VechileTypeCode
                        objParam.Car_Type__c = _spkDetail.VechileColor.VechileType.Description
                    Else
                        objParam.Car_Code__c = _spkDetail.VehicleTypeCode
                        Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(_spkDetail.VehicleTypeCode)
                        If Not IsNothing(objVT) Then
                            objParam.Car_Type__c = objVT.Description
                        Else
                            objParam.Car_Type__c = String.Empty
                        End If
                    End If
                Else
                    If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer) Then
                        If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer.VechileType) Then
                            objParam.Car_Code__c = _spkHeader.SPKCustomer.SAPCustomer.VechileType.VechileTypeCode
                            objParam.Car_Type__c = _spkHeader.SPKCustomer.SAPCustomer.VechileType.Description
                        Else
                            objParam.Car_Code__c = _spkHeader.SPKCustomer.SAPCustomer.VehicleTypeCode
                            Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(_spkHeader.SPKCustomer.SAPCustomer.VehicleTypeCode)
                            If Not IsNothing(objVT) Then
                                objParam.Car_Type__c = objVT.Description
                            Else
                                objParam.Car_Type__c = String.Empty
                            End If
                        End If
                    Else
                        objParam.Car_Code__c = String.Empty
                        objParam.Car_Type__c = String.Empty
                    End If
                End If

                System.Threading.Tasks.Task.Run(Sub() KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", KTB.DNet.WebApi.Models.paramUpdateOpportunity.SObjectTypeName), objParam, True).Wait())

                If KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess = False Then
                    vReturn = False
                    msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message

                    _strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam)
                    CreateWsLog(vReturn, msg, "K:SALESFORCEUPDATEOPPORTUNITY\n" & _strJson)

                End If
            Catch ex As Exception
                vReturn = False
                msg = "Gagal update opportunity di salesforce"
                _strJson = "SPKNumber : " & _spkHeader.SPKNumber & " Error : " & ex.Message
                CreateWsLog(vReturn, msg, "K:SALESFORCEUPDATEOPPORTUNITY\n" & _strJson)
            End Try
        End If
        Return vReturn
    End Function

    Public Function UpdateOportunity(ByVal objresponse As SAPCustomerResponse, ByVal status As EnumSAPCustomerResponse.SAPCustomerResponse, ByVal isSms As Boolean) As Boolean
        Dim vReturn As Boolean = True
        Dim msg As String = String.Empty

        GetConfig()

        If _valTransferToSF = "1" Then
            Try

                'Update oportunity
                Dim objParam As paramUpdateOpportunity = New paramUpdateOpportunity()
                objParam.id = objresponse.SAPCustomer.SalesforceID
                objParam.Dealer_Code__c = objresponse.SAPCustomer.Dealer.DealerCode
                objParam.Dealer_Name__c = objresponse.SAPCustomer.Dealer.DealerName
                objParam.SPK_No__c = ""
                objParam.Dealer_SPK_No__c = ""
                objParam.Validation_Key__c = ""
                If Not IsNothing(objresponse.SAPCustomer.VechileType) Then
                    objParam.Car_Code__c = objresponse.SAPCustomer.VechileType.VechileTypeCode
                    objParam.Car_Type__c = objresponse.SAPCustomer.VechileType.Description
                Else
                    objParam.Car_Code__c = ""
                    objParam.Car_Type__c = ""
                End If
                objParam.SPK_Status__c = EnumSAPCustomerResponse.GetStringValue(status)
                objParam.StageName = EnumSAPCustomerResponse.GetStringValue(status)
                objParam.Is_Valid_To_Send_SMS__c = IIf(isSms = True, "true", "false")

                If status >= 4 Then
                    Dim _criterias As New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    _criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKCustomer.SAPCustomer.ID", MatchType.Exact, objresponse.SAPCustomer.ID))
                    Dim arrSPKHeader As ArrayList = New SPKHeaderFacade(User).Retrieve(_criterias)
                    If Not IsNothing(arrSPKHeader) AndAlso arrSPKHeader.Count > 0 Then
                        Dim _spkHeader As SPKHeader = CType(arrSPKHeader(0), SPKHeader)
                        If _spkHeader.ID > 0 Then
                            objParam.SPK_No__c = _spkHeader.SPKNumber
                            objParam.Dealer_SPK_No__c = _spkHeader.DealerSPKNumber
                            objParam.Validation_Key__c = _spkHeader.ValidationKey

                            If _spkHeader.SPKDetails.Count > 0 Then
                                Dim _spkDetail As SPKDetail = CType(_spkHeader.SPKDetails(0), SPKDetail)
                                If Not IsNothing(_spkDetail.VechileColor) Then
                                    objParam.Car_Code__c = _spkDetail.VechileColor.VechileType.VechileTypeCode
                                    objParam.Car_Type__c = _spkDetail.VechileColor.VechileType.Description
                                Else
                                    objParam.Car_Code__c = _spkDetail.VehicleTypeCode
                                    Dim objVT As VechileType = New VechileTypeFacade(User).Retrieve(_spkDetail.VehicleTypeCode)
                                    If Not IsNothing(objVT) Then
                                        objParam.Car_Type__c = objVT.Description
                                    Else
                                        objParam.Car_Type__c = String.Empty
                                    End If
                                End If
                            Else
                                If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer) Then
                                    If Not IsNothing(_spkHeader.SPKCustomer.SAPCustomer.VechileType) Then
                                        objParam.Car_Code__c = _spkHeader.SPKCustomer.SAPCustomer.VechileType.VechileTypeCode
                                        objParam.Car_Type__c = _spkHeader.SPKCustomer.SAPCustomer.VechileType.Description
                                    Else
                                        objParam.Car_Code__c = _spkHeader.SPKCustomer.SAPCustomer.VehicleTypeCode
                                        objParam.Car_Type__c = String.Empty
                                    End If
                                Else
                                    objParam.Car_Code__c = String.Empty
                                    objParam.Car_Type__c = String.Empty
                                End If
                            End If
                        End If
                    End If
                End If

                System.Threading.Tasks.Task.Run(Sub() KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", KTB.DNet.WebApi.Models.paramUpdateOpportunity.SObjectTypeName), objParam).Wait())
                Dim A As String

                If KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess = False Then

                    vReturn = False
                    msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message

                    _strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam)
                    CreateWsLog(vReturn, msg, "K:SALESFORCEUPDATEOPPORTUNITY\n" & _strJson)
                End If

            Catch ex As Exception
                vReturn = False
                msg = "Gagal update opportunity di salesforce"
                _strJson = "SAPCustomerID : " & objresponse.SAPCustomer.ID & " Error: " & ex.Message
                CreateWsLog(vReturn, msg, "K:SALESFORCEUPDATEOPPORTUNITY\n" & _strJson)
            End Try

            Return vReturn
        End If
    End Function

    Public Function UpdateCase(ByVal objresponse As CustomerCaseResponse) As Boolean
        Dim vReturn As Boolean = True
        Dim msg As String = String.Empty
        GetConfig()

        If _valTransferToSF = "1" Then
            Try

                'Update response
                Dim objParam As KTB.DNet.WebApi.Models.paramUpdateCase = New KTB.DNet.WebApi.Models.paramUpdateCase()
                objParam.id = objresponse.CustomerCase.SalesforceID
                objParam.status = EnumCustomerCaseResponse.GetStringCustomerResponse(objresponse.Status)
                objParam.Status_By_Dealer__c = EnumCustomerCaseResponse.GetStringCustomerResponse(objresponse.Status)
                objParam.comment = objresponse.Description
                If objresponse.CustomerCase.SubCategory1.Contains("Test Drive") Then
                    objParam.Confirm_Test_Drive_Request_Date__c = Format(objresponse.BookingDatetime, "yyyy-MM-dd hh:mm:ss")
                    objParam.Confirm_Service_Booking_Request_Date__c = ""
                ElseIf objresponse.CustomerCase.SubCategory1.Contains("Service Booking") Then
                    objParam.Confirm_Service_Booking_Request_Date__c = Format(objresponse.BookingDatetime, "yyyy-MM-dd hh:mm:ss")
                    objParam.Confirm_Test_Drive_Request_Date__c = ""
                Else
                    objParam.Confirm_Service_Booking_Request_Date__c = ""
                    objParam.Confirm_Test_Drive_Request_Date__c = ""
                End If
                If objresponse.Status = 6 Then
                    objParam.Confirm_Service_Booking_Request_Date__c = ""
                    objParam.Confirm_Test_Drive_Request_Date__c = ""
                End If
                If objresponse.Response > 0 Then
                    Dim crit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Partial, "CustomerCase.Response"))
                    crit.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, objresponse.Response))
                    objParam.Dealer_Respons__c = CType(New StandardCodeFacade(User).Retrieve(crit)(0), StandardCode).ValueDesc
                Else
                    objParam.Dealer_Respons__c = ""
                End If

                'System.Threading.Tasks.Task.Run(Sub() KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", KTB.DNet.WebApi.Models.paramUpdateCase.SObjectTypeName), objParam, False).Wait())
                Dim t As System.Threading.Tasks.Task = System.Threading.Tasks.Task.Run(Sub() KTB.DNet.WebApi.Models.SalesForce.SalesForce.Send(User, String.Concat("services/apexrest/", KTB.DNet.WebApi.Models.paramUpdateCase.SObjectTypeName), objParam, False).Wait())
                If Not t.Wait(30000) Then
                    Throw New Exception("Timeout")
                End If

                If KTB.DNet.WebApi.Models.SalesForce.SalesForce.IsSuccess = False Then
                    vReturn = False
                    msg = KTB.DNet.WebApi.Models.SalesForce.SalesForce.Message

                    _strJson = Newtonsoft.Json.JsonConvert.SerializeObject(objParam)
                    CreateWsLog(vReturn, msg, "K:SALESFORCEIUPDATECASE\n" & _strJson)
                End If

            Catch ex As Exception
                vReturn = False
                msg = "Gagal update data case di salesforce" & ex.Message
                _strJson = "CustomerCase.ID : " & objresponse.CustomerCase.ID & " Error : " & ex.Message
                CreateWsLog(vReturn, msg, "K:SALESFORCEIUPDATECASE\n" & _strJson)
            End Try
            Return vReturn
        End If
    End Function

    Public Sub CreateWsLog(ByVal vReturn As Boolean, ByVal errMsg As String, ByVal bodyMessage As String)
        Try
            Dim WsLog As WsLog = New WsLog()
            WsLog.Source = "Internal"
            WsLog.Status = vReturn.ToString()
            WsLog.Message = errMsg
            WsLog.Body = bodyMessage
            WsLog.RowStatus = 0
            WsLog.CreatedBy = "DNetWebService"

            Dim facade As WsLogFacade = New WsLogFacade(User)
            facade.Insert(WsLog)
        Catch ex As Exception
            Dim err As String = ex.Message.ToString & " - " & ex.InnerException.ToString
        End Try
    End Sub

    Private Sub GetConfig()
        Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objappconfig As AppConfig = objfacade.Retrieve("TransferToSF")
        If Not IsNothing(objappconfig) AndAlso objappconfig.ID > 0 Then
            _valTransferToSF = objappconfig.Value.Trim
        End If
    End Sub

    Private Sub GetAccount()
        Dim objfacade As AppConfigFacade = New AppConfigFacade(User)
        Dim objappconfig As AppConfig = objfacade.Retrieve("Account_SF_ID")
        If Not IsNothing(objappconfig) AndAlso objappconfig.ID > 0 Then
            _accountID = objappconfig.Value.Trim
        End If
    End Sub

    Private Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

End Class
