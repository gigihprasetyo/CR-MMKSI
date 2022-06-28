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
'// Generated on 7/16/2007 - 2:31:06 PM
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
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace KTB.DNet.BusinessFacade.Salesman

    Public Class SalesmanDSEFacade

        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SalesmanDSEMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SalesmanDSEMapper = MapperFactory.GetInstance.GetMapper(GetType(SalesmanDSE).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SalesmanDSE
            Return CType(m_SalesmanDSEMapper.Retrieve(ID), SalesmanDSE)
        End Function

        Public Function Retrieve(ByVal Code As String) As SalesmanDSE
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDSE), "SalesmanDSECode", MatchType.Exact, Code))

            Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias)
            If (SalesmanDSEColl.Count > 0) Then
                Return CType(SalesmanDSEColl(0), SalesmanDSE)
            End If
            Return New SalesmanDSE
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SalesmanDSEMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SalesmanDSEMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SalesmanDSEMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanDSE), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanDSEMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanDSE), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SalesmanDSEMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SalesmanDSE As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias)
            Return _SalesmanDSE
        End Function

        

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SalesmanDSEColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SalesmanDSE), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SalesmanDSEColl


            'Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            'Return SalesmanDSEColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sortCollection As ICollection) As ArrayList

            ' modify code for sorting

            Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias, sortCollection)
            Return SalesmanDSEColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList

            ' modify code for sorting

            Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias)
            Return SalesmanDSEColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SalesmanDSE), columnName, matchOperator, columnValue))
            Return SalesmanDSEColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SalesmanDSE), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), columnName, matchOperator, columnValue))

            Return m_SalesmanDSEMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"





#End Region

#Region "Need To Add"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "AreaCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SalesmanDSE), "AreaCode", AggregateType.Count)
            Return CType(m_SalesmanDSEMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function Insert(ByVal objDomain As SalesmanDSE) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SalesmanDSEMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As SalesmanDSE) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SalesmanDSEMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SalesmanDSE)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_SalesmanDSEMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SalesmanDSE) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SalesmanDSEMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is SalesmanDSE) Then
                CType(InsertArg.DomainObject, SalesmanDSE).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SalesmanDSE).MarkLoaded()

            End If
        End Sub

#End Region

#Region "Custom Method"
        Public Function GetBySalesmanHeader(ByVal Code As String) As SalesmanDSE
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDSE), "SalesmanHeader.SalesmanCode", MatchType.Exact, Code))

            Dim SalesmanDSEColl As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias)
            If (SalesmanDSEColl.Count > 0) Then
                Return CType(SalesmanDSEColl(0), SalesmanDSE)
            End If
            Return New SalesmanDSE
        End Function

        Public Function NonAktif_Changes(ByVal objDSE As SalesmanDSE) As Integer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDSE.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, 1))

            Dim listDSE As List(Of SalesmanDSE) = m_SalesmanDSEMapper.RetrieveByCriteria(criterias).Cast(Of SalesmanDSE).ToList()

            'Get salesman DSE sama dengan atau lebih besar dari urutan
            For Each iDSE As SalesmanDSE In listDSE.Where(Function(x) x.Priority > objDSE.Priority)
                iDSE.Priority = iDSE.Priority - 1
                m_SalesmanDSEMapper.Update(iDSE, m_userPrincipal.Identity.Name)
            Next
            objDSE.Priority = 0
            objDSE.Status = 0
            Return m_SalesmanDSEMapper.Update(objDSE, m_userPrincipal.Identity.Name)
        End Function

        Public Function GetNewPriority(ByVal objDealer As Dealer) As Integer
            Dim result As Integer = 1
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanDSE), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Dealer.ID", MatchType.Exact, objDealer.ID))
            criterias.opAnd(New Criteria(GetType(SalesmanDSE), "Status", MatchType.Exact, 1))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(SalesmanDSE), "Priority", Sort.SortDirection.DESC))

            Dim arrDSE As ArrayList = m_SalesmanDSEMapper.RetrieveByCriteria(criterias, sortColl)
            If arrDSE.Count > 0 Then
                result = CType(arrDSE(0), SalesmanDSE).Priority + 1
            End If

            Return result
        End Function

        Public Function GetPhoneNumber(ByVal objSalesmanHeader As SalesmanHeader) As String
            Dim result As String = String.Empty
            Try
                Dim arrParam As ArrayList = New ArrayList()
                Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@SalesmanCode", objSalesmanHeader.SalesmanCode)
                arrParam.Add(param1)

                Dim dsResult As DataSet = m_SalesmanDSEMapper.RetrieveDataSet("sp_GetPhoneNumberSFD", arrParam)
                result = dsResult.Tables(0).Rows(0)(0).ToString()

                If String.IsNullOrEmpty(result) Then
                    result = objSalesmanHeader.SalesmanProfiles.Cast(Of SalesmanProfile).FirstOrDefault(Function(y) y.ProfileHeader.Code = "NO_HP" _
                            And y.ProfileGroup.Code = "sals_dbs_unit").ProfileValue()
                End If

            Catch
            End Try
            Return result
        End Function

#End Region

    End Class

End Namespace

