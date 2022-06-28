
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
Imports KTB.DNet.DataMapper
#End Region

Namespace KTB.DNET.BusinessFacade.SparePart

    Public Class OrderRestrictionFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_OrderRestrictionMapper As IMapper

        '  Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_OrderRestrictionMapper = MapperFactory.GetInstance.GetMapper(GetType(OrderRestriction).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As OrderRestriction
            Return CType(m_OrderRestrictionMapper.Retrieve(ID), OrderRestriction)
        End Function

        Public Function Retrieve(ByVal code As String) As OrderRestriction
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "PartNumber", MatchType.Exact, code))

            Dim partColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(criterias)
            If (partColl.Count > 0) Then
                Return CType(partColl(0), OrderRestriction)
            End If
            Return New OrderRestriction
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_OrderRestrictionMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_OrderRestrictionMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_OrderRestrictionMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrderRestriction), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OrderRestrictionMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrderRestriction), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_OrderRestrictionMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _OrderRestriction As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(criterias)
            Return _OrderRestriction
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim OrderRestrictionColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return OrderRestrictionColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrderRestriction), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            'Dim OrderRestrictColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Dim OrderRestrictColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return OrderRestrictColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrderRestriction), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim OrderRestrictionColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return OrderRestrictionColl
        End Function

        Public Function RetrieveActiveList(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrderRestriction), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim OrderRestrictionColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return OrderRestrictionColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim OrderRestrictionColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return OrderRestrictionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), columnName, matchOperator, columnValue))
            Dim OrderRestrictionColl As ArrayList = m_OrderRestrictionMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return OrderRestrictionColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrderRestriction), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), columnName, matchOperator, columnValue))

            Return m_OrderRestrictionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveWithOneCriteriaExtModel(ByVal ExtModel As String, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(OrderRestriction), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), columnName, matchOperator, columnValue))
            If ExtModel <> "" And Not ExtModel Is String.Empty Then
                criterias.opAnd(New Criteria(GetType(OrderRestriction), "ModelCode", matchOperator.Exact, ExtModel))
            End If

            Return m_OrderRestrictionMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Return CType(m_OrderRestrictionMapper.RetrieveScalar(aggregation, criterias), Integer)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "OrderRestrictionCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(OrderRestriction), "OrderRestrictionCode", AggregateType.Count)
            Return CType(m_OrderRestrictionMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As OrderRestriction) As Integer
            Dim iReturn As Integer = -2
            Try
                m_OrderRestrictionMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As OrderRestriction) As Integer
            Dim nResult As Integer = -2
            Try
                nResult = m_OrderRestrictionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As OrderRestriction)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = objDomain.RowStatus
                m_OrderRestrictionMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As OrderRestriction)
            Try
                m_OrderRestrictionMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


#End Region

#Region "Custom Method"

        Public Function isOrderRestricted(ByVal ObjDealer As Dealer, ByVal sOrderType As String) As String

            Dim ObjOrderRestriction As New OrderRestriction
            Dim arrList As ArrayList = New ArrayList
            Dim tDateFrom As DateTime
            Dim tDateTo As DateTime
            Dim sTimeFrom() As String
            Dim sTimeTo() As String

            Dim criterias As New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "IsActive", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "Dealer.DealerCode", MatchType.Exact, ObjDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "OrderType", MatchType.Exact, sOrderType))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateFrom", MatchType.LesserOrEqual, Format(DateTime.Now, "yyyy/MM/dd")))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "DateTo", MatchType.GreaterOrEqual, Format(DateTime.Now, "yyyy/MM/dd")))

            arrList = Me.Retrieve(criterias)
            If arrList.Count > 0 Then
                ObjOrderRestriction = CType(arrList(0), OrderRestriction)
                Return "Pesan : " & ObjOrderRestriction.Note
            End If

            criterias = New CriteriaComposite(New Criteria(GetType(OrderRestriction), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "IsActive", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "Dealer.DealerCode", MatchType.Exact, ObjDealer.DealerCode))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "OrderType", MatchType.Exact, sOrderType))
            criterias.opAnd(New Criteria(GetType(OrderRestriction), "Days", MatchType.Exact, CType(DateTime.Now.DayOfWeek, Integer)))

            arrList = Me.Retrieve(criterias)

            If arrList.Count > 0 Then
                For i As Integer = 0 To arrList.Count - 1
                    sTimeFrom = CType(arrList(i), OrderRestriction).TimeFrom.Split(":")
                    tDateFrom = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, sTimeFrom(0), sTimeFrom(1), sTimeFrom(2))

                    sTimeTo = CType(arrList(i), OrderRestriction).TimeTO.Split(":")
                    tDateTo = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, sTimeTo(0), sTimeTo(1), sTimeTo(2))

                    If tDateFrom >= DateTime.Now And DateTime.Now <= tDateTo Then
                        ObjOrderRestriction = CType(arrList(0), OrderRestriction)
                        Return "Pesan : " & ObjOrderRestriction.Note
                    End If
                Next i
            End If

            Return ""

        End Function

#End Region

    End Class

End Namespace




