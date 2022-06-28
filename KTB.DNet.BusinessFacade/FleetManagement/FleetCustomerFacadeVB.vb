
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
'// Copyright  2017
'// ---------------------
'// $History      : $
'// Generated on 9/11/2017 - 3:04:45 PM
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

    Public Class FleetCustomerFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_FleetCustomerMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_FleetCustomerMapper = MapperFactory.GetInstance.GetMapper(GetType(FleetCustomer).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FleetCustomer
            Return CType(m_FleetCustomerMapper.Retrieve(ID), FleetCustomer)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FleetCustomerMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FleetCustomerMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FleetCustomerMapper.RetrieveList
        End Function

        ''' <summary>
        ''' Retrieve by Code
        ''' </summary>
        ''' <param name="Code"></param>
        ''' <returns></returns>
        ''' <remarks> append by ali</remarks>
        Public Function Retrieve(ByVal Code As String) As FleetCustomer
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetCustomer), "Code", MatchType.Exact, Code))

            Dim FleetCustomerColl As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(criterias)
            If (FleetCustomerColl.Count > 0) Then
                Return CType(FleetCustomerColl(0), FleetCustomer)
            End If
            Return Nothing
        End Function


        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FleetCustomerMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FleetCustomerMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FleetCustomer As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(criterias)
            Return _FleetCustomer
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FleetCustomerColl As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FleetCustomerColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FleetCustomerColl As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FleetCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FleetCustomerColl As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return FleetCustomerColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(FleetCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim FleetCustomerColl As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FleetCustomerColl

        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FleetCustomerColl As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(FleetCustomer), columnName, matchOperator, columnValue))
            Return FleetCustomerColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetCustomer), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomer), columnName, matchOperator, columnValue))

            Return m_FleetCustomerMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As FleetCustomer) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_FleetCustomerMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FleetCustomer) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FleetCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As FleetCustomer)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FleetCustomerMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As FleetCustomer)
            Try
                m_FleetCustomerMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveSp(str As String) As DataSet
            Return m_FleetCustomerMapper.RetrieveDataSet(str)
        End Function

        ' fleet customer id di set 0 jika saat insert new data
        Public Function ValidateCode(ByVal Code As String, Optional ByVal fleetCustomerID As Integer = 0) As String
            Dim str As String = String.Empty
            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim arrList As ArrayList = m_FleetCustomerMapper.RetrieveByCriteria(crt)
            If arrList.Count > 0 Then
                If fleetCustomerID <> 0 Then
                    '    ' jika saat insert
                    '    For Each item As FleetCustomer In arrList
                    '        If item.Code = Code Then
                    '            str = "Code sudah ada!"
                    '        End If
                    '    Next
                    'Else
                    ' jika saat edit
                    For Each item As FleetCustomer In arrList
                        If item.ID = fleetCustomerID Then
                            Continue For
                        End If

                        If item.Code = Code Then
                            str = "Code sudah ada!"
                        End If

                    Next
                End If

            End If

            Return str
        End Function

#End Region

    End Class

End Namespace

