
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VW_ServiceTemplateLabor Objects Mapper.
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

    Public Class VW_ServiceTemplateLaborMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVW_ServiceTemplateLabor"
        Private m_UpdateStatement As String = "up_UpdateVW_ServiceTemplateLabor"
        Private m_RetrieveStatement As String = "up_RetrieveVW_ServiceTemplateLabor"
        Private m_RetrieveListStatement As String = "up_RetrieveVW_ServiceTemplateLaborList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVW_ServiceTemplateLabor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vW_ServiceTemplateLabor As VW_ServiceTemplateLabor = Nothing
            While dr.Read

                vW_ServiceTemplateLabor = Me.CreateObject(dr)

            End While

            Return vW_ServiceTemplateLabor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vW_ServiceTemplateLaborList As ArrayList = New ArrayList

            While dr.Read
                Dim vW_ServiceTemplateLabor As VW_ServiceTemplateLabor = Me.CreateObject(dr)
                vW_ServiceTemplateLaborList.Add(vW_ServiceTemplateLabor)
            End While

            Return vW_ServiceTemplateLaborList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vW_ServiceTemplateLabor As VW_ServiceTemplateLabor = CType(obj, VW_ServiceTemplateLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_ServiceTemplateLabor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            '    Dim vW_ServiceTemplateLabor As VW_ServiceTemplateLabor = CType(obj, VW_ServiceTemplateLabor)
            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            '    DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_ServiceTemplateLabor.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_ServiceTemplateLabor.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_ServiceTemplateLabor.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_ServiceTemplateLabor.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_ServiceTemplateLabor.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_ServiceTemplateLabor.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_ServiceTemplateLabor.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_ServiceTemplateLabor.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_ServiceTemplateLabor.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_ServiceTemplateLabor.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_ServiceTemplateLabor.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_ServiceTemplateLabor.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_ServiceTemplateLabor.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_ServiceTemplateLabor.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_ServiceTemplateLabor.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_ServiceTemplateLabor.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_ServiceTemplateLabor.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_ServiceTemplateLabor.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_ServiceTemplateLabor.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_ServiceTemplateLabor.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_ServiceTemplateLabor.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vW_ServiceTemplateLabor.LastUpdateBy)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_ServiceTemplateLabor.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_ServiceTemplateLabor.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_ServiceTemplateLabor.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_ServiceTemplateLabor.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_ServiceTemplateLabor.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_ServiceTemplateLabor.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_ServiceTemplateLabor.ReasonDescription)


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

            '    Dim vW_ServiceTemplateLabor As VW_ServiceTemplateLabor = CType(obj, VW_ServiceTemplateLabor)

            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            '    DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_ServiceTemplateLabor.ID)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_ServiceTemplateLabor.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_ServiceTemplateLabor.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_ServiceTemplateLabor.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_ServiceTemplateLabor.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_ServiceTemplateLabor.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_ServiceTemplateLabor.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_ServiceTemplateLabor.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_ServiceTemplateLabor.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_ServiceTemplateLabor.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_ServiceTemplateLabor.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_ServiceTemplateLabor.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_ServiceTemplateLabor.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_ServiceTemplateLabor.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_ServiceTemplateLabor.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_ServiceTemplateLabor.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_ServiceTemplateLabor.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_ServiceTemplateLabor.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_ServiceTemplateLabor.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_ServiceTemplateLabor.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_ServiceTemplateLabor.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_ServiceTemplateLabor.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vW_ServiceTemplateLabor.CreatedBy)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_ServiceTemplateLabor.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_ServiceTemplateLabor.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_ServiceTemplateLabor.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_ServiceTemplateLabor.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_ServiceTemplateLabor.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_ServiceTemplateLabor.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_ServiceTemplateLabor.ReasonDescription)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VW_ServiceTemplateLabor

            Dim vW_ServiceTemplateLabor As VW_ServiceTemplateLabor = New VW_ServiceTemplateLabor

            vW_ServiceTemplateLabor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TemplateType")) Then vW_ServiceTemplateLabor.TemplateType = dr("TemplateType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then vW_ServiceTemplateLabor.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then vW_ServiceTemplateLabor.KindCode = dr("KindCode").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then vW_ServiceTemplateLabor.LinkID = dr("ID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                vW_ServiceTemplateLabor.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then vW_ServiceTemplateLabor.DealerID = dr("DealerID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindID")) Then vW_ServiceTemplateLabor.KindID = dr("KindID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then vW_ServiceTemplateLabor.VechileTypeID = dr("VechileTypeID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LaborDuration")) Then vW_ServiceTemplateLabor.LaborDuration = CType(dr("LaborDuration"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborCost")) Then vW_ServiceTemplateLabor.LaborCost = CType(dr("LaborCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then vW_ServiceTemplateLabor.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vW_ServiceTemplateLabor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vW_ServiceTemplateLabor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vW_ServiceTemplateLabor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then vW_ServiceTemplateLabor.LastUpdateBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then vW_ServiceTemplateLabor.LastUpdateTime = CType(dr("LastUpdatedTime"), DateTime)
            Return vW_ServiceTemplateLabor

        End Function

        Private Sub SetTableName()

            If Not (GetType(VW_ServiceTemplateLabor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VW_ServiceTemplateLabor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VW_ServiceTemplateLabor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


