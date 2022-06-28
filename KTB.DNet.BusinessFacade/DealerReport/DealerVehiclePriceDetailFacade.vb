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
'// Copyright  2021
'// ---------------------
'// $History      : $
'// Generated on 8/19/2021 - 3:28:03 PM
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

    Public Class DealerVehiclePriceDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DealerVehiclePriceDetailMapper As IMapper


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DealerVehiclePriceDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(DealerVehiclePriceDetail).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DealerVehiclePriceDetail
            Return CType(m_DealerVehiclePriceDetailMapper.Retrieve(ID), DealerVehiclePriceDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As DealerVehiclePriceDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "DealerVehiclePriceDetailCode", MatchType.Exact, Code))

            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias)
            If (DealerVehiclePriceDetailColl.Count > 0) Then
                Return CType(DealerVehiclePriceDetailColl(0), DealerVehiclePriceDetail)
            End If
            Return New DealerVehiclePriceDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DealerVehiclePriceDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerVehiclePriceDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerVehiclePriceDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerVehiclePriceDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DealerVehiclePriceDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DealerVehiclePriceDetail As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias)
            Return _DealerVehiclePriceDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DealerVehiclePriceDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DealerVehiclePriceDetail), SortColumn, sortDirection))
            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DealerVehiclePriceDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DealerVehiclePriceDetailColl
        End Function
        'cr sfid
        Public Function RetrieveByCategory(ByVal Category As String, ByVal Tipe As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileColorCode", MatchType.Exact, Category))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileTypeCode", MatchType.Exact, Tipe))

            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias)
            Return DealerVehiclePriceDetailColl
        End Function

        Public Function RetrieveByCategory(ByVal Category As String, ByVal Tipe As String, ByVal CustomerType As String, ByVal DealerID As String) As ArrayList
            Dim query = "select top 1 a.GUID " +
                        "from dealervehicleprice a " +
                        "JOIN DealervehiclePRiceDetail b on b.DealerVehiclePriceGUID = a.GUID " +
                        "where a.rowstatus=0 and b.rowstatus=0 and b.vechiletypecode = '" + Tipe + "' and b.VechileColorCode = '" + Category + "' and a.dealerID = " + DealerID + " and a.CustomerTypeDNET = " + CustomerType + " and effectiveStartDate <= getdate() order by effectiveStartDate desc "
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "DealerVehiclePriceGUID", MatchType.InSet, "(" + query + ")"))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileColorCode", MatchType.Exact, Category))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileTypeCode", MatchType.Exact, Tipe))

            'Dim sortColl As SortCollection = New SortCollection
            'sortColl.Add(New Sort(GetType(DealerVehiclePriceDetail), "effectiveStartDate", "DESC"))

            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias)
            Return DealerVehiclePriceDetailColl
        End Function

        Public Function RetrieveByCategory(ByVal Category As String, ByVal Tipe As String, ByVal CustomerType As String, ByVal DealerID As String, ByVal ID As String) As ArrayList
            Dim query = "select top 1 a.GUID " +
                        "from dealervehicleprice a " +
                        "JOIN DealervehiclePRiceDetail b on b.DealerVehiclePriceGUID = a.GUID " +
                        "where a.rowstatus=0 and b.rowstatus=0 and b.vechiletypecode = '" + Tipe + "' and b.VechileColorCode = '" + Category + "' and a.dealerID = " + DealerID + " and a.CustomerTypeDNET = " + CustomerType + " and effectiveStartDate <= getdate() order by effectiveStartDate desc "
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "DealerVehiclePriceGUID", MatchType.InSet, "(" + query + ")"))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileColorCode", MatchType.Exact, Category))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "VechileTypeCode", MatchType.Exact, Tipe))
            criterias.opOr(New Criteria(GetType(DealerVehiclePriceDetail), "ID", MatchType.Exact, ID))

            'Dim sortColl As SortCollection = New SortCollection
            'sortColl.Add(New Sort(GetType(DealerVehiclePriceDetail), "effectiveStartDate", "DESC"))

            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias)
            Return DealerVehiclePriceDetailColl
        End Function

        Public Function RetrieveByID(ByVal id As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), "ID", MatchType.Exact, id))


            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias)
            Return DealerVehiclePriceDetailColl
        End Function
        '

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DealerVehiclePriceDetailColl As ArrayList = m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DealerVehiclePriceDetail), columnName, matchOperator, columnValue))
            Return DealerVehiclePriceDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DealerVehiclePriceDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), columnName, matchOperator, columnValue))

            Return m_DealerVehiclePriceDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerVehiclePriceDetail), "DealerVehiclePriceDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DealerVehiclePriceDetail), "DealerVehiclePriceDetailCode", AggregateType.Count)
            Return CType(m_DealerVehiclePriceDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DealerVehiclePriceDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DealerVehiclePriceDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DealerVehiclePriceDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DealerVehiclePriceDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DealerVehiclePriceDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DealerVehiclePriceDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DealerVehiclePriceDetail)
            Try
                m_DealerVehiclePriceDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace
