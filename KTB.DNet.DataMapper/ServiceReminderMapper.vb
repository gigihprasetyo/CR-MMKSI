
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceReminder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2020 - 12:23:31
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

    Public Class ServiceReminderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceReminder"
        Private m_UpdateStatement As String = "up_UpdateServiceReminder"
        Private m_RetrieveStatement As String = "up_RetrieveServiceReminder"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceReminderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceReminder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim serviceReminder As ServiceReminder = Nothing
            While dr.Read

                serviceReminder = Me.CreateObject(dr)

            End While

            Return serviceReminder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim serviceReminderList As ArrayList = New ArrayList

            While dr.Read
                Dim serviceReminder As ServiceReminder = Me.CreateObject(dr)
                serviceReminderList.Add(serviceReminder)
            End While

            Return serviceReminderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceReminder As ServiceReminder = CType(obj, ServiceReminder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceReminder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceReminder As ServiceReminder = CType(obj, ServiceReminder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesforceID", DbType.AnsiString, serviceReminder.SalesforceID)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, serviceReminder.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(serviceReminder.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(serviceReminder.DealerBranch))
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, serviceReminder.ChassisNumber)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.DateTime, serviceReminder.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(serviceReminder.ChassisMaster))
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, serviceReminder.EngineNumber)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, serviceReminder.VehicleType)
            DbCommandWrapper.AddInParameter("@WONumber", DbType.AnsiString, serviceReminder.WONumber)
            DbCommandWrapper.AddInParameter("@ServiceReminderDate", DbType.DateTime, serviceReminder.ServiceReminderDate)
            DbCommandWrapper.AddInParameter("@MaxFUDealerDate", DbType.DateTime, serviceReminder.MaxFUDealerDate)
            DbCommandWrapper.AddInParameter("@BookingDate", DbType.DateTime, serviceReminder.BookingDate)
            DbCommandWrapper.AddInParameter("@BookingTime", DbType.AnsiString, serviceReminder.BookingTime)
            DbCommandWrapper.AddInParameter("@CaseNumber", DbType.AnsiString, serviceReminder.CaseNumber)
            'DbCommandWrapper.AddInParameter("@AssistServiceIncomingID", DbType.Int32, serviceReminder.AssistServiceIncomingID)
            DbCommandWrapper.AddInParameter("@AssistServiceIncomingID", DbType.Int32, Me.GetRefObject(serviceReminder.AssistServiceIncoming))
            DbCommandWrapper.AddInParameter("@ServiceActualDate", DbType.DateTime, serviceReminder.ServiceActualDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, serviceReminder.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerPhoneNumber", DbType.AnsiString, serviceReminder.CustomerPhoneNumber)
            DbCommandWrapper.AddInParameter("@ContactPersonName", DbType.AnsiString, serviceReminder.ContactPersonName)
            DbCommandWrapper.AddInParameter("@ContactPersonPhoneNumber", DbType.AnsiString, serviceReminder.ContactPersonPhoneNumber)
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(serviceReminder.PMKind))
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.Byte, serviceReminder.TransactionType)
            DbCommandWrapper.AddInParameter("@ActualKM", DbType.Int32, serviceReminder.ActualKM)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, serviceReminder.PKTDate)
            DbCommandWrapper.AddInParameter("@SourceFlag", DbType.Int16, serviceReminder.SourceFlag)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, serviceReminder.Status)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, serviceReminder.Remark)
            DbCommandWrapper.AddInParameter("@ActualServiceDealerID", DbType.Int32, Me.GetRefObject(serviceReminder.ActualServiceDealer))
            DbCommandWrapper.AddInParameter("@ActualserviceDealerBranchID", DbType.Int32, Me.GetRefObject(serviceReminder.ActualServiceDealerBranch))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int16, Me.GetRefObject(serviceReminder.Category))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceReminder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, serviceReminder.LastUpdateBy)
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

            Dim serviceReminder As ServiceReminder = CType(obj, ServiceReminder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceReminder.ID)
            DbCommandWrapper.AddInParameter("@SalesforceID", DbType.AnsiString, serviceReminder.SalesforceID)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, serviceReminder.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(serviceReminder.Dealer))
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, serviceReminder.ChassisNumber)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.DateTime, serviceReminder.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(serviceReminder.ChassisMaster))
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, serviceReminder.EngineNumber)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, serviceReminder.VehicleType)
            DbCommandWrapper.AddInParameter("@WONumber", DbType.AnsiString, serviceReminder.WONumber)
            DbCommandWrapper.AddInParameter("@ServiceReminderDate", DbType.DateTime, serviceReminder.ServiceReminderDate)
            DbCommandWrapper.AddInParameter("@MaxFUDealerDate", DbType.DateTime, serviceReminder.MaxFUDealerDate)
            DbCommandWrapper.AddInParameter("@BookingDate", DbType.DateTime, serviceReminder.BookingDate)
            DbCommandWrapper.AddInParameter("@BookingTime", DbType.AnsiString, serviceReminder.BookingTime)
            DbCommandWrapper.AddInParameter("@CaseNumber", DbType.AnsiString, serviceReminder.CaseNumber)
            'DbCommandWrapper.AddInParameter("@AssistServiceIncomingID", DbType.Int32, serviceReminder.AssistServiceIncomingID)
            DbCommandWrapper.AddInParameter("@AssistServiceIncomingID", DbType.Int32, Me.GetRefObject(serviceReminder.AssistServiceIncoming))
            DbCommandWrapper.AddInParameter("@ServiceActualDate", DbType.DateTime, serviceReminder.ServiceActualDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, serviceReminder.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerPhoneNumber", DbType.AnsiString, serviceReminder.CustomerPhoneNumber)
            DbCommandWrapper.AddInParameter("@ContactPersonName", DbType.AnsiString, serviceReminder.ContactPersonName)
            DbCommandWrapper.AddInParameter("@ContactPersonPhoneNumber", DbType.AnsiString, serviceReminder.ContactPersonPhoneNumber)
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(serviceReminder.PMKind))
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.Byte, serviceReminder.TransactionType)
            DbCommandWrapper.AddInParameter("@ActualKM", DbType.Int32, serviceReminder.ActualKM)
            DbCommandWrapper.AddInParameter("@PKTDate", DbType.DateTime, serviceReminder.PKTDate)
            DbCommandWrapper.AddInParameter("@SourceFlag", DbType.Int16, serviceReminder.SourceFlag)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, serviceReminder.Status)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, serviceReminder.Remark)
            DbCommandWrapper.AddInParameter("@ActualServiceDealerID", DbType.Int32, Me.GetRefObject(serviceReminder.ActualServiceDealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(serviceReminder.DealerBranch))
            DbCommandWrapper.AddInParameter("@ActualserviceDealerBranchID", DbType.Int32, Me.GetRefObject(serviceReminder.ActualServiceDealerBranch))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int16, Me.GetRefObject(serviceReminder.Category))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceReminder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, serviceReminder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceReminder

            Dim serviceReminder As ServiceReminder = New ServiceReminder

            serviceReminder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesforceID")) Then serviceReminder.SalesforceID = dr("SalesforceID").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then serviceReminder.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then serviceReminder.ChassisNumber = dr("ChassisNumber").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then serviceReminder.ChassisMasterID = CType(dr("ChassisMasterID"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then serviceReminder.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then serviceReminder.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WONumber")) Then serviceReminder.WONumber = dr("WONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceReminderDate")) Then serviceReminder.ServiceReminderDate = CType(dr("ServiceReminderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxFUDealerDate")) Then serviceReminder.MaxFUDealerDate = CType(dr("MaxFUDealerDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingDate")) Then serviceReminder.BookingDate = CType(dr("BookingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingTime")) Then serviceReminder.BookingTime = dr("BookingTime").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CaseNumber")) Then serviceReminder.CaseNumber = dr("CaseNumber").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("AssistServiceIncomingID")) Then serviceReminder.AssistServiceIncomingID = CType(dr("AssistServiceIncomingID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceActualDate")) Then serviceReminder.ServiceActualDate = CType(dr("ServiceActualDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then serviceReminder.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPhoneNumber")) Then serviceReminder.CustomerPhoneNumber = dr("CustomerPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPersonName")) Then serviceReminder.ContactPersonName = dr("ContactPersonName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPersonPhoneNumber")) Then serviceReminder.ContactPersonPhoneNumber = dr("ContactPersonPhoneNumber").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ServiceType")) Then serviceReminder.ServiceType = CType(dr("ServiceType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionType")) Then serviceReminder.TransactionType = CType(dr("TransactionType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualKM")) Then serviceReminder.ActualKM = CType(dr("ActualKM"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDate")) Then serviceReminder.PKTDate = CType(dr("PKTDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SourceFlag")) Then serviceReminder.SourceFlag = CType(dr("SourceFlag"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then serviceReminder.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then serviceReminder.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then serviceReminder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then serviceReminder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then serviceReminder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then serviceReminder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then serviceReminder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                serviceReminder.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                serviceReminder.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("AssistServiceIncomingID")) Then
                serviceReminder.AssistServiceIncoming = New AssistServiceIncoming(CType(dr("AssistServiceIncomingID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then
                serviceReminder.PMKind = New PMKind(CType(dr("PMKindID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ActualServiceDealerID")) Then
                serviceReminder.ActualServiceDealer = New Dealer(CType(dr("ActualServiceDealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                serviceReminder.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ActualServiceDealerBranchID")) Then
                serviceReminder.ActualServiceDealerBranch = New DealerBranch(CType(dr("ActualServiceDealerBranchID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                serviceReminder.Category = New Category(CType(dr("CategoryID"), Short))
            End If

            Return serviceReminder

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceReminder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceReminder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceReminder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

