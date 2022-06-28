
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ServiceReminder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 07/03/2018 - 13:18:56
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

    Public Class VWI_ServiceReminderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_ServiceReminder"
        Private m_UpdateStatement As String = "up_UpdateVWI_ServiceReminder"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_ServiceReminder"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ServiceReminderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_ServiceReminder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_ServiceReminder As VWI_ServiceReminder = Nothing
            While dr.Read

                VWI_ServiceReminder = Me.CreateObject(dr)

            End While

            Return VWI_ServiceReminder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_ServiceReminderList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_ServiceReminder As VWI_ServiceReminder = Me.CreateObject(dr)
                VWI_ServiceReminderList.Add(VWI_ServiceReminder)
            End While

            Return VWI_ServiceReminderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceReminder As VWI_ServiceReminder = CType(obj, VWI_ServiceReminder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceReminder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceReminder As VWI_ServiceReminder = CType(obj, VWI_ServiceReminder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            'DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, VWI_ServiceReminder.NomorSurat)
            'DbCommandWrapper.AddInParameter("@Status", DbType.Int16, VWI_ServiceReminder.Status)
            'DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, VWI_ServiceReminder.BenefitRegNo)
            'DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, VWI_ServiceReminder.Remarks)
            'DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_ServiceReminder.RowStatus)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)
            ''DbCommandWrapper.AddInParameter("@DetailRowStatus", DbType.Int16, VWI_ServiceReminder.DetailRowStatus)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, VWI_ServiceReminder.DealerID)
            'DbCommandWrapper.AddInParameter("@DealerCode", DbType.String, VWI_ServiceReminder.DealerCode)
            'DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, VWI_ServiceReminder.FakturValidationStart)
            'DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, VWI_ServiceReminder.FakturValidationEnd)
            'DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, VWI_ServiceReminder.FakturOpenStart)
            'DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, VWI_ServiceReminder.FakturOpenEnd)
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, VWI_ServiceReminder.VehicleTypeID)
            'DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, VWI_ServiceReminder.VehicleTypeCode)
            'DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, VWI_ServiceReminder.VehicleTypeDesc)            


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

            Dim VWI_ServiceReminder As VWI_ServiceReminder = CType(obj, VWI_ServiceReminder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            'DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceReminder.ID)
            'DbCommandWrapper.AddInParameter("@NomorSurat", DbType.AnsiString, VWI_ServiceReminder.NomorSurat)
            'DbCommandWrapper.AddInParameter("@Status", DbType.Int16, VWI_ServiceReminder.Status)
            'DbCommandWrapper.AddInParameter("@BenefitRegNo", DbType.AnsiString, VWI_ServiceReminder.BenefitRegNo)
            'DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, VWI_ServiceReminder.Remarks)
            'DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_ServiceReminder.RowStatus)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)
            ''DbCommandWrapper.AddInParameter("@DetailRowStatus", DbType.Int16, VWI_ServiceReminder.DetailRowStatus)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, VWI_ServiceReminder.DealerID)
            'DbCommandWrapper.AddInParameter("@DealerCode", DbType.String, VWI_ServiceReminder.DealerCode)
            'DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, VWI_ServiceReminder.FakturValidationStart)
            'DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, VWI_ServiceReminder.FakturValidationEnd)
            'DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, VWI_ServiceReminder.FakturOpenStart)
            'DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, VWI_ServiceReminder.FakturOpenEnd)
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, VWI_ServiceReminder.VehicleTypeID)
            'DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, VWI_ServiceReminder.VehicleTypeCode)
            'DbCommandWrapper.AddInParameter("@VehicleTypeDesc", DbType.AnsiString, VWI_ServiceReminder.VehicleTypeDesc)            

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ServiceReminder

            Dim vwi_ServiceReminder As VWI_ServiceReminder = New VWI_ServiceReminder

            vwi_ServiceReminder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesforceID")) Then vwi_ServiceReminder.SalesforceID = dr("SalesforceID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vwi_ServiceReminder.DealerCode = CType(dr("DealerCode"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then vwi_ServiceReminder.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then vwi_ServiceReminder.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then vwi_ServiceReminder.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WONumber")) Then vwi_ServiceReminder.WONumber = dr("WONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceReminderDate")) Then vwi_ServiceReminder.ServiceReminderDate = CType(dr("ServiceReminderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxFUDealerDate")) Then vwi_ServiceReminder.MaxFUDealerDate = CType(dr("MaxFUDealerDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingDate")) Then vwi_ServiceReminder.BookingDate = CType(dr("BookingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BookingTime")) Then vwi_ServiceReminder.BookingTime = CType(dr("BookingTime"), TimeSpan)
            If Not dr.IsDBNull(dr.GetOrdinal("CaseNumber")) Then vwi_ServiceReminder.CaseNumber = dr("CaseNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AssistServiceIncomingID")) Then vwi_ServiceReminder.AssistServiceIncomingID = CType(dr("AssistServiceIncomingID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceActualDate")) Then vwi_ServiceReminder.ServiceActualDate = CType(dr("ServiceActualDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then vwi_ServiceReminder.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPhoneNumber")) Then vwi_ServiceReminder.CustomerPhoneNumber = dr("CustomerPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPersonName")) Then vwi_ServiceReminder.ContactPersonName = dr("ContactPersonName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPersonPhoneNumber")) Then vwi_ServiceReminder.ContactPersonPhoneNumber = dr("ContactPersonPhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceType")) Then vwi_ServiceReminder.ServiceType = dr("ServiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionType")) Then vwi_ServiceReminder.TransactionType = CType(dr("TransactionType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualKM")) Then vwi_ServiceReminder.ActualKM = CType(dr("ActualKM"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vwi_ServiceReminder.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vwi_ServiceReminder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vwi_ServiceReminder

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ServiceReminder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ServiceReminder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ServiceReminder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace