#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VechileType Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 25/09/2007 - 13:08:31
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

    Public Class VechileTypeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVechileType"
        Private m_UpdateStatement As String = "up_UpdateVechileType"
        Private m_RetrieveStatement As String = "up_RetrieveVechileType"
        Private m_RetrieveListStatement As String = "up_RetrieveVechileTypeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVechileType"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vechileType As VechileType = Nothing
            While dr.Read

                vechileType = Me.CreateObject(dr)

            End While

            Return vechileType

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vechileTypeList As ArrayList = New ArrayList

            While dr.Read
                Dim vechileType As VechileType = Me.CreateObject(dr)
                vechileTypeList.Add(vechileType)
            End While

            Return vechileTypeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileType As VechileType = CType(obj, VechileType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vechileType.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileType As VechileType = CType(obj, VechileType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, vechileType.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vechileType.Description)
            DbCommandWrapper.AddInParameter("@DescriptionDealer", DbType.AnsiString, vechileType.DescriptionDealer)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileType.Status)
            DBCommandWrapper.AddInParameter("@IsVehicleKind1", DbType.Byte, vechileType.IsVehicleKind1)
            DBCommandWrapper.AddInParameter("@IsVehicleKind2", DbType.Byte, vechileType.IsVehicleKind2)
            DBCommandWrapper.AddInParameter("@IsVehicleKind3", DbType.Byte, vechileType.IsVehicleKind3)
            DbCommandWrapper.AddInParameter("@IsVehicleKind4", DbType.Byte, vechileType.IsVehicleKind4)
            DbCommandWrapper.AddInParameter("@SegmentType", DbType.AnsiString, vechileType.SegmentType)
            DbCommandWrapper.AddInParameter("@VariantType", DbType.AnsiString, vechileType.VariantType)
            DbCommandWrapper.AddInParameter("@TransmitType", DbType.AnsiString, vechileType.TransmitType)
            DbCommandWrapper.AddInParameter("@DriveSystemType", DbType.AnsiString, vechileType.DriveSystemType)
            DbCommandWrapper.AddInParameter("@SpeedType", DbType.AnsiString, vechileType.SpeedType)
            DbCommandWrapper.AddInParameter("@FuelType", DbType.AnsiString, vechileType.FuelType)
            DbCommandWrapper.AddInParameter("@IsActiveOnPK", DbType.Byte, vechileType.IsActiveOnPK)
            DBCommandWrapper.AddInParameter("@MaxTOPDays", DbType.Int32, vechileType.MaxTOPDays)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@SAPModel", DbType.AnsiString, vechileType.SAPModel)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vechileType.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(vechileType.Category))
            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(vechileType.ProductCategory))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Int32, Me.GetRefObject(vechileType.VechileModel))
            DbCommandWrapper.AddInParameter("@SalesVechileModelID", DbType.Int32, Me.GetRefObject(vechileType.SalesVechileModel))
            DbCommandWrapper.AddInParameter("@VehicleClassID", DbType.Int32, Me.GetRefObject(vechileType.VehicleClass))

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

            Dim vechileType As VechileType = CType(obj, VechileType)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vechileType.ID)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, vechileType.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vechileType.Description)
            DbCommandWrapper.AddInParameter("@DescriptionDealer", DbType.AnsiString, vechileType.DescriptionDealer)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileType.Status)
            DBCommandWrapper.AddInParameter("@IsVehicleKind1", DbType.Byte, vechileType.IsVehicleKind1)
            DBCommandWrapper.AddInParameter("@IsVehicleKind2", DbType.Byte, vechileType.IsVehicleKind2)
            DBCommandWrapper.AddInParameter("@IsVehicleKind3", DbType.Byte, vechileType.IsVehicleKind3)
            DbCommandWrapper.AddInParameter("@IsVehicleKind4", DbType.Byte, vechileType.IsVehicleKind4)
            DbCommandWrapper.AddInParameter("@SegmentType", DbType.AnsiString, vechileType.SegmentType)
            DbCommandWrapper.AddInParameter("@VariantType", DbType.AnsiString, vechileType.VariantType)
            DbCommandWrapper.AddInParameter("@TransmitType", DbType.AnsiString, vechileType.TransmitType)
            DbCommandWrapper.AddInParameter("@DriveSystemType", DbType.AnsiString, vechileType.DriveSystemType)
            DbCommandWrapper.AddInParameter("@SpeedType", DbType.AnsiString, vechileType.SpeedType)
            DbCommandWrapper.AddInParameter("@FuelType", DbType.AnsiString, vechileType.FuelType)
            DbCommandWrapper.AddInParameter("@IsActiveOnPK", DbType.Byte, vechileType.IsActiveOnPK)
            DBCommandWrapper.AddInParameter("@MaxTOPDays", DbType.Int32, vechileType.MaxTOPDays)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vechileType.CreatedBy)
            DbCommandWrapper.AddInParameter("@SAPModel", DbType.AnsiString, vechileType.SAPModel)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(vechileType.Category))
            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(vechileType.ProductCategory))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Int32, Me.GetRefObject(vechileType.VechileModel))
            DbCommandWrapper.AddInParameter("@SalesVechileModelID", DbType.Int32, Me.GetRefObject(vechileType.SalesVechileModel))
            DbCommandWrapper.AddInParameter("@VehicleClassID", DbType.Int32, Me.GetRefObject(vechileType.VehicleClass))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VechileType

            Dim vechileType As VechileType = New VechileType

            vechileType.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then vechileType.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vechileType.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DescriptionDealer")) Then vechileType.DescriptionDealer = dr("DescriptionDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vechileType.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsVehicleKind1")) Then vechileType.IsVehicleKind1 = CType(dr("IsVehicleKind1"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsVehicleKind2")) Then vechileType.IsVehicleKind2 = CType(dr("IsVehicleKind2"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsVehicleKind3")) Then vechileType.IsVehicleKind3 = CType(dr("IsVehicleKind3"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsVehicleKind4")) Then vechileType.IsVehicleKind4 = CType(dr("IsVehicleKind4"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SegmentType")) Then vechileType.SegmentType = dr("SegmentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VariantType")) Then vechileType.VariantType = dr("VariantType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TransmitType")) Then vechileType.TransmitType = dr("TransmitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DriveSystemType")) Then vechileType.DriveSystemType = dr("DriveSystemType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SpeedType")) Then vechileType.SpeedType = dr("SpeedType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FuelType")) Then vechileType.FuelType = dr("FuelType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDays")) Then vechileType.MaxTOPDays = CType(dr("MaxTOPDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SAPModel")) Then vechileType.SAPModel = dr("SAPModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vechileType.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vechileType.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vechileType.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vechileType.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vechileType.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                vechileType.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                vechileType.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ModelID")) Then
                vechileType.VechileModel = New VechileModel(CType(dr("ModelID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesVechileModelID")) Then
                vechileType.SalesVechileModel = New SalesVechileModel(CType(dr("SalesVechileModelID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleClassID")) Then
                vechileType.VehicleClass = New VehicleClass(CType(dr("VehicleClassID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("IsActiveOnPK")) Then vechileType.IsActiveOnPK = CType(dr("IsActiveOnPK"), Byte)

            Return vechileType

        End Function

        Private Sub SetTableName()

            If Not (GetType(VechileType) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VechileType), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VechileType).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

