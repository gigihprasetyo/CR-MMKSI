#Region "imports library namespace"
Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
#End Region

#Region "imports custom namespace"
Imports KTB.Dnet.BusinessFacade
Imports KTB.Dnet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.Dnet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Linq
Imports System.Collections.Generic
#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class SPLFacade
        Inherits AbstractFacade

#Region "private variables"
        Private m_SPLMapper As IMapper
        Private m_SPLDealerMapper As IMapper
        Private m_SPLDetailMapper As IMapper
        Private m_SPLDetailtoSPLMapper As IMapper
        Private m_TransactionManager As TransactionManager
        Private m_userPrincipal As IPrincipal = Nothing
#End Region

#Region "constructor"
        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_SPLMapper = MapperFactory.GetInstance().GetMapper(GetType(SPL).ToString)
            Me.m_SPLDealerMapper = MapperFactory.GetInstance().GetMapper(GetType(SPLDealer).ToString)
            Me.m_SPLDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(SPLDetail).ToString)
            Me.m_SPLDetailtoSPLMapper = MapperFactory.GetInstance().GetMapper(GetType(SPLDetailtoSPL).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.Dnet.Domain.SPL))
            Me.DomainTypeCollection.Add(GetType(KTB.Dnet.Domain.SPLDealer))
            Me.DomainTypeCollection.Add(GetType(KTB.Dnet.Domain.SPLDetail))
            Me.DomainTypeCollection.Add(GetType(KTB.Dnet.Domain.SPLDetailtoSPL))
        End Sub
#End Region

#Region "retrieve"
        Public Function Retrieve(ByVal ID As Integer) As SPL
            Return CType(m_SPLMapper.Retrieve(ID), SPL)
        End Function

        Public Function Retrieve(ByVal Code As String) As SPL
            Dim objReturnValue As SPL
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.Exact, Code))
            Dim List As ArrayList
            List = m_SPLMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = CType(List.Item(0), SPL)
                End If
            End If
            Return objReturnValue
        End Function

        Public Function IsMainUsageFound(ByVal strMainUsageCode As String) As Boolean _
        'lagi trace yg ini jgn lupa

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim bResult As Boolean = False
            Dim SPLColl As ArrayList = m_SPLMapper.RetrieveByCriteria(criterias)
            If (SPLColl.Count > 0) Then
                bResult = True
            Else
            End If
            Return bResult

        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPLMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPLMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPLMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPLMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPLMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _MainUsage As ArrayList = m_SPLMapper.RetrieveByCriteria(criterias)
            Return _MainUsage
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPLColl As ArrayList = m_SPLMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPLColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPLColl As ArrayList = m_SPLMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPLColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal sortColumn As String, _
          ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_SPLMapper.RetrieveByCriteria(Criterias, sortColl)
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
               ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPLMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
            ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList

            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not sortColumn = "") Then
                sortColl.Add(New Sort(GetType(SPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim SPLColl As ArrayList = m_SPLMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPLColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPL), columnName, matchOperator, columnValue))
            Dim SPLColl As ArrayList = m_SPLMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPLColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPL), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), columnName, matchOperator, columnValue))

            Return m_SPLMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveDealerID(ByVal ID As Integer)

            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPLDealer), "SPL.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_SPLDealerMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveSPLDetail(ByVal ID As Integer)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPLDetail), "SPL.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_SPLDetailMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function

        Public Function RetrieveSPLDetailtoSPL(ByVal ID As Integer)
            Dim objReturnValue As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPLDetailtoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SPLDetailtoSPL), "SPLDetail.SPL.ID", MatchType.Exact, ID))
            Dim List As ArrayList
            List = m_SPLDetailtoSPLMapper.RetrieveByCriteria(criterias)
            If Not List Is Nothing Then
                If List.Count > 0 Then
                    objReturnValue = List
                End If
            End If
            Return objReturnValue
        End Function


#End Region

#Region "trans/other public method"
        'aa
#Region "Insert"
        Public Function InsertSPL(ByVal objDomain As SPL, ByVal arlSPLDetails As ArrayList, ByVal arlSPLDetailtoSPLs As ArrayList, _
                                  ByVal arlSPLDealerList As ArrayList, ByVal arlSPLDtlDocument As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    If Not IsNothing(arlSPLDetails) Then
                        If arlSPLDetails.Count > 0 Then
                            For Each oSPLDetails As SPLDetail In arlSPLDetails
                                oSPLDetails.SPL = objDomain
                                m_TransactionManager.AddInsert(oSPLDetails, m_userPrincipal.Identity.Name)

                                If Not IsNothing(arlSPLDetailtoSPLs) Then
                                    If arlSPLDetailtoSPLs.Count > 0 Then
                                        For Each oSPLDetailtoSPL As SPLDetailtoSPL In arlSPLDetailtoSPLs
                                            If oSPLDetails.ModelID = oSPLDetailtoSPL.ModelID Then
                                                If oSPLDetails.VechileType.ID = oSPLDetailtoSPL.VechileTypeID Then
                                                    oSPLDetailtoSPL.SPLDetail = oSPLDetails
                                                    m_TransactionManager.AddInsert(oSPLDetailtoSPL, m_userPrincipal.Identity.Name)
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If

                    For Each item As SPLDealer In arlSPLDealerList
                        item.SPL = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If Not IsNothing(arlSPLDtlDocument) Then
                        If arlSPLDtlDocument.Count > 0 Then
                            For Each oSPLDtlDoc As SPLDetailDocument In arlSPLDtlDocument
                                oSPLDtlDoc.SPL = objDomain
                                m_TransactionManager.AddInsert(oSPLDtlDoc, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

        Public Function Insert(ByVal objDomain As SPL) As Integer
            Dim iReturn As Integer = -2
            Try
                m_SPLMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn
        End Function

        Public Function InsertTransactionGenerateFiletoGW(ByVal objDomain As SPL, ByVal arlSPLDtlDocument As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arlSPLDtlDocument) Then
                        If arlSPLDtlDocument.Count > 0 Then
                            For Each oSPLDtlDoc As SPLDetailDocument In arlSPLDtlDocument
                                oSPLDtlDoc.SPL = objDomain
                                m_TransactionManager.AddInsert(oSPLDtlDoc, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objDomain.ID
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

#End Region
#Region "Update"
        Public Function Update(ByVal objDomain As SPL) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_SPLMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function
        Public Function UpdateSPL(ByVal objDomain As SPL, ByVal arrDetail As ArrayList, ByVal arrDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arrDetail.Count > 0 Then
                        'Update SPLDetail
                        For Each objDetail As SPLDetail In arrDetail
                            objDetail.SPL = objDomain
                            m_TransactionManager.AddInsert(objDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDealer.Count > 0 Then
                        'UpdateDealer
                        For Each item As SPLDealer In arrDealer
                            item.SPL = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    'Update SPL
                    returnValue = m_SPLMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        Public Function UpdateSPL(ByVal objDomain As SPL, ByVal arrDetail As ArrayList, ByVal arrDealer As ArrayList, ByVal OldSPLDetail As ArrayList, ByVal OldSPLDealer As ArrayList) As Integer
            Dim returnValue As Integer = -1

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    'Delete Old SPL Detail
                    'If Not IsNothing(OldSPLDetail) AndAlso OldSPLDetail.Count > 0 Then
                    '    For Each objDetail As SPLDetail In OldSPLDetail
                    '        m_TransactionManager.AddDelete(objDetail)
                    '    Next
                    'End If

                    If Not IsNothing(OldSPLDetail) AndAlso OldSPLDetail.Count > 0 Then
                        For Each objDetailOld As SPLDetail In OldSPLDetail
                            Dim isFound As Boolean = False
                            For Each objDetailNew As SPLDetail In arrDetail
                                If objDetailOld.ID = objDetailNew.ID Then
                                    isFound = True
                                    Exit For
                                End If
                            Next
                            If isFound = False Then
                                If Not objDetailOld.ID = 0 Then
                                    objDetailOld.RowStatus = CType(DBRowStatus.Deleted, Short)
                                    m_TransactionManager.AddUpdate(objDetailOld, m_userPrincipal.Identity.Name)
                                End If
                            End If
                        Next
                    End If

                    'Delete OLD SPL Dealer
                    If Not IsNothing(OldSPLDealer) AndAlso OldSPLDealer.Count > 0 Then
                        For Each objDetail As SPLDealer In OldSPLDealer
                            m_TransactionManager.AddDelete(objDetail)
                        Next
                    End If

                    If arrDetail.Count > 0 Then
                        'Update SPLDetail
                        For Each objDetail As SPLDetail In arrDetail
                            objDetail.SPL = objDomain
                            If Not objDetail.ID = 0 Then
                                If objDetail.IsUpdated Then
                                    m_TransactionManager.AddUpdate(objDetail, m_userPrincipal.Identity.Name)
                                End If
                            Else
                                m_TransactionManager.AddInsert(objDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrDealer.Count > 0 Then
                        'UpdateDealer
                        For Each item As SPLDealer In arrDealer
                            item.SPL = objDomain
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    'Update SPL
                    returnValue = m_SPLMapper.Update(objDomain, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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

        ''New Function per tgl 20200826
        Public Function UpdateSPL(ByVal objDomain As SPL, _
                                  ByRef arlSPLDetails As ArrayList, ByVal arlDelSPLDetails As ArrayList, _
                                  ByRef arlSPLDetailtoSPLs As ArrayList, ByVal arlDelSPLDetailtoSPLs As ArrayList, _
                                  ByVal arrSPLDealer As ArrayList, ByVal OldSPLDealer As ArrayList, _
                                  ByVal arlSPLDetailDoc As ArrayList, ByVal arlDelSPLDetailDoc As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim seqLoop As Integer = 0

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim arlSPLDetails0 As ArrayList
                    If Not IsNothing(arlSPLDetails) Then
                        arlSPLDetails0 = New System.Collections.ArrayList(
                                                        (From obj As SPLDetail In arlSPLDetails.OfType(Of SPLDetail)()
                                                            Where obj.ID = 0
                                                            Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                                            Select obj).ToList())
                        arlSPLDetails = New System.Collections.ArrayList(
                                                (From obj As SPLDetail In arlSPLDetails.OfType(Of SPLDetail)()
                                                    Where obj.ID > 0
                                                    Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                                    Select obj).ToList())
                    End If
                    Dim arlSPLDetailtoSPLs0 As ArrayList
                    If Not IsNothing(arlSPLDetailtoSPLs) Then
                        arlSPLDetailtoSPLs0 = New System.Collections.ArrayList(
                                                        (From obj As SPLDetailtoSPL In arlSPLDetailtoSPLs.OfType(Of SPLDetailtoSPL)()
                                                            Where obj.ID = 0
                                                            Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                                            Select obj).ToList())

                        arlSPLDetailtoSPLs = New System.Collections.ArrayList(
                                                        (From obj As SPLDetailtoSPL In arlSPLDetailtoSPLs.OfType(Of SPLDetailtoSPL)()
                                                            Where obj.ID > 0
                                                            Order By obj.PeriodYear, obj.PeriodMonth, obj.ModelID, obj.VechileTypeID
                                                            Select obj).ToList())
                    End If

                    'EDIT SPLDetail and SPLDetailtoSPL
                    '============================================================================
                    If Not IsNothing(arlDelSPLDetailtoSPLs) Then
                        If arlDelSPLDetailtoSPLs.Count > 0 Then
                            For Each oSPLDetailtoSPL As SPLDetailtoSPL In arlDelSPLDetailtoSPLs
                                oSPLDetailtoSPL.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(oSPLDetailtoSPL, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlSPLDetails) Then
                        If arlSPLDetails.Count > 0 Then
                            For Each oSPLDetail As SPLDetail In arlSPLDetails
                                oSPLDetail.SPL = objDomain
                                If oSPLDetail.ID > 0 Then
                                    If Not IsNothing(arlSPLDetailtoSPLs) Then
                                        If arlSPLDetailtoSPLs.Count > 0 Then
                                            For i As Integer = seqLoop To arlSPLDetailtoSPLs.Count - 1
                                                Dim oSPLDetailtoSPL As SPLDetailtoSPL = CType(arlSPLDetailtoSPLs(i), SPLDetailtoSPL)
                                                If oSPLDetail.PeriodYear = oSPLDetailtoSPL.PeriodYear AndAlso _
                                                    oSPLDetail.PeriodMonth = oSPLDetailtoSPL.PeriodMonth AndAlso _
                                                    oSPLDetail.ModelID = oSPLDetailtoSPL.ModelID AndAlso _
                                                    oSPLDetail.VechileType.ID = oSPLDetailtoSPL.VechileTypeID Then
                                                    oSPLDetailtoSPL.SPLDetail = oSPLDetail
                                                    If oSPLDetailtoSPL.ID > 0 Then
                                                        m_TransactionManager.AddUpdate(oSPLDetailtoSPL, m_userPrincipal.Identity.Name)
                                                    End If
                                                    Dim oSPLDetailtoSPL2 As SPLDetailtoSPL
                                                    Try
                                                        oSPLDetailtoSPL2 = CType(arlSPLDetailtoSPLs(i + 1), SPLDetailtoSPL)
                                                        If Not IsNothing(oSPLDetailtoSPL2) Then
                                                            If oSPLDetail.PeriodYear <> oSPLDetailtoSPL2.PeriodYear OrElse _
                                                                oSPLDetail.PeriodMonth <> oSPLDetailtoSPL2.PeriodMonth OrElse _
                                                                oSPLDetail.ModelID <> oSPLDetailtoSPL2.ModelID OrElse _
                                                                oSPLDetail.VechileType.ID <> oSPLDetailtoSPL2.VechileTypeID Then
                                                                seqLoop = i + 1
                                                                Exit For
                                                            End If
                                                        End If
                                                    Catch
                                                    End Try
                                                    seqLoop = i + 1
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If

                    seqLoop = 0
                    If Not IsNothing(arlSPLDetails) Then
                        If arlSPLDetails.Count > 0 Then
                            For Each oSPLDetail As SPLDetail In arlSPLDetails
                                oSPLDetail.SPL = objDomain
                                If oSPLDetail.ID > 0 Then
                                    If Not IsNothing(arlSPLDetailtoSPLs0) Then
                                        If arlSPLDetailtoSPLs0.Count > 0 Then
                                            For i As Integer = seqLoop To arlSPLDetailtoSPLs0.Count - 1
                                                Dim oSPLDetailtoSPL0 As SPLDetailtoSPL = CType(arlSPLDetailtoSPLs0(i), SPLDetailtoSPL)
                                                If oSPLDetail.PeriodYear = oSPLDetailtoSPL0.PeriodYear AndAlso _
                                                    oSPLDetail.PeriodMonth = oSPLDetailtoSPL0.PeriodMonth AndAlso _
                                                    oSPLDetail.ModelID = oSPLDetailtoSPL0.ModelID AndAlso _
                                                    oSPLDetail.VechileType.ID = oSPLDetailtoSPL0.VechileTypeID Then
                                                    oSPLDetailtoSPL0.SPLDetail = oSPLDetail
                                                    If oSPLDetailtoSPL0.ID = 0 Then
                                                        m_TransactionManager.AddInsert(oSPLDetailtoSPL0, m_userPrincipal.Identity.Name)
                                                    End If

                                                    Dim oSPLDetailtoSPL02 As SPLDetailtoSPL
                                                    Try
                                                        oSPLDetailtoSPL02 = CType(arlSPLDetailtoSPLs0(i + 1), SPLDetailtoSPL)
                                                        If Not IsNothing(oSPLDetailtoSPL02) Then
                                                            If oSPLDetail.PeriodYear <> oSPLDetailtoSPL02.PeriodYear OrElse _
                                                                oSPLDetail.PeriodMonth <> oSPLDetailtoSPL02.PeriodMonth OrElse _
                                                                oSPLDetail.ModelID <> oSPLDetailtoSPL02.ModelID OrElse _
                                                                oSPLDetail.VechileType.ID <> oSPLDetailtoSPL02.VechileTypeID Then
                                                                seqLoop = i + 1
                                                                Exit For
                                                            End If
                                                        End If
                                                    Catch
                                                    End Try
                                                    seqLoop = i + 1
                                                End If
                                            Next
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    End If
                    '=========>
                    If Not IsNothing(arlDelSPLDetails) Then
                        If arlDelSPLDetails.Count > 0 Then
                            For Each oSPLDetail As SPLDetail In arlDelSPLDetails
                                oSPLDetail.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(oSPLDetail, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If
                    If Not IsNothing(arlSPLDetails) Then
                        If arlSPLDetails.Count > 0 Then
                            For Each oSPLDetail As SPLDetail In arlSPLDetails
                                oSPLDetail.SPL = objDomain
                                If oSPLDetail.ID > 0 Then
                                    m_TransactionManager.AddUpdate(oSPLDetail, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If
                    '============================================================================

                    'INSERT SPLDetail and SPLDetailtoSPL
                    seqLoop = 0
                    If Not IsNothing(arlSPLDetails0) Then
                        If arlSPLDetails0.Count > 0 Then
                            For Each oSPLDetail0 As SPLDetail In arlSPLDetails0
                                oSPLDetail0.SPL = objDomain
                                If oSPLDetail0.ID = 0 Then
                                    m_TransactionManager.AddInsert(oSPLDetail0, m_userPrincipal.Identity.Name)
                                End If
                                If Not IsNothing(arlSPLDetailtoSPLs0) Then
                                    If arlSPLDetailtoSPLs0.Count > 0 Then
                                        For i As Integer = seqLoop To arlSPLDetailtoSPLs0.Count - 1
                                            Dim oSPLDetailtoSPL0 As SPLDetailtoSPL = CType(arlSPLDetailtoSPLs0(i), SPLDetailtoSPL)
                                            If oSPLDetail0.PeriodYear = oSPLDetailtoSPL0.PeriodYear AndAlso _
                                                oSPLDetail0.PeriodMonth = oSPLDetailtoSPL0.PeriodMonth AndAlso _
                                                oSPLDetail0.ModelID = oSPLDetailtoSPL0.ModelID AndAlso _
                                                oSPLDetail0.VechileType.ID = oSPLDetailtoSPL0.VechileTypeID Then
                                                oSPLDetailtoSPL0.SPLDetail = oSPLDetail0
                                                If oSPLDetailtoSPL0.ID = 0 Then
                                                    m_TransactionManager.AddInsert(oSPLDetailtoSPL0, m_userPrincipal.Identity.Name)
                                                End If

                                                Dim oSPLDetailtoSPL02 As SPLDetailtoSPL
                                                Try
                                                    oSPLDetailtoSPL02 = CType(arlSPLDetailtoSPLs0(i + 1), SPLDetailtoSPL)
                                                    If Not IsNothing(oSPLDetailtoSPL02) Then
                                                        If oSPLDetail0.PeriodYear <> oSPLDetailtoSPL02.PeriodYear OrElse _
                                                            oSPLDetail0.PeriodMonth <> oSPLDetailtoSPL02.PeriodMonth OrElse _
                                                            oSPLDetail0.ModelID <> oSPLDetailtoSPL02.ModelID OrElse _
                                                            oSPLDetail0.VechileType.ID <> oSPLDetailtoSPL02.VechileTypeID Then
                                                            seqLoop = i + 1
                                                            Exit For
                                                        End If
                                                    End If
                                                Catch
                                                End Try
                                                seqLoop = i + 1
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If
                    '============================================================================


                    Dim blnIsExist As Boolean = False
                    For Each objOldSPLDealer As SPLDealer In OldSPLDealer
                        blnIsExist = False
                        For Each objSPLDealer As SPLDealer In arrSPLDealer
                            If objOldSPLDealer.SPL.ID = objSPLDealer.SPL.ID Then
                                If objOldSPLDealer.Dealer.ID = objSPLDealer.Dealer.ID Then
                                    blnIsExist = True
                                    Exit For
                                End If
                            End If
                        Next
                        If blnIsExist = False Then
                            objOldSPLDealer.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(objOldSPLDealer, m_userPrincipal.Identity.Name)
                        End If
                    Next
                    blnIsExist = False
                    For Each objSPLDealer As SPLDealer In arrSPLDealer
                        blnIsExist = False
                        For Each objOldSPLDealer As SPLDealer In OldSPLDealer
                            If objOldSPLDealer.SPL.ID = objSPLDealer.SPL.ID Then
                                If objOldSPLDealer.Dealer.ID = objSPLDealer.Dealer.ID Then
                                    blnIsExist = True
                                    objSPLDealer.SPL = objDomain
                                    m_TransactionManager.AddUpdate(objSPLDealer, m_userPrincipal.Identity.Name)
                                    Exit For
                                End If
                            End If
                        Next
                        If blnIsExist = False Then
                            objSPLDealer.SPL = objDomain
                            m_TransactionManager.AddInsert(objSPLDealer, m_userPrincipal.Identity.Name)
                        End If
                    Next

                    ''Delete OLD SPL Dealer
                    'If Not IsNothing(OldSPLDealer) AndAlso OldSPLDealer.Count > 0 Then
                    '    For Each objDetail As SPLDealer In OldSPLDealer
                    '        m_TransactionManager.AddDelete(objDetail)
                    '    Next
                    'End If

                    'If arrSPLDealer.Count > 0 Then
                    '    'UpdateDealer
                    '    For Each item As SPLDealer In arrSPLDealer
                    '        item.SPL = objDomain
                    '        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    '    Next
                    'End If

                    If Not IsNothing(arlDelSPLDetailDoc) Then
                        If arlDelSPLDetailDoc.Count > 0 Then
                            For Each item As SPLDetailDocument In arlDelSPLDetailDoc
                                item.RowStatus = DBRowStatus.Deleted
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If Not IsNothing(arlSPLDetailDoc) Then
                        If arlSPLDetailDoc.Count > 0 Then
                            For Each item As SPLDetailDocument In arlSPLDetailDoc
                                item.SPL = objDomain
                                If item.ID <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    'Update SPL
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
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
#Region "Delete"
        Public Sub Delete(ByVal objDomain As SPL)
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_SPLMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function DeleteFromDB(ByVal objDomain As SPL) As Integer
            Dim iReturn As Integer = -1
            Try
                iReturn = m_SPLMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function DeleteOLDDealer(ByVal arrDealer As ArrayList) As Integer
            Dim iReturn As Integer = -1
            Try
                For Each _dealer As SPLDealer In arrDealer
                    Dim objDomain As SPLDealer = _dealer
                    iReturn = m_SPLDealerMapper.Delete(objDomain)
                Next

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

        Public Function DeleteOLDSPLDetail(ByVal arrDetail As ArrayList) As Integer
            Dim iReturn As Integer = -1
            Try
                For Each _detail As SPLDetail In arrDetail
                    Dim objDomain As SPLDetail = _detail
                    iReturn = m_SPLDetailMapper.Delete(objDomain)
                Next

            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return iReturn
        End Function

#End Region
        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPL), "Code", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(SPL), "ID", AggregateType.Count)
            Return CType(m_SPLMapper.RetrieveScalar(agg, crit), Integer)
        End Function

#End Region

#Region "custom method"

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is SPL) Then
                CType(InsertArg.DomainObject, SPL).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SPL).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is SPLDealer) Then
                CType(InsertArg.DomainObject, SPLDealer).ID = InsertArg.ID
            End If

            If (TypeOf InsertArg.DomainObject Is SPL) Then
                CType(InsertArg.DomainObject, SPL).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SPL).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is SPLDetail) Then
                CType(InsertArg.DomainObject, SPLDetail).ID = InsertArg.ID
            End If

            If (TypeOf InsertArg.DomainObject Is SPLDetail) Then
                CType(InsertArg.DomainObject, SPLDetail).ID = InsertArg.ID
                CType(InsertArg.DomainObject, SPLDetail).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is SPLDetailtoSPL) Then
                CType(InsertArg.DomainObject, SPLDetailtoSPL).ID = InsertArg.ID
            End If
        End Sub

        Public Function UpdateStatus(arrSPL As ArrayList, arrSPLOld As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arrSPL) Then
                        If arrSPL.Count > 0 Then
                            For Each item As SPL In arrSPL
                                Dim objStatusChangeHistory As StatusChangeHistory = New StatusChangeHistory
                                Dim listOld As List(Of SPL) = arrSPLOld.Cast(Of SPL).ToList()

                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)

                                objStatusChangeHistory.DocumentType = 17
                                objStatusChangeHistory.DocumentRegNumber = item.SPLNumber
                                objStatusChangeHistory.OldStatus = listOld.Where(Function(i) i.ID = item.ID).FirstOrDefault().Status
                                objStatusChangeHistory.NewStatus = item.Status
                                objStatusChangeHistory.RowStatus = 0
                                objStatusChangeHistory.CreatedBy = m_userPrincipal.Identity.Name
                                objStatusChangeHistory.CreatedTime = Date.Now
                                objStatusChangeHistory.LastUpdateBy = Date.Now
                                m_TransactionManager.AddInsert(objStatusChangeHistory, m_userPrincipal.Identity.Name)
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 1
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
#End Region


    End Class

End Namespace

