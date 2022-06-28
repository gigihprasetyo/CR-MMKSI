#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VechileTypeGeneral Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2020 - 10:06:12 AM
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

    Public Class VechileTypeGeneralMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVechileTypeGeneral"
        Private m_UpdateStatement As String = "up_UpdateVechileTypeGeneral"
        Private m_RetrieveStatement As String = "up_RetrieveVechileTypeGeneral"
        Private m_RetrieveListStatement As String = "up_RetrieveVechileTypeGeneralList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVechileTypeGeneral"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vechileTypeGeneral As VechileTypeGeneral = Nothing
            While dr.Read

                vechileTypeGeneral = Me.CreateObject(dr)

            End While

            Return vechileTypeGeneral

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vechileTypeGeneralList As ArrayList = New ArrayList

            While dr.Read
                Dim vechileTypeGeneral As VechileTypeGeneral = Me.CreateObject(dr)
                vechileTypeGeneralList.Add(vechileTypeGeneral)
            End While

            Return vechileTypeGeneralList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileTypeGeneral As VechileTypeGeneral = CType(obj, VechileTypeGeneral)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, vechileTypeGeneral.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileTypeGeneral As VechileTypeGeneral = CType(obj, VechileTypeGeneral)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, vechileTypeGeneral.SubCategoryVehicleID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vechileTypeGeneral.Name)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileTypeGeneral.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileTypeGeneral.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vechileTypeGeneral.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, Me.GetRefObject(vechileTypeGeneral.SubCategoryVehicle))


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
            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileTypeGeneral As VechileTypeGeneral = CType(obj, VechileTypeGeneral)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, vechileTypeGeneral.ID)
            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, vechileTypeGeneral.SubCategoryVehicleID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vechileTypeGeneral.Name)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileTypeGeneral.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileTypeGeneral.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vechileTypeGeneral.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, Me.GetRefObject(vechileTypeGeneral.SubCategoryVehicle))



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VechileTypeGeneral

            Dim vechileTypeGeneral As VechileTypeGeneral = New VechileTypeGeneral

            vechileTypeGeneral.ID = CType(dr("ID"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then vechileTypeGeneral.SubCategoryVehicleID = CType(dr("SubCategoryVehicleID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then vechileTypeGeneral.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vechileTypeGeneral.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vechileTypeGeneral.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vechileTypeGeneral.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vechileTypeGeneral.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vechileTypeGeneral.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vechileTypeGeneral.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                vechileTypeGeneral.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Short))
            End If


            Return vechileTypeGeneral

        End Function

        Private Sub SetTableName()

            If Not (GetType(VechileTypeGeneral) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VechileTypeGeneral), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VechileTypeGeneral).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
