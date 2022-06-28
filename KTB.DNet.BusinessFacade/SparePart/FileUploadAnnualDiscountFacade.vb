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
Imports KTB.Dnet.DataMapper
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.Dnet.BusinessFacade.SparePart

    Public Class FileUploadAnnualDiscountFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_FileUploadAnnualDiscountMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FileUploadAnnualDiscountMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount).ToString)
            'Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.FileUploadAnnualDiscount))
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveListValidateFrom() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Me.m_FileUploadAnnualDiscountMapper = MapperFactory.GetInstance.GetMapper(GetType(FileUploadAnnualDiscount).ToString)
            Return m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As FileUploadAnnualDiscount
            Return CType(m_FileUploadAnnualDiscountMapper.Retrieve(ID), FileUploadAnnualDiscount)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FileUploadAnnualDiscount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias, sortColl)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FileUploadAnnualDiscountMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(FileUploadAnnualDiscount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FileUploadAnnualDiscountMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(FileUploadAnnualDiscount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FileUploadAnnualDiscountMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FileUploadAnnualDiscount As ArrayList = m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias)
            Return _FileUploadAnnualDiscount
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FileUploadAnnualDiscountColl As ArrayList = m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FileUploadAnnualDiscountColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
        ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FileUploadAnnualDiscount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FileUploadAnnualDiscount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim FileUploadAnnualDiscountColl As ArrayList = m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return FileUploadAnnualDiscountColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FileUploadAnnualDiscountColl As ArrayList = m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return FileUploadAnnualDiscountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FileUploadAnnualDiscountColl As ArrayList = m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(FileUploadAnnualDiscount), columnName, matchOperator, columnValue))
            Return FileUploadAnnualDiscountColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(FileUploadAnnualDiscount), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), columnName, matchOperator, columnValue))

            Return m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FileUploadAnnualDiscount) As Integer
            Dim iReturn As Integer = -2
            Try
                m_FileUploadAnnualDiscountMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FileUploadAnnualDiscount) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FileUploadAnnualDiscountMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As FileUploadAnnualDiscount)
            Try
                m_FileUploadAnnualDiscountMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub
#End Region

#Region "Custom Method"
        Public Function ValidateValueFileName(ByVal _FileName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            crit.opAnd(New Criteria(GetType(FileUploadAnnualDiscount), "FileName", MatchType.Exact, _FileName))

            Dim agg As Aggregate = New Aggregate(GetType(FileUploadAnnualDiscount), "ID", AggregateType.Count)

            Return CType(m_FileUploadAnnualDiscountMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function ValidateValueProgramName(ByVal _ProgramName As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            crit.opAnd(New Criteria(GetType(FileUploadAnnualDiscount), "ProgramName", MatchType.Exact, _ProgramName))

            Dim agg As Aggregate = New Aggregate(GetType(FileUploadAnnualDiscount), "ID", AggregateType.Count)

            Return CType(m_FileUploadAnnualDiscountMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function RetrieveByFileName(ByVal _filename As String) As FileUploadAnnualDiscount
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FileUploadAnnualDiscount), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
            criterias.opAnd(New Criteria(GetType(FileUploadAnnualDiscount), "FileName", MatchType.Exact, _filename))

            Dim Coll As ArrayList = m_FileUploadAnnualDiscountMapper.RetrieveByCriteria(criterias)
            If (Coll.Count > 0) Then
                Return CType(Coll(0), FileUploadAnnualDiscount)
            End If
            Return New FileUploadAnnualDiscount
        End Function
#End Region

    End Class

End Namespace