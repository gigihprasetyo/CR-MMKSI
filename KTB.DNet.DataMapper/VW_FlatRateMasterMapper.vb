
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VW_FlatRateMaster Objects Mapper.
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

    Public Class VW_FlatRateMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVW_FlatRateMaster"
        Private m_UpdateStatement As String = "up_UpdateVW_FlatRateMaster"
        Private m_RetrieveStatement As String = "up_RetrieveVW_FlatRateMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveVW_FlatRateMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVW_FlatRateMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vW_FlatRateMaster As VW_FlatRateMaster = Nothing
            While dr.Read

                vW_FlatRateMaster = Me.CreateObject(dr)

            End While

            Return vW_FlatRateMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vW_FlatRateMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim vW_FlatRateMaster As VW_FlatRateMaster = Me.CreateObject(dr)
                vW_FlatRateMasterList.Add(vW_FlatRateMaster)
            End While

            Return vW_FlatRateMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vW_FlatRateMaster As VW_FlatRateMaster = CType(obj, VW_FlatRateMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_FlatRateMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            '    Dim vW_FlatRateMaster As VW_FlatRateMaster = CType(obj, VW_FlatRateMaster)
            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            '    DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_FlatRateMaster.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_FlatRateMaster.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_FlatRateMaster.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_FlatRateMaster.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_FlatRateMaster.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_FlatRateMaster.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_FlatRateMaster.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_FlatRateMaster.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_FlatRateMaster.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_FlatRateMaster.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_FlatRateMaster.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_FlatRateMaster.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_FlatRateMaster.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_FlatRateMaster.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_FlatRateMaster.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_FlatRateMaster.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_FlatRateMaster.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_FlatRateMaster.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_FlatRateMaster.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_FlatRateMaster.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_FlatRateMaster.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vW_FlatRateMaster.LastUpdateBy)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_FlatRateMaster.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_FlatRateMaster.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_FlatRateMaster.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_FlatRateMaster.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_FlatRateMaster.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_FlatRateMaster.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_FlatRateMaster.ReasonDescription)


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

            '    Dim vW_FlatRateMaster As VW_FlatRateMaster = CType(obj, VW_FlatRateMaster)

            '    DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            '    DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vW_FlatRateMaster.ID)
            '    DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vW_FlatRateMaster.Status)
            '    DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, vW_FlatRateMaster.ChassisMasterID)
            '    DbCommandWrapper.AddInParameter("@FSKindID", DbType.Byte, vW_FlatRateMaster.FSKindID)
            '    DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, vW_FlatRateMaster.MileAge)
            '    DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vW_FlatRateMaster.ServiceDate)
            '    DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, vW_FlatRateMaster.ServiceDealerID)
            '    DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, vW_FlatRateMaster.SoldDate)
            '    DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, vW_FlatRateMaster.NotificationNumber)
            '    DbCommandWrapper.AddInParameter("@NotificationType", DbType.AnsiString, vW_FlatRateMaster.NotificationType)
            '    DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, vW_FlatRateMaster.TotalAmount)
            '    DbCommandWrapper.AddInParameter("@LabourAmount", DbType.Currency, vW_FlatRateMaster.LabourAmount)
            '    DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, vW_FlatRateMaster.PartAmount)
            '    DbCommandWrapper.AddInParameter("@PPNAmount", DbType.Currency, vW_FlatRateMaster.PPNAmount)
            '    DbCommandWrapper.AddInParameter("@PPHAmount", DbType.Currency, vW_FlatRateMaster.PPHAmount)
            '    DbCommandWrapper.AddInParameter("@Reject", DbType.AnsiString, vW_FlatRateMaster.Reject)
            '    DbCommandWrapper.AddInParameter("@Reason", DbType.Int16, vW_FlatRateMaster.Reason)
            '    DbCommandWrapper.AddInParameter("@ReleaseBy", DbType.AnsiString, vW_FlatRateMaster.ReleaseBy)
            '    DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, vW_FlatRateMaster.ReleaseDate)
            '    DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, vW_FlatRateMaster.VisitType)
            '    DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vW_FlatRateMaster.WorkOrderNumber)
            '    DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vW_FlatRateMaster.RowStatus)
            '    DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vW_FlatRateMaster.CreatedBy)
            '    'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            '    'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            '    DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vW_FlatRateMaster.DealerCode)
            '    DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vW_FlatRateMaster.ChassisNumber)
            '    DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, vW_FlatRateMaster.KindCode)
            '    DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, vW_FlatRateMaster.CategoryID)
            '    DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vW_FlatRateMaster.CategoryCode)
            '    DbCommandWrapper.AddInParameter("@ReasonCode", DbType.AnsiString, vW_FlatRateMaster.ReasonCode)
            '    DbCommandWrapper.AddInParameter("@ReasonDescription", DbType.AnsiString, vW_FlatRateMaster.ReasonDescription)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VW_FlatRateMaster

            Dim vW_FlatRateMaster As VW_FlatRateMaster = New VW_FlatRateMaster

            vW_FlatRateMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vW_FlatRateMaster.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then vW_FlatRateMaster.Type = dr("Type").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then vW_FlatRateMaster.KindCode = dr("KindCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindDescription")) Then vW_FlatRateMaster.KindDescription = dr("KindDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Varian")) Then vW_FlatRateMaster.Varian = dr("Varian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LinkID")) Then vW_FlatRateMaster.LinkID = dr("LinkID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FlatRate")) Then vW_FlatRateMaster.FlatRate = CType(dr("FlatRate"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vW_FlatRateMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vW_FlatRateMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vW_FlatRateMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then vW_FlatRateMaster.LastUpdateBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then vW_FlatRateMaster.LastUpdateTime = CType(dr("LastUpdatedTime"), DateTime)
            Return vW_FlatRateMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(VW_FlatRateMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VW_FlatRateMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VW_FlatRateMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


