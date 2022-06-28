
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VW_ServiceTemplateHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 11/15/2016 - 8:53:46 AM
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

    Public Class VW_ServiceTemplateHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVW_ServiceTemplateHeader"
        Private m_UpdateStatement As String = "up_UpdateVW_ServiceTemplateHeader"
        Private m_RetrieveStatement As String = "up_RetrieveVW_ServiceTemplateHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveVW_ServiceTemplateHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVW_ServiceTemplateHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vW_ServiceTemplateHeader As VW_ServiceTemplateHeader = Nothing
            While dr.Read

                vW_ServiceTemplateHeader = Me.CreateObject(dr)

            End While

            Return vW_ServiceTemplateHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vW_ServiceTemplateHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim vW_ServiceTemplateHeader As VW_ServiceTemplateHeader = Me.CreateObject(dr)
                vW_ServiceTemplateHeaderList.Add(vW_ServiceTemplateHeader)
            End While

            Return vW_ServiceTemplateHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vW_ServiceTemplateHeader As VW_ServiceTemplateHeader = CType(obj, VW_ServiceTemplateHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_ServiceTemplateHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            '    Dim vW_ServiceTemplateHeader As VW_ServiceTemplateHeader = CType(obj, VW_ServiceTemplateHeader)
            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            '    DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_ServiceTemplateHeader.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_ServiceTemplateHeader.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_ServiceTemplateHeader.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_ServiceTemplateHeader.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_ServiceTemplateHeader.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_ServiceTemplateHeader.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_ServiceTemplateHeader.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_ServiceTemplateHeader.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_ServiceTemplateHeader.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_ServiceTemplateHeader.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_ServiceTemplateHeader.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_ServiceTemplateHeader.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_ServiceTemplateHeader.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_ServiceTemplateHeader.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_ServiceTemplateHeader.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_ServiceTemplateHeader.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_ServiceTemplateHeader.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_ServiceTemplateHeader.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_ServiceTemplateHeader.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_ServiceTemplateHeader.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_ServiceTemplateHeader.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vW_ServiceTemplateHeader.LastUpdateBy)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_ServiceTemplateHeader.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_ServiceTemplateHeader.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_ServiceTemplateHeader.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_ServiceTemplateHeader.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_ServiceTemplateHeader.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_ServiceTemplateHeader.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_ServiceTemplateHeader.ReasonDescription)


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

            '    Dim vW_ServiceTemplateHeader As VW_ServiceTemplateHeader = CType(obj, VW_ServiceTemplateHeader)

            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            '    DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_ServiceTemplateHeader.ID)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_ServiceTemplateHeader.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_ServiceTemplateHeader.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_ServiceTemplateHeader.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_ServiceTemplateHeader.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_ServiceTemplateHeader.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_ServiceTemplateHeader.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_ServiceTemplateHeader.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_ServiceTemplateHeader.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_ServiceTemplateHeader.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_ServiceTemplateHeader.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_ServiceTemplateHeader.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_ServiceTemplateHeader.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_ServiceTemplateHeader.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_ServiceTemplateHeader.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_ServiceTemplateHeader.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_ServiceTemplateHeader.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_ServiceTemplateHeader.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_ServiceTemplateHeader.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_ServiceTemplateHeader.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_ServiceTemplateHeader.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_ServiceTemplateHeader.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vW_ServiceTemplateHeader.CreatedBy)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_ServiceTemplateHeader.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_ServiceTemplateHeader.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_ServiceTemplateHeader.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_ServiceTemplateHeader.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_ServiceTemplateHeader.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_ServiceTemplateHeader.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_ServiceTemplateHeader.ReasonDescription)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VW_ServiceTemplateHeader

            Dim vW_ServiceTemplateHeader As VW_ServiceTemplateHeader = New VW_ServiceTemplateHeader

            vW_ServiceTemplateHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TemplateType")) Then vW_ServiceTemplateHeader.TemplateType = dr("TemplateType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then vW_ServiceTemplateHeader.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then vW_ServiceTemplateHeader.KindCode = dr("KindCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindDescription")) Then vW_ServiceTemplateHeader.KindDescription = dr("KindDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleVariant")) Then vW_ServiceTemplateHeader.VehicleVariant = dr("VehicleVariant").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then vW_ServiceTemplateHeader.LinkID = dr("ID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindID")) Then vW_ServiceTemplateHeader.KindID = dr("KindID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then vW_ServiceTemplateHeader.VechileTypeID = dr("VechileTypeID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then vW_ServiceTemplateHeader.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vW_ServiceTemplateHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vW_ServiceTemplateHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vW_ServiceTemplateHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then vW_ServiceTemplateHeader.LastUpdateBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then vW_ServiceTemplateHeader.LastUpdateTime = CType(dr("LastUpdatedTime"), DateTime)
            Return vW_ServiceTemplateHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(VW_ServiceTemplateHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VW_ServiceTemplateHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VW_ServiceTemplateHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


