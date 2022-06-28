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
'// Generated on 9/26/2005 - 2:38:25 PM
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

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class SparePartPODetailFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SparePartPODetailMapper As IMapper

        Private m_SparePartPOMapper As IMapper

        '  Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SparePartPODetailMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPODetail).ToString)
            Me.m_SparePartPOMapper = MapperFactory.GetInstance.GetMapper(GetType(SparePartPO).ToString)
        End Sub

#End Region

#Region "Retrieve"

        'Public Function RetrieveBySPPOID_SPMasterID(ByVal intSPPOId As Integer, ByVal intSPMasterID As Integer)
        '    Dim objSPPO As SparePartPO = Me.m_SparePartPOMapper.Retrieve(intSPPOId)
        '    For Each objDetail As SparePartPODetail In objSPPO.SparePartPODetails
        '        If (objDetail.SparePartMaster.ID = intSPMasterID) Then
        '            Return objDetail
        '        End If
        '    Next
        '    Return Nothing
        'End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If Not IsNothing(sortColumn) And sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(SparePartPODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPODetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As SparePartPODetail
            Return CType(m_SparePartPODetailMapper.Retrieve(ID), SparePartPODetail)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SparePartPODetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SparePartPODetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SparePartPODetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPODetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SparePartPODetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SparePartPODetail As ArrayList = m_SparePartPODetailMapper.RetrieveByCriteria(criterias)
            Return _SparePartPODetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPODetailColl As ArrayList = m_SparePartPODetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SparePartPODetailColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SparePartPODetailColl As ArrayList = m_SparePartPODetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SparePartPODetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SparePartPODetailColl As ArrayList = m_SparePartPODetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), columnName, matchOperator, columnValue))
            Return SparePartPODetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), columnName, matchOperator, columnValue))

            Return m_SparePartPODetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SparePartPODetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPPODetailsColl As ArrayList = m_SparePartPODetailMapper.RetrieveByCriteria(Criterias, sortColl)
            Return SPPODetailsColl
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As SparePartPODetail) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SparePartPODetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                Throw
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SparePartPODetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPODetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SparePartPODetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SparePartPODetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SparePartPODetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SparePartPODetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function


#End Region

#Region "Custom Method"

        Public Function RetrieveSparePartPOTypeTOP_Validation(ByVal partnumber As String, ByVal id As Integer) As DataTable
            Dim arr As New DataSet
            Dim strQuery As String

            strQuery = "exec up_RetrieveSparePartPOTypeTOP_Validation '" & partnumber & "', " & id & ""
            arr = m_SparePartPODetailMapper.RetrieveDataSet(strQuery)

            If arr.Tables(0).Rows.Count > 0 Then
                Return arr.Tables(0)
            Else
                Return Nothing
            End If
        End Function

        Public Function ValidateCheckListStatus(ByVal nPOID As Integer, ByVal strStatus As String) As Integer
            Dim agg As Aggregate = New Aggregate(GetType(SparePartPODetail), "CheckListStatus", AggregateType.Count)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartPO.ID", MatchType.Exact, nPOID))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "CheckListStatus", MatchType.Exact, strStatus))
            Return CType(m_SparePartPODetailMapper.RetrieveScalar(agg, criterias), Integer)
        End Function

        Public Function ValidateItem(ByVal nPOID As Integer, ByVal strPartNumber As String) As SparePartPODetail
            'Dim agg As Aggregate = New Aggregate(GetType(SparePartPODetail), "CheckListStatus", AggregateType.Count)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPODetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartPO.ID", MatchType.Exact, nPOID))
            criterias.opAnd(New Criteria(GetType(SparePartPODetail), "SparePartMaster.PartNumber", MatchType.Exact, strPartNumber))
            Dim arlPODetail As ArrayList = m_SparePartPODetailMapper.RetrieveByCriteria(criterias)
            If arlPODetail.Count > 0 Then
                Return CType(arlPODetail(0), SparePartPODetail)
            End If
            Return Nothing
        End Function

#End Region

    End Class



End Namespace
