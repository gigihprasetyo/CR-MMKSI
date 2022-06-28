
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBReceipt Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 5/27/2016 - 7:13:11 PM
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

    Public Class DepositBReceiptMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBReceipt"
        Private m_UpdateStatement As String = "up_UpdateDepositBReceipt"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBReceipt"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBReceiptList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBReceipt"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBReceipt As DepositBReceipt = Nothing
            While dr.Read

                depositBReceipt = Me.CreateObject(dr)

            End While

            Return depositBReceipt

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBReceiptList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBReceipt As DepositBReceipt = Me.CreateObject(dr)
                depositBReceiptList.Add(depositBReceipt)
            End While

            Return depositBReceiptList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBReceipt As DepositBReceipt = CType(obj, DepositBReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBReceipt.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBReceipt As DepositBReceipt = CType(obj, DepositBReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NoRegKuitansi", DbType.AnsiString, depositBReceipt.NoRegKuitansi)
            DbCommandWrapper.AddInParameter("@NomorKuitansi", DbType.AnsiString, depositBReceipt.NomorKuitansi)
            DbCommandWrapper.AddInParameter("@TanggalKuitansi", DbType.DateTime, depositBReceipt.TanggalKuitansi)
            DbCommandWrapper.AddInParameter("@TanggalTransfer", DbType.DateTime, depositBReceipt.TanggalTransfer)
            DbCommandWrapper.AddInParameter("@TanggalPelunasan", DbType.DateTime, depositBReceipt.TanggalPelunasan)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, depositBReceipt.JVNumber)
            DbCommandWrapper.AddInParameter("@NamaPejabat", DbType.AnsiString, depositBReceipt.NamaPejabat)
            DbCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, depositBReceipt.Jabatan)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, depositBReceipt.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBReceipt.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositBPencairanHeaderID", DbType.Int32, Me.GetRefObject(depositBReceipt.DepositBPencairanHeader))

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

            Dim depositBReceipt As DepositBReceipt = CType(obj, DepositBReceipt)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBReceipt.ID)
            DbCommandWrapper.AddInParameter("@NoRegKuitansi", DbType.AnsiString, depositBReceipt.NoRegKuitansi)
            DbCommandWrapper.AddInParameter("@NomorKuitansi", DbType.AnsiString, depositBReceipt.NomorKuitansi)
            DbCommandWrapper.AddInParameter("@TanggalKuitansi", DbType.DateTime, depositBReceipt.TanggalKuitansi)
            DbCommandWrapper.AddInParameter("@TanggalTransfer", DbType.DateTime, depositBReceipt.TanggalTransfer)
            DbCommandWrapper.AddInParameter("@TanggalPelunasan", DbType.DateTime, depositBReceipt.TanggalPelunasan)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, depositBReceipt.JVNumber)
            DbCommandWrapper.AddInParameter("@NamaPejabat", DbType.AnsiString, depositBReceipt.NamaPejabat)
            DbCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, depositBReceipt.Jabatan)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, depositBReceipt.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBReceipt.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositBPencairanHeaderID", DbType.Int32, Me.GetRefObject(depositBReceipt.DepositBPencairanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBReceipt

            Dim depositBReceipt As DepositBReceipt = New DepositBReceipt

            depositBReceipt.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoRegKuitansi")) Then depositBReceipt.NoRegKuitansi = dr("NoRegKuitansi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NomorKuitansi")) Then depositBReceipt.NomorKuitansi = dr("NomorKuitansi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalKuitansi")) Then depositBReceipt.TanggalKuitansi = CType(dr("TanggalKuitansi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalTransfer")) Then depositBReceipt.TanggalTransfer = CType(dr("TanggalTransfer"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalPelunasan")) Then depositBReceipt.TanggalPelunasan = CType(dr("TanggalPelunasan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JVNumber")) Then depositBReceipt.JVNumber = dr("JVNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NamaPejabat")) Then depositBReceipt.NamaPejabat = dr("NamaPejabat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jabatan")) Then depositBReceipt.Jabatan = dr("Jabatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then depositBReceipt.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBReceipt.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBReceipt.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBReceipt.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBReceipt.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBReceipt.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBPencairanHeaderID")) Then
                depositBReceipt.DepositBPencairanHeader = New DepositBPencairanHeader(CType(dr("DepositBPencairanHeaderID"), Integer))
            End If

            Return depositBReceipt

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBReceipt) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBReceipt), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBReceipt).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

