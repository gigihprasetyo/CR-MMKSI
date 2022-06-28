
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AccessoriesSaleDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 7/18/2012 - 10:48:49 AM
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

    Public Class AccessoriesSaleDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAccessoriesSaleDetail"
        Private m_UpdateStatement As String = "up_UpdateAccessoriesSaleDetail"
        Private m_RetrieveStatement As String = "up_RetrieveAccessoriesSaleDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveAccessoriesSaleDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAccessoriesSaleDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim accessoriesSaleDetail As AccessoriesSaleDetail = Nothing
            While dr.Read

                accessoriesSaleDetail = Me.CreateObject(dr)

            End While

            Return accessoriesSaleDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim accessoriesSaleDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim accessoriesSaleDetail As AccessoriesSaleDetail = Me.CreateObject(dr)
                accessoriesSaleDetailList.Add(accessoriesSaleDetail)
            End While

            Return accessoriesSaleDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim accessoriesSaleDetail As AccessoriesSaleDetail = CType(obj, AccessoriesSaleDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, accessoriesSaleDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim accessoriesSaleDetail As AccessoriesSaleDetail = CType(obj, AccessoriesSaleDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@AccessoriesSaleID", DbType.Int32, accessoriesSaleDetail.AccessoriesSaleID)
            'DbCommandWrapper.AddInParameter("@SpartPartMasterID", DbType.Int32, accessoriesSaleDetail.SpartPartMasterID)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Int32, accessoriesSaleDetail.Jumlah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, accessoriesSaleDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, accessoriesSaleDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@AccessoriesSaleID", DbType.Int32, Me.GetRefObject(accessoriesSaleDetail.AccessoriesSale))
            DBCommandWrapper.AddInParameter("@SpartPartMasterID", DbType.Int32, Me.GetRefObject(accessoriesSaleDetail.SparePartMaster))

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

            Dim accessoriesSaleDetail As AccessoriesSaleDetail = CType(obj, AccessoriesSaleDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, accessoriesSaleDetail.ID)
            'DbCommandWrapper.AddInParameter("@AccessoriesSaleID", DbType.Int32, accessoriesSaleDetail.AccessoriesSaleID)
            'DbCommandWrapper.AddInParameter("@SpartPartMasterID", DbType.Int32, accessoriesSaleDetail.SpartPartMasterID)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Int32, accessoriesSaleDetail.Jumlah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, accessoriesSaleDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, accessoriesSaleDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@AccessoriesSaleID", DbType.Int32, Me.GetRefObject(accessoriesSaleDetail.AccessoriesSale))
            DBCommandWrapper.AddInParameter("@SpartPartMasterID", DbType.Int32, Me.GetRefObject(accessoriesSaleDetail.SparePartMaster))



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AccessoriesSaleDetail

            Dim accessoriesSaleDetail As AccessoriesSaleDetail = New AccessoriesSaleDetail

            accessoriesSaleDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("AccessoriesSaleID")) Then accessoriesSaleDetail.AccessoriesSaleID = CType(dr("AccessoriesSaleID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SpartPartMasterID")) Then accessoriesSaleDetail.SpartPartMasterID = CType(dr("SpartPartMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Jumlah")) Then accessoriesSaleDetail.Jumlah = CType(dr("Jumlah"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then accessoriesSaleDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then accessoriesSaleDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then accessoriesSaleDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then accessoriesSaleDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then accessoriesSaleDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("AccessoriesSaleID")) Then accessoriesSaleDetail.AccessoriesSale = New AccessoriesSale(CType(dr("AccessoriesSaleID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("SpartPartMasterID")) Then accessoriesSaleDetail.SparePartMaster = New SparePartMaster(CType(dr("SpartPartMasterID"), Integer))

            Return accessoriesSaleDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(AccessoriesSaleDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AccessoriesSaleDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AccessoriesSaleDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

