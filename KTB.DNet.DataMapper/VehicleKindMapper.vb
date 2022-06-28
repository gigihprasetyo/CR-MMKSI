
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VehicleKind Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 5/11/2011 - 8:57:08 AM
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

    Public Class VehicleKindMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVehicleKind"
        Private m_UpdateStatement As String = "up_UpdateVehicleKind"
        Private m_RetrieveStatement As String = "up_RetrieveVehicleKind"
        Private m_RetrieveListStatement As String = "up_RetrieveVehicleKindList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVehicleKind"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vehicleKind As VehicleKind = Nothing
            While dr.Read

                vehicleKind = Me.CreateObject(dr)

            End While

            Return vehicleKind

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vehicleKindList As ArrayList = New ArrayList

            While dr.Read
                Dim vehicleKind As VehicleKind = Me.CreateObject(dr)
                vehicleKindList.Add(vehicleKind)
            End While

            Return vehicleKindList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vehicleKind As VehicleKind = CType(obj, VehicleKind)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vehicleKind.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vehicleKind As VehicleKind = CType(obj, VehicleKind)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DBCommandWrapper.AddInParameter("@VehicleKindGroupID", DbType.Int32, vehicleKind.VehicleKindGroupID)
            DBCommandWrapper.AddInParameter("@Code", DbType.AnsiString, vehicleKind.Code)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vehicleKind.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vehicleKind.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vehicleKind.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.Object,DateTime.Now)

            If Not IsNothing(vehicleKind.VehicleKindGroup) AndAlso vehicleKind.VehicleKindGroup.ID > 0 Then
                DbCommandWrapper.AddInParameter("@VehicleKindGroupID", DbType.Int32, Me.GetRefObject(vehicleKind.VehicleKindGroup))
            Else
                DbCommandWrapper.AddInParameter("@VehicleKindGroupID", DbType.Int32, vehicleKind.VehicleKindGroupID)
            End If

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

            Dim vehicleKind As VehicleKind = CType(obj, VehicleKind)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vehicleKind.ID)
            'DBCommandWrapper.AddInParameter("@VehicleKindGroupID", DbType.Int32, vehicleKind.VehicleKindGroupID)
            DBCommandWrapper.AddInParameter("@Code", DbType.AnsiString, vehicleKind.Code)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vehicleKind.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vehicleKind.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vehicleKind.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.Object,DateTime.Now)

            If Not IsNothing(vehicleKind.VehicleKindGroup) AndAlso vehicleKind.VehicleKindGroup.ID > 0 Then
                DbCommandWrapper.AddInParameter("@VehicleKindGroupID", DbType.Int32, Me.GetRefObject(vehicleKind.VehicleKindGroup))
            Else
                DbCommandWrapper.AddInParameter("@VehicleKindGroupID", DbType.Int32, vehicleKind.VehicleKindGroupID)
            End If

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VehicleKind

            Dim vehicleKind As VehicleKind = New VehicleKind

            vehicleKind.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindGroupID")) Then vehicleKind.VehicleKindGroupID = CType(dr("VehicleKindGroupID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then vehicleKind.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vehicleKind.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vehicleKind.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vehicleKind.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vehicleKind.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vehicleKind.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vehicleKind.LastUpdateTime = CType(dr("LastUpdateTime"), Object)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleKindGroupID")) Then
                vehicleKind.VehicleKindGroup = New VehicleKindGroup(CType(dr("VehicleKindGroupID"), Integer))
            End If
            Return vehicleKind

        End Function

        Private Sub SetTableName()

            If Not (GetType(VehicleKind) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VehicleKind), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VehicleKind).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
