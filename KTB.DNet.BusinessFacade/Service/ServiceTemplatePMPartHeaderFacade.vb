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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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
Imports System.Data.SqlClient

#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class ServiceTemplatePMPartHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ServiceTemplatePMPartHeaderMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager



#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ServiceTemplatePMPartHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(ServiceTemplatePMPartHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(ServiceTemplatePMPartHeader))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ServiceTemplatePMPartHeader
            Return CType(m_ServiceTemplatePMPartHeaderMapper.Retrieve(ID), ServiceTemplatePMPartHeader)
        End Function

        Public Function Retrieve(ByVal ID As String) As ServiceTemplatePMPartHeader
            Return CType(m_ServiceTemplatePMPartHeaderMapper.Retrieve(Convert.ToInt32(ID)), ServiceTemplatePMPartHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplatePMPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplatePMPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ServiceTemplatePMPartHeader As ArrayList = m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias)
            Return _ServiceTemplatePMPartHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ServiceTemplatePMPartHeaderColl As ArrayList = m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceTemplatePMPartHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplatePMPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ServiceTemplatePMPartHeaderColl As ArrayList = m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceTemplatePMPartHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList


            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplatePMPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ServiceTemplatePMPartHeaderColl As ArrayList = m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceTemplatePMPartHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(ServiceTemplatePMPartHeader), sortColumn, sortDirection))
            Else
                'sortColl = Nothing
                sortColl.Add(New Sort(GetType(ServiceTemplatePMPartHeader), "ID", Sort.SortDirection.DESC))

            End If

            Dim ServiceTemplatePMPartHeaderColl As ArrayList = m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ServiceTemplatePMPartHeaderColl
        End Function


        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ServiceTemplatePMPartHeader), columnName, matchOperator, columnValue))
            Dim ServiceTemplatePMPartHeaderColl As ArrayList = m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ServiceTemplatePMPartHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ServiceTemplatePMPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMPartHeader), columnName, matchOperator, columnValue))

            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Return m_ServiceTemplatePMPartHeaderMapper.RetrieveScalar(aggr, crit)
        End Function
#End Region

#Region "Transaction/Other Public Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is ServiceTemplatePMPartHeader) Then
                CType(InsertArg.DomainObject, ServiceTemplatePMPartHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ServiceTemplatePMPartHeader).MarkLoaded()
            End If
        End Sub

        Public Function Insert(ByVal objDomain As ServiceTemplatePMPartHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ServiceTemplatePMPartHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ServiceTemplatePMPartHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ServiceTemplatePMPartHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


        Public Sub Delete(ByVal objDomain As ServiceTemplatePMPartHeader)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ServiceTemplatePMPartHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ServiceTemplatePMPartHeader)
            Try
                m_ServiceTemplatePMPartHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal sID As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMPartHeader), "ID", MatchType.Exact, sID))
            Dim agg As Aggregate = New Aggregate(GetType(ServiceTemplatePMPartHeader), "ID", AggregateType.Count)

            Return CType(m_ServiceTemplatePMPartHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function
#End Region

#Region "Custom Method"
        Public Function Insert(ByVal objDomain As ServiceTemplatePMPartHeader, ByVal objDetails As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                If objDomain.ID = 0 Then
                    m_TransactionManager.AddInsert(objDomain, _user)
                    For Each objDet As ServiceTemplatePMPartDetail In objDetails
                        objDet.ServiceTemplatePMPartHeader = objDomain
                        m_TransactionManager.AddInsert(objDet, _user)
                    Next
                Else
                    For Each objDet As ServiceTemplatePMPartDetail In objDetails
                        objDet.ServiceTemplatePMPartHeader = objDomain
                        If objDet.ID = 0 Then
                            m_TransactionManager.AddInsert(objDet, _user)
                        Else
                            m_TransactionManager.AddUpdate(objDet, _user)
                        End If

                    Next

                    For Each item As ServiceTemplatePMPartDetail In objDomain.ServiceTemplatePMPartDetails
                        Dim isExist As Boolean = False
                        For Each itemExist As ServiceTemplatePMPartDetail In objDetails
                            If item.ID = itemExist.ID Then
                                isExist = True
                                Exit For
                            End If
                        Next

                        If Not isExist Then
                            item.RowStatus = CShort(DBRowStatus.Deleted)
                            m_TransactionManager.AddUpdate(item, _user)
                        End If
                    Next

                    m_TransactionManager.AddUpdate(objDomain, _user)
                End If

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function
#End Region

    End Class

End Namespace

