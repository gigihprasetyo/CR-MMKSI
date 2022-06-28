#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : ServiceTemplateGRLabor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 12/1/2021 - 5:42:18 PM
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

    Public Class ServiceTemplateGRLaborMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplateGRLabor"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplateGRLabor"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplateGRLabor"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplateGRLaborList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplateGRLabor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim serviceTemplateGRLabor As ServiceTemplateGRLabor = Nothing
            While dr.Read

                serviceTemplateGRLabor = Me.CreateObject(dr)

            End While

            Return serviceTemplateGRLabor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim serviceTemplateGRLaborList As ArrayList = New ArrayList

            While dr.Read
                Dim serviceTemplateGRLabor As ServiceTemplateGRLabor = Me.CreateObject(dr)
                serviceTemplateGRLaborList.Add(serviceTemplateGRLabor)
            End While

            Return serviceTemplateGRLaborList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceTemplateGRLabor As ServiceTemplateGRLabor = CType(obj, ServiceTemplateGRLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceTemplateGRLabor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim serviceTemplateGRLabor As ServiceTemplateGRLabor = CType(obj, ServiceTemplateGRLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@GRKindID", DbType.Int32, serviceTemplateGRLabor.GRKindID)
            DbCommandWrapper.AddInParameter("@Variant", DbType.String, serviceTemplateGRLabor.Variants)
            DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, serviceTemplateGRLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Currency, serviceTemplateGRLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, serviceTemplateGRLabor.ValidFrom)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceTemplateGRLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, serviceTemplateGRLabor.LastUpdatedBy)
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

            Dim serviceTemplateGRLabor As ServiceTemplateGRLabor = CType(obj, ServiceTemplateGRLabor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, serviceTemplateGRLabor.ID)
            DbCommandWrapper.AddInParameter("@GRKindID", DbType.Int32, serviceTemplateGRLabor.GRKindID)
            DbCommandWrapper.AddInParameter("@Variant", DbType.String, serviceTemplateGRLabor.Variants)
            DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, serviceTemplateGRLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Currency, serviceTemplateGRLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, serviceTemplateGRLabor.ValidFrom)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, serviceTemplateGRLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, serviceTemplateGRLabor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplateGRLabor

            Dim serviceTemplateGRLabor As ServiceTemplateGRLabor = New ServiceTemplateGRLabor

            serviceTemplateGRLabor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GRKindID")) Then serviceTemplateGRLabor.GRKindID = CType(dr("GRKindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Variant")) Then serviceTemplateGRLabor.Variants = CType(dr("Variant"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborDuration")) Then serviceTemplateGRLabor.LaborDuration = CType(dr("LaborDuration"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborCost")) Then serviceTemplateGRLabor.LaborCost = CType(dr("LaborCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then serviceTemplateGRLabor.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then serviceTemplateGRLabor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then serviceTemplateGRLabor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then serviceTemplateGRLabor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then serviceTemplateGRLabor.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then serviceTemplateGRLabor.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            Return serviceTemplateGRLabor

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplateGRLabor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplateGRLabor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplateGRLabor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
