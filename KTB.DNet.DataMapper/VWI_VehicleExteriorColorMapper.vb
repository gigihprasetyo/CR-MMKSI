
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_VehicleExteriorColor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 13/04/2018 - 15:59:51
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

    Public Class VWI_VehicleExteriorColorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_VehicleExteriorColor"
        Private m_UpdateStatement As String = "up_UpdateVWI_VehicleExteriorColor"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_VehicleExteriorColor"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_VehicleExteriorColorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_VehicleExteriorColor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_VehicleExteriorColor As VWI_VehicleExteriorColor = Nothing
            While dr.Read

                vWI_VehicleExteriorColor = Me.CreateObject(dr)

            End While

            Return vWI_VehicleExteriorColor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_VehicleExteriorColorList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_VehicleExteriorColor As VWI_VehicleExteriorColor = Me.CreateObject(dr)
                vWI_VehicleExteriorColorList.Add(vWI_VehicleExteriorColor)
            End While

            Return vWI_VehicleExteriorColorList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_VehicleExteriorColor As VWI_VehicleExteriorColor = CType(obj, VWI_VehicleExteriorColor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_VehicleExteriorColor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_VehicleExteriorColor As VWI_VehicleExteriorColor = CType(obj, VWI_VehicleExteriorColor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vWI_VehicleExteriorColor.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorIndName", DbType.AnsiString, vWI_VehicleExteriorColor.ColorIndName)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vWI_VehicleExteriorColor.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vWI_VehicleExteriorColor.RowStatus)


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

            Dim vWI_VehicleExteriorColor As VWI_VehicleExteriorColor = CType(obj, VWI_VehicleExteriorColor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_VehicleExteriorColor.ID)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vWI_VehicleExteriorColor.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorIndName", DbType.AnsiString, vWI_VehicleExteriorColor.ColorIndName)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vWI_VehicleExteriorColor.RowStatus)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_VehicleExteriorColor

            Dim vWI_VehicleExteriorColor As VWI_VehicleExteriorColor = New VWI_VehicleExteriorColor

            vWI_VehicleExteriorColor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then vWI_VehicleExteriorColor.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorIndName")) Then vWI_VehicleExteriorColor.ColorIndName = dr("ColorIndName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vWI_VehicleExteriorColor.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_VehicleExteriorColor.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vWI_VehicleExteriorColor.RowStatus = CType(dr("RowStatus"), Short)

            Return vWI_VehicleExteriorColor

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_VehicleExteriorColor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_VehicleExteriorColor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_VehicleExteriorColor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace