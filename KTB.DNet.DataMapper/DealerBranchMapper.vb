
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : DealerBranch Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2018 - 18:48:46
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class DealerBranchMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerBranch"
        Private m_UpdateStatement As String = "up_UpdateDealerBranch"
        Private m_RetrieveStatement As String = "up_RetrieveDealerBranch"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerBranchList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerBranch"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerBranch As DealerBranch = Nothing
            While dr.Read

                dealerBranch = Me.CreateObject(dr)

            End While

            Return dealerBranch

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerBranchList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerBranch As DealerBranch = Me.CreateObject(dr)
                dealerBranchList.Add(dealerBranch)
            End While

            Return dealerBranchList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerBranch As DealerBranch = CType(obj, DealerBranch)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, dealerBranch.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerBranch As DealerBranch = CType(obj, DealerBranch)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, dealerBranch.Name)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, dealerBranch.Status)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, dealerBranch.Address)
            DbCommandWrapper.AddInParameter("@ZipCode", DbType.AnsiString, dealerBranch.ZipCode)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealerBranch.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, dealerBranch.Fax)
            DbCommandWrapper.AddInParameter("@Website", DbType.AnsiString, dealerBranch.Website)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, dealerBranch.Email)
            DbCommandWrapper.AddInParameter("@TypeBranch", DbType.AnsiString, dealerBranch.TypeBranch)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, dealerBranch.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@Term1", DbType.AnsiString, dealerBranch.Term1)
            DbCommandWrapper.AddInParameter("@Term2", DbType.AnsiString, dealerBranch.Term2)

            DbCommandWrapper.AddInParameter("@BranchAssignmentNo", DbType.AnsiString, dealerBranch.BranchAssignmentNo)
            DbCommandWrapper.AddInParameter("@BranchAssignmentDate", DbType.DateTime, dealerBranch.BranchAssignmentDate)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, dealerBranch.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, dealerBranch.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, dealerBranch.SparepartFlag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerBranch.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerBranch.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerBranch.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(dealerBranch.City))
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(dealerBranch.Province))


            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(dealerBranch.MainArea))
            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(dealerBranch.Area1))
            DbCommandWrapper.AddInParameter("@Area2ID", DbType.Int32, Me.GetRefObject(dealerBranch.Area2))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerBranch As DealerBranch = CType(obj, DealerBranch)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, dealerBranch.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, dealerBranch.Name)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, dealerBranch.Status)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, dealerBranch.Address)
            DbCommandWrapper.AddInParameter("@ZipCode", DbType.AnsiString, dealerBranch.ZipCode)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealerBranch.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, dealerBranch.Fax)
            DbCommandWrapper.AddInParameter("@Website", DbType.AnsiString, dealerBranch.Website)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, dealerBranch.Email)
            DbCommandWrapper.AddInParameter("@TypeBranch", DbType.AnsiString, dealerBranch.TypeBranch)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, dealerBranch.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@Term1", DbType.AnsiString, dealerBranch.Term1)
            DbCommandWrapper.AddInParameter("@Term2", DbType.AnsiString, dealerBranch.Term2)

            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(dealerBranch.MainArea))
            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(dealerBranch.Area1))
            DbCommandWrapper.AddInParameter("@Area2ID", DbType.Int32, Me.GetRefObject(dealerBranch.Area2))
            DbCommandWrapper.AddInParameter("@BranchAssignmentNo", DbType.AnsiString, dealerBranch.BranchAssignmentNo)
            DbCommandWrapper.AddInParameter("@BranchAssignmentDate", DbType.DateTime, dealerBranch.BranchAssignmentDate)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, dealerBranch.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, dealerBranch.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, dealerBranch.SparepartFlag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerBranch.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerBranch.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dealerBranch.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(dealerBranch.City))
            DbCommandWrapper.AddInParameter("@ProvinceID", DbType.Int32, Me.GetRefObject(dealerBranch.Province))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerBranch

            Dim dealerBranch As DealerBranch = New DealerBranch

            dealerBranch.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then dealerBranch.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then dealerBranch.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then dealerBranch.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ZipCode")) Then dealerBranch.ZipCode = dr("ZipCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then dealerBranch.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then dealerBranch.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Website")) Then dealerBranch.Website = dr("Website").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then dealerBranch.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TypeBranch")) Then dealerBranch.TypeBranch = dr("TypeBranch").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then dealerBranch.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Term1")) Then dealerBranch.Term1 = dr("Term1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Term2")) Then dealerBranch.Term2 = dr("Term2").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("BranchAssignmentNo")) Then dealerBranch.BranchAssignmentNo = dr("BranchAssignmentNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BranchAssignmentDate")) Then dealerBranch.BranchAssignmentDate = CType(dr("BranchAssignmentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnitFlag")) Then dealerBranch.SalesUnitFlag = dr("SalesUnitFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceFlag")) Then dealerBranch.ServiceFlag = dr("ServiceFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartFlag")) Then dealerBranch.SparepartFlag = dr("SparepartFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerBranch.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerBranch.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerBranch.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerBranch.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerBranch.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerBranch.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                dealerBranch.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceID")) Then
                dealerBranch.Province = New Province(CType(dr("ProvinceID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MainAreaID")) Then
                dealerBranch.MainArea = New MainArea(CType(dr("MainAreaID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("Area1ID")) Then
                dealerBranch.Area1 = New Area1(CType(dr("Area1ID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("Area2ID")) Then
                dealerBranch.Area2 = New Area2(CType(dr("Area2ID"), Short))
            End If

            Return dealerBranch

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerBranch) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerBranch), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerBranch).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

