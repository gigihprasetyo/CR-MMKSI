#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2009

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
'// Copyright © 2009
'// ---------------------
'// $History      : $
'// Generated on 8/25/2009 - 10:53:00 AM
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
    Public Class FleetGradeDiscountFacade

#Region "Private Variables"
        Private m_FleetGradeDiscountMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "Constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FleetGradeDiscountMapper = MapperFactory.GetInstance().GetMapper(GetType(FleetGradeDiscount).ToString)
        End Sub
#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FleetGradeDiscount
            Return CType(m_FleetGradeDiscountMapper.Retrieve(ID), FleetGradeDiscount)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FleetGradeDiscount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim _FleetGradeDiscountColl As ArrayList = m_FleetGradeDiscountMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return _FleetGradeDiscountColl
        End Function

        Public Function RetrieveByGroupCode(ByVal intGroupCode As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetGradeDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetGradeDiscount), "VehicleType", MatchType.Exact, intGroupCode))
            Dim sorts As New SortCollection
            sorts.Add(New Sort(GetType(FleetGradeDiscount), "Grade", Sort.SortDirection.ASC))
            Dim lists As ArrayList = m_FleetGradeDiscountMapper.RetrieveByCriteria(criterias, sorts)
            Return lists
        End Function

        Public Function SearchGradeAndVehicleType(ByVal strGrade As String, ByVal strVehicleType As String, ByVal intID As Integer, ByVal strMode As String) As Boolean
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FleetGradeDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FleetGradeDiscount), "Grade", MatchType.Exact, strGrade))
            criterias.opAnd(New Criteria(GetType(FleetGradeDiscount), "VehicleType", MatchType.Exact, strVehicleType.Trim))
            If strMode = "Edit" Then
                If intID <> 0 Then
                    criterias.opAnd(New Criteria(GetType(FleetGradeDiscount), "ID", MatchType.No, intID))
                End If
            End If
            Dim lists As ArrayList = m_FleetGradeDiscountMapper.RetrieveByCriteria(criterias)
            If Not IsNothing(lists) AndAlso lists.Count > 0 Then
                Return True
            End If
            Return False
        End Function
#End Region


#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FleetGradeDiscount) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_FleetGradeDiscountMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FleetGradeDiscount) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FleetGradeDiscountMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As FleetGradeDiscount) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_FleetGradeDiscountMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As FleetGradeDiscount)
            Try
                m_FleetGradeDiscountMapper.Delete(objDomain)
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