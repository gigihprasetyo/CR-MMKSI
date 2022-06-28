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
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 8/2/2007 - 12:59:07 PM
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
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.BusinessFacade.General
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNET.BusinessFacade.IndentPart


    Public Class IndentPartHeaderFacade
        Inherits AbstractFacade


#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_IndentPartHeaderMapper As IMapper
        Private m_IndentPartPOHeaderMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_IndentPartHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(IndentPartHeader).ToString)
            Me.m_IndentPartPOHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(IndentPartPOHeader).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(IndentPartHeader))
            Me.DomainTypeCollection.Add(GetType(IndentPartDetail))


        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrievePOHeader(ByVal ID As Integer) As IndentPartPOHeader
            Return CType(m_IndentPartPOHeaderMapper.Retrieve(ID), IndentPartPOHeader)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As IndentPartHeader
            Return CType(m_IndentPartHeaderMapper.Retrieve(ID), IndentPartHeader)
        End Function

        Public Function Retrieve(ByVal RequestNo As String) As IndentPartHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), "RequestNo", MatchType.Exact, RequestNo))

            Dim IndentPartHeaderColl As ArrayList = m_IndentPartHeaderMapper.RetrieveByCriteria(criterias)
            If (IndentPartHeaderColl.Count > 0) Then
                Return CType(IndentPartHeaderColl(0), IndentPartHeader)
            End If
            Return New IndentPartHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_IndentPartHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_IndentPartHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_IndentPartHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_IndentPartHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_IndentPartHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _IndentPartHeader As ArrayList = m_IndentPartHeaderMapper.RetrieveByCriteria(criterias)
            Return _IndentPartHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim IndentPartHeaderColl As ArrayList = m_IndentPartHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return IndentPartHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartHeader), sortColumn, sortDirection))

                If sortColumn.ToUpper() = "TERMOFPAYMENT.ID" Then
                    Dim sSQL As String = GetRetrieveSpSortByTOP(Criterias, sortColl, pageNumber, pageSize, totalRow)
                    Dim result As ArrayList = m_IndentPartHeaderMapper.RetrieveSP(sSQL)
                    totalRow = GetRowCount(Criterias)
                    Return result
                End If

            Else
                sortColl = Nothing
            End If

            Dim ClaimReasonColl As ArrayList = m_IndentPartHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)

            Return ClaimReasonColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(IndentPartHeader), SortColumn, sortDirection))

            Dim IndentPartHeaderColl As ArrayList = m_IndentPartHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)

            Return IndentPartHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim IndentPartHeaderColl As ArrayList = m_IndentPartHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return IndentPartHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim IndentPartHeaderColl As ArrayList = m_IndentPartHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(IndentPartHeader), columnName, matchOperator, columnValue))
            Return IndentPartHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(IndentPartHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), columnName, matchOperator, columnValue))

            Return m_IndentPartHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(IndentPartHeader), "IndentPartHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(IndentPartHeader), "IndentPartHeaderCode", AggregateType.Count)
            Return CType(m_IndentPartHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function InsertIndentPartheader(ByVal objDomain As KTB.DNET.Domain.IndentPartHeader, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As IndentPartDetail In arrIPDetail
                            objIPDetail.IndentPartHeader = objDomain
                            m_TransactionManager.AddInsert(objIPDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                    If returnValue > 0 Then
                        Me.WriteHistory(objDomain, Nothing)
                    End If
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

        Public Function InsertIndentPartDetail(ByVal objDomain As KTB.DNET.Domain.IndentPartHeader, ByVal objIPDetail As IndentPartDetail) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    m_TransactionManager.AddInsert(objIPDetail, m_userPrincipal.Identity.Name)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objIPDetail.ID
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

        Public Function UpdateIndentPartheader(ByVal objDomain As KTB.DNET.Domain.IndentPartHeader, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    'm_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As IndentPartDetail In arrIPDetail
                            If IsNothing(New IndentPartDetailFacade(Me.m_userPrincipal).ValidateItem(objDomain.ID, objIPDetail.SparePartMaster.PartNumber)) Then
                                objIPDetail.IndentPartHeader = objDomain
                                m_TransactionManager.AddInsert(objIPDetail, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddUpdate(objIPDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    Dim objDomainInDB As IndentPartHeader = m_IndentPartHeaderMapper.Retrieve(objDomain.ID)
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                    If returnValue > 0 Then Me.WriteHistory(objDomain, objDomainInDB)
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

        Public Function Update(ByVal arlIPH As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlIPH.Count > 0 Then
                        For Each objIPHH As IndentPartHeader In arlIPH
                            Dim objDomainInDB As IndentPartHeader = m_IndentPartHeaderMapper.Retrieve(objIPHH.ID)
                            m_TransactionManager.AddUpdate(objIPHH, m_userPrincipal.Identity.Name)
                            Me.WriteHistory(objIPHH, objDomainInDB)
                        Next
                    End If
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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

        Public Function Update(ByVal objDomain As IndentPartHeader) As Integer
            Dim nResult As Integer = -1
            Try
                Dim objDomainInDB As IndentPartHeader = m_IndentPartHeaderMapper.Retrieve(objDomain.ID)
                Me.WriteHistory(objDomain, objDomainInDB)
                Return m_IndentPartHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Function Delete(ByVal objDomain As IndentPartHeader) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                Return m_IndentPartHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Function

        Public Sub DeleteFromDB(ByVal objDomain As IndentPartHeader)
            Try
                m_IndentPartHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteIndentPartHeader(ByVal objDomain As KTB.DNET.Domain.IndentPartHeader, ByVal arrIPDetail As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    If arrIPDetail.Count > 0 Then
                        For Each objIPDetail As IndentPartDetail In arrIPDetail
                            m_TransactionManager.AddDelete(objIPDetail)
                        Next
                    End If

                    m_TransactionManager.AddDelete(objDomain)
                    m_TransactionManager.PerformTransaction()
                    returnValue = 1
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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is IndentPartHeader) Then
                CType(InsertArg.DomainObject, IndentPartHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, IndentPartHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is IndentPartDetail) Then
                CType(InsertArg.DomainObject, IndentPartDetail).ID = InsertArg.ID
            End If
        End Sub

#End Region

#Region "Custom Method"
        'Public Function AggreateForTotalQty() As Aggregate
        '    Dim aggregates As New Aggregate(GetType(IndentPartDetail), "Qty", AggregateType.Count)
        '    Return aggregates
        'End Function

        'Public Function TotalQty(ByVal criteria As ICriteria, ByVal aggregate As IAggregate) As Integer
        '    Return CType(m_IndentPartHeaderMapper.RetrieveScalar(aggregate, criteria), Integer)
        'End Function

        'Public Function AllowChangeStatus(ByVal iph As IndentPartHeader, ByVal val1 As Byte, ByVal val2 As Byte) As Boolean
        '    If iph.StatusInProgres <> "Belum Diproses" Then
        '        Return False
        '    Else
        '        Select Case val1
        '            '----Status 'Baru' can updated to 'Batal' or 'Validasi'
        '        Case EnumIndentPartStatus.IndentPartStatusDealer.Baru
        '                If val2 <> EnumIndentPartStatus.IndentPartStatusDealer.BatalValidasi Then
        '                    Return True
        '                Else
        '                    Return False
        '                End If

        '                '----Status 'Batal' can't  updated 
        '            Case EnumIndentPartStatus.IndentPartStatusDealer.Batal
        '                Return False

        '                '----Status 'Validasi' can updated to 'Batal Validasi'
        '            Case EnumIndentPartStatus.IndentPartStatusDealer.Validasi
        '                If val2 > val1 Then
        '                    Return True
        '                Else
        '                    Return False
        '                End If

        '                '----Status 'Batal Validasi' can't  updated 
        '            Case EnumIndentPartStatus.IndentPartStatusDealer.BatalValidasi
        '                Return False
        '        End Select
        '    End If
        'End Function

        'Public Function AllowChangeStatusForKTB(ByVal iph As IndentPartHeader, ByVal val1 As Byte, ByVal val2 As Byte) As Boolean

        '    Select Case val1

        '        '----Status 'Baru' can updated to 'Konfirmasi'
        '    Case EnumIndentPartStatus.IndentPartStatusKTB.Baru
        '            If val2 <> EnumIndentPartStatus.IndentPartStatusKTB.Konfirmasi Then
        '                Return False
        '            Else
        '                Return True
        '            End If

        '            '----Status 'Batal' can't  updated 
        '        Case EnumIndentPartStatus.IndentPartStatusKTB.Konfirmasi
        '            '----cek allocation

        '            If val2 = EnumIndentPartStatus.IndentPartStatusKTB.BatalKonfirmasi OrElse _
        '                val2 = EnumIndentPartStatus.IndentPartStatusKTB.Rilis OrElse _
        '                val2 = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
        '                Return True
        '            Else
        '                Return False
        '            End If

        '            '----Status 'Batal Konfirmasi' can't updated
        '        Case EnumIndentPartStatus.IndentPartStatusKTB.BatalKonfirmasi
        '            Return False

        '            '----Status 'Batal Validasi' can't  updated 
        '        Case EnumIndentPartStatus.IndentPartStatusKTB.Rilis
        '            Return False
        '            If val2 = EnumIndentPartStatus.IndentPartStatusKTB.BatalRilis OrElse _
        '                val2 = EnumIndentPartStatus.IndentPartStatusKTB.Selesai OrElse _
        '                val2 = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
        '                Return True
        '            End If

        '        Case EnumIndentPartStatus.IndentPartStatusKTB.BatalRilis
        '            Return False
        '            If val2 = EnumIndentPartStatus.IndentPartStatusKTB.Rilis OrElse _
        '                val2 = EnumIndentPartStatus.IndentPartStatusKTB.Selesai OrElse _
        '                val2 = EnumIndentPartStatus.IndentPartStatusKTB.Tolak Then
        '                Return True
        '            End If

        '        Case EnumIndentPartStatus.IndentPartStatusKTB.Selesai
        '            Return False

        '    End Select

        'End Function

        Public Sub RecordStatusChangeHistory(ByVal item As IndentPartHeader, ByVal oldStatus As Integer)
            Dim objStatusChangeHistoryFacade As StatusChangeHistoryFacade
            If Not item Is Nothing Then
                objStatusChangeHistoryFacade = New StatusChangeHistoryFacade(m_userPrincipal)
                objStatusChangeHistoryFacade.Insert(CInt(LookUp.DocumentType.Indent_Part), item.RequestNo, oldStatus, item.Status)
            End If
        End Sub

        Private Function GetRetrieveSpSortByTOP(ByVal Criterias As ICriteria, ByVal Sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As String
            Dim sSQL As String = "EXEC up_PagingQuery "
            sSQL &= "@Table = N'IndentPartHeader', "
            sSQL &= "@PK = N'ID', "
            sSQL &= "@PageSize = " & pageSize & ", "
            sSQL &= "@PageNumber = " & pageNumber & ", "
            sSQL &= "@Filter = N' LEFT JOIN TermOfPayment ON IndentPartHeader.TermOfPaymentID = TermOfPayment.ID "
            sSQL &= Criterias.ToString().Replace("'", "''")
            sSQL &= "', @Sort = N'" & Sorts.ToString() & "'"

            Return sSQL
        End Function

        Private Function GetRowCount(ByVal Criterias As ICriteria) As Integer
            Dim agg As Aggregate = New Aggregate(GetType(IndentPartHeader), "ID", AggregateType.Count)

            Return CType(m_IndentPartHeaderMapper.RetrieveScalar(agg, Criterias), Integer)
        End Function

#Region "DataHistory"

        Private m_DataHistoryMapper As IMapper
        Private m_DataHistoryDetailMapper As IMapper

        Public Function WriteHistory(ByVal oCR As IndentPartHeader, ByVal oCRinDB As IndentPartHeader) As Integer
            Dim iResult As Integer = -1
            If Not IsNothing(oCR) AndAlso oCR.ID < 1 AndAlso Not IsNothing(oCRinDB) Then oCR.ID = oCRinDB.ID
            If oCR.ID > 0 Then

                If IsNothing(Me.m_DataHistoryMapper) Then
                    Me.m_DataHistoryMapper = MapperFactory.GetInstance.GetMapper(GetType(DataHistory).ToString)
                    Me.m_DataHistoryDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(DataHistoryDetail).ToString)
                End If


                'Dim oCRinDB As IndentPartHeader = Me.m_IndentPartHeaderMapper.Retrieve(oCR.ID)
                Dim oDH As New DataHistory
                Dim oDHD As DataHistoryDetail
                Dim aDHDs As New ArrayList
                Dim IsChanged As Boolean = False

                oDH.DataTable = "IndentPartHeader"
                oDH.DataID = oCR.ID
                If IsNothing(oCRinDB) Then
                    oCRinDB = New IndentPartHeader
                End If
                If 1 = 1 Then ' oCRinDB.ID < 1 OrElse oCR.Status <> oCRinDB.Status Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "Status" : oDHD.OldValue = oCRinDB.Status : oDHD.NewValue = oCR.Status : IsChanged = True
                    aDHDs.Add(oDHD)
                End If
                If 1 = 1 Then ' oCRinDB.ID < 1 OrElse oCR.StatusKTB <> oCRinDB.StatusKTB Then
                    oDHD = New DataHistoryDetail
                    oDHD.FieldName = "StatusKTB" : oDHD.OldValue = oCRinDB.StatusKTB : oDHD.NewValue = oCR.StatusKTB : IsChanged = True
                    aDHDs.Add(oDHD)
                End If

                If IsChanged Then
                    Dim cDH As New CriteriaComposite(New Criteria(GetType(DataHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim aDHs As ArrayList

                    cDH.opAnd(New Criteria(GetType(DataHistory), "DataTable", MatchType.Exact, "IndentPartHeader"))
                    cDH.opAnd(New Criteria(GetType(DataHistory), "DataID", MatchType.Exact, oCR.ID))
                    aDHs = Me.m_DataHistoryMapper.RetrieveByCriteria(cDH)
                    If aDHs.Count > 0 Then
                        oDH = CType(aDHs(0), DataHistory)
                    End If
                    If oDH.ID <= 0 Then
                        oDH.ID = Me.m_DataHistoryMapper.Insert(oDH, m_userPrincipal.Identity.Name)
                    End If
                    If oDH.ID > 0 Then
                        iResult = oDH.ID
                        For Each oDHDNew As DataHistoryDetail In aDHDs
                            oDHDNew.DataHistory = oDH
                            oDHD.ID = Me.m_DataHistoryDetailMapper.Insert(oDHDNew, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        iResult = -2
                    End If
                End If

            End If
            Return iResult
        End Function

#End Region

#End Region

    End Class

End Namespace

