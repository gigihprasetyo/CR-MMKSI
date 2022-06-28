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
'// Author Name   : Ariwibawa
'// PURPOSE       : Facade for Page Leasing - Daftar Dokumen.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2009 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2009 - 11:26:00 AM
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

Namespace KTB.DNet.BusinessFacade.SPAF
    Public Class V_LeasingDaftarDokumenFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_V_LeasingDaftarDokumenMapper As IMapper
        Private m_SPAFDoc As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_V_LeasingDaftarDokumenMapper = MapperFactory.GetInstance().GetMapper(GetType(V_LeasingDaftarDokumen).ToString)
            Me.m_SPAFDoc = MapperFactory.GetInstance().GetMapper(GetType(SPAFDoc).ToString)
            Me.m_TransactionManager = New TransactionManager
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As V_LeasingDaftarDokumen
            Return CType(m_V_LeasingDaftarDokumenMapper.Retrieve(ID), V_LeasingDaftarDokumen)
        End Function



        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_V_LeasingDaftarDokumenMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarDokumen), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_LeasingDaftarDokumenMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarDokumen), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarDokumen), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_LeasingDaftarDokumenMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarDokumen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _V_LeasingDaftarKondisi As ArrayList = m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias)
            Return _V_LeasingDaftarKondisi
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarDokumen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarDokumen), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
            ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarDokumen), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarDokumen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarDokumen), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarDokumen), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(V_LeasingDaftarDokumen), columnName, matchOperator, columnValue))
            Dim V_LeasingDaftarKondisiColl As ArrayList = m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return V_LeasingDaftarKondisiColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(V_LeasingDaftarKondisi), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(V_LeasingDaftarDokumen), columnName, matchOperator, columnValue))

            Return m_V_LeasingDaftarDokumenMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Update(ByVal objDomain As V_LeasingDaftarDokumen) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPAFDoc.Update(MappingToSPAFDoc(objDomain), m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As V_LeasingDaftarDokumen) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_SPAFDoc.Update(MappingToSPAFDoc(objDomain), m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As V_LeasingDaftarDokumen) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SPAFDoc.Delete(MappingToSPAFDoc(objDomain))
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region

#Region "Custom Method"
        Private Function MappingToSPAFDoc(ByVal objDomain As V_LeasingDaftarDokumen) As SPAFDoc
            Dim objRealDomain As New SPAFDoc
            objRealDomain.AlasanPenolakan = objDomain.AlasanPenolakan
            objRealDomain.ChassisMaster = objDomain.ChassisMaster
            objRealDomain.CreatedBy = objDomain.CreatedBy
            objRealDomain.CreatedTime = objDomain.CreatedTime
            objRealDomain.CustomerName = objDomain.CustomerName
            objRealDomain.DateLetter = objDomain.DateLetter
            objRealDomain.Dealer = objDomain.Dealer
            objRealDomain.DealerLeasing = objDomain.DealerLeasing
            objRealDomain.DocType = objDomain.DocType
            objRealDomain.ErrorMessage = objDomain.ErrorMessage
            objRealDomain.ID = objDomain.ID
            objRealDomain.LastUpdateBy = objDomain.LastUpdateBy
            objRealDomain.LastUpdateTime = objDomain.LastUpdateTime
            objRealDomain.OrderDealer = objDomain.OrderDealer
            objRealDomain.PostingDate = objDomain.PostingDate
            objRealDomain.ReffLetter = objDomain.ReffLetter
            objRealDomain.RetailPrice = objDomain.RetailPrice
            objRealDomain.RowStatus = objDomain.RowStatus
            objRealDomain.SellingType = objDomain.SellingType
            objRealDomain.SPAF = objDomain.SPAF
            objRealDomain.Status = objDomain.Status
            objRealDomain.Subsidi = objDomain.Subsidi
            objRealDomain.TglSetuju = objDomain.TglSetuju
            objRealDomain.UploadBy = objDomain.UploadBy
            objRealDomain.UploadDate = objDomain.UploadDate
            objRealDomain.UploadFile = objDomain.UploadFile
            Return objRealDomain
        End Function
        Private Function GenerateDateCriteria(ByVal nDate As Date, ByVal startDate As Boolean) As DateTime
            Dim Hour As Integer
            Dim Minute As Integer
            Dim Second As Integer

            If startDate Then
                Hour = 0
                Minute = 0
                Second = 0
            Else
                Hour = 23
                Minute = 59
                Second = 59
            End If

            Return New DateTime(nDate.Year, nDate.Month, nDate.Day, Hour, Minute, Second)
        End Function
#End Region

    End Class
End Namespace