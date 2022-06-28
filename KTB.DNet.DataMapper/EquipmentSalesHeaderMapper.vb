#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipmentSalesHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/5/2006 - 9:34:00 AM
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

    Public Class EquipmentSalesHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEquipmentSalesHeader"
        Private m_UpdateStatement As String = "up_UpdateEquipmentSalesHeader"
        Private m_RetrieveStatement As String = "up_RetrieveEquipmentSalesHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveEquipmentSalesHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEquipmentSalesHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim equipmentSalesHeader As EquipmentSalesHeader = Nothing
            While dr.Read

                equipmentSalesHeader = Me.CreateObject(dr)

            End While

            Return equipmentSalesHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim equipmentSalesHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim equipmentSalesHeader As EquipmentSalesHeader = Me.CreateObject(dr)
                equipmentSalesHeaderList.Add(equipmentSalesHeader)
            End While

            Return equipmentSalesHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipmentSalesHeader As EquipmentSalesHeader = CType(obj, EquipmentSalesHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipmentSalesHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipmentSalesHeader As EquipmentSalesHeader = CType(obj, EquipmentSalesHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RegPONumber", DbType.AnsiString, equipmentSalesHeader.RegPONumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, equipmentSalesHeader.Status)
            DbCommandWrapper.AddInParameter("@IsSOProcess", DbType.Int32, equipmentSalesHeader.IsSOProcess)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, equipmentSalesHeader.Kind)
            DbCommandWrapper.AddInParameter("@IsKTBView", DbType.Int32, equipmentSalesHeader.IsKTBView)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, equipmentSalesHeader.PONumber)
            DbCommandWrapper.AddInParameter("@ReqDeliveryDate", DbType.DateTime, equipmentSalesHeader.ReqDeliveryDate)
            DbCommandWrapper.AddInParameter("@DeteilRequirement", DbType.AnsiString, equipmentSalesHeader.DeteilRequirement)
            DbCommandWrapper.AddInParameter("@ApproveBy", DbType.AnsiString, equipmentSalesHeader.ApproveBy)
            DbCommandWrapper.AddInParameter("@ApproveDate", DbType.DateTime, equipmentSalesHeader.ApproveDate)
            DbCommandWrapper.AddInParameter("@EstimateDeliveryDate", DbType.DateTime, equipmentSalesHeader.EstimateDeliveryDate)
            DbCommandWrapper.AddInParameter("@ResponseBy", DbType.AnsiString, equipmentSalesHeader.ResponseBy)
            DbCommandWrapper.AddInParameter("@ResponseDate", DbType.DateTime, equipmentSalesHeader.ResponseDate)
            DbCommandWrapper.AddInParameter("@ResponseDetail", DbType.AnsiString, equipmentSalesHeader.ResponseDetail)
            DbCommandWrapper.AddInParameter("@Validate1By", DbType.AnsiString, equipmentSalesHeader.Validate1By)
            DbCommandWrapper.AddInParameter("@Validate1Date", DbType.DateTime, equipmentSalesHeader.Validate1Date)
            DbCommandWrapper.AddInParameter("@Validate2By", DbType.AnsiString, equipmentSalesHeader.Validate2By)
            DbCommandWrapper.AddInParameter("@Validate2Date", DbType.DateTime, equipmentSalesHeader.Validate2Date)
            DbCommandWrapper.AddInParameter("@ValidateDate", DbType.DateTime, equipmentSalesHeader.ValidateDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipmentSalesHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, equipmentSalesHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(equipmentSalesHeader.Dealer))

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

            Dim equipmentSalesHeader As EquipmentSalesHeader = CType(obj, EquipmentSalesHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipmentSalesHeader.ID)
            DbCommandWrapper.AddInParameter("@RegPONumber", DbType.AnsiString, equipmentSalesHeader.RegPONumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, equipmentSalesHeader.Status)
            DbCommandWrapper.AddInParameter("@IsSOProcess", DbType.Int32, equipmentSalesHeader.IsSOProcess)
            DbCommandWrapper.AddInParameter("@Kind", DbType.Int32, equipmentSalesHeader.Kind)
            DbCommandWrapper.AddInParameter("@IsKTBView", DbType.Int32, equipmentSalesHeader.IsKTBView)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, equipmentSalesHeader.PONumber)
            DbCommandWrapper.AddInParameter("@ReqDeliveryDate", DbType.DateTime, equipmentSalesHeader.ReqDeliveryDate)
            DbCommandWrapper.AddInParameter("@DeteilRequirement", DbType.AnsiString, equipmentSalesHeader.DeteilRequirement)
            DbCommandWrapper.AddInParameter("@ApproveBy", DbType.AnsiString, equipmentSalesHeader.ApproveBy)
            DbCommandWrapper.AddInParameter("@ApproveDate", DbType.DateTime, equipmentSalesHeader.ApproveDate)
            DbCommandWrapper.AddInParameter("@EstimateDeliveryDate", DbType.DateTime, equipmentSalesHeader.EstimateDeliveryDate)
            DbCommandWrapper.AddInParameter("@ResponseBy", DbType.AnsiString, equipmentSalesHeader.ResponseBy)
            DbCommandWrapper.AddInParameter("@ResponseDate", DbType.DateTime, equipmentSalesHeader.ResponseDate)
            DbCommandWrapper.AddInParameter("@ResponseDetail", DbType.AnsiString, equipmentSalesHeader.ResponseDetail)
            DbCommandWrapper.AddInParameter("@Validate1By", DbType.AnsiString, equipmentSalesHeader.Validate1By)
            DbCommandWrapper.AddInParameter("@Validate1Date", DbType.DateTime, equipmentSalesHeader.Validate1Date)
            DbCommandWrapper.AddInParameter("@Validate2By", DbType.AnsiString, equipmentSalesHeader.Validate2By)
            DbCommandWrapper.AddInParameter("@Validate2Date", DbType.DateTime, equipmentSalesHeader.Validate2Date)
            DbCommandWrapper.AddInParameter("@ValidateDate", DbType.DateTime, equipmentSalesHeader.ValidateDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipmentSalesHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, equipmentSalesHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(equipmentSalesHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EquipmentSalesHeader

            Dim equipmentSalesHeader As EquipmentSalesHeader = New EquipmentSalesHeader

            equipmentSalesHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegPONumber")) Then equipmentSalesHeader.RegPONumber = dr("RegPONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then equipmentSalesHeader.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSOProcess")) Then equipmentSalesHeader.IsSOProcess = CType(dr("IsSOProcess"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Kind")) Then equipmentSalesHeader.Kind = CType(dr("Kind"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsKTBView")) Then equipmentSalesHeader.IsKTBView = CType(dr("IsKTBView"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then equipmentSalesHeader.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqDeliveryDate")) Then equipmentSalesHeader.ReqDeliveryDate = CType(dr("ReqDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DeteilRequirement")) Then equipmentSalesHeader.DeteilRequirement = dr("DeteilRequirement").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ApproveBy")) Then equipmentSalesHeader.ApproveBy = dr("ApproveBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ApproveDate")) Then equipmentSalesHeader.ApproveDate = CType(dr("ApproveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EstimateDeliveryDate")) Then equipmentSalesHeader.EstimateDeliveryDate = CType(dr("EstimateDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseBy")) Then equipmentSalesHeader.ResponseBy = dr("ResponseBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseDate")) Then equipmentSalesHeader.ResponseDate = CType(dr("ResponseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseDetail")) Then equipmentSalesHeader.ResponseDetail = dr("ResponseDetail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Validate1By")) Then equipmentSalesHeader.Validate1By = dr("Validate1By").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Validate1Date")) Then equipmentSalesHeader.Validate1Date = CType(dr("Validate1Date"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Validate2By")) Then equipmentSalesHeader.Validate2By = dr("Validate2By").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Validate2Date")) Then equipmentSalesHeader.Validate2Date = CType(dr("Validate2Date"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDate")) Then equipmentSalesHeader.ValidateDate = CType(dr("ValidateDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then equipmentSalesHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then equipmentSalesHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then equipmentSalesHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then equipmentSalesHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then equipmentSalesHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                equipmentSalesHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return equipmentSalesHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(EquipmentSalesHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EquipmentSalesHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EquipmentSalesHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace