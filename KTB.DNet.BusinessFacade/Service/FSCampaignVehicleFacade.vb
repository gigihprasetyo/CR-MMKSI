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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.BusinessFacade.Service
    Public Class FSCampaignVehicleFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_FSCampaignVehicleMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_FSCampaignVehicleMapper = MapperFactory.GetInstance.GetMapper(GetType(FSCampaignVehicle).ToString)
            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region


#Region "Retrieve"

        Public Function Retrieve(ByVal ID As Integer) As FSCampaignVehicle
            Return CType(m_FSCampaignVehicleMapper.Retrieve(ID), FSCampaignVehicle)
        End Function

        Public Function RetrieveList(ByVal fsCampaignID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaignVehicle), "FSCampaign.ID", MatchType.Exact, fsCampaignID))
            Dim FSCampaignVehicleColl As ArrayList = m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias)
            If (FSCampaignVehicleColl.Count > 0) Then
                Return FSCampaignVehicleColl
            Else
                Return New ArrayList
            End If
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_FSCampaignVehicleMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignVehicleMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignVehicleMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _FSCampaignVehicle As ArrayList = m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias)
            Return _FSCampaignVehicle
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim FSCampaignVehicleColl As ArrayList = m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignVehicleColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
       ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim FSCampaignVehicleColl As ArrayList = m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignVehicleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(FSCampaignVehicle), columnName, matchOperator, columnValue))
            Dim FSCampaignVehicleColl As ArrayList = m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return FSCampaignVehicleColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(FSCampaignVehicle), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), columnName, matchOperator, columnValue))

            Return m_FSCampaignVehicleMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function Insert(ByVal objDomain As FSCampaignVehicle) As Integer
            Dim iReturn As Integer = -2
            Try
                m_FSCampaignVehicleMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As FSCampaignVehicle) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_FSCampaignVehicleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function Delete(ByVal objDomain As FSCampaignVehicle) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_FSCampaignVehicleMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If

            End Try
            Return nResult
        End Function

        Public Function DeleteFromDB(ByVal objDomain As FSCampaignVehicle) As Integer
            Dim nResult As Integer = 1
            Try
                m_FSCampaignVehicleMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Function InsertTransaction(ByVal arlVehicleType As ArrayList, ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            Dim arrListVehicleType As ArrayList = New ArrayList
            If arlVehicleType.Count > 0 Then
                For Each item As String In arlVehicleType
                    Dim objVehicleTypeFacade As VechileTypeFacade = New VechileTypeFacade(m_userPrincipal)
                    Dim objVehicleType As VechileType = objVehicleTypeFacade.Retrieve(CType(item, Integer))
                    If Not objVehicleType Is Nothing Then
                        arrListVehicleType.Add(objVehicleType)
                    End If
                Next
            End If

            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlVehicleType.Count > 0 Then
                        For Each objVechileType As VechileType In arrListVehicleType
                            Dim objFSCampaignVehicle As FSCampaignVehicle = New FSCampaignVehicle
                            objFSCampaignVehicle.FSCampaign = fsCampaign
                            objFSCampaignVehicle.VechileType = objVechileType
                            objFSCampaignVehicle.RowStatus = 0
                            m_TransactionManager.AddInsert(objFSCampaignVehicle, m_userPrincipal.Identity.Name)
                        Next
                    Else
                        arlVehicleType = New ArrayList
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

        Public Function UpdateTransaction(ByVal arlVehicleType As ArrayList, ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If arlVehicleType.Count > 0 Then
                        For Each strVehicleID As String In arlVehicleType
                            Dim arrFSCampaignVehicle As ArrayList = New ArrayList
                            Dim objFSCampaignVehicle As FSCampaignVehicle = New FSCampaignVehicle

                            Dim objVechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(m_userPrincipal)
                            Dim objVehicleType As VechileType = objVechileTypeFacade.Retrieve(CType(strVehicleID, Integer))

                            Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSCampaignVehicle), "FSCampaign", MatchType.Exact, fsCampaign.ID))
                            critCol.opAnd(New Criteria(GetType(FSCampaignVehicle), "VechileType", MatchType.Exact, objVehicleType.ID))

                            arrFSCampaignVehicle = Me.Retrieve(critCol)
                            If arrFSCampaignVehicle.Count > 0 Then
                                objFSCampaignVehicle = arrFSCampaignVehicle(0)
                                objFSCampaignVehicle.RowStatus = 0
                                objFSCampaignVehicle.LastUpdateBy = m_userPrincipal.Identity.Name
                                objFSCampaignVehicle.LastUpdateTime = Date.Now
                                m_TransactionManager.AddUpdate(objFSCampaignVehicle, m_userPrincipal.Identity.Name)
                            Else
                                

                                objFSCampaignVehicle = New FSCampaignVehicle
                                objFSCampaignVehicle.FSCampaign = fsCampaign
                                objFSCampaignVehicle.VechileType = objVehicleType
                                objFSCampaignVehicle.RowStatus = 0
                                m_TransactionManager.AddInsert(objFSCampaignVehicle, m_userPrincipal.Identity.Name)
                            End If
                        Next
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

        Public Function DeleteTransaction(ByVal fsCampaign As FSCampaign) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim objVechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(m_userPrincipal)
                    Dim arlFSCampaignVehicle As ArrayList = New ArrayList
                    arlFSCampaignVehicle = Me.RetrieveList(fsCampaign.ID)
                    For Each objFSCampaignVehicle As FSCampaignVehicle In arlFSCampaignVehicle
                        objFSCampaignVehicle.RowStatus = 1
                        objFSCampaignVehicle.LastUpdateBy = m_userPrincipal.Identity.Name
                        objFSCampaignVehicle.LastUpdateTime = Date.Now
                        m_TransactionManager.AddUpdate(objFSCampaignVehicle, m_userPrincipal.Identity.Name)
                    Next
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

#End Region

#Region "Custom Method"
        Private Sub m_TransactionManager_Insert(ByVal sender As Object, ByVal InsertArg As TransactionManager.OnInsertArgs)

            If (TypeOf InsertArg.DomainObject Is FSCampaignVehicle) Then
                CType(InsertArg.DomainObject, FSCampaignVehicle).ID = InsertArg.ID
                CType(InsertArg.DomainObject, FSCampaignVehicle).MarkLoaded()

            End If
        End Sub
#End Region

    End Class

End Namespace
