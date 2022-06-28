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
'// Generated on 9/26/2005 - 1:43:31 PM
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

Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.Dnet.BusinessFacade.SparePart

    Public Class AnnualDiscountAchievementFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AnnualDiscountAchievementMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_AnnualDiscountAchievementMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.AnnualDiscountAchievement).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AnnualDiscountAchievement))
        End Sub

#End Region

#Region "Retrieve"


        Public Function RetrieveListValidateFrom() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Me.m_AnnualDiscountAchievementMapper = MapperFactory.GetInstance.GetMapper(GetType(AnnualDiscountAchievement).ToString)
            Return m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As AnnualDiscountAchievement
            Return CType(m_AnnualDiscountAchievementMapper.Retrieve(ID), AnnualDiscountAchievement)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AnnualDiscountAchievement), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias, sortColl)
        End Function


        Public Function Retrieve(ByVal Code As String) As AnnualDiscountAchievement
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AnnualDiscountAchievement), "VechileColor.ID", MatchType.Exact, CType(Code, Integer)))

            Dim AnnualDiscountAchievementColl As ArrayList = m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias)
            If (AnnualDiscountAchievementColl.Count > 0) Then
                Return CType(AnnualDiscountAchievementColl(0), AnnualDiscountAchievement)
            End If
            Return New AnnualDiscountAchievement
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_AnnualDiscountAchievementMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(AnnualDiscountAchievement), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AnnualDiscountAchievementMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(AnnualDiscountAchievement), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_AnnualDiscountAchievementMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _AnnualDiscountAchievement As ArrayList = m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias)
            Return _AnnualDiscountAchievement
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim PKHeaderColl As ArrayList = m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return PKHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(AnnualDiscountAchievement), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim AnnualDiscountAchievementColl As ArrayList = m_AnnualDiscountAchievementMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return AnnualDiscountAchievementColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim PKHeaderColl As ArrayList = m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return PKHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AnnualDiscountAchievement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim AnnualDiscountAchievementColl As ArrayList = m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(AnnualDiscountAchievement), columnName, matchOperator, columnValue))
            Return AnnualDiscountAchievementColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(AnnualDiscountAchievement), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PKHeader), columnName, matchOperator, columnValue))

            Return m_AnnualDiscountAchievementMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Sub Insert(ByVal objDomain As AnnualDiscountAchievement)
            Dim iReturn As Integer = -2
            Try
                m_AnnualDiscountAchievementMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try

        End Sub

        Public Sub Update(ByVal objDomain As AnnualDiscountAchievement)
            Try
                m_AnnualDiscountAchievementMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region

#Region "Custom Method"

        Private Function RetrieveAnnualDiscountAchivement(ByVal objAnnualDiscountAchievement As AnnualDiscountAchievement, ByVal headerID As Integer) As AnnualDiscountAchievement
            Dim _AnnualDiscountAchievementFacade As AnnualDiscountAchievementFacade
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "AnnualDiscountAchievementHeader.ID", MatchType.Exact, headerID))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscountAchievement), "MaterialCode", MatchType.Exact, objAnnualDiscountAchievement.MaterialCode))
            _AnnualDiscountAchievementFacade = New AnnualDiscountAchievementFacade(System.Threading.Thread.CurrentPrincipal)
            Dim collAnnualDiscount As ArrayList = _AnnualDiscountAchievementFacade.Retrieve(criterias)
            If collAnnualDiscount.Count > 0 Then
                Dim objADC As AnnualDiscountAchievement = CType(collAnnualDiscount(0), AnnualDiscountAchievement)
                objADC.MaterialCode = objAnnualDiscountAchievement.MaterialCode
                objADC.MaterialDescription = objAnnualDiscountAchievement.MaterialDescription
                objADC.Point = objAnnualDiscountAchievement.Point
                objADC.MinimumQty = objAnnualDiscountAchievement.MinimumQty
                objADC.BillQtyThisMonth = objAnnualDiscountAchievement.BillQtyThisMonth
                objADC.BillQtyThisPeriod = objAnnualDiscountAchievement.BillQtyThisPeriod
                objADC.RebateQtyThisPeriod = objAnnualDiscountAchievement.RebateQtyThisPeriod
                objADC.RebateAmountThisPeriod = objAnnualDiscountAchievement.RebateAmountThisPeriod
                objADC.RemainQty = objAnnualDiscountAchievement.RemainQty
                objADC.Semester = objAnnualDiscountAchievement.Semester
                Return objADC
            Else
                Return objAnnualDiscountAchievement
            End If
        End Function

        Public Sub SychronizeAnnualDiscountAchivement(ByVal objAnnualDiscountAchievement As AnnualDiscountAchievement, ByVal objHeader As AnnualDiscountAchievementHeader)
            objAnnualDiscountAchievement = RetrieveAnnualDiscountAchivement(objAnnualDiscountAchievement, objHeader.ID)
            Try
                If objAnnualDiscountAchievement.ID > 0 Then
                    Update(objAnnualDiscountAchievement)
                Else
                    objAnnualDiscountAchievement.AnnualDiscountAchievementHeader = objHeader
                    Insert(objAnnualDiscountAchievement)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub





#End Region

    End Class

End Namespace