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
'// Generated on 8/3/2007 - 11:14:25 AM
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

    Public Class MaterialPromotionGIGRFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_MaterialPromotionGIGRMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_MaterialPromotionGIGRMapper = MapperFactory.GetInstance.GetMapper(GetType(MaterialPromotionGIGR).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As MaterialPromotionGIGR
            Return CType(m_MaterialPromotionGIGRMapper.Retrieve(ID), MaterialPromotionGIGR)
        End Function

        Public Function Retrieve(ByVal Code As String) As MaterialPromotionGIGR
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotionGIGRCode", MatchType.Exact, Code))

            Dim MaterialPromotionGIGRColl As ArrayList = m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias)
            If (MaterialPromotionGIGRColl.Count > 0) Then
                Return CType(MaterialPromotionGIGRColl(0), MaterialPromotionGIGR)
            End If
            Return New MaterialPromotionGIGR
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_MaterialPromotionGIGRMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionGIGR), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionGIGRMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionGIGR), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_MaterialPromotionGIGRMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MaterialPromotionGIGR As ArrayList = m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias)
            Return _MaterialPromotionGIGR
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionGIGRColl As ArrayList = m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return MaterialPromotionGIGRColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionGIGR), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim MaterialPromotionGIGRColl As ArrayList = m_MaterialPromotionGIGRMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return MaterialPromotionGIGRColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim MaterialPromotionGIGRColl As ArrayList = m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return MaterialPromotionGIGRColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim MaterialPromotionGIGRColl As ArrayList = m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(MaterialPromotionGIGR), columnName, matchOperator, columnValue))
            Return MaterialPromotionGIGRColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(MaterialPromotionGIGR), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), columnName, matchOperator, columnValue))

            Return m_MaterialPromotionGIGRMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MaterialPromotionGIGR), "MaterialPromotionGIGRCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(MaterialPromotionGIGR), "MaterialPromotionGIGRCode", AggregateType.Count)
            Return CType(m_MaterialPromotionGIGRMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function InsertTransaction(ByVal objGI As MaterialPromotionGIGR) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objGI, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objGI.ID
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

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionGIGR) As Integer
            Dim returnValue As Short = 1
            Try
                m_MaterialPromotionGIGRMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                returnValue = -1
            End Try

            Return returnValue
        End Function

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionGIGR) As Integer
            Dim nResult As Integer = 1
            If (Me.IsTaskFree()) Then

                Try
                    If objDomain.RowStatus = DBRowStatus.Active Then
                        objDomain.RowStatus = DBRowStatus.Deleted
                    End If
                    m_MaterialPromotionGIGRMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Catch ex As Exception
                    Dim s As String = ex.Message
                    nResult = -1
                End Try
            End If
            Return nResult
        End Function

        Public Function Update(ByVal objDomain As KTB.DNet.Domain.MaterialPromotionGIGR) As Integer
            Dim iReturn As Integer = 1
            Try
                m_MaterialPromotionGIGRMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is MaterialPromotionGIGR) Then
                CType(InsertArg.DomainObject, MaterialPromotionGIGR).ID = InsertArg.ID
                CType(InsertArg.DomainObject, MaterialPromotionGIGR).MarkLoaded()
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

