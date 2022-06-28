Imports Ktb.DNet.Domain
Imports Ktb.DNet.BusinessFacade.General
Imports Ktb.DNet.BusinessFacade.FinishUnit
Imports Ktb.DNet.BusinessFacade.PO
Imports Ktb.DNet.BusinessFacade.PK
Imports KTB.DNet.Domain.Search

Public Class PopUpDocumentFlow
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            If Not IsNothing(Request.QueryString("flow")) Then
                Initialize(Request.QueryString("flow"))
            End If
        End If
    End Sub

    Private Sub GenFakturString(ByVal obj As SalesOrder, ByVal nSpace As Integer)
        'temporary doesn't use this procedure(display Faktur) because GoLive 1Nov09 is only CreditControl
        Exit Sub


        Dim index As Integer
        Dim bgColorFaktur As String = "#ffbbff"
        Dim strBuffer As String = ""
        Dim i As Integer
        Dim objEC As EndCustomer
        Dim objECFac As EndCustomerFacade = New EndCustomerFacade(User)
        Dim IsDoWrite As Boolean = True

        index = 0
        For Each DOItem As DeliveryOrder In obj.DeliveryOrders
            index = index + 1
            IsDoWrite = True
            strBuffer = "<tr><td bgcolor=""" & bgColorFaktur & """>"
            For i = 1 To nSpace
                strBuffer = strBuffer & "&nbsp;"
            Next
            strBuffer = strBuffer & index & ". Faktur - " & IIf(DOItem.ChassisMaster.FakturStatus = 0, "Baru   ", IIf(DOItem.ChassisMaster.FakturStatus = 4, "Selesai", "")) & " : "

            If Not IsNothing(DOItem.ChassisMaster.EndCustomer) Then
                If DOItem.ChassisMaster.FakturStatus = 0 Then 'Baru
                    If Format(DOItem.ChassisMaster.EndCustomer.FakturDate, "yyyyMMdd") = "17530101" Then
                        IsDoWrite = False
                        'index = index - 1
                    Else
                        strBuffer = strBuffer & DOItem.ChassisMaster.EndCustomer.FakturDate
                    End If

                ElseIf DOItem.ChassisMaster.FakturStatus = 4 Then 'Selesai
                    strBuffer = strBuffer & DOItem.ChassisMaster.EndCustomer.OpenFakturDate & " - " & DOItem.ChassisMaster.EndCustomer.FakturNumber
                End If
            Else
                'index = index - 1
                IsDoWrite = False
            End If
            strBuffer = strBuffer & "<td></tr>"
            If IsDoWrite = True Then
                Response.Write(strBuffer)
            End If
        Next

        'index = 0
        'For Each DOItem As DeliveryOrder In obj.DeliveryOrders
        '    index = index + 1
        '    If DOItem.ChassisMaster.FakturStatus = 0 Then 'Baru
        '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
        '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Baru    :" & DOItem.ChassisMaster.EndCustomer.FakturDate & "<td></tr>")
        '        End If
        '    ElseIf DOItem.ChassisMaster.FakturStatus = 4 Then 'Selesai
        '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
        '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Selesai :" & DOItem.ChassisMaster.EndCustomer.OpenFakturDate & " " & DOItem.ChassisMaster.EndCustomer.FakturNumber & "<td></tr>")
        '        End If
        '    End If
        'Next
    End Sub

    Private Function TotalPO(ByVal POHeaderID As Integer) As Integer
        Dim objPODFac As PODetailFacade = New PODetailFacade(User)
        Dim arlPOD As ArrayList = New ArrayList
        Dim crtPOD As CriteriaComposite
        Dim Total As Integer = 0

        crtPOD = New CriteriaComposite(New Criteria(GetType(PODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtPOD.opAnd(New Criteria(GetType(PODetail), "POHeader.ID", MatchType.Exact, POHeaderID))
        arlPOD = objPODFac.Retrieve(crtPOD)
        For Each objPOD As PODetail In arlPOD
            Total = Total + objPOD.ReqQty
        Next
        Return Total

    End Function

    Private Sub Initialize(ByVal flow As String)
        Dim ObjPKHeader As PKHeader
        Dim ArrContract As ArrayList
        Dim ArrPO As ArrayList
        Dim ArrChassis As ArrayList
        Dim objPO As POHeader
        Dim objContract As ContractHeader
        Dim ArrPKHeader As New ArrayList
        'Dim objCMFac As ChassisMasterFacade = New ChassisMasterFacade(User)
        'Dim objCM As ChassisMaster
        'Dim objECFac As EndCustomerFacade = New EndCustomerFacade(User)
        'Dim objEC As EndCustomer
        Dim arrDailyPayment As ArrayList
        Dim arrInvoiceHeader As ArrayList
        Dim arrVAlurInvoice As New ArrayList
        Dim objVAlurInvoice As New VAlurInvoice
        Dim arrInvoiceDetail As New ArrayList

        Dim bgColorFaktur As String = "#ffbbff"

        ' Dim objInvoiceHeader As New InvoiceHeader

        Dim arrFlow() As String = flow.Split("_")
        If arrFlow.Length = 2 Or arrFlow.Length = 3 Then
            Response.Write("<table width=""100%"" cellpadding=""4"" cellspacing=""1"" >")
            Dim type As String = arrFlow(0)
            Dim value As String = arrFlow(1)
            Dim value2 As String = String.Empty
            If arrFlow.Length = 3 Then
                value2 = arrFlow(2)
            End If
            If type = "PK" Then
                ObjPKHeader = New PKHeaderFacade(User).Retrieve(value)
                Response.Write("<tr><td bgcolor=""#99ffcc"" class=""titleField"">P/K - " & value & " -> " & ObjPKHeader.TotalTargetQuantity & " Unit<td></tr>")

                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(ContractHeader), "PKNumber", _
                    MatchType.Exact, value))
                criterias2.opAnd(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, _
                    CType(DBRowStatus.Active, Short)))
                ArrContract = New ContractHeaderFacade(User).Retrieve(criterias2)

                For Each contractItem As ContractHeader In ArrContract
                    Response.Write("<tr><td bgcolor=""#ff99ff"">&nbsp;O/C - " & contractItem.ContractNumber & " -> " & contractItem.TotalQuantity & " Unit<td></tr>")
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(POHeader), "ContractHeader.ID", _
                    MatchType.Exact, contractItem.ID))
                    criterias.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, _
                        CType(DBRowStatus.Active, Short)))
                    ArrPO = New POHeaderFacade(User).Retrieve(criterias)

                    For Each poItem As POHeader In ArrPO
                        'Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & poItem.PONumber & " -> " & poItem.TotalQuantity & " Unit<td></tr>")
                        Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & poItem.PONumber & " -> " & TotalPO(poItem.ID) & " Unit<td></tr>")
                        criterias = New CriteriaComposite(New Criteria(GetType(SalesOrder), "POHeader.ID", _
                        MatchType.Exact, poItem.ID))
                        criterias.opAnd(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, _
                            CType(DBRowStatus.Active, Short)))

                        Dim arrSalesOrder As New ArrayList
                        arrSalesOrder = New SalesOrderFacade(User).Retrieve(criterias)

                        If arrSalesOrder.Count > 0 Then
                            For Each obj As SalesOrder In arrSalesOrder
                                Dim strSONumber As String = obj.SONumber
                                Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;S/O - " & strSONumber & " <td></tr>")


                                If Not IsNothing(obj.LogisticDCHeader) Then
                                    Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;D/C - " & obj.LogisticDCHeader.DebitChargeNo & " <td></tr>")
                                End If

                                Dim index As Integer = 0
                                For Each DailyPaymentItem As DailyPayment In obj.DailyPayments
                                    index = index + 1
                                    Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                                Next

                                index = 0
                                For Each item As DeliveryOrder In obj.DeliveryOrders
                                    index = index + 1
                                    Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & item.DONumber & " : " & item.ChassisMaster.ChassisNumber & " - " & item.ChassisMaster.VechileColor.MaterialNumber & "<td></tr>")
                                Next

                                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(Invoice), "SalesOrder.ID", _
                                                        MatchType.Exact, obj.ID))
                                criterias1.opAnd(New Criteria(GetType(Invoice), "RowStatus", MatchType.Exact, _
                                    CType(DBRowStatus.Active, Short)))

                                arrInvoiceHeader = New InvoiceFacade(User).Retrieve(criterias1)
                                index = 0
                                For Each InvoiceItem As Invoice In arrInvoiceHeader
                                    index = index + 1
                                    If InvoiceItem.InvoiceType = "ZA7O" Then
                                        Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                        If Not IsNothing(InvoiceItem.LogisticDN) Then
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                        End If
                                    Else
                                        Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                        If Not IsNothing(InvoiceItem.LogisticDN) Then
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                        End If
                                    End If


                                Next

                                GenFakturString(obj, 17)
                                'index = 0
                                'For Each DOItem As DeliveryOrder In obj.DeliveryOrders
                                '    index = index + 1
                                '    If DOItem.ChassisMaster.FakturStatus = 0 Then 'Baru
                                '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Baru    :" & DOItem.ChassisMaster.EndCustomer.FakturDate & "<td></tr>")
                                '        End If
                                '    ElseIf DOItem.ChassisMaster.FakturStatus = 4 Then 'Selesai
                                '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Selesai :" & DOItem.ChassisMaster.EndCustomer.OpenFakturDate & " " & DOItem.ChassisMaster.EndCustomer.FakturNumber & "<td></tr>")
                                '        End If
                                '    End If
                                'Next

                            Next
                        End If
                    Next

                    'For Each pOItem As POHeader In ArrPO
                    '    Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & pOItem.PONumber & " :S/O - " & pOItem.SONumber & " -> " & pOItem.TotalQuantity & " Unit<td></tr>")
                    '    If pOItem.SONumber <> String.Empty Then

                    '        Dim criterias1 = New CriteriaComposite(New Criteria(GetType(DailyPayment), "POHeader.ID", _
                    '                                MatchType.Exact, pOItem.ID))
                    '        criterias1.opAnd(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, _
                    '            CType(DBRowStatus.Active, Short)))
                    '        arrDailyPayment = New DailyPaymentFacade(User).Retrieve(criterias1)
                    '        Dim index As Integer = 0
                    '        For Each DailyPaymentItem As DailyPayment In arrDailyPayment
                    '            index = index + 1
                    '            Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                    '        Next


                    '        criterias1 = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "SONumber", _
                    '                                MatchType.Exact, pOItem.SONumber))
                    '        criterias1.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, _
                    '            CType(DBRowStatus.Active, Short)))
                    '        ArrChassis = New ChassisMasterFacade(User).Retrieve(criterias1)
                    '        index = 0
                    '        For Each chassisItem As ChassisMaster In ArrChassis
                    '            index = index + 1
                    '            Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & chassisItem.DONumber & " : " & chassisItem.ChassisNumber & "<td></tr>")
                    '        Next


                    '        'criterias1 = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "POHeader.ID", _
                    '        '                        MatchType.Exact, pOItem.ID))
                    '        'criterias1.opAnd(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, _
                    '        '    CType(DBRowStatus.Active, Short)))
                    '        'arrInvoiceHeader = New InvoiceHeaderFacade(User).Retrieve(criterias1)
                    '        'index = 0
                    '        'For Each InvoiceHeaderItem As InvoiceHeader In arrInvoiceHeader
                    '        '    index = index + 1
                    '        '    Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceHeaderItem.InvoiceNumber & "<td></tr>")
                    '        'Next
                    '    End If
                    'Next
                Next
            ElseIf type = "PO" Then
                If value = String.Empty Then
                    objPO = New POHeaderFacade(User).Retrieve(value2)
                    If objPO.ID > 0 Then
                        ObjPKHeader = New PKHeaderFacade(User).Retrieve(objPO.ContractHeader.PKNumber)
                        If ObjPKHeader.ID > 0 Then
                            Dim strPKNumber As String = objPO.ContractHeader.PKNumber
                            Dim PKQuantity As Integer = ObjPKHeader.TotalTargetQuantity

                            Dim strContractNumber As String = objPO.ContractHeader.ContractNumber
                            Dim OCQuantity As Integer = objPO.ContractHeader.TotalQuantity

                            Dim strPONumber As String = objPO.PONumber
                            Dim POQuantity As Integer = objPO.TotalQuantity

                            Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & strPKNumber & " -> " & PKQuantity & " Unit<td></tr>")
                            Response.Write("<tr><td bgcolor=""#ff99ff"" class=""titleField"">&nbsp;O/C - " & strContractNumber & " -> " & OCQuantity & " Unit<td></tr>")
                            'Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & POQuantity & " Unit<td></tr>")
                            Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & TotalPO(objPO.ID) & " Unit<td></tr>")

                        End If
                    End If
                Else
                    Dim arrSalesOrder As New ArrayList
                    Dim objSalesOrder As New SalesOrder

                    objPO = New POHeaderFacade(User).Retrieve(value2)
                    If objPO.ID > 0 Then
                        ObjPKHeader = New PKHeaderFacade(User).Retrieve(objPO.ContractHeader.PKNumber)
                        If ObjPKHeader.ID > 0 Then
                            Dim strPKNumber As String = objPO.ContractHeader.PKNumber
                            Dim PKQuantity As Integer = ObjPKHeader.TotalTargetQuantity

                            Dim strContractNumber As String = objPO.ContractHeader.ContractNumber
                            Dim OCQuantity As Integer = objPO.ContractHeader.TotalQuantity

                            Dim strPONumber As String = objPO.PONumber
                            Dim POQuantity As Integer = objPO.TotalQuantity

                            Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & strPKNumber & " -> " & PKQuantity & " Unit<td></tr>")
                            Response.Write("<tr><td bgcolor=""#ff99ff"" class=""titleField"">&nbsp;O/C - " & strContractNumber & " -> " & OCQuantity & " Unit<td></tr>")
                            'Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & POQuantity & " Unit<td></tr>")
                            Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & TotalPO(objPO.ID) & " Unit<td></tr>")

                        End If
                    End If

                    objSalesOrder = New SalesOrderFacade(User).Retrieve(value)
                    If objSalesOrder.ID > 0 Then
                        '    ObjPKHeader = New PKHeaderFacade(User).Retrieve(objSalesOrder.POHeader.ContractHeader.PKNumber)
                        '   If ObjPKHeader.ID > 0 Then
                        '  Dim strPKNumber As String = objSalesOrder.POHeader.ContractHeader.PKNumber
                        ' Dim PKQuantity As Integer = ObjPKHeader.TotalTargetQuantity

                        'Dim strContractNumber As String = objSalesOrder.POHeader.ContractHeader.ContractNumber
                        'Dim OCQuantity As Integer = objSalesOrder.POHeader.ContractHeader.TotalQuantity

                        'Dim strPONumber As String = objSalesOrder.POHeader.PONumber
                        'Dim POQuantity As Integer = objSalesOrder.POHeader.TotalQuantity

                        If objSalesOrder.PaymentRef <> String.Empty Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesOrder), "SONumber", _
                                               MatchType.Exact, objSalesOrder.PaymentRef))
                            criterias.opAnd(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, _
                                CType(DBRowStatus.Active, Short)))
                            criterias.opOr(New Criteria(GetType(SalesOrder), "PaymentRef", MatchType.Exact, objSalesOrder.PaymentRef))
                            arrSalesOrder = New SalesOrderFacade(User).Retrieve(criterias)
                        Else
                            arrSalesOrder.Add(objSalesOrder)
                        End If

                        If arrSalesOrder.Count > 0 Then
                            'Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & strPKNumber & " -> " & PKQuantity & " Unit<td></tr>")
                            'Response.Write("<tr><td bgcolor=""#ff99ff"" class=""titleField"">&nbsp;O/C - " & strContractNumber & " -> " & OCQuantity & " Unit<td></tr>")
                            'Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & POQuantity & " Unit<td></tr>")

                            For Each obj As SalesOrder In arrSalesOrder
                                Dim strSONumber As String = obj.SONumber
                                Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;S/O - " & strSONumber & " <td></tr>")

                                If Not IsNothing(obj.LogisticDCHeader) Then
                                    Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;D/C - " & obj.LogisticDCHeader.DebitChargeNo & " <td></tr>")
                                End If

                                Dim index As Integer = 0
                                For Each DailyPaymentItem As DailyPayment In obj.DailyPayments
                                    index = index + 1
                                    Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                                Next

                                index = 0
                                For Each item As DeliveryOrder In obj.DeliveryOrders
                                    index = index + 1
                                    Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & item.DONumber & " : " & item.ChassisMaster.ChassisNumber & " - " & item.ChassisMaster.VechileColor.MaterialNumber & "<td></tr>")
                                Next

                                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(Invoice), "SalesOrder.ID", _
                                                        MatchType.Exact, obj.ID))
                                criterias1.opAnd(New Criteria(GetType(Invoice), "RowStatus", MatchType.Exact, _
                                    CType(DBRowStatus.Active, Short)))

                                arrInvoiceHeader = New InvoiceFacade(User).Retrieve(criterias1)
                                index = 0
                                For Each InvoiceItem As Invoice In arrInvoiceHeader
                                    index = index + 1
                                    If InvoiceItem.InvoiceType = "ZA7O" Then
                                        Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                        If Not IsNothing(InvoiceItem.LogisticDN) Then
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                        End If
                                    Else
                                        Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                        If Not IsNothing(InvoiceItem.LogisticDN) Then
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                        End If
                                    End If
                                Next

                                GenFakturString(obj, 17)

                                'index = 0
                                'For Each DOItem As DeliveryOrder In obj.DeliveryOrders
                                '    index = index + 1
                                '    If DOItem.ChassisMaster.FakturStatus = 0 Then 'Baru
                                '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Baru    :" & DOItem.ChassisMaster.EndCustomer.FakturDate & "<td></tr>")
                                '        End If


                                '    ElseIf DOItem.ChassisMaster.FakturStatus = 4 Then 'Selesai
                                '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Selesai :" & DOItem.ChassisMaster.EndCustomer.OpenFakturDate & " " & DOItem.ChassisMaster.EndCustomer.FakturNumber & "<td></tr>")

                                '        End If
                                '    End If

                                'Next

                            Next
                        End If
                    End If
                End If

                'objPO = New POHeaderFacade(User).Retrieve(value)
                'If Not objPO Is Nothing Then
                '    ObjPKHeader = New PKHeaderFacade(User).Retrieve(objPO.ContractHeader.PKNumber)
                '    Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & objPO.ContractHeader.PKNumber & " -> " & ObjPKHeader.TotalTargetQuantity & " Unit<td></tr>")
                '    Response.Write("<tr><td bgcolor=""#ff99ff"">&nbsp;O/C - " & objPO.ContractHeader.ContractNumber & " -> " & objPO.ContractHeader.TotalQuantity & " Unit<td></tr>")
                '    Response.Write("<tr><td bgcolor=""#ccffff"" class=""titleField"">&nbsp;&nbsp;&nbsp;P/O - " & objPO.PONumber & " :S/O - " & objPO.SONumber & " -> " & objPO.TotalQuantity & " Unit<td></tr>")
                '    If objPO.SONumber <> String.Empty Then
                '        Dim criterias1 = New CriteriaComposite(New Criteria(GetType(DailyPayment), "POHeader.ID", _
                '                                MatchType.Exact, objPO.ID))
                '        criterias1.opAnd(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, _
                '            CType(DBRowStatus.Active, Short)))
                '        arrDailyPayment = New DailyPaymentFacade(User).Retrieve(criterias1)
                '        Dim index As Integer = 0
                '        For Each DailyPaymentItem As DailyPayment In arrDailyPayment
                '            index = index + 1
                '            Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                '        Next


                '        criterias1 = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "SONumber", _
                '                                MatchType.Exact, objPO.SONumber))
                '        criterias1.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, _
                '            CType(DBRowStatus.Active, Short)))
                '        ArrChassis = New ChassisMasterFacade(User).Retrieve(criterias1)
                '        index = 0
                '        For Each chassisItem As ChassisMaster In ArrChassis
                '            index = index + 1
                '            Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & chassisItem.DONumber & " : " & chassisItem.ChassisNumber & "<td></tr>")
                '        Next


                '        'criterias1 = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "POHeader.ID", _
                '        '                        MatchType.Exact, objPO.ID))
                '        'criterias1.opAnd(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, _
                '        '    CType(DBRowStatus.Active, Short)))
                '        'arrInvoiceHeader = New InvoiceHeaderFacade(User).Retrieve(criterias1)
                '        'index = 0
                '        'For Each InvoiceHeaderItem As InvoiceHeader In arrInvoiceHeader
                '        '    index = index + 1
                '        '    Response.Write("<tr><td bgcolor=""#ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceHeaderItem.InvoiceNumber & "<td></tr>")
                '        'Next
                '    End If
                'End If


            ElseIf type = "MO" Then

                Dim arrSalesOrder As New ArrayList
                Dim objSalesOrder As New SalesOrder
                'Dim objPOHeader As New POHeader

                objContract = New ContractHeaderFacade(User).Retrieve(value)
                If objContract.ID > 0 Then
                    ObjPKHeader = New PKHeaderFacade(User).Retrieve(objContract.PKNumber)
                    If ObjPKHeader.ID > 0 Then
                        Dim strPKNumber As String = objContract.PKNumber
                        Dim PKQuantity As Integer = ObjPKHeader.TotalTargetQuantity
                        Dim strContractNumber As String = objContract.ContractNumber
                        Dim OCQuantity As Integer = objContract.TotalQuantity

                        Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & strPKNumber & " -> " & PKQuantity & " Unit<td></tr>")
                        Response.Write("<tr><td bgcolor=""#ff99ff"" class=""titleField"">&nbsp;O/C - " & strContractNumber & " -> " & OCQuantity & " Unit<td></tr>")

                        Dim criteriax As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriax.opAnd(New Criteria(GetType(POHeader), "ContractHeader.ID", MatchType.Exact, objContract.ID))

                        Dim arlPO As ArrayList = New POHeaderFacade(User).Retrieve(criteriax)

                        For Each objPOHeader As POHeader In arlPO
                            'objPOHeader = New POHeaderFacade(User).RetrieveByContract(objContract.ID)
                            If objPOHeader.ID > 0 Then


                                Dim strPONumber As String = objPOHeader.PONumber
                                Dim POQuantity As Integer = objPOHeader.TotalQuantity

                                'Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & POQuantity & " Unit<td></tr>")
                                Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & TotalPO(objPOHeader.ID) & " Unit<td></tr>")

                                Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesOrder), "POHeader.ID", _
                                                   MatchType.Exact, objPOHeader.ID))
                                criterias.opAnd(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, _
                                    CType(DBRowStatus.Active, Short)))
                                arrSalesOrder = New SalesOrderFacade(User).Retrieve(criterias)

                                If arrSalesOrder.Count > 0 Then

                                    For Each obj As SalesOrder In arrSalesOrder
                                        Dim strSONumber As String = obj.SONumber
                                        Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;S/O - " & strSONumber & " <td></tr>")

                                        If Not IsNothing(obj.LogisticDCHeader) Then
                                            Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;D/C - " & obj.LogisticDCHeader.DebitChargeNo & " <td></tr>")
                                        End If

                                        Dim index As Integer = 0
                                        For Each DailyPaymentItem As DailyPayment In obj.DailyPayments
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                                        Next

                                        index = 0
                                        For Each item As DeliveryOrder In obj.DeliveryOrders
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & item.DONumber & " : " & item.ChassisMaster.ChassisNumber & " - " & item.ChassisMaster.VechileColor.MaterialNumber & "<td></tr>")
                                        Next

                                        Dim criterias1 As New CriteriaComposite(New Criteria(GetType(Invoice), "SalesOrder.ID", _
                                                                MatchType.Exact, obj.ID))
                                        criterias1.opAnd(New Criteria(GetType(Invoice), "RowStatus", MatchType.Exact, _
                                            CType(DBRowStatus.Active, Short)))

                                        arrInvoiceHeader = New InvoiceFacade(User).Retrieve(criterias1)
                                        index = 0
                                        For Each InvoiceItem As Invoice In arrInvoiceHeader
                                            index = index + 1
                                            If InvoiceItem.InvoiceType = "ZA7O" Then
                                                Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                                If Not IsNothing(InvoiceItem.LogisticDN) Then
                                                    index = index + 1
                                                    Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                                End If
                                            Else
                                                Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                                If Not IsNothing(InvoiceItem.LogisticDN) Then
                                                    index = index + 1
                                                    Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                                End If
                                            End If

                                        Next

                                        GenFakturString(obj, 17)

                                        'index = 0
                                        'For Each DOItem As DeliveryOrder In obj.DeliveryOrders
                                        '    index = index + 1
                                        '    If DOItem.ChassisMaster.FakturStatus = 0 Then 'Baru
                                        '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                        '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Baru    :" & DOItem.ChassisMaster.EndCustomer.FakturDate & "<td></tr>")

                                        '        End If
                                        '    ElseIf DOItem.ChassisMaster.FakturStatus = 4 Then 'Selesai
                                        '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                        '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Selesai :" & DOItem.ChassisMaster.EndCustomer.OpenFakturDate & " " & DOItem.ChassisMaster.EndCustomer.FakturNumber & "<td></tr>")
                                        '        End If

                                        '    End If
                                        'Next

                                    Next
                                End If
                            End If
                        Next


                        ''--
                    End If
                End If

                'objContract = New ContractHeaderFacade(User).Retrieve(value)
                'If objContract.ID > 0 Then
                '    ObjPKHeader = New PKHeaderFacade(User).Retrieve(objContract.PKNumber)
                '    Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & objContract.PKNumber & " -> " & ObjPKHeader.TotalTargetQuantity & " Unit<td></tr>")
                '    Response.Write("<tr><td bgcolor=""#ff99ff"" class=""titleField"">&nbsp;O/C - " & objContract.ContractNumber & " -> " & objContract.TotalQuantity & " Unit<td></tr>")
                '    Dim criterias As New CriteriaComposite(New Criteria(GetType(POHeader), "ContractHeader.ID", _
                '                        MatchType.Exact, objContract.ID))
                '    criterias.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, _
                '        CType(DBRowStatus.Active, Short)))
                '    ArrPO = New POHeaderFacade(User).Retrieve(criterias)

                '    For Each pOItem As POHeader In ArrPO
                '        Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & pOItem.PONumber & " :S/O - " & pOItem.SONumber & " -> " & poItem.TotalQuantity & " Unit<td></tr>")
                '        If pOItem.SONumber <> String.Empty Then
                '            Dim criterias1 = New CriteriaComposite(New Criteria(GetType(DailyPayment), "POHeader.ID", _
                '                                    MatchType.Exact, pOItem.ID))
                '            criterias1.opAnd(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, _
                '                CType(DBRowStatus.Active, Short)))
                '            arrDailyPayment = New DailyPaymentFacade(User).Retrieve(criterias1)
                '            Dim index As Integer = 0
                '            For Each DailyPaymentItem As DailyPayment In arrDailyPayment
                '                index = index + 1
                '                Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                '            Next


                '            criterias1 = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "SONumber", _
                '                                    MatchType.Exact, pOItem.SONumber))
                '            criterias1.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, _
                '                CType(DBRowStatus.Active, Short)))
                '            ArrChassis = New ChassisMasterFacade(User).Retrieve(criterias1)
                '            index = 0
                '            For Each chassisItem As ChassisMaster In ArrChassis
                '                index = index + 1
                '                Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & chassisItem.DONumber & " : " & chassisItem.ChassisNumber & "<td></tr>")
                '            Next


                '            'criterias1 = New CriteriaComposite(New Criteria(GetType(InvoiceHeader), "POHeader.ID", _
                '            '                        MatchType.Exact, pOItem.ID))
                '            'criterias1.opAnd(New Criteria(GetType(InvoiceHeader), "RowStatus", MatchType.Exact, _
                '            '    CType(DBRowStatus.Active, Short)))
                '            'arrInvoiceHeader = New InvoiceHeaderFacade(User).Retrieve(criterias1)
                '            'index = 0
                '            'For Each InvoiceHeaderItem As InvoiceHeader In arrInvoiceHeader
                '            '    index = index + 1
                '            '    Response.Write("<tr><td bgcolor=""#ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceHeaderItem.InvoiceNumber & "<td></tr>")
                '            'Next
                '        End If
                '    Next
                'End If

            ElseIf type = "Invoice" Then
                Dim objInvoice As New Invoice
                Dim arrPK As New ArrayList
                Dim arrSalesOrder As New ArrayList

                objInvoice = New InvoiceFacade(User).Retrieve(CInt(value))
                If objInvoice.ID > 0 Then
                    ObjPKHeader = New PKHeaderFacade(User).Retrieve(objInvoice.SalesOrder.POHeader.ContractHeader.PKNumber)
                    If ObjPKHeader.ID > 0 Then
                        Dim strPKNumber As String = objInvoice.SalesOrder.POHeader.ContractHeader.PKNumber
                        Dim PKQuantity As Integer = ObjPKHeader.TotalTargetQuantity

                        Dim strContractNumber As String = objInvoice.SalesOrder.POHeader.ContractHeader.ContractNumber
                        Dim OCQuantity As Integer = objInvoice.SalesOrder.POHeader.ContractHeader.TotalQuantity

                        Dim strPONumber As String = objInvoice.SalesOrder.POHeader.PONumber
                        'Dim POQuantity As Integer = objInvoice.SalesOrder.POHeader.TotalQuantity
                        Dim POQuantity As Integer = TotalPO(objInvoice.SalesOrder.POHeader.ID)

                        If objInvoice.SalesOrder.PaymentRef <> String.Empty Then
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesOrder), "SONumber", _
                                               MatchType.Exact, objInvoice.SalesOrder.PaymentRef))
                            criterias.opAnd(New Criteria(GetType(SalesOrder), "RowStatus", MatchType.Exact, _
                                CType(DBRowStatus.Active, Short)))
                            criterias.opOr(New Criteria(GetType(SalesOrder), "PaymentRef", MatchType.Exact, objInvoice.SalesOrder.PaymentRef))
                            arrSalesOrder = New SalesOrderFacade(User).Retrieve(criterias)
                        Else
                            arrSalesOrder.Add(objInvoice.SalesOrder)
                        End If

                        If arrSalesOrder.Count > 0 Then
                            Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & strPKNumber & " -> " & PKQuantity & " Unit<td></tr>")
                            Response.Write("<tr><td bgcolor=""#ff99ff"" class=""titleField"">&nbsp;O/C - " & strContractNumber & " -> " & OCQuantity & " Unit<td></tr>")
                            Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & strPONumber & " -> " & POQuantity & " Unit<td></tr>")

                            For Each obj As SalesOrder In arrSalesOrder
                                Dim strSONumber As String = obj.SONumber
                                Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;S/O - " & strSONumber & " <td></tr>")

                                If Not IsNothing(obj.LogisticDCHeader) Then
                                    Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;D/C - " & obj.LogisticDCHeader.DebitChargeNo & " <td></tr>")
                                End If

                                Dim index As Integer = 0
                                For Each DailyPaymentItem As DailyPayment In obj.DailyPayments
                                    index = index + 1
                                    Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                                Next

                                index = 0
                                For Each item As DeliveryOrder In obj.DeliveryOrders
                                    index = index + 1
                                    Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & item.DONumber & " : " & item.ChassisMaster.ChassisNumber & " - " & item.ChassisMaster.VechileColor.MaterialNumber & "<td></tr>")
                                Next

                                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(Invoice), "SalesOrder.ID", _
                                                        MatchType.Exact, obj.ID))
                                criterias1.opAnd(New Criteria(GetType(Invoice), "RowStatus", MatchType.Exact, _
                                    CType(DBRowStatus.Active, Short)))

                                arrInvoiceHeader = New InvoiceFacade(User).Retrieve(criterias1)
                                index = 0
                                For Each InvoiceItem As Invoice In arrInvoiceHeader
                                    index = index + 1
                                    If InvoiceItem.InvoiceType = "ZA7O" Then
                                        Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                        If Not IsNothing(InvoiceItem.LogisticDN) Then
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""#ff0000"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                        End If
                                    Else
                                        Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceItem.InvoiceNumber & "<td></tr>")
                                        If Not IsNothing(InvoiceItem.LogisticDN) Then
                                            index = index + 1
                                            Response.Write("<tr><td bgcolor=""##ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & index & ". Debit Memo - " & InvoiceItem.LogisticDN.DebitMemoNo & " <td></tr>")
                                        End If
                                    End If

                                Next


                                GenFakturString(obj, 17)

                                'index = 0
                                'For Each DOItem As DeliveryOrder In obj.DeliveryOrders
                                '    index = index + 1
                                '    If DOItem.ChassisMaster.FakturStatus = 0 Then 'Baru
                                '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Baru    :" & DOItem.ChassisMaster.EndCustomer.FakturDate & "<td></tr>")
                                '        Else

                                '        End If
                                '    ElseIf DOItem.ChassisMaster.FakturStatus = 4 Then 'Selesai
                                '        If Not DOItem.ChassisMaster.EndCustomer Is Nothing Then
                                '            Response.Write("<tr><td bgcolor=""" & bgColorFaktur & """>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Faktur - Selesai :" & DOItem.ChassisMaster.EndCustomer.OpenFakturDate & " " & DOItem.ChassisMaster.EndCustomer.FakturNumber & "<td></tr>")
                                '        End If

                                '    End If
                                'Next

                            Next
                        End If
                    End If
                End If


                'If Not objVAlurInvoice Is Nothing Then

                'If objVAlurInvoice.ID > 0 Then
                '    Response.Write("<tr><td bgcolor=""#99ffcc"">P/K - " & objVAlurInvoice.PKNumber & " -> " & objVAlurInvoice.PKQty & " Unit<td></tr>")
                '    Response.Write("<tr><td bgcolor=""#ff99ff"" class=""titleField"">&nbsp;O/C - " & objVAlurInvoice.ContractNumber & " -> " & objVAlurInvoice.ContractQty & " Unit<td></tr>")
                '    Response.Write("<tr><td bgcolor=""#ccffff"">&nbsp;&nbsp;&nbsp;P/O - " & objVAlurInvoice.PONumber & " :S/O - " & objVAlurInvoice.SONumber & " -> " & objVAlurInvoice.POQty & " Unit<td></tr>")

                '    Dim criterias = New CriteriaComposite(New Criteria(GetType(InvoiceDetail), "InvoiceHeader.ID", _
                '                            MatchType.Exact, objVAlurInvoice.ID))
                '    criterias.opAnd(New Criteria(GetType(InvoiceDetail), "RowStatus", MatchType.Exact, _
                '        CType(DBRowStatus.Active, Short)))
                '    arrInvoiceDetail = New InvoiceDetailFacade(User).Retrieve(criterias)
                '    objInvoiceHeader = New InvoiceHeaderFacade(User).Retrieve(CInt(value))

                '    Dim index As Integer = 0
                '    For Each DOItem As InvoiceDetail In arrInvoiceDetail
                '        index = index + 1
                '            Response.Write("<tr><td bgcolor=""#ffff99"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". D/O - " & DOItem.ChassisMaster.DONumber & "<td></tr>")
                '    Next

                '    Dim obj As New DailyPayment

                '    criterias = New CriteriaComposite(New Criteria(GetType(DailyPayment), "POHeader.ID", _
                '                            MatchType.Exact, objInvoiceHeader.POHeader.ID))
                '    criterias.opAnd(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, _
                '        CType(DBRowStatus.Active, Short)))
                '    arrDailyPayment = New DailyPaymentFacade(User).Retrieve(criterias)
                '    index = 0
                '    For Each DailyPaymentItem As DailyPayment In arrDailyPayment
                '        index = index + 1
                '        Response.Write("<tr><td bgcolor=""#99ffcc"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Giro - " & DailyPaymentItem.SlipNumber & "<td></tr>")
                '    Next

                '    Response.Write("<tr><td bgcolor=""#ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & objVAlurInvoice.InvoiceNumber & "<td></tr>")

                '    'index = 0
                '    'For Each InvoiceHeaderItem As InvoiceHeader In arrInvoiceHeader
                '    '    index = index + 1
                '    '    Response.Write("<tr><td bgcolor=""#ff99ff"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & index & ". Invoice - " & InvoiceHeaderItem.InvoiceNumber & "<td></tr>")
                '    'Next
                '    End If
                'End If
            End If

            Response.Write("<tr><td><input type=button onclick=""window.close()"" value=""Tutup""></td></tr>")
            Response.Write("</table>")
        End If
    End Sub

End Class
