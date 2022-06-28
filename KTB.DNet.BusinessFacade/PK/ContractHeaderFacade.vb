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
'// Generated on 10/5/2005 - 3:23:28 PM
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
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.PK

    Public Class ContractHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ContractHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ContractHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.ContractHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ContractHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.ContractDetail))


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ContractHeader
            Return CType(m_ContractHeaderMapper.Retrieve(ID), ContractHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As ContractHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ContractHeader), "ContractNumber", MatchType.Exact, Code))

            Dim ContractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias)
            If (ContractHeaderColl.Count > 0) Then
                Return CType(ContractHeaderColl(0), ContractHeader)
            End If
            Return New ContractHeader
        End Function

        Public Function RetrieveByPKNumber(ByVal PKNumber As String) As ContractHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), _
                "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ContractHeader), "PKNumber", MatchType.Exact, PKNumber))

            Dim ContractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias)
            If (ContractHeaderColl.Count > 0) Then
                Return CType(ContractHeaderColl(0), ContractHeader)
            End If
            Return New ContractHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ContractHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ContractHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ContractHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ContractHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ContractHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ContractHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ContractHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ContractHeader As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias)
            Return _ContractHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ContractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ContractHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ContractHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim contractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return contractHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ContractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ContractHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ContractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ContractHeader), columnName, matchOperator, columnValue))
            Return ContractHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.DNet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(ContractHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), columnName, matchOperator, columnValue))

            Return m_ContractHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Update(ByVal objDomain As ContractHeader)
            Try
                m_ContractHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "ContractHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ContractHeader), "ContractHeaderCode", AggregateType.Count)
            Return CType(m_ContractHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.ContractHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As ContractDetail In objDomain.ContractDetails
                        item.ContractHeader = objDomain
                        m_TransactionManager.AddDelete(item)
                    Next
                    m_TransactionManager.AddDelete(objDomain)

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


        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.ContractHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As ContractDetail In objDomain.ContractDetails
                        item.ContractHeader = objDomain
                        m_TransactionManager.AddUpdate(item, "SAP")
                    Next
                    m_TransactionManager.AddUpdate(objDomain, "SAP")

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

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.ContractHeader, ByVal arrPkDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As ContractDetail In arrPkDetails
                        item.ContractHeader = objDomain
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                    Next
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


        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.ContractHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As ContractDetail In objDomain.ContractDetails
                        item.ContractHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next
                    If objDomain.RefContractNumber <> "" Then
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(ContractHeader), "ContractNumber", MatchType.Exact, objDomain.RefContractNumber))

                        Dim ContractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias)
                        If (ContractHeaderColl.Count > 0) Then
                            Dim oCH As ContractHeader = ContractHeaderColl(0)
                            oCH.IsCarriedOver = 1
                            m_TransactionManager.AddUpdate(oCH, m_userPrincipal.Identity.Name)
                        End If
                    End If

                    Dim pkNumber = objDomain.PKNumber
                    Dim pkHeader As pkHeader = RetrievePK(pkNumber, m_userPrincipal)
                    If pkHeader.ID > 0 Then
                        pkHeader.PKStatus = enumStatusPK.Status.Selesai
                        m_TransactionManager.AddUpdate(pkHeader, m_userPrincipal.Identity.Name)
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

        Private Function RetrievePK(ByVal pkNumber As String, ByVal principle As IPrincipal) As PKHeader
            Dim pkFacade As PKHeaderFacade = New PKHeaderFacade(principle)
            Return pkFacade.Retrieve(pkNumber)
        End Function


        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.ContractHeader) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.ContractHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.ContractHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is ContractDetail) Then

                CType(InsertArg.DomainObject, ContractDetail).ID = InsertArg.ID

            End If

        End Sub

        Public Function RetrieveIDDealer(ByVal id As Integer) As ContractHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ContractHeader), "Dealer.ID", MatchType.Exact, id))

            Dim ContractHeaderColl As ArrayList = m_ContractHeaderMapper.RetrieveByCriteria(criterias)
            If (ContractHeaderColl.Count > 0) Then
                Return CType(ContractHeaderColl(0), ContractHeader)
            End If
            Return New ContractHeader
        End Function
#End Region

    End Class

End Namespace