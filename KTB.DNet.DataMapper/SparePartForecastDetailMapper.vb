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
    Public Class SparePartForecastDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartForecastDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartForecastDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartForecastDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartForecastDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartForecastDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartForecastDetail As SparePartForecastDetail = Nothing
            While dr.Read

                sparePartForecastDetail = Me.CreateObject(dr)

            End While

            Return sparePartForecastDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartForecastDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartForecastDetail As SparePartForecastDetail = Me.CreateObject(dr)
                sparePartForecastDetailList.Add(sparePartForecastDetail)
            End While

            Return sparePartForecastDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForecastDetail As SparePartForecastDetail = CType(obj, SparePartForecastDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForecastDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForecastDetail As SparePartForecastDetail = CType(obj, SparePartForecastDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, sparePartForecastDetail.RequestQty)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, sparePartForecastDetail.RequestDate)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, sparePartForecastDetail.ApprovedQty)
            DbCommandWrapper.AddInParameter("@SendDate", DbType.DateTime, sparePartForecastDetail.SendDate)
            DbCommandWrapper.AddInParameter("@NoAWB", DbType.AnsiString, sparePartForecastDetail.NoAWB)
            DbCommandWrapper.AddInParameter("@SoNumber", DbType.AnsiString, sparePartForecastDetail.SoNumber)
            DbCommandWrapper.AddInParameter("@DoNumber", DbType.AnsiString, sparePartForecastDetail.DoNumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartForecastDetail.BillingNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForecastDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForecastDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, sparePartForecastDetail.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartForecastHeaderID", DbType.Int32, Me.GetRefObject(sparePartForecastDetail.SparePartForecastHeader))
            DbCommandWrapper.AddInParameter("@SparePartForecastMasterID", DbType.Int32, Me.GetRefObject(sparePartForecastDetail.SparePartForecastMaster))

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

            Dim sparePartForecastDetail As SparePartForecastDetail = CType(obj, SparePartForecastDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForecastDetail.ID)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, sparePartForecastDetail.RequestQty)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, sparePartForecastDetail.RequestDate)
            DbCommandWrapper.AddInParameter("@ApprovedQty", DbType.Int32, sparePartForecastDetail.ApprovedQty)
            DbCommandWrapper.AddInParameter("@SendDate", DbType.DateTime, sparePartForecastDetail.SendDate)
            DbCommandWrapper.AddInParameter("@NoAWB", DbType.AnsiString, sparePartForecastDetail.NoAWB)
            DbCommandWrapper.AddInParameter("@SoNumber", DbType.AnsiString, sparePartForecastDetail.SoNumber)
            DbCommandWrapper.AddInParameter("@DoNumber", DbType.AnsiString, sparePartForecastDetail.DoNumber)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartForecastDetail.BillingNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForecastDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForecastDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartForecastDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartForecastHeaderID", DbType.Int32, Me.GetRefObject(sparePartForecastDetail.SparePartForecastHeader))
            DbCommandWrapper.AddInParameter("@SparePartForecastMasterID", DbType.Int32, Me.GetRefObject(sparePartForecastDetail.SparePartForecastMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartForecastDetail

            Dim sparePartForecastDetail As SparePartForecastDetail = New SparePartForecastDetail

            sparePartForecastDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestQty")) Then sparePartForecastDetail.RequestQty = CType(dr("RequestQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then sparePartForecastDetail.RequestDate = CType(dr("RequestDAte"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedQty")) Then sparePartForecastDetail.ApprovedQty = CType(dr("ApprovedQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SendDate")) Then sparePartForecastDetail.SendDate = CType(dr("SendDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NoAWB")) Then sparePartForecastDetail.NoAWB = dr("NoAWB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SoNumber")) Then sparePartForecastDetail.SoNumber = dr("SoNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DoNumber")) Then sparePartForecastDetail.DoNumber = dr("DoNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then sparePartForecastDetail.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartForecastDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartForecastDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartForecastDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartForecastDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then sparePartForecastDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then sparePartForecastDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartForecastHeaderID")) Then
                sparePartForecastDetail.SparePartForecastHeader = New SparePartForecastHeader(CType(dr("SparePartForecastHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartForecastMasterID")) Then
                sparePartForecastDetail.SparePartForecastMaster = New SparePartForecastMaster(CType(dr("SparePartForecastMasterID"), Integer))
            End If
            Return sparePartForecastDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartForecastDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartForecastDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartForecastDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

    End Class
End Namespace
