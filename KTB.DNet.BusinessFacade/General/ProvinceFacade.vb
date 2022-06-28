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

Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Data.SqlClient

#End Region

Namespace KTB.DNet.BusinessFacade.General
    Public Class ProvinceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_ProvinceMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_ProvinceMapper = MapperFactory.GetInstance().GetMapper(GetType(Province).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
        End Sub

#End Region

#Region "Retrieve"



        Public Function Retrieve(ByVal ID As Integer) As Province
            Return CType(m_ProvinceMapper.Retrieve(ID), Province)
        End Function

        Public Function Retrieve(ByVal Code As String) As Province
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Province), "ProvinceCode", MatchType.Exact, Code))

            Dim ProvinceColl As ArrayList = m_ProvinceMapper.RetrieveByCriteria(criterias)
            If (ProvinceColl.Count > 0) Then
                Return CType(ProvinceColl(0), Province)
            End If
            Return New Province
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ProvinceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ProvinceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ProvinceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Province), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProvinceMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Province), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProvinceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Province As ArrayList = m_ProvinceMapper.RetrieveByCriteria(criterias)
            Return _Province
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ProvinceColl As ArrayList = m_ProvinceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProvinceColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Province), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ProvinceColl As ArrayList = m_ProvinceMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ProvinceColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Province), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim ProvinceColl As ArrayList = m_ProvinceMapper.RetrieveByCriteria(Criterias, sortColl)
            Return ProvinceColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Province), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ProvinceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ProvinceColl As ArrayList = m_ProvinceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProvinceColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ProvinceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Province), columnName, matchOperator, columnValue))
            Dim ProvinceColl As ArrayList = m_ProvinceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ProvinceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(Province), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), columnName, matchOperator, columnValue))

            Return m_ProvinceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)

            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(Province)) Then
                CType(args.DomainObject, Province).ID = args.ID
                CType(args.DomainObject, Province).MarkLoaded()
            ElseIf (args.DomainObject.GetType = GetType(City)) Then
                CType(args.DomainObject, City).ID = args.ID
                CType(args.DomainObject, City).MarkLoaded()
            End If

        End Sub

        Public Function InsertWithTransactionManager(ByVal province As Province, ByVal listOfCity As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert Province
                    Me.m_TransactionManager.AddInsert(province, m_userPrincipal.Identity.Name)
                    ' add command to insert vehicle Color
                    For Each city As City In listOfCity
                        If city.ID < 0 Then
                            Me.m_TransactionManager.AddInsert(city, m_userPrincipal.Identity.Name)
                        Else
                            Me.m_TransactionManager.AddUpdate(city, m_userPrincipal.Identity.Name)
                        End If


                    Next
                    Me.m_TransactionManager.PerformTransaction()
                    result = province.ID

                    Return result
                Catch sqlException As SqlException
                    Throw sqlException
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return result

        End Function

        Public Function UpdateWithTransactionManager(ByVal province As Province, ByVal listOfCity As ArrayList) As Integer
            ' mark as loaded to prevent it loads from db
            province.MarkLoaded()
            For Each city As City In listOfCity
                city.MarkLoaded()
            Next
            ' set default result
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to update/insert city
                    For Each city As City In listOfCity
                        If (city.ID >= 0) Then
                            If (city.LastUpdateBy.ToLower <> "not update") Then
                                m_TransactionManager.AddUpdate(city, m_userPrincipal.Identity.Name)
                            End If
                        Else
                            m_TransactionManager.AddInsert(city, m_userPrincipal.Identity.Name)
                        End If

                        city.MarkLoaded()

                    Next
                    ' add command to update province
                    If (province.LastUpdateBy.ToLower <> "not update") Then
                        m_TransactionManager.AddUpdate(province, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.PerformTransaction()
                    result = province.ID
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result
        End Function

        Public Function Insert(ByVal objDomain As Province) As Integer
            Dim iReturn As Integer = -2
            Try
                m_ProvinceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As Province) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ProvinceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As Province)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ProvinceMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As Province)
            Try
                m_ProvinceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "ProvinceCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(Province), "ProvinceCode", AggregateType.Count)

            Return CType(m_ProvinceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"
        Public Function RetrieveByName(ByVal ProvinceName As String) As Province

            Try
                Dim strSP As String = " up_RetrieveProvince_ByName @ProvinceName='{0}'"

                strSP = String.Format(strSP, ProvinceName)
                Dim ProvinceColl As ArrayList = m_ProvinceMapper.RetrieveSP(strSP)
                If (ProvinceColl.Count > 0) Then
                    Return CType(ProvinceColl(0), Province)
                End If
            Catch ex As Exception
                Dim aaa = 0
            End Try


            Return New Province
        End Function
#End Region

    End Class

End Namespace
