Imports System.Net
Imports System.Web.Http
Imports System.Net.Http
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports Newtonsoft.Json
Imports System.Security.Principal
Imports KTB.DNet.BusinessFacade.Service

Public Class BillingUpdateToSAPController
    Inherits System.Web.Mvc.Controller


    <HttpGet>
    Public Function GetBillingUpdate(Optional ByVal DataDate As String = "") As JsonResult
        Dim obj As BillingUpdateToSAPModel = New BillingUpdateToSAPModel()
        obj.Message = "-"
        obj.Status = -1

        Dim _msg As New MessageModel
        _msg.Status = True
        Dim _err As String = "Success "
        Dim str As String = DataDate.ToString
        Dim _SourceAddress = Request.UserHostAddress
        Dim strByPass As String = ""

        Dim headers = Request.Headers

        If Not IsNothing(Request.Headers.GetValues("x-pass-header")) Then
            strByPass = headers.GetValues("x-pass-header").First()
        End If

        Try
            'IP Filtering
            If Not CommonHelper.IsAllowed(_SourceAddress, strByPass) Then
                Throw New Exception("Forbidden Source Address")
            End If
        Catch exx As Exception
            obj.Message = exx.Message.ToString()
            obj.Status = 0
            _msg.Status = False
            _msg.Message = exx.Message.ToString()
        End Try

        If _msg.Status = True Then
            Dim lst As List(Of BillingUpdate) = New List(Of BillingUpdate)()
            Try
                Dim User As New System.Security.Principal.GenericPrincipal(New GenericIdentity("WebService"), Nothing)
                Dim ds As New ArrayList
                Dim fc As MonthlyDocumentToFakturEvidanceFacade = New MonthlyDocumentToFakturEvidanceFacade(User)

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If DataDate.Trim.Length > 0 Then
                    Dim _startDate As DateTime = New DateTime(DataDate.Substring(4, 4), DataDate.Substring(2, 2), DataDate.Substring(0, 2), 0, 0, 0)
                    Dim _endDate As DateTime = New DateTime(DataDate.Substring(4, 4), DataDate.Substring(2, 2), DataDate.Substring(0, 2), 23, 59, 59)
                    crit.opAnd(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "CreatedTime", MatchType.GreaterOrEqual, _startDate))
                    crit.opAnd(New Criteria(GetType(MonthlyDocumentToFakturEvidance), "CreatedTime", MatchType.LesserOrEqual, _endDate))
                End If

                ds = fc.Retrieve(crit)

                If (Not IsNothing(ds) AndAlso ds.Count > 0) Then
                    obj.Message = "OK"
                    obj.Status = 1

                    For Each dr As MonthlyDocumentToFakturEvidance In ds
                        Dim ob As BillingUpdate = New BillingUpdate()
                        If Not IsNothing(dr.MonthlyDocument) Then
                            ob.BillingNo = dr.MonthlyDocument.BillingNo
                            ob.AccountingNo = dr.MonthlyDocument.AccountingNo
                            ob.BankAccount = dr.MonthlyDocument.AccountNumberBank
                        Else
                            ob.BillingNo = ""
                            ob.AccountingNo = ""
                            ob.BankAccount = ""
                        End If
                        ob.TransferDate = dr.PlanningTransferDate.ToString("yyyy-MM-dd")
                        ob.TaxNo = dr.FakturNumber
                        ob.Text = dr.PaymentDescription
                        lst.Add(ob)
                    Next
                    obj.ObjectResult = lst
                Else
                    obj.Message = "No Data"
                    obj.Status = 1

                End If

            Catch ex As Exception
                obj.Message = ex.Message.ToString()
                obj.Status = 0

            End Try
        End If

        ''Log 
        Try
            str = str & "\n" & JsonConvert.SerializeObject(obj)
            CommonHelper.LogDB(str, _msg, _SourceAddress, Date.Now)
        Catch ex As Exception
            _msg.Message = _SourceAddress & " ; " & ex.Message & " ; " & _msg.Message
            CommonHelper.LogTxt(str, _msg)
        End Try


        Return Json(obj, JsonRequestBehavior.AllowGet)

    End Function
End Class
