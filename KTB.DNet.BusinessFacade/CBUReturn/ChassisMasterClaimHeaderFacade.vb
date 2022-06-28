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
'// Copyright  2020
'// ---------------------
'// $History      : $
'// Generated on 9/7/2020 - 9:14:22 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
Imports System.Collections.Generic
Imports System.Linq

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class ChassisMasterClaimHeaderFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_ChassisMasterClaimHeaderMapper As IMapper
        Private m_ChassisMasterClaimDetailMapper As IMapper
        Private m_DocumentUploadMapper As IMapper
        Private m_ChassisMasterMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_ChassisMasterClaimHeaderMapper = MapperFactory.GetInstance.GetMapper(GetType(ChassisMasterClaimHeader).ToString)
            Me.m_ChassisMasterClaimDetailMapper = MapperFactory.GetInstance.GetMapper(GetType(ChassisMasterClaimDetail).ToString)
            Me.m_DocumentUploadMapper = MapperFactory.GetInstance.GetMapper(GetType(DocumentUpload).ToString)
            Me.m_ChassisMasterMapper = MapperFactory.GetInstance.GetMapper(GetType(ChassisMaster).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
            Me.DomainTypeCollection.Add(GetType(ChassisMasterClaimHeader))
            Me.DomainTypeCollection.Add(GetType(ChassisMasterClaimDetail))
            Me.DomainTypeCollection.Add(GetType(DocumentUpload))
            Me.DomainTypeCollection.Add(GetType(ChassisMaster))
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As ChassisMasterClaimHeader
            Return CType(m_ChassisMasterClaimHeaderMapper.Retrieve(ID), ChassisMasterClaimHeader)
        End Function

        Public Function Retrieve(ByVal Code As String) As ChassisMasterClaimHeader
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), "ClaimNumber", MatchType.Exact, Code))

            Dim ChassisMasterClaimHeaderColl As ArrayList = m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias)
            If (ChassisMasterClaimHeaderColl.Count > 0) Then
                Return CType(ChassisMasterClaimHeaderColl(0), ChassisMasterClaimHeader)
            End If
            Return New ChassisMasterClaimHeader
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_ChassisMasterClaimHeaderMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterClaimHeaderMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_ChassisMasterClaimHeaderMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _ChassisMasterClaimHeader As ArrayList = m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias)
            Return _ChassisMasterClaimHeader
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterClaimHeaderColl As ArrayList = m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return ChassisMasterClaimHeaderColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(ChassisMasterClaimHeader), SortColumn, sortDirection))
            Dim ChassisMasterClaimHeaderColl As ArrayList = m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return ChassisMasterClaimHeaderColl
        End Function


        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim ChassisMasterClaimHeaderColl As ArrayList = m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return ChassisMasterClaimHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim ChassisMasterClaimHeaderColl As ArrayList = m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(ChassisMasterClaimHeader), columnName, matchOperator, columnValue))
            Return ChassisMasterClaimHeaderColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(ChassisMasterClaimHeader), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), columnName, matchOperator, columnValue))

            Return m_ChassisMasterClaimHeaderMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimHeader), "ChassisMasterClaimHeaderCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(ChassisMasterClaimHeader), "ChassisMasterClaimHeaderCode", AggregateType.Count)
            Return CType(m_ChassisMasterClaimHeaderMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As ChassisMasterClaimHeader) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_ChassisMasterClaimHeaderMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As ChassisMasterClaimHeader) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_ChassisMasterClaimHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As ChassisMasterClaimHeader)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_ChassisMasterClaimHeaderMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As ChassisMasterClaimHeader)
            Try
                m_ChassisMasterClaimHeaderMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)
            If (TypeOf InsertArg.DomainObject Is ChassisMasterClaimHeader) Then
                CType(InsertArg.DomainObject, ChassisMasterClaimHeader).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ChassisMasterClaimHeader).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is ChassisMasterClaimDetail) Then
                CType(InsertArg.DomainObject, ChassisMasterClaimDetail).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ChassisMasterClaimDetail).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is DocumentUpload) Then
                CType(InsertArg.DomainObject, DocumentUpload).ID = InsertArg.ID
                CType(InsertArg.DomainObject, DocumentUpload).MarkLoaded()
            ElseIf (TypeOf InsertArg.DomainObject Is ChassisMaster) Then
                CType(InsertArg.DomainObject, ChassisMaster).ID = InsertArg.ID
                CType(InsertArg.DomainObject, ChassisMaster).MarkLoaded()
            End If
        End Sub

        Public Function Insert(ByVal objDomain As ChassisMasterClaimHeader, ByVal objDets As ArrayList, ByVal objDocs As ArrayList) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                m_TransactionManager.AddInsert(objDomain, _user)

                For Each objDet As ChassisMasterClaimDetail In objDets
                    objDet.ChassisMasterClaimHeader = objDomain
                    m_TransactionManager.AddInsert(objDet, _user)
                Next

                For Each item As DocumentUpload In objDocs
                    m_TransactionManager.AddInsert(item, _user)
                Next

                'm_TransactionManager.AddInsert(objStatusChangesHistory, _user)

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue

        End Function

        Public Function Update(ByVal objDomain As ChassisMasterClaimHeader, ByVal objStatusChangesHistories As ArrayList, _
                               ByVal objChassisMasters As ArrayList, _
                               Optional ByVal objChassisMasterClaimEmailQueues As ArrayList = Nothing) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String

            Try
                Dim performTransaction As Boolean = True
                Dim statusRetur As Integer = 0
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                m_TransactionManager.AddUpdate(objDomain, _user)

                For Each obj As StatusChangeHistory In objStatusChangesHistories
                    If obj.DocumentType = LookUp.DocumentType.CBUReturn_ReturStatus Then
                        statusRetur = obj.OldStatus
                    End If
                    obj.DocumentRegNumber = objDomain.ClaimNumber
                    m_TransactionManager.AddInsert(obj, _user)
                Next

                If Not objChassisMasters Is Nothing Then
                    For Each obj As ChassisMaster In objChassisMasters
                        

                        If obj.ID = 0 Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, obj.ChassisNumber))

                            Dim arrChassis As ArrayList = m_ChassisMasterMapper.RetrieveByCriteria(criterias)
                            If arrChassis.Count > 0 Then
                                Dim oldChassis As ChassisMaster = CType(arrChassis(0), ChassisMaster)
                                oldChassis.RowStatus = -1
                                m_TransactionManager.AddUpdate(oldChassis, _user)
                            End If

                            m_TransactionManager.AddInsert(obj, _user)

                            'Insert ChassisMasterLocation
                            For Each cmLoc As ChassisMasterLocation In objDomain.ChassisMaster.ChassisMasterLocations
                                cmLoc.ChassisMaster = obj
                                m_TransactionManager.AddInsert(cmLoc, _user)
                            Next

                            'Insert ChassisMasterProfile
                            For Each cmProf As ChassisMasterProfile In objDomain.ChassisMaster.ChassisMasterProfiles
                                cmProf.ChassisMaster = obj
                                m_TransactionManager.AddInsert(cmProf, _user)

                                'Insert ChassisMasterProfileHistory
                                For Each cmProfHist As ChassisMasterProfileHistory In cmProf.ChassisMasterProfileHistorys
                                    cmProfHist.ChassisMasterProfile = cmProf
                                    m_TransactionManager.AddInsert(cmProfHist, _user)
                                Next
                            Next

                            'Insert DO Only when current status Retur "Sales Replacement"
                            If statusRetur = EnumCBUReturn.StatusProsesRetur.Sales_Replacement Then
                                'Insert DeliveryOrder
                                For Each objDo As DeliveryOrder In objDomain.ChassisMaster.DeliveryOrders
                                    objDo.ChassisMaster = obj
                                    objDo.DODate = obj.DODate
                                    objDo.DONumber = obj.DONumber
                                    m_TransactionManager.AddInsert(objDo, _user)
                                Next
                            End If
                        Else
                            m_TransactionManager.AddUpdate(obj, _user)
                        End If
                    Next
                End If

                If Not objChassisMasterClaimEmailQueues Is Nothing Then
                    For Each item As ChassisMasterClaimEmailQueue In objChassisMasterClaimEmailQueues
                        If item.ID = 0 Then
                            m_TransactionManager.AddInsert(item, _user)
                        Else
                            item.IsSend = 2
                            m_TransactionManager.AddUpdate(item, _user)
                        End If
                    Next
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
            End Try

            Return returnValue
        End Function

        Public Function Update(ByVal objDomain As ChassisMasterClaimHeader, ByVal objDets As ArrayList, ByVal objDocs As ArrayList, _
                               ByVal objStatusChangesHistory As StatusChangeHistory, _
                               Optional ByVal objChassisMasterClaimEmailQueues As ArrayList = Nothing) As Integer
            Dim returnValue As Integer = -1
            Dim _user As String
            Dim isExist As Boolean = False

            Try
                Dim performTransaction As Boolean = True
                Dim ObjMapper As IMapper
                _user = m_userPrincipal.Identity.Name

                For Each item As ChassisMasterClaimDetail In objDets
                    isExist = False
                    For Each itemExist As ChassisMasterClaimDetail In objDomain.ChassisMasterClaimDetails
                        If item.ID = itemExist.ID Then
                            item.CreatedBy = itemExist.CreatedBy
                            isExist = True
                            Exit For
                        End If
                    Next

                    item.ChassisMasterClaimHeader = objDomain

                    If isExist Then
                        m_TransactionManager.AddUpdate(item, _user)
                    Else
                        m_TransactionManager.AddInsert(item, _user)
                    End If
                Next

                For Each item As ChassisMasterClaimDetail In objDomain.ChassisMasterClaimDetails
                    isExist = False
                    For Each itemExist As ChassisMasterClaimDetail In objDets
                        If item.ID = itemExist.ID Then
                            isExist = True
                            Exit For
                        End If
                    Next

                    If Not isExist Then
                        m_TransactionManager.AddDelete(item)
                    End If
                Next

                For Each item As DocumentUpload In objDocs
                    isExist = False
                    For Each itemExist As DocumentUpload In objDomain.DocumentUploads
                        If item.ID = itemExist.ID And item.Type = itemExist.Type And item.DocRegNumber = itemExist.DocRegNumber Then
                            item.CreatedBy = itemExist.CreatedBy
                            isExist = True
                            Exit For
                        End If
                    Next

                    If isExist Then
                        m_TransactionManager.AddUpdate(item, _user)
                    Else
                        m_TransactionManager.AddInsert(item, _user)
                    End If
                Next

                For Each item As DocumentUpload In objDomain.DocumentUploads
                    isExist = False
                    For Each itemExist As DocumentUpload In objDocs
                        If item.ID = itemExist.ID And item.Type = itemExist.Type And item.DocRegNumber = itemExist.DocRegNumber Then
                            isExist = True
                            Exit For
                        End If
                    Next

                    If Not isExist Then
                        m_TransactionManager.AddDelete(item)
                    End If
                Next

                If Not objStatusChangesHistory Is Nothing Then
                    m_TransactionManager.AddInsert(objStatusChangesHistory, _user)
                End If

                If Not objChassisMasterClaimEmailQueues Is Nothing Then
                    For Each item As ChassisMasterClaimEmailQueue In objChassisMasterClaimEmailQueues
                        If item.ID = 0 Then
                            m_TransactionManager.AddInsert(item, _user)
                        Else
                            m_TransactionManager.AddUpdate(item, _user)
                        End If
                    Next
                End If

                m_TransactionManager.AddUpdate(objDomain, _user)

                If performTransaction Then
                    m_TransactionManager.PerformTransaction()
                    returnValue = objDomain.ID
                End If
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try

            Return returnValue
        End Function

        Public Function GetDownLoadExcel(ByVal strCondition As String) As DataTable
            Dim arrParam As New ArrayList
            arrParam.Add(New SqlClient.SqlParameter("@WhereCondition", strCondition.Replace("{", "").Replace("}", "")))

            Dim dSet As DataSet = m_ChassisMasterClaimHeaderMapper.RetrieveDataSet("up_DownloadVehicleReturn", arrParam)
            Return dSet.Tables(0)
        End Function
#End Region

    End Class

End Namespace
