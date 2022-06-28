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
'// Generated on 8/3/2007 - 5:02:18 PM
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

Imports ktb.DNet.Domain
Imports ktb.DNet.Domain.Search
Imports ktb.DNet.DataMapper.Framework
Imports ktb.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.MaterialPromotion

    Public Class MaterialPromotionRequestFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MaterialPromotionRequestMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MaterialPromotionRequestMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNET.Domain.MaterialPromotionRequest).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNET.Domain.MaterialPromotionRequest))
            Me.m_TransactionManager = New TransactionManager
            AddHandler Me.m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MaterialPromotionRequest
            Return CType(m_MaterialPromotionRequestMapper.Retrieve(ID), MaterialPromotionRequest)
        End Function

        Public Function Retrieve(ByVal Code As String) As MaterialPromotionRequest
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), "RequestNo", MatchType.Exact, Code))

            Dim MaterialPromotionRequestColl As ArrayList = m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias)
            If (MaterialPromotionRequestColl.Count > 0) Then
                Return CType(MaterialPromotionRequestColl(0), MaterialPromotionRequest)
            End If
            Return New MaterialPromotionRequest
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MaterialPromotionRequestMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionRequestMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionRequestMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MaterialPromotionRequest As ArrayList = m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias)
            Return _MaterialPromotionRequest
        End Function
        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(MaterialPromotionRequest), SortColumn, sortDirection))

            Dim MaterialPromotionRequestColl As ArrayList = m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return MaterialPromotionRequestColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionRequestColl As ArrayList = m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaterialPromotionRequestColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MaterialPromotionRequestColl As ArrayList = m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MaterialPromotionRequestColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
    ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(MaterialPromotionRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim MaterialPromotionRequestColl As ArrayList = m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MaterialPromotionRequestColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionRequestColl As ArrayList = m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequest), columnName, matchOperator, columnValue))
            Return MaterialPromotionRequestColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionRequest), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), columnName, matchOperator, columnValue))

            Return m_MaterialPromotionRequestMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequest), "MaterialPromotionRequestCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MaterialPromotionRequest), "MaterialPromotionRequestCode", AggregateType.Count)
            Return CType(m_MaterialPromotionRequestMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As MaterialPromotionRequest, ByVal objDomainDetail As ArrayList)
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim objMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If Not objDomainDetail Is Nothing Then
                        If objDomainDetail.Count > 0 Then
                            For Each item As MaterialPromotionRequestDetail In objDomainDetail
                                item.MaterialPromotionRequest = objDomain
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    Else
                        objDomainDetail = New ArrayList
                    End If
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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

            Return returnVal
        End Function
        Public Function Update(ByVal objDomain As MaterialPromotionRequest) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MaterialPromotionRequestMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNET.Domain.MaterialPromotionRequest) Then

                CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotionRequest).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNET.Domain.MaterialPromotionRequest).MarkLoaded()
            Else
                If (TypeOf InsertArg.DomainObject Is MaterialPromotionRequestDetail) Then

                    CType(InsertArg.DomainObject, MaterialPromotionRequestDetail).ID = InsertArg.ID
                    CType(InsertArg.DomainObject, MaterialPromotionRequestDetail).MarkLoaded()
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

