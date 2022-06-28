#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_MobileServiceReminder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:26:31 PM
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

    Public Class VWI_MobileServiceReminderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_MobileServiceReminder"
        Private m_UpdateStatement As String = "up_UpdateVWI_MobileServiceReminder"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_MobileServiceReminder"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_MobileServiceReminderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_MobileServiceReminder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_MobileServiceReminder As VWI_MobileServiceReminder = Nothing
            While dr.Read

                VWI_MobileServiceReminder = Me.CreateObject(dr)

            End While

            Return VWI_MobileServiceReminder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_MobileServiceReminderList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_MobileServiceReminder As VWI_MobileServiceReminder = Me.CreateObject(dr)
                VWI_MobileServiceReminderList.Add(VWI_MobileServiceReminder)
            End While

            Return VWI_MobileServiceReminderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_MobileServiceReminder As VWI_MobileServiceReminder = CType(obj, VWI_MobileServiceReminder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_MobileServiceReminder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_MobileServiceReminder As VWI_MobileServiceReminder = CType(obj, VWI_MobileServiceReminder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_MobileServiceReminder.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_MobileServiceReminder.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ServiceReminderDate", DbType.DateTime, VWI_MobileServiceReminder.ServiceReminderDate)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, VWI_MobileServiceReminder.KindCode)
            DbCommandWrapper.AddInParameter("@KindDescription", DbType.AnsiString, VWI_MobileServiceReminder.KindDescription)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, VWI_MobileServiceReminder.Remark)
            DbCommandWrapper.AddInParameter("@ReminderType", DbType.Int32, VWI_MobileServiceReminder.ReminderType)
            DbCommandWrapper.AddInParameter("@ReminderDelta", DbType.Int32, VWI_MobileServiceReminder.ReminderDelta)


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

            Dim VWI_MobileServiceReminder As VWI_MobileServiceReminder = CType(obj, VWI_MobileServiceReminder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_MobileServiceReminder.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_MobileServiceReminder.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_MobileServiceReminder.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ServiceReminderDate", DbType.DateTime, VWI_MobileServiceReminder.ServiceReminderDate)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, VWI_MobileServiceReminder.KindCode)
            DbCommandWrapper.AddInParameter("@KindDescription", DbType.AnsiString, VWI_MobileServiceReminder.KindDescription)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, VWI_MobileServiceReminder.Remark)
            DbCommandWrapper.AddInParameter("@ReminderType", DbType.Int32, VWI_MobileServiceReminder.ReminderType)
            DbCommandWrapper.AddInParameter("@ReminderDelta", DbType.Int32, VWI_MobileServiceReminder.ReminderDelta)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_MobileServiceReminder

            Dim VWI_MobileServiceReminder As VWI_MobileServiceReminder = New VWI_MobileServiceReminder

            VWI_MobileServiceReminder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_MobileServiceReminder.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then VWI_MobileServiceReminder.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceReminderDate")) Then VWI_MobileServiceReminder.ServiceReminderDate = CType(dr("ServiceReminderDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then VWI_MobileServiceReminder.KindCode = dr("KindCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindDescription")) Then VWI_MobileServiceReminder.KindDescription = dr("KindDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then VWI_MobileServiceReminder.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReminderType")) Then VWI_MobileServiceReminder.ReminderType = CType(dr("ReminderType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReminderDelta")) Then VWI_MobileServiceReminder.ReminderDelta = CType(dr("ReminderDelta"), Integer)

            Return VWI_MobileServiceReminder

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_MobileServiceReminder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_MobileServiceReminder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_MobileServiceReminder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
