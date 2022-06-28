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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 9/13/2007 - 2:10:37 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Configuration

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Salesman


#End Region

Namespace KTB.DNet.BusinessFacade.SAP

    Public Class SAPRegisterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SAPRegisterMapper As IMapper

        Private m_TransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SAPRegisterMapper = MapperFactory.GetInstance.GetMapper(GetType(SAPRegister).ToString)
            Me.m_TransactionManager = New TransactionManager


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SAPRegister
            Return CType(m_SAPRegisterMapper.Retrieve(ID), SAPRegister)
        End Function

        Public Function Retrieve(ByVal Code As String) As SAPRegister
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPRegisterCode", MatchType.Exact, Code))
            criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.SAPNumber", MatchType.Exact, Code))
            Dim SAPRegisterColl As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias)
            If (SAPRegisterColl.Count > 0) Then
                Return CType(SAPRegisterColl(0), SAPRegister)
            End If
            Return New SAPRegister
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SAPRegisterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SAPRegisterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SAPRegisterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPRegister), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPRegisterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPRegister), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPRegisterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SAPRegister), SortColumn, sortDirection))
            Dim SAPRegisterColl As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SAPRegisterColl
        End Function

        Public Function RetrieveActiveList(ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SAPRegister), SortColumn, sortDirection))
            'Dim SAPRegisterColl As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Dim SAPRegisterColl As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias, sortColl)

            Return SAPRegisterColl
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SAPRegister As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias)
            Return _SAPRegister
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPRegisterColl As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SAPRegisterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SAPRegisterColl As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SAPRegisterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPRegisterColl As ArrayList = m_SAPRegisterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SAPRegister), columnName, matchOperator, columnValue))
            Return SAPRegisterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPRegister), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), columnName, matchOperator, columnValue))

            Return m_SAPRegisterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region


#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "SAPRegisterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SAPRegister), "SAPRegisterCode", AggregateType.Count)
            Return CType(m_SAPRegisterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function UpdateNilai(ByVal arrToUpdate As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrToUpdate.Count > 0 Then
                        For Each item As SAPRegister In arrToUpdate
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function UpdateTransaction(ByVal arrToUpdate As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrToUpdate.Count > 0 Then
                        For Each item As SAPRegister In arrToUpdate
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            returnValue = item.ID
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

        Public Function Update(ByVal arrToUpdate As ArrayList) As Integer
            Dim returnVal As Integer = -1
            If arrToUpdate.Count > 0 Then
                For Each Item As SAPRegister In arrToUpdate
                    Try
                        returnVal = m_SAPRegisterMapper.Update(Item, m_userPrincipal.Identity.Name)
                    Catch ex As Exception
                        returnVal = -1
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                        If rethrow Then
                            Throw
                        End If
                        Return returnVal
                    End Try
                Next
                Return returnVal
            End If
        End Function
#End Region

#Region "Generate Report"

        Private CodeSalesman As String '= Config
        Private CodeSalesCounter As String '= Config
        Private CodeSpv As String '= Config
        Private CodeSalesMgr As String '= Config
        Private NominatorAmount As Integer '= Config
        Private ArrLstSAP As ArrayList
        Private _sls As String = KTB.DNet.Lib.WebConfig.GetValue("SlmanCode")
        Private _slc As String = KTB.DNet.Lib.WebConfig.GetValue("SCntCode")
        Private _spv As String = KTB.DNet.Lib.WebConfig.GetValue("SSpvCode")
        Private _ssm As String = KTB.DNet.Lib.WebConfig.GetValue("SManCode")


        Public Function FillReportField(ByVal idPeriod As Integer, ByVal _dealer As String, ByVal _area As String) As Integer


            Dim arlAllPos As ArrayList  'All Position
            Dim arlSls As ArrayList 'Salesman 
            Dim arlSlc As ArrayList 'Salesman  Counter
            Dim arlSPV As ArrayList 'Supervisor
            Dim arlSManager As ArrayList 'SalesManager
            Dim positioncode As String = String.Empty
            Dim fakturCount As Integer = 0

            ArrLstSAP = New ArrayList

            'Peringkat per Area
            Dim arlArea As ArrayList = New SalesmanAreaFacade(Me.m_userPrincipal).RetrieveList()
            For Each objArea As SalesmanArea In arlArea
                If objArea.SalesmanHeaders.Count > 0 Then
                    ArrLstSAP = New ArrayList
                    arlSls = GetAllSAPRegister(idPeriod, _dealer, _sls, objArea.ID.ToString)
                    ProcessSls(arlSls, ArrLstSAP)

                    arlSlc = GetAllSAPRegister(idPeriod, _dealer, _slc, objArea.ID.ToString)
                    ProcessSls(arlSlc, ArrLstSAP)

                    arlSPV = GetAllSAPRegister(idPeriod, _dealer, _spv, objArea.ID.ToString)
                    ProcessSPV(arlSPV, ArrLstSAP)

                    arlSManager = GetAllSAPRegister(idPeriod, _dealer, _ssm, objArea.ID.ToString)
                    ProcessSM(arlSManager, ArrLstSAP)

                    Dim nresult As Integer = 0
                    Try
                        nresult = New SAPRegisterFacade(Me.m_userPrincipal).Update(ArrLstSAP)
                    Catch ex As Exception
                        Return -1
                    End Try

                End If
            Next



        End Function

        Public Function GetAllSAPRegister(ByVal idPeriod As Integer, ByVal strDealer As String, ByVal strposition As String, ByVal _area As String) As ArrayList
            Dim criteriaToInclude As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaToInclude.opAnd(New criteria(GetType(SalesmanHeader), "JobPosition.Code", MatchType.Exact, strposition))

            If strDealer <> String.Empty Then
                criteriaToInclude.opAnd(New criteria(GetType(SalesmanHeader), "Dealer.DealerCode", MatchType.InSet, "('" & strDealer.Replace(";", "','") & "')"))
            End If

            If CInt(_area) > 0 Then
                criteriaToInclude.opAnd(New criteria(GetType(SalesmanHeader), "SalesmanArea.ID", MatchType.InSet, "(" & _area & ")"))
            End If

            Dim arlToInclude As ArrayList = New SalesmanHeaderFacade(Me.m_userPrincipal).Retrieve(criteriaToInclude)

            If arlToInclude.Count = 0 Then
                Return New ArrayList
            End If

            Dim strToInclude As String = ""

            For Each item As SalesmanHeader In arlToInclude
                strToInclude = strToInclude & item.ID.ToString & ","
            Next

            If strToInclude <> String.Empty Then
                strToInclude = Left(strToInclude, strToInclude.Length - 1)
            Else
                strToInclude = "0"
            End If


            Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New criteria(GetType(SAPRegister), "SAPPeriod.ID", MatchType.Exact, idPeriod))
            If strToInclude.Trim.Length > 0 Then
                criteria.opAnd(New criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(" & strToInclude & ")"))
            End If

            Return New SAPRegisterFacade(Me.m_userPrincipal).Retrieve(criteria)

        End Function

        Public Function GetListofSAPRegister(ByVal idPeriod As Integer, ByVal salesID As String) As ArrayList
            Dim arrlst As ArrayList = New ArrayList
            Dim criteriaSR As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If idPeriod > 0 Then
                criteriaSR.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.ID", MatchType.Exact, idPeriod))
            End If

            If salesID <> String.Empty Then
                criteriaSR.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(" & salesID & ")"))
            End If

            arrlst = New SAPRegisterFacade(Me.m_userPrincipal).Retrieve(criteriaSR)
            Return arrlst
        End Function

        Public Function GetProspectGrade(ByVal objSAPRegister As SAPRegister) As Integer

            Dim objSAPPeriod As SAPPeriod = objSAPRegister.SAPPeriod
            Dim StartDate As Date = objSAPPeriod.StartDate
            Dim EndDate As Date = objSAPPeriod.EndDate



            Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "ProspectDate", MatchType.GreaterOrEqual, StartDate))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Lesser, EndDate.AddDays(1)))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "Status", MatchType.Exact, CInt(EnumSAPCustomerStatus.SAPCustomerStatus.Prospect)))

            Dim agg As Aggregate = New Aggregate(GetType(SAPCustomer), "ID", AggregateType.Count)

            Dim prospectAmount As Integer = New SAPCustomerFacade(Me.m_userPrincipal).RetrieveScalar(criteria, agg)

            If prospectAmount >= 60 Then
                Return 100
            ElseIf prospectAmount >= 45 And prospectAmount <= 59 Then
                Return 75
            ElseIf prospectAmount >= 30 And prospectAmount <= 44 Then
                Return 50
            ElseIf prospectAmount >= 15 And prospectAmount <= 29 Then
                Return 25
            ElseIf prospectAmount >= 0 And prospectAmount <= 15 Then
                Return 0
            End If

        End Function

        Public Function GetHotProspectGrade(ByVal objSAPRegister As SAPRegister) As Integer
            Dim objSAPPeriod As SAPPeriod = objSAPRegister.SAPPeriod
            Dim StartDate As Date = objSAPPeriod.StartDate
            Dim EndDate As Date = objSAPPeriod.EndDate

            'Dim CodeSalesman As String = KTB.DNet.Lib.WebConfig.GetValue("SlmanCode")
            'Dim CodeSalesCounter As String = KTB.DNet.Lib.WebConfig.GetValue("SCntCode")

            Dim criteria As CriteriaComposite = New CriteriaComposite(New criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "ProspectDate", MatchType.GreaterOrEqual, StartDate))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "ProspectDate", MatchType.Lesser, EndDate.AddDays(1)))
            criteria.opAnd(New criteria(GetType(SAPCustomer), "Status", MatchType.Exact, CInt(EnumSAPCustomerStatus.SAPCustomerStatus.Hot_Prospect)))


            Dim agg As Aggregate = New Aggregate(GetType(SAPCustomer), "ID", AggregateType.Count)

            Dim prospectAmount As Integer = New SAPCustomerFacade(Me.m_userPrincipal).RetrieveScalar(criteria, agg)

            If objSAPRegister.SalesmanHeader.JobPosition.Code = CodeSalesman Then

                If prospectAmount >= 10 Then
                    Return 100
                ElseIf prospectAmount >= 7 And prospectAmount <= 9 Then
                    Return 75
                ElseIf prospectAmount >= 4 And prospectAmount <= 6 Then
                    Return 50
                ElseIf prospectAmount >= 2 And prospectAmount <= 3 Then
                    Return 25
                ElseIf prospectAmount >= 0 And prospectAmount <= 1 Then
                    Return 0
                End If

            ElseIf objSAPRegister.SalesmanHeader.JobPosition.Code = CodeSalesCounter Then

                If prospectAmount >= 15 Then
                    Return 100
                ElseIf prospectAmount >= 11 And prospectAmount <= 14 Then
                    Return 75
                ElseIf prospectAmount >= 7 And prospectAmount <= 10 Then
                    Return 50
                ElseIf prospectAmount >= 3 And prospectAmount <= 6 Then
                    Return 25
                ElseIf prospectAmount >= 0 And prospectAmount <= 2 Then
                    Return 0
                End If


            End If


        End Function

        Public Function GetFakturGrade(ByVal objSAPRegister As SAPRegister, ByRef fakturCount As Integer) As Integer
            Dim _nresult As Integer = 0
            Dim CodeSalesman As String = KTB.DNet.Lib.WebConfig.GetValue("SlmanCode")
            Dim CodeSalesCounter As String = KTB.DNet.Lib.WebConfig.GetValue("SCntCode")

            Dim aggr As Aggregate = New Aggregate(GetType(ChassisMaster), "ID", AggregateType.Count)
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "SalesmanID", MatchType.Exact, objSAPRegister.SalesmanHeader.ID))

            Dim _IDChassis As String = String.Empty
            Dim critForFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critForFaktur.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.OpenFakturDate", MatchType.GreaterOrEqual, objSAPRegister.SAPPeriod.StartDate))
            critForFaktur.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.OpenFakturDate", MatchType.LesserOrEqual, objSAPRegister.SAPPeriod.EndDate))
            'critForFaktur.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


            '---get list DeliverySalesmanHeader
            Dim arlDeliveryCustomer As ArrayList = New KTB.DNet.BusinessFacade.DealerReport.DeliveryCustomerHeaderFacade(Me.m_userPrincipal).Retrieve(crits)
            If arlDeliveryCustomer.Count > 0 Then
                '--get chassisMasterID in DeliverySalesmanDetail
                For Each _item As DeliveryCustomerHeader In arlDeliveryCustomer
                    For Each _detail As DeliveryCustomerDetail In _item.DeliveryCustomerDetails
                        If _detail.RowStatus = CType(DBRowStatus.Active, Short) Then
                            _IDChassis = _IDChassis & _detail.ChassisMaster.ID & ","
                        End If
                    Next
                Next
                If _IDChassis.Trim.ToString <> String.Empty Then
                    critForFaktur.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(" & Left(_IDChassis, _IDChassis.Length - 1) & ")"))
                End If
            End If

            _nresult = New KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterFacade(Me.m_userPrincipal).RetrieveScalar(critForFaktur, aggr)
            fakturCount = _nresult
            If objSAPRegister.SalesmanHeader.JobPosition.Code = CodeSalesman Then  '--salesman
                Select Case _nresult
                    Case Is >= 5
                        Return 100
                    Case Is >= 4
                        Return 75
                    Case Is >= 3
                        Return 50
                    Case Is >= 2
                        Return 25
                    Case Is >= 0
                        Return 0
                End Select
            ElseIf objSAPRegister.SalesmanHeader.JobPosition.Code = CodeSalesCounter Then  '---salescounter

                Select Case _nresult
                    Case Is >= 8
                        Return 100
                    Case Is >= 6
                        Return 75
                    Case Is >= 4
                        Return 50
                    Case Is >= 2
                        Return 25
                    Case Is >= 0
                        Return 0
                End Select
            End If

        End Function

        Public Function GetPDIGrade(ByVal objsapRegister As SAPRegister, ByVal fakturCount As Integer) As Integer
            'TODO PDI
            Dim _nresult As Integer = 0

            Dim aggr As Aggregate = New Aggregate(GetType(PDI), "ID", AggregateType.Count)
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "SalesmanID", MatchType.Exact, objsapRegister.SalesmanHeader.ID))
            'crits.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "DeliveryCustomerDetail.Rowstatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


            Dim _IDChassis As String = String.Empty
            Dim critForPDI As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'critForPDI.opAnd(New Criteria(GetType(PDI), "EndCustomer.OpenFakturDate", MatchType.GreaterOrEqual, objsapRegister.SAPPeriod.StartDate))
            'critForPDI.opAnd(New Criteria(GetType(PDI), "EndCustomer.OpenFakturDate", MatchType.LesserOrEqual, objsapRegister.SAPPeriod.EndDate))


            '---get list DeliverySalesmanHeader
            Dim arlSalesmanheader As ArrayList = New KTB.DNet.BusinessFacade.DealerReport.DeliveryCustomerHeaderFacade(Me.m_userPrincipal).Retrieve(crits)
            If arlSalesmanheader.Count > 0 Then
                '--get chassisMasterID in DeliverySalesmanDetail
                For Each _item As DeliveryCustomerHeader In arlSalesmanheader
                    For Each _detail As DeliveryCustomerDetail In _item.DeliveryCustomerDetails
                        _IDChassis = _IDChassis & _detail.ChassisMaster.ID & ","
                    Next
                Next
                If _IDChassis.Trim.ToString <> String.Empty Then
                    critForPDI.opAnd(New Criteria(GetType(PDI), "ID", MatchType.InSet, "(" & Left(_IDChassis, _IDChassis.Length - 1) & ")"))
                End If
            End If

            _nresult = New KTB.DNet.BusinessFacade.Service.PDIFacade(Me.m_userPrincipal).RetrieveScalar(critForPDI, aggr)
            If fakturCount = 0 Then
                _nresult = 0
            Else
                _nresult = _nresult / fakturCount
            End If

            Select Case _nresult
                Case Is >= 100
                    Return 100
                Case Is >= 71
                    Return 75
                Case Is >= 0
                    Return 50
            End Select
        End Function

        Public Function GetPresentasiSWAP7PKT() As Integer

        End Function

        Public Function GetAveSubOrdinate() As Integer

        End Function

        Public Function GetFreQuencyTraining() As Integer

        End Function

        Private Function GetFakturCount(ByVal objSAPRegister As SAPRegister) As Integer
            Dim _nresult As Integer = 0

            Dim aggr As Aggregate = New Aggregate(GetType(ChassisMaster), "ID", AggregateType.Count)
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DeliveryCustomerHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(DeliveryCustomerHeader), "SalesmanID", MatchType.Exact, objSAPRegister.SalesmanHeader.ID))

            Dim _IDChassis As String = String.Empty
            Dim critForFaktur As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critForFaktur.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.OpenFakturDate", MatchType.GreaterOrEqual, objSAPRegister.SAPPeriod.StartDate))
            critForFaktur.opAnd(New Criteria(GetType(ChassisMaster), "EndCustomer.OpenFakturDate", MatchType.LesserOrEqual, objSAPRegister.SAPPeriod.EndDate))


            Dim arlDeliveryCustomer As ArrayList = New KTB.DNet.BusinessFacade.DealerReport.DeliveryCustomerHeaderFacade(Me.m_userPrincipal).Retrieve(crits)
            If arlDeliveryCustomer.Count > 0 Then
                '--get chassisMasterID in DeliverySalesmanDetail
                For Each _item As DeliveryCustomerHeader In arlDeliveryCustomer
                    For Each _detail As DeliveryCustomerDetail In _item.DeliveryCustomerDetails
                        If _detail.RowStatus = CType(DBRowStatus.Active, Short) Then
                            _IDChassis = _IDChassis & _detail.ChassisMaster.ID & ","
                        End If
                    Next
                Next
                If _IDChassis.Trim.ToString <> String.Empty Then
                    critForFaktur.opAnd(New Criteria(GetType(ChassisMaster), "ID", MatchType.InSet, "(" & Left(_IDChassis, _IDChassis.Length - 1) & ")"))
                End If
            End If

            _nresult = New KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterFacade(Me.m_userPrincipal).RetrieveScalar(critForFaktur, aggr)

            Return _nresult
        End Function


        Private Function listSubOrdinate(ByVal _ID As String) As String
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanHeader), "LeaderId", MatchType.Exact, CInt(_ID)))
            Dim arrlst As ArrayList = New ArrayList
            arrlst = New SalesmanHeaderFacade(Me.m_userPrincipal).Retrieve(criterias)
            Dim _str As String = String.Empty
            For Each item As SalesmanHeader In arrlst
                _str = _str & item.ID & ","
            Next
            If _str <> String.Empty Then
                Return Left(_str, _str.Length - 1)
            Else
                Return String.Empty
            End If


        End Function

        'Private Sub updateList(ByVal item As SAPRegister, ByRef arlSales As ArrayList, ByRef arltmp As ArrayList)
        '    Dim _sls As String = KTB.DNet.Lib.WebConfig.GetValue("SlmanCode")
        '    Dim _slc As String = KTB.DNet.Lib.WebConfig.GetValue("SCntCode")
        '    Dim _spv As String = KTB.DNet.Lib.WebConfig.GetValue("SSpvCode")
        '    Dim _ssm As String = KTB.DNet.Lib.WebConfig.GetValue("SManCode")

        '    If item.SalesmanHeader.JobPosition.Code = _sls Or item.SalesmanHeader.JobPosition.Code = _slc Then
        '        CalcData(item)
        '    ElseIf item.SalesmanHeader.JobPosition.Code = _spv Then
        '        '----hitung ulang data spv
        '    ElseIf item.SalesmanHeader.JobPosition.Code = _ssm Then
        '        '--hitung ulang data ssm
        '    End If


        '    Dim i As Integer = 0
        '    Dim isMatch As Boolean = False
        '    For Each objItem As SAPRegister In arlSales
        '        If item.ID = objItem.ID Then
        '            isMatch = True
        '            Exit For
        '        End If
        '        i = i + 1
        '    Next
        '    If isMatch Then
        '        arlSales.RemoveAt(i)
        '    End If
        '    arltmp.Add(item)

        'End Sub

        Private Function isProcess(ByVal _str As String, ByVal arlst As ArrayList) As Boolean
            For Each _listSR As SAPRegister In ArrLstSAP
                If _listSR.ID.ToString = _str Then
                    Return True
                End If
            Next
            Return False
        End Function

        Private Sub SetValueSales(ByRef obj As SAPRegister)

            Dim fakturCount As Integer = 0
            obj.RptProsPek = 0
            obj.RptHotProspek = 0
            obj.RptFaktur = 0
            obj.RptPDI = 0

            obj.RptProsPek = GetProspectGrade(obj)
            obj.RptHotProspek = GetHotProspectGrade(obj)
            obj.RptFaktur = GetFakturGrade(obj, fakturCount)
            If fakturCount = 0 Then
                obj.RptPDI = fakturCount
            Else
                obj.RptPDI = GetPDIGrade(obj, fakturCount)
            End If
            obj.GradeFinal = CInt((obj.WritingTestScore + obj.RptProsPek + obj.RptHotProspek + obj.RptFaktur + obj.RptPDI) / 5)
            obj.RptAvgScoreNominator = CInt(obj.GradeKelengkapan + obj.GradeSWAP / 2)




        End Sub

        Private Sub ProcessSls(ByVal arlSLS As ArrayList, ByRef arrToProcess As ArrayList)
            For Each item As SAPRegister In arlSLS
                SetValueSales(item)
                arrToProcess.Add(item)
            Next
        End Sub

        Private Sub ProcessSPV(ByVal arlSPV As ArrayList, ByRef arrtoProcess As ArrayList)
            Dim _subOrdinate As String = String.Empty
            Dim avgsubOrdinate As Integer = 0
            Dim AvgScoreSubOrdinate As Integer = 0
            Dim ValWriteTest As Integer = 0
            Dim valTargetsls As Integer = 0
            Dim valTargetslc As Integer = 0
            Dim SMAchievementTarget As Integer = 0

            For Each item As SAPRegister In arlSPV
                '--get list salesman
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.ID", MatchType.Exact, item.SAPPeriod.ID))

                _subOrdinate = listSubOrdinate(item.ID.ToString)
                If _subOrdinate <> String.Empty Then
                    criterias.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(" & _subOrdinate & ")"))

                    Dim arrLstSubOrdinate As ArrayList = New ArrayList
                    arrLstSubOrdinate = New SAPRegisterFacade(Me.m_userPrincipal).Retrieve(criterias)
                    If arrLstSubOrdinate.Count > 0 Then
                        For Each itemSls As SAPRegister In arrLstSubOrdinate
                            avgsubOrdinate = avgsubOrdinate + itemSls.GradeFinal
                            ValWriteTest = ValWriteTest + itemSls.WritingTestScore
                            If itemSls.SalesmanHeader.JobPosition.Code = _sls Then
                                valTargetsls = valTargetsls + GetFakturCount(itemSls)
                            ElseIf itemSls.SalesmanHeader.JobPosition.Code = _slc Then
                                valTargetslc = valTargetslc + GetFakturCount(itemSls)
                            End If
                        Next
                        item.RptAvgScoreSubOrdinate = AvgScoreSubOrdinate / arrLstSubOrdinate.Count
                        item.RptAchievement = (valTargetsls / 5) + (valTargetslc / 8)
                    Else
                        item.RptAvgScoreSubOrdinate = 0
                        item.RptAchievement = 0
                    End If
                Else
                    item.RptAvgScoreSubOrdinate = 0
                    item.RptAchievement = 0
                End If
                arrtoProcess.Add(item)
            Next

        End Sub

        Private Sub ProcessSM(ByVal arlSM As ArrayList, ByRef arrtoProcess As ArrayList)
            Dim _subOrdinate As String = String.Empty
            Dim avgsubOrdinate As Integer = 0
            Dim AvgScoreSubOrdinate As Integer = 0
            Dim ValWriteTest As Integer = 0
            Dim valTargetsls As Integer = 0
            Dim valTargetslc As Integer = 0
            Dim SMAchievementTarget As Integer = 0


            For Each item As SAPRegister In arlSM
                Dim criteriaSPV As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaSPV.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.ID", MatchType.Exact, item.SAPPeriod.ID))

                _subOrdinate = listSubOrdinate(item.SalesmanHeader.ID.ToString)
                If _subOrdinate <> String.Empty Then
                    criteriaSPV.opAnd(New Criteria(GetType(SAPRegister), "SalesmanHeader.ID", MatchType.InSet, "(" & _subOrdinate & ")"))

                    Dim arrLstSPVSubOrdinate As ArrayList = New ArrayList
                    arrLstSPVSubOrdinate = New SAPRegisterFacade(Me.m_userPrincipal).Retrieve(criteriaSPV)
                    If arrLstSPVSubOrdinate.Count > 0 Then
                        For Each itemSPV As SAPRegister In arrLstSPVSubOrdinate
                            '    '--get list salesman
                            '    Dim criteriaSLS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            '    criteriaSLS.opAnd(New Criteria(GetType(SAPRegister), "SAP.ID", "SAPPeriod.ID", MatchType.Exact, itemSPV.SAPPeriod.ID))

                            '    _subOrdinate = String.Empty
                            '    _subOrdinate = listSubOrdinate(itemSPV.ID.ToString)
                            '    If _subOrdinate <> String.Empty Then

                            '    End If
                            '    '--update data for SPV
                            AvgScoreSubOrdinate = AvgScoreSubOrdinate + itemSPV.RptAvgScoreSubOrdinate
                            SMAchievementTarget = SMAchievementTarget + itemSPV.RptAchievement
                        Next
                    Else
                        AvgScoreSubOrdinate = 0
                        SMAchievementTarget = 0
                    End If
                    item.RptAvgScoreSubOrdinate = AvgScoreSubOrdinate
                    item.RptAchievement = SMAchievementTarget
                Else
                    item.RptAvgScoreSubOrdinate = 0
                    item.RptAchievement = 0
                End If
                arrtoProcess.Add(item)
            Next
        End Sub

#End Region

#Region "Synchronize"
        Public Function SynchronizeSapRegister(ByVal NewArrSapregister As ArrayList) As Integer
            Dim nresultSynch As Integer = 0

            Try
                Dim objSAPPeriod As SAPPeriod = New SAPPeriod
                If NewArrSapregister.Count > 0 Then
                    Dim objSAPRegister As SAPRegister = New SAPRegister
                    objSAPRegister = CType(NewArrSapregister(0), SAPRegister)
                    objSAPPeriod = objSAPRegister.SAPPeriod
                End If

                Dim OldArrSAPRegister As ArrayList = New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPRegister), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SAPRegister), "SAPPeriod.ID", objSAPPeriod.ID))

                OldArrSAPRegister = m_SAPRegisterMapper.RetrieveByCriteria(criterias)

                If OldArrSAPRegister.Count > 0 Then
                    Delete(OldArrSAPRegister)
                End If


                For Each itemSAPReg As SAPRegister In NewArrSapregister
                    nresultSynch = m_SAPRegisterMapper.Insert(itemSAPReg, Me.m_userPrincipal.Identity.Name)
                Next

                Return nresultSynch

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Sub Delete(ByVal arrList As ArrayList)
            Try
                For Each item As SAPRegister In arrList
                    m_SAPRegisterMapper.Delete(item)
                Next
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region

    End Class

End Namespace

