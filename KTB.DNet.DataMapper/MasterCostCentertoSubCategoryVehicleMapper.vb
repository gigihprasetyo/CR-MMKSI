
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MasterCostCentertoSubCategoryVehicle Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 30/04/2020 - 10:00:11
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

    Public Class MasterCostCentertoSubCategoryVehicleMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMasterCostCentertoSubCategoryVehicle"
        Private m_UpdateStatement As String = "up_UpdateMasterCostCentertoSubCategoryVehicle"
        Private m_RetrieveStatement As String = "up_RetrieveMasterCostCentertoSubCategoryVehicle"
        Private m_RetrieveListStatement As String = "up_RetrieveMasterCostCentertoSubCategoryVehicleList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMasterCostCentertoSubCategoryVehicle"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim masterCostCentertoSubCategoryVehicle As MasterCostCentertoSubCategoryVehicle = Nothing
            While dr.Read

                masterCostCentertoSubCategoryVehicle = Me.CreateObject(dr)

            End While

            Return masterCostCentertoSubCategoryVehicle

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim masterCostCentertoSubCategoryVehicleList As ArrayList = New ArrayList

            While dr.Read
                Dim masterCostCentertoSubCategoryVehicle As MasterCostCentertoSubCategoryVehicle = Me.CreateObject(dr)
                masterCostCentertoSubCategoryVehicleList.Add(masterCostCentertoSubCategoryVehicle)
            End While

            Return masterCostCentertoSubCategoryVehicleList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim masterCostCentertoSubCategoryVehicle As MasterCostCentertoSubCategoryVehicle = CType(obj, MasterCostCentertoSubCategoryVehicle)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, masterCostCentertoSubCategoryVehicle.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim masterCostCentertoSubCategoryVehicle As MasterCostCentertoSubCategoryVehicle = CType(obj, MasterCostCentertoSubCategoryVehicle)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, masterCostCentertoSubCategoryVehicle.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, masterCostCentertoSubCategoryVehicle.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MasterCostCenterID", DbType.Int32, Me.GetRefObject(masterCostCentertoSubCategoryVehicle.MasterCostCenter))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(masterCostCentertoSubCategoryVehicle.SubCategoryVehicle))

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

            Dim masterCostCentertoSubCategoryVehicle As MasterCostCentertoSubCategoryVehicle = CType(obj, MasterCostCentertoSubCategoryVehicle)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, masterCostCentertoSubCategoryVehicle.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, masterCostCentertoSubCategoryVehicle.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, masterCostCentertoSubCategoryVehicle.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@MasterCostCenterID", DbType.Int32, Me.GetRefObject(masterCostCentertoSubCategoryVehicle.MasterCostCenter))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(masterCostCentertoSubCategoryVehicle.SubCategoryVehicle))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MasterCostCentertoSubCategoryVehicle

            Dim masterCostCentertoSubCategoryVehicle As MasterCostCentertoSubCategoryVehicle = New MasterCostCentertoSubCategoryVehicle

            masterCostCentertoSubCategoryVehicle.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then masterCostCentertoSubCategoryVehicle.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then masterCostCentertoSubCategoryVehicle.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then masterCostCentertoSubCategoryVehicle.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then masterCostCentertoSubCategoryVehicle.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then masterCostCentertoSubCategoryVehicle.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("MasterCostCenterID")) Then
                masterCostCentertoSubCategoryVehicle.MasterCostCenter = New MasterCostCenter(CType(dr("MasterCostCenterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                masterCostCentertoSubCategoryVehicle.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Integer))
            End If

            Return masterCostCentertoSubCategoryVehicle

        End Function

        Private Sub SetTableName()

            If Not (GetType(MasterCostCentertoSubCategoryVehicle) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MasterCostCentertoSubCategoryVehicle), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MasterCostCentertoSubCategoryVehicle).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

