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
Imports System.Collections.Generic

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.SAP

    Public Class SAPCustomerFacade

        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SAPCustomerMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SAPCustomerMapper = MapperFactory.GetInstance.GetMapper(GetType(SAPCustomer).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SAPCustomer
            Return CType(m_SAPCustomerMapper.Retrieve(ID), SAPCustomer)
        End Function

        Public Function Retrieve(ByVal CustomerName As String) As SAPCustomer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SAPCustomer), "CustomerName", MatchType.Exact, CustomerName))

            Dim SAPCustomerColl As ArrayList = m_SAPCustomerMapper.RetrieveByCriteria(criterias)
            If (SAPCustomerColl.Count > 0) Then
                Return CType(SAPCustomerColl(0), SAPCustomer)
            End If
            Return New SAPCustomer
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SAPCustomerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SAPCustomerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SAPCustomerMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPCustomerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SAPCustomerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SAPCustomer As ArrayList = m_SAPCustomerMapper.RetrieveByCriteria(criterias)
            Return _SAPCustomer
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPCustomerColl As ArrayList = m_SAPCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SAPCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SAPCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SAPCustomerColl As ArrayList = m_SAPCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SAPCustomerColl


            'Dim SAPCustomerColl As ArrayList = m_SAPCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            'Return SAPCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SAPCustomerColl As ArrayList = m_SAPCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SAPCustomer), columnName, matchOperator, columnValue))
            Return SAPCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SAPCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), columnName, matchOperator, columnValue))

            Return m_SAPCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


#End Region

#Region "Transaction/Other Public Method"





#End Region

#Region "Need To Add"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "AreaCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(SAPCustomer), "AreaCode", AggregateType.Count)
        '    Return CType(m_SAPCustomerMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        'Public Function ValidateCode(ByVal Code As String, ByVal IdEdit As Integer) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "AreaCode", MatchType.Exact, Code))
        '    If IdEdit <> 0 Then
        '        crit.opAnd(New Criteria(GetType(SAPCustomer), "ID", MatchType.No, IdEdit))
        '    End If
        '    Dim agg As Aggregate = New Aggregate(GetType(SAPCustomer), "AreaCode", AggregateType.Count)
        '    Return CType(m_SAPCustomerMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function Insert(ByVal objDomain As SAPCustomer) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SAPCustomerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function Update(ByVal objDomain As SAPCustomer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SAPCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SAPCustomer)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SAPCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SAPCustomer) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SAPCustomerMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function DeleteStatus(ByVal objDomain As SAPCustomer) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SAPCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                nResult = 1
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return nResult
        End Function
#End Region

#Region "Custom Method"
        Public Function RetrieveScalar(ByVal crit As CriteriaComposite, ByVal aggr As Aggregate) As Integer
            Try
                Return CType(m_SAPCustomerMapper.RetrieveScalar(aggr, crit), Integer)
            Catch ex As Exception
                Return 0
            End Try

        End Function

        Public Function GetSequence(ByVal vehicleModel As String) As Integer
            Dim arrParam As ArrayList = New ArrayList()
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@VehicleModel", vehicleModel)
            arrParam.Add(param1)

            Dim dataTable As DataTable = m_SAPCustomerMapper.RetrieveDataSet("up_GetSequenceFromSapCustomer", arrParam).Tables(0)

            Return CInt(dataTable.Rows(0)(0))
        End Function

        Public Function GetBodyMessage(ByVal sapCustomerID As Integer) As Dictionary(Of String, String)
            Dim arrParam As ArrayList = New ArrayList()
            Dim result As New Dictionary(Of String, String)
            Dim param1 As SqlClient.SqlParameter = New SqlClient.SqlParameter("@SapCustomerID", sapCustomerID)
            arrParam.Add(param1)

            Dim dsResult As DataSet = m_SAPCustomerMapper.RetrieveDataSet("up_GetBodymessageFromSapCustomer", arrParam)
            result.Add("BodyMessage", dsResult.Tables(0).Rows(0)(0).ToString())
            result.Add("ClientID", dsResult.Tables(1).Rows(0)(0).ToString())
            result.Add("Username", dsResult.Tables(1).Rows(0)(1).ToString())
            result.Add("Password", dsResult.Tables(1).Rows(0)(2).ToString())

            Return result
        End Function

        Public Function GetLeadCustomerWrite() As DataSet
            Return m_SAPCustomerMapper.RetrieveDataSet("up_GetCustomerWriteInExcel")
        End Function


        Public Function GetLeadCustomerQXWrite() As DataSet
            Return m_SAPCustomerMapper.RetrieveDataSet("up_GetCustomerWriteInExcelQX")
        End Function

        Public Function GetDataBroadcast() As DataSet
            Return m_SAPCustomerMapper.RetrieveDataSet("sp_GetDigitalLead_AC_Email")
        End Function
#End Region

    End Class

End Namespace

