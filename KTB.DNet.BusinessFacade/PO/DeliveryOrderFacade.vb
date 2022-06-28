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
Imports KTb.DNet.Domain.Search
Imports KTb.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.PO

    Public Class DeliveryOrderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_DeliveryOrderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_DeliveryOrderMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.DeliveryOrder).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As KTB.DNet.Domain.DeliveryOrder
            Return CType(m_DeliveryOrderMapper.Retrieve(ID), KTB.DNet.Domain.DeliveryOrder)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_DeliveryOrderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_DeliveryOrderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_DeliveryOrderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.DeliveryOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DeliveryOrderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.DeliveryOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_DeliveryOrderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _DeliveryOrder As ArrayList = m_DeliveryOrderMapper.RetrieveByCriteria(criterias)
            Return _DeliveryOrder
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DeliveryOrderColl As ArrayList = m_DeliveryOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return DeliveryOrderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(DeliveryOrder), SortColumn, sortDirection))
            Dim DeliveryOrderColl As ArrayList = m_DeliveryOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return DeliveryOrderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim DeliveryOrderColl As ArrayList = m_DeliveryOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return DeliveryOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim DeliveryOrderColl As ArrayList = m_DeliveryOrderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), columnName, matchOperator, columnValue))
            Return DeliveryOrderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.DeliveryOrder), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), columnName, matchOperator, columnValue))

            Return m_DeliveryOrderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function Retrieve(ByVal Code As String) As KTB.DNet.Domain.DeliveryOrder
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "DONumber", MatchType.Exact, Code))

            Dim DeliveryOrderColl As ArrayList = m_DeliveryOrderMapper.RetrieveByCriteria(criterias)
            If (DeliveryOrderColl.Count > 0) Then
                Return CType(DeliveryOrderColl(0), KTB.DNet.Domain.DeliveryOrder)
            End If
            Return New KTB.DNet.Domain.DeliveryOrder
        End Function

#End Region

#Region "Transaction/Other Public Method"

        'Public Function ValidateCode(ByVal Code As String) As Integer
        '    Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DeliveryOrder), "DeliveryOrderCode", MatchType.Exact, Code))
        '    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DeliveryOrder), "DeliveryOrderCode", AggregateType.Count)
        '    Return CType(m_DeliveryOrderMapper.RetrieveScalar(agg, crit), Integer)
        'End Function

        Public Function Insert(ByVal objDeliveryOrder As KTB.DNet.Domain.DeliveryOrder) As Integer
            Dim nInsertedRow As Integer = -1
            Try
                nInsertedRow = m_DeliveryOrderMapper.Insert(objDeliveryOrder, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return ex.Message
            End Try
            Return nInsertedRow
        End Function

        Public Function Insert(ByVal objDeliveryOrder As DeliveryOrder, ByVal objChassisMaster As KTB.DNet.Domain.ChassisMaster, ByVal objSalesOrder As SalesOrder) As Integer
            Dim nInsertedRow As Integer = -1
            Try
                objDeliveryOrder.SalesOrder = objSalesOrder
                objDeliveryOrder.ChassisMaster = objChassisMaster

                nInsertedRow = m_DeliveryOrderMapper.Insert(objDeliveryOrder, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return ex.Message
            End Try
            Return nInsertedRow
        End Function

        Public Function Update(ByVal objDeliveryOrder As KTB.DNet.Domain.DeliveryOrder) As Integer
            Dim nUpdatedRow As Integer = -1
            Try
                nUpdatedRow = m_DeliveryOrderMapper.Update(objDeliveryOrder, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Return ex.Message
            End Try
            Return nUpdatedRow
        End Function

        Public Function InsertTransaction(ByVal objDeliveryOrder As DeliveryOrder) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDeliveryOrder, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDeliveryOrder.ID
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

        Public Function InsertTransaction(ByVal objDeliveryOrder As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If objDeliveryOrder.Count > 0 Then
                        For Each item As DeliveryOrder In objDeliveryOrder
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        objDeliveryOrder = New ArrayList
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = -2
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

        Public Sub DeleteFromDB(ByVal objDomain As DeliveryOrder)
            Try
                m_DeliveryOrderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        'Public Function DeleteFromDB(ByVal objDomain As KTB.DNet.Domain.DeliveryOrder, ByVal sPhysicalMainFolderPath As String) As Integer
        '    Dim nResult As Integer = -1
        '    Try
        '        ' get the filename from db
        '        Dim sFileName As String = objDomain.Buletin.FileName.Trim()

        '        ' correct the path
        '        sPhysicalMainFolderPath = sPhysicalMainFolderPath.Trim()
        '        If sPhysicalMainFolderPath = "" Then
        '            'sPhysicalMainFolderPath = App.path
        '        End If
        '        If Right(sPhysicalMainFolderPath, 1) <> "\" Then sPhysicalMainFolderPath = sPhysicalMainFolderPath & "\"

        '        ' set the folder path that the filename exist
        '        Dim sFilePath As String = ""

        '        If sFileName <> "" Then
        '            Dim separator As String = "\"
        '            sFilePath = Me.GetHierarchicalCategoryName(objDomain.Buletin.BuletinCategory.ID, separator)
        '        End If

        '        ' and then added the filename
        '        sFilePath = sPhysicalMainFolderPath + sFilePath + "\" + sFileName

        '        ' delete data from db
        '        nResult = m_DeliveryOrderMapper.Delete(objDomain)

        '        ' if success and there is a file then do delete file
        '        If nResult <> -1 And sFileName <> "" Then
        '            Dim fInfo As FileInfo = New FileInfo(sFilePath)
        '            If fInfo.Exists Then
        '                fInfo.Delete()
        '            End If
        '        End If
        '    Catch ex As Exception
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        '    Return nResult
        'End Function

        'Public Function DeleteFromDB(ByVal objDomain As KTB.DNet.Domain.DeliveryOrder, ByVal sPhysicalMainFolderPaths() As String) As Integer
        '    Dim nResult As Integer = -1
        '    Try
        '        ' get the filename from db
        '        Dim sFileName As String = objDomain.Buletin.FileName.Trim()
        '        Dim lLengthArray As Integer = sPhysicalMainFolderPaths.Length - 1 : Dim i As Integer
        '        Dim sFilePaths(lLengthArray) As String

        '        Dim sSeparatePath As String = ""
        '        If sFileName <> "" Then
        '            Dim separator As String = "\"
        '            sSeparatePath = Me.GetHierarchicalCategoryName(objDomain.Buletin.BuletinCategory.ID, separator)
        '        End If

        '        For i = sPhysicalMainFolderPaths.GetLowerBound(0) To sPhysicalMainFolderPaths.GetUpperBound(0)

        '            ' correct the path
        '            Dim sPhysicalMainFolderPath As String = sPhysicalMainFolderPaths(i).Trim()
        '            'If sPhysicalMainFolderPath = "" Then
        '            'sPhysicalMainFolderPath = App.path
        '            'End If
        '            If Right(sPhysicalMainFolderPath, 1) <> "\" Then sPhysicalMainFolderPath = sPhysicalMainFolderPath & "\"

        '            ' set the folder path that the filename exist
        '            sFilePaths(i) = ""

        '            ' and then added the filename
        '            sFilePaths(i) = sPhysicalMainFolderPath + sSeparatePath + "\" + sFileName
        '        Next

        '        ' delete data from db
        '        nResult = m_DeliveryOrderMapper.Delete(objDomain)

        '        ' if success and there is a file then do delete file
        '        If nResult <> -1 And sFileName <> "" Then
        '            For Each sFilePath As String In sFilePaths
        '                Dim fInfo As FileInfo = New FileInfo(sFilePath)
        '                If fInfo.Exists Then
        '                    fInfo.Delete()
        '                End If
        '            Next
        '        End If
        '    Catch ex As Exception
        '        Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '        If rethrow Then
        '            Throw
        '        End If
        '    End Try
        '    Return nResult
        'End Function

        'Public Function GetHierarchicalCategoryName(ByVal CategoryID As Integer, ByVal Separator As String) As String
        '    Dim Retval As String = ""

        '    '1st version
        '    'Dim oDeliveryOrderCategory As DeliveryOrderCategory = New DeliveryOrderCategoryFacade(Me.m_userPrincipal).Retrieve(CategoryID)
        '    Dim oBuletinCategory As BuletinCategory = New BuletinCategoryFacade(Me.m_userPrincipal).Retrieve(CategoryID)
        '    If oBuletinCategory.Leveling > 0 Then
        '        '-- then recall again the method GetHierarchicalCategoryName until Leveling is 0.
        '        Retval = GetHierarchicalCategoryName(oBuletinCategory.Parent, Separator) & Separator & oBuletinCategory.Code
        '    Else
        '        Retval = oBuletinCategory.Code
        '    End If

        '    Return Retval
        'End Function


#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is DeliveryOrder) Then
                CType(InsertArg.DomainObject, DeliveryOrder).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DeliveryOrder).MarkLoaded()

            End If
        End Sub
#End Region

    End Class

End Namespace

