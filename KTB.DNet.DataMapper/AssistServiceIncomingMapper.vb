
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistServiceIncoming Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/17/2018 - 9:24:46 AM
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

    Public Class AssistServiceIncomingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistServiceIncoming"
        Private m_UpdateStatement As String = "up_UpdateAssistServiceIncoming"
        Private m_RetrieveStatement As String = "up_RetrieveAssistServiceIncoming"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistServiceIncomingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistServiceIncoming"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistServiceIncoming As AssistServiceIncoming = Nothing
            While dr.Read

                assistServiceIncoming = Me.CreateObject(dr)

            End While

            Return assistServiceIncoming

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistServiceIncomingList As ArrayList = New ArrayList

            While dr.Read
                Dim assistServiceIncoming As AssistServiceIncoming = Me.CreateObject(dr)
                assistServiceIncomingList.Add(assistServiceIncoming)
            End While

            Return assistServiceIncomingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistServiceIncoming As AssistServiceIncoming = CType(obj, AssistServiceIncoming)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistServiceIncoming.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistServiceIncoming As AssistServiceIncoming = CType(obj, AssistServiceIncoming)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistUploadLog))
            DbCommandWrapper.AddInParameter("@TglBukaTransaksi", DbType.DateTime, assistServiceIncoming.TglBukaTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.Time, assistServiceIncoming.WaktuMasuk)
            'DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.AnsiString, assistServiceIncoming.WaktuMasuk.ToString())
            DbCommandWrapper.AddInParameter("@TglTutupTransaksi", DbType.DateTime, assistServiceIncoming.TglTutupTransaksi)
            DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.Time, assistServiceIncoming.WaktuKeluar)
            'DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.AnsiString, assistServiceIncoming.WaktuKeluar.ToString())
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistServiceIncoming.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.DealerBranch))
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, assistServiceIncoming.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@TrTraineMekanikID", DbType.Int32, assistServiceIncoming.TrTraineMekanikID)
            DbCommandWrapper.AddInParameter("@KodeMekanik", DbType.AnsiString, assistServiceIncoming.KodeMekanik)
            DbCommandWrapper.AddInParameter("@NoWorkOrder", DbType.AnsiString, assistServiceIncoming.NoWorkOrder)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.ChassisMaster))
            DbCommandWrapper.AddInParameter("@KodeChassis", DbType.AnsiString, assistServiceIncoming.KodeChassis)
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistWorkOrderCategory))
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryCode", DbType.AnsiString, assistServiceIncoming.WorkOrderCategoryCode)
            DbCommandWrapper.AddInParameter("@KMService", DbType.Int32, assistServiceIncoming.KMService)
            DbCommandWrapper.AddInParameter("@ServicePlaceID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistServicePlace))
            DbCommandWrapper.AddInParameter("@ServicePlaceCode", DbType.AnsiString, assistServiceIncoming.ServicePlaceCode)
            DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistServiceType))
            DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, assistServiceIncoming.ServiceTypeCode)
            DbCommandWrapper.AddInParameter("@TotalLC", DbType.Currency, assistServiceIncoming.TotalLC)
            DbCommandWrapper.AddInParameter("@MetodePembayaran", DbType.AnsiString, assistServiceIncoming.MetodePembayaran)
            DbCommandWrapper.AddInParameter("@Model", DbType.AnsiString, assistServiceIncoming.Model)
            DbCommandWrapper.AddInParameter("@Transmition", DbType.AnsiString, assistServiceIncoming.Transmition)
            DbCommandWrapper.AddInParameter("@DriveSystem", DbType.AnsiString, assistServiceIncoming.DriveSystem)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistServiceIncoming.RemarksSystem)
            DbCommandWrapper.AddInParameter("@RemarksSpecial", DbType.AnsiString, assistServiceIncoming.RemarksSpecial)
            DbCommandWrapper.AddInParameter("@RemarksBM", DbType.AnsiString, assistServiceIncoming.RemarksBM)
            DbCommandWrapper.AddInParameter("@WOStatus", DbType.Int16, assistServiceIncoming.WOStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistServiceIncoming.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistServiceIncoming.ValidateSystemStatus)
            'new cols
            DbCommandWrapper.AddInParameter("@CustomerOwnerName", DbType.AnsiString, assistServiceIncoming.CustomerOwnerName)
            DbCommandWrapper.AddInParameter("@CustomerOwnerPhoneNumber", DbType.AnsiString, assistServiceIncoming.CustomerOwnerPhoneNumber)
            DbCommandWrapper.AddInParameter("@CustomerVisitName", DbType.AnsiString, assistServiceIncoming.CustomerVisitName)
            DbCommandWrapper.AddInParameter("@CustomerVisitPhoneNumber", DbType.AnsiString, assistServiceIncoming.CustomerVisitPhoneNumber)
            'end new cols

            DbCommandWrapper.AddInParameter("@StallMasterID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.StallMaster))
            DbCommandWrapper.AddInParameter("@StallCode", DbType.AnsiString, assistServiceIncoming.StallCode)
            DbCommandWrapper.AddInParameter("@ServiceBookingID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.ServiceBooking))
            DbCommandWrapper.AddInParameter("@BookingCode", DbType.AnsiString, assistServiceIncoming.BookingCode)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistServiceIncoming.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistServiceIncoming.LastUpdateBy)
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

            Dim assistServiceIncoming As AssistServiceIncoming = CType(obj, AssistServiceIncoming)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistServiceIncoming.ID)
            DbCommandWrapper.AddInParameter("@AssistUploadLogID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistUploadLog))

            If assistServiceIncoming.TglBukaTransaksi Is Nothing Then
                DbCommandWrapper.AddInParameter("@TglBukaTransaksi", DbType.Date, DBNull.Value)
            Else
                DbCommandWrapper.AddInParameter("@TglBukaTransaksi", DbType.Date, assistServiceIncoming.TglBukaTransaksi)
            End If

            If assistServiceIncoming.WaktuMasuk Is Nothing Then
                DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.Time, DBNull.Value)
            Else
                DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.Time, assistServiceIncoming.WaktuMasuk.ToString())
            End If

            If assistServiceIncoming.WaktuKeluar Is Nothing Then
                DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.Time, DBNull.Value)
            Else
                DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.Time, assistServiceIncoming.WaktuKeluar.ToString())
            End If

            If assistServiceIncoming.TglTutupTransaksi Is Nothing Then
                DbCommandWrapper.AddInParameter("@TglTutupTransaksi", DbType.Date, DBNull.Value)
            Else
                DbCommandWrapper.AddInParameter("@TglTutupTransaksi", DbType.Date, assistServiceIncoming.TglTutupTransaksi)
            End If

            'DbCommandWrapper.AddInParameter("@WaktuMasuk", DbType.AnsiString, assistServiceIncoming.WaktuMasuk.ToString())


            'DbCommandWrapper.AddInParameter("@WaktuKeluar", DbType.AnsiString, assistServiceIncoming.WaktuKeluar.ToString())
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.Dealer))
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, assistServiceIncoming.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.DealerBranch))
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, assistServiceIncoming.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@TrTraineMekanikID", DbType.Int32, assistServiceIncoming.TrTraineMekanikID)
            DbCommandWrapper.AddInParameter("@KodeMekanik", DbType.AnsiString, assistServiceIncoming.KodeMekanik)
            DbCommandWrapper.AddInParameter("@NoWorkOrder", DbType.AnsiString, assistServiceIncoming.NoWorkOrder)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.ChassisMaster))
            DbCommandWrapper.AddInParameter("@KodeChassis", DbType.AnsiString, assistServiceIncoming.KodeChassis)
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistWorkOrderCategory))
            DbCommandWrapper.AddInParameter("@WorkOrderCategoryCode", DbType.AnsiString, assistServiceIncoming.WorkOrderCategoryCode)
            DbCommandWrapper.AddInParameter("@KMService", DbType.Int32, assistServiceIncoming.KMService)
            DbCommandWrapper.AddInParameter("@ServicePlaceID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistServicePlace))
            DbCommandWrapper.AddInParameter("@ServicePlaceCode", DbType.AnsiString, assistServiceIncoming.ServicePlaceCode)
            DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.AssistServiceType))
            DbCommandWrapper.AddInParameter("@ServiceTypeCode", DbType.AnsiString, assistServiceIncoming.ServiceTypeCode)
            DbCommandWrapper.AddInParameter("@TotalLC", DbType.Currency, assistServiceIncoming.TotalLC)
            DbCommandWrapper.AddInParameter("@MetodePembayaran", DbType.AnsiString, assistServiceIncoming.MetodePembayaran)
            DbCommandWrapper.AddInParameter("@Model", DbType.AnsiString, assistServiceIncoming.Model)
            DbCommandWrapper.AddInParameter("@Transmition", DbType.AnsiString, assistServiceIncoming.Transmition)
            DbCommandWrapper.AddInParameter("@DriveSystem", DbType.AnsiString, assistServiceIncoming.DriveSystem)
            DbCommandWrapper.AddInParameter("@RemarksSystem", DbType.AnsiString, assistServiceIncoming.RemarksSystem)
            DbCommandWrapper.AddInParameter("@RemarksSpecial", DbType.AnsiString, assistServiceIncoming.RemarksSpecial)
            DbCommandWrapper.AddInParameter("@RemarksBM", DbType.AnsiString, assistServiceIncoming.RemarksBM)
            DbCommandWrapper.AddInParameter("@WOStatus", DbType.Int16, assistServiceIncoming.WOStatus)
            DbCommandWrapper.AddInParameter("@StatusAktif", DbType.Int16, assistServiceIncoming.StatusAktif)
            DbCommandWrapper.AddInParameter("@ValidateSystemStatus", DbType.Int16, assistServiceIncoming.ValidateSystemStatus)
            'new cols
            DbCommandWrapper.AddInParameter("@CustomerOwnerName", DbType.AnsiString, assistServiceIncoming.CustomerOwnerName)
            DbCommandWrapper.AddInParameter("@CustomerOwnerPhoneNumber", DbType.AnsiString, assistServiceIncoming.CustomerOwnerPhoneNumber)
            DbCommandWrapper.AddInParameter("@CustomerVisitName", DbType.AnsiString, assistServiceIncoming.CustomerVisitName)
            DbCommandWrapper.AddInParameter("@CustomerVisitPhoneNumber", DbType.AnsiString, assistServiceIncoming.CustomerVisitPhoneNumber)
            'end new cols

            DbCommandWrapper.AddInParameter("@StallMasterID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.StallMaster))
            DbCommandWrapper.AddInParameter("@StallCode", DbType.AnsiString, assistServiceIncoming.StallCode)
            DbCommandWrapper.AddInParameter("@ServiceBookingID", DbType.Int32, Me.GetRefObject(assistServiceIncoming.ServiceBooking))
            DbCommandWrapper.AddInParameter("@BookingCode", DbType.AnsiString, assistServiceIncoming.BookingCode)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistServiceIncoming.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistServiceIncoming.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistServiceIncoming

            Dim assistServiceIncoming As AssistServiceIncoming = New AssistServiceIncoming

            assistServiceIncoming.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("TglBukaTransaksi")) Then assistServiceIncoming.TglBukaTransaksi = CType(dr("TglBukaTransaksi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WaktuMasuk")) Then assistServiceIncoming.WaktuMasuk = CType(dr("WaktuMasuk"), TimeSpan)
            'If Not dr.IsDBNull(dr.GetOrdinal("WaktuMasuk")) Then assistServiceIncoming.WaktuMasuk = CType(dr("WaktuMasuk"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("TglTutupTransaksi")) Then assistServiceIncoming.TglTutupTransaksi = CType(dr("TglTutupTransaksi"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WaktuKeluar")) Then assistServiceIncoming.WaktuKeluar = CType(dr("WaktuKeluar"), TimeSpan)
            'If Not dr.IsDBNull(dr.GetOrdinal("WaktuKeluar")) Then assistServiceIncoming.WaktuKeluar = CType(dr("WaktuKeluar"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then assistServiceIncoming.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then assistServiceIncoming.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TrTraineMekanikID")) Then assistServiceIncoming.TrTraineMekanikID = CType(dr("TrTraineMekanikID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KodeMekanik")) Then assistServiceIncoming.KodeMekanik = dr("KodeMekanik").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoWorkOrder")) Then assistServiceIncoming.NoWorkOrder = dr("NoWorkOrder").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KodeChassis")) Then assistServiceIncoming.KodeChassis = dr("KodeChassis").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderCategoryCode")) Then assistServiceIncoming.WorkOrderCategoryCode = dr("WorkOrderCategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KMService")) Then assistServiceIncoming.KMService = CType(dr("KMService"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePlaceCode")) Then assistServiceIncoming.ServicePlaceCode = dr("ServicePlaceCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeCode")) Then assistServiceIncoming.ServiceTypeCode = dr("ServiceTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalLC")) Then assistServiceIncoming.TotalLC = CType(dr("TotalLC"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("MetodePembayaran")) Then assistServiceIncoming.MetodePembayaran = dr("MetodePembayaran").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Model")) Then assistServiceIncoming.Model = dr("Model").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Transmition")) Then assistServiceIncoming.Transmition = dr("Transmition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DriveSystem")) Then assistServiceIncoming.DriveSystem = dr("DriveSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSystem")) Then assistServiceIncoming.RemarksSystem = dr("RemarksSystem").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksSpecial")) Then assistServiceIncoming.RemarksSpecial = dr("RemarksSpecial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarksBM")) Then assistServiceIncoming.RemarksBM = dr("RemarksBM").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WOStatus")) Then assistServiceIncoming.WOStatus = CType(dr("WOStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusAktif")) Then assistServiceIncoming.StatusAktif = CType(dr("StatusAktif"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateSystemStatus")) Then assistServiceIncoming.ValidateSystemStatus = CType(dr("ValidateSystemStatus"), Short)
            'new cols
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerOwnerName")) Then assistServiceIncoming.CustomerOwnerName = dr("CustomerOwnerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerOwnerPhoneNumber")) Then assistServiceIncoming.CustomerOwnerPhoneNumber = dr("CustomerOwnerPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerVisitName")) Then assistServiceIncoming.CustomerVisitName = dr("CustomerVisitName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerVisitPhoneNumber")) Then assistServiceIncoming.CustomerVisitPhoneNumber = dr("CustomerVisitPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallCode")) Then assistServiceIncoming.StallCode = dr("StallCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BookingCode")) Then assistServiceIncoming.BookingCode = dr("BookingCode").ToString
            'end new cols
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistServiceIncoming.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistServiceIncoming.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistServiceIncoming.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistServiceIncoming.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistServiceIncoming.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistUploadLogID")) Then
                assistServiceIncoming.AssistUploadLog = New AssistUploadLog(CType(dr("AssistUploadLogID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                assistServiceIncoming.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                assistServiceIncoming.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                assistServiceIncoming.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderCategoryID")) Then
                assistServiceIncoming.AssistWorkOrderCategory = New AssistWorkOrderCategory(CType(dr("WorkOrderCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ServicePlaceID")) Then
                assistServiceIncoming.AssistServicePlace = New AssistServicePlace(CType(dr("ServicePlaceID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeID")) Then
                assistServiceIncoming.AssistServiceType = New AssistServiceType(CType(dr("ServiceTypeID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("StallMasterID")) Then
                assistServiceIncoming.StallMaster = New StallMaster(CType(dr("StallMasterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ServiceBookingID")) Then
                assistServiceIncoming.ServiceBooking = New ServiceBooking(CType(dr("ServiceBookingID"), Integer))
            End If

            Return assistServiceIncoming

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistServiceIncoming) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistServiceIncoming), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistServiceIncoming).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

