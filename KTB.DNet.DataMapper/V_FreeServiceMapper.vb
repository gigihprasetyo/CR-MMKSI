
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_FreeService Objects Mapper.
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

    Public Class V_FreeServiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_FreeService"
        Private m_UpdateStatement As String = "up_UpdateV_FreeService"
        Private m_RetrieveStatement As String = "up_RetrieveV_FreeService"
        Private m_RetrieveListStatement As String = "up_RetrieveV_FreeServiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_FreeService"

#End Region
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_FreeService As V_FreeService = Nothing
            While dr.Read

                v_FreeService = Me.CreateObject(dr)

            End While

            Return v_FreeService

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_FreeServiceList As ArrayList = New ArrayList

            While dr.Read
                Dim v_FreeService As V_FreeService = Me.CreateObject(dr)
                v_FreeServiceList.Add(v_FreeService)
            End While

            Return v_FreeServiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_FreeService As V_FreeService = CType(obj, V_FreeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_FreeService.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_FreeService As V_FreeService = CType(obj, V_FreeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_FreeService.Status)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_FreeService.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, v_FreeService.FSKindID)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, v_FreeService.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, v_FreeService.ServiceDate)
            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, v_FreeService.ServiceDealerID)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, v_FreeService.SoldDate)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, v_FreeService.NotificationNumber)
            DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, v_FreeService.NotificationType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, v_FreeService.TotalAmount)
            DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, v_FreeService.LabourAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, v_FreeService.PartAmount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, v_FreeService.PPNAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, v_FreeService.PPHAmount)
            DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, v_FreeService.Reject)
            DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, v_FreeService.Reason)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, v_FreeService.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, v_FreeService.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, v_FreeService.VisitType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, v_FreeService.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@CashBack", DbType.Currency, v_FreeService.CashBack)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_FreeService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_FreeService.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_FreeService.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_FreeService.ChassisNumber)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, v_FreeService.KindCode)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_FreeService.CategoryID)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_FreeService.CategoryCode)
            DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, v_FreeService.ReasonCode)
            DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, v_FreeService.ReasonDescription)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, v_FreeService.FileName)
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, v_FreeService.FilePath)


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

            Dim v_FreeService As V_FreeService = CType(obj, V_FreeService)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_FreeService.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_FreeService.Status)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_FreeService.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, v_FreeService.FSKindID)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, v_FreeService.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, v_FreeService.ServiceDate)
            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, v_FreeService.ServiceDealerID)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, v_FreeService.SoldDate)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, v_FreeService.NotificationNumber)
            DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, v_FreeService.NotificationType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, v_FreeService.TotalAmount)
            DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, v_FreeService.LabourAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, v_FreeService.PartAmount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, v_FreeService.PPNAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, v_FreeService.PPHAmount)
            DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, v_FreeService.Reject)
            DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, v_FreeService.Reason)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, v_FreeService.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, v_FreeService.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, v_FreeService.VisitType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, v_FreeService.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@CashBack", DbType.Currency, v_FreeService.CashBack)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_FreeService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_FreeService.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_FreeService.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_FreeService.ChassisNumber)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, v_FreeService.KindCode)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_FreeService.CategoryID)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_FreeService.CategoryCode)
            DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, v_FreeService.ReasonCode)
            DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, v_FreeService.ReasonDescription)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, v_FreeService.FileName)
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, v_FreeService.FilePath)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_FreeService

            Dim v_FreeService As V_FreeService = New V_FreeService

            v_FreeService.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_FreeService.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then v_FreeService.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then v_FreeService.FSKindID = CType(dr("FSKindID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("MileAge")) Then v_FreeService.MileAge = CType(dr("MileAge"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then v_FreeService.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDealerID")) Then v_FreeService.ServiceDealerID = CType(dr("ServiceDealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDate")) Then v_FreeService.SoldDate = CType(dr("SoldDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationNumber")) Then v_FreeService.NotificationNumber = CType(dr("NotificationNumber"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationType")) Then v_FreeService.NotificationType = dr("NotificationType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then v_FreeService.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LabourAmount")) Then v_FreeService.LabourAmount = CType(dr("LabourAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then v_FreeService.PartAmount = CType(dr("PartAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPNAmount")) Then v_FreeService.PPNAmount = CType(dr("PPNAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHAmount")) Then v_FreeService.PPHAmount = CType(dr("PPHAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Reject")) Then v_FreeService.Reject = dr("Reject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Reason")) Then v_FreeService.Reason = CType(dr("Reason"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseBy")) Then v_FreeService.ReleaseBy = dr("ReleaseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then v_FreeService.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VisitType")) Then v_FreeService.VisitType = dr("VisitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then v_FreeService.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CashBack")) Then v_FreeService.CashBack = CType(dr("CashBack"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_FreeService.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_FreeService.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_FreeService.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_FreeService.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_FreeService.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_FreeService.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then v_FreeService.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then v_FreeService.KindCode = dr("KindCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then v_FreeService.CategoryID = CType(dr("CategoryID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then v_FreeService.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReasonCode")) Then v_FreeService.ReasonCode = dr("ReasonCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReasonDescription")) Then v_FreeService.ReasonDescription = dr("ReasonDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then v_FreeService.DealerBranchCode = dr("DealerBranchCode").ToString


            If Not dr.IsDBNull(dr.GetOrdinal("NoRegRequest")) Then v_FreeService.NoRegRequest = dr("NoRegRequest").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then v_FreeService.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FilePath")) Then v_FreeService.FilePath = dr("FilePath").ToString

            Return v_FreeService

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_FreeService) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_FreeService), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_FreeService).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

