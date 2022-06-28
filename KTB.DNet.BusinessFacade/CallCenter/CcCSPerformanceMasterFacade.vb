
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
'// Copyright  2018
'// ---------------------
'// $History      : $
'// Generated on 5/6/2018 - 2:57:20 PM
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade.CallCenter

    Public Class CcCSPerformanceMasterFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CcCSPerformanceMasterMapper As IMapper
        Private m_CcCSPerformanceParameterMapper As IMapper
        Private m_CcCSPerformanceSubParameterMapper As IMapper
        Private m_CcCSPerformanceClasificationMapper As IMapper
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CcCSPerformanceMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(CcCSPerformanceMaster).ToString)
            Me.m_CcCSPerformanceParameterMapper = MapperFactory.GetInstance.GetMapper(GetType(CcCSPerformanceParameter).ToString)
            Me.m_CcCSPerformanceSubParameterMapper = MapperFactory.GetInstance.GetMapper(GetType(CcCSPerformanceSubParameter).ToString)
            Me.m_CcCSPerformanceClasificationMapper = MapperFactory.GetInstance.GetMapper(GetType(CcCSPerformanceClasification).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CcCSPerformanceMaster
            Return CType(m_CcCSPerformanceMasterMapper.Retrieve(ID), CcCSPerformanceMaster)
        End Function

        Public Function Retrieve(ByVal code As String) As Integer
            Dim nreturn As Integer
            Dim criCSPMaster As New CriteriaComposite(New Criteria(GetType(CcCSPerformanceMaster), "Code", MatchType.Exact, code))

            nreturn = CType((m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criCSPMaster)).Item(0), CcCSPerformanceMaster).ID

            Return nreturn
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CcCSPerformanceMasterMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcCSPerformanceMasterMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcCSPerformanceMasterMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CcCSPerformanceMaster As ArrayList = m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias)
            Return _CcCSPerformanceMaster
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcCSPerformanceMasterColl As ArrayList = m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CcCSPerformanceMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
           ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CSPMasterColl As ArrayList = m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CSPMasterColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CcCSPerformanceMasterColl As ArrayList = m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CcCSPerformanceMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcCSPerformanceMasterColl As ArrayList = m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CcCSPerformanceMaster), columnName, matchOperator, columnValue))
            Return CcCSPerformanceMasterColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcCSPerformanceMaster), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcCSPerformanceMaster), columnName, matchOperator, columnValue))

            Return m_CcCSPerformanceMasterMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As CcCSPerformanceMaster) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_CcCSPerformanceMasterMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CcCSPerformanceMaster) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_CcCSPerformanceMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As CcCSPerformanceMaster)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_CcCSPerformanceMasterMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As CcCSPerformanceMaster)
            Try
                m_CcCSPerformanceMasterMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function RetrieveUsed(ByVal ID As Integer) As Integer
            Dim sp = String.Format(" exec sp_CcCSPerformanceMasterRetrieveUsed {0}", ID)
            Return Me.m_CcCSPerformanceMasterMapper.ExecuteSP(sp)
        End Function

        Public Function InsertFromReff(ByVal CSPMasterID As Integer, ByVal CSPMasterReffId As Integer) As Integer
            Dim sp = String.Format(" exec sp_CcCSPerformanceFormulaCopyReferences {0},{1},'{2}'", CSPMasterID, CSPMasterReffId, m_userPrincipal.Identity.Name)
            Return Me.m_CcCSPerformanceMasterMapper.ExecuteSP(sp)
        End Function

        Public Function GenerateCSP()
            Dim sp = String.Format(" exec up_CcCSPerformanceGenerator '{0}',{1}", Now.Date, 1)
            Me.m_CcCSPerformanceMasterMapper.ExecuteSP(sp)
        End Function
#End Region

    End Class

End Namespace

