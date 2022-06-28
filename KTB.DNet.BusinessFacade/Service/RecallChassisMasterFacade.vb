
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
'// Copyright  2016
'// ---------------------
'// $History      : $
'// Generated on 19/04/2016 - 13:14:03
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

Namespace KTB.DNET.BusinessFacade.Service

    Public Class RecallChassisMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_RecallChassisMasterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_RecallChassisMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(RecallChassisMaster).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As RecallChassisMaster
            Return CType(m_RecallChassisMasterMapper.Retrieve(ID), RecallChassisMaster)
        End Function

        Public Function Retrieve(ByVal Code As String) As RecallChassisMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.Exact, Code))

            Dim RecallChassisMasterColl As ArrayList = m_RecallChassisMasterMapper.RetrieveByCriteria(criterias)
            If (RecallChassisMasterColl.Count > 0) Then
                Return CType(RecallChassisMasterColl(0), RecallChassisMaster)
            End If
            Return New RecallChassisMaster
        End Function


        Public Function Retrieve(ByVal strChassisNo As String, ByVal strREcallRegNo As String, Optional ByVal isForService As Boolean = False) As RecallChassisMaster
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.Exact, strChassisNo))
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "RecallCategory.RecallRegNo", MatchType.Exact, strREcallRegNo))
            'If isForService Then
            '    criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "RecallCategory.ValidStartDate", MatchType.LesserOrEqual, DateTime.Now.ToString("yyyy/MM/dd")))
            'End If
            Dim RecallChassisMasterColl As ArrayList = m_RecallChassisMasterMapper.RetrieveByCriteria(criterias)
            If (RecallChassisMasterColl.Count > 0) Then
                Return CType(RecallChassisMasterColl(0), RecallChassisMaster)
            End If
            Return New RecallChassisMaster
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_RecallChassisMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_RecallChassisMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_RecallChassisMasterMapper.RetrieveList
        End Function

        ' Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        'ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal criterias As CriteriaComposite) As ArrayList

        '     Dim sortColl As SortCollection = New SortCollection

        '     If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
        '         sortColl.Add(New Sort(GetType(RecallChassisMaster), sortColumn, sortDirection))
        '     Else
        '         sortColl = Nothing
        '     End If

        '     Return m_RecallChassisMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        ' End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(RecallChassisMaster), SortColumn, sortDirection))
            Dim PresentationGroupColl As ArrayList = m_RecallChassisMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return PresentationGroupColl
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RecallChassisMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_RecallChassisMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _RecallChassisMaster As ArrayList = m_RecallChassisMasterMapper.RetrieveByCriteria(criterias)
            Return _RecallChassisMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RecallChassisMasterColl As ArrayList = m_RecallChassisMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return RecallChassisMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim RecallChassisMasterColl As ArrayList = m_RecallChassisMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return RecallChassisMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim RecallChassisMasterColl As ArrayList = m_RecallChassisMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), columnName, matchOperator, columnValue))
            Return RecallChassisMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(RecallChassisMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), columnName, matchOperator, columnValue))

            Return m_RecallChassisMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RecallChassisMasterCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(RecallChassisMaster), "RecallChassisMasterCode", AggregateType.Count)
            Return CType(m_RecallChassisMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function RetrieveScalar(ByVal agg As Aggregate, ByVal crit As CriteriaComposite) As Integer
            'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RecallChassisMasterCode", MatchType.Exact, Code))
            'Dim agg As Aggregate = New Aggregate(GetType(RecallChassisMaster), "RecallChassisMasterCode", AggregateType.Count)
            Return CType(m_RecallChassisMasterMapper.RetrieveScalar(agg, crit), Integer)
        End Function


        Public Function Insert(ByVal objDomain As RecallChassisMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_RecallChassisMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As RecallChassisMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_RecallChassisMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As RecallChassisMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_RecallChassisMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As RecallChassisMaster)
            Try
                m_RecallChassisMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveSp(str As String) As DataTable
            Dim arr As DataSet
            arr = m_RecallChassisMasterMapper.RetrieveDataSet(str)

            If arr.Tables.Count > 0 Then
                Return arr.Tables(0)
            Else
                Return Nothing
            End If
        End Function
#End Region

    End Class

End Namespace

