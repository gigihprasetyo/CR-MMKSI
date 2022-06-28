#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistServiceIncomingBP Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 3/24/2021 - 10:29:57 AM
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

    Public Class AssistServiceIncomingBPMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistServiceIncomingBP"
        Private m_UpdateStatement As String = "up_UpdateAssistServiceIncomingBP"
        Private m_RetrieveStatement As String = "up_RetrieveAssistServiceIncomingBP"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistServiceIncomingBPList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistServiceIncomingBP"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistServiceIncomingBP As AssistServiceIncomingBP = Nothing
            While dr.Read

                assistServiceIncomingBP = Me.CreateObject(dr)

            End While

            Return assistServiceIncomingBP

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistServiceIncomingBPList As ArrayList = New ArrayList

            While dr.Read
                Dim assistServiceIncomingBP As AssistServiceIncomingBP = Me.CreateObject(dr)
                assistServiceIncomingBPList.Add(assistServiceIncomingBP)
            End While

            Return assistServiceIncomingBPList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistServiceIncomingBP As AssistServiceIncomingBP = CType(obj, AssistServiceIncomingBP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistServiceIncomingBP.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistServiceIncomingBP As AssistServiceIncomingBP = CType(obj, AssistServiceIncomingBP)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, assistServiceIncomingBP.AssistUploadLogID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@TglPengajuanEstimasi", DbType.Date, assistServiceIncomingBP.TglPengajuanEstimasi)
            DbCommandWrapper.AddInParameter("@TglPersetujuanEstimasi", DbType.Date, assistServiceIncomingBP.TglPersetujuanEstimasi)
            DbCommandWrapper.AddInParameter("@TglBukaTransaksi", DbType.Date, assistServiceIncomingBP.TglBukaTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.Time, assistServiceIncomingBP.WaktuMasuk)
            DbCommandWrapper.AddInParameter("@TglJanjiSelesai", DbType.Date, assistServiceIncomingBP.TglJanjiSelesai)
            DbCommandWrapper.AddInParameter("@TglTutupTransaksi", DbType.Date, assistServiceIncomingBP.TglTutupTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.Time, assistServiceIncomingBP.WaktuKeluar)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, assistServiceIncomingBP.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistServiceIncomingBP.DealerCode)
            DbCommandWrapper.AddInParameter("@TrTraineMekanikID", DbType.Int32, assistServiceIncomingBP.TrTraineMekanikID)
            DbCommandWrapper.AddInParameter("@KodeMekanik", DbType.AnsiString, assistServiceIncomingBP.KodeMekanik)
            DbCommandWrapper.AddInParameter("@NoWorkOrder", DbType.AnsiString, assistServiceIncomingBP.NoWorkOrder)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, assistServiceIncomingBP.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.ChassisMaster))
            DbCommandWrapper.AddInParameter("@KodeChassis", DbType.AnsiString, assistServiceIncomingBP.KodeChassis)
            DbCommandWrapper.AddInParameter("@VehicleModelDesc", DbType.AnsiString, assistServiceIncomingBP.VehicleModelDesc)
            DbCommandWrapper.AddInParameter("@VehicleColorDesc", DbType.AnsiString, assistServiceIncomingBP.VehicleColorDesc)
            DbCommandWrapper.AddInParameter("@KMService", DbType.Int32, assistServiceIncomingBP.KMService)
            'DbCommandWrapper.AddInParameter("@WorkOrderCategoryID", DbType.Int32, assistServiceIncomingBP.WorkOrderCategoryID)
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.AssistWorkOrderCategory))
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryCode", DbType.AnsiString, assistServiceIncomingBP.WorkOrderCategoryCode)
            'DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int32, assistServiceIncomingBP.ServiceTypeID)
            DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.ServiceType))
            DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, assistServiceIncomingBP.ServiceTypeCode)
            DbCommandWrapper.AddInParameter("@ServiceBooking", DbType.AnsiString, assistServiceIncomingBP.ServiceBooking)
            DbCommandWrapper.AddInParameter("@TotalLC", DbType.Currency, assistServiceIncomingBP.TotalLC)
            DbCommandWrapper.AddInParameter("@TotalSubOrder", DbType.Currency, assistServiceIncomingBP.TotalSubOrder)
            DbCommandWrapper.AddInParameter("@TotalCat", DbType.Currency, assistServiceIncomingBP.TotalCat)
            DbCommandWrapper.AddInParameter("@TotalNonCat", DbType.Currency, assistServiceIncomingBP.TotalNonCat)
            DbCommandWrapper.AddInParameter("@DamageCategory", DbType.AnsiString, assistServiceIncomingBP.DamageCategory)
            DbCommandWrapper.AddInParameter("@TotalPanel", DbType.AnsiString, assistServiceIncomingBP.TotalPanel)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, assistServiceIncomingBP.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@InsuranceName", DbType.AnsiString, assistServiceIncomingBP.InsuranceName)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistServiceIncomingBP.RemarksSystem)
            DbCommandWrapper.AddInParameter("@RemarksSpecial", DbType.AnsiString, assistServiceIncomingBP.RemarksSpecial)
            DbCommandWrapper.AddInParameter("@RemarksBM", DbType.AnsiString, assistServiceIncomingBP.RemarksBM)
            DbCommandWrapper.AddInParameter("@WOStatus", DbType.Int16, assistServiceIncomingBP.WOStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistServiceIncomingBP.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistServiceIncomingBP.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@CustomerOwnerName", DbType.AnsiString, assistServiceIncomingBP.CustomerOwnerName)
            DbCommandWrapper.AddInParameter("@CustomerOwnerPhoneNumber", DbType.AnsiString, assistServiceIncomingBP.CustomerOwnerPhoneNumber)
            DbCommandWrapper.AddInParameter("@CustomerVisitName", DbType.AnsiString, assistServiceIncomingBP.CustomerVisitName)
            DbCommandWrapper.AddInParameter("@CustomerVisitPhoneNumber", DbType.AnsiString, assistServiceIncomingBP.CustomerVisitPhoneNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistServiceIncomingBP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistServiceIncomingBP.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, assistServiceIncomingBP.LastUpdateTime)


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

            Dim assistServiceIncomingBP As AssistServiceIncomingBP = CType(obj, AssistServiceIncomingBP)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, assistServiceIncomingBP.AssistUploadLogID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@TglPengajuanEstimasi", DbType.Date, assistServiceIncomingBP.TglPengajuanEstimasi)
            DbCommandWrapper.AddInParameter("@TglPersetujuanEstimasi", DbType.Date, assistServiceIncomingBP.TglPersetujuanEstimasi)
            DbCommandWrapper.AddInParameter("@TglBukaTransaksi", DbType.Date, assistServiceIncomingBP.TglBukaTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.Time, assistServiceIncomingBP.WaktuMasuk)
            DbCommandWrapper.AddInParameter("@TglJanjiSelesai", DbType.Date, assistServiceIncomingBP.TglJanjiSelesai)
            DbCommandWrapper.AddInParameter("@TglTutupTransaksi", DbType.Date, assistServiceIncomingBP.TglTutupTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.Time, assistServiceIncomingBP.WaktuKeluar)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, assistServiceIncomingBP.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistServiceIncomingBP.DealerCode)
            DbCommandWrapper.AddInParameter("@TrTraineMekanikID", DbType.Int32, assistServiceIncomingBP.TrTraineMekanikID)
            DbCommandWrapper.AddInParameter("@KodeMekanik", DbType.AnsiString, assistServiceIncomingBP.KodeMekanik)
            DbCommandWrapper.AddInParameter("@NoWorkOrder", DbType.AnsiString, assistServiceIncomingBP.NoWorkOrder)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, assistServiceIncomingBP.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.ChassisMaster))
            DbCommandWrapper.AddInParameter("@KodeChassis", DbType.AnsiString, assistServiceIncomingBP.KodeChassis)
            DbCommandWrapper.AddInParameter("@VehicleModelDesc", DbType.AnsiString, assistServiceIncomingBP.VehicleModelDesc)
            DbCommandWrapper.AddInParameter("@VehicleColorDesc", DbType.AnsiString, assistServiceIncomingBP.VehicleColorDesc)
            DbCommandWrapper.AddInParameter("@KMService", DbType.Int32, assistServiceIncomingBP.KMService)
            'DbCommandWrapper.AddInParameter("@WorkOrderCategoryID", DbType.Int32, assistServiceIncomingBP.WorkOrderCategoryID)
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.AssistWorkOrderCategory))
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryCode", DbType.AnsiString, assistServiceIncomingBP.WorkOrderCategoryCode)
            'DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int32, assistServiceIncomingBP.ServiceTypeID)
            DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int32, Me.GetRefObject(assistServiceIncomingBP.ServiceType))
            DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, assistServiceIncomingBP.ServiceTypeCode)
            DbCommandWrapper.AddInParameter("@ServiceBooking", DbType.AnsiString, assistServiceIncomingBP.ServiceBooking)
            DbCommandWrapper.AddInParameter("@TotalLC", DbType.Currency, assistServiceIncomingBP.TotalLC)
            DbCommandWrapper.AddInParameter("@TotalSubOrder", DbType.Currency, assistServiceIncomingBP.TotalSubOrder)
            DbCommandWrapper.AddInParameter("@TotalCat", DbType.Currency, assistServiceIncomingBP.TotalCat)
            DbCommandWrapper.AddInParameter("@TotalNonCat", DbType.Currency, assistServiceIncomingBP.TotalNonCat)
            DbCommandWrapper.AddInParameter("@DamageCategory", DbType.AnsiString, assistServiceIncomingBP.DamageCategory)
            DbCommandWrapper.AddInParameter("@TotalPanel", DbType.AnsiString, assistServiceIncomingBP.TotalPanel)
            DbCommandWrapper.AddInParameter("@MethodOfPayment", DbType.AnsiString, assistServiceIncomingBP.MethodOfPayment)
            DbCommandWrapper.AddInParameter("@InsuranceName", DbType.AnsiString, assistServiceIncomingBP.InsuranceName)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistServiceIncomingBP.RemarksSystem)
            DbCommandWrapper.AddInParameter("@RemarksSpecial", DbType.AnsiString, assistServiceIncomingBP.RemarksSpecial)
            DbCommandWrapper.AddInParameter("@RemarksBM", DbType.AnsiString, assistServiceIncomingBP.RemarksBM)
            DbCommandWrapper.AddInParameter("@WOStatus", DbType.Int16, assistServiceIncomingBP.WOStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistServiceIncomingBP.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistServiceIncomingBP.ValidateSystemStatus)
            DbCommandWrapper.AddInParameter("@CustomerOwnerName", DbType.AnsiString, assistServiceIncomingBP.CustomerOwnerName)
            DbCommandWrapper.AddInParameter("@CustomerOwnerPhoneNumber", DbType.AnsiString, assistServiceIncomingBP.CustomerOwnerPhoneNumber)
            DbCommandWrapper.AddInParameter("@CustomerVisitName", DbType.AnsiString, assistServiceIncomingBP.CustomerVisitName)
            DbCommandWrapper.AddInParameter("@CustomerVisitPhoneNumber", DbType.AnsiString, assistServiceIncomingBP.CustomerVisitPhoneNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistServiceIncomingBP.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistServiceIncomingBP.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, assistServiceIncomingBP.LastUpdateTime)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistServiceIncomingBP

            Dim assistServiceIncomingBP As AssistServiceIncomingBP = New AssistServiceIncomingBP

            assistServiceIncomingBP.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then assistServiceIncomingBP.AssistUploadLogID = CType(dr("AssistUploadLogID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TglPengajuanEstimasi")) Then assistServiceIncomingBP.TglPengajuanEstimasi = CType(dr("TglPengajuanEstimasi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TglPersetujuanEstimasi")) Then assistServiceIncomingBP.TglPersetujuanEstimasi = CType(dr("TglPersetujuanEstimasi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TglBukaTransaksi")) Then assistServiceIncomingBP.TglBukaTransaksi = CType(dr("TglBukaTransaksi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WaktuMasuk")) Then assistServiceIncomingBP.WaktuMasuk = CType(dr("WaktuMasuk"), TimeSpan)
            If Not dr.IsDBNull(dr.GetOrdinal("TglJanjiSelesai")) Then assistServiceIncomingBP.TglJanjiSelesai = CType(dr("TglJanjiSelesai"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TglTutupTransaksi")) Then assistServiceIncomingBP.TglTutupTransaksi = CType(dr("TglTutupTransaksi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WaktuKeluar")) Then assistServiceIncomingBP.WaktuKeluar = CType(dr("WaktuKeluar"), TimeSpan)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then assistServiceIncomingBP.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then assistServiceIncomingBP.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineMekanikID")) Then assistServiceIncomingBP.TrTraineMekanikID = CType(dr("TrTraineMekanikID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KodeMekanik")) Then assistServiceIncomingBP.KodeMekanik = dr("KodeMekanik").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoWorkOrder")) Then assistServiceIncomingBP.NoWorkOrder = dr("NoWorkOrder").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then assistServiceIncomingBP.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KodeChassis")) Then assistServiceIncomingBP.KodeChassis = dr("KodeChassis").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModelDesc")) Then assistServiceIncomingBP.VehicleModelDesc = dr("VehicleModelDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorDesc")) Then assistServiceIncomingBP.VehicleColorDesc = dr("VehicleColorDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KMService")) Then assistServiceIncomingBP.KMService = CType(dr("KMService"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderCategoryID")) Then assistServiceIncomingBP.WorkOrderCategoryID = CType(dr("WorkOrderCategoryID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderCategoryCode")) Then assistServiceIncomingBP.WorkOrderCategoryCode = dr("WorkOrderCategoryCode").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeID")) Then assistServiceIncomingBP.ServiceTypeID = CType(dr("ServiceTypeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeCode")) Then assistServiceIncomingBP.ServiceTypeCode = dr("ServiceTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBooking")) Then assistServiceIncomingBP.ServiceBooking = dr("ServiceBooking").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalLC")) Then assistServiceIncomingBP.TotalLC = CType(dr("TotalLC"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalSubOrder")) Then assistServiceIncomingBP.TotalSubOrder = CType(dr("TotalSubOrder"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalCat")) Then assistServiceIncomingBP.TotalCat = CType(dr("TotalCat"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalNonCat")) Then assistServiceIncomingBP.TotalNonCat = CType(dr("TotalNonCat"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DamageCategory")) Then assistServiceIncomingBP.DamageCategory = dr("DamageCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPanel")) Then assistServiceIncomingBP.TotalPanel = dr("TotalPanel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MethodOfPayment")) Then assistServiceIncomingBP.MethodOfPayment = dr("MethodOfPayment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InsuranceName")) Then assistServiceIncomingBP.InsuranceName = dr("InsuranceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSystem")) Then assistServiceIncomingBP.RemarksSystem = dr("RemarksSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSpecial")) Then assistServiceIncomingBP.RemarksSpecial = dr("RemarksSpecial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksBM")) Then assistServiceIncomingBP.RemarksBM = dr("RemarksBM").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WOStatus")) Then assistServiceIncomingBP.WOStatus = CType(dr("WOStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusAktif")) Then assistServiceIncomingBP.StatusAktif = CType(dr("StatusAktif"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateSystemStatus")) Then assistServiceIncomingBP.ValidateSystemStatus = CType(dr("ValidateSystemStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerOwnerName")) Then assistServiceIncomingBP.CustomerOwnerName = dr("CustomerOwnerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerOwnerPhoneNumber")) Then assistServiceIncomingBP.CustomerOwnerPhoneNumber = dr("CustomerOwnerPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerVisitName")) Then assistServiceIncomingBP.CustomerVisitName = dr("CustomerVisitName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerVisitPhoneNumber")) Then assistServiceIncomingBP.CustomerVisitPhoneNumber = dr("CustomerVisitPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistServiceIncomingBP.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistServiceIncomingBP.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistServiceIncomingBP.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistServiceIncomingBP.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistServiceIncomingBP.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then
                assistServiceIncomingBP.AssistUploadLog = New AssistUploadLog(CType(dr("AssistUploadLogID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                assistServiceIncomingBP.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                assistServiceIncomingBP.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderCategoryID")) Then
                assistServiceIncomingBP.AssistWorkOrderCategory = New AssistWorkOrderCategory(CType(dr("WorkOrderCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeID")) Then
                assistServiceIncomingBP.ServiceType = New AssistServiceType(CType(dr("ServiceTypeID"), Integer))
            End If

            Return assistServiceIncomingBP

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistServiceIncomingBP) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistServiceIncomingBP), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistServiceIncomingBP).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

