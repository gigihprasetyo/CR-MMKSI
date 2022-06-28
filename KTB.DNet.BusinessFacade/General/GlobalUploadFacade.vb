
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
'// Generated on 10/07/2019 - 10:11:23
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Reflection

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class GlobalUploadFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_GlobalUploadMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_GlobalUploadMapper = MapperFactory.GetInstance.GetMapper(GetType(GlobalUpload).ToString)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As GlobalUpload
            Return CType(m_GlobalUploadMapper.Retrieve(ID), GlobalUpload)
        End Function

        Public Function Retrieve(ByVal Code As String) As GlobalUpload
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(GlobalUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(GlobalUpload), "Code", MatchType.Exact, Code))

            Dim GlobalUploadColl As ArrayList = m_GlobalUploadMapper.RetrieveByCriteria(criterias)
            If (GlobalUploadColl.Count > 0) Then
                Return CType(GlobalUploadColl(0), GlobalUpload)
            End If
            Return New GlobalUpload
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_GlobalUploadMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_GlobalUploadMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_GlobalUploadMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(GlobalUpload), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_GlobalUploadMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(GlobalUpload), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_GlobalUploadMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(GlobalUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _GlobalUpload As ArrayList = m_GlobalUploadMapper.RetrieveByCriteria(criterias)
            Return _GlobalUpload
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(GlobalUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim GlobalUploadColl As ArrayList = m_GlobalUploadMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return GlobalUploadColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(GlobalUpload), SortColumn, sortDirection))
            Dim GlobalUploadColl As ArrayList = m_GlobalUploadMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return GlobalUploadColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(GlobalUpload), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim GlobalUploadColl As ArrayList = m_GlobalUploadMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return GlobalUploadColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim GlobalUploadColl As ArrayList = m_GlobalUploadMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return GlobalUploadColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(GlobalUpload), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim GlobalUploadColl As ArrayList = m_GlobalUploadMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(GlobalUpload), columnName, matchOperator, columnValue))
            Return GlobalUploadColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(GlobalUpload), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(GlobalUpload), columnName, matchOperator, columnValue))

            Return m_GlobalUploadMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(GlobalUpload), "TrMRTC.Code", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(GlobalUpload), "TrMRTC.Code", AggregateType.Count)
        '    Return CType(m_GlobalUploadMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function Insert(ByVal objDomain As GlobalUpload) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_GlobalUploadMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As GlobalUpload) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_GlobalUploadMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As GlobalUpload)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_GlobalUploadMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As GlobalUpload)
            Try
                m_GlobalUploadMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"

        Public Function UploadTemplate(ByVal objDomain As GlobalUpload, ByVal fileName As String) As String
            Try
                Dim parserClass As Type = Type.GetType(objDomain.ParserName & ",KTB.DNet.Parser")

                Dim parserConstructor As ConstructorInfo = parserClass.GetConstructor(Type.EmptyTypes)
                Dim parser As Object = parserConstructor.Invoke(New Object() {})

                Dim uploadMethod As MethodInfo = parserClass.GetMethod(objDomain.UploadMethodName)
                Dim uploadResult As Object = uploadMethod.Invoke(parser, New Object() {fileName, String.Empty, m_userPrincipal.Identity.Name})

                Return "Success"
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function

        Public Function GetTemplateExcel(ByVal objDomain As GlobalUpload) As DataTable
            Dim facadeType As Type = GetFacadeType(objDomain)
            Dim facade As Object = GetFacade(objDomain)
            Dim downloadMethod As MethodInfo = facadeType.GetMethod(objDomain.DownloadMethodName)
            Dim downloadValue As Object = downloadMethod.Invoke(facade, New Object() {})

            Return CType(downloadValue, DataTable)
        End Function

        Private Function GetFacadeType(ByVal objDomain As GlobalUpload) As Type
            Dim facadeClassName As String = objDomain.FacadeName & ",KTB.DNet.BusinessFacade"
            Dim facadeType As Type = Type.GetType(facadeClassName)

            Return facadeType
        End Function

        Private Function GetFacade(ByVal objDomain As GlobalUpload) As Object

            Dim facadeType As Type = GetFacadeType(objDomain)

            Dim types As Type() = New Type(0) {}
            types(0) = GetType(System.Security.Principal.IPrincipal)

            Dim facadeConstructor As ConstructorInfo = facadeType.GetConstructor(types)
            Dim facade As Object = facadeConstructor.Invoke(New Object() {m_userPrincipal})

            Return facade
        End Function

        Private Function GetParser(ByVal uploadProfile As GlobalUpload) As Object

        End Function

#End Region

    End Class

End Namespace

