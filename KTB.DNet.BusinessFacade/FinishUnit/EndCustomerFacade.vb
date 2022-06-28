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
'// Author Name   : Agus Soepriadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 10/10/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class EndCustomerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_EndCustomerMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_EndCustomerMapper = MapperFactory.GetInstance().GetMapper(GetType(EndCustomer).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return CType(m_EndCustomerMapper.RetrieveScalar(aggr, crit), Integer)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As EndCustomer
            Return CType(m_EndCustomerMapper.Retrieve(ID), EndCustomer)
        End Function
        Public Function Retrieve(ByVal ID As String) As EndCustomer
            Return CType(m_EndCustomerMapper.Retrieve(Convert.ToInt32(ID)), EndCustomer)
        End Function
        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As DateTime) As EndCustomer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(EndCustomer), "ValidFrom", MatchType.Exact, sCode))

            Dim EndCustomerColl As ArrayList = m_EndCustomerMapper.RetrieveByCriteria(crit)
            If (EndCustomerColl.Count > 0) Then
                Return CType(EndCustomerColl(0), EndCustomer)
            End If
            Return New EndCustomer
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_EndCustomerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_EndCustomerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_EndCustomerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EndCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EndCustomerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EndCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_EndCustomerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _EndCustomer As ArrayList = m_EndCustomerMapper.RetrieveByCriteria(criterias)
            Return _EndCustomer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim EndCustomerColl As ArrayList = m_EndCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EndCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim EndCustomerColl As ArrayList = m_EndCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EndCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EndCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim EndCustomerColl As ArrayList = m_EndCustomerMapper.RetrieveByCriteria(Criterias, sortColl)
            Return EndCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EndCustomer), columnName, matchOperator, columnValue))
            Dim EndCustomerColl As ArrayList = m_EndCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return EndCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(EndCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), columnName, matchOperator, columnValue))

            Return m_EndCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As EndCustomer) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_EndCustomerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As EndCustomer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_EndCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
            End Try
            Return nResult
        End Function
        Public Function UpdateTransaction(ByVal oCustomer As Customer, ByVal arrList As ArrayList, ByVal delList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As ChassisMaster In arrList
                        Dim EndCust As EndCustomer = item.EndCustomer
                        EndCust.RefChassisNumberID = item.EndCustomer.RefChassisNumberID
                        EndCust.Customer = oCustomer
                        EndCust.FakturDate = item.EndCustomer.FakturDate
                        EndCust.ProjectIndicator = String.Empty
                        EndCust.FakturNumber = String.Empty
                        'Deleted Field
                        EndCust.Name1 = EndCust.Customer.Name1
                        EndCust.SaveTime = Now
                        EndCust.SaveBy = m_userPrincipal.Identity.Name
                        If EndCust.ID > 0 Then
                            m_TransactionManager.AddUpdate(EndCust, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(EndCust, m_userPrincipal.Identity.Name)
                        End If
                        item.EndCustomer = EndCust
                        item.FakturStatus = EnumChassisMaster.FakturStatus.Baru
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        If Not IsNothing(oCustomer) AndAlso Not IsNothing(oCustomer.MyCustomerRequest) AndAlso oCustomer.MyCustomerRequest.ID > 0 Then
                            m_TransactionManager.AddUpdate(oCustomer.MyCustomerRequest, m_userPrincipal.Identity.Name) 'for MCPStatus
                        End If
                    Next
                    If delList.Count > 0 Then
                        For Each items As ChassisMaster In delList
                            Dim EndCust As EndCustomer = items.EndCustomer
                            EndCust.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(EndCust, m_userPrincipal.Identity.Name)
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

        Public Function UpdateTransaction(ByVal objDomain As EndCustomer) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddUpdate(objDomain.Customer, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
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

        Public Sub Delete(ByVal objDomain As EndCustomer)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_EndCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As EndCustomer)
            Try
                m_EndCustomerMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function InsertTransactionPengajuanDummyFaktur(ByVal oCustomer As Customer, ByVal dtFakturDate As DateTime, ByVal arrList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As v_RetrieveDummyFaktur In arrList
                        'Dim EndCust As EndCustomer = item.ChassisMaster.EndCustomer
                        'If IsNothing(EndCust) Then EndCust = New EndCustomer

                        Dim EndCust As EndCustomer =  New EndCustomer
                        EndCust.Customer = oCustomer
                        EndCust.FakturDate = dtFakturDate
                        EndCust.ProjectIndicator = String.Empty
                        EndCust.FakturNumber = String.Empty
                        EndCust.CleansingCustomerID = oCustomer.ID
                        EndCust.SaveTime = Now
                        EndCust.SaveBy = m_userPrincipal.Identity.Name
                        EndCust.Name1 = oCustomer.Name1
                        EndCust.IsTemporary = 1
                        If Not IsNothing(EndCust) AndAlso EndCust.ID > 0 Then
                            m_TransactionManager.AddUpdate(EndCust, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(EndCust, m_userPrincipal.Identity.Name)
                        End If

                        Dim oChassisMaster As ChassisMaster = item.ChassisMaster
                        oChassisMaster.EndCustomer = EndCust
                        oChassisMaster.FakturStatus = EnumChassisMaster.FakturStatus.Baru

                        Dim VehicleKindGroupID As String = String.Empty
                        If item.ChassisMaster.VechileColor.VechileType.IsVehicleKind1 = 1 Then
                            VehicleKindGroupID += "2,"
                        End If
                        If item.ChassisMaster.VechileColor.VechileType.IsVehicleKind2 = 1 Then
                            VehicleKindGroupID += "3,"
                        End If
                        If item.ChassisMaster.VechileColor.VechileType.IsVehicleKind3 = 1 Then
                            VehicleKindGroupID += "4,"
                        End If
                        If item.ChassisMaster.VechileColor.VechileType.IsVehicleKind4 = 1 Then
                            VehicleKindGroupID += "5,"
                        End If

                        If VehicleKindGroupID.Length > 0 Then
                            VehicleKindGroupID = Left(VehicleKindGroupID, VehicleKindGroupID.Length - 1)
                        End If
                        Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crt.opAnd(New Criteria(GetType(VehicleKind), "VehicleKindGroup.ID", MatchType.InSet, "(" & VehicleKindGroupID & ")"))

                        Dim arlVehicleKind As ArrayList = New VehicleKindFacade(m_userPrincipal).Retrieve(crt)
                        If arlVehicleKind.Count > 0 Then
                            Dim objVehicleKind As VehicleKind = CType(arlVehicleKind(0), VehicleKind)
                            oChassisMaster.VehicleKind = objVehicleKind
                        End If

                        m_TransactionManager.AddUpdate(oChassisMaster, m_userPrincipal.Identity.Name)
                    Next
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

        Public Function InsertTransactionPengajuanFaktur(ByVal oCustomer As Customer, ByVal arrList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As ChassisMaster In arrList
                        Dim EndCust As EndCustomer = item.EndCustomer
                        EndCust.RefChassisNumberID = item.EndCustomer.RefChassisNumberID
                        EndCust.Customer = oCustomer
                        EndCust.FakturDate = item.EndCustomer.FakturDate
                        EndCust.ProjectIndicator = String.Empty
                        EndCust.FakturNumber = String.Empty
                        EndCust.CleansingCustomerID = oCustomer.ID
                        EndCust.SaveTime = Now
                        EndCust.SaveBy = m_userPrincipal.Identity.Name
                        EndCust.Name1 = oCustomer.Name1
                        m_TransactionManager.AddInsert(EndCust, m_userPrincipal.Identity.Name)
                        item.EndCustomer = EndCust
                        item.FakturStatus = EnumChassisMaster.FakturStatus.Baru
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        If Not IsNothing(oCustomer) AndAlso Not IsNothing(oCustomer.MyCustomerRequest) AndAlso oCustomer.MyCustomerRequest.ID > 0 Then
                            m_TransactionManager.AddUpdate(oCustomer.MyCustomerRequest, m_userPrincipal.Identity.Name) 'for MCPStatus
                        End If
                    Next
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

        Public Function InsertTransactionPengajuanFaktur(ByVal oCustomer As Customer, ByVal arrList As ArrayList, ByVal oSPKHeader As SPKHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As ChassisMaster In arrList
                        Dim EndCust As EndCustomer = item.EndCustomer
                        EndCust.RefChassisNumberID = item.EndCustomer.RefChassisNumberID
                        EndCust.Customer = oCustomer
                        EndCust.FakturDate = item.EndCustomer.FakturDate
                        EndCust.ProjectIndicator = String.Empty
                        EndCust.FakturNumber = String.Empty
                        'Deleted Field
                        EndCust.CleansingCustomerID = oCustomer.ID
                        EndCust.SaveTime = Now
                        EndCust.SaveBy = m_userPrincipal.Identity.Name
                        EndCust.Name1 = oCustomer.Name1
                        m_TransactionManager.AddInsert(EndCust, m_userPrincipal.Identity.Name)
                        item.EndCustomer = EndCust
                        item.FakturStatus = EnumChassisMaster.FakturStatus.Baru
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)

                        Dim objSPKFaktur As SPKFaktur = New SPKFaktur
                        objSPKFaktur.EndCustomer = EndCust
                        objSPKFaktur.SPKHeader = oSPKHeader
                        m_TransactionManager.AddInsert(objSPKFaktur, m_userPrincipal.Identity.Name)

                        If Not IsNothing(oCustomer) AndAlso Not IsNothing(oCustomer.MyCustomerRequest) AndAlso oCustomer.MyCustomerRequest.ID > 0 Then
                            m_TransactionManager.AddUpdate(oCustomer.MyCustomerRequest, m_userPrincipal.Identity.Name) 'for MCPStatus
                        End If
                    Next
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

        Public Function ValidateCode(ByVal Code As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "ChassisID", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(EndCustomer), "ChassisID", AggregateType.Count)

            Return CType(m_EndCustomerMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function GetCountMCP(ByVal MCPHeaderID As Integer) As Integer
            'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "MCPHeader.ID", MatchType.Exact, MCPHeaderID))
            Dim crit As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EndCustomer), "MCPHeader.ID", MatchType.Exact, MCPHeaderID))

            Dim agg As Aggregate = New Aggregate(GetType(EndCustomer), "ID", AggregateType.Count)

            Return CType(m_EndCustomerMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function GetCountLKPP(ByVal LKPPHeaderID As Integer) As Integer
            'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(EndCustomer), "MCPHeader.ID", MatchType.Exact, MCPHeaderID))
            Dim crit As New CriteriaComposite(New Criteria(GetType(EndCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(EndCustomer), "LKPPHeader.ID", MatchType.Exact, LKPPHeaderID))

            Dim agg As Aggregate = New Aggregate(GetType(EndCustomer), "ID", AggregateType.Count)

            Return CType(m_EndCustomerMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InsertTransactionPengajuanRevisiFaktur(ByVal oCustomer As Customer, ByVal arrList As ArrayList, ByVal oSPKHeader As SPKHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As ChassisMaster In arrList
                        Dim EndCust As EndCustomer = item.EndCustomer
                        EndCust.ID = 0
                        EndCust.RefChassisNumberID = item.EndCustomer.RefChassisNumberID
                        EndCust.Customer = oCustomer
                        EndCust.FakturDate = item.EndCustomer.FakturDate
                        EndCust.ProjectIndicator = String.Empty
                        EndCust.FakturNumber = String.Empty
                        EndCust.CleansingCustomerID = oCustomer.ID
                        EndCust.SaveTime = Now
                        EndCust.SaveBy = m_userPrincipal.Identity.Name
                        EndCust.Name1 = oCustomer.Name1

                        m_TransactionManager.AddInsert(EndCust, m_userPrincipal.Identity.Name)

                        Dim objRevisionFaktur As RevisionFaktur = New RevisionFaktur
                        objRevisionFaktur.ChassisMaster = item
                        objRevisionFaktur.EndCustomer = EndCust
                        objRevisionFaktur.OldEndCustomer = Retrieve(item.EndCustomerID)
                        objRevisionFaktur.RevisionTypeID = 2
                        objRevisionFaktur.RevisionStatus = 0
                        objRevisionFaktur.IsPay = -1
                        objRevisionFaktur.RowStatus = 0
                        objRevisionFaktur.CreatedTime = Now
                        objRevisionFaktur.CreatedBy = m_userPrincipal.Identity.Name
                        objRevisionFaktur.LastUpdateBy = m_userPrincipal.Identity.Name
                        m_TransactionManager.AddInsert(objRevisionFaktur, m_userPrincipal.Identity.Name)

                        Dim objRevisionSPKFaktur As RevisionSPKFaktur = New RevisionSPKFaktur
                        objRevisionSPKFaktur.EndCustomer = EndCust
                        objRevisionSPKFaktur.SPKHeader = oSPKHeader
                        objRevisionSPKFaktur.RowStatus = 0
                        objRevisionSPKFaktur.CreatedTime = Now
                        objRevisionSPKFaktur.CreatedBy = m_userPrincipal.Identity.Name
                        objRevisionSPKFaktur.LastUpdateBy = m_userPrincipal.Identity.Name
                        m_TransactionManager.AddInsert(objRevisionSPKFaktur, m_userPrincipal.Identity.Name)

                        If Not IsNothing(oCustomer) AndAlso Not IsNothing(oCustomer.MyCustomerRequest) AndAlso oCustomer.MyCustomerRequest.ID > 0 Then
                            m_TransactionManager.AddUpdate(oCustomer.MyCustomerRequest, m_userPrincipal.Identity.Name) 'for MCPStatus
                        End If
                    Next
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

        Public Function InsertTransactionPengajuanRevisiFaktur(ByVal oCustomer As Customer, ByVal arrList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As ChassisMaster In arrList
                        Dim EndCust As EndCustomer = item.EndCustomer
                        EndCust.ID = 0
                        EndCust.RefChassisNumberID = item.EndCustomer.RefChassisNumberID
                        EndCust.Customer = oCustomer
                        EndCust.FakturDate = item.EndCustomer.FakturDate
                        EndCust.ProjectIndicator = String.Empty
                        EndCust.FakturNumber = String.Empty
                        EndCust.CleansingCustomerID = oCustomer.ID
                        EndCust.SaveTime = Now
                        EndCust.SaveBy = m_userPrincipal.Identity.Name
                        EndCust.Name1 = oCustomer.Name1
                        m_TransactionManager.AddInsert(EndCust, m_userPrincipal.Identity.Name)

                        Dim objRevisionFaktur As RevisionFaktur = New RevisionFaktur
                        objRevisionFaktur.ChassisMaster = item
                        objRevisionFaktur.EndCustomer = EndCust
                        objRevisionFaktur.OldEndCustomer = Retrieve(item.EndCustomerID)
                        objRevisionFaktur.RevisionTypeID = 2
                        objRevisionFaktur.RevisionStatus = 0
                        objRevisionFaktur.IsPay = -1
                        objRevisionFaktur.RowStatus = 0
                        objRevisionFaktur.CreatedTime = Now
                        objRevisionFaktur.CreatedBy = m_userPrincipal.Identity.Name
                        objRevisionFaktur.LastUpdateBy = m_userPrincipal.Identity.Name
                        m_TransactionManager.AddInsert(objRevisionFaktur, m_userPrincipal.Identity.Name)

                        If Not IsNothing(oCustomer) AndAlso Not IsNothing(oCustomer.MyCustomerRequest) AndAlso oCustomer.MyCustomerRequest.ID > 0 Then
                            m_TransactionManager.AddUpdate(oCustomer.MyCustomerRequest, m_userPrincipal.Identity.Name) 'for MCPStatus
                        End If
                    Next
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

        Public Function UpdateTransactionPengajuanRevisiFaktur(ByVal oCustomer As Customer, ByVal arrList As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    For Each item As ChassisMaster In arrList
                        Dim EndCust As EndCustomer = item.EndCustomer
                        EndCust.Customer = oCustomer
                        'Deleted Field
                        EndCust.Name1 = EndCust.Customer.Name1
                        EndCust.SaveTime = Now
                        EndCust.SaveBy = m_userPrincipal.Identity.Name
                        If EndCust.ID > 0 Then
                            m_TransactionManager.AddUpdate(EndCust, m_userPrincipal.Identity.Name)
                        End If

                        If Not IsNothing(oCustomer) AndAlso Not IsNothing(oCustomer.MyCustomerRequest) AndAlso oCustomer.MyCustomerRequest.ID > 0 Then
                            m_TransactionManager.AddUpdate(oCustomer.MyCustomerRequest, m_userPrincipal.Identity.Name) 'for MCPStatus
                        End If
                    Next

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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is EndCustomer) Then
                CType(InsertArg.DomainObject, EndCustomer).ID = InsertArg.ID
                CType(InsertArg.DomainObject, EndCustomer).MarkLoaded()
            End If
        End Sub
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

