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
    Public Class WSCHeaderBBFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_WSCMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_WSCMapper = MapperFactory.GetInstance().GetMapper(GetType(WSCHeaderBB).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(WSCHeaderBB))
            Me.DomainTypeCollection.Add(GetType(WSCDetailBB))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As WSCHeaderBB
            Return CType(m_WSCMapper.Retrieve(ID), WSCHeaderBB)
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

        Public Function Retrieve(ByVal Code As String) As WSCHeaderBB
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ClaimNumber", MatchType.Exact, Code))

            Dim WSCHeaderBBColl As ArrayList = m_WSCMapper.RetrieveByCriteria(criterias)
            If (WSCHeaderBBColl.Count > 0) Then
                Return CType(WSCHeaderBBColl(0), WSCHeaderBB)
            End If
            Return New WSCHeaderBB
        End Function

        Public Function RetrieveActiveList(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            'If Not IsNothing(sortColumn) And sortColumn <> "" Then
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(WSCHeaderBB), sortColumn, sortDirection))
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

#End Region

#Region "Transaction/Other Public Method"

        Public Function Update(ByVal objDomain As WSCHeaderBB) As Integer
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
                        Dim objWSCDetail As WSCDetailBB
                        objWSCDetail = CType(objDomain.WSCDetails.Item(detCounter), WSCDetailBB)
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

        Public Function UpdateRowStatus(ByVal objDomain As WSCHeaderBB) As Integer
            Dim nResult As Integer = -1
            Try
                objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                nResult = objDomain.RowStatus
                m_WSCMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function InsertWSC(ByVal ListWSCHeaderBB As ArrayList) As Integer
            Dim nReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    Dim nCounter As Integer
                    Dim mCounter As Integer
                    Dim rCounter As Integer
                    For nCounter = 0 To ListWSCHeaderBB.Count - 1
                        Dim objWSCHeaderBB As WSCHeaderBB
                        objWSCHeaderBB = CType(ListWSCHeaderBB.Item(nCounter), WSCHeaderBB)
                        m_TransactionManager.AddInsert(objWSCHeaderBB, m_userPrincipal.Identity.Name)

                        For mCounter = 0 To objWSCHeaderBB.WSCDetailBBs.Count - 1
                            Dim objWSCDetailBB As WSCDetailBB
                            objWSCDetailBB = CType(objWSCHeaderBB.WSCDetailBBs.Item(mCounter), WSCDetailBB)
                            m_TransactionManager.AddInsert(objWSCDetailBB, m_userPrincipal.Identity.Name)
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

        Public Function UpdateWSCUploadedToSAP(ByVal ListWSCHeaderBB As ArrayList) As Integer
            Dim nReturnValue As Integer = 0
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim nCounter As Integer
                    For nCounter = 0 To ListWSCHeaderBB.Count - 1
                        Dim objWSCHeaderBB As WSCHeaderBB
                        objWSCHeaderBB = CType(ListWSCHeaderBB.Item(nCounter), WSCHeaderBB)
                        m_TransactionManager.AddUpdate(objWSCHeaderBB, m_userPrincipal.Identity.Name)
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

            If (TypeOf InsertArg.DomainObject Is WSCHeaderBB) Then
                CType(InsertArg.DomainObject, WSCHeaderBB).ID = InsertArg.ID
                CType(InsertArg.DomainObject, WSCHeaderBB).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is WSCDetailBB) Then
                CType(InsertArg.DomainObject, WSCDetailBB).ID = InsertArg.ID
            End If
        End Sub

        ''--- add start by rudi
        Public Function InsertTransaction(ByVal objWSCHeader As WSCHeaderBB, ByVal arrWSCOngkosKerja As ArrayList, ByVal arrWSCParts As ArrayList, ByVal arrWSCEvidence As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()

                    m_TransactionManager.AddInsert(objWSCHeader, m_userPrincipal.Identity.Name)

                    If arrWSCOngkosKerja.Count > 0 Then
                        For Each oWSCOngkosKerja As WSCDetailBB In arrWSCOngkosKerja
                            oWSCOngkosKerja.WSCHeaderBB = objWSCHeader
                            m_TransactionManager.AddInsert(oWSCOngkosKerja, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrWSCParts.Count > 0 Then
                        For Each oWSCParts As WSCDetailBB In arrWSCParts
                            oWSCParts.WSCHeaderBB = objWSCHeader
                            m_TransactionManager.AddInsert(oWSCParts, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrWSCEvidence.Count > 0 Then
                        For Each oWSCEvidence As WSCEvidenceBB In arrWSCEvidence
                            oWSCEvidence.WSCHeaderBB = objWSCHeader
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

        Public Function Insert(ByVal objDomain As WSCHeaderBB) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)
                    For Each item As WSCDetailBB In objDomain.WSCDetailBBs
                        item.WSCHeaderBB = objDomain
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

        Public Function Delete(ByVal objDomain As WSCHeaderBB) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As WSCDetailBB In objDomain.WSCDetailBBs
                        item.WSCHeaderBB = objDomain
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
        Public Function UpdateTransaction(ByVal objWSCHeader As WSCHeaderBB, ByVal arrWSCOngkosKerja As ArrayList, ByVal arrDeletedWSCOngkosKerja As ArrayList, ByVal arrWSCParts As ArrayList, ByVal arrDeletedWSCParts As ArrayList, ByVal arrWSCEvidence As ArrayList, ByVal arrDeletedWSCEvidence As ArrayList, ByRef ErrMsg As String) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If arrDeletedWSCOngkosKerja.Count > 0 Then
                        For Each item As WSCDetailBB In arrDeletedWSCOngkosKerja
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedWSCParts.Count > 0 Then
                        For Each item As WSCDetailBB In arrDeletedWSCParts
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrDeletedWSCEvidence.Count > 0 Then
                        For Each item As WSCEvidenceBB In arrDeletedWSCEvidence
                            item.RowStatus = DBRowStatus.Deleted
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Next
                    End If

                    If arrWSCOngkosKerja.Count > 0 Then
                        For Each item As WSCDetailBB In arrWSCOngkosKerja
                            item.WSCHeaderBB = objWSCHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If
                        Next
                    End If

                    If arrWSCParts.Count > 0 Then
                        For Each item As WSCDetailBB In arrWSCParts
                            item.WSCHeaderBB = objWSCHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
                                m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                            End If

                        Next
                    End If

                    If arrWSCEvidence.Count > 0 Then
                        For Each item As WSCEvidenceBB In arrWSCEvidence
                            item.WSCHeaderBB = objWSCHeader
                            If item.ID <> 0 Then
                                m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                            Else
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

    'Public Class CompareChassisNumber
    '    Implements IComparer

    '    Private m_IsAsc As Boolean

    '    Public Sub New(ByVal IsAsc As Boolean)
    '        Me.m_IsAsc = IsAsc
    '    End Sub

    '    Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
    '        Implements IComparer.Compare

    '        Return IIf(m_IsAsc, (New CaseInsensitiveComparer).Compare(CType(x, WSCHeaderBB).ChassisMasterBB.ChassisNumber, CType(y, WSCHeaderBB).ChassisMasterBB.ChassisNumber), _
    '            (New CaseInsensitiveComparer).Compare(CType(y, WSCHeaderBB).ChassisMasterBB.ChassisNumber, CType(x, WSCHeaderBB).ChassisMasterBB.ChassisNumber))
    '    End Function
    'End Class
#End Region

End Namespace
