
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SubCategoryVehicleToModel Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 22/01/2019 - 10:01:19
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

    Public Class SubCategoryVehicleToModelMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSubCategoryVehicleToModel"
        Private m_UpdateStatement As String = "up_UpdateSubCategoryVehicleToModel"
        Private m_RetrieveStatement As String = "up_RetrieveSubCategoryVehicleToModel"
        Private m_RetrieveListStatement As String = "up_RetrieveSubCategoryVehicleToModelList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSubCategoryVehicleToModel"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim subCategoryVehicleToModel As SubCategoryVehicleToModel = Nothing
            While dr.Read

                subCategoryVehicleToModel = Me.CreateObject(dr)

            End While

            Return subCategoryVehicleToModel

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim subCategoryVehicleToModelList As ArrayList = New ArrayList

            While dr.Read
                Dim subCategoryVehicleToModel As SubCategoryVehicleToModel = Me.CreateObject(dr)
                subCategoryVehicleToModelList.Add(subCategoryVehicleToModel)
            End While

            Return subCategoryVehicleToModelList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim subCategoryVehicleToModel As SubCategoryVehicleToModel = CType(obj, SubCategoryVehicleToModel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, subCategoryVehicleToModel.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim subCategoryVehicleToModel As SubCategoryVehicleToModel = CType(obj, SubCategoryVehicleToModel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, subCategoryVehicleToModel.VechileModelID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, subCategoryVehicleToModel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, subCategoryVehicleToModel.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, subCategoryVehicleToModel.SubCategoryVehicleID)
            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int32, Me.GetRefObject(subCategoryVehicleToModel.VechileModel))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(subCategoryVehicleToModel.SubCategoryVehicle))


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

            Dim subCategoryVehicleToModel As SubCategoryVehicleToModel = CType(obj, SubCategoryVehicleToModel)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, subCategoryVehicleToModel.ID)
            'DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, subCategoryVehicleToModel.VechileModelID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, subCategoryVehicleToModel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, subCategoryVehicleToModel.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, subCategoryVehicleToModel.SubCategoryVehicleID)

            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int32, Me.GetRefObject(subCategoryVehicleToModel.VechileModel))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(subCategoryVehicleToModel.SubCategoryVehicle))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SubCategoryVehicleToModel

            Dim subCategoryVehicleToModel As SubCategoryVehicleToModel = New SubCategoryVehicleToModel

            subCategoryVehicleToModel.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("VechileModelID")) Then subCategoryVehicleToModel.VechileModelID = CType(dr("VechileModelID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then subCategoryVehicleToModel.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then subCategoryVehicleToModel.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then subCategoryVehicleToModel.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then subCategoryVehicleToModel.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then subCategoryVehicleToModel.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
            '    subCategoryVehicleToModel.SubCategoryVehicleID = CType(dr("SubCategoryVehicleID"), Short)
            'End If

            If Not dr.IsDBNull(dr.GetOrdinal("VechileModelID")) Then
                subCategoryVehicleToModel.VechileModel = New VechileModel(CType(dr("VechileModelID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                subCategoryVehicleToModel.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Integer))
            End If

            Return subCategoryVehicleToModel

        End Function

        Private Sub SetTableName()

            If Not (GetType(SubCategoryVehicleToModel) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SubCategoryVehicleToModel), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SubCategoryVehicleToModel).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

