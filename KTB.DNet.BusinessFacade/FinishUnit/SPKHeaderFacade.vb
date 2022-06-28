#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011
'// ---------------------
'// $History      : $
'// Generated on 18/2/2011 - 1:43:31 PM
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
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.General

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class SPKHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_SPKHeaderMapper As IMapper
        Private m_SPKHeaderMapperView As IMapper
        Private m_CategoryMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_SPKHeaderMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.SPKHeader).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPKHeader))
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.SPKDetail))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As SPKHeader
            Return CType(m_SPKHeaderMapper.Retrieve(ID), SPKHeader)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_SPKHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_SPKHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_SPKHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(SPKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(SPKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _SPKHeader As ArrayList = m_SPKHeaderMapper.RetrieveByCriteria(criterias)
            Return _SPKHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKHeaderColl As ArrayList = m_SPKHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return SPKHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Dim SPKHeaderColl As ArrayList = m_SPKHeaderMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
            Return SPKHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(SPKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_SPKHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPKHeaderColl As ArrayList = m_SPKHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPKHeaderColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim SPKHeaderColl As ArrayList = m_SPKHeaderMapper.RetrieveByCriteria(criterias, sorts, pageNumber, pageSize, totalRow)
            Return SPKHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim SPKHeaderColl As ArrayList = m_SPKHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(SPKHeader), columnName, matchOperator, columnValue))
            Return SPKHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As KTB.Dnet.Domain.Search.Sort.SortDirection) As ArrayList
            Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(SPKHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), columnName, matchOperator, columnValue))

            Return m_SPKHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"


        'Public Sub Insert(ByVal objDomain As SPKHeader)
        '    Dim iReturn As Integer = -2
        '    Try
        '        m_SPKHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
        '    Catch ex As Exception
        '        Dim s As String = ex.Message
        '        iReturn = -1
        '    End Try

        'End Sub

        Public Sub Update(ByVal objDomain As SPKHeader)
            Try
                m_SPKHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub Update(ByVal id As Integer)
            Dim objSPK As SPKHeader
            Dim crispk As CriteriaComposite

            Try
                crispk = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crispk.opAnd(New Criteria(GetType(SPKHeader), "ID", MatchType.Exact, id))

                objSPK = m_SPKHeaderMapper.Retrieve(id)
                objSPK.IsSend = 1

                m_SPKHeaderMapper.Update(objSPK, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Function Insert(ByVal objDomain As KTB.DNet.Domain.SPKHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper
                    m_TransactionManager.AddInsert(objDomain, m_userPrincipal.Identity.Name)

                    For i As Integer = 0 To objDomain.SPKDetails.Count - 1
                        CType(objDomain.SPKDetails(i), SPKDetail).SPKHeader = objDomain
                        CType(objDomain.SPKDetails(i), SPKDetail).VehicleKind = Nothing
                        m_TransactionManager.AddInsert(objDomain.SPKDetails(i), m_userPrincipal.Identity.Name)
                    Next

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

        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is KTB.DNet.Domain.SPKHeader) Then

                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, KTB.DNet.Domain.SPKHeader).MarkLoaded()

            ElseIf (TypeOf InsertArg.DomainObject Is PKDetail) Then

                CType(InsertArg.DomainObject, PKDetail).ID = InsertArg.ID

            End If

        End Sub

        Public Function UpdateTransaction(ByVal objDomain As KTB.DNet.Domain.SPKHeader) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As SPKDetail In objDomain.SPKDetails
                        item.SPKHeader = objDomain
                        If item.ID <> 0 Then
                            m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                        End If

                    Next

                    m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)


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

#Region "Custom Method"

        'Public Function RetrieveListCategory() As ArrayList
        '    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Me.m_CategoryMapper = MapperFactory.GetInstance.GetMapper(GetType(Category).ToString)
        '    Return m_CategoryMapper.RetrieveByCriteria(criterias)
        'End Function

        Public Shared Function RetrieveListDealer() As ArrayList
            Dim m_DealerMapper As IMapper
            m_DealerMapper = MapperFactory.GetInstance.GetMapper(GetType(Dealer).ToString)
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing("DealerCode")) And (Not IsNothing("DealerCode")) Then
                sortColl.Add(New Sort(GetType(KTB.DNet.Domain.Dealer), "DealerCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Return m_DealerMapper.RetrieveList(sortColl)
        End Function

        Public Function retrieveListView() As ArrayList
            Return m_SPKHeaderMapperView.RetrieveList
        End Function

        Public Function RetrieveByCriteriaView(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Me.m_SPKHeaderMapperView = MapperFactory.GetInstance.GetMapper(GetType(view_PKList).ToString)

            Dim SPKHeaderCollView As ArrayList = m_SPKHeaderMapperView.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return SPKHeaderCollView
        End Function

        Public Function UpdateSPKs(ByVal arrSPK As ArrayList)
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    For Each item As SPKHeader In arrSPK
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
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

        'Public Function UpdateSPKHeaderDetail(ByVal objDomain As KTB.DNet.Domain.SPKHeader, ByVal arrPKDetail As ArrayList) As Integer
        '    Dim returnValue As Integer = -1
        '    If (Me.IsTaskFree()) Then
        '        Try
        '            Me.SetTaskLocking()
        '            Dim performTransaction As Boolean = True
        '            Dim ObjMapper As IMapper

        '            If arrPKDetail.Count > 0 Then
        '                For Each objPKDetail As PKDetail In arrPKDetail
        '                    objPKDetail.SPKHeader = objDomain
        '                    m_TransactionManager.AddUpdate(objPKDetail, m_userPrincipal.Identity.Name)
        '                Next
        '            End If

        '            'For Each item As PKDetail In objDomain.PKDetail
        '            '    item.SPKHeader = objDomain
        '            '    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
        '            'Next

        '            m_TransactionManager.AddUpdate(objDomain, m_userPrincipal.Identity.Name)

        '            If performTransaction Then
        '                m_TransactionManager.PerformTransaction()
        '                returnValue = 0
        '            End If
        '        Catch ex As Exception
        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
        '            If rethrow Then
        '                Throw
        '            End If
        '        Finally
        '            Me.RemoveTaskLocking()
        '        End Try
        '    End If
        '    Return returnValue
        'End Function

        Private Function IsStatusExist(ByVal _spkHeader As SPKHeader) As Boolean
            Try
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(SPKHeader), "SPKNumber", MatchType.Exact, _spkHeader.SPKNumber))
                criterias.opAnd(New Criteria(GetType(SPKHeader), "Dealer.ID", MatchType.Exact, _spkHeader.Dealer.ID))
                criterias.opAnd(New Criteria(GetType(SPKHeader), "Status", MatchType.Exact, _spkHeader.Status))
                Dim arlSPK As ArrayList = m_SPKHeaderMapper.RetrieveByCriteria(criterias)
                If Not IsNothing(arlSPK) AndAlso arlSPK.Count > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function UpdateSPKStatus(ByVal spkcoll As ArrayList, ByVal status As Integer)
            Dim returnvalue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performtransaction As Boolean = True
                    Dim objmapper As IMapper

                    For Each item As SPKHeader In spkcoll
                        'If IsStatusExist(item) Then
                        For Each itemDetail As SPKDetail In item.SPKDetails
                            If itemDetail.Status <> 1 And itemDetail.Status <> 3 Then
                                itemDetail.Status = status
                                itemDetail.RejectedReason = item.RejectedReason
                                m_TransactionManager.AddUpdate(itemDetail, m_userPrincipal.Identity.Name)
                            End If
                        Next
                        item.Status = status.ToString()
                        item.FlagUpdate = 0 'terupdate
                        m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                        'End If
                    Next

                    If performtransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnvalue = 0
                    End If
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "domain policy")
                    If rethrow Then
                        Throw
                    End If
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return returnvalue
        End Function

        Public Function RetrieveDataset(ByVal spString As String) As DataSet
            Dim ds As New DataSet

            ds = m_SPKHeaderMapper.RetrieveDataSet(spString)

            Return ds
        End Function

        Public Function RetrieveSP(ByVal strQuery As String) As ArrayList
            Dim arr As New ArrayList
            strQuery = "exec sp_SF_GetSPK"
            arr = m_SPKHeaderMapper.RetrieveSP(strQuery)
            Return arr
        End Function


        Public Function RetrieveQuery(ByVal strQuery As String) As ArrayList
            Dim arr As New ArrayList

            arr = m_SPKHeaderMapper.RetrieveSP(strQuery)
            Return arr
        End Function

        Public Function UpdateSAPCustomer(ByVal spk As SPKHeader, ByVal SFID As String) As String
            Dim arr As DataSet
            Dim strMSG As String

            Dim str As String = "exec UpdateSAPCustomer '" + spk.SPKNumber + "','" + SFID + "'"
            arr = m_SPKHeaderMapper.RetrieveDataSet(str)

            If arr.Tables.Count > 0 Then
                strMSG = " SalesForceID berhasil Di update pada SAPCustomer"
            Else
                strMSG = " SalesForceID Tidak berhasil Di update pada SAPCustomer"
            End If

        End Function

#End Region

    End Class

End Namespace