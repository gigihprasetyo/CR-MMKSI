
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 20/02/2019 - 16:34:45
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.MDP

    Public Class MDPMasterVehicleFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MDPMasterVehicleMapper As IMapper
        Private m_MDPMasterVehicleHistoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MDPMasterVehicleMapper = MapperFactory.GetInstance.GetMapper(GetType(MDPMasterVehicle).ToString)
            Me.m_MDPMasterVehicleHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(MDPMasterVehicleHistory).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.MDPMasterVehicle))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.MDPMasterVehicleHistory))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MDPMasterVehicle
            Return CType(m_MDPMasterVehicleMapper.Retrieve(ID), MDPMasterVehicle)
        End Function

        Public Function Retrieve(ByVal Code As String) As MDPMasterVehicle
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MDPMasterVehicle), "MDPMasterVehicleCode", MatchType.Exact, Code))

            Dim MDPMasterVehicleColl As ArrayList = m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias)
            If (MDPMasterVehicleColl.Count > 0) Then
                Return CType(MDPMasterVehicleColl(0), MDPMasterVehicle)
            End If
            Return New MDPMasterVehicle
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MDPMasterVehicleMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPMasterVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPMasterVehicleMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPMasterVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MDPMasterVehicleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MDPMasterVehicle As ArrayList = m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias)
            Return _MDPMasterVehicle
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPMasterVehicleColl As ArrayList = m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MDPMasterVehicleColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(SortColumn)) And (Not IsNothing(SortColumn)) Then
                sortColl.Add(New Search.Sort(GetType(MDPMasterVehicle), SortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim MDPMasterVehicleColl As ArrayList = m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MDPMasterVehicleColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MDPMasterVehicleColl As ArrayList = m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MDPMasterVehicleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MDPMasterVehicleColl As ArrayList = m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MDPMasterVehicle), columnName, matchOperator, columnValue))
            Return MDPMasterVehicleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MDPMasterVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), columnName, matchOperator, columnValue))

            Return m_MDPMasterVehicleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), "MDPMasterVehicleCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MDPMasterVehicle), "MDPMasterVehicleCode", AggregateType.Count)
            Return CType(m_MDPMasterVehicleMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.MDPMasterVehicle) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.MDPMasterVehicle).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.MDPMasterVehicle).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is MDPMasterVehicleHistory) Then
                CType(InsertArg.DomainObject, MDPMasterVehicleHistory).ID = InsertArg.ID
            End If
        End Sub

        Public Function Insert(ByVal objDomain As MDPMasterVehicle) As Integer
            Dim iReturn As Integer = -2
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As MDPMasterVehicleHistory In objDomain.MDPMasterVehicleHistory
                        item.MDPMasterVehicle = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        iReturn = objDomain.ID
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
        End Function

        Public Function Update(ByVal objDomain As MDPMasterVehicle) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If objDomain.MDPMasterVehicleHistory.Count > 0 Then
                        For Each objMDPMasterVehicleHistory As MDPMasterVehicleHistory In objDomain.MDPMasterVehicleHistory
                            objMDPMasterVehicleHistory.MDPMasterVehicle = objDomain
                            If m_userPrincipal.Identity.Name = "" Then
                                _user = "SAP"
                            Else
                                _user = m_userPrincipal.Identity.Name
                            End If
                            m_TransactionManager.AddUpdate(objMDPMasterVehicleHistory, _user)
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
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

        Public Sub Delete(ByVal objDomain As MDPMasterVehicle)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_MDPMasterVehicleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As MDPMasterVehicle)
            Try
                For Each oMMVAs As MDPMasterVehicleHistory In objDomain.MDPMasterVehicleHistory
                    m_MDPMasterVehicleHistoryMapper.Delete(oMMVAs)
                Next
                m_MDPMasterVehicleMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function InsertFromWebSevice(ByVal MdpMV As MDPMasterVehicle) As Short
            Dim returnValue As Integer = -1
            If Me.IsTaskFree() Then
                Try
                    'Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    Dim User As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
                    Dim objMdpMVHistory As MDPMasterVehicleHistory

                    Dim MdpMVcriterias As New CriteriaComposite(New Criteria(GetType(MDPMasterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    MdpMVcriterias.opAnd(New Criteria(GetType(MDPMasterVehicle), "VehicleColor.ID", MatchType.Exact, MdpMV.VehicleColor.ID))

                    Dim ExistingData As ArrayList = New MDPMasterVehicleFacade(User).Retrieve(MdpMVcriterias)

                    objMdpMVHistory = New MDPMasterVehicleHistory
                    objMdpMVHistory.StatusTo = MdpMV.Status
                    If ExistingData.Count > 0 Then
                        Dim objExistingData As MDPMasterVehicle = CType(ExistingData(0), MDPMasterVehicle)
                        objExistingData.Status = MdpMV.Status
                        objMdpMVHistory.MDPMasterVehicle = objExistingData
                        objMdpMVHistory.StatusFrom = objExistingData.Status
                        m_TransactionManager.AddInsert(objMdpMVHistory, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddUpdate(objExistingData, m_userPrincipal.Identity.Name)
                    Else
                        objMdpMVHistory.MDPMasterVehicle = MdpMV
                        m_TransactionManager.AddInsert(MdpMV, m_userPrincipal.Identity.Name)
                        m_TransactionManager.AddInsert(objMdpMVHistory, m_userPrincipal.Identity.Name)

                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
                    End If
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


#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

