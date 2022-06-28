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
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 2:38:25 PM
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

Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports KTB.DNET.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Collections.Generic
Imports System.Data.SqlClient


#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit

    Public Class VechileTypeFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_userPrincipal As IPrincipal = Nothing
        Private m_VechileTypeMapper As IMapper
        Private m_VechileColorIsActiveOnPK As IMapper

        Private m_TransactionManager As TransactionManager

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)

            Me.m_userPrincipal = userPrincipal
            Me.m_VechileTypeMapper = MapperFactory.GetInstance.GetMapper(GetType(VechileType).ToString)
            Me.m_VechileColorIsActiveOnPK = MapperFactory.GetInstance.GetMapper(GetType(VechileColorIsActiveOnPK).ToString)

            Me.m_TransactionManager = New TransactionManager
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            AddHandler m_TransactionManager.Update, New TransactionManager.OnUpdateEventHandler(AddressOf m_TransactionManager_Update)
        End Sub


#End Region

#Region "Retrieve"


        Public Function RetrieveByVehicleModelId(ByVal intVehicleModelId As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CInt(DBRowStatus.Active)))
            crits.opAnd(New Criteria(GetType(VechileType), "VechileModel", MatchType.Exact, intVehicleModelId))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), sortColumn, sortDirection))
            Return RetrieveByCriteria(crits, sortColl)
        End Function

        Public Function Retrieve(ByVal ID As Integer) As VechileType
            Return CType(m_VechileTypeMapper.Retrieve(ID), VechileType)
        End Function

        Public Function Retrieve(ByVal Code As String) As VechileType
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, Code))

            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias)
            If (VechileTypeColl.Count > 0) Then
                Return CType(VechileTypeColl(0), VechileType)
            End If
            Return New VechileType
        End Function

        Public Function Retrieve(ByVal criterias As ICriteria) As ArrayList
            Return m_VechileTypeMapper.RetrieveByCriteria(criterias)
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria) As VechileType

            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias)
            If (VechileTypeColl.Count > 0) Then
                Return CType(VechileTypeColl(0), VechileType)
            End If
            Return New VechileType
        End Function
        Public Function Retrieve(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VechileTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Function RetrieveList() As ArrayList
            Return m_VechileTypeMapper.RetrieveList
        End Function

        Public Function RetrieveList(ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VechileTypeMapper.RetrieveList(sortColl)
        End Function

        Public Function RetrieveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VechileTypeMapper.RetrieveList(sortColl, pageNumber, pageSize, totalRow)

        End Function

        Public Function RetrieveActiveList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias)
            Return VechileTypeColl
        End Function

        Public Function RetrieveActiveSortList(Optional ByVal intModelID As Integer = 0) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))

            If intModelID <> 0 Then
                Dim strSql As String = "select VechileModelID from [SubCategoryVehicleToModel] where SubCategoryVehicleID = " & intModelID
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
            End If

            Dim sortColl As SortCollection = New SortCollection

            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))

            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, sortColl)
            Return VechileTypeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)

            Return VechileTypeColl
        End Function

        Public Function RetrieveActiveList(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, _
              ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Return m_VechileTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If
            Return m_VechileTypeMapper.RetrieveByCriteria(Criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function
        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer) As ArrayList
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            Return VechileTypeColl
        End Function
        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As String, ByVal matchOperator As MatchType, ByVal columnValue As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, Nothing, pageNumber, pageSize, totalRow)
            criterias.opAnd(New Criteria(GetType(VechileType), columnName, matchOperator, columnValue))
            Return VechileTypeColl
        End Function

        Public Function RetrieveWithOneCriteria(ByVal pageNumber As Integer, ByVal pageSize As Integer, ByRef totalRow As Integer, ByVal columnName As Integer, ByVal matchOperator As MatchType, ByVal columnValue As String, ByVal sortColumn As String, ByVal sortDirection As Sort.SortDirection) As ArrayList
            Dim sortColl As SortCollection = New SortCollection

            If (Not IsNothing(sortColumn)) And (Not IsNothing(sortColumn)) Then
                sortColl.Add(New Sort(GetType(VechileType), sortColumn, sortDirection))
            Else
                sortColl = Nothing
            End If

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), columnName, matchOperator, columnValue))

            Return m_VechileTypeMapper.RetrieveByCriteria(criterias, sortColl, pageNumber, pageSize, totalRow)
        End Function

#End Region

#Region "Transaction/Other Public Method"

        Public Function ValidateCode(ByVal Code As String) As Integer
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, Code))
            Dim agg As Aggregate = New Aggregate(GetType(VechileType), "VechileTypeCode", AggregateType.Count)
            Return CType(m_VechileTypeMapper.RetrieveScalar(agg, crit), Integer)
        End Function

        Public Function Insert(ByVal objDomain As VechileType) As Integer
            Dim iReturn As Integer = -2
            Try
                m_VechileTypeMapper.Insert(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                Dim s As String = ex.Message
                iReturn = -1
            End Try
            Return iReturn

        End Function

        Public Function Update(ByVal objDomain As VechileType) As Integer
            Dim nResult As Integer = -1
            Try
                nResult = m_VechileTypeMapper.Update(objDomain, m_userPrincipal.Identity.Name)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
            Return nResult
        End Function

        Public Sub Delete(ByVal objDomain As VechileType)
            Dim nResult As Integer = -1
            Try
                nResult = objDomain.RowStatus = CType(DBRowStatus.Deleted, Short)
                m_VechileTypeMapper.Delete(objDomain)
            Catch ex As Exception
                nResult = -1
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                If rethrow Then
                    Throw
                End If
            End Try
        End Sub

        Public Sub DeleteFromDB(ByVal objDomain As VechileType)
            Try
                m_VechileTypeMapper.Delete(objDomain)
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                If rethrow Then
                    Throw
                End If
            End Try
        End Sub


#End Region

#Region "Custom Method"
        Public Function RetrieveModelList(ByVal category As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, category))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, sortColl)
            Return VechileTypeColl
        End Function

        Public Function RetrieveModelList(ByVal category As Integer, ByVal assemblyyear As Integer, ByVal intModelID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSql As String = String.Format(
                String.Join(
                Environment.NewLine,
"SELECT VechileTypeID FROM VechileColorIsActiveOnPK WITH (NOLOCK) ",
"INNER JOIN VechileColor on VechileColorIsActiveOnPK.VehicleColorID=VechileColor.id",
"WHERE (VechileColorIsActiveOnPK.RowStatus = 0",
"AND VechileColorIsActiveOnPK.ProductionYear = {0}",
"AND VechileColorIsActiveOnPK.Status = 1",
"AND VechileColor.Status <> 'X')"
), assemblyyear)

            criterias.opAnd(New Criteria(GetType(VechileType), "ID", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, category))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))

            If intModelID <> 0 Then
                strSql = "select VechileModelID from [SubCategoryVehicleToModel] where SubCategoryVehicleID = " & intModelID
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.InSet, "(" & strSql & ")"))
            End If

            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias)
            Return VechileTypeColl
        End Function

        Public Function RetrieveModelListOnPK(ByVal categoryID As Integer, ByVal strVechileTypeCode As String, ByVal strDescription As String) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If categoryID <> 0 Then
                criterias.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, categoryID))
            End If
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            If (strVechileTypeCode.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, strVechileTypeCode))
            End If
            If (strDescription.Trim <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.Partial, strDescription))
            End If

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, sortColl)
            Return VechileTypeColl
        End Function

        Public Function RetrieveModelProductList(ByVal ProductCategoryID As Integer) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "ProductCategory.ID", MatchType.Exact, ProductCategoryID))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, sortColl)
            Return VechileTypeColl
        End Function

        Public Function RetrieveModelList() As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.No, "X"))
            Dim sortColl As SortCollection = New SortCollection
            If (Not IsNothing("VechileTypeCode")) Then
                sortColl.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
            Else
                sortColl = Nothing
            End If
            Dim VechileTypeColl As ArrayList = m_VechileTypeMapper.RetrieveByCriteria(criterias, sortColl)
            Return VechileTypeColl
        End Function

        Private Function RetrieveVechileModelByID(ByVal id As Integer) As VechileModel
            Dim _vm As VechileModel = New VechileModelFacade(Me.m_userPrincipal).Retrieve(id)
            Return _vm
        End Function

        Public Function RetrieveByCriteria(ByVal criterias As ICriteria, ByVal sorts As ICollection) As ArrayList
            Return m_VechileTypeMapper.RetrieveByCriteria(criterias, sorts)
        End Function

        Public Sub InsertWithTransactionManagerHandler(ByVal sender As Object, ByVal args As TransactionManager.OnInsertArgs)

            ' set the object ID from db returned id
            If (args.DomainObject.GetType = GetType(VechileType)) Then
                CType(args.DomainObject, VechileType).ID = args.ID
                CType(args.DomainObject, VechileType).MarkLoaded()
            ElseIf (args.DomainObject.GetType = GetType(VechileColor)) Then
                CType(args.DomainObject, VechileColor).ID = args.ID
                CType(args.DomainObject, VechileColor).MarkLoaded()
            ElseIf (args.DomainObject.GetType = GetType(VechileColorIsActiveOnPK)) Then
                CType(args.DomainObject, VechileColorIsActiveOnPK).id = args.ID
                CType(args.DomainObject, VechileColorIsActiveOnPK).MarkLoaded()
            End If

        End Sub

        Public Function InsertWithTransactionManager(ByVal vechileType As VechileType, ByVal listOfVechileColor As ArrayList) As Integer
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert vehicle Type
                    Me.m_TransactionManager.AddInsert(vechileType, m_userPrincipal.Identity.Name)
                    ' add command to insert vehicle Color
                    For Each vechileColor As VechileColor In listOfVechileColor
                        vechileColor.VechileType = vechileType
                        Me.m_TransactionManager.AddInsert(vechileColor, m_userPrincipal.Identity.Name)
                        Save_VechileColorIsActiveOnPK(vechileColor, m_TransactionManager, ManipulationFlag.Insert)
                    Next
                    Me.m_TransactionManager.PerformTransaction()
                    result = vechileType.ID

                    Return result
                Catch sqlException As SqlException
                    Throw sqlException
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try
            End If
            Return result

        End Function

        Public Function UpdateWithTransactionManager(ByVal vechileType As VechileType, ByVal listOfVechileColor As ArrayList) As Integer
            ' mark as loaded to prevent it loads from db
            vechileType.MarkLoaded()
            For Each vechileColor As VechileColor In listOfVechileColor
                vechileColor.MarkLoaded()
            Next
            ' set default result
            Dim result As Integer = -1
            Me.m_TransactionManager = New TransactionManager()
            AddHandler m_TransactionManager.Insert, AddressOf Me.InsertWithTransactionManagerHandler
            If Me.IsTaskFree Then
                Try
                    Me.SetTaskLocking()

                    ' add command to insert vehicle color
                    For Each vechileColor As VechileColor In listOfVechileColor
                        If (vechileColor.ID <> 0) Then
                            If (vechileColor.LastUpdateBy.ToLower <> "not update") Then
                                If vechileColor.Status.ToLower().Trim() = "x" Then
                                    Save_VechileColorIsActiveOnPK(vechileColor, m_TransactionManager, ManipulationFlag.Update)
                                End If
                            End If
                            m_TransactionManager.AddUpdate(vechileColor, m_userPrincipal.Identity.Name)
                        Else
                            m_TransactionManager.AddInsert(vechileColor, m_userPrincipal.Identity.Name)
                            Save_VechileColorIsActiveOnPK(vechileColor, m_TransactionManager, ManipulationFlag.Insert)
                        End If

                        vechileColor.MarkLoaded()

                    Next
                    ' add command to update spk
                    If (vechileType.LastUpdateBy.ToLower <> "not update") Then
                        m_TransactionManager.AddUpdate(vechileType, m_userPrincipal.Identity.Name)
                    End If
                    m_TransactionManager.PerformTransaction()
                    result = vechileType.ID
                Catch ex As Exception
                    Throw ex
                Finally
                    Me.RemoveTaskLocking()
                End Try

            End If

            Return result
        End Function

        Private Sub m_TransactionManager_Update(ByVal sender As Object, ByVal UpdateArg As TransactionManager.OnUpdateArgs)
            If (TypeOf UpdateArg.DomainObject Is VechileType) Then
                CType(UpdateArg.DomainObject, VechileType).ID = UpdateArg.ID
                CType(UpdateArg.DomainObject, VechileType).MarkLoaded()
            End If
        End Sub


        Public Function UpdateWithTransaction(ByVal dataFor As ArrayList) As Integer
            Dim returnValue As Integer = -1
            If (Me.IsTaskFree()) Then
                Try
                    Me.SetTaskLocking()
                    If Not IsNothing(dataFor) Then
                        If dataFor.Count > 0 Then
                            For Each data As VechileType In dataFor
                                If data.ID > 0 Then
                                    m_TransactionManager.AddUpdate(data, m_userPrincipal.Identity.Name)
                                End If
                            Next
                        End If
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


        Private Enum ManipulationFlag
            Insert
            Update
        End Enum
        Private Sub Save_VechileColorIsActiveOnPK(ByVal _VechileColor As VechileColor, ByVal _TransactionManager As TransactionManager, ByVal _ManipulationFlag As ManipulationFlag)
            Dim _VechileColorIsActiveOnPK As VechileColorIsActiveOnPK = New VechileColorIsActiveOnPK()
            Select Case _ManipulationFlag
                Case ManipulationFlag.Insert
                    _VechileColorIsActiveOnPK.VechileColor = _VechileColor
                    _VechileColorIsActiveOnPK.Status = If(_VechileColor.Status.ToLower().Trim() = "x", 0, 1)
                    _VechileColorIsActiveOnPK.ProductionYear = Date.Now.Year
                    _TransactionManager.AddInsert(_VechileColorIsActiveOnPK, m_userPrincipal.Identity.Name)

                Case ManipulationFlag.Update
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileColor.ID", MatchType.Exact, _VechileColor.ID))
                    Dim arrVechileColorIsActiveOnPK As ArrayList = m_VechileColorIsActiveOnPK.RetrieveByCriteria(criterias)
                    If (arrVechileColorIsActiveOnPK.Count > 0) Then
                        '_VechileColorIsActiveOnPK = CType(arrVechileColorIsActiveOnPK(0), VechileColorIsActiveOnPK)
                        For Each objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK In arrVechileColorIsActiveOnPK
                            objVechileColorIsActiveOnPK.Status = If(_VechileColor.Status.ToLower().Trim() = "x", 0, 1)
                            _TransactionManager.AddUpdate(objVechileColorIsActiveOnPK, m_userPrincipal.Identity.Name)
                        Next
                    End If
            End Select
        End Sub

        Public Function RetrieveUsingSP(ByVal SPName As String, ByVal Param As List(Of SqlClient.SqlParameter)) As DataTable
            Dim arr As DataSet
            arr = m_VechileTypeMapper.RetrieveDataSet(SPName, New ArrayList(Param))

            If arr.Tables.Count > 0 Then
                Return arr.Tables(0)
            Else
                Return Nothing
            End If

        End Function
#End Region

    End Class

End Namespace

