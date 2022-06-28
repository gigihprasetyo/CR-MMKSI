
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
'// Copyright  2019
'// ---------------------
'// $History      : $
'// Generated on 20/06/2019 - 8:27:09
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
Imports KTB.DNET.BusinessFacade.General

#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class BabitMasterRetailTargetFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitMasterRetailTargetMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitMasterRetailTargetMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitMasterRetailTarget).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitMasterRetailTarget
            Return CType(m_BabitMasterRetailTargetMapper.Retrieve(ID), BabitMasterRetailTarget)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitMasterRetailTarget
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitMasterRetailTarget), "BabitMasterRetailTargetCode", MatchType.Exact, Code))

            Dim BabitMasterRetailTargetColl As ArrayList = m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias)
            If (BabitMasterRetailTargetColl.Count > 0) Then
                Return CType(BabitMasterRetailTargetColl(0), BabitMasterRetailTarget)
            End If
            Return New BabitMasterRetailTarget
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitMasterRetailTargetMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMasterRetailTarget), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMasterRetailTargetMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMasterRetailTarget), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitMasterRetailTargetMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitMasterRetailTarget As ArrayList = m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias)
            Return _BabitMasterRetailTarget
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitMasterRetailTargetColl As ArrayList = m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitMasterRetailTargetColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitMasterRetailTarget), SortColumn, sortDirection))
            Dim BabitMasterRetailTargetColl As ArrayList = m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitMasterRetailTargetColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitMasterRetailTargetColl As ArrayList = m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitMasterRetailTargetColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterRetailTarget), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitMasterRetailTargetColl As ArrayList = m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitMasterRetailTarget), columnName, matchOperator, columnValue))
            Return BabitMasterRetailTargetColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitMasterRetailTarget), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterRetailTarget), columnName, matchOperator, columnValue))

            Return m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveFromSPAlloc(ByVal strDealerCode As String, ByVal strYearPeriod As String, ByVal strMonthPeriod As String, ByVal intStatus As Integer) As ArrayList
            Dim arrBMRT As New ArrayList
            Dim _strSQL As String = "EXEC [up_RetrieveAllocBabitMasterRetailTarget]"
            _strSQL += " @DealerCode = '" & strDealerCode & "',"
            _strSQL += " @YearPeriod = '" & strYearPeriod & "',"
            _strSQL += " @MonthPeriod = '" & strMonthPeriod & "',"
            _strSQL += " @Status = " & intStatus
            Dim dsBMRT As DataSet = m_BabitMasterRetailTargetMapper.RetrieveDataSet(_strSQL)

            Dim objBabitMasterRetailTarget As New BabitMasterRetailTarget
            Dim arrBabitEvent As New ArrayList
            Dim row As DataRow
            Dim i As Integer = 0
            For i = 0 To dsBMRT.Tables(0).Rows.Count - 1
                row = dsBMRT.Tables(0).Rows(i)
                Try
                    objBabitMasterRetailTarget = New BabitMasterRetailTarget
                    objBabitMasterRetailTarget.ID = row("ID")
                    objBabitMasterRetailTarget.Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(CInt(row("DealerID")))
                    If Not IsDBNull(row("DealerBranchID")) Then
                        objBabitMasterRetailTarget.DealerBranch = New DealerBranchFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(CInt(row("DealerBranchID")))
                    End If
                    objBabitMasterRetailTarget.CategoryID = CInt(row("CategoryID"))
                    objBabitMasterRetailTarget.SubCategoryVehicle = New SubCategoryVehicleFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)).Retrieve(CType(row("SubCategoryVehicleID"), Short))
                    objBabitMasterRetailTarget.MonthPeriod = row("MonthPeriod")
                    objBabitMasterRetailTarget.YearPeriod = row("YearPeriod")
                    objBabitMasterRetailTarget.RetailTarget = row("RetailTarget")
                    objBabitMasterRetailTarget.Status = CInt(row("Status"))
                    objBabitMasterRetailTarget.BabitMasterPriceID = row("BabitMasterPriceID")
                    objBabitMasterRetailTarget.ValidFrom = Format(row("ValidFrom"), "dd/MM/yyyy")
                    objBabitMasterRetailTarget.ValidTo = Format(row("ValidTo"), "dd/MM/yyyy")
                    objBabitMasterRetailTarget.UnitPrice = row("UnitPrice")
                    objBabitMasterRetailTarget.SpecialCategoryFlag = row("SpecialCategoryFlag")
                    objBabitMasterRetailTarget.SpecialFlag = row("SpecialFlag")

                    arrBMRT.Add(objBabitMasterRetailTarget)

                Catch ex As Exception
                End Try
            Next

            Return arrBMRT
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitMasterRetailTarget), "BabitMasterRetailTargetCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitMasterRetailTarget), "BabitMasterRetailTargetCode", AggregateType.Count)
            Return CType(m_BabitMasterRetailTargetMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitMasterRetailTarget) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitMasterRetailTargetMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitMasterRetailTarget) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitMasterRetailTargetMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitMasterRetailTarget)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitMasterRetailTargetMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitMasterRetailTarget)
            Try
                m_BabitMasterRetailTargetMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitMasterRetailTargetMapper.RetrieveByCriteria(criterias, sorts)
        End Function
#End Region

    End Class

End Namespace

