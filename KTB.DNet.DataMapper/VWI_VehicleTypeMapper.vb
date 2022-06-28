#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_VehicleType Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 24/09/2007 - 15:19:02
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

    Public Class VWI_VehicleTypeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_VehicleType"
        Private m_UpdateStatement As String = "up_UpdateVWI_VehicleType"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_VehicleType"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_VehicleTypeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_VehicleType"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_VehicleType As VWI_VehicleType = Nothing
            While dr.Read

                VWI_VehicleType = Me.CreateObject(dr)

            End While

            Return VWI_VehicleType

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_VehicleTypeList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_VehicleType As VWI_VehicleType = Me.CreateObject(dr)
                VWI_VehicleTypeList.Add(VWI_VehicleType)
            End While

            Return VWI_VehicleTypeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_VehicleType As VWI_VehicleType = CType(obj, VWI_VehicleType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_VehicleType.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_VehicleType As VWI_VehicleType = CType(obj, VWI_VehicleType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_VehicleType.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_VehicleType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, VWI_VehicleType.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim VWI_VehicleType As VWI_VehicleType = CType(obj, VWI_VehicleType)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_VehicleType.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_VehicleType.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_VehicleType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, VWI_VehicleType.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_VehicleType

            Dim VWI_VehicleType As VWI_VehicleType = New VWI_VehicleType

            VWI_VehicleType.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_VehicleType.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then VWI_VehicleType.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleDesc")) Then VWI_VehicleType.VehicleDesc = dr("VehicleDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VariantType")) Then VWI_VehicleType.VariantType = dr("VariantType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModel_S1")) Then VWI_VehicleType.VehicleModel_S1 = dr("VehicleModel_S1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleCategory_S2")) Then VWI_VehicleType.VehicleCategory_S2 = dr("VehicleCategory_S2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductSegment_S3")) Then VWI_VehicleType.ProductSegment_S3 = dr("ProductSegment_S3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DriveSystem_S4")) Then VWI_VehicleType.DriveSystem_S4 = dr("DriveSystem_S4").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SpeedType")) Then VWI_VehicleType.SpeedType = dr("SpeedType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FuelType")) Then VWI_VehicleType.FuelType = dr("FuelType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DriveSystemType")) Then VWI_VehicleType.DriveSystemType = dr("DriveSystemType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategory")) Then VWI_VehicleType.ProductCategory = dr("ProductCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransmitType")) Then VWI_VehicleType.TransmitType = dr("TransmitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_VehicleType.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
           

            Return VWI_VehicleType

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_VehicleType) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_VehicleType), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_VehicleType).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

