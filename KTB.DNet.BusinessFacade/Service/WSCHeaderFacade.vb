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

Namespace KTB.DNet.BusinessFacade.Service
    Public Class WSCHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_WSCMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_WSCMapper = MapperFactory.GetInstance().GetMapper(GetType(WSCHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(WSCHeader))
            Me.DomainTypeCollection.Add(GetType(WSCDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As WSCHeader
            Return CType(m_WSCMapper.Retrieve(ID), WSCHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_WSCMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_WSCMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveScalar(ByVal aggregation As IAggregate, ByVal criterias As ICriteria) As Integer
            Dim count As Integer = -1
            Try
                count = CType(m_WSCMapper.RetrieveScalar(aggregation, criterias), Integer)
            Catch ex As Exception
                count = -1
            End Try
            Return count
        End Function

        Public Function Retrieve(ByVal Code As String) As WSCHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.Exact, Code))

            Dim WSCHeaderColl As ArrayList = m_WSCMapper.RetrieveByCriteria(criterias)
            If (WSCHeaderColl.Count > 0) Then
                Return CType(WSCHeaderColl(0), WSCHeader)
            End If
            Return New WSCHeader
        End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            'If Not IsNothing(sortColumn) And sortColumn <> "" Then
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_WSCMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortDirection)) Then
                sortColl.Add(New Sort(GetType(WSCHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim WSCHeaderColl As ArrayList = m_WSCMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return WSCHeaderColl
        End Function
        Public Function RetrieveNoClaimList(ByVal dealerID As Integer) As ArrayList
            Dim SQL As String = String.Empty

            SQL = "select * from wscheader where DealerID = " & dealerID & " and status = 0 and rowstatus = 0 and CreatedTime < DATEADD(Day,-3, getdate())"

            Return m_WSCMapper.RetrieveSP(SQL)
        End Function

        Public Function Retrieve(ByVal oDealer As Dealer, ByVal ClaimNumber As String, ByVal ChassisNumber As String) As WSCHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, oDealer.ID))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ClaimNumber", MatchType.Exact, ClaimNumber))
            criterias.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, ChassisNumber))

            Dim WSCHeaderColl As ArrayList = m_WSCMapper.RetrieveByCriteria(criterias)
            If (WSCHeaderColl.Count > 0) Then
                Return CType(WSCHeaderColl(0), WSCHeader)
            End If
            Return New WSCHeader
        End Function
#End Region

#Region "Transaction/Other Public Method"

        Public Function Update(ByVal objDomain As WSCHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_WSCMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function UpdateHeaderDetail(ByVal objDomain As WSCHeader) As Integer
            Dim returnVal As Integer = -1
            Dim _user As String
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True

                    Dim detCounter As Integer
                    For detCounter = 0 To objDomain.WSCDetails.Count - 1
                        Dim objWSCDetail As WSCDetail
                        objWSCDetail = CType(objDomain.WSCDetails.Item(detCounter), WSCDetail)
                        m_TransactionManager.AddUpdate(objWSCDetail, m_userPrincipal.Identity.Name)
                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)
                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnVal = objDomain.ID
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
            Return returnVal
        End Function

        Public Function UpdateRowStatus(ByVal objDomain As WSCHeader) As Integer
            Dim _user As String
            Dim nResult As Integer = -1
            If (Me.IsTaskFree()) Then
                Me.SetTaskLocking()
                Dim performTransaction As Boolean = True

                Try
                    objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                    If objDomain.WSCDetails.Count > 0 Then
                        For Each owscDetail As WSCDetail In objDomain.WSCDetails
                            owscDetail.RowStatus = CType(DBRowStatus.Deleted, Short)
                            m_TransactionManager.AddUpdate(owscDetail, m_userPrincipal.Identity.Name)
                        Next
                    End If
                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        nResult = objDomain.RowStatus
                    End If

                    'm_WSCMapper.Update(objDomain, m_userPrincipal.Identity.Name)
                Catch ex As Exception
                    nResult = -1
                    Me.RemoveTaskLocking()
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return nResult
        End Function

        Public Function InsertWSC(ByVal ListWSCHeader As ArrayList) As Integer
            Dim nReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim nCounter As Integer
                    Dim mCounter As Integer
                    Dim rCounter As Integer
                    For nCounter = 0 To ListWSCHeader.Count - 1
                        Dim objWSCHeader As WSCHeader
                        objWSCHeader = CType(ListWSCHeader.Item(nCounter), WSCHeader)
                        m_TransactionManager.AddInsert(objWSCHeader, m_userPrincipal.Identity.Name)

                        For mCounter = 0 To objWSCHeader.WSCDetails.Count - 1
                            Dim objWSCDetail As WSCDetail
                            objWSCDetail = CType(objWSCHeader.WSCDetails.Item(mCounter), WSCDetail)
                            m_TransactionManager.AddInsert(objWSCDetail, m_userPrincipal.Identity.Name)
                        Next

                    Next

                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    nReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return nReturnValue
        End Function

        Public Function UpdateWSCUploadedToSAP(ByVal ListWSCHeader As ArrayList) As Integer
            Dim nReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim nCounter As Integer
                    For nCounter = 0 To ListWSCHeader.Count - 1
                        Dim objWSCHeader As WSCHeader
                        objWSCHeader = CType(ListWSCHeader.Item(nCounter), WSCHeader)
                        m_TransactionManager.AddUpdate(objWSCHeader, m_userPrincipal.Identity.Name)
                    Next
                    m_TransactionManager.PerformTransaction()
                Catch ex As Exception
                    nReturnValue = -1
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return nReturnValue
        End Function

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is WSCHeader) Then
                CType(InsertArg.DomainObject, WSCHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, WSCHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is WSCDetail) Then
                CType(InsertArg.DomainObject, WSCDetail).ID = InsertArg.ID
            End If
        End Sub

        ''--- add start by rudi
        Public Function InsertTransaction(ByVal objWSCHeader As WSCHeader, ByVal arrWSCOngkosKerja As ArrayList, ByVal arrWSCParts As ArrayList, ByVal arrWSCEvidence As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objWSCHeader, m_userPrincipal.Identity.Name)

                    If arrWSCOngkosKerja.Count > 0 Then
                        For Each oWSCOngkosKerja As WSCDetail In arrWSCOngkosKerja
                            oWSCOngkosKerja.WSCHeader = objWSCHeader
                            m_TransactionManager.AddInsert(oWSCOngkosKerja, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrWSCParts.Count > 0 Then
                        For Each oWSCParts As WSCDetail In arrWSCParts
                            oWSCParts.WSCHeader = objWSCHeader
                            m_TransactionManager.AddInsert(oWSCParts, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrWSCEvidence.Count > 0 Then
                        For Each oWSCEvidence As WSCEvidence In arrWSCEvidence
                            oWSCEvidence.WSCHeader = objWSCHeader
                            If oWSCEvidence.PathFile.Contains("{0}") Then
                                oWSCEvidence.PathFile = String.Format(oWSCEvidence.PathFile, objWSCHeader.ClaimNumber)
                            End If
                            m_TransactionManager.AddInsert(oWSCEvidence, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    m_TransactionManager.PerformTransaction()
                    returnValue = objWSCHeader.ID
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
        ''--- add end by rudi

#End Region

#Region "Custom Method"

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_WSCMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function Insert(ByVal objDomain As WSCHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As WSCDetail In objDomain.WSCDetails
                        item.WSCHeader = objDomain
                        m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                    Next

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Public Function Delete(ByVal objDomain As WSCHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As WSCDetail In objDomain.WSCDetails
                        item.WSCHeader = objDomain
                        m_TransactionManager.AddDelete(item)
                    Next
                    m_TransactionManager.AddDelete(objDomain)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 0
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

        Public Function DoRetrieveDataset(ByVal strSql As String) As DataSet
            Dim ds As New DataSet()
            ds = m_WSCMapper.RetrieveDataSet(strSql)
            Return ds
        End Function

        Public Function UpdateTransaction(ByVal objWSCHeader As WSCHeader, ByVal arrWSCOngkosKerja As ArrayList, ByVal arrDeletedWSCOngkosKerja As ArrayList, ByVal arrWSCParts As ArrayList, ByVal arrDeletedWSCParts As ArrayList, ByVal arrWSCEvidence As ArrayList, ByVal arrDeletedWSCEvidence As ArrayList, ByRef ErrMsg As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeletedWSCOngkosKerja.Count > 0 Then
                        For Each item As WSCDetail In arrDeletedWSCOngkosKerja
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedWSCParts.Count > 0 Then
                        For Each item As WSCDetail In arrDeletedWSCParts
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedWSCEvidence.Count > 0 Then
                        For Each item As WSCEvidence In arrDeletedWSCEvidence
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrWSCOngkosKerja.Count > 0 Then
                        For Each item As WSCDetail In arrWSCOngkosKerja
                            item.WSCHeader = objWSCHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrWSCParts.Count > 0 Then
                        For Each item As WSCDetail In arrWSCParts
                            item.WSCHeader = objWSCHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If

                    If arrWSCEvidence.Count > 0 Then
                        For Each item As WSCEvidence In arrWSCEvidence
                            item.WSCHeader = objWSCHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                If item.PathFile.Contains("{0}") Then
                                    item.PathFile = String.Format(item.PathFile, objWSCHeader.ClaimNumber)
                                End If
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    m_TransactionManager.AddUpdate(objWSCHeader, m_userPrincipal.Identity.Name)

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = objWSCHeader.ID
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

#Region "IComparer Class"

    Public Class CompareChassisNumber
        Implements IComparer

        Private m_IsAsc As Boolean

        Public Sub New(ByVal IsAsc As Boolean)
            Me.m_IsAsc = IsAsc
        End Sub

        Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
            Implements IComparer.Compare

            Return IIf(m_IsAsc, (New CaseInsensitiveComparer).Compare(CType(x, WSCHeader).ChassisMaster.ChassisNumber, CType(y, WSCHeader).ChassisMaster.ChassisNumber), _
                (New CaseInsensitiveComparer).Compare(CType(y, WSCHeader).ChassisMaster.ChassisNumber, CType(x, WSCHeader).ChassisMaster.ChassisNumber))
        End Function
    End Class
#End Region

End Namespace
