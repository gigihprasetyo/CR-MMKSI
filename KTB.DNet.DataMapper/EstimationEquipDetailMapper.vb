#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EstimationEquipDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2009 - 12:54:40
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

    Public Class EstimationEquipDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEstimationEquipDetail"
        Private m_UpdateStatement As String = "up_UpdateEstimationEquipDetail"
        Private m_RetrieveStatement As String = "up_RetrieveEstimationEquipDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveEstimationEquipDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEstimationEquipDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim estimationEquipDetail As EstimationEquipDetail = Nothing
            While dr.Read

                estimationEquipDetail = Me.CreateObject(dr)

            End While

            Return estimationEquipDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim estimationEquipDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim estimationEquipDetail As EstimationEquipDetail = Me.CreateObject(dr)
                estimationEquipDetailList.Add(estimationEquipDetail)
            End While

            Return estimationEquipDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim estimationEquipDetail As EstimationEquipDetail = CType(obj, EstimationEquipDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, estimationEquipDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim estimationEquipDetail As EstimationEquipDetail = CType(obj, EstimationEquipDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Harga", DbType.Currency, estimationEquipDetail.Harga)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Decimal, estimationEquipDetail.Discount)
            DbCommandWrapper.AddInParameter("@TotalForecast", DbType.Int32, estimationEquipDetail.TotalForecast)
            DbCommandWrapper.AddInParameter("@EstimationUnit", DbType.Int32, estimationEquipDetail.EstimationUnit)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, estimationEquipDetail.Status)
            DbCommandWrapper.AddInParameter("@ConfirmedDate", DbType.DateTime, estimationEquipDetail.ConfirmedDate)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, estimationEquipDetail.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, estimationEquipDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, estimationEquipDetail.CreatedTime)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DBCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(estimationEquipDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@EstimationEquipHeaderID", DbType.Int32, Me.GetRefObject(estimationEquipDetail.EstimationEquipHeader))

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

            Dim estimationEquipDetail As EstimationEquipDetail = CType(obj, EstimationEquipDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, estimationEquipDetail.ID)
            DbCommandWrapper.AddInParameter("@Harga", DbType.Currency, estimationEquipDetail.Harga)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Decimal, estimationEquipDetail.Discount)
            DbCommandWrapper.AddInParameter("@TotalForecast", DbType.Int32, estimationEquipDetail.TotalForecast)
            DbCommandWrapper.AddInParameter("@EstimationUnit", DbType.Int32, estimationEquipDetail.EstimationUnit)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, estimationEquipDetail.Status)
            DbCommandWrapper.AddInParameter("@ConfirmedDate", DbType.DateTime, estimationEquipDetail.ConfirmedDate)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, estimationEquipDetail.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, estimationEquipDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, estimationEquipDetail.CreatedBy)
            'DBCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(estimationEquipDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@EstimationEquipHeaderID", DbType.Int32, Me.GetRefObject(estimationEquipDetail.EstimationEquipHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EstimationEquipDetail

            Dim estimationEquipDetail As EstimationEquipDetail = New EstimationEquipDetail

            estimationEquipDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Harga")) Then estimationEquipDetail.Harga = CType(dr("Harga"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then estimationEquipDetail.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalForecast")) Then estimationEquipDetail.TotalForecast = CType(dr("TotalForecast"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EstimationUnit")) Then estimationEquipDetail.EstimationUnit = CType(dr("EstimationUnit"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then estimationEquipDetail.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedDate")) Then estimationEquipDetail.ConfirmedDate = CType(dr("ConfirmedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then estimationEquipDetail.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then estimationEquipDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then estimationEquipDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then estimationEquipDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then estimationEquipDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then estimationEquipDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                estimationEquipDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EstimationEquipHeaderID")) Then
                estimationEquipDetail.EstimationEquipHeader = New EstimationEquipHeader(CType(dr("EstimationEquipHeaderID"), Integer))
            End If

            Return estimationEquipDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(EstimationEquipDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EstimationEquipDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EstimationEquipDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

