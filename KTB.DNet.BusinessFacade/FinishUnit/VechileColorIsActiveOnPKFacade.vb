
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
'// Generated on 04/03/2020 - 13:24:40
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
Imports KTB.DNet.Framework
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling


#End Region

Namespace KTB.DNET.BusinessFacade

    Public Class VechileColorIsActiveOnPKFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_VechileColorIsActiveOnPKMapper As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_VechileColorIsActiveOnPKMapper = MapperFactory.GetInstance.GetMapper(GetType(VechileColorIsActiveOnPK).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, New TransactionManager.OnInsertEventHandler(AddressOf m_TransactionManager_Insert)
        End Sub

#End Region

#Region "Retrieve"

        Public Function Retrieve(ByVal id As Integer) As VechileColorIsActiveOnPK
            Return CType(m_VechileColorIsActiveOnPKMapper.Retrieve(id), VechileColorIsActiveOnPK)
        End Function

        Public Function Retrieve(ByVal Code As String) As VechileColorIsActiveOnPK
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColorIsActiveOnPKCode", MatchType.Exact, Code))

            Dim VechileColorIsActiveOnPKColl As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias)
            If (VechileColorIsActiveOnPKColl.Count > 0) Then
                Return CType(VechileColorIsActiveOnPKColl(0), VechileColorIsActiveOnPK)
            End If
            Return New VechileColorIsActiveOnPK
        End Function

        Public Function RetrieveByVechileColorID(ByVal _vechileColorID As Integer, ByVal _assyYear As Integer, ByVal _modelYear As Integer) As VechileColorIsActiveOnPK
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.ID", MatchType.Exact, _vechileColorID))
            criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ProductionYear", MatchType.Exact, _assyYear))
            criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ModelYear", MatchType.Exact, _modelYear))

            Dim VechileColorIsActiveOnPKColl As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias)
            If (VechileColorIsActiveOnPKColl.Count > 0) Then
                Return CType(VechileColorIsActiveOnPKColl(0), VechileColorIsActiveOnPK)
            End If
            Return New VechileColorIsActiveOnPK
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias)
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VechileColorIsActiveOnPKMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileColorIsActiveOnPK), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VechileColorIsActiveOnPKMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileColorIsActiveOnPK), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VechileColorIsActiveOnPKMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim _VechileColorIsActiveOnPK As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias)
            Return _VechileColorIsActiveOnPK
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VechileColorIsActiveOnPKColl As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VechileColorIsActiveOnPKColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String, ByVal criterias As CriteriaComposite) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Search.Sort(GetType(VechileColorIsActiveOnPK), SortColumn, sortDirection))
            Dim VechileColorIsActiveOnPKColl As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
            Return VechileColorIsActiveOnPKColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal SortColumn As String, ByVal sortDirection As String) As ArrayList
            Dim VechileColorIsActiveOnPKColl As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return VechileColorIsActiveOnPKColl
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VechileColorIsActiveOnPKColl As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return VechileColorIsActiveOnPKColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VechileColorIsActiveOnPKColl As ArrayList = m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), columnName, matchOperator, columnValue))
            Return VechileColorIsActiveOnPKColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileColorIsActiveOnPK), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), columnName, matchOperator, columnValue))

            Return m_VechileColorIsActiveOnPKMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColorIsActiveOnPKCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(VechileColorIsActiveOnPK), "VechileColorIsActiveOnPKCode", AggregateType.Count)
            Return CType(m_VechileColorIsActiveOnPKMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As VechileColorIsActiveOnPK) As Integer
            Dim iReturn As Integer = -2
            Try
                iReturn = m_VechileColorIsActiveOnPKMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As VechileColorIsActiveOnPK) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_VechileColorIsActiveOnPKMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As VechileColorIsActiveOnPK)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_VechileColorIsActiveOnPKMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As VechileColorIsActiveOnPK)
            Try
                m_VechileColorIsActiveOnPKMapper.Delete(objDomain)
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
            If (TypeOf InsertArg.DomainObject Is VechileColorIsActiveOnPK) Then
                CType(InsertArg.DomainObject, VechileColorIsActiveOnPK).id = InsertArg.ID
                CType(InsertArg.DomainObject, VechileColorIsActiveOnPK).MarkLoaded()
            End If
        End Sub

        Function UpdateTransaction(ByVal arrVechileColorIsActiveOnPK As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    Dim performTransaction As Boolean = True
                    Dim ObjMapper As IMapper

                    If Not IsNothing(arrVechileColorIsActiveOnPK) Then
                        If arrVechileColorIsActiveOnPK.Count > 0 Then
                            For Each item As VechileColorIsActiveOnPK In arrVechileColorIsActiveOnPK
                                If item.id <> 0 Then
                                    m_TransactionManager.AddUpdate(item, m_userPrincipal.Identity.Name)
                                Else
                                    m_TransactionManager.AddInsert(item, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
                    End If

                    If performTransaction Then
                        m_TransactionManager.PerformTransaction()
                        returnValue = 9
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

        Public Function RetrieveAssyYearByVechileTypeGeneralID(ByVal _vechileTypeGeneralID As String) As ArrayList
            Dim dtbs As New DataSet
            Dim result As ArrayList = New ArrayList
            Dim strCommand As String = String.Empty

            strCommand += " select "
            strCommand += " distinct "
            strCommand += " a.ProductionYear "
            strCommand += " from vechileColorisActiveOnPK a with (nolock) "
            strCommand += " join VechileTypeGeneral b with (nolock) on b.ID = a.VechileTypeGeneralID "
            strCommand += " where "
            strCommand += " a.rowstatus = 0 "
            strCommand += " and b.rowstatus = 0 "
            strCommand += " and a.Status <> 'X' "
            strCommand += " and b.ID = " & _vechileTypeGeneralID
            strCommand += " Order by a.ProductionYear"

            dtbs = m_VechileColorIsActiveOnPKMapper.RetrieveDataSet(strCommand)
            Dim k% = 1
            For Each dr As DataRow In dtbs.Tables(0).Rows
                Dim obj As New VechileColorIsActiveOnPK
                obj.id = k
                obj.ProductionYear = dr("ProductionYear")
                result.Add(obj)
                k += 1
            Next
            Return result
        End Function

        Public Function RetrieverModelYearByVechileTypeGeneralIDAndAssyYear(ByVal _vechileTypeGeneralID As String, ByVal _assyYear As String) As ArrayList
            Dim dtbs As New DataSet
            Dim result As ArrayList = New ArrayList
            Dim strCommand As String = String.Empty

            strCommand += " select "
            strCommand += " distinct "
            strCommand += " a.ModelYear "
            strCommand += " from vechileColorisActiveOnPK a with (nolock) "
            strCommand += " join VechileTypeGeneral b with (nolock) on b.ID = a.VechileTypeGeneralID "
            strCommand += " where "
            strCommand += " a.rowstatus = 0 "
            strCommand += " and b.rowstatus = 0 "
            strCommand += " and a.Status <> 'X' "
            strCommand += " and b.ID = " & _vechileTypeGeneralID
            strCommand += " and a.ProductionYear = '" & _assyYear & "' "
            strCommand += " Order by a.ModelYear"

            dtbs = m_VechileColorIsActiveOnPKMapper.RetrieveDataSet(strCommand)
            Dim k% = 1
            For Each dr As DataRow In dtbs.Tables(0).Rows
                Dim obj As New VechileColorIsActiveOnPK
                obj.id = k
                obj.ModelYear = dr("ModelYear")
                result.Add(obj)
                k += 1
            Next
            Return result
        End Function

        Public Function RetrieveByVechileTypeGeneralID(ByVal _vechileTypeGeneralID As String) As ArrayList
            Dim result As ArrayList = New ArrayList
            Dim dtbs As New DataSet

            Dim strCommand As String = String.Empty
            Dim strCommand2 As String = String.Empty
            Dim strIntersect As String = " INTERSECT "

            strCommand += " select "
            strCommand += " distinct "
            strCommand += " a.ModelYear "
            strCommand += " from vechileColorisActiveOnPK a with (nolock) "
            strCommand += " join VechileTypeGeneral b with (nolock) on b.ID = a.VechileTypeGeneralID "
            strCommand += " where "
            strCommand += " a.rowstatus = 0 "
            strCommand += " and b.rowstatus = 0 "
            strCommand += " and a.Status <> 'X' "

            Dim idx% = 0
            Dim strCommandExec As String = "select * from ("
            For Each strID As String In _vechileTypeGeneralID.Split(";")
                strCommand2 = " and b.ID = " & strID & " "
                strCommandExec += strCommand + strCommand2
                If (_vechileTypeGeneralID.Split(";").Length - 1) <> idx Then
                    strCommandExec += strIntersect
                End If
                idx += 1
            Next
            strCommandExec += ") a Order by a.ModelYear"

            dtbs = m_VechileColorIsActiveOnPKMapper.RetrieveDataSet(strCommandExec)
            Dim k% = 1
            For Each dr As DataRow In dtbs.Tables(0).Rows
                Dim obj As New VechileColorIsActiveOnPK
                obj.id = k
                obj.ModelYear = dr("ModelYear")
                result.Add(obj)
                k += 1
            Next
            Return result
        End Function

        Public Function RetrieveByVechileTypeGeneralIDAndModelYear(ByVal _vechileTypeGeneralID As String, ByVal _modelYear As String) As ArrayList
            Dim result As ArrayList = New ArrayList
            Dim dtbs As New DataSet

            Dim strCommand As String = String.Empty
            Dim strCommand2 As String = String.Empty
            Dim strIntersect As String = " INTERSECT "

            strCommand += " select "
            strCommand += " distinct "
            strCommand += " a.ProductionYear "
            strCommand += " from vechileColorisActiveOnPK a with (nolock) "
            strCommand += " join VechileTypeGeneral b with (nolock) on b.ID = a.VechileTypeGeneralID "
            strCommand += " where "
            strCommand += " a.rowstatus = 0 "
            strCommand += " and b.rowstatus = 0 "
            strCommand += " and a.Status <> 'X' "

            Dim idx% = 0
            Dim strCommandExec As String = String.Empty
            If _vechileTypeGeneralID.Trim <> "" Then
                strCommandExec = "select * from ("
                For Each strID As String In _vechileTypeGeneralID.Split(";")
                    Dim strModelYears As String = String.Empty
                    If _modelYear.Trim <> "" Then
                        strModelYears = " and a.ModelYear in ("
                        Dim strModelYear As String = String.Empty
                        For Each str As String In _modelYear.Split(";")
                            If strModelYear = "" Then
                                strModelYear = str
                            Else
                                strModelYear += "," & str
                            End If
                            strModelYears += strModelYear
                        Next
                        strModelYears += ")"
                    End If
                    strCommand2 = " and b.ID = " & strID & strModelYears
                    strCommandExec += strCommand + strCommand2
                    If (_vechileTypeGeneralID.Split(";").Length - 1) <> idx Then
                        strCommandExec += strIntersect
                    End If
                    idx += 1
                Next
                strCommandExec += ") a Order by a.ProductionYear"

                dtbs = m_VechileColorIsActiveOnPKMapper.RetrieveDataSet(strCommandExec)
                Dim k% = 1
                For Each dr As DataRow In dtbs.Tables(0).Rows
                    Dim obj As New VechileColorIsActiveOnPK
                    obj.id = k
                    obj.ProductionYear = dr("ProductionYear")
                    result.Add(obj)
                    k += 1
                Next
            End If
            Return result
        End Function

#End Region

    End Class

End Namespace

