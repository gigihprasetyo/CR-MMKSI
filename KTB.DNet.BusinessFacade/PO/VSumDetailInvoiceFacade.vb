#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 10/7/2005 - 1:28:25 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.SAP

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class VSumDetailInvoiceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_VSumDetailInvoiceMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_VSumDetailInvoiceMapper = MapperFactory.GetInstance.GetMapper(GetType(VSumDetailInvoice).ToString)
            Me.m_TransactionManager = New TransactionManager
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.VSumDetailInvoice))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.PODetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As VSumDetailInvoice
            Return CType(m_VSumDetailInvoiceMapper.Retrieve(ID), VSumDetailInvoice)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal Code As String) As VSumDetailInvoice
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VSumDetailInvoice), "ID", MatchType.Exact, Code))

            Dim VSumDetailInvoiceColl As ArrayList = m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias)
            If (VSumDetailInvoiceColl.Count > 0) Then
                Return CType(VSumDetailInvoiceColl(0), VSumDetailInvoice)
            End If
            Return Nothing
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(VSumDetailInvoice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VSumDetailInvoiceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal ContractHeaderID As Integer) As ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), "ContractHeaderID", _
                MatchType.Exact, ContractHeaderID))
            Return m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(VSumDetailInvoice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VSumDetailInvoiceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(VSumDetailInvoice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VSumDetailInvoiceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _VSumDetailInvoice As ArrayList = m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias)
            Return _VSumDetailInvoice
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VSumDetailInvoiceColl As ArrayList = m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VSumDetailInvoiceColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VSumDetailInvoice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim VSumDetailInvoiceColl As ArrayList = m_VSumDetailInvoiceMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return VSumDetailInvoiceColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VSumDetailInvoiceColl As ArrayList = m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return VSumDetailInvoiceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VSumDetailInvoiceColl As ArrayList = m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(VSumDetailInvoice), columnName, matchOperator, columnValue))
            Return VSumDetailInvoiceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(VSumDetailInvoice), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), columnName, matchOperator, columnValue))

            Return m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region



#Region "Custom Method"

        Public Function IsEnabledCreditControl(ByVal objDealer As Dealer) As Boolean
            Dim objTransaction As TransactionControl = New DealerFacade(Me.m_userPrincipal).RetrieveTransactionControl(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.CreditControl).ToString)
            If (objTransaction Is Nothing) Then
                Return True
            Else
                If objTransaction.Status = EnumDealerStatus.DealerStatus.Aktive Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function RetrieveIDContract(ByVal id As Integer) As VSumDetailInvoice
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VSumDetailInvoice), "ContractHeader.ID", MatchType.Exact, id))

            Dim VSumDetailInvoiceColl As ArrayList = m_VSumDetailInvoiceMapper.RetrieveByCriteria(criterias)
            If (VSumDetailInvoiceColl.Count > 0) Then
                Return CType(VSumDetailInvoiceColl(0), VSumDetailInvoice)
            End If
            Return New VSumDetailInvoice
        End Function

        Private Sub UpdatePODetail(ByVal obj As PODetail)
            Dim _PODetailFacade As PODetailFacade = New PODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            _PODetailFacade.Update(obj)
        End Sub

        Private Function GetPoDetailByVSumDetailInvoiceAndLineNumber(ByVal poheaderID As Integer, ByVal lineItem As Integer) As PODetail
            Dim _PODetailFacade As PODetailFacade = New PODetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PODetail), "VSumDetailInvoice.ID", MatchType.Exact, poheaderID))
            criterias.opAnd(New Criteria(GetType(PODetail), "LineItem", MatchType.Exact, lineItem))
            Dim listPODetail As ArrayList = _PODetailFacade.Retrieve(criterias)
            If listPODetail.Count > 0 Then
                Return CType(listPODetail(0), PODetail)
            Else
                Return New PODetail
            End If
        End Function

        Private Function IsSONumberExist(ByVal soNumber As String, ByVal poNumber As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VSumDetailInvoice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VSumDetailInvoice), "SONumber", MatchType.Exact, soNumber.Trim))
            criterias.opAnd(New Criteria(GetType(VSumDetailInvoice), "PONumber", MatchType.Exact, poNumber.Trim))
            Dim aggregates As New Aggregate(GetType(VSumDetailInvoice), "ID", AggregateType.Count)
            If CType(m_VSumDetailInvoiceMapper.RetrieveScalar(aggregates, criterias), Integer) > 0 Then
                Return True
            End If
            Return False
        End Function



        Private Function IsSPLExistInSAPList(ByVal spl As String, ByVal sapList As ArrayList) As Boolean
            For Each item As String In sapList
                If item.ToUpper.Trim = spl.ToUpper.Trim Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private Function GetDueDateUsingDailyPayment(ByVal objPO As VSumDetailInvoice) As Date
            Dim objDailyPaymentFacade As DailyPaymentFacade = New DailyPaymentFacade(Me.m_userPrincipal)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "VSumDetailInvoice.ID", MatchType.Exact, objPO.ID))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.PaymentPurposeCode", MatchType.[Partial], "VH"))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, 0))
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DailyPayment), "ID", Sort.SortDirection.DESC))
            Dim list As ArrayList = objDailyPaymentFacade.Retrieve(criterias, sortColl)
            Dim objPayment As DailyPayment
            If list.Count > 0 Then
                objPayment = CType(list(0), DailyPayment)
                Return objPayment.BaselineDate
            Else
                Return New Date(1900, 1, 1)
            End If
        End Function



        Public Function GetPaymentRejection(ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment) As Integer
            'TODO PENDING
            Return 0
        End Function

        Private Function FillCreditCeilingFromSAP(ByVal objSAPCR As ZFUST0042) As SAPCreditCeiling
            Dim objCreditCeiling As SAPCreditCeiling = New SAPCreditCeiling
            Try
                objCreditCeiling.DealerCode = objSAPCR.Knkli
                objCreditCeiling.BlokedAmount = CInt(objSAPCR.Blimk)
                objCreditCeiling.BlokedName = objSAPCR.Blnam
                objCreditCeiling.BufferDay = 0
                objCreditCeiling.CeilingAmount = CLng(objSAPCR.Klimk)
                objCreditCeiling.CreditAccount = objSAPCR.Knkli
                objCreditCeiling.ModifyName = objSAPCR.Mfnam
                objCreditCeiling.SPLNumber = objSAPCR.Splnm
                objCreditCeiling.TemporaryCode = objSAPCR.Tmcod
                objCreditCeiling.TemporaryKind = objSAPCR.Tmknd
                objCreditCeiling.TemporaryType = objSAPCR.Tmtyp
                objCreditCeiling.PeriodMonth = CInt(objSAPCR.Klmonth)
                objCreditCeiling.PeriodYear = CInt(objSAPCR.Klyear)
                If objSAPCR.Bldat.Length > 3 Then
                    If Not objSAPCR.Bldat.Substring(0, 3) = "000" Then
                        objCreditCeiling.BlockedDate = ConvertToDate(objSAPCR.Bldat)
                    End If
                End If
                If objSAPCR.Klfrm.Length > 3 Then
                    If Not objSAPCR.Klfrm.Substring(0, 3) = "000" Then
                        objCreditCeiling.ValidFrom = ConvertToDate(objSAPCR.Klfrm)
                    End If
                End If
                If objSAPCR.Kldto.Length > 0 Then
                    If Not objSAPCR.Kldto.Substring(0, 3) = "000" Then
                        objCreditCeiling.ValidTo = ConvertToDate(objSAPCR.Kldto)
                    End If
                End If

            Catch ex As Exception
            End Try
            Return objCreditCeiling
        End Function

        Private Function ConvertToDate(ByVal strDate As String) As Date
            Dim _dt As Date = New Date(strDate.Substring(4, 4), strDate.Substring(2, 2), strDate.Substring(0, 2))
            Return _dt
        End Function

        'Public Function CountACC(ByVal objContract As ContractHeader, ByVal objDealer As Dealer, ByVal objTOP As TermOfPayment, ByVal currentPO As VSumDetailInvoice, ByVal ConStr As String) As Long
        '    Dim result As Double = 0
        '    Dim oParamSAPCreditCeiling As SAPCreditCeiling = New SAPCreditCeiling
        '    oParamSAPCreditCeiling.DealerCode = objDealer.LegalStatus
        '    oParamSAPCreditCeiling.PeriodMonth = objContract.ContractPeriodMonth
        '    oParamSAPCreditCeiling.PeriodYear = objContract.ContractPeriodYear
        '    oParamSAPCreditCeiling.SPLNumber = objContract.SPLNumber
        '    Dim listSapCr As ArrayList = New ArrayList
        '    listSapCr.Add(oParamSAPCreditCeiling)
        '    Dim isRegular As Boolean = False
        '    Dim objSAPCreditCeiling As SAPCreditCeiling = GetCreditCeiling(listSapCr, ConStr)(0)
        '    If objSAPCreditCeiling.TemporaryType = "" Then
        '        isRegular = True
        '    End If
        '    Dim CreditCeilingAmount As Double = 0
        '    Dim totalCreditExposure As Double = 0
        '    Dim totalPaymentRejection As Double = 0
        '    If Not objSAPCreditCeiling Is Nothing Then
        '        CreditCeilingAmount = objSAPCreditCeiling.CeilingAmount - objSAPCreditCeiling.BlokedAmount
        '        totalCreditExposure = GetCreditExposure(objDealer, objTOP, currentPO, isRegular, objContract.SPLNumber.Trim, ConStr)
        '        totalPaymentRejection = GetPaymentRejection(objDealer, objTOP)
        '    End If
        '    result = CreditCeilingAmount - totalCreditExposure - totalPaymentRejection
        '    Return result
        'End Function

        Public Function GetCreditCeiling(ByVal ojbSAPCRList As ArrayList, ByVal constr As String) As ArrayList
            Dim oSAPDnet As SAPDNet = New SAPDNet(constr)
            Dim listSAPCRReturn As ArrayList = New ArrayList
            Dim oCR As SAPCreditCeiling
            Try
                Dim list As ArrayList = oSAPDnet.GetCreditControl(ojbSAPCRList)
                'For Each item As ZFUST0042Table In list
                '    For Each dt As ZFUST0042 In item
                '        oCR = New SAPCreditCeiling
                '        oCR = FillCreditCeilingFromSAP(dt)
                '        listSAPCRReturn.Add(oCR)
                '    Next
                'Next
                For Each dt As ZFUST0042 In list
                    oCR = New SAPCreditCeiling
                    oCR = FillCreditCeilingFromSAP(dt)
                    listSAPCRReturn.Add(oCR)
                Next
                Return listSAPCRReturn
            Catch ex As Exception
                Throw ex
            End Try
            Return New ArrayList
        End Function

        Public Function GetCreditOutstandingReportCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal pTempKind As String, ByVal ConStr As String, ByRef TotalRow As Integer) As ArrayList
            Dim Criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidFrom", MatchType.LesserOrEqual, pDueDate))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidTo", MatchType.GreaterOrEqual, pDueDate))
            Dim ListSPL As New ArrayList
            ListSPL = New SPLFacade(m_userPrincipal).Retrieve(Criterias)
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            Dim listSapCr As ArrayList = New ArrayList
            For Each sLegalStatus As String In pLegalStatus.Split(";")
                If sLegalStatus.Trim <> String.Empty Then
                    For Each SPLItem As SPL In ListSPL
                        oParamSAPCreditCeiling = New SAPCreditCeiling
                        oParamSAPCreditCeiling.DealerCode = sLegalStatus
                        oParamSAPCreditCeiling.PeriodMonth = pDueDate.Month
                        oParamSAPCreditCeiling.PeriodYear = pDueDate.Year
                        oParamSAPCreditCeiling.SPLNumber = SPLItem.SPLNumber
                        listSapCr.Add(oParamSAPCreditCeiling)
                    Next
                End If
            Next
            Dim objSAPCreditCeiling As ArrayList = GetCreditCeiling(listSapCr, ConStr)
            Dim ResultList As New ArrayList
            If objSAPCreditCeiling.Count > 0 Then
                For Each item As SAPCreditCeiling In objSAPCreditCeiling
                    If Not item.TemporaryType.Trim = String.Empty Then
                        If pTempKind = String.Empty Then
                            ResultList.Add(item)
                        Else
                            If item.TemporaryKind.IndexOf(pTempKind.Trim()) >= 0 Then
                                ResultList.Add(item)
                            End If
                        End If
                    End If
                Next
            End If
            TotalRow = ResultList.Count
            Return ResultList
        End Function

        Public Function GetCreditTempReportCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal ConStr As String) As ArrayList
            Dim Criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidFrom", MatchType.LesserOrEqual, pDueDate))
            Criterias.opAnd(New Criteria(GetType(SPL), "ValidTo", MatchType.GreaterOrEqual, pDueDate))
            Dim ListSPL As New ArrayList
            ListSPL = New SPLFacade(m_userPrincipal).Retrieve(Criterias)
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            Dim listSapCr As ArrayList = New ArrayList
            For Each sLegalStatus As String In pLegalStatus.Split(";")
                If sLegalStatus.Trim <> String.Empty Then
                    For Each SPLItem As SPL In ListSPL
                        oParamSAPCreditCeiling = New SAPCreditCeiling
                        oParamSAPCreditCeiling.DealerCode = sLegalStatus
                        oParamSAPCreditCeiling.PeriodMonth = pDueDate.Month
                        oParamSAPCreditCeiling.PeriodYear = pDueDate.Year
                        oParamSAPCreditCeiling.SPLNumber = SPLItem.SPLNumber
                        listSapCr.Add(oParamSAPCreditCeiling)
                    Next
                End If
            Next
            Dim objSAPCreditCeiling As ArrayList = GetCreditCeiling(listSapCr, ConStr)
            Dim ResultList As New ArrayList
            If objSAPCreditCeiling.Count > 0 Then
                For Each item As SAPCreditCeiling In objSAPCreditCeiling
                    If Not item.TemporaryType.Trim = String.Empty Then
                        ResultList.Add(item)
                    End If
                Next
            End If
            Return ResultList
        End Function

        Public Function GetCreditPosotionReportCeiling(ByVal pLegalStatus As String, ByVal pDueDate As DateTime, ByVal ConStr As String, ByRef TotalRow As Integer) As ArrayList
            Dim oParamSAPCreditCeiling As SAPCreditCeiling
            Dim listSapCr As ArrayList = New ArrayList
            For Each sLegalStatus As String In pLegalStatus.Split(";")
                If sLegalStatus.Trim <> String.Empty Then
                    oParamSAPCreditCeiling = New SAPCreditCeiling
                    oParamSAPCreditCeiling.DealerCode = sLegalStatus
                    oParamSAPCreditCeiling.PeriodMonth = pDueDate.Month
                    oParamSAPCreditCeiling.PeriodYear = pDueDate.Year
                    oParamSAPCreditCeiling.SPLNumber = ""
                    listSapCr.Add(oParamSAPCreditCeiling)
                End If
            Next
            Dim objSAPCreditCeiling As ArrayList = GetCreditCeiling(listSapCr, ConStr)
            TotalRow = objSAPCreditCeiling.Count
            Return objSAPCreditCeiling
        End Function
#End Region

    End Class

End Namespace