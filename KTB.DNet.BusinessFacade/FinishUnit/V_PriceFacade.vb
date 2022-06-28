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
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 29/9/2005 - 10:53:00 AM
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

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class V_PriceFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_PriceMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_PriceMapper = MapperFactory.GetInstance().GetMapper(GetType(V_Price).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As V_Price
            Return CType(m_PriceMapper.Retrieve(ID), V_Price)
        End Function

        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As DateTime) As V_Price
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(V_Price), "ValidFrom", MatchType.Exact, sCode))

            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(crit)
            If (PriceColl.Count > 0) Then
                Return CType(PriceColl(0), V_Price)
            End If
            Return New V_Price
        End Function

        Public Function Retrieve(ByVal nTypeID As Integer, ByVal sCode As DateTime, ByVal DealerID As Integer) As V_Price
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(V_Price), "ValidFrom", MatchType.Exact, sCode))
            crit.opAnd(New Criteria(GetType(V_Price), "Dealer.ID", MatchType.Exact, DealerID))

            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(crit)
            If (PriceColl.Count > 0) Then
                Return CType(PriceColl(0), V_Price)
            End If
            Return New V_Price
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_PriceMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_Price), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PriceMapper.RetrieveList(sortColl)
        End Function


        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_Price), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_PriceMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Price As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias)
            Return _Price
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PriceColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) AndAlso (Not IsNothing(sortColumn)) AndAlso sortColumn <> "" Then
                sortColl.Add(New Sort(GetType(V_Price), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AppConfigColl As ArrayList = m_PriceMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AppConfigColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_Price), columnName, matchOperator, columnValue))
            Dim PriceColl As ArrayList = m_PriceMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PriceColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_Price), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), columnName, matchOperator, columnValue))

            Return m_PriceMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As V_Price) As Integer
            Dim iReturn As Integer = -2
            Try
                m_PriceMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As V_Price) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_PriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As V_Price)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_PriceMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As V_Price)
            Try
                m_PriceMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function ValidateCode(ByVal nTypeID As Integer, ByVal dValidFrom As DateTime, ByVal DealerID As Integer) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), "VechileColor.ID", MatchType.Exact, nTypeID))
            crit.opAnd(New Criteria(GetType(V_Price), "ValidFrom", MatchType.Exact, dValidFrom))
            crit.opAnd(New Criteria(GetType(V_Price), "Dealer.ID", MatchType.Exact, DealerID))


            Dim agg As Aggregate = New Aggregate(GetType(V_Price), "ValidFrom", AggregateType.Count)

            Return CType(m_PriceMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_PriceMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24

        Public Function RetrieveByCriteria(ByVal ObjContractDetail As ContractDetail) As V_Price
            Dim ObjPrice As V_Price = New V_Price
            Dim objPriceArrayList As ArrayList = New ArrayList
            Dim validFrom As DateTime = New DateTime(ObjContractDetail.ContractHeader.PricePeriodYear, ObjContractDetail.ContractHeader.PricePeriodMonth, ObjContractDetail.ContractHeader.PricePeriodDay)

            'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(V_Price), "Dealer.ID", MatchType.Exact, ObjContractDetail.ContractHeader.Dealer.ID))
            'criterias.opAnd(New Criteria(GetType(V_Price), "VechileColor.ID", MatchType.Exact, ObjContractDetail.VechileColor.ID))
            'criterias.opAnd(New Criteria(GetType(V_Price), "ValidFrom", MatchType.LesserOrEqual, validFrom))
            'Dim sortColl As SortCollection = New SortCollection
            'sortColl.Add(New Sort(GetType(V_Price), "ValidFrom", Sort.SortDirection.DESC))
            'objPriceArrayList = m_PriceMapper.RetrieveByCriteria(criterias, sortColl)

            'For Each item as V_price In objPriceArrayList
            '    If item.ValidFrom <= ValidFrom Then
            '        ObjPrice = item
            '        Exit For
            '    End If
            'Next
            Dim SQL As String

            SQL = String.Format("exec up_RetrievePriceList_Active @ValidFrom='{0}', @dealerid={1} ,@VechileColorID={2}", validFrom.ToString("yyyy/MM/dd"), ObjContractDetail.ContractHeader.Dealer.ID.ToString(), ObjContractDetail.VechileColor.ID.ToString())

            objPriceArrayList = m_PriceMapper.RetrieveSP(SQL)


            For Each item As V_price In objPriceArrayList
                If item.ValidFrom <= validFrom Then
                    ObjPrice = item
                    Exit For
                End If
            Next


            Return ObjPrice

        End Function

        Public Function Retrieve(ByVal ObjContractDetail As ContractDetail) As ArrayList

            Dim objPriceArrayList As ArrayList = New ArrayList
            Dim validFrom As DateTime = New DateTime(ObjContractDetail.ContractHeader.PricePeriodYear, ObjContractDetail.ContractHeader.PricePeriodMonth, ObjContractDetail.ContractHeader.PricePeriodDay)

            Dim SQL As String

            SQL = String.Format("exec up_RetrievePriceList_Active @ValidFrom='{0}', @dealerid={1} ,@VechileColorID={2}", validFrom.ToString("yyyy/MM/dd"), ObjContractDetail.ContractHeader.Dealer.ID.ToString(), ObjContractDetail.VechileColor.ID.ToString())

            objPriceArrayList = m_PriceMapper.RetrieveSP(SQL)

            Return objPriceArrayList

        End Function

        '' END OF CR Sirkular Rewards
#End Region

    End Class
End Namespace
