
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FreeService Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/4/2016 - 10:22:42 AM
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

    Public Class FreeServiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++
#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFreeService"
        Private m_UpdateStatement As String = "up_UpdateFreeService"
        Private m_RetrieveStatement As String = "up_RetrieveFreeService"
        Private m_RetrieveListStatement As String = "up_RetrieveFreeServiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFreeService"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim freeService As FreeService = Nothing
            While dr.Read

                freeService = Me.CreateObject(dr)

            End While

            Return freeService

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim freeServiceList As ArrayList = New ArrayList

            While dr.Read
                Dim freeService As FreeService = Me.CreateObject(dr)
                freeServiceList.Add(freeService)
            End While

            Return freeServiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim freeService As FreeService = CType(obj, FreeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, freeService.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim freeService As FreeService = CType(obj, FreeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, freeService.Status)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, freeService.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, freeService.ServiceDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, freeService.SoldDate)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, freeService.NotificationNumber)
            DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, freeService.NotificationType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, freeService.TotalAmount)
            DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, freeService.LabourAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, freeService.PartAmount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, freeService.PPNAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, freeService.PPHAmount)
            DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, freeService.Reject)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, freeService.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, freeService.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, freeService.VisitType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, freeService.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@CashBack", DbType.Currency, freeService.CashBack)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, freeService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, freeService.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, Me.GetRefObject(freeService.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(freeService.DealerBranch))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(freeService.ChassisMaster))
            DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, Me.GetRefObject(freeService.Reason))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, Me.GetRefObject(freeService.FSKind))
            DbCommandWrapper.AddInParameter("@FleetRequestID", DbType.Int32, Me.GetRefObject(freeService.FleetRequest))
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, freeService.FileName)
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, freeService.FilePath)

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

            Dim freeService As FreeService = CType(obj, FreeService)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, freeService.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, freeService.Status)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, freeService.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, freeService.ServiceDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, freeService.SoldDate)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, freeService.NotificationNumber)
            DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, freeService.NotificationType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, freeService.TotalAmount)
            DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, freeService.LabourAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, freeService.PartAmount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, freeService.PPNAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, freeService.PPHAmount)
            DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, freeService.Reject)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, freeService.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, freeService.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, freeService.VisitType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, freeService.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@CashBack", DbType.Currency, freeService.CashBack)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, freeService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, freeService.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, Me.GetRefObject(freeService.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(freeService.DealerBranch))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(freeService.ChassisMaster))
            DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, Me.GetRefObject(freeService.Reason))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, Me.GetRefObject(freeService.FSKind))
            DbCommandWrapper.AddInParameter("@FleetRequestID", DbType.Int32, Me.GetRefObject(freeService.FleetRequest))
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, freeService.FileName)
            DbCommandWrapper.AddInParameter("@FilePath", DbType.AnsiString, freeService.FilePath)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FreeService

            Dim freeService As FreeService = New FreeService

            freeService.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then freeService.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MileAge")) Then freeService.MileAge = CType(dr("MileAge"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then freeService.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDate")) Then freeService.SoldDate = CType(dr("SoldDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationNumber")) Then freeService.NotificationNumber = CType(dr("NotificationNumber"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationType")) Then freeService.NotificationType = dr("NotificationType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then freeService.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LabourAmount")) Then freeService.LabourAmount = CType(dr("LabourAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then freeService.PartAmount = CType(dr("PartAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPNAmount")) Then freeService.PPNAmount = CType(dr("PPNAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHAmount")) Then freeService.PPHAmount = CType(dr("PPHAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Reject")) Then freeService.Reject = dr("Reject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseBy")) Then freeService.ReleaseBy = dr("ReleaseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then freeService.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VisitType")) Then freeService.VisitType = dr("VisitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then freeService.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CashBack")) Then freeService.CashBack = CType(dr("CashBack"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then freeService.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then freeService.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then freeService.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then freeService.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then freeService.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDealerID")) Then
                freeService.Dealer = New Dealer(CType(dr("ServiceDealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                freeService.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                freeService.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("Reason")) Then
                freeService.Reason = New Reason(CType(dr("Reason"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then
                freeService.FSKind = New FSKind(CType(dr("FSKindID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FleetRequestID")) Then
                freeService.FleetRequest = New FleetRequest(CType(dr("FleetRequestID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then freeService.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FilePath")) Then freeService.FilePath = dr("FilePath").ToString

            Return freeService

        End Function

        Private Sub SetTableName()

            If Not (GetType(FreeService) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FreeService), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FreeService).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

