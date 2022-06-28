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
'// Copyright  2006
'// ---------------------
'// $History      : $
'// Generated on 1/6/2006 - 4:11:44 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.IO

#End Region

#Region "Custom Namespace"
Imports KTb.DNet.Domain
Imports KTb.DNet.Domain.Buletin
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Buletin

    Public Class BuletinFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_BuletinMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_BuletinMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.Buletin).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)

        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As KTB.DNet.Domain.Buletin
            Return CType(m_BuletinMapper.Retrieve(ID), KTB.DNet.Domain.Buletin)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.Buletin
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Buletin), "BuletinCode", MatchType.Exact, Code))

            Dim BuletinColl As ArrayList = m_BuletinMapper.RetrieveByCriteria(criterias)
            If (BuletinColl.Count > 0) Then
                Return CType(BuletinColl(0), KTB.DNet.Domain.Buletin)
            End If
            Return New KTB.DNet.Domain.Buletin
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_BuletinMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_BuletinMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_BuletinMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Buletin), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BuletinMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Buletin), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_BuletinMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _Buletin As ArrayList = m_BuletinMapper.RetrieveByCriteria(criterias)
            Return _Buletin
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BuletinColl As ArrayList = m_BuletinMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return BuletinColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim BuletinColl As ArrayList = m_BuletinMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return BuletinColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            ' modify code for sorting
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Buletin), sortColumn, sortDirection))
            Else
                sortColl = Nothing
                'sortColl.Add(New Sort(GetType(RecallCategory), "RecallRegNo", Sort.SortDirection.DESC))

            End If

            Dim RecallCategoryColl As ArrayList = m_BuletinMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return RecallCategoryColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim BuletinColl As ArrayList = m_BuletinMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Buletin), columnName, matchOperator, columnValue))
            Return BuletinColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Buletin), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), columnName, matchOperator, columnValue))

            Return m_BuletinMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Buletin), "BuletinCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.Buletin), "BuletinCode", AggregateType.Count)
            Return CType(m_BuletinMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objBuletin As KTB.DNet.Domain.Buletin) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objBuletin, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objBuletin.ID
                Catch ex As Exception
                    returnValue = -1
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

        Public Function Update(ByVal objBuletin As KTB.DNet.Domain.Buletin) As Integer
            Dim nUpdatedRow As Integer = -1
            Try
                nUpdatedRow = m_BuletinMapper.Update(objBuletin, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return -1
            End Try
            Return nUpdatedRow
        End Function

        Public Function DeleteFromDB(ByVal objDomain As KTB.DNet.Domain.Buletin, ByVal sPhysicalMainFolderPath As String) As Integer
            Dim nResult As Integer = -1
            Try
                ' get the filename from db
                Dim sFileName As String = objDomain.FileName.Trim()

                ' correct the path
                sPhysicalMainFolderPath = sPhysicalMainFolderPath.Trim()
                If sPhysicalMainFolderPath = "" Then
                    'sPhysicalMainFolderPath = App.path
                End If
                If Right(sPhysicalMainFolderPath, 1) <> "\" Then sPhysicalMainFolderPath = sPhysicalMainFolderPath & "\"

                ' set the folder path that the filename exist
                Dim sFilePath As String = ""

                If sFileName <> "" Then
                    Dim separator As String = "\"
                    sFilePath = Me.GetHierarchicalCategoryName(objDomain.BuletinCategory.ID, separator)
                End If

                ' and then added the filename
                sFilePath = sPhysicalMainFolderPath + sFilePath + "\" + sFileName

                ' delete data from db
                nResult = m_BuletinMapper.Delete(objDomain)

                ' if success and there is a file then do delete file
                If nResult <> -1 And sFileName <> "" Then
                    Dim fInfo As FileInfo = New FileInfo(sFilePath)
                    If fInfo.Exists Then
                        fInfo.Delete()
                    End If
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As KTB.DNet.Domain.Buletin, ByVal sPhysicalMainFolderPaths() As String) As Integer
            Dim nResult As Integer = -1
            Try
                ' get the filename from db
                Dim sFileName As String = objDomain.FileName.Trim()
                Dim lLengthArray As Integer = sPhysicalMainFolderPaths.Length - 1 : Dim i As Integer
                Dim sFilePaths(lLengthArray) As String

                Dim sSeparatePath As String = ""
                If sFileName <> "" Then
                    Dim separator As String = "\"
                    sSeparatePath = Me.GetHierarchicalCategoryName(objDomain.BuletinCategory.ID, separator)
                End If

                For i = sPhysicalMainFolderPaths.GetLowerBound(0) To sPhysicalMainFolderPaths.GetUpperBound(0)

                    ' correct the path
                    Dim sPhysicalMainFolderPath As String = sPhysicalMainFolderPaths(i).Trim()
                    'If sPhysicalMainFolderPath = "" Then
                    'sPhysicalMainFolderPath = App.path
                    'End If
                    If Right(sPhysicalMainFolderPath, 1) <> "\" Then sPhysicalMainFolderPath = sPhysicalMainFolderPath & "\"

                    ' set the folder path that the filename exist
                    sFilePaths(i) = ""

                    ' and then added the filename
                    sFilePaths(i) = sPhysicalMainFolderPath + sSeparatePath + "\" + sFileName
                Next

                ' delete data from db
                nResult = m_BuletinMapper.Delete(objDomain)

                ' if success and there is a file then do delete file
                If nResult <> -1 And sFileName <> "" Then
                    For Each sFilePath As String In sFilePaths
                        Dim fInfo As FileInfo = New FileInfo(sFilePath)
                        If fInfo.Exists Then
                            fInfo.Delete()
                        End If
                    Next
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.Buletin, ByVal sPhysicalMainFolderPath As String) As Integer
            Dim nResult As Integer = -1
            Try
                ' get the filename from db
                Dim sFileName As String = objDomain.FileName.Trim()

                ' correct the path
                sPhysicalMainFolderPath = sPhysicalMainFolderPath.Trim()
                If sPhysicalMainFolderPath = "" Then
                    'sPhysicalMainFolderPath = App.path
                End If
                If Right(sPhysicalMainFolderPath, 1) <> "\" Then sPhysicalMainFolderPath = sPhysicalMainFolderPath & "\"

                ' set the folder path that the filename exist
                Dim sFilePath As String = ""

                If sFileName <> "" Then
                    Dim separator As String = "\"
                    sFilePath = Me.GetHierarchicalCategoryName(objDomain.BuletinCategory.ID, separator)
                End If

                ' and then added the filename
                sFilePath = sPhysicalMainFolderPath + sFilePath + "\" + sFileName

                ' delete data from db
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_BuletinMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                ' if success and there is a file then do delete file
                If nResult <> -1 And sFileName <> "" Then
                    Dim fInfo As FileInfo = New FileInfo(sFilePath)
                    If fInfo.Exists Then
                        fInfo.Delete()
                    End If
                End If

            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As KTB.DNet.Domain.Buletin, ByVal sPhysicalMainFolderPaths() As String) As Integer
            Dim nResult As Integer = -1
            Try
                ' get the filename from db
                Dim sFileName As String = objDomain.FileName.Trim()
                Dim lLengthArray As Integer = sPhysicalMainFolderPaths.Length - 1 : Dim i As Integer
                Dim sFilePaths(lLengthArray) As String

                Dim sSeparatePath As String = ""
                If sFileName <> "" Then
                    Dim separator As String = "\"
                    sSeparatePath = Me.GetHierarchicalCategoryName(objDomain.BuletinCategory.ID, separator)
                End If

                For i = sPhysicalMainFolderPaths.GetLowerBound(0) To sPhysicalMainFolderPaths.GetUpperBound(0)

                    ' correct the path
                    Dim sPhysicalMainFolderPath As String = sPhysicalMainFolderPaths(i).Trim()
                    'If sPhysicalMainFolderPath = "" Then
                    'sPhysicalMainFolderPath = App.path
                    'End If
                    If Right(sPhysicalMainFolderPath, 1) <> "\" Then sPhysicalMainFolderPath = sPhysicalMainFolderPath & "\"

                    ' set the folder path that the filename exist
                    sFilePaths(i) = ""

                    ' and then added the filename
                    sFilePaths(i) = sPhysicalMainFolderPath + sSeparatePath + "\" + sFileName
                Next


                ' delete data from db
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = m_BuletinMapper.Update(objDomain, m_userPrincipal.Identity.Name)


                ' if success and there is a file then do delete file
                If nResult <> -1 And sFileName <> "" Then
                    For Each sFilePath As String In sFilePaths
                        Dim fInfo As FileInfo = New FileInfo(sFilePath)
                        If fInfo.Exists Then
                            fInfo.Delete()
                        End If
                    Next
                End If
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function GetHierarchicalCategoryName(ByVal CategoryID As Integer, ByVal Separator As String) As String
            Dim Retval As String = ""

            '1st version
            Dim oBuletinCategory As BuletinCategory = New BuletinCategoryFacade(Me.m_userPrincipal).Retrieve(CategoryID)
            If oBuletinCategory.Leveling > 0 Then
                '-- then recall again the method GetHierarchicalCategoryName until Leveling is 0.
                Retval = GetHierarchicalCategoryName(oBuletinCategory.Parent, Separator) & Separator & oBuletinCategory.Code
            Else
                Retval = oBuletinCategory.Code
            End If

            Return Retval
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.Buletin) Then
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Buletin).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.Buletin).MarkLoaded()

            End If
        End Sub
#End Region

#Region "Custom Method"
        
#End Region

    End Class

End Namespace

