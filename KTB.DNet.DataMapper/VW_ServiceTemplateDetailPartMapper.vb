
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VW_ServiceTemplateDetailPart Objects Mapper.
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

    Public Class VW_ServiceTemplateDetailPartMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVW_ServiceTemplateDetailPart"
        Private m_UpdateStatement As String = "up_UpdateVW_ServiceTemplateDetailPart"
        Private m_RetrieveStatement As String = "up_RetrieveVW_ServiceTemplateDetailPart"
        Private m_RetrieveListStatement As String = "up_RetrieveVW_ServiceTemplateDetailPartList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVW_ServiceTemplateDetailPart"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vW_ServiceTemplateDetailPart As VW_ServiceTemplateDetailPart = Nothing
            While dr.Read

                vW_ServiceTemplateDetailPart = Me.CreateObject(dr)

            End While

            Return vW_ServiceTemplateDetailPart

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vW_ServiceTemplateDetailPartList As ArrayList = New ArrayList

            While dr.Read
                Dim vW_ServiceTemplateDetailPart As VW_ServiceTemplateDetailPart = Me.CreateObject(dr)
                vW_ServiceTemplateDetailPartList.Add(vW_ServiceTemplateDetailPart)
            End While

            Return vW_ServiceTemplateDetailPartList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vW_ServiceTemplateDetailPart As VW_ServiceTemplateDetailPart = CType(obj, VW_ServiceTemplateDetailPart)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_ServiceTemplateDetailPart.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            '    Dim vW_ServiceTemplateDetailPart As VW_ServiceTemplateDetailPart = CType(obj, VW_ServiceTemplateDetailPart)
            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            '    DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_ServiceTemplateDetailPart.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_ServiceTemplateDetailPart.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_ServiceTemplateDetailPart.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_ServiceTemplateDetailPart.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_ServiceTemplateDetailPart.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_ServiceTemplateDetailPart.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_ServiceTemplateDetailPart.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_ServiceTemplateDetailPart.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_ServiceTemplateDetailPart.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_ServiceTemplateDetailPart.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_ServiceTemplateDetailPart.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_ServiceTemplateDetailPart.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_ServiceTemplateDetailPart.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_ServiceTemplateDetailPart.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_ServiceTemplateDetailPart.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_ServiceTemplateDetailPart.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_ServiceTemplateDetailPart.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_ServiceTemplateDetailPart.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_ServiceTemplateDetailPart.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_ServiceTemplateDetailPart.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_ServiceTemplateDetailPart.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vW_ServiceTemplateDetailPart.LastUpdateBy)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_ServiceTemplateDetailPart.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_ServiceTemplateDetailPart.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_ServiceTemplateDetailPart.ReasonDescription)


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

            '    Dim vW_ServiceTemplateDetailPart As VW_ServiceTemplateDetailPart = CType(obj, VW_ServiceTemplateDetailPart)

            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            '    DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_ServiceTemplateDetailPart.ID)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_ServiceTemplateDetailPart.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_ServiceTemplateDetailPart.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_ServiceTemplateDetailPart.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_ServiceTemplateDetailPart.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_ServiceTemplateDetailPart.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_ServiceTemplateDetailPart.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_ServiceTemplateDetailPart.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_ServiceTemplateDetailPart.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_ServiceTemplateDetailPart.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_ServiceTemplateDetailPart.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_ServiceTemplateDetailPart.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_ServiceTemplateDetailPart.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_ServiceTemplateDetailPart.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_ServiceTemplateDetailPart.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_ServiceTemplateDetailPart.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_ServiceTemplateDetailPart.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_ServiceTemplateDetailPart.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_ServiceTemplateDetailPart.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_ServiceTemplateDetailPart.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_ServiceTemplateDetailPart.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_ServiceTemplateDetailPart.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vW_ServiceTemplateDetailPart.CreatedBy)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_ServiceTemplateDetailPart.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_ServiceTemplateDetailPart.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_ServiceTemplateDetailPart.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_ServiceTemplateDetailPart.ReasonDescription)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VW_ServiceTemplateDetailPart

            Dim vW_ServiceTemplateDetailPart As VW_ServiceTemplateDetailPart = New VW_ServiceTemplateDetailPart

            vW_ServiceTemplateDetailPart.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TemplateType")) Then vW_ServiceTemplateDetailPart.TemplateType = dr("TemplateType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then vW_ServiceTemplateDetailPart.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then vW_ServiceTemplateDetailPart.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTemplateHeaderID")) Then vW_ServiceTemplateDetailPart.ServiceTemplateHeaderID = dr("ServiceTemplateHeaderID").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then vW_ServiceTemplateDetailPart.LinkID = dr("ID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then vW_ServiceTemplateDetailPart.SparepartMasterID = dr("SparePartMasterID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then vW_ServiceTemplateDetailPart.PartAmount = CType(dr("PartAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartQuantity")) Then vW_ServiceTemplateDetailPart.PartQuantity = CType(dr("PartQuantity"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vW_ServiceTemplateDetailPart.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vW_ServiceTemplateDetailPart.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vW_ServiceTemplateDetailPart.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then vW_ServiceTemplateDetailPart.LastUpdateBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then vW_ServiceTemplateDetailPart.LastUpdateTime = CType(dr("LastUpdatedTime"), DateTime)
            Return vW_ServiceTemplateDetailPart

        End Function

        Private Sub SetTableName()

            If Not (GetType(VW_ServiceTemplateDetailPart) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VW_ServiceTemplateDetailPart), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VW_ServiceTemplateDetailPart).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


