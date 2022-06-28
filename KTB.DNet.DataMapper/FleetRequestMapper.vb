
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetRequest Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2016 - 2:28:40 PM
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

    Public Class FleetRequestMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetRequest"
        Private m_UpdateStatement As String = "up_UpdateFleetRequest"
        Private m_RetrieveStatement As String = "up_RetrieveFleetRequest"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetRequestList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetRequest"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fleetRequest As FleetRequest = Nothing
            While dr.Read

                fleetRequest = Me.CreateObject(dr)

            End While

            Return fleetRequest

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fleetRequestList As ArrayList = New ArrayList

            While dr.Read
                Dim fleetRequest As FleetRequest = Me.CreateObject(dr)
                fleetRequestList.Add(fleetRequest)
            End While

            Return fleetRequestList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetRequest As FleetRequest = CType(obj, FleetRequest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetRequest.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fleetRequest As FleetRequest = CType(obj, FleetRequest)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddInParameter("@NoRegRequest", DbType.AnsiString, fleetRequest.NoRegRequest)
            DbCommandWrapper.AddInParameter("@TanggalPengajuan", DbType.DateTime, fleetRequest.TanggalPengajuan)
            DbCommandWrapper.AddInParameter("@NamaKonsumen", DbType.AnsiString, fleetRequest.NamaKonsumen)
            DbCommandWrapper.AddInParameter("@StatusKonsumen", DbType.Int16, fleetRequest.StatusKonsumen)
            DbCommandWrapper.AddInParameter("@ProfilBisnis", DbType.AnsiString, fleetRequest.ProfilBisnis)
            DbCommandWrapper.AddInParameter("@KebutuhanUnit", DbType.AnsiString, fleetRequest.KebutuhanUnit)
            DbCommandWrapper.AddInParameter("@MulaiPengadaan", DbType.DateTime, fleetRequest.MulaiPengadaan)
            DbCommandWrapper.AddInParameter("@SelesaiPengadaan", DbType.DateTime, fleetRequest.SelesaiPengadaan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, fleetRequest.Status)
            DbCommandWrapper.AddInParameter("@BatalKonfirmasiNote", DbType.AnsiString, fleetRequest.BatalKonfirmasiNote)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetRequest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fleetRequest.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, fleetRequest.Attachment)

            DbCommandWrapper.AddInParameter("@FleetMasterDealerID", DbType.Int32, Me.GetRefObject(fleetRequest.FleetMasterDealer))
            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, fleetRequest.ID)

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

            Dim fleetRequest As FleetRequest = CType(obj, FleetRequest)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@NoRegRequest", DbType.AnsiString, fleetRequest.NoRegRequest)
            DbCommandWrapper.AddInParameter("@TanggalPengajuan", DbType.DateTime, fleetRequest.TanggalPengajuan)
            DbCommandWrapper.AddInParameter("@NamaKonsumen", DbType.AnsiString, fleetRequest.NamaKonsumen)
            DbCommandWrapper.AddInParameter("@StatusKonsumen", DbType.Int16, fleetRequest.StatusKonsumen)
            DbCommandWrapper.AddInParameter("@ProfilBisnis", DbType.AnsiString, fleetRequest.ProfilBisnis)
            DbCommandWrapper.AddInParameter("@KebutuhanUnit", DbType.AnsiString, fleetRequest.KebutuhanUnit)
            DbCommandWrapper.AddInParameter("@MulaiPengadaan", DbType.DateTime, fleetRequest.MulaiPengadaan)
            DbCommandWrapper.AddInParameter("@SelesaiPengadaan", DbType.DateTime, fleetRequest.SelesaiPengadaan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, fleetRequest.Status)
            DbCommandWrapper.AddInParameter("@BatalKonfirmasiNote", DbType.AnsiString, fleetRequest.BatalKonfirmasiNote)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fleetRequest.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fleetRequest.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.AnsiString, fleetRequest.Attachment)


            DbCommandWrapper.AddInParameter("@FleetMasterDealerID", DbType.Int32, Me.GetRefObject(fleetRequest.FleetMasterDealer))
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fleetRequest.ID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetRequest

            Dim fleetRequest As FleetRequest = New FleetRequest

            If Not dr.IsDBNull(dr.GetOrdinal("NoRegRequest")) Then fleetRequest.NoRegRequest = dr("NoRegRequest").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalPengajuan")) Then fleetRequest.TanggalPengajuan = CType(dr("TanggalPengajuan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NamaKonsumen")) Then fleetRequest.NamaKonsumen = dr("NamaKonsumen").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusKonsumen")) Then fleetRequest.StatusKonsumen = CType(dr("StatusKonsumen"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfilBisnis")) Then fleetRequest.ProfilBisnis = dr("ProfilBisnis").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KebutuhanUnit")) Then fleetRequest.KebutuhanUnit = dr("KebutuhanUnit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MulaiPengadaan")) Then fleetRequest.MulaiPengadaan = CType(dr("MulaiPengadaan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SelesaiPengadaan")) Then fleetRequest.SelesaiPengadaan = CType(dr("SelesaiPengadaan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then fleetRequest.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("BatalKonfirmasiNote")) Then fleetRequest.BatalKonfirmasiNote = dr("BatalKonfirmasiNote").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fleetRequest.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fleetRequest.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fleetRequest.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fleetRequest.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fleetRequest.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then fleetRequest.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FleetMasterDealerID")) Then
                fleetRequest.FleetMasterDealer = New FleetMasterDealer(CType(dr("FleetMasterDealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then
                fleetRequest.ID = CType(dr("ID"), Integer)
            End If

            Return fleetRequest

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetRequest) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetRequest), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetRequest).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

