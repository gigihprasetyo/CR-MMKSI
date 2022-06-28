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
'// Generated on 8/14/2007 - 2:31:58 PM
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

Namespace KTB.DNet.BusinessFacade.CallCenter

    Public Class CcContactFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_CcContactMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_CcContactMapper = MapperFactory.GetInstance.GetMapper(GetType(CcContact).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As CcContact
            Return CType(m_CcContactMapper.Retrieve(ID), CcContact)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_CcContactMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_CcContactMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_CcContactMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcContact), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcContactMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcContact), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_CcContactMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _CcContact As ArrayList = m_CcContactMapper.RetrieveByCriteria(criterias)
            Return _CcContact
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcContactColl As ArrayList = m_CcContactMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return CcContactColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim CcContactColl As ArrayList = m_CcContactMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return CcContactColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim CcContactColl As ArrayList = m_CcContactMapper.RetrieveByCriteria(criterias)
            Return CcContactColl
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(CcContact), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim CcContactColl As ArrayList = m_CcContactMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return CcContactColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim CcContactColl As ArrayList = m_CcContactMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(CcContact), columnName, matchOperator, columnValue))
            Return CcContactColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(CcContact), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcContact), columnName, matchOperator, columnValue))

            Return m_CcContactMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveCountByCriteria(ByVal strVehicleCategoryID As String, ByVal strCustomerCategoryID As String, _
                                           ByVal handphoneNo As String, ByVal homePhoneNo As String, ByVal officePhoneNo As String, ByVal periodID As Integer) As Boolean
            Dim isExist As Boolean = False
            Try

                Dim _SQL As String = "EXEC [Sp_CcContactSurveyed] "
                _SQL += "@CcCustomerCategoryID = " & strCustomerCategoryID & ","
                _SQL += "@CcVehicleCategoryID = " & strVehicleCategoryID & ","
                _SQL += "@HandphoneNo = N'" & handphoneNo & "',"
                _SQL += "@HomePhoneNo = N'" & homePhoneNo & "',"
                _SQL += "@OfficePhoneNo = N'" & officePhoneNo & "',"
                _SQL += "@periodID = " & periodID & ""

                Dim arl As ArrayList = m_CcContactMapper.RetrieveSP(_SQL)
                If arl.Count > 0 Then
                    isExist = True
                End If
            Catch ex As Exception

            End Try

            Return isExist
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As CcContact) As Integer
            Dim iReturn As Integer = 1
            Try
                m_CcContactMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As CcContact) As Integer
            Dim nResult As Integer = 1
            Try
                nResult = m_CcContactMapper.Update(objDomain, m_userPrincipal.Identity.Name)
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

#End Region

    End Class

End Namespace


