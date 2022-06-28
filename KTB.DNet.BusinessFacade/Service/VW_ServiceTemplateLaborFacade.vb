
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
'// Generated on 11/15/2016 - 9:11:13 AM
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

Namespace KTB.DNET.BusinessFacade

    Public Class VW_ServiceTemplateLaborFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_VW_ServiceTemplateLaborMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_VW_ServiceTemplateLaborMapper = MapperFactory.GetInstance.GetMapper(GetType(VW_ServiceTemplateLabor).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As VW_ServiceTemplateLabor
            Return CType(m_VW_ServiceTemplateLaborMapper.Retrieve(ID), VW_ServiceTemplateLabor)
        End Function


        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VW_ServiceTemplateLaborMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VW_ServiceTemplateLaborMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VW_ServiceTemplateLaborMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VW_ServiceTemplateLabor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VW_ServiceTemplateLaborMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VW_ServiceTemplateLabor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VW_ServiceTemplateLaborMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_ServiceTemplateLabor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _VW_ServiceTemplateLabor As ArrayList = m_VW_ServiceTemplateLaborMapper.RetrieveByCriteria(criterias)
            Return _VW_ServiceTemplateLabor
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_ServiceTemplateLabor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VW_ServiceTemplateLaborColl As ArrayList = m_VW_ServiceTemplateLaborMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VW_ServiceTemplateLaborColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VW_ServiceTemplateLaborColl As ArrayList = m_VW_ServiceTemplateLaborMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return VW_ServiceTemplateLaborColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_ServiceTemplateLabor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VW_ServiceTemplateLaborColl As ArrayList = m_VW_ServiceTemplateLaborMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(VW_ServiceTemplateLabor), columnName, matchOperator, columnValue))
            Return VW_ServiceTemplateLaborColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VW_ServiceTemplateLabor), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VW_ServiceTemplateLabor), columnName, matchOperator, columnValue))

            Return m_VW_ServiceTemplateLaborMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As VW_ServiceTemplateLabor) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_VW_ServiceTemplateLaborMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As VW_ServiceTemplateLabor) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_VW_ServiceTemplateLaborMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As VW_ServiceTemplateLabor)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_VW_ServiceTemplateLaborMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As VW_ServiceTemplateLabor)
            Try
                m_VW_ServiceTemplateLaborMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function GetDownLoadExcel(ByVal strCondition As String) As DataTable
            Dim arrParam As New ArrayList
            arrParam.Add(New SqlClient.SqlParameter("@WhereCondition", strCondition.Replace("{", "").Replace("}", "")))

            Dim dSet As DataSet = m_VW_ServiceTemplateLaborMapper.RetrieveDataSet("VW_ServiceTemplateLabor", arrParam)
            Return dSet.Tables(0)
        End Function
#End Region

    End Class

End Namespace


