
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOStatus Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/29/2015 - 1:42:21 PM
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

    Public Class SparePartPOStatusMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPOStatus"
        Private m_UpdateStatement As String = "up_UpdateSparePartPOStatus"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPOStatus"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPOStatusList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPOStatus"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPOStatus As SparePartPOStatus = Nothing
            While dr.Read

                sparePartPOStatus = Me.CreateObject(dr)

            End While

            Return sparePartPOStatus

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPOStatusList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPOStatus As SparePartPOStatus = Me.CreateObject(dr)
                sparePartPOStatusList.Add(sparePartPOStatus)
            End While

            Return sparePartPOStatusList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOStatus As SparePartPOStatus = CType(obj, SparePartPOStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOStatus.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOStatus As SparePartPOStatus = CType(obj, SparePartPOStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartPOStatus.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, sparePartPOStatus.SODate)
            DbCommandWrapper.AddInParameter("@PackingStatus", DbType.AnsiString, sparePartPOStatus.PackingStatus)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartPOStatus.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, sparePartPOStatus.BillingDate)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sparePartPOStatus.DeliveryDate)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, sparePartPOStatus.DocumentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOStatus.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPOStatus.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartPOStatus.SparePartPO))

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

            Dim sparePartPOStatus As SparePartPOStatus = CType(obj, SparePartPOStatus)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOStatus.ID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartPOStatus.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, sparePartPOStatus.SODate)
            DbCommandWrapper.AddInParameter("@PackingStatus", DbType.AnsiString, sparePartPOStatus.PackingStatus)
            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, sparePartPOStatus.BillingNumber)
            DbCommandWrapper.AddInParameter("@BillingDate", DbType.DateTime, sparePartPOStatus.BillingDate)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sparePartPOStatus.DeliveryDate)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, sparePartPOStatus.DocumentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOStatus.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPOStatus.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartPOStatus.SparePartPO))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPOStatus

            Dim sparePartPOStatus As SparePartPOStatus = New SparePartPOStatus

            sparePartPOStatus.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then sparePartPOStatus.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then sparePartPOStatus.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PackingStatus")) Then sparePartPOStatus.PackingStatus = dr("PackingStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then sparePartPOStatus.BillingNumber = dr("BillingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingDate")) Then sparePartPOStatus.BillingDate = CType(dr("BillingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryDate")) Then sparePartPOStatus.DeliveryDate = CType(dr("DeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then sparePartPOStatus.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPOStatus.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPOStatus.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPOStatus.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPOStatus.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPOStatus.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOID")) Then
                sparePartPOStatus.SparePartPO = New SparePartPO(CType(dr("SparePartPOID"), Integer))
            End If

            Return sparePartPOStatus

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPOStatus) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPOStatus), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPOStatus).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

