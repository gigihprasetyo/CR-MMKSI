#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceDueDateNotification Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/13/2021 - 10:26:40 AM
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

    Public Class ServiceDueDateNotificationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceDueDateNotification"
        Private m_UpdateStatement As String = "up_UpdateServiceDueDateNotification"
        Private m_RetrieveStatement As String = "up_RetrieveServiceDueDateNotification"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceDueDateNotificationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceDueDateNotification"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim serviceDueDateNotification As ServiceDueDateNotification = Nothing
            While dr.Read

                serviceDueDateNotification = Me.CreateObject(dr)

            End While

            Return serviceDueDateNotification

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim serviceDueDateNotificationList As ArrayList = New ArrayList

            While dr.Read
                Dim serviceDueDateNotification As ServiceDueDateNotification = Me.CreateObject(dr)
                serviceDueDateNotificationList.Add(serviceDueDateNotification)
            End While

            Return serviceDueDateNotificationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceDueDateNotification As ServiceDueDateNotification = CType(obj, ServiceDueDateNotification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceDueDateNotification.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceDueDateNotification As ServiceDueDateNotification = CType(obj, ServiceDueDateNotification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NameRecipient", DbType.AnsiString, serviceDueDateNotification.NameRecipient)
            DbCommandWrapper.AddInParameter("@EmailDealer", DbType.AnsiString, serviceDueDateNotification.EmailDealer)
            DbCommandWrapper.AddInParameter("@PositionRecipient", DbType.AnsiString, serviceDueDateNotification.PositionRecipient)
            DbCommandWrapper.AddInParameter("@EmailNotificationKind", DbType.Int16, serviceDueDateNotification.EmailNotificationKind)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceDueDateNotification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, serviceDueDateNotification.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, serviceDueDateNotification.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(serviceDueDateNotification.Dealer))

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

            Dim serviceDueDateNotification As ServiceDueDateNotification = CType(obj, ServiceDueDateNotification)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceDueDateNotification.ID)
            DbCommandWrapper.AddInParameter("@NameRecipient", DbType.AnsiString, serviceDueDateNotification.NameRecipient)
            DbCommandWrapper.AddInParameter("@EmailDealer", DbType.AnsiString, serviceDueDateNotification.EmailDealer)
            DbCommandWrapper.AddInParameter("@PositionRecipient", DbType.AnsiString, serviceDueDateNotification.PositionRecipient)
            DbCommandWrapper.AddInParameter("@EmailNotificationKind", DbType.Int16, serviceDueDateNotification.EmailNotificationKind)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceDueDateNotification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, serviceDueDateNotification.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, serviceDueDateNotification.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(serviceDueDateNotification.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceDueDateNotification

            Dim serviceDueDateNotification As ServiceDueDateNotification = New ServiceDueDateNotification

            serviceDueDateNotification.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NameRecipient")) Then serviceDueDateNotification.NameRecipient = dr("NameRecipient").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EmailDealer")) Then serviceDueDateNotification.EmailDealer = dr("EmailDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PositionRecipient")) Then serviceDueDateNotification.PositionRecipient = dr("PositionRecipient").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EmailNotificationKind")) Then serviceDueDateNotification.EmailNotificationKind = CType(dr("EmailNotificationKind"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then serviceDueDateNotification.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then serviceDueDateNotification.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then serviceDueDateNotification.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then serviceDueDateNotification.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then serviceDueDateNotification.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
            '    serviceDueDateNotification.DealerID = CType(dr("DealerID"), Short)
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                serviceDueDateNotification.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return serviceDueDateNotification

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceDueDateNotification) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceDueDateNotification), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceDueDateNotification).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
