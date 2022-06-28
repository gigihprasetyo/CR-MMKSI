#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FreeServiceBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:06:44 PM
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

    Public Class FreeServiceBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFreeServiceBB"
        Private m_UpdateStatement As String = "up_UpdateFreeServiceBB"
        Private m_RetrieveStatement As String = "up_RetrieveFreeServiceBB"
        Private m_RetrieveListStatement As String = "up_RetrieveFreeServiceBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFreeServiceBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim FreeServiceBB As FreeServiceBB = Nothing
            While dr.Read

                FreeServiceBB = Me.CreateObject(dr)

            End While

            Return FreeServiceBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim FreeServiceBBList As ArrayList = New ArrayList

            While dr.Read
                Dim FreeServiceBB As FreeServiceBB = Me.CreateObject(dr)
                FreeServiceBBList.Add(FreeServiceBB)
            End While

            Return FreeServiceBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FreeServiceBB As FreeServiceBB = CType(obj, FreeServiceBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FreeServiceBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FreeServiceBB As FreeServiceBB = CType(obj, FreeServiceBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, FreeServiceBB.Status)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, FreeServiceBB.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, FreeServiceBB.ServiceDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, FreeServiceBB.SoldDate)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, FreeServiceBB.NotificationNumber)
            DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, FreeServiceBB.NotificationType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, FreeServiceBB.TotalAmount)
            DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, FreeServiceBB.LabourAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, FreeServiceBB.PartAmount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, FreeServiceBB.PPNAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, FreeServiceBB.PPHAmount)
            DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, FreeServiceBB.Reject)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, FreeServiceBB.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, FreeServiceBB.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, FreeServiceBB.VisitType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, FreeServiceBB.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FreeServiceBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, FreeServiceBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@Reason", DbType.Int32, Me.GetRefObject(FreeServiceBB.Reason))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(FreeServiceBB.ChassisMasterBB))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, Me.GetRefObject(FreeServiceBB.FSKind))
            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int32, Me.GetRefObject(FreeServiceBB.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(FreeServiceBB.DealerBranch))

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

            Dim FreeServiceBB As FreeServiceBB = CType(obj, FreeServiceBB)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FreeServiceBB.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, FreeServiceBB.Status)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, FreeServiceBB.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, FreeServiceBB.ServiceDate)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, FreeServiceBB.SoldDate)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, FreeServiceBB.NotificationNumber)
            DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, FreeServiceBB.NotificationType)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, FreeServiceBB.TotalAmount)
            DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, FreeServiceBB.LabourAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, FreeServiceBB.PartAmount)
            DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, FreeServiceBB.PPNAmount)
            DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, FreeServiceBB.PPHAmount)
            DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, FreeServiceBB.Reject)
            DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, FreeServiceBB.ReleaseBy)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, FreeServiceBB.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, FreeServiceBB.VisitType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, FreeServiceBB.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FreeServiceBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, FreeServiceBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@Reason", DbType.Int32, Me.GetRefObject(FreeServiceBB.Reason))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(FreeServiceBB.ChassisMasterBB))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, Me.GetRefObject(FreeServiceBB.FSKind))
            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int32, Me.GetRefObject(FreeServiceBB.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(FreeServiceBB.DealerBranch))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FreeServiceBB

            Dim FreeServiceBB As FreeServiceBB = New FreeServiceBB

            FreeServiceBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then FreeServiceBB.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MileAge")) Then FreeServiceBB.MileAge = CType(dr("MileAge"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then FreeServiceBB.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDate")) Then FreeServiceBB.SoldDate = CType(dr("SoldDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationNumber")) Then FreeServiceBB.NotificationNumber = dr("NotificationNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationType")) Then FreeServiceBB.NotificationType = dr("NotificationType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then FreeServiceBB.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LabourAmount")) Then FreeServiceBB.LabourAmount = CType(dr("LabourAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then FreeServiceBB.PartAmount = CType(dr("PartAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPNAmount")) Then FreeServiceBB.PPNAmount = CType(dr("PPNAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHAmount")) Then FreeServiceBB.PPHAmount = CType(dr("PPHAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Reject")) Then FreeServiceBB.Reject = dr("Reject").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseBy")) Then FreeServiceBB.ReleaseBy = dr("ReleaseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then FreeServiceBB.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VisitType")) Then FreeServiceBB.VisitType = dr("VisitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then FreeServiceBB.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then FreeServiceBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then FreeServiceBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then FreeServiceBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then FreeServiceBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then FreeServiceBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Reason")) Then
                FreeServiceBB.Reason = New Reason(CType(dr("Reason"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                FreeServiceBB.ChassisMasterBB = New ChassisMasterBB(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then
                FreeServiceBB.FSKind = New FSKind(CType(dr("FSKindID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDealerID")) Then
                FreeServiceBB.Dealer = New Dealer(CType(dr("ServiceDealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                FreeServiceBB.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            Return FreeServiceBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(FreeServiceBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FreeServiceBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FreeServiceBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

