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
'// Generated on 06/08/2014 - 8:53:00 AM
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

Namespace KTB.DNet.BusinessFacade.General
    Public Class AppConfigFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_AppConfigMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_AppConfigMapper = MapperFactory.GetInstance().GetMapper(GetType(AppConfig).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As AppConfig
            Return CType(m_AppConfigMapper.Retrieve(ID), AppConfig)
        End Function

        Public Function RetrieveAll() As ArrayList
            Dim objReturnValue As AppConfig
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim AppConfigList As ArrayList
            AppConfigList = m_AppConfigMapper.RetrieveByCriteria(criterias)

            Return AppConfigList
        End Function


        Public Function Retrieve(ByVal AppConfigName As String) As AppConfig
            Dim objReturnValue As AppConfig
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, AppConfigName))
            Dim AppConfigList As ArrayList
            AppConfigList = m_AppConfigMapper.RetrieveByCriteria(criterias)
            If Not AppConfigList Is Nothing Then
                If AppConfigList.Count > 0 Then
                    objReturnValue = CType(AppConfigList.Item(0), AppConfig)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function IsAppConfigFound(ByVal AppConfigName As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, AppConfigName))
            Dim AppConfigColl As ArrayList = m_AppConfigMapper.RetrieveByCriteria(criterias)
            If (AppConfigColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AppConfigMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AppConfigMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AppConfigMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AppConfig), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AppConfigMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal crit As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AppConfig), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AppConfigMapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AppConfig), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AppConfigMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AppConfig As ArrayList = m_AppConfigMapper.RetrieveByCriteria(criterias)
            Return _AppConfig
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AppConfigColl As ArrayList = m_AppConfigMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AppConfigColl
        End Function
        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AppConfig), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_AppConfigMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AppConfig), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_AppConfigMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AppConfig), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AppConfigMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim AppConfigColl As ArrayList = m_AppConfigMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AppConfigColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim AppConfigColl As ArrayList = m_AppConfigMapper.RetrieveByCriteria(criterias)

            Return AppConfigColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AppConfig), columnName, matchOperator, columnValue))
            Dim AppConfigColl As ArrayList = m_AppConfigMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return AppConfigColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AppConfig), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), columnName, matchOperator, columnValue))

            Return m_AppConfigMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As AppConfig) As Integer
            Dim iReturn As Integer = -2
            Try
                m_AppConfigMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As AppConfig) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_AppConfigMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As AppConfig)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_AppConfigMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As AppConfig) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_AppConfigMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function ValidateCode(ByVal AppConfigName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, AppConfigName))

            crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim agg As Aggregate = New Aggregate(GetType(AppConfig), "Name", AggregateType.Count)

            Return CType(m_AppConfigMapper.RetrieveScalar(agg, crit), Integer)
        End Function





#End Region

#Region "Custom Method"

        Public Function InsertMarbox(strQuery As String) As Boolean
            Dim arr As ArrayList
            arr = m_AppConfigMapper.RetrieveSP(strQuery)
            If arr IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function ValidateCode(ByVal ObjAppConfig As AppConfig) As Boolean

            If IsNothing(ObjAppConfig) Then
                Return False
            End If


            If (IsNothing(ObjAppConfig) OrElse ObjAppConfig.ID = 0) Then
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, ObjAppConfig.Name.ToString()))

                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "AppID", MatchType.Exact, ObjAppConfig.AppID))
                Dim agg As Aggregate = New Aggregate(GetType(AppConfig), "Name", AggregateType.Count)

                ValidateCode = IIf(CType(m_AppConfigMapper.RetrieveScalar(agg, crit), Integer) > 0, False, True)

            ElseIf ObjAppConfig.ID > 0 Then

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, ObjAppConfig.Name.ToString()))
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "AppID", MatchType.Exact, ObjAppConfig.AppID))
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(KTB.DNet.Domain.AppConfig), "ID", MatchType.No, ObjAppConfig.ID))
                Dim agg As Aggregate = New Aggregate(GetType(AppConfig), "Name", AggregateType.Count)

                ValidateCode = IIf(CType(m_AppConfigMapper.RetrieveScalar(agg, crit), Integer) > 0, False, True)
            Else
                ValidateCode = False
            End If



        End Function


#End Region

    End Class

End Namespace
