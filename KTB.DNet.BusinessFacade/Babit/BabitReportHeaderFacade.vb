
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
'// Generated on 12/09/2019 - 14:23:37
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

    Public Class BabitReportHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BabitReportHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BabitReportHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(BabitReportHeader).ToString)


        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As BabitReportHeader
            Return CType(m_BabitReportHeaderMapper.Retrieve(ID), BabitReportHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As BabitReportHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitReportHeader), "BabitReportHeaderCode", MatchType.Exact, Code))

            Dim BabitReportHeaderColl As ArrayList = m_BabitReportHeaderMapper.RetrieveByCriteria(criterias)
            If (BabitReportHeaderColl.Count > 0) Then
                Return CType(BabitReportHeaderColl(0), BabitReportHeader)
            End If
            Return New BabitReportHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BabitReportHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BabitReportHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BabitReportHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitReportHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BabitReportHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _BabitReportHeader As ArrayList = m_BabitReportHeaderMapper.RetrieveByCriteria(criterias)
            Return _BabitReportHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitReportHeaderColl As ArrayList = m_BabitReportHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BabitReportHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(BabitReportHeader), SortColumn, sortDirection))
            Dim BabitReportHeaderColl As ArrayList = m_BabitReportHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return BabitReportHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BabitReportHeaderColl As ArrayList = m_BabitReportHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BabitReportHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BabitReportHeaderColl As ArrayList = m_BabitReportHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(BabitReportHeader), columnName, matchOperator, columnValue))
            Return BabitReportHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(BabitReportHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportHeader), columnName, matchOperator, columnValue))

            Return m_BabitReportHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitReportHeader), "BabitReportHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(BabitReportHeader), "BabitReportHeaderCode", AggregateType.Count)
            Return CType(m_BabitReportHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As BabitReportHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_BabitReportHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As BabitReportHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_BabitReportHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As BabitReportHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_BabitReportHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As BabitReportHeader)
            Try
                m_BabitReportHeaderMapper.Delete(objDomain)
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
            Return m_BabitReportHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function DoRetrieveDataSet(ByVal _BabitHeaderID As Integer) As DataSet
            Dim strSql As String = String.Empty
            strSql = "select BabitHeaderID, sum(SubsidyAmount) as SumSubsidyAmount  "
            strSql += "from BabitDealerAllocation "
            strSql += "where RowStatus = 0 "
            strSql += "and BabitHeaderID = " & _BabitHeaderID & " "
            strSql += "group by BabitHeaderID "
            Return m_BabitReportHeaderMapper.RetrieveDataSet(strSql)
        End Function

        Public Function DoRetrieveDataSetBySPK(ByVal strBabitRegNumber As String) As ArrayList
            Dim _strSQL As String = String.Empty
            Dim ds As DataSet

            _strSQL = "select "
            _strSQL += "ROW_NUMBER() OVER(ORDER BY BabitRegNumber ASC) AS ROWNum, "
            _strSQL += "ID, BabitRegNumber, VechileTypeName, QtyUnit "
            _strSQL += "from ( "
            _strSQL += "select "
            _strSQL += "a.ID, "
            _strSQL += "a.BabitRegNumber, "
            _strSQL += "VechileTypeName = i.Name, "
            _strSQL += "QtyUnit = Sum(e.Quantity) "
            _strSQL += "from BabitHeader a "
            _strSQL += "join SAPCustomer b on a.BabitRegNumber = b.CampaignName and b.RowStatus = 0 "
            _strSQL += "join SPKCustomer c on c.SAPCustomerID = b.ID and c.RowStatus = 0 "
            _strSQL += "join SPKHeader d on d.SPKCustomerID = c.ID and d.RowStatus = 0 "
            _strSQL += "join SPKDetail e on e.SPKHeaderID = d.ID and e.RowStatus = 0 "
            _strSQL += "join VechileColor f on e.VehicleColorID = f.ID and f.RowStatus = 0 "
            _strSQL += "join VechileType g on g.ID = f.VechileTypeID and g.RowStatus = 0 "
            _strSQL += "join SubCategoryVehicleToModel h on g.ModelID = h.VechileModelID and h.RowStatus = 0 "
            _strSQL += "join SubCategoryVehicle i on i.ID = h.SubCategoryVehicleID and e.CategoryID in (1,2) and i.RowStatus = 0 "
            _strSQL += "where a.rowstatus = 0 "
            _strSQL += "group by a.id, a.BabitRegNumber, i.Name "
            _strSQL += ") a "
            _strSQL += "where a.BabitRegNumber = '" & strBabitRegNumber & "'"

            ds = m_BabitReportHeaderMapper.RetrieveDataSet(_strSQL)

            Dim tblBH As DataTable = ds.Tables(0)
            Dim arrlist As New ArrayList()
            For Each dr As DataRow In tblBH.Rows
                Dim oBabitHeader As New BabitHeader
                oBabitHeader.ID = dr("ID")
                oBabitHeader.BabitRegNumber = dr("BabitRegNumber")
                oBabitHeader.VechileTypeName = dr("VechileTypeName")
                oBabitHeader.QtyUnit = dr("QtyUnit")
                arrlist.Add(oBabitHeader)
            Next

            Return arrlist
        End Function

        Function DeleteTransaction(ByVal oBabitReportHeader As BabitReportHeader, ByVal arrBRDetail As ArrayList, ByVal arrBRDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrBRDetail.Count > 0 Then
                        For Each oBabitReportEventDetail As BabitReportEventDetail In arrBRDetail
                            m_TransactionManager.AddDelete(oBabitReportEventDetail)
                        Next
                    End If

                    If arrBRDoc.Count > 0 Then
                        For Each oBabitReportDocument As BabitReportDocument In arrBRDoc
                            m_TransactionManager.AddDelete(oBabitReportDocument)
                        Next
                    End If
                    m_TransactionManager.AddDelete(oBabitReportHeader)

                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnValue
        End Function

#End Region

    End Class

End Namespace

