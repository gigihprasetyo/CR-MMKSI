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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.General
    Public Class LeasingFeeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_LeasingFeeMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_LeasingFeeMapper = MapperFactory.GetInstance().GetMapper(GetType(LeasingFee).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function IsPeriodeExist(ByVal intVechileTypeId As Integer, ByVal dtmFrom As DateTime, ByVal dtmTo As DateTime, ByVal intId As Integer) As Boolean
            Dim arl As ArrayList = RetrieveByVehicleTypeID(intVechileTypeId, intId, dtmFrom, dtmTo)
            If IsNothing(arl) Or arl.Count = 0 Then Return False
            Return True
        End Function

        Public Function RetrieveByVehicleTypeID(ByVal intVechileTypeId As Integer, ByVal intId As Integer, ByVal dtmFrom As DateTime, ByVal dtmTo As DateTime) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
            crits.opAnd(New Criteria(GetType(LeasingFee), "VechileType", MatchType.Exact, intVechileTypeId))

            crits.opAnd(New Criteria(GetType(LeasingFee), "DateFrom", MatchType.GreaterOrEqual, dtmFrom), "(", True)
            crits.opAnd(New Criteria(GetType(LeasingFee), "DateFrom", MatchType.LesserOrEqual, dtmTo))

            crits.opOr(New Criteria(GetType(LeasingFee), "DateTo", MatchType.GreaterOrEqual, dtmFrom))
            crits.opAnd(New Criteria(GetType(LeasingFee), "DateTo", MatchType.LesserOrEqual, dtmTo), ")", False)

            If (intId <> 0) Then
                crits.opAnd(New Criteria(GetType(LeasingFee), "ID", MatchType.No, intId))
            End If
            Return Retrieve(crits)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As LeasingFee
            Return CType(m_LeasingFeeMapper.Retrieve(ID), LeasingFee)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_LeasingFeeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_LeasingFeeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Return m_LeasingFeeMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LeasingFeeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection, ByVal crit As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LeasingFeeMapper.RetrieveByCriteria(crit, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LeasingFeeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _coll As ArrayList = m_LeasingFeeMapper.RetrieveByCriteria(criterias)
            Return _coll
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim LeasingFeeColl As ArrayList = m_LeasingFeeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LeasingFeeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim LeasingFeeColl As ArrayList = m_LeasingFeeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return LeasingFeeColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_LeasingFeeMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_LeasingFeeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim LeasingFeeColl As ArrayList = m_LeasingFeeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LeasingFeeColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, _
            ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim LeasingFeeColl As ArrayList = m_LeasingFeeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return LeasingFeeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(LeasingFee), columnName, matchOperator, columnValue))
            Dim LeasingFeeColl As ArrayList = m_LeasingFeeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return LeasingFeeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(LeasingFee), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LeasingFee), columnName, matchOperator, columnValue))

            Return m_LeasingFeeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As LeasingFee) As Integer
            Dim iReturn As Integer = -2
            Try
                m_LeasingFeeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As LeasingFee) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_LeasingFeeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As LeasingFee)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_LeasingFeeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As LeasingFee) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_LeasingFeeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
