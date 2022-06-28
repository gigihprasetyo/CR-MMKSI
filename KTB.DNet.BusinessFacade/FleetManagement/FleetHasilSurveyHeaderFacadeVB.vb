
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
'// Generated on 8/31/2017 - 1:35:35 PM
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

	public class FleetHasilSurveyHeaderFacade
		inherits AbstractFacade

#Region "Private Variables"

	Private m_userPrincipal As IPrincipal = Nothing
	Private m_FleetHasilSurveyHeaderMapper as IMapper
	
	Private	m_TransactionManager As TransactionManager
	
#end region

#region "Constructor"

Public Sub New(ByVal userPrincipal As IPrincipal)

	Me.m_userPrincipal = userPrincipal
	me.m_FleetHasilSurveyHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(FleetHasilSurveyHeader).ToString)
	
		
End Sub

#end region

#Region "Retrieve"

       Public Function Retrieve(ByVal ID as integer ) As FleetHasilSurveyHeader
            Return CType(m_FleetHasilSurveyHeaderMapper.Retrieve(ID), FleetHasilSurveyHeader)
       End Function
        
        
        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias)
        End Function
        
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

		Public Function RetrieveList() As ArrayList
            Return m_FleetHasilSurveyHeaderMapper.RetrieveList
        End Function
        
        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetHasilSurveyHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
           End If

            Return m_FleetHasilSurveyHeaderMapper.RetrieveList(sortColl)
        End Function
        
        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetHasilSurveyHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

			Return m_FleetHasilSurveyHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function
		
		Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetHasilSurveyHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FleetHasilSurveyHeader As ArrayList = m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias)
            Return _FleetHasilSurveyHeader
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As ArrayList
            Dim _FleetHasilSurveyHeader As ArrayList = m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias)
            Return _FleetHasilSurveyHeader
        End Function

        Public Function RetrieveByCriteria(ByVal Criterias As ICriteria, ByVal sortCollection As ICollection) As ArrayList
            Dim _FleetHasilSurveyHeader As ArrayList = m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(Criterias, sortCollection)
            Return _FleetHasilSurveyHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetHasilSurveyHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FleetHasilSurveyHeaderColl As ArrayList = m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FleetHasilSurveyHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FleetHasilSurveyHeaderColl As ArrayList = m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return FleetHasilSurveyHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetHasilSurveyHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetHasilSurveyHeader), columnName, matchOperator, columnValue))

            Dim FleetHasilSurveyHeaderColl As ArrayList = m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return FleetHasilSurveyHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetHasilSurveyHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetHasilSurveyHeader), columnName, matchOperator, columnValue))

            Return m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        Public Function Insert(ByVal objDomain As FleetHasilSurveyHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_FleetHasilSurveyHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FleetHasilSurveyHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FleetHasilSurveyHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As FleetHasilSurveyHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FleetHasilSurveyHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As FleetHasilSurveyHeader)
            Try
                m_FleetHasilSurveyHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Public Function ValidateSurveyDate(ByVal SurveyDate As DateTime, ByVal fleetCustomerID As Integer, Optional ByVal fleetHasilSurveyHeaderID As Integer = 0) As String
            Dim str As String = String.Empty
            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetHasilSurveyHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crt.opAnd(New Criteria(GetType(FleetHasilSurveyHeader), "FleetCustomerID", MatchType.Exact, CInt(fleetCustomerID)))

            Dim arrList As ArrayList = m_FleetHasilSurveyHeaderMapper.RetrieveByCriteria(crt)
            If arrList.Count > 0 Then

                If fleetHasilSurveyHeaderID = 0 Then
                    ' jika saat insert
                    For Each item As FleetHasilSurveyHeader In arrList
                        If item.SurveyDate = SurveyDate Then
                            str = "Tanggal visitasi sudah ada!"
                        End If
                        If item.SurveyDate > SurveyDate Then
                            If str = String.Empty Then
                                str = "Tanggal visitasi harus lebih dari tanggal visitasi sebelumnya(" & item.SurveyDate.ToString("dd MMM yyyy") & ")!"
                            Else
                                str += "\nTanggal visitasi harus lebih dari tanggal visitasi sebelumnya(" & item.SurveyDate.ToString("dd MMM yyyy") & ")!"
                            End If

                        End If
                    Next
                Else
                    ' jika saat edit
                    For Each item As FleetHasilSurveyHeader In arrList
                        If item.ID = fleetHasilSurveyHeaderID Then
                            Continue For
                        End If

                        If item.SurveyDate = SurveyDate Then
                            str = "Tanggal visitasi sudah ada!"
                        End If

                        If item.SurveyDate > SurveyDate Then
                            If str = String.Empty Then
                                str = "Tanggal visitasi harus lebih dari tanggal visitasi sebelumnya(" & item.SurveyDate.ToString("dd MMM yyyy") & ")!"
                            Else
                                str += "\nTanggal visitasi harus lebih dari tanggal visitasi sebelumnya(" & item.SurveyDate.ToString("dd MMM yyyy") & ")!"
                            End If
                        End If
                    Next
                End If

            End If

            Return str
        End Function

#End Region

    End Class
	
end namespace

