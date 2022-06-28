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
'// Generated on 4/27/2021 - 11:29:26 AM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.Framework
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

namespace KTB.DNET.BusinessFacade

	public class SPKNationalEventFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_SPKNationalEventMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_SPKNationalEventMapper = MapperFactory.GetInstance.GetMapper(GetType(SPKNationalEvent).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As SPKNationalEvent
            Return CType(m_SPKNationalEventMapper.Retrieve(ID), SPKNationalEvent)
       End Function
        
        Public Function Retrieve(ByVal Code As String) As SPKNationalEvent
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPKNationalEvent), "SPKNationalEventCode", MatchType.Exact, Code))

			Dim SPKNationalEventColl As ArrayList = m_SPKNationalEventMapper.RetrieveByCriteria(criterias)
            If (SPKNationalEventColl.Count > 0) Then
                Return CType(SPKNationalEventColl(0), SPKNationalEvent)
            End If
            Return New SPKNationalEvent
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPKNationalEventMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPKNationalEventMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_SPKNationalEventMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKNationalEvent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_SPKNationalEventMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKNationalEvent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_SPKNationalEventMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPKNationalEvent As ArrayList = m_SPKNationalEventMapper.RetrieveByCriteria(criterias)
            Return _SPKNationalEvent
        End Function

		Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKNationalEventColl As ArrayList = m_SPKNationalEventMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

			Return SPKNationalEventColl
        End Function
		
		 Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(SPKNationalEvent), SortColumn, sortDirection))
            Dim  SPKNationalEventColl As ArrayList = m_SPKNationalEventMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
           Return SPKNationalEventColl
        End Function
		

		Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPKNationalEventColl As ArrayList = m_SPKNationalEventMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPKNationalEventColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKNationalEventColl As ArrayList = m_SPKNationalEventMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SPKNationalEvent), columnName, matchOperator, columnValue))
            Return SPKNationalEventColl
        End Function

		Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKNationalEvent), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), columnName, matchOperator, columnValue))

            Return m_SPKNationalEventMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#end Region

#Region "Transaction/Other Public Method"

		Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), "SPKNationalEventCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SPKNationalEvent), "SPKNationalEventCode", AggregateType.Count)
            Return CType(m_SPKNationalEventMapper.RetrieveScalar(agg, crit), Integer)
        End Function
		
		Public Function Insert(ByVal objDomain As  SPKNationalEvent) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_SPKNationalEventMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As SPKNationalEvent) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPKNationalEventMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As SPKNationalEvent)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPKNationalEventMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As SPKNationalEvent)
            Try
                m_SPKNationalEventMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
        
#End Region

#Region "Custom Method"
        Public Function RetrieveReportSPKNationalEventBySP(ByVal intDealerID As Integer, ByVal strRegNumber As String, ByRef dealerCodeChampion As String, ByRef dealerCodeWithlowestDO As String) As ArrayList
            Dim arrReportSPK As New ArrayList
            Dim _strSQL As String = "EXEC [SP_RetrieveReportSPKNationalEvent]"
            _strSQL += " @DealerID = " & intDealerID & ", "
            _strSQL += " @RegNumber = '" & strRegNumber & "'"
            Dim dsReportSPK As DataSet = m_SPKNationalEventMapper.RetrieveDataSet(_strSQL)

            Dim objSPKNationalEventReport As New SPKNationalEventReport
            Dim row As DataRow
            Dim i As Integer = 0

            dealerCodeChampion = dsReportSPK.Tables(1).Rows(0)("DealerCode")
            dealerCodeWithlowestDO = dsReportSPK.Tables(2).Rows(0)("DealerCode")

            For i = 0 To dsReportSPK.Tables(0).Rows.Count - 1
                row = dsReportSPK.Tables(0).Rows(i)
                Try
                    objSPKNationalEventReport = New SPKNationalEventReport
                    With objSPKNationalEventReport
                        .ID = row("ID")
                        .EventName = row("EventName")
                        .SPKNumber = row("SPKNumber")
                        .DealerSPKDate = Format(row("DealerSPKDate"), "dd/MM/yyyy")
                        .CustomerName = row("CustomerName")
                        .SalesName = row("SalesName")
                        .SalesCode = row("SalesCode")
                        .DealerName = row("DealerName")
                        .DealerCode = row("DealerCode")
                        .VehicleTypeCategory = row("VehicleTypeCategory")
                        .VehicleTypeName = row("VehicleTypeName")
                        .VechileColorName = row("VechileColorName")
                        .AssyYear = row("AssyYear")
                        .FakturDate = If(row("FakturDate") = "", CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime), Format(CDate(row("FakturDate")), "dd/MM/yyyy"))
                        .FakturNumber = row("FakturNumber")
                        .DownPayment = row("DownPayment")
                        .Quantity = row("Quantity")
                        .Remarks = row("Remarks")
                        .Shift = row("Shift")
                        .PaymentMethod = row("PaymentMethod")
                        .PaymentMethodID = row("PaymentMethodID")
                        .LeasingName = row("LeasingName")
                        .LeasingID = row("LeasingID")
                        .TypeInputSPK = row("TypeInputSPK")
                        .SPKNationalEventID = row("SPKNationalEventID")

                        .RowStatus = row("RowStatus")
                        .CreatedTime = row("CreatedTime")
                        .CreatedBy = row("CreatedBy")
                        .LastUpdateTime = row("LastUpdateTime")
                        .LastUpdateBy = row("LastUpdateBy")
                    End With

                    arrReportSPK.Add(objSPKNationalEventReport)

                Catch ex As Exception
                End Try
            Next

            Return arrReportSPK
        End Function

        Public Function RetrieveDataTableForSendMail() As DataTable
            Dim dt As New DataTable
            Dim ds As New DataSet
            Dim spName As String = "SP_NATIONALEVENT_GETBODYEMAILNOTIFICATION"
            ds = m_SPKNationalEventMapper.RetrieveDataSet(spName)
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
            Return dt
        End Function

#End Region
		
	end class
	
end namespace
