
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_AssistPartStockFlagLastData Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 2/6/2018 - 8:56:11 AM
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

    Public Class V_AssistPartStockFlagLastDataMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_AssistPartStockFlagLastData"
        Private m_UpdateStatement As String = "up_UpdateV_AssistPartStockFlagLastData"
        Private m_RetrieveStatement As String = "up_RetrieveV_AssistPartStockFlagLastData"
        Private m_RetrieveListStatement As String = "up_RetrieveV_AssistPartStockFlagLastDataList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_AssistPartStockFlagLastData"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_AssistPartStockFlagLastData As V_AssistPartStockFlagLastData = Nothing
            While dr.Read

                v_AssistPartStockFlagLastData = Me.CreateObject(dr)

            End While

            Return v_AssistPartStockFlagLastData

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_AssistPartStockFlagLastDataList As ArrayList = New ArrayList

            While dr.Read
                Dim v_AssistPartStockFlagLastData As V_AssistPartStockFlagLastData = Me.CreateObject(dr)
                v_AssistPartStockFlagLastDataList.Add(v_AssistPartStockFlagLastData)
            End While

            Return v_AssistPartStockFlagLastDataList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_AssistPartStockFlagLastData As V_AssistPartStockFlagLastData = CType(obj, V_AssistPartStockFlagLastData)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_AssistPartStockFlagLastData.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_AssistPartStockFlagLastData As V_AssistPartStockFlagLastData = CType(obj, V_AssistPartStockFlagLastData)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(v_AssistPartStockFlagLastData.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@Month", DbType.StringFixedLength, v_AssistPartStockFlagLastData.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.StringFixedLength, v_AssistPartStockFlagLastData.Year)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(v_AssistPartStockFlagLastData.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_AssistPartStockFlagLastData.DealerCode)
            DbCommandWrapper.AddInParameter("@SparepartMasterID", DbType.Int32, Me.GetRefObject(v_AssistPartStockFlagLastData.SparePartMaster))
            DbCommandWrapper.AddInParameter("@NoParts", DbType.AnsiString, v_AssistPartStockFlagLastData.NoParts)
            DbCommandWrapper.AddInParameter("@JumlahStokAwal", DbType.Int32, v_AssistPartStockFlagLastData.JumlahStokAwal)
            DbCommandWrapper.AddInParameter("@JumlahDatang", DbType.Int32, v_AssistPartStockFlagLastData.JumlahDatang)
            DbCommandWrapper.AddInParameter("@HargaBeli", DbType.Currency, v_AssistPartStockFlagLastData.HargaBeli)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, v_AssistPartStockFlagLastData.RemarksSystem)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, v_AssistPartStockFlagLastData.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, v_AssistPartStockFlagLastData.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_AssistPartStockFlagLastData.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_AssistPartStockFlagLastData.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@IsLastData", DbType.Int16, v_AssistPartStockFlagLastData.IsLastData)


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

            Dim v_AssistPartStockFlagLastData As V_AssistPartStockFlagLastData = CType(obj, V_AssistPartStockFlagLastData)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_AssistPartStockFlagLastData.ID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(v_AssistPartStockFlagLastData.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@Month", DbType.StringFixedLength, v_AssistPartStockFlagLastData.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.StringFixedLength, v_AssistPartStockFlagLastData.Year)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(v_AssistPartStockFlagLastData.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_AssistPartStockFlagLastData.DealerCode)
            DbCommandWrapper.AddInParameter("@SparepartMasterID", DbType.Int32, Me.GetRefObject(v_AssistPartStockFlagLastData.SparePartMaster))
            DbCommandWrapper.AddInParameter("@NoParts", DbType.AnsiString, v_AssistPartStockFlagLastData.NoParts)
            DbCommandWrapper.AddInParameter("@JumlahStokAwal", DbType.Int32, v_AssistPartStockFlagLastData.JumlahStokAwal)
            DbCommandWrapper.AddInParameter("@JumlahDatang", DbType.Int32, v_AssistPartStockFlagLastData.JumlahDatang)
            DbCommandWrapper.AddInParameter("@HargaBeli", DbType.Currency, v_AssistPartStockFlagLastData.HargaBeli)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, v_AssistPartStockFlagLastData.RemarksSystem)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, v_AssistPartStockFlagLastData.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, v_AssistPartStockFlagLastData.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_AssistPartStockFlagLastData.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_AssistPartStockFlagLastData.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@IsLastData", DbType.Int16, v_AssistPartStockFlagLastData.IsLastData)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_AssistPartStockFlagLastData

            Dim v_AssistPartStockFlagLastData As V_AssistPartStockFlagLastData = New V_AssistPartStockFlagLastData

            v_AssistPartStockFlagLastData.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then v_AssistPartStockFlagLastData.Month = dr("Month").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then v_AssistPartStockFlagLastData.Year = dr("Year").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_AssistPartStockFlagLastData.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then v_AssistPartStockFlagLastData.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoParts")) Then v_AssistPartStockFlagLastData.NoParts = dr("NoParts").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JumlahStokAwal")) Then v_AssistPartStockFlagLastData.JumlahStokAwal = CType(dr("JumlahStokAwal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("JumlahDatang")) Then v_AssistPartStockFlagLastData.JumlahDatang = CType(dr("JumlahDatang"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaBeli")) Then v_AssistPartStockFlagLastData.HargaBeli = CType(dr("HargaBeli"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSystem")) Then v_AssistPartStockFlagLastData.RemarksSystem = dr("RemarksSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusAktif")) Then v_AssistPartStockFlagLastData.StatusAktif = CType(dr("StatusAktif"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateSystemStatus")) Then v_AssistPartStockFlagLastData.ValidateSystemStatus = CType(dr("ValidateSystemStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_AssistPartStockFlagLastData.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_AssistPartStockFlagLastData.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_AssistPartStockFlagLastData.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_AssistPartStockFlagLastData.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_AssistPartStockFlagLastData.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsLastData")) Then v_AssistPartStockFlagLastData.IsLastData = CType(dr("IsLastData"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then
                v_AssistPartStockFlagLastData.AssistUploadLog = New AssistUploadLog(CType(dr("AssistUploadLogID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                v_AssistPartStockFlagLastData.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartMasterID")) Then
                v_AssistPartStockFlagLastData.SparePartMaster = New SparePartMaster(CType(dr("SparepartMasterID"), Integer))
            End If
            Return v_AssistPartStockFlagLastData

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_AssistPartStockFlagLastData) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_AssistPartStockFlagLastData), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_AssistPartStockFlagLastData).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

