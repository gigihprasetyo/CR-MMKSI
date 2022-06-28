
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistPartStock Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 10:37:17 AM
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

    Public Class AssistPartStockMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistPartStock"
        Private m_UpdateStatement As String = "up_UpdateAssistPartStock"
        Private m_RetrieveStatement As String = "up_RetrieveAssistPartStock"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistPartStockList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistPartStock"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistPartStock As AssistPartStock = Nothing
            While dr.Read

                assistPartStock = Me.CreateObject(dr)

            End While

            Return assistPartStock

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistPartStockList As ArrayList = New ArrayList

            While dr.Read
                Dim assistPartStock As AssistPartStock = Me.CreateObject(dr)
                assistPartStockList.Add(assistPartStock)
            End While

            Return assistPartStockList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistPartStock As AssistPartStock = CType(obj, AssistPartStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistPartStock.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistPartStock As AssistPartStock = CType(obj, AssistPartStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistPartStock.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@Month", DbType.StringFixedLength, assistPartStock.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.StringFixedLength, assistPartStock.Year)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistPartStock.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistPartStock.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(assistPartStock.DealerBranch))
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, assistPartStock.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@SparepartMasterID", DbType.Int32, Me.GetRefObject(assistPartStock.SparePartMaster))
            DbCommandWrapper.AddInParameter("@NoParts", DbType.AnsiString, assistPartStock.NoParts)
            DbCommandWrapper.AddInParameter("@JumlahStokAwal", DbType.Double, assistPartStock.JumlahStokAwal)
            DbCommandWrapper.AddInParameter("@JumlahDatang", DbType.Double, assistPartStock.JumlahDatang)
            DbCommandWrapper.AddInParameter("@HargaBeli", DbType.Currency, assistPartStock.HargaBeli)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistPartStock.RemarksSystem)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistPartStock.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistPartStock.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistPartStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistPartStock.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim assistPartStock As AssistPartStock = CType(obj, AssistPartStock)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistPartStock.ID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistPartStock.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@Month", DbType.StringFixedLength, assistPartStock.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.StringFixedLength, assistPartStock.Year)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistPartStock.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistPartStock.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(assistPartStock.DealerBranch))
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, assistPartStock.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@SparepartMasterID", DbType.Int32, Me.GetRefObject(assistPartStock.SparePartMaster))
            DbCommandWrapper.AddInParameter("@NoParts", DbType.AnsiString, assistPartStock.NoParts)
            DbCommandWrapper.AddInParameter("@JumlahStokAwal", DbType.Double, assistPartStock.JumlahStokAwal)
            DbCommandWrapper.AddInParameter("@JumlahDatang", DbType.Double, assistPartStock.JumlahDatang)
            DbCommandWrapper.AddInParameter("@HargaBeli", DbType.Currency, assistPartStock.HargaBeli)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistPartStock.RemarksSystem)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistPartStock.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistPartStock.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistPartStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistPartStock.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistPartStock

            Dim assistPartStock As AssistPartStock = New AssistPartStock

            assistPartStock.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then assistPartStock.Month = dr("Month").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then assistPartStock.Year = dr("Year").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then assistPartStock.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then assistPartStock.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoParts")) Then assistPartStock.NoParts = dr("NoParts").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JumlahStokAwal")) Then assistPartStock.JumlahStokAwal = CType(dr("JumlahStokAwal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("JumlahDatang")) Then assistPartStock.JumlahDatang = CType(dr("JumlahDatang"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("HargaBeli")) Then assistPartStock.HargaBeli = CType(dr("HargaBeli"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSystem")) Then assistPartStock.RemarksSystem = dr("RemarksSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusAktif")) Then assistPartStock.StatusAktif = CType(dr("StatusAktif"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateSystemStatus")) Then assistPartStock.ValidateSystemStatus = CType(dr("ValidateSystemStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistPartStock.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistPartStock.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistPartStock.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistPartStock.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistPartStock.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)            
            If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then
                assistPartStock.AssistUploadLog = New AssistUploadLog(CType(dr("AssistUploadLogID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                assistPartStock.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                assistPartStock.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartMasterID")) Then
                assistPartStock.SparePartMaster = New SparePartMaster(CType(dr("SparepartMasterID"), Integer))
            End If
            Return assistPartStock

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistPartStock) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistPartStock), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistPartStock).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

