
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDueDateNotification Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 25/06/2020 - 10:32:30
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

    Public Class SparePartDueDateNotificationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartDueDateNotification"
        Private m_UpdateStatement As String = "up_UpdateSparePartDueDateNotification"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartDueDateNotification"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartDueDateNotificationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartDueDateNotification"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartDueDateNotification As SparePartDueDateNotification = Nothing
            While dr.Read

                sparePartDueDateNotification = Me.CreateObject(dr)

            End While

            Return sparePartDueDateNotification

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartDueDateNotificationList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartDueDateNotification As SparePartDueDateNotification = Me.CreateObject(dr)
                sparePartDueDateNotificationList.Add(sparePartDueDateNotification)
            End While

            Return sparePartDueDateNotificationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDueDateNotification As SparePartDueDateNotification = CType(obj, SparePartDueDateNotification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDueDateNotification.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDueDateNotification As SparePartDueDateNotification = CType(obj, SparePartDueDateNotification)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NameRecipient", DbType.AnsiString, sparePartDueDateNotification.NameRecipient)
            DbCommandWrapper.AddInParameter("@EmailDealer", DbType.AnsiString, sparePartDueDateNotification.EmailDealer)
            DbCommandWrapper.AddInParameter("@PositionRecipient", DbType.AnsiString, sparePartDueDateNotification.PositionRecipient)
            DbCommandWrapper.AddInParameter("@EmailNotificationKind", DbType.Int16, sparePartDueDateNotification.EmailNotificationKind)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDueDateNotification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartDueDateNotification.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartDueDateNotification.Dealer))

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

            Dim sparePartDueDateNotification As SparePartDueDateNotification = CType(obj, SparePartDueDateNotification)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDueDateNotification.ID)
            DbCommandWrapper.AddInParameter("@NameRecipient", DbType.AnsiString, sparePartDueDateNotification.NameRecipient)
            DbCommandWrapper.AddInParameter("@EmailDealer", DbType.AnsiString, sparePartDueDateNotification.EmailDealer)
            DbCommandWrapper.AddInParameter("@PositionRecipient", DbType.AnsiString, sparePartDueDateNotification.PositionRecipient)
            DbCommandWrapper.AddInParameter("@EmailNotificationKind", DbType.Int16, sparePartDueDateNotification.EmailNotificationKind)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDueDateNotification.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartDueDateNotification.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sparePartDueDateNotification.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartDueDateNotification

            Dim sparePartDueDateNotification As SparePartDueDateNotification = New SparePartDueDateNotification

            sparePartDueDateNotification.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NameRecipient")) Then sparePartDueDateNotification.NameRecipient = dr("NameRecipient").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EmailDealer")) Then sparePartDueDateNotification.EmailDealer = dr("EmailDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PositionRecipient")) Then sparePartDueDateNotification.PositionRecipient = dr("PositionRecipient").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EmailNotificationKind")) Then sparePartDueDateNotification.EmailNotificationKind = CType(dr("EmailNotificationKind"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartDueDateNotification.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartDueDateNotification.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartDueDateNotification.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartDueDateNotification.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartDueDateNotification.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sparePartDueDateNotification.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return sparePartDueDateNotification

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartDueDateNotification) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartDueDateNotification), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartDueDateNotification).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

