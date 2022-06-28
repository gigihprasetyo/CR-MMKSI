#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : ServiceTemplateGRPartDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 12/1/2021 - 5:44:22 PM
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

    Public Class ServiceTemplateGRPartDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplateGRPartDetail"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplateGRPartDetail"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplateGRPartDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplateGRPartDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplateGRPartDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim serviceTemplateGRPartDetail As ServiceTemplateGRPartDetail = Nothing
            While dr.Read

                serviceTemplateGRPartDetail = Me.CreateObject(dr)

            End While

            Return serviceTemplateGRPartDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim serviceTemplateGRPartDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim serviceTemplateGRPartDetail As ServiceTemplateGRPartDetail = Me.CreateObject(dr)
                serviceTemplateGRPartDetailList.Add(serviceTemplateGRPartDetail)
            End While

            Return serviceTemplateGRPartDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceTemplateGRPartDetail As ServiceTemplateGRPartDetail = CType(obj, ServiceTemplateGRPartDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceTemplateGRPartDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceTemplateGRPartDetail As ServiceTemplateGRPartDetail = CType(obj, ServiceTemplateGRPartDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ServiceTemplateGRLaborID", DbType.Int32, serviceTemplateGRPartDetail.ServiceTemplateGRLaborID)
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, serviceTemplateGRPartDetail.SparePartMasterID)
            DbCommandWrapper.AddInParameter("@PartQuantity", DbType.Decimal, serviceTemplateGRPartDetail.PartQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceTemplateGRPartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, serviceTemplateGRPartDetail.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)


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

            Dim serviceTemplateGRPartDetail As ServiceTemplateGRPartDetail = CType(obj, ServiceTemplateGRPartDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceTemplateGRPartDetail.ID)
            DbCommandWrapper.AddInParameter("@ServiceTemplateGRLaborID", DbType.Int32, serviceTemplateGRPartDetail.ServiceTemplateGRLaborID)
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, serviceTemplateGRPartDetail.SparePartMasterID)
            DbCommandWrapper.AddInParameter("@PartQuantity", DbType.Decimal, serviceTemplateGRPartDetail.PartQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceTemplateGRPartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, serviceTemplateGRPartDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplateGRPartDetail

            Dim serviceTemplateGRPartDetail As ServiceTemplateGRPartDetail = New ServiceTemplateGRPartDetail

            serviceTemplateGRPartDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTemplateGRLaborID")) Then serviceTemplateGRPartDetail.ServiceTemplateGRLaborID = CType(dr("ServiceTemplateGRLaborID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then serviceTemplateGRPartDetail.SparePartMasterID = CType(dr("SparePartMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartQuantity")) Then serviceTemplateGRPartDetail.PartQuantity = CType(dr("PartQuantity"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then serviceTemplateGRPartDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then serviceTemplateGRPartDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then serviceTemplateGRPartDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then serviceTemplateGRPartDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then serviceTemplateGRPartDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            Return serviceTemplateGRPartDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplateGRPartDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplateGRPartDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplateGRPartDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
