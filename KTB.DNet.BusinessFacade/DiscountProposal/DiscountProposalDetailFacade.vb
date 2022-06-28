
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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:50:25
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

    Public Class DiscountProposalDetailFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DiscountProposalDetailMapper As IMapper


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_DiscountProposalDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(DiscountProposalDetail).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As DiscountProposalDetail
            Return CType(m_DiscountProposalDetailMapper.Retrieve(ID), DiscountProposalDetail)
        End Function

        Public Function Retrieve(ByVal Code As String) As DiscountProposalDetail
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DiscountProposalDetail), "DiscountProposalDetailCode", MatchType.Exact, Code))

            Dim DiscountProposalDetailColl As ArrayList = m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias)
            If (DiscountProposalDetailColl.Count > 0) Then
                Return CType(DiscountProposalDetailColl(0), DiscountProposalDetail)
            End If
            Return New DiscountProposalDetail
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DiscountProposalDetailMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalDetailMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DiscountProposalDetailMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DiscountProposalDetail As ArrayList = m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias)
            Return _DiscountProposalDetail
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalDetailColl As ArrayList = m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DiscountProposalDetailColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DiscountProposalDetail), SortColumn, sortDirection))
            Dim DiscountProposalDetailColl As ArrayList = m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DiscountProposalDetailColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DiscountProposalDetailColl As ArrayList = m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DiscountProposalDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DiscountProposalDetailColl As ArrayList = m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(DiscountProposalDetail), columnName, matchOperator, columnValue))
            Return DiscountProposalDetailColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(DiscountProposalDetail), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetail), columnName, matchOperator, columnValue))

            Return m_DiscountProposalDetailMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalDetail), "DiscountProposalDetailCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(DiscountProposalDetail), "DiscountProposalDetailCode", AggregateType.Count)
            Return CType(m_DiscountProposalDetailMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As DiscountProposalDetail) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_DiscountProposalDetailMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As DiscountProposalDetail) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_DiscountProposalDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As DiscountProposalDetail)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_DiscountProposalDetailMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As DiscountProposalDetail)
            Try
                m_DiscountProposalDetailMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveByHeaderID(ByRef strProposalRegNo As String, ByVal selectedCategory As Integer, ByVal selectedSubCategory As Integer) As ArrayList
            Dim result As ArrayList = New ArrayList
            Dim dtbs As New DataSet

            Dim strCommand As String = String.Empty
            strCommand = "SELECT "
            strCommand += " a.ProposalRegNo [NoReg Aplikasi], "
            strCommand += " o.Name [Model], "
            strCommand += " k.Name [Tipe], "
            strCommand += " j.ColorIndName [Warna], "
            strCommand += " h.AssyYear [AssyYear], "
            strCommand += " h.ProposeQty [Unit], "
            strCommand += " l.DiscountRequest [PermohonanDiskon], "
            strCommand += " p.[DiskonDisetujui] "
            strCommand += "FROM DiscountProposalHeader a with(nolock) "
            strCommand += "LEFT JOIN DiscountProposalDetail h on a.ID = h.DiscountProposalHeaderID "
            strCommand += "LEFT JOIN VechileColorIsActiveOnPK i on h.VechileColorIsActiveOnPKID = i.ID "
            strCommand += "LEFT JOIN VechileColor j on i.VehicleColorID = j.ID "
            strCommand += "LEFT JOIN VechileTypeGeneral k on i.VechileTypeGeneralID = k.ID "
            strCommand += "LEFT JOIN DiscountproposaldetailPrice l on h.ID = l.DiscountProposalDetailID "
            strCommand += "LEFT JOIN VechileType m on j.VechileTypeID = m.ID "
            strCommand += "LEFT JOIN SubCategoryVehicleToModel n on m.ModelID = n.VechileModelID "
            strCommand += "LEFT JOIN SubCategoryVehicle o on n.SubCategoryVehicleID = o.ID "
            strCommand += "OUTER APPLY( "
            strCommand += " SELECT SUM(y.DiscountApproved) AS [DiskonDisetujui] "
            strCommand += " FROM DiscountProposalDetailApproval x "
            strCommand += " LEFT JOIN DiscountProposalDetailApprovaltoSPL y on x.ID = y.DiscountProposalDetailApprovalID "
            strCommand += " WHERE "
            strCommand += " x.RowStatus = 0 "
            strCommand += " and y.RowStatus = 0 "
            strCommand += " and x.DiscountProposalHeaderID = a.ID "
            strCommand += " and j.VechileTypeID = x.VechileTypeID "
            strCommand += ") p "
            strCommand += "where "
            strCommand += "a.RowStatus = 0 "
            strCommand += "and h.RowStatus = 0 "
            strCommand += "and i.RowStatus = 0 "
            strCommand += "and j.RowStatus = 0 "
            strCommand += "and k.RowStatus = 0 "
            strCommand += "and l.RowStatus = 0 "
            strCommand += "and m.RowStatus = 0 "
            strCommand += "and n.RowStatus = 0 "
            strCommand += "and o.RowStatus = 0 "
            strCommand += "and o.CategoryID = (case when " & selectedCategory & "=-1 then o.CategoryID else " & selectedCategory & " end) "
            strCommand += "and o.ID = (case when " & selectedSubCategory & "=-1 then o.ID else " & selectedSubCategory & " end) "
            strCommand += "and a.ProposalRegNo = '" & strProposalRegNo & "'" 'sesuai dengan nomor prposal headernya
            strCommand += "group by "
            strCommand += "a.ProposalRegNo, "
            strCommand += "k.Name , "
            strCommand += "h.ProposeQty , "
            strCommand += "j.ColorIndName, "
            strCommand += "p.[DiskonDisetujui], "
            strCommand += "l.DiscountRequest, "
            strCommand += "o.Name, "
            strCommand += "h.AssyYear "

            dtbs = m_DiscountProposalDetailMapper.RetrieveDataSet(strCommand)
            For Each dr As DataRow In dtbs.Tables(0).Rows
                result.Add(dr)
            Next
            Return result
        End Function
#End Region

    End Class

End Namespace

