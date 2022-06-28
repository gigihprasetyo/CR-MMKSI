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

    Public Class MaterialPromotionRequestDetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MaterialPromotionRequestDetailMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MaterialPromotionRequestDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(MaterialPromotionRequestDetail).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MaterialPromotionRequestDetail
            Return CType(m_MaterialPromotionRequestDetailMapper.Retrieve(ID), MaterialPromotionRequestDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As MaterialPromotionRequestDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequestDetailCode", MatchType.Exact, Code))

            Dim MaterialPromotionRequestDetailColl As ArrayList = m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias)
            If (MaterialPromotionRequestDetailColl.Count > 0) Then
                Return CType(MaterialPromotionRequestDetailColl(0), MaterialPromotionRequestDetail)
            End If
            Return New MaterialPromotionRequestDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MaterialPromotionRequestDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionRequestDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionRequestDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionRequestDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionRequestDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MaterialPromotionRequestDetail As ArrayList = m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias)
            Return _MaterialPromotionRequestDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionRequestDetailColl As ArrayList = m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaterialPromotionRequestDetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MaterialPromotionRequestDetailColl As ArrayList = m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MaterialPromotionRequestDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionRequestDetailColl As ArrayList = m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MaterialPromotionRequestDetail), columnName, matchOperator, columnValue))
            Return MaterialPromotionRequestDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionRequestDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), columnName, matchOperator, columnValue))

            Return m_MaterialPromotionRequestDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequestDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MaterialPromotionRequestDetail), "MaterialPromotionRequestDetailCode", AggregateType.Count)
            Return CType(m_MaterialPromotionRequestDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As MaterialPromotionRequestDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                m_MaterialPromotionRequestDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
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
        Public Function Update(ByVal objDomain As MaterialPromotionRequestDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_MaterialPromotionRequestDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As MaterialPromotionRequestDetail) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_MaterialPromotionRequestDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

