
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 6/8/2012 - 11:38:19 AM
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

    Public Class SPKDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKDetail"
        Private m_UpdateStatement As String = "up_UpdateSPKDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSPKDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPKDetail As SPKDetail = Nothing
            While dr.Read

                sPKDetail = Me.CreateObject(dr)

            End While

            Return sPKDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPKDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sPKDetail As SPKDetail = Me.CreateObject(dr)
                sPKDetailList.Add(sPKDetail)
            End While

            Return sPKDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKDetail As SPKDetail = CType(obj, SPKDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKDetail As SPKDetail = CType(obj, SPKDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int16, sPKDetail.LineItem)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, sPKDetail.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, sPKDetail.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, sPKDetail.VehicleColorName)
            DbCommandWrapper.AddInParameter("@Additional", DbType.Byte, sPKDetail.Additional)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.String, sPKDetail.Remarks)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPKDetail.Quantity)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, sPKDetail.Amount)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sPKDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.String, sPKDetail.RejectedReason)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, sPKDetail.Status)
            DbCommandWrapper.AddInParameter("@EventType", DbType.Int32, sPKDetail.EventType)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.AnsiString, sPKDetail.CampaignName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKDetail.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, sPKDetail.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(sPKDetail.Category))
            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, Me.GetRefObject(sPKDetail.SPKHeader))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, Me.GetRefObject(sPKDetail.VechileColor))
            DbCommandWrapper.AddInParameter("@ProfileDetailID", DbType.Int32, Me.GetRefObject(sPKDetail.ProfileDetail))
            DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, Me.GetRefObject(sPKDetail.VehicleKind))

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

            Dim sPKDetail As SPKDetail = CType(obj, SPKDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKDetail.ID)
            DbCommandWrapper.AddInParameter("@LineItem", DbType.Int16, sPKDetail.LineItem)
            DbCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, sPKDetail.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@VehicleColorCode", DbType.AnsiString, sPKDetail.VehicleColorCode)
            DbCommandWrapper.AddInParameter("@VehicleColorName", DbType.AnsiString, sPKDetail.VehicleColorName)
            DbCommandWrapper.AddInParameter("@Additional", DbType.Byte, sPKDetail.Additional)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.String, sPKDetail.Remarks)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, sPKDetail.Quantity)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, sPKDetail.Amount)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, sPKDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.String, sPKDetail.RejectedReason)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, sPKDetail.Status)
            DbCommandWrapper.AddInParameter("@EventType", DbType.Int32, sPKDetail.EventType)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.AnsiString, sPKDetail.CampaignName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKDetail.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, sPKDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(sPKDetail.Category))
            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, Me.GetRefObject(sPKDetail.SPKHeader))
            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, Me.GetRefObject(sPKDetail.VechileColor))
            DbCommandWrapper.AddInParameter("@ProfileDetailID", DbType.Int32, Me.GetRefObject(sPKDetail.ProfileDetail))
            DbCommandWrapper.AddInParameter("@VehicleKindID", DbType.Int32, Me.GetRefObject(sPKDetail.VehicleKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKDetail

            Dim sPKDetail As SPKDetail = New SPKDetail

            sPKDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LineItem")) Then sPKDetail.LineItem = CType(dr("LineItem"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then sPKDetail.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorCode")) Then sPKDetail.VehicleColorCode = dr("VehicleColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorName")) Then sPKDetail.VehicleColorName = dr("VehicleColorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Additional")) Then sPKDetail.Additional = CType(dr("Additional"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then sPKDetail.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then sPKDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then sPKDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then sPKDetail.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReason")) Then sPKDetail.RejectedReason = dr("RejectedReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPKDetail.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("EventType")) Then sPKDetail.EventType = CType(dr("EventType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignName")) Then sPKDetail.CampaignName = dr("CampaignName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPKDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPKDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                sPKDetail.Category = New Category(CType(dr("CategoryID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKHeaderID")) Then
                sPKDetail.SPKHeader = New SPKHeader(CType(dr("SPKHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                sPKDetail.VechileColor = New VechileColor(CType(dr("VehicleColorID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileDetailID")) Then
                sPKDetail.ProfileDetail = New ProfileDetail(CType(dr("ProfileDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindID")) Then
                sPKDetail.VehicleKind = New VehicleKind(CType(dr("VehicleKindID"), Integer))
            End If

            Return sPKDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

